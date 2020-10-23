using ClientApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientApi.Data.Configuration
{
    class GenderTypeEntityConfiguration : IEntityTypeConfiguration<GenderType>
    {
        public void Configure(EntityTypeBuilder<GenderType> builder)
        {
            builder.ToTable("CatalogType");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .ValueGeneratedNever();

            builder.Property(cb => cb.Type)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new GenderType { Id = 0, Type = "male"},
                new GenderType { Id = 1, Type = "female"});
        }
    }
}
