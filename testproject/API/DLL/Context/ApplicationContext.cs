using DAL.enities;
using DAL.EntityConfigurations;

using Microsoft.EntityFrameworkCore;

    namespace DAL.Context
    {
        public class ApplicationContext : DbContext
        {
            public ApplicationContext(DbContextOptions<ApplicationContext> options)
                : base(options)
            {

            }
            public DbSet<Person> People { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.ApplyConfiguration(new PersonConfiguration());
            }

        }
    }


