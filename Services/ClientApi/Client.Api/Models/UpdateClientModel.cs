﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ClientApi.Models
{
    public class UpdateClientModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int GenderId { get; set; }
    }
}
