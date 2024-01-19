
using DAL.enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EntityConfigurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.DateOfBirth).IsRequired();
            builder.Property(x => x.IsMarried).IsRequired();
            builder.Property(x => x.Phone).IsRequired();
            builder.Property(x => x.Salary).HasColumnType("decimal(18, 2)").IsRequired();
        }
    }
}
