using InkyDidIt.Core.Interfaces.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkyDidIt.BLL.Services
{
	public class TokenBlacklistService : ITokenBlacklistService
	{
		private readonly IConnectionMultiplexer _redis;

		public TokenBlacklistService(IConnectionMultiplexer redis)
		{
			_redis = redis;
		}

		public async Task BlacklistTokenAsync(string jti, TimeSpan ttl)
		{
			var db = _redis.GetDatabase();
			await db.StringSetAsync($"blacklist:{jti}", "1", ttl);
		}

		public async Task<bool> IsBlacklistedAsync(string jti)
		{
			var db = _redis.GetDatabase();
			return await db.KeyExistsAsync($"blacklist:{jti}");
		}
	}
}
