using System;

namespace ClientApi.Domain.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int GenderId { get; set; }
        public GenderType Gender { get; set; }
    }
}
