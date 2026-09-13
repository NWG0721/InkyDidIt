using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using InkyDidIt.Core.Common.Results;
using InkyDidIt.Core.DTOs.UserDTOs;
using InkyDidIt.Core.Entities.UserEntities;
using InkyDidIt.Core.Interfaces.Repositories;
using InkyDidIt.Core.Interfaces.Services;
using InkyDidIt.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data;

namespace InkyDidIt.BLL.Services
{
	public class UserService : IUserService
	{

		#region Constructor
		private readonly IUnitOfWork _unitOfWork;
		private readonly IConfiguration _configuration;
		private readonly IUserRoleService _userRoleService;
		private readonly ITokenBlacklistService _tokenBlacklistService;
		public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, IUserRoleService userRoleService, ITokenBlacklistService tokenBlacklistService)
		{
			_unitOfWork = unitOfWork;
			_configuration = configuration;
			_userRoleService = userRoleService;
			_tokenBlacklistService = tokenBlacklistService;
		}

		#endregion

		#region ADD USER

		public async Task<OperationResult<User>> InsertAsync(User entity, CancellationToken cancellationToken)
		{
			var extractResult = new OperationResult<User>();
			var result = await _unitOfWork.genericRepository<User>().InsertAsync(entity, cancellationToken);
			if (result.Code == OperationCode.InsertSuccess)
			{
				extractResult.Success = true;
				extractResult.Data = entity;
				extractResult.Code = result.Code;
				extractResult.Message = "User added successfully.";

			}
			if (result.Code == OperationCode.InsertNullArgument)
			{
				extractResult.Success = false;
				extractResult.Data = entity;
				extractResult.Code = result.Code;
				extractResult.Message = "Null exception detected";
			}
			if (result.Code == OperationCode.Canceled)
			{
				extractResult.Success = false;
				extractResult.Data = entity;
				extractResult.Code = result.Code;
				extractResult.Message = "Canceled";

			}
			if (result.Code == OperationCode.InsertFailed)
			{
				extractResult.Success = false;
				extractResult.Data = entity;
				extractResult.Code = result.Code;
				if (result.Exception is not null)
				{
					if (result.Exception is DbUpdateException dbEx)
					{
						if (dbEx.InnerException is Npgsql.PostgresException pgEx)
						{
							extractResult.Message = pgEx.Message;
						}
					}
				}

			}
			return extractResult;
		}

		public async Task<OperationResult<User>> InsertRangeAsync(IEnumerable<User> entities, CancellationToken cancellationToken)
		{
			var extractResult = new OperationResult<User>();
			var result = await _unitOfWork.genericRepository<User>().InsertRangeAsync(entities, cancellationToken);
			if (result.Code == OperationCode.InsertManySuccess)
			{
				extractResult.Success = true;
				extractResult.DataList = entities;
				extractResult.Code = result.Code;
				extractResult.Message = "Users added successfully.";

			}
			if (result.Code == OperationCode.InsertManyNullArgument)
			{
				extractResult.Success = false;
				extractResult.DataList = entities;
				extractResult.Code = result.Code;
				extractResult.Message = "Null exception detected";
			}
			if (result.Code == OperationCode.Canceled)
			{
				extractResult.Success = false;
				extractResult.DataList = entities;
				extractResult.Code = result.Code;
				extractResult.Message = "Canceled";
			}
			if (result.Code == OperationCode.InsertManyFailed)
			{
				extractResult.Success = false;
				extractResult.DataList = entities;
				extractResult.Code = result.Code;
				if (result.Exception is not null)
				{
					if (result.Exception is DbUpdateException dbEx)
					{
						if (dbEx.InnerException is Npgsql.PostgresException pgEx)
						{
							extractResult.Message = pgEx.Message;
						}
					}
				}

			}
			return extractResult;
		}

		#endregion

		#region GET USER

