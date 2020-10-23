using System;
using DoctorApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DoctorApi.Data.Configuration
{
    class DoctorEntityConfigruation : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasDefaultValueSql("(newid())"); 

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.MiddleName)
                .HasMaxLength(50);

            builder.Property(p => p.BirthDate)
                .IsRequired();

            builder.Property(p => p.Speciality)
                .HasMaxLength(30);
        }
    }
}
