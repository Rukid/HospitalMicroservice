using System;
using System.Linq;
using ClientApi.Data.Database;
using ClientApi.Domain.Entities;

namespace ClientApi.Data.Tests.Infrastructure
{
    public class DatabaseInitializer
    {
        public static void Initialize(ClientContext context)
        {
            if (context.Clients.Any())
            {
                return;
            }

            Seed(context);
        }

        private static void Seed(ClientContext context)
        {
            var genders = new[]
            {
                new GenderType
                {
                    Id = 1,
                    Type = "male"
                },
                new GenderType
                {
                    Id = 2,
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
                    GenderId = 1
                },
                new Client {
                    Id = Guid.Parse("334feb16-d7bb-4ca9-ab56-f4fadeb88d21"),
                    FirstName = "Ivan",
                    LastName = "Klimov",
                    BirthDate = new DateTime(1995, 5, 21),
                    Address = "Lenina street 32",
                    PhoneNumber = "8232157623",
                    GenderId = 1
                }
            };

            context.GenderTypes.AddRange(genders);
            context.Clients.AddRange(clients);
            context.SaveChanges();
        }
    }
}
