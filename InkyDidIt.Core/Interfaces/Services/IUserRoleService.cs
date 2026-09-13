using InkyDidIt.Core.Common.Results;
using InkyDidIt.Core.DTOs.UserDTOs;
using InkyDidIt.Core.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkyDidIt.Core.Interfaces.Services
{
	/// <summary>
	/// Service for managing user roles, including retrieving, creating, editing, 
	/// removing, and assigning roles to users.
	/// </summary>
	public interface IUserRoleService
	{
		/// <summary>
		/// It lists all roles in the system.
		/// </summary>
		/// <param name="token">Token to signal if the operation should be cancelled.</param>
		/// <returns>An <see cref="OperationResult{Role}"/>, including data and useful logs for exception handling.</returns>
		public Task<OperationResult<UserRole>> ShowAllTheRoles(CancellationToken token);

		/// <summary>
		/// It finds the role in the system.
		/// </summary>
		/// <param name="name">Gives name of the role</param>
		/// <param name="token">Token to signal if the operation should be cancelled.</param>
		/// <returns>An <see cref="OperationResult{Role}"/>, including data and useful logs for exception handling.</returns>
		public Task<OperationResult<UserRole>> RoleFinder(string name, CancellationToken token);

		/// <summary>
		/// It finds the role in the system.
		/// </summary>
		/// <param name="id">Role's Id</param>
		/// <param name="token">Token to signal if the operation should be cancelled.</param>
		/// <returns>An <see cref="OperationResult{Role}"/>, including data and useful logs for exception handling.</returns>
		public Task<OperationResult<UserRole>> RoleFinder(Guid id, CancellationToken token);


		/// <summary>
		/// It adds a new role to the system.
		/// </summary>
		/// <param name="role">An instance form RoleDTO</param>
		/// <param name="token">Token to signal if the operation should be cancelled.</param>
		/// <returns>An <see cref="OperationResult{Role}"/>, including data and useful logs for exception handling.</returns>
		public Task<OperationResult<UserRole>> AddRole(RoleDTO role, CancellationToken token);


		/// <summary>
		/// It edits the information of an existing role in the system.
		/// </summary>
		/// <param name="role">An instance form RoleDTO</param>
		/// <param name="token">Token to signal if the operation should be cancelled.</param>
		/// <returns>An <see cref="OperationResult{Role}"/>, including data and useful logs for exception handling.</returns>
		public Task<OperationResult<UserRole>> EditRoleInformation(RoleDTO role, CancellationToken token);

		/// <summary>
		/// It removes an existing role from the system.
		/// </summary>
		/// <param name="role">An instance form RoleDTO</param>
		/// <param name="token">Token to signal if the operation should be cancelled.</param>
		/// <returns>An <see cref="OperationResult{Role}"/>, including data and useful logs for exception handling.</returns>
		public Task<OperationResult<UserRole>> RemoveRole(RoleDTO role, CancellationToken token);

		/// <summary>
		/// It removes an existing role from the system.
		/// </summary>
		/// <param name="id">Role's Id</param>
		/// <param name="token">Token to signal if the operation should be cancelled.</param>
		/// <returns>An <see cref="OperationResult{Role}"/>, including data and useful logs for exception handling.</returns>
		public Task<OperationResult<UserRole>> RemoveRole(Guid id,CancellationToken token);
	}
}
