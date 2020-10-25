using System;
using System.ComponentModel.DataAnnotations;

namespace VisitApi.Models
{
    public class VisitModel
    {
        [Required] 
        public Guid ClientGuid { get; set; }

        [Required]
        public string ClientFullName { get; set; }

        [Required]
        public Guid DoctorGuid { get; set; }

        [Required]
        public string DoctorFullName { get; set; }
    }
}