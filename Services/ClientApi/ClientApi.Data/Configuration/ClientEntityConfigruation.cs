using ClientApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClientApi.Data.Configuration
{
    class ClientEntityConfigruation : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

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

            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(10);

            builder.HasOne(ci => ci.Gender)
                .WithMany()
                .HasForeignKey(ci => ci.GenderId);

            //builder.HasData(
            //    new Client {
            //        Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
            //        FirstName = "Sergey",
            //        LastName = "Ivanov",
            //        BirthDate = new DateTime(1991, 1, 1),
            //        Address = "Lenina street 52",
            //        PhoneNumber = "8239485762",
            //        GenderId = 1
            //    },
            //    new Client {
            //        Id = Guid.Parse("334feb16-d7bb-4ca9-ab56-f4fadeb88d21"),
            //        FirstName = "Ivan",
            //        LastName = "Klimov",
            //        BirthDate = new DateTime(1995, 5, 21),
            //        Address = "Lenina street 32",
            //        PhoneNumber = "8232157623",
            //        GenderId = 1
            //    });
        }
    }
}
