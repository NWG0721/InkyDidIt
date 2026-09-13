using InkyDidIt.Core.DTOs.UserDTOs;
using InkyDidIt.Core.Entities.UserEntities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkyDidIt.Core.Interfaces.Services
{
	public interface ITokenBlacklistService
	{
		public Task BlacklistTokenAsync(string jti, TimeSpan ttl);
		public Task<bool> IsBlacklistedAsync(string jti);
	}
}
