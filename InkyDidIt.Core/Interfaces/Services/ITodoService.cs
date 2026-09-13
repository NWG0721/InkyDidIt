using InkyDidIt.Core.Common.Results;
using InkyDidIt.Core.Entities.Task;
using InkyDidIt.Core.Entities.UserEntities;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InkyDidIt.Core.Interfaces.Services
{
	public interface ITodoService
	{
		public Task<OperationResult<TodoItem>> ShowTodoListAsync(Expression<Func<TodoItem, bool>>? predicate, CancellationToken cancellationToken);
		public Task<OperationResult<TodoItem>> AddTodoItemAsync(TodoItem todoItem, CancellationToken cancellationToken);
		public Task<OperationResult<TodoItem>> AddManyTodoItemsAsync(ICollection<TodoItem> todoItems, CancellationToken cancellationToken);
		public Task<OperationResult<TodoItem>> EditTodoItemAsync(TodoItem todoItem, CancellationToken cancellationToken);
		public Task<OperationResult<TodoItem>> DeleteTodoItemAsync(TodoItem todoItem, CancellationToken cancellationToken);
		public Task<OperationResult<TodoItem>> DeleteByIdTodoItemAsync(Guid Id, CancellationToken cancellationToken);
		public Task<OperationResult<TodoItem>> DeleteManyByIdTodoItemsAsync(ICollection<Guid> Ids, CancellationToken cancellationToken);
	}
}
