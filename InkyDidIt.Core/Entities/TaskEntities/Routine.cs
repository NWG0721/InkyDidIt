using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkyDidIt.Core.Entities.TaskEntities
{
	public class Routine : BaseEntity
	{
		public string Title { get; set; }
		public string? Description { get; set; }
		public TimeOnly?[] DaysOfWeekStartsAt { get; set; } = new TimeOnly?[7];
		public TimeOnly?[] DaysOfWeekEndsAt { get; set; } = new TimeOnly?[7];

	}
}
