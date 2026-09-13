using InkyDidIt.Core.Common.Results;
using InkyDidIt.Core.Entities;
using System.Linq.Expressions;

namespace InkyDidIt.Core.Interfaces.Repositories
{
	/// <summary>
	/// Defines a generic repository interface for SQL-based data operations.<br/>
	///		-> Select(2 types)<br/>
	///		-> Insert(2 types)<br/>
	///		-> Update(2 types)<br/>
	///		-> Delete(4 types)
	/// </summary>
	/// <typeparam name="T"> An inheritance of <see cref="BaseEntity"/>, EFCore Entities e.g (User - Posts - Roles ...)</typeparam>
	public interface IGenericRepository<T> where T : BaseEntity
	{
		//------------------| Select |------------------\\
		#region Select

		/// <summary>
		/// Returns all entities. Filters entities if a predicate is provided.<br/>
		/// Includes soft-deleted entities if isSoftDelete is true.
		/// </summary>
		/// <param name="onlySoftDeleted">Indicates whether soft-deleted entities should be included.</param>
		/// <param name="predicate">Optional filter expression to select specific entities.</param>
		/// <param name="cancellationToken">Token to signal if the operation should be cancelled.</param>
		/// <returns>
		/// An <see cref="OperationResult{T}"/> containing the retrieved entities, some extra information about operation and any encountered exception.
		/// </returns>
		public Task<OperationResult<T>> SelectAll(bool onlySoftDeleted, Expression<Func<T, bool>>? predicate, CancellationToken cancellationToken);

		/// <summary>
		/// Returns specific entity by it's unique identifier.
		/// </summary>
		/// <param name="onlySoftDeleted">Include entity if it is soft deleted</param>
		/// <param name="id">Unique Identifier that every record has</param>
		/// <param name="cancellationToken">Token to signal if the operation should be cancelled.</param>
		/// <returns>
		/// An <see cref="OperationResult{T}"/> containing the retrieved entity, some extra information about operation and any encountered exception.
		/// </returns>
		public Task<OperationResult<T>> SelectById(bool onlySoftDeleted, Guid id, CancellationToken cancellationToken);

		#endregion
		//------------------| Insert |------------------\\
		#region Insert

		/// <summary>
		/// Inserts a new entity into the repository.
		/// </summary>
		/// <param name="entity">Entity to be inserted.</param>
		/// <param name="cancellationToken">Token to signal if the operation should be cancelled.</param>	
		/// <returns>
		/// An <see cref="OperationResult{T}"/> containing the entity, operation status, and any exception occurred.
		/// </returns>
		public Task<OperationResult<T>> InsertAsync(T entity, CancellationToken cancellationToken);

		/// <summary>
		/// Inserts multiple entities into the repository.
		/// </summary>
		/// <param name="entities">Entities to be inserted.</param>
		/// <param name="cancellationToken">Token to signal if the operation should be cancelled.</param>
		/// <returns>
		/// An <see cref="OperationResult{T}"/> containing the entities, operation status, and any exception occurred.
		/// </returns>
		public Task<OperationResult<T>> InsertRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

		#endregion
		//------------------| Update |------------------\\
		#region Update

		/// <summary>
		/// Updates an existing entity in the repository.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		/// <param name="cancellationToken">Token to signal if the operation should be cancelled.</param>
		/// <returns>An <see cref="OperationResult{T}"/> containing the entity, operation status,any encountered exception</returns>
		public Task<OperationResult<T>> UpdateAsync(T entity, CancellationToken cancellationToken);


		/// <summary>
		/// Updates a range of existing entities in the repository.
		/// </summary>
		/// <param name="entity">Entities to update</param>
		/// <param name="cancellationToken">Token to signal if the operation should be cancelled.</param>
		/// <returns>An <see cref="OperationResult{T}"/> containing the entities, operation status, any encountered exception</returns>
		public Task<OperationResult<T>> UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

