using InkyDidIt.Core.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkyDidIt.Core.Common.Results
{
	public class AuthenticationResult
	{
		public OperationResult<User> Result { get; set; }

		public string? Token { get; set; }
	}
}
