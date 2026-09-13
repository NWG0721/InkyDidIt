using BCrypt.Net;
using InkyDidIt.Core.Entities;
using InkyDidIt.Core.Entities.Task;
using InkyDidIt.Core.Entities.TaskEntities;
using InkyDidIt.Core.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Data;

namespace InkyDidIt.DAL.DbContexts
{
	public class InkyDidItDbContext : DbContext
	{
		public InkyDidItDbContext(DbContextOptions<InkyDidItDbContext> options) : base(options)
		{
		}
		#region GenericDbSet
		public DbSet<T> Set<T>() where T : BaseEntity => base.Set<T>();

		#endregion
		protected override void OnModelCreating(ModelBuilder builder)
		{
			//------------------| UserEntites |------------------\\

			//#region User

			//builder.Entity<User>(
			//	user =>
			//	{
			//		user.HasKey(u => u.Id);
			//		user.Property(x => x.DateCreate).HasColumnType("timestamptz");
			//		user.Property(x => x.DateUpdate).HasColumnType("timestamptz");
			//		user.Property(x => x.DeletedAt).HasColumnType("timestamptz");
			//		user.HasIndex(u => u.Username).IsUnique();
			//		user.Property(u => u.Username).IsRequired().HasMaxLength(25);
			//		user.Property(u => u.Password).IsRequired().HasMaxLength(256);
			//		user.HasOne(u => u.Role).WithMany(r => r.Users).HasForeignKey(user => user.RoleID).OnDelete(DeleteBehavior.Restrict);
			//	});

			//#endregion

			//#region Role
			//builder.Entity<UserRole>(
			//	role =>
			//	{
			//		role.HasKey(r => r.Id);
			//		role.Property(x => x.DateCreate).HasColumnType("timestamptz");
			//		role.Property(x => x.DateUpdate).HasColumnType("timestamptz");
			//		role.Property(x => x.DeletedAt).HasColumnType("timestamptz");
			//		role.Property(r => r.Name).IsRequired().HasMaxLength(50);
			//		role.Property(r => r.Description).HasMaxLength(255);
			//	}
			//	);
			//#endregion

			#region TodoItem

			builder.Entity<TodoItem>(item => {

				item.HasKey(i => i.Id);
				item.Property(i => i.DateCreate).HasColumnType("timestamptz");
				item.Property(i => i.DateUpdate).HasColumnType("timestamptz");
				item.Property(i => i.DeletedAt).HasColumnType("timestamptz");
				item.Property(i => i.Title).IsRequired().HasMaxLength(50);
				item.Property(i => i.Description).HasMaxLength(255);
				item.Property(i => i.HasDone).IsRequired();
				item.Property(i => i.HasDone).HasColumnType("boolean");
				item.Property(i => i.StartsAt).HasColumnType("time[]");
				item.Property(i => i.EndsAt).HasColumnType("time[]");

			});

			#endregion

			#region Routine

			builder.Entity<Routine>(routine => {

				routine.HasKey(i => i.Id);
				routine.Property(r => r.DateCreate).HasColumnType("timestamptz");
				routine.Property(r => r.DateUpdate).HasColumnType("timestamptz");
				routine.Property(r => r.DeletedAt).HasColumnType("timestamptz");
				routine.Property(r => r.Title).IsRequired().HasMaxLength(50);
				routine.Property(r => r.Description).HasMaxLength(255);
				routine.Property(r => r.DaysOfWeekStartsAt).HasColumnType("timestamptz");
				routine.Property(r => r.DaysOfWeekEndsAt).HasColumnType("timestamptz");

			});

			#endregion


			//------------------| UserEntites |------------------\\

			//builder.Entity<UserRole>().HasData(
			//new UserRole
			//{
			//	Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
			//	Name = "Admin",
			//	Description = "Administrator role with full access",
			//	DateCreate = new DateTime(2026, 7, 28, 0, 0, 0, DateTimeKind.Utc)

			//},
			//new UserRole
			//{
			//	Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
			//	Name = "Root",
			//	Description = "Root role with system access",
			//	DateCreate = new DateTime(2026, 7, 28, 0, 0, 0, DateTimeKind.Utc)

			//},
			//new UserRole
			//{
			//	Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
			//	Name = "Standard User",
			//	Description = "Regular user role with limited access",
			//	DateCreate = new DateTime(2026, 7, 28, 0, 0, 0, DateTimeKind.Utc)
			//},
			//new UserRole
			//{
			//	Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
			//	Name = "Guest",
			//	Description = "Guest role with minimal access",
			//	DateCreate = new DateTime(2026, 7, 28, 0, 0, 0, DateTimeKind.Utc)
			//});

			//builder.Entity<User>().HasData(
			//new User
			//{
			//	Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
			//	Username = "Administrator",
			//	Password = "$2a$12$fVzhaUOGH1OISVr4E15LyeXEW5qd/Mnt8FIBLls7eFaXbLiwP.GIe",
			//	RoleID = Guid.Parse("22222222-2222-2222-2222-222222222222"),
			//	DateCreate = new DateTime(2026, 7, 28, 0, 0, 0, DateTimeKind.Utc)
			//});
		}
	}
}
