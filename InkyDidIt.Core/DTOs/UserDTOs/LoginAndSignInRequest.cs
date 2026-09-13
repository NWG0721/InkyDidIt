using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkyDidIt.Core.DTOs.UserDTOs
{
	public class LoginAndSignInRequest
	{
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public CancellationToken Token { get; set; }
	}
}
