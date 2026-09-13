using InkyDidIt.Core.Common.Results;
using InkyDidIt.Core.Entities.Task;
using InkyDidIt.Core.Entities.TaskEntities;
using InkyDidIt.Core.Interfaces.Services;
using InkyDidIt.DAL.UnitOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InkyDidIt.BLL.Services
{
	public class RoutineService : IRoutineService
	{

		#region Constructor

		private readonly IUnitOfWork _unitOfWork;
		private readonly ITodoService _todoService;

		public RoutineService(IUnitOfWork unitOfWork, ITodoService todoService)
		{
			_unitOfWork = unitOfWork;
			_todoService = todoService;
		}

		#endregion


		#region Get

		public async Task<OperationResult<Routine>> ShowRoutineAsync(Expression<Func<Routine, bool>>? predicate, CancellationToken cancellationToken)
		{
			// TODO: Soft Delete deprecated for this project, always pass false. Refactor later if repo layer gets touched.
			OperationResult<Routine> routines = await _unitOfWork.genericRepository<Routine>().SelectAll(false, predicate, cancellationToken);

			return routines.Success
				? new OperationResult<Routine>(true, "Data has fetched successfully.", routines.DataList, routines.Code)
				: new OperationResult<Routine>(false, "Data could not be fetched", routines.Exception, routines.Code);
		}

		#endregion

		#region Add

		public async Task<OperationResult<Routine>> AddManyRoutinesAsync(ICollection<Routine> routines, CancellationToken cancellationToken)
		{
			OperationResult<Routine> result;

			if (routines is null || routines.Count is 0)
			{
				return new OperationResult<Routine>(false, "No data to add.", new ArgumentNullException(nameof(routines)), OperationCode.InsertManyNullArgument);
			}

			result = await _unitOfWork.genericRepository<Routine>().InsertRangeAsync(routines, cancellationToken);
			return result.Success is true ? new OperationResult<Routine>(true, "Data has been added successfully.", result.DataList, result.Code)
				: new OperationResult<Routine>(false, "Data could not be added.", result.Exception, result.Code);
		}

		public async Task<OperationResult<Routine>> AddRoutineAsync(Routine routine, CancellationToken cancellationToken)
		{
			OperationResult<Routine> result;

			if (routine is null)
			{
				return new OperationResult<Routine>(false, "No data to add.", new ArgumentNullException(nameof(routine)), OperationCode.InsertFailed);
			}

			result = await _unitOfWork.genericRepository<Routine>().InsertAsync(routine, cancellationToken);
			return result.Success is true ? new OperationResult<Routine>(true, "Data has been added successfully.", result.Data, result.Code)
				: new OperationResult<Routine>(false, "Data could not be added.", result.Exception, result.Code);
		}

		#endregion

		#region delete

		public async Task<OperationResult<Routine>> DeleteByIdRoutineAsync(Guid id, CancellationToken cancellationToken)
		{
			var result = await _unitOfWork.genericRepository<Routine>().DeleteAsync(true, id, cancellationToken);
			return result.Success is true ? new OperationResult<Routine>(true, "Data has been removed successfully.", result.Data, result.Code)
				: new OperationResult<Routine>(false, "Data could not be removed.", result.Exception, result.Code);
		}

		public async Task<OperationResult<Routine>> DeleteManyByRoutinesAsync(ICollection<Guid> ids, CancellationToken cancellationToken)
		{
			var result = await _unitOfWork.genericRepository<Routine>().DeleteRangeAsync(true, ids, cancellationToken);
			return result.Success is true ? new OperationResult<Routine>(true, "Data has been removed successfully.", result.DataList, result.Code)
				: new OperationResult<Routine>(false, "Data could not be removed.", result.Exception, result.Code);
		}

		public async Task<OperationResult<Routine>> DeleteManyRoutinesAsync(ICollection<Routine> routines, CancellationToken cancellationToken)
		{
			if (routines is null || routines.Count is 0)
			{
				return new OperationResult<Routine>(false, "No data to delete.", new ArgumentNullException(nameof(routines)), OperationCode.HardDeleteManyNullArgument);
			}
			var result = await _unitOfWork.genericRepository<Routine>().DeleteRangeAsync(true, routines, cancellationToken);
			return result.Success is true ? new OperationResult<Routine>(true, "Data has been removed successfully.", result.DataList, result.Code)
				: new OperationResult<Routine>(false, "Data could not be removed.", result.Exception, result.Code);
		}

		public async Task<OperationResult<Routine>> DeleteRoutineAsync(Routine routine, CancellationToken cancellationToken)
		{
			if (routine is null)
			{
				return new OperationResult<Routine>(false, "No data to delete.", new ArgumentNullException(nameof(routine)), OperationCode.HardDeleteNullArgument);
			}
			var result = await _unitOfWork.genericRepository<Routine>().DeleteAsync(true, routine, cancellationToken);
			return result.Success is true ? new OperationResult<Routine>(true, "Data has been removed successfully.", result.Data, result.Code)
				: new OperationResult<Routine>(false, "Data could not be removed.", result.Exception, result.Code);
		}

		#endregion

		#region Edit

		public async Task<OperationResult<Routine>> EditRoutineAsync(Routine routine, CancellationToken cancellationToken)
		{
			if (routine is null)
			{
				return new OperationResult<Routine>(false, "No data to edit.", new ArgumentNullException(nameof(routine)), OperationCode.InsertManyNullArgument);
			}
			var result = await _unitOfWork.genericRepository<Routine>().UpdateAsync(routine, cancellationToken);
			return result.Success is true ? new OperationResult<Routine>(true, "Data has been edited successfully.", result.Data, result.Code)
				: new OperationResult<Routine>(false, "Data could not be edited.", result.Exception, result.Code);
		}

		#endregion

		#region Routine to TodoItem Engine

		public async Task<OperationResult<TodoItem>> RoutineToTodoItemEngineAsync(Routine routine, CancellationToken cancellationToken)
		{
			DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);
			List<TodoItem> generatedItems = new List<TodoItem>();

			for (int week = 0; week < 2; week++)
			{
				for (int day = 0; day < 7; day++)
				{
					TimeOnly? startTime = routine.DaysOfWeekStartsAt[day];
					TimeOnly? endTime = routine.DaysOfWeekEndsAt[day];

					if (startTime is null)
						continue;

					
					int diff = ((day - (int)today.DayOfWeek) + 7) % 7;

					
					DateOnly targetDate = today.AddDays(diff + (week * 7));

					if (week == 0 && targetDate < today)
						continue;

					var newItem = new TodoItem
					{
						Title = routine.Title,
						Description = routine.Description,
						HasDone = false,
						StartsAt = targetDate.ToDateTime(startTime.Value),
						EndsAt = endTime is not null ? targetDate.ToDateTime(endTime.Value) : null
					};

					generatedItems.Add(newItem);
				}
			}

			var result = await _unitOfWork.genericRepository<TodoItem>().InsertRangeAsync(generatedItems, cancellationToken);

			return result.Success
				? new OperationResult<TodoItem>(true, "Todo items generated successfully.", result.DataList, result.Code)
				: new OperationResult<TodoItem>(false, "Failed to generate todo items.", result.Exception, result.Code);
		}

		#endregion
	}
}
