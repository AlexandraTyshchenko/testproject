using Api.Mapper;
using BLL.Interfaces;
using BLL.Services;
using DAL.Context;
using LinguaDecks.Api.Middleware;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(options =>
          options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPersonDeletter, PersonDeletter>();
builder.Services.AddScoped<ICSVUploader,CSVUploader>();
builder.Services.AddScoped<IPersonGetter,PersonGetter>();
builder.Services.AddScoped<IPersonEditor, PersonEditor>();
var mapperConfig = new MapperConfig();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile(mapperConfig));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSwagger");
app.UseCors(options => options
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200") 
    .AllowCredentials());
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();

app.MapControllers();
UpdateDatabase(app);
app.Run();
void UpdateDatabase(WebApplication app) //for applying migrations automatically
{
    using (var serviceScope = app.Services.CreateScope())
    {
        ApplicationContext context = serviceScope.ServiceProvider.GetService<ApplicationContext>();
        context.Database.Migrate();
    }
}