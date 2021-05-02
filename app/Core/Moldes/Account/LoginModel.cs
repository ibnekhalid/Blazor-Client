using System.ComponentModel.DataAnnotations;

namespace app.Core.Moldes.Account
{
	public class LoginModel
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
