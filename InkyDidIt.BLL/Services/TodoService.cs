using InkyDidIt.Core.Common.Results;
using InkyDidIt.Core.Entities.Task;
using InkyDidIt.Core.Interfaces.Services;
using InkyDidIt.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace InkyDidIt.BLL.Services
{
	public class TodoService : ITodoService
	{

		#region Constructor
		private readonly IUnitOfWork _unitOfWork;
		public TodoService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		#endregion


		#region Get

		public async Task<OperationResult<TodoItem>> ShowTodoListAsync(Expression<Func<TodoItem, bool>>? predicate, CancellationToken cancellationToken)
		{
			// TODO: Soft Delete deprecated for this project, always pass false. Refactor later if repo layer gets touched.
			OperationResult<TodoItem> todoItems = await _unitOfWork.genericRepository<TodoItem>().SelectAll(false, predicate, cancellationToken);

			return todoItems.Success
				? new OperationResult<TodoItem>(true, "Data has fetched successfully.", todoItems.DataList, todoItems.Code)
				: new OperationResult<TodoItem>(false, "Data could not be fetched", todoItems.Exception, todoItems.Code);
		}

		#endregion

		#region Add

		public async Task<OperationResult<TodoItem>> AddManyTodoItemsAsync(ICollection<TodoItem> todoItems, CancellationToken cancellationToken)
		{
			OperationResult<TodoItem> result;

			if (todoItems is null || todoItems.Count is 0)
			{
				return new OperationResult<TodoItem>(false, "No data to add.", new ArgumentNullException(nameof(todoItems)), OperationCode.InsertManyNullArgument);
			}

			result = await _unitOfWork.genericRepository<TodoItem>().InsertRangeAsync(todoItems, cancellationToken);
			return result.Success is true ? new OperationResult<TodoItem>(true, "Data has been added successfully.", result.DataList, result.Code)
				: new OperationResult<TodoItem>(false, "Data could not be added.", result.Exception, result.Code);
		}

		public async Task<OperationResult<TodoItem>> AddTodoItemAsync(TodoItem todoItem, CancellationToken cancellationToken)
		{
			OperationResult<TodoItem> result;

			if (todoItem is null)
			{
				return new OperationResult<TodoItem>(false, "No data to add.", new ArgumentNullException(nameof(todoItem)), OperationCode.InsertFailed);
			}

			result = await _unitOfWork.genericRepository<TodoItem>().InsertAsync(todoItem, cancellationToken);
			return result.Success is true ? new OperationResult<TodoItem>(true, "Data has been added successfully.", result.Data, result.Code)
				: new OperationResult<TodoItem>(false, "Data could not be added.", result.Exception, result.Code);
		}

		#endregion

		#region delete

		public async Task<OperationResult<TodoItem>> DeleteByIdTodoItemAsync(Guid id, CancellationToken cancellationToken)
		{
			var result = await _unitOfWork.genericRepository<TodoItem>().DeleteAsync(true,id,cancellationToken);
			return result.Success is true ? new OperationResult<TodoItem>(true, "Data has been removed successfully.", result.Data, result.Code)
				: new OperationResult<TodoItem>(false, "Data could not be removed.", result.Exception, result.Code);
		}

		public async Task<OperationResult<TodoItem>> DeleteManyByIdTodoItemsAsync(ICollection<Guid> ids, CancellationToken cancellationToken)
		{
			var result = await _unitOfWork.genericRepository<TodoItem>().DeleteRangeAsync(true, ids, cancellationToken);
			return result.Success is true ? new OperationResult<TodoItem>(true, "Data has been removed successfully.", result.DataList, result.Code)
				: new OperationResult<TodoItem>(false, "Data could not be removed.", result.Exception, result.Code);
		}

		public async Task<OperationResult<TodoItem>> DeleteTodoItemAsync(TodoItem todoItem, CancellationToken cancellationToken)
		{
			if (todoItem is null)
			{
				return new OperationResult<TodoItem>(false, "No data to delete.", new ArgumentNullException(nameof(todoItem)), OperationCode.HardDeleteNullArgument);
			}
			var result = await _unitOfWork.genericRepository<TodoItem>().DeleteAsync(true, todoItem, cancellationToken);
			return result.Success is true ? new OperationResult<TodoItem>(true, "Data has been removed successfully.", result.Data, result.Code)
				: new OperationResult<TodoItem>(false, "Data could not be removed.", result.Exception, result.Code);
		}

		public async Task<OperationResult<TodoItem>> DeleteManyTodoItemsAsync(ICollection<TodoItem> todoItems, CancellationToken cancellationToken)
		{
			if (todoItems is null || todoItems.Count is 0)
			{
				return new OperationResult<TodoItem>(false, "No data to delete.", new ArgumentNullException(nameof(todoItems)), OperationCode.HardDeleteManyNullArgument);
			}
			var result = await _unitOfWork.genericRepository<TodoItem>().DeleteRangeAsync(true, todoItems, cancellationToken);
			return result.Success is true ? new OperationResult<TodoItem>(true, "Data has been removed successfully.", result.DataList, result.Code)
				: new OperationResult<TodoItem>(false, "Data could not be removed.", result.Exception, result.Code);
		}

		#endregion

		#region Edit

		public async Task<OperationResult<TodoItem>> EditTodoItemAsync(TodoItem todoItem, CancellationToken cancellationToken)
		{
			if (todoItem is null)
			{
				return new OperationResult<TodoItem>(false, "No data to edit.", new ArgumentNullException(nameof(todoItem)), OperationCode.InsertManyNullArgument);
			}
			var result = await _unitOfWork.genericRepository<TodoItem>().UpdateAsync(todoItem, cancellationToken);
			return result.Success is true ? new OperationResult<TodoItem>(true, "Data has been edited successfully.", result.Data, result.Code)
				: new OperationResult<TodoItem>(false, "Data could not be edited.", result.Exception, result.Code);
		}

		#endregion
	}
}
