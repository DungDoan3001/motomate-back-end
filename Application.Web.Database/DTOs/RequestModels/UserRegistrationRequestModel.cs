﻿using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class UserRegistrationRequestModel
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		[Required(ErrorMessage = "Username is required.")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		public string Password { get; set; }
		[Required(ErrorMessage = "Password confirmation is required.")]
		public string PasswordConfirm { get; set; }
		[Required(ErrorMessage = "Email is required.")]
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
	}
}