		public async Task<OperationResult<User>> SelectAll(bool onlySoftDeleted, Expression<Func<User, bool>>? predicate, CancellationToken cancellationToken)
		{
			var extractResult = new OperationResult<User>();
			var result = await _unitOfWork.genericRepository<User>().SelectAll(onlySoftDeleted, predicate, cancellationToken);
			if (result.Code == OperationCode.SelectSuccess)
			{
				extractResult.Success = true;
				extractResult.DataList = result.DataList;
				extractResult.Code = result.Code;
				extractResult.Message = "Data has fetched successfully";

			}
			if (result.Code == OperationCode.SelectNullResult)
			{
				extractResult.Success = false;
				extractResult.DataList = result.DataList;
				extractResult.Code = result.Code;
				extractResult.Message = "Found nothing...";
			}
			if (result.Code == OperationCode.Canceled)
			{
				extractResult.Success = false;
				extractResult.DataList = result.DataList;
				extractResult.Code = result.Code;
				extractResult.Message = "Canceled";
			}
			if (result.Code == OperationCode.SelectFailed)
			{
				extractResult.Success = false;
				extractResult.DataList = result.DataList;
				extractResult.Code = result.Code;
				if (result.Exception is not null)
				{
					if (result.Exception is DbUpdateException dbEx)
					{
						if (dbEx.InnerException is Npgsql.PostgresException pgEx)
						{
							extractResult.Message = pgEx.Message;
						}
					}
				}

			}
			return extractResult;
		}

		public async Task<OperationResult<User>> SelectById(bool onlySoftDeleted, Guid id, CancellationToken cancellationToken)
		{
			var extractResult = new OperationResult<User>();
			var result = await _unitOfWork.genericRepository<User>().SelectById(onlySoftDeleted, id, cancellationToken);
			if (result.Code == OperationCode.SelectSuccess)
			{
				extractResult.Success = true;
				extractResult.Data = result.Data;
				extractResult.Code = OperationCode.SelectSuccess;
				extractResult.Message = "Data has fetched successfully";

			}
			if (result.Code == OperationCode.SelectNullResult)
			{
				extractResult.Success = false;
				extractResult.Data = result.Data;
				extractResult.Code = OperationCode.SelectNullResult;
				extractResult.Message = "Found nothing...";
			}
			if (result.Code == OperationCode.Canceled)
			{
				extractResult.Success = false;
				extractResult.Data = result.Data;
				extractResult.Code = OperationCode.Canceled;
				extractResult.Message = "Canceled";
			}
			if (result.Code == OperationCode.SelectFailed)
			{
				extractResult.Success = false;
				extractResult.Data = result.Data;
				extractResult.Code = OperationCode.SelectFailed;
				if (result.Exception is not null)
				{
					if (result.Exception is DbUpdateException dbEx)
					{
						if (dbEx.InnerException is Npgsql.PostgresException pgEx)
						{
							extractResult.Message = pgEx.Message;
						}
					}
				}

			}
			return extractResult;
		}

		#endregion

		#region EDIT USER

		public async Task<OperationResult<User>> UpdateAsync(User entity, CancellationToken cancellationToken)
		{
			var extractResult = new OperationResult<User>();
			var result = await _unitOfWork.genericRepository<User>().UpdateAsync(entity, cancellationToken);
			if (result.Code == OperationCode.UpdateSuccess)
			{
				extractResult.Success = true;
				extractResult.Data = result.Data;
				extractResult.Code = OperationCode.UpdateSuccess;
				extractResult.Message = "Data has updated successfully";

			}
			if (result.Code == OperationCode.UpdateNullArgument)
			{
				extractResult.Success = false;
				extractResult.Data = result.Data;
				extractResult.Code = OperationCode.UpdateNullArgument;
				extractResult.Message = "object contains nothing...";
			}
			if (result.Code == OperationCode.Canceled)
			{
				extractResult.Success = false;
				extractResult.Data = result.Data;
				extractResult.Code = OperationCode.Canceled;
				extractResult.Message = "Canceled";
			}
			if (result.Code == OperationCode.UpdateFailed)
			{
				extractResult.Success = false;
				extractResult.Data = result.Data;
				extractResult.Code = OperationCode.UpdateFailed;
				if (result.Exception is not null)
				{
					if (result.Exception is DbUpdateException dbEx)
					{
						if (dbEx.InnerException is Npgsql.PostgresException pgEx)
						{
							extractResult.Message = pgEx.Message;
						}
					}
				}

			}
			return extractResult;
		}

