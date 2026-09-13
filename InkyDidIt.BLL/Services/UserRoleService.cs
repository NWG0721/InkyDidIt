using InkyDidIt.Core.Common.Results;
using InkyDidIt.Core.DTOs.UserDTOs;
using InkyDidIt.Core.Entities.UserEntities;
using InkyDidIt.Core.Interfaces.Services;
using InkyDidIt.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkyDidIt.BLL.Services
{
	public class UserRoleService : IUserRoleService
	{

		private readonly IUnitOfWork _unitOfWork;

		public UserRoleService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		#region Show All The Roles


		public async Task<OperationResult<UserRole>> RoleFinder(string name, CancellationToken token)
		{
			OperationResult<UserRole> _catch = await _unitOfWork.genericRepository<UserRole>().SelectAll(false, r => r.Name.Equals(name), token);
			var role = _catch.DataList?.FirstOrDefault();
			if (role is not null)
				return new OperationResult<UserRole>()
				{
					Code = OperationCode.SelectSuccess,
					Data = role,
					Success = true,
					Message = "Role has found"
				};
			else
				return new OperationResult<UserRole>()
				{
					Code = OperationCode.SelectFailed,
					Success = false,
					Message = "Couldn't find the role"
				};
		}

		public async Task<OperationResult<UserRole>> RoleFinder(Guid id, CancellationToken token)
		{
			OperationResult<UserRole> _catch = await _unitOfWork.genericRepository<UserRole>().SelectAll(false, r => r.Id.Equals(id), token);
			var role = _catch.DataList?.FirstOrDefault();
			if (role is not null)
				return new OperationResult<UserRole>()
				{
					Code = OperationCode.SelectSuccess,
					Data = role,
					Success = true,
					Message = "Role has found"
				};
			else
				return new OperationResult<UserRole>()
				{
					Code = OperationCode.SelectFailed,
					Success = false,
					Message = "Couldn't find the role"
				};
		}


		public Task<OperationResult<UserRole>> ShowAllTheRoles(CancellationToken token)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Add Role

		public Task<OperationResult<UserRole>> AddRole(RoleDTO role, CancellationToken token)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Edit Role Information

		public Task<OperationResult<UserRole>> EditRoleInformation(RoleDTO role, CancellationToken token)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Remove Role

		public Task<OperationResult<UserRole>> RemoveRole(RoleDTO role, CancellationToken token)
		{
			throw new NotImplementedException();
		}

		public Task<OperationResult<UserRole>> RemoveRole(Guid id, CancellationToken token)
		{
			throw new NotImplementedException();
		}

		#endregion


		//------------------| User Role DataSheet |------------------\\

	}
}
