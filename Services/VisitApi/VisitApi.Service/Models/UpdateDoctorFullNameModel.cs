using System;

namespace VisitApi.Service.Models
{
    public class UpdateDoctorFullNameModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
    }
}