		public async Task<OperationResult<User>> UpdateRangeAsync(IEnumerable<User> entities, CancellationToken cancellationToken)
		{
			var extractResult = new OperationResult<User>();
			var result = await _unitOfWork.genericRepository<User>().UpdateRangeAsync(entities, cancellationToken);
			if (result.Code == OperationCode.UpdateSuccess)
			{
				extractResult.Success = true;
				extractResult.DataList = result.DataList;
				extractResult.Code = OperationCode.UpdateSuccess;
				extractResult.Message = "All data has updated successfully";
			}
			if (result.Code == OperationCode.UpdateNullArgument)
			{
				extractResult.Success = false;
				extractResult.DataList = result.DataList;
				extractResult.Code = OperationCode.UpdateNullArgument;
				extractResult.Message = "object contains nothing...";
			}
			if (result.Code == OperationCode.Canceled)
			{
				extractResult.Success = false;
				extractResult.DataList = result.DataList;
				extractResult.Code = OperationCode.Canceled;
				extractResult.Message = "Canceled";
			}
			if (result.Code == OperationCode.UpdateFailed)
			{
				extractResult.Success = false;
				extractResult.DataList = result.DataList;
				extractResult.Code = OperationCode.UpdateFailed;
				if (result.Exception is not null)
				{
					if (result.Exception is DbUpdateException dbEx)
					{
						if (dbEx.InnerException is Npgsql.PostgresException pgEx)
						{
							extractResult.Message = pgEx.Message;
						}
					}
				}

			}
			return extractResult;
		}

		#endregion

		#region DELETE USER

		public async Task<OperationResult<User>> DeleteAsync(bool hardDelete, User entity, CancellationToken cancellationToken)
		{
			var extractResult = new OperationResult<User>();
			var result = await _unitOfWork.genericRepository<User>().DeleteAsync(hardDelete, entity, cancellationToken);
			if (result.Code == OperationCode.SoftDeleteSuccess || result.Code == OperationCode.HardDeleteSuccess)
			{
				extractResult.Success = true;
				extractResult.Data = result.Data;
				extractResult.Code = result.Code;
				extractResult.Message = $"All data has {(result.Code == OperationCode.HardDeleteSuccess ? "hard" : "soft")}deleted successfully";
			}
			if (result.Code == OperationCode.HardDeleteNullArgument || result.Code == OperationCode.SoftDeleteNullArgument)
			{
				extractResult.Success = false;
				extractResult.Data = result.Data;
				extractResult.Code = result.Code;
				extractResult.Message = "object contains nothing...";
			}
			if (result.Code == OperationCode.Canceled)
			{
				extractResult.Success = false;
				extractResult.DataList = result.DataList;
				extractResult.Code = result.Code;
				extractResult.Message = "Canceled";
			}
			if (result.Code == OperationCode.HardDeleteFailed || result.Code == OperationCode.SoftDeleteFailed)
			{
				extractResult.Success = false;
				extractResult.DataList = result.DataList;
				extractResult.Code = result.Code;
				if (result.Exception is not null)
				{
					if (result.Exception is DbUpdateException dbEx)
					{
						if (dbEx.InnerException is Npgsql.PostgresException pgEx)
						{
							extractResult.Message = pgEx.Message;
						}
					}
				}

			}
			return extractResult;
		}

