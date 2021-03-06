﻿using System;
using Microsoft.EntityFrameworkCore;
using VisitApi.Data.Configuration;
using VisitApi.Domain;

namespace VisitApi.Data.Database
{
    public class VisitContext : DbContext
    {
        public VisitContext()
        {
        }

        public VisitContext(DbContextOptions<VisitContext> options)
            : base(options)
        {
            var visitTypes = new[]
            {
                new VisitType()
                {
                    Id = 1,
                    VisitName = "fisrt"
                },
                new VisitType
                {
                    Id = 2,
                    VisitName = "repeated"
                }
            };

            var orders = new[]
            {
                new Visit
                {
                    Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                    ClientGuid = Guid.Parse("d3e3137e-ccc9-488c-9e89-50ba354738c2"),
                    ClientFullName = "Sergey Ivanov",
                    DoctorGuid = Guid.Parse("30bf946d-046f-4a47-8130-f876095ccb51"),
                    DoctorFullName = "Alexey Petrow",
                    VisitDate = new DateTime(2020, 10, 10),
                    VisitId = 0,
                },
                new Visit
                {
                    Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    ClientGuid = Guid.Parse("334feb16-d7bb-4ca9-ab56-f4fadeb88d21"),
                    ClientFullName = "Ivan Klimov",
                    DoctorGuid = Guid.Parse("30bf946d-046f-4a47-8130-f876095ccb51"),
                    DoctorFullName = "Alexey Petrow",
                    VisitDate = new DateTime(2020, 10, 10),
                    VisitId = 1,
                    Diagnosis = "Drink vodka every day"
                }
            };

            VisitTypes.AddRange(visitTypes);
            Visits.AddRange(orders);
            SaveChanges();
        }

        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<VisitType> VisitTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VisitEntityConfigruation());
            modelBuilder.ApplyConfiguration(new VisitTypeEntityConfiguration());
        }
    }
}