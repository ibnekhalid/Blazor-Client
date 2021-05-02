﻿using System.ComponentModel.DataAnnotations;

namespace app.Core.Moldes.Account
{
	public class RegisterModel : LoginModel
	{
		[Required]
		public string CompanyName { get; set; }
		
		[Required]
		public string Email { get; set; }
		[Required]
		[Compare("Password")]
		[RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]

		public string ConfirmPassword { get; set; }
	}
}