		public async Task<OperationResult<User>> DeleteAsync(bool hardDelete, Guid id, CancellationToken cancellationToken)
		{
			var extractResult = new OperationResult<User>();
			var result = await _unitOfWork.genericRepository<User>().DeleteAsync(hardDelete, id, cancellationToken);
			if (result.Code == OperationCode.SoftDeleteSuccess || result.Code == OperationCode.HardDeleteSuccess)
			{
				extractResult.Success = true;
				extractResult.Data = result.Data;
				extractResult.Code = result.Code;
				extractResult.Message = $"All data has {(result.Code == OperationCode.HardDeleteSuccess ? "hard" : "soft")}deleted successfully";
			}
			if (result.Code == OperationCode.HardDeleteNullArgument || result.Code == OperationCode.SoftDeleteNullArgument)
			{
				extractResult.Success = false;
				extractResult.Data = result.Data;
				extractResult.Code = result.Code;
				extractResult.Message = "object contains nothing...";
			}
			if (result.Code == OperationCode.Canceled)
			{
				extractResult.Success = false;
				extractResult.DataList = result.DataList;
				extractResult.Code = result.Code;
				extractResult.Message = "Canceled";
			}
			if (result.Code == OperationCode.HardDeleteFailed || result.Code == OperationCode.SoftDeleteFailed)
			{
				extractResult.Success = false;
				extractResult.DataList = result.DataList;
				extractResult.Code = result.Code;
				if (result.Exception is not null)
				{
					if (result.Exception is DbUpdateException dbEx)
					{
						if (dbEx.InnerException is Npgsql.PostgresException pgEx)
						{
							extractResult.Message = pgEx.Message;
						}
					}
				}

			}
			return extractResult;
		}

		public async Task<OperationResult<User>> DeleteRangeAsync(bool hardDelete, IEnumerable<User> entities, CancellationToken cancellationToken)
		{
			var extractResult = new OperationResult<User>();
			var result = await _unitOfWork.genericRepository<User>().DeleteRangeAsync(hardDelete, entities, cancellationToken);
			if (result.Code == OperationCode.SoftDeleteSuccess || result.Code == OperationCode.HardDeleteManySuccess)
			{
				extractResult.Success = true;
				extractResult.Data = result.Data;
				extractResult.Code = result.Code;
				extractResult.Message = $"All data has {(result.Code == OperationCode.HardDeleteManySuccess ? "hard" : "soft")}deleted successfully";
			}
			if (result.Code == OperationCode.HardDeleteManyNullArgument || result.Code == OperationCode.SoftDeleteNullArgument)
			{
				extractResult.Success = false;
				extractResult.Data = result.Data;
				extractResult.Code = result.Code;
				extractResult.Message = "The list contains nothing...";
			}
			if (result.Code == OperationCode.Canceled)
			{
				extractResult.Success = false;
				extractResult.DataList = result.DataList;
				extractResult.Code = result.Code;
				extractResult.Message = "Canceled";
			}
			if (result.Code == OperationCode.HardDeleteManyFailed || result.Code == OperationCode.SoftDeleteFailed)
			{
				extractResult.Success = false;
				extractResult.DataList = result.DataList;
				extractResult.Code = result.Code;
				if (result.Exception is not null)
				{
					if (result.Exception is DbUpdateException dbEx)
					{
						if (dbEx.InnerException is Npgsql.PostgresException pgEx)
						{
							extractResult.Message = pgEx.Message;
						}
					}
				}

			}
			return extractResult;
		}

