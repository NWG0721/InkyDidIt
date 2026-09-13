using InkyDidIt.Core.Common.Results;
using InkyDidIt.Core.Entities.Task;
using InkyDidIt.Core.Interfaces.Services;
using InkyDidIt.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkyDidIt.BLL.Services
{
	public class TodoService : ITodoService
	{

		private readonly IUnitOfWork _untiOfWork;
		public TodoService(IUnitOfWork unitOfWork)
		{
			_untiOfWork = unitOfWork;
		}

		#region Get

		public Task<OperationResult<TodoItem>> ShowTodoListAsync()
		{
			if
		}

		#endregion

		#region Add

		public Task<OperationResult<TodoItem>> EditTodoItemAsync(TodoItem todoItem)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region delete

		public Task<OperationResult<TodoItem>> DeleteManyTodoItemsAsync(ICollection<TodoItem> todoItems)
		{
			throw new NotImplementedException();
		}

		public Task<OperationResult<TodoItem>> DeleteTodoItemAsync(TodoItem todoItem)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region Edit

		public Task<OperationResult<TodoItem>> AddManyTodoItemsAsync(ICollection<TodoItem> todoItems)
		{
			throw new NotImplementedException();
		}

		public Task<OperationResult<TodoItem>> AddTodoItemAsync(TodoItem todoItem)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
