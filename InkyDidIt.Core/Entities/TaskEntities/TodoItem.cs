using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InkyDidIt.Core.Entities.Task
{
	public class TodoItem : BaseEntity
	{
		public string Title { get; set; }
		public string? Description { get; set; }
		public bool HasDone { get; set; }
		public DateTime? StartsAt { get; set; }
		public DateTime? EndsAt { get; set; }

	}
}
