using DoctorApi.Data.Configuration;
using DoctorApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DoctorApi.Data.Database
{
    public class DoctorContext : DbContext
    {
        public virtual DbSet<Doctor> Doctors { get; set; }

        public DoctorContext()
        {
        }

        public DoctorContext(DbContextOptions<DoctorContext> options)
            : base(options)
        {
            var doctors = new[]
            {
                new Doctor {
                    Id = Guid.Parse("30bf946d-046f-4a47-8130-f876095ccb51"),
                    FirstName = "Sergey",
                    LastName = "Ivanov",
                    BirthDate = new DateTime(1991, 1, 1),
                    Speciality = "surgeon"
                },
                new Doctor {
                    Id = Guid.Parse("0ddb3f76-eae4-4927-be6b-bff5115bcabc"),
                    FirstName = "Ivan",
                    LastName = "Klimov",
                    BirthDate = new DateTime(1995, 5, 21),
                    Speciality = "therapist"
                }
            };
          
            Doctors.AddRange(doctors);
            SaveChanges();
        }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DoctorEntityConfigruation());
        }
    }
}
