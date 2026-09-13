namespace InkyDidIt.Core.Entities
{
	public class BaseEntity
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public DateTime DateCreate { get; set; } = DateTime.UtcNow;
		public Guid? CreatedByUserId { get; set; }
		public DateTime? DateUpdate { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public Guid? DeletedBy { get; set; }
	}
}
