using System.ComponentModel.DataAnnotations;

namespace InkyDidIt.Core.Entities.UserEntities
{
	public class UserRole : BaseEntity
	{
		[MinLength(2)]
		public string Name { get; set; }

		public string? Description { get; set; }

		public List<User> Users { get; set; } = new List<User>();
	}
}
