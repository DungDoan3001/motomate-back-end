﻿using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.DTOs.RequestModels
{
    public class UserRequestModel
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Picture { get; set; }

        public DateTime? DateOfBirth { get; set; } = null;
    }
}