		public async Task<OperationResult<User>> DeleteRangeAsync(bool hardDelete, IEnumerable<Guid> ids, CancellationToken cancellationToken)
		{
			var extractResult = new OperationResult<User>();
			var result = await _unitOfWork.genericRepository<User>().DeleteRangeAsync(hardDelete, ids, cancellationToken);
			if (result.Code == OperationCode.SoftDeleteSuccess || result.Code == OperationCode.HardDeleteManySuccess)
			{
				extractResult.Success = true;
				extractResult.Data = result.Data;
				extractResult.Code = result.Code;
				extractResult.Message = $"All data has {(result.Code == OperationCode.HardDeleteManySuccess ? "hard" : "soft")}deleted successfully";
			}
			if (result.Code == OperationCode.HardDeleteManyNullArgument || result.Code == OperationCode.SoftDeleteNullArgument)
			{
				extractResult.Success = false;
				extractResult.Data = result.Data;
				extractResult.Code = result.Code;
				extractResult.Message = "The list contains nothing...";
			}
			if (result.Code == OperationCode.Canceled)
			{
				extractResult.Success = false;
				extractResult.DataList = result.DataList;
				extractResult.Code = result.Code;
				extractResult.Message = "Canceled";
			}
			if (result.Code == OperationCode.HardDeleteManyFailed || result.Code == OperationCode.SoftDeleteFailed)
			{
				extractResult.Success = false;
				extractResult.DataList = result.DataList;
				extractResult.Code = result.Code;
				if (result.Exception is not null)
				{
					if (result.Exception is DbUpdateException dbEx)
					{
						if (dbEx.InnerException is Npgsql.PostgresException pgEx)
						{
							extractResult.Message = pgEx.Message;
						}
					}
				}

			}
			return extractResult;
		}

		#endregion

		#region RESTORE DELETED USER

		public async Task<OperationResult<User>> RestoreAsync(Guid id, CancellationToken token)
		{
			var extractResult = new OperationResult<User>();
			var result = await _unitOfWork.genericRepository<User>().RestoreAsync(id, token);
			if (result.Code == OperationCode.RestoreSuccess)
			{
				extractResult.Success = true;
				extractResult.Data = result.Data;
				extractResult.Code = result.Code;
				extractResult.Message = "Data restored successfully";
			}
			if (result.Code == OperationCode.RestoreNullArgument)
			{
				extractResult.Success = false;
				extractResult.Data = result.Data;
				extractResult.Code = result.Code;
				extractResult.Message = "The list contains nothing...";
			}
			if (result.Code == OperationCode.Canceled)
			{
				extractResult.Success = false;
				extractResult.DataList = result.DataList;
				extractResult.Code = result.Code;
				extractResult.Message = "Canceled";
			}
			if (result.Code == OperationCode.RestoreFailed)
			{
				extractResult.Success = false;
				extractResult.DataList = result.DataList;
				extractResult.Code = result.Code;
				if (result.Exception is not null)
				{
					if (result.Exception is DbUpdateException dbEx)
					{
						if (dbEx.InnerException is Npgsql.PostgresException pgEx)
						{
							extractResult.Message = pgEx.Message;
						}
					}
				}

			}
			return extractResult;
		}

		public Task<OperationResult<User>> RestoreAsync(User entity, CancellationToken token)
		{
			throw new NotImplementedException();
		}

