using ClientApi.Data.Configuration;
using ClientApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ClientApi.Data.Database
{
    public class ClientContext : DbContext
    {
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<GenderType> GenderTypes { get; set; }

        public ClientContext()
        {
        }

        public ClientContext(DbContextOptions<ClientContext> options)
            : base(options)
        {
            var genders = new[]
            {
                new GenderType
                {
                    Id = 0,
                    Type = "male"
                },
                new GenderType
                {
                    Id = 1,
                    Type = "female"
                }
            };

            var clients = new[]
            {
                new Client {
                    Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                    FirstName = "Sergey",
                    LastName = "Ivanov",
                    BirthDate = new DateTime(1991, 1, 1),
                    Address = "Lenina street 52",
                    PhoneNumber = "8239485762",
                    GenderId = 0
                },
                new Client {
                    Id = Guid.Parse("334feb16-d7bb-4ca9-ab56-f4fadeb88d21"),
                    FirstName = "Ivan",
                    LastName = "Klimov",
                    BirthDate = new DateTime(1995, 5, 21),
                    Address = "Lenina street 32",
                    PhoneNumber = "8232157623",
                    GenderId = 0
                }
            };

            GenderTypes.AddRange(genders);
            Clients.AddRange(clients);
            SaveChanges();
        }  
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientEntityConfigruation());
            modelBuilder.ApplyConfiguration(new GenderTypeEntityConfiguration());
        }
    }
}
