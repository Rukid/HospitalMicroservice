using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisitApi.Domain;

namespace VisitApi.Data.Configuration
{
    class VisitEntityConfigruation : IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> builder)
        {
            builder.ToTable("Visit");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasDefaultValueSql("(newid())");

            builder.Property(p => p.VisitDate).IsRequired();

            builder.Property(p => p.ClientFullName).IsRequired();

            builder.Property(p => p.DoctorFullName).IsRequired();

            builder.HasOne(ci => ci.VisitType)
                .WithMany()
                .HasForeignKey(ci => ci.VisitId);
        }
    }
}
