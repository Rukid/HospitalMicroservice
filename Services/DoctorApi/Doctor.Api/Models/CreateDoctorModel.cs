using System;
using System.ComponentModel.DataAnnotations;

namespace DoctorApi.Models
{
    public class CreateDoctorModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Speciality { get; set; }
    }
}