		public Task<OperationResult<User>> RestoreManyAsync(Expression<Func<User, bool>> predicate, CancellationToken token)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region LOGIN
		public async Task<AuthenticationResult> LoginUserAsync(LoginAndSignInRequest loginRequest, CancellationToken cancellationToken)
		{
			AuthenticationResult result = new AuthenticationResult();
			OperationResult<User> dataResult;

			if (loginRequest.Username is not null && loginRequest.Password is not null)
			{
				var findUser = await _unitOfWork.genericRepository<User>().SelectAll(false, u => u.Username == loginRequest.Username, cancellationToken);
				var user = findUser.DataList?.FirstOrDefault();
				if (user is not null)
				{
					bool isValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password);
					if (!isValid)
					{
						dataResult = new OperationResult<User>(false, "Invalid username or password");
						result.Result = dataResult;
					}
					else
					{
						result = JWTTokenGenerator(user.Id, user.Username);

					}

				}
				else
				{
					dataResult = new OperationResult<User>(false, "Invalid username or password");
					result.Result = dataResult;
				}
			}
			else
			{
				dataResult = new OperationResult<User>(false, "Username or password are empty.");
				result.Result = dataResult;
			}
			return result;
		}

		#endregion

		#region SIGN UP
		public async Task<AuthenticationResult> RegisterUserAsync(LoginAndSignInRequest registerRequest, CancellationToken token)
		{
			AuthenticationResult result = new AuthenticationResult();
			OperationResult<User> dataResult = new OperationResult<User>();

			if (registerRequest.Username is not null && registerRequest.Password is not null)
			{
				bool userTakenBefore = false;

				var userFinder = await _unitOfWork.genericRepository<User>().SelectAll(false, u => u.Username == registerRequest.Username, token);
				if (userFinder.DataList.FirstOrDefault() is not null)
					userTakenBefore = true;
				if (userTakenBefore)
					dataResult = new OperationResult<User>(false, "Username has already taken");
				result.Result = dataResult;

				if (!userTakenBefore)
				{
					OperationResult<UserRole> role = await _userRoleService.RoleFinder(Guid.Parse("11111111-1111-1111-1111-111111111111"), token);

					if (role.Data != null)
					{
						User user = new User()
						{
							Username = registerRequest.Username,
							Password = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password),
							Role = role.Data,
						};

						var whatTheResult = await _unitOfWork.genericRepository<User>().InsertAsync(user, token);
						if (whatTheResult.Success == true)
						{
							result = JWTTokenGenerator(user.Id, user.Username);
						}
						else
						{
							result.Result = new OperationResult<User> { Success = false, Message = "Something went wrong." };
							return result;
						}
					}
					else
					{
						dataResult = new OperationResult<User>(false, "Role did not find");
						result.Result = dataResult;
					}

				}

			}
			else
			{
				dataResult = new OperationResult<User>(false, "empty username or password.");
				result.Result = dataResult;

			}

			return result;
		}

		#endregion

		#region LOG OUT
		//public Task<AuthenticationResult> LogOutAsync(Guid userId, CancellationToken cancellationToken)
		//{
		//	throw new NotImplementedException();
		//}

		//public Task<AuthenticationResult> LogOutAsync(string username, CancellationToken cancellationToken)
		//{
		//	throw new NotImplementedException();
		//}

		public async Task<AuthenticationResult> LogOutAsync(string token, CancellationToken cancellationToken)
		{
			AuthenticationResult result = new AuthenticationResult();

			var handler = new JwtSecurityTokenHandler();
			var jwtToken = handler.ReadJwtToken(token);
			string? jti = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
			await _tokenBlacklistService.BlacklistTokenAsync(jti, TimeSpan.FromHours(2));
			result.Result = new OperationResult<User>(true, "Logout successfully!");

			return result;
		}

		#endregion


		#region Change Role

		public async Task<OperationResult<User>> ChangeRole(Guid roleId, Guid userId)
		{
			OperationResult<User> result = new OperationResult<User>();

			// soon...

			return result;
		}

		#endregion


		#region JWT Token Generator

		/// <summary>
		/// JWT Tokens are being generated here.
		/// </summary>
		/// <param name="userId">Target user's Id.</param>
		/// <param name="userName">Target user's username.</param>
		/// <returns>An <see cref="AuthenticationResult"/>, including data and JWT Token be used to demonstrate logs and carrying generated JWT token which had been requested from any of containing class methods.</returns>
		AuthenticationResult JWTTokenGenerator(Guid userId, string userName)
		{
			AuthenticationResult result = new AuthenticationResult();
			var claims = new[] {
							new Claim(ClaimTypes.NameIdentifier , userId.ToString()),
							new Claim(ClaimTypes.Name ,userName),
							new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
						};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT_KEY"]));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var tokenObject = new JwtSecurityToken(
				issuer: _configuration["JWT_ISSUER"],
				audience: _configuration["JWT_AUDIENCE"],
				claims: claims,
				expires: DateTime.UtcNow.AddHours(2),
				signingCredentials: creds);

			string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
			var dataResult = new OperationResult<User>(true, "Login successfully!");
			result.Result = dataResult;
			result.Token = token;
			return result;
		}
		#endregion

		#region
		#endregion

		#region
		#endregion
	}
}
