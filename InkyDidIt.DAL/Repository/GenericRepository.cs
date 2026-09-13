using Microsoft.EntityFrameworkCore;
using InkyDidIt.Core.Common.Results;
using InkyDidIt.Core.Entities;
using InkyDidIt.Core.Interfaces.Repositories;
using InkyDidIt.DAL.DbContexts;
using System.Linq.Expressions;

namespace InkyDidIt.DAL.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		#region Constructor

		protected readonly InkyDidItDbContext _context;
		private readonly DbSet<T> _db;
		public GenericRepository(InkyDidItDbContext context)
		{
			_context = context;
			_db = _context.Set<T>();
		}

		#endregion

		//------------------| Select |------------------\\
		#region Select

		public async Task<OperationResult<T>> SelectAll(bool onlySoftDeleted, Expression<Func<T, bool>>? predicate, CancellationToken cancellationToken)
		{
			var result = new OperationResult<T>();
			try
			{
				predicate ??= x => true;
				result.DataList = await _db
					.Where(e => e.IsDeleted == onlySoftDeleted)
					.Where(predicate)
					.ToListAsync(cancellationToken);
				result.Success = true;
				result.Message = "Entities retrieved successfully";
				result.Code = OperationCode.SelectSuccess;
				return result;
			}
			catch (OperationCanceledException ex)
			{
				result.Success = false;
				result.Exception = ex;
				result.Message = "Operation was cancelled";
				result.Code = OperationCode.Canceled;

				return result;
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Message = "Failed to retrieve entities";
				result.Exception = ex;
				result.Code = OperationCode.SelectFailed;
				return result;
			}

		}

		public async Task<OperationResult<T>> SelectById(bool onlySoftDeleted, Guid id, CancellationToken cancellationToken)
		{
			var result = new OperationResult<T>();
			try
			{
				result.Data = await _db.Where(e => e.Id == id && e.IsDeleted == onlySoftDeleted).FirstOrDefaultAsync(cancellationToken);
				result.Success = true;
				result.Message = "Entity retrieved successfully";
				result.Code = OperationCode.SelectSuccess;
				return result;
			}
			catch (OperationCanceledException ex)
			{
				result.Success = false;
				result.Exception = ex;
				result.Message = "Operation was cancelled";
				result.Code = OperationCode.Canceled;

				return result;
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Exception = ex;
				result.Message = "Failed to retrieve entity";
				result.Code = OperationCode.SelectFailed;
				return result;
			}
		}
		#endregion
		//------------------| Insert |------------------\\
		#region Insert

		public async Task<OperationResult<T>> InsertAsync(T entity, CancellationToken cancellationToken)
		{
			var result = new OperationResult<T>();
			if (entity == null)
			{
				result.Success = false;
				result.Message = "Entity is null";
				result.Code = OperationCode.InsertNullArgument;

				return result;
			}

			try
			{
				await _db.AddAsync(entity, cancellationToken);
				result.Success = true;
				result.Data = entity;
				result.Message = "Entity inserted successfully";
				result.Code = OperationCode.InsertSuccess;
				return result;
			}
			catch (OperationCanceledException ex)
			{
				result.Success = false;
				result.Exception = ex;
				result.Data = entity;
				result.Message = "Operation was cancelled";
				result.Code = OperationCode.Canceled;

				return result;
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Exception = ex;
				result.Data = entity;
				result.Message = "Failed to insert new entity";
				result.Code = OperationCode.InsertFailed;
				return result;
			}
		}

		public async Task<OperationResult<T>> InsertRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
		{
			var result = new OperationResult<T>();
			if (!entities.Any())
			{
				result.Success = false;
				result.Message = "Entities are null";
				result.Code = OperationCode.InsertManyNullArgument;
				return result;
			}

			try
			{
				await _db.AddRangeAsync(entities, cancellationToken);
				result.Success = true;
				result.DataList = entities;
				result.Message = "Entities inserted successfully";
				result.Code = OperationCode.InsertManySuccess;
				return result;

			}
			catch (OperationCanceledException ex)
			{
				result.Success = false;
				result.Exception = ex;
				result.Message = "Operation was cancelled";
				result.DataList = entities;
				result.Code = OperationCode.Canceled;
				return result;
			}
			catch (Exception ex)
			{
				result.Success = false;
				result.Exception = ex;
				result.DataList = entities;
				result.Message = "Failed to insert new entities";
				result.Code = OperationCode.InsertManyFailed;
				return result;
			}
		}

		#endregion
		//------------------| Update |------------------\\
		#region Update
		public async Task<OperationResult<T>> UpdateAsync(T entity, CancellationToken cancellationToken)
		{
			var result = new OperationResult<T>();
			if (entity == null)
			{
				result.Success = false;
				result.Message = "Entity is null";
				result.Code = OperationCode.UpdateNullArgument;

				return result;

			}

			try
			{
				_db.Update(entity);
				result.Success = true;
				result.Message = "Entity updated successfully";
				result.Code = OperationCode.UpdateSuccess;
				result.Data = entity;
				return result;
			}
			catch (OperationCanceledException ex)
			{
				result.Success = false;
				result.Exception = ex;
				result.Message = "Operation was cancelled";
				result.Code = OperationCode.Canceled;
				result.Data = entity;
				return result;
			}
			catch (Exception)
			{
				result.Success = false;
				result.Message = "Failed to update entity";
				result.Code = OperationCode.UpdateFailed;
				result.Data = entity;
				return result;
			}
		}

		public async Task<OperationResult<T>> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
		{
			var result = new OperationResult<T>();
			if (!entities.Any())
			{
				result.Success = false;
				result.Message = "Entities are null";
				result.Code = OperationCode.UpdateNullArgument;

				return result;

			}
			try
			{
				_db.UpdateRange(entities);
				result.Success = true;
				result.Message = "Entity updated successfully";
				result.Code = OperationCode.UpdateSuccess;
				result.DataList = entities;
				return result;
			}
			catch (OperationCanceledException ex)
			{
				result.Success = false;
				result.Exception = ex;
				result.Message = "Operation was cancelled";
				result.Code = OperationCode.Canceled;
				result.DataList = entities;
				return result;
			}
			catch (Exception)
			{
				result.Success = false;
				result.Message = "Failed to update entity";
				result.Code = OperationCode.UpdateFailed;
				result.DataList = entities;
				return result;
			}
		}

		#endregion
		//------------------| Delete |------------------\\
		#region Delete

		public async Task<OperationResult<T>> DeleteAsync(bool hardDelete, T entity, CancellationToken cancellationToken)
		{
			var result = new OperationResult<T>();
			if (entity == null)
			{
				result.Success = false;
				result.Message = "Entity is null";
				result.Code = OperationCode.SelectDeletedNullResult;
				return result;
			}
			try
			{
				if (hardDelete)
				{
					_db.Remove(entity);
					result.Message = "Entity deleted hard-successfully";
					result.Code = OperationCode.HardDeleteSuccess;
				}
				else
				{
					entity.IsDeleted = true;
					_db.Update(entity);
					result.Message = "Entity soft-deleted successfully";
					result.Code = OperationCode.SoftDeleteSuccess;

				}
				result.Success = true;
				result.Data = entity;
				return result;
			}
			catch (OperationCanceledException ex)
			{
				result.Success = false;
				result.Exception = ex;
				result.Message = "Operation was cancelled";
				result.Code = OperationCode.Canceled;
				result.Data = entity;
				return result;
			}
			catch (Exception ex)
			{
				result.Exception = ex;
				result.Success = false;
				if (hardDelete)
				{
					result.Message = "Failed to hard-delete entity";
					result.Code = OperationCode.HardDeleteFailed;
				}
				else
				{
					result.Message = "Failed to soft-delete entity";
					result.Code = OperationCode.SoftDeleteFailed;
				}
				result.Data = entity;
				return result;
			}
		}

		public async Task<OperationResult<T>> DeleteAsync(bool hardDelete, Guid id, CancellationToken cancellationToken)
		{
			var result = new OperationResult<T>();
			var entity = default(T);

			try
			{
				entity = await _db.FindAsync(id, cancellationToken);
				if (entity == null)
				{
					result.Success = false;
					result.Message = "Entity not found";
					result.Code = OperationCode.SelectNullResult;
					return result;
				}
				if (hardDelete)
				{
					_db.Remove(entity);
					result.Message = "Entity hard-deleted successfully";
					result.Code = OperationCode.HardDeleteSuccess;
				}
				else
				{
					entity.IsDeleted = true;
					_db.Update(entity);
					result.Message = "Entity soft-deleted successfully";
					result.Code = OperationCode.SoftDeleteSuccess;
				}
				result.Success = true;
				result.Data = entity;
				return result;
			}
			catch (OperationCanceledException ex)
			{
				result.Success = false;
				result.Exception = ex;
				result.Message = "Operation was cancelled";
				result.Code = OperationCode.Canceled;
				result.Data = entity;
				return result;
			}
			catch (Exception ex)
			{
				result.Exception = ex;
				result.Success = false;
				if (hardDelete)
				{
					result.Code = OperationCode.HardDeleteFailed;
					result.Message = "Failed to hard-delete entity";

				}
				else
				{
					result.Code = OperationCode.SoftDeleteFailed;
					result.Message = "Failed to soft-delete entity";

				}
				result.Data = entity;
				return result;
			}
		}

		public async Task<OperationResult<T>> DeleteRangeAsync(bool hardDelete, IEnumerable<T> entities, CancellationToken cancellationToken)
		{
			var result = new OperationResult<T>();
			if (!entities.Any())
			{
				result.Success = false;
				result.Message = "Entities are null";
				result.Code = OperationCode.SelectDeletedNullResult;
				return result;
			}
			try
			{
				if (hardDelete)
				{
					_db.RemoveRange(entities);
					result.Message = "Entities hard-deleted successfully";
					result.Code = OperationCode.HardDeleteSuccess;
				}
				else
				{
					foreach (var entity in entities)
					{
						entity.IsDeleted = true;
						_db.Update(entity);
					}
					result.Message = "Entities soft-deleted successfully";
					result.Code = OperationCode.SoftDeleteSuccess;
				}
				result.Success = true;
				result.DataList = entities;
				return result;
			}
			catch (OperationCanceledException ex)
			{
				result.Success = false;
				result.Exception = ex;
				result.Message = "Operation was cancelled";
				result.Code = OperationCode.Canceled;
				result.DataList = entities;
				return result;
			}
			catch (Exception ex)
			{
				result.Exception = ex;
				result.Success = false;
				if (hardDelete)
				{
					result.Message = "Failed to hard-delete entity";
					result.Code = OperationCode.HardDeleteFailed;
				}
				else
				{
					result.Message = "Failed to soft-delete entity";
					result.Code = OperationCode.SoftDeleteFailed;
				}
				result.DataList = entities;
				return result;
			}
		}

		public async Task<OperationResult<T>> DeleteRangeAsync(bool hardDelete, IEnumerable<Guid> ids, CancellationToken cancellationToken)
		{
			var result = new OperationResult<T>();
			var entities = new List<T>();
			try
			{
				entities = await _db.Where(e => ids.Contains(e.Id)).ToListAsync(cancellationToken);
				if (!entities.Any())
				{
					result.Success = false;
					result.Message = "Entities are null";
					result.Code = OperationCode.SelectDeletedNullResult;
					return result;
				}
				if (hardDelete)
				{
					_db.RemoveRange(entities);
					result.Code = OperationCode.HardDeleteSuccess;
				}
				else
				{
					foreach (var entity in entities)
					{
						entity.IsDeleted = true;
						_db.Update(entity);
					}
					result.Code = OperationCode.SoftDeleteSuccess;

				}
				result.Success = true;
				result.Message = "Entity deleted successfully";
				result.DataList = entities;
				return result;
			}
			catch (OperationCanceledException ex)
			{
				result.Success = false;
				result.Exception = ex;
				result.Message = "Operation was cancelled";
				result.Code = OperationCode.Canceled;
				result.DataList = entities;
				return result;
			}
			catch (Exception ex)
			{
				result.Exception = ex;
				result.Success = false;
				if (hardDelete)
				{
					result.Message = "Failed to hard-delete entity";
					result.Code = OperationCode.HardDeleteFailed;
				}
				else
				{
					result.Message = "Failed to soft-delete entity";
					result.Code = OperationCode.SoftDeleteFailed;
				}
				result.DataList = entities;
				return result;
			}
		}

		#endregion
		//------------------| restore |------------------\\
		#region Restore

		public async Task<OperationResult<T>> RestoreAsync(Guid id, CancellationToken token)
		{
			var result = new OperationResult<T>();
			var entity = default(T);
			try
			{
				entity = await _db.FindAsync(id, token);
				if (entity == null)
				{
					result.Success = false;
					result.Message = "Entity is null";
					result.Code = OperationCode.RestoreNullArgument;
					return result;
				}
				if (entity.IsDeleted)
				{
					entity.IsDeleted = false;
					_db.Update(entity);
				}
				result.Code = OperationCode.RestoreSuccess;
				result.Success = true;
				result.Message = "Entity restored successfully";
				result.Data = entity;
				return result;
			}
			catch (OperationCanceledException ex)
			{
				result.Success = false;
				result.Exception = ex;
				result.Message = "Operation was cancelled";
				result.Code = OperationCode.Canceled;
				result.Data = entity;
				return result;
			}
			catch (Exception ex)
			{
				result.Exception = ex;
				result.Success = false;
				result.Message = "Failed to restore entity";
				result.Code = OperationCode.RestoreFailed;
				result.Data = entity;
				return result;

			}
		}

		public async Task<OperationResult<T>> RestoreAsync(T entity, CancellationToken token)
		{
			var result = new OperationResult<T>();
			try
			{
				if (entity == null)
				{
					result.Success = false;
					result.Message = "Entity is null";
					result.Code = OperationCode.RestoreNullArgument;
					return result;
				}
				if (entity.IsDeleted)
				{
					entity.IsDeleted = false;
					_db.Update(entity);
				}
				result.Code = OperationCode.RestoreSuccess;
				result.Success = true;
				result.Message = "Entity restored successfully";
				result.Data = entity;
				return result;
			}
			catch (OperationCanceledException ex)
			{
				result.Success = false;
				result.Exception = ex;
				result.Message = "Operation was cancelled";
				result.Code = OperationCode.Canceled;
				result.Data = entity;
				return result;
			}
			catch (Exception ex)
			{
				result.Exception = ex;
				result.Success = false;
				result.Message = "Failed to restore entity";
				result.Code = OperationCode.RestoreFailed;
				result.Data = entity;
				return result;

			}
		}

		public async Task<OperationResult<T>> RestoreManyAsync(Expression<Func<T, bool>> predicate, CancellationToken token)
		{
			var result = new OperationResult<T>();
			predicate ??= x => true;
			IEnumerable<T> entities = null;
			try
			{
				var _operation = await SelectAll(true, predicate, token);
				if (!_operation.Success)
				{
					return new OperationResult<T>
					{
						Success = false,
						Message = _operation.Message,
						Exception = _operation.Exception,
						Code = _operation.Code
					};
				}
				entities = _operation.DataList ?? Enumerable.Empty<T>();
				if (!entities.Any())
				{
					return new OperationResult<T>
					{
						Success = false,
						Message = "No entities found to restore",
						Code = OperationCode.RestoreNullArgument
					};
				}
				foreach (var entity in entities)
					entity.IsDeleted = false;

				_db.UpdateRange(entities);

				result.Code = OperationCode.RestoreSuccess;
				result.Success = true;
				result.Message = "Entities restored successfully";
				result.DataList = entities;
				return result;
			}
			catch (OperationCanceledException ex)
			{
				result.Success = false;
				result.Exception = ex;
				result.Message = "Operation was cancelled";
				result.Code = OperationCode.Canceled;
				result.DataList = entities;
				return result;
			}
			catch (Exception ex)
			{
				result.Exception = ex;
				result.Success = false;
				result.Message = "Failed to restore entity";
				result.Code = OperationCode.RestoreFailed;
				result.DataList = entities;
				return result;

			}
		}

		#endregion
	}
}