		#endregion
		//------------------| Delete |------------------\\
		#region Delete
		/// <summary>
		/// Soft-deletes the specified entity. If hardDelete is true, the entity is permanently removed. 
		/// </summary>
		/// <param name="hardDelete">Indicates whether the entity should be permanently deleted.</param>
		/// <param name="entity">The entity to delete.</param>
		/// <param name="cancellationToken">Token to signal if the operation should be cancelled.</param>
		/// <returns>An <see cref="OperationResult{T}"/> containing the entity, operation status, any encountered exception</returns>
		public Task<OperationResult<T>> DeleteAsync(bool hardDelete, T entity, CancellationToken cancellationToken);

		/// <summary>
		/// Soft-delete the specified entity by Id. If hardDelete is true, the entity is permanently removed.
		/// </summary>
		/// <param name="hardDelete">Indicates whether the entity should be permanently deleted.</param>
		/// <param name="id">The Identifier of entity</param>
		/// <param name="cancellationToken">Token to signal if the operation should be cancelled.</param>
		/// <returns>An <see cref="OperationResult{T}"/> containing the entity, operation status, any encountered exception</returns>
		public Task<OperationResult<T>> DeleteAsync(bool hardDelete, Guid id, CancellationToken cancellationToken);

		/// <summary>
		/// Soft-deletes specified entities. If hardDelete is true, entities are permanently removed.
		/// </summary>
		/// <param name="hardDelete">Indicates whether entities should be permanently deleted.</param>
		/// <param name="entities">The entities to delete</param>
		/// <param name="cancellationToken">Token to signal if the operation should be cancelled.</param>
		/// <returns>An <see cref="OperationResult{T}"/> containing the entities, operation status, any encountered exception</returns>
		public Task<OperationResult<T>> DeleteRangeAsync(bool hardDelete, IEnumerable<T> entities, CancellationToken cancellationToken);

		/// <summary>
		/// Soft-deletes specified entities. If hardDelete is true, entities are permanently removed.
		/// </summary>
		/// <param name="hardDelete">Indicates whether entities should be permanently deleted.</param>
		/// <param name="id">Identifiers of entities</param>
		/// <param name="cancellationToken">Token to signal if the operation should be cancelled.</param>
		/// <returns>An <see cref="OperationResult{T}"/> containing the entities, operation status, any encountered exception</returns>
		public Task<OperationResult<T>> DeleteRangeAsync(bool hardDelete, IEnumerable<Guid> ids, CancellationToken cancellationToken);


		#endregion
		//------------------| Restore |------------------\\
		#region Restore

		/// <summary>
		/// Restore the specific soft-deleted record on the repository.
		/// </summary>
		/// <param name="id">Identifier of the specific entity</param>
		/// <param name="token">Token to signal if the operation should be cancelled.</param>
		/// <returns>An <see cref="OperationResult{T}"/> containing the entity, operation status, any encountered exception</returns>
		Task<OperationResult<T>> RestoreAsync(Guid id, CancellationToken token);

		/// <summary>
		/// Restore the specific soft-deleted record on the repository.
		/// </summary>
		/// <param name="id">Gets an object of the specific entity</param>
		/// <param name="token">Token to signal if the operation should be cancelled.</param>
		/// <returns>An <see cref="OperationResult{T}"/> containing the entity, operation status, any encountered exception</returns>
		Task<OperationResult<T>> RestoreAsync(T entity, CancellationToken token);

		/// <summary>
		/// Restore the soft-deleted records on the repository.
		/// </summary>
		/// <param name="predicate">Expression to filter the condition</param>
		/// <param name="token">Token to signal if the operation should be cancelled.</param>
		/// <returns>An <see cref="OperationResult{T}"/> containing the entity, operation status, any encountered exception</returns>
		Task<OperationResult<T>> RestoreManyAsync(Expression<Func<T, bool>> predicate, CancellationToken token);
		#endregion
	}
}
