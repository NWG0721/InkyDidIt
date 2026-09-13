using InkyDidIt.Core.Common.Results;
using InkyDidIt.Core.Entities.Task;
using InkyDidIt.Core.Entities.TaskEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InkyDidIt.Core.Interfaces.Services
{
	public interface IRoutineService
	{
		public Task<OperationResult<Routine>> ShowRoutineAsync(Expression<Func<Routine, bool>>? predicate, CancellationToken cancellationToken);
	
		public Task<OperationResult<Routine>> AddRoutineAsync(Routine routine, CancellationToken cancellationToken);
		public Task<OperationResult<Routine>> AddManyRoutinesAsync(ICollection<Routine> routines, CancellationToken cancellationToken);
	
		public Task<OperationResult<Routine>> EditRoutineAsync(Routine routine	, CancellationToken cancellationToken);
		
		public Task<OperationResult<Routine>> DeleteRoutineAsync(Routine routine, CancellationToken cancellationToken);
		public Task<OperationResult<Routine>> DeleteByIdRoutineAsync(Guid id, CancellationToken cancellationToken);
		public Task<OperationResult<Routine>> DeleteManyRoutinesAsync(ICollection<Routine> routines, CancellationToken cancellationToken);
		public Task<OperationResult<Routine>> DeleteManyByRoutinesAsync(ICollection<Guid> ids, CancellationToken cancellationToken);

		public Task<OperationResult<TodoItem>> RoutineToTodoItemEngineAsync(Routine routine	, CancellationToken cancellationToken);
	}
}
