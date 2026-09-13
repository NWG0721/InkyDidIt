using InkyDidIt.Core.Entities;
using InkyDidIt.Core.Interfaces.Repositories;

namespace InkyDidIt.DAL.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		IGenericRepository<T> genericRepository<T>() where T : BaseEntity;
		Task<int> SaveChangesAsync();

	}
}
