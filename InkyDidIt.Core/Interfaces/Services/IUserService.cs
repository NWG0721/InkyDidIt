using InkyDidIt.Core.Common.Results;
using InkyDidIt.Core.DTOs.UserDTOs;
using InkyDidIt.Core.Entities;
using InkyDidIt.Core.Entities.UserEntities;
using InkyDidIt.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InkyDidIt.Core.Interfaces.Services
{
	/// <summary>
	/// User services Interface for operating logical tasks. Contains:<br/>
	///  -> Login<br/>
	///  -> Sign in<br/>
	///  -> Forget password<br/>
	///  -> JWT token manager(just some if it's settings) <br/>
	///  -> User Client side web application settings(such as, changing password or username)
	/// </summary>
	public interface IUserService : IGenericRepository<User>
	{

		//------------------| Services and logic |------------------\\

		#region RegisterUser

		/// <summary>
		/// Registers new user in the database.<br/>
		/// -> Checks if the username is already taken<br/>
		/// -> Checks the password once again.<br/>
		/// -> Hashes the password.<br/>
		/// -> Generates a JWT token to send to the front-end.<br/>
		/// </summary>
		/// <param name="registerRequest">DTO containing username and password, sent from the front-end.</param>
		/// <param name="token">Token to signal if the operation should be cancelled.</param>
		/// <returns>An <see cref="AuthenticationResult"/>, including data, JWT and logs for exception handling.</returns>
		public Task<AuthenticationResult> RegisterUserAsync(LoginAndSignInRequest registerRequest, CancellationToken token);

		#endregion


		#region LoginUser

		/// <summary>
		/// Logs the user into the web app.<br/>
		/// -> Checks if the username exists.<br/>
		/// -> rechecking password once again.<br/>
		/// -> Verifies the password against the stored hash.
		/// </summary>
		/// <param name="token">Token to signal if the operation should be cancelled.</param>
		/// <param name="loginRequest">DTO containing username and password, sent from the front-end.</param>
		/// <returns>An <see cref="AuthenticationResult"/>, including data, JWT and useful logs for exception handling.</returns>
		public Task<AuthenticationResult> LoginUserAsync(LoginAndSignInRequest loginRequest, CancellationToken token);

		#endregion


		#region LogOut

		///// <summary>
		///// Logs the use out of web app.
		///// </summary>
		///// <param name="userId">Using id for finding user object.</param>
		///// <param name="cancellationToken">Token to signal if the operation should be cancelled.</param>
		///// <returns>An <see cref="AuthenticationResult"/>, including data, JWT and useful logs for exception handling.</returns>
		//public Task<AuthenticationResult> LogOutAsync(Guid userId, CancellationToken cancellationToken);

		///// <summary>
		///// Logs the use out of web app. 
		///// </summary>
		///// <param name="username">Using username for finding user object.</param>
		///// <param name="cancellationToken">Token to signal if the operation should be cancelled.</param>
		///// <returns>An <see cref="AuthenticationResult"/>, including data, JWT and logs for exception handling.</returns>
		//public Task<AuthenticationResult> LogOutAsync(string username, CancellationToken cancellationToken);

		/// <summary>
		/// Logs the use out of web app. 
		/// </summary>
		/// <param name="token">User's JWT Token.</param>
		/// <param name="cancellationToken">Token to signal if the operation should be cancelled.</param>
		/// <returns>An <see cref="AuthenticationResult"/>, including data, JWT and useful logs for exception handling.</returns>
		public Task<AuthenticationResult> LogOutAsync(string token, CancellationToken cancellationToken);

		#endregion

		#region Change Roles

		/// <summary>
		/// Changes the current role of the user.
		/// </summary>
		/// <param name="roleId">Target Role's Id.</param>
		/// <param name="userId">Target user's Id.</param>
		/// <returns>An <see cref="OperationResult{User}"/>, including data and useful logs for exception handling.</returns>
		public Task<OperationResult<User>> ChangeRole(Guid roleId, Guid userId);

		#endregion

		//#region JWT Token Generator

		///// <summary>
		///// JWT Tokens are being generated here.
		///// </summary>
		///// <param name="userId">Target user's Id.</param>
		///// <param name="userName">Target user's username.</param>
		///// <returns>An <see cref="AuthenticationResult"/>, including data and JWT Token be used to demonstrate logs and carrying generated JWT token which had been requested from any of containing class methods.</returns>
		//public AuthenticationResult JWTTokenGenerator(Guid userId, string userName);

		//#endregion
		//------------------| Services and logic |------------------\\

	}
}
