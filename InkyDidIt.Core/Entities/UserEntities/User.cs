namespace InkyDidIt.Core.Entities.UserEntities
{
	public class User : BaseEntity
	{
		public Guid RoleID { get; set; }

		public string Username { get; set; }
		public DateTime? LastLogin { get; set; }
		public string Password { get; set; }

		public UserRole Role { get; set; }

	}
}
