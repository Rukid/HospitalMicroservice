using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisitApi.Domain;

namespace VisitApi.Data.Configuration
{
    class VisitTypeEntityConfiguration : IEntityTypeConfiguration<VisitType>
    {
        public void Configure(EntityTypeBuilder<VisitType> builder)
        {
            builder.ToTable("VisitType");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.Id)
                .ValueGeneratedNever();

            builder.Property(cb => cb.VisitName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
