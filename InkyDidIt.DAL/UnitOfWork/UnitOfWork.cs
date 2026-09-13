using InkyDidIt.Core.Entities;
using InkyDidIt.Core.Interfaces.Repositories;
using InkyDidIt.DAL.DbContexts;
using InkyDidIt.DAL.Repository;

namespace InkyDidIt.DAL.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly InkyDidItDbContext _context;
		private readonly Dictionary<Type, Object> _repositories = new();


		public UnitOfWork(InkyDidItDbContext context) => _context = context;

		public IGenericRepository<T> genericRepository<T>() where T : BaseEntity
		{
			var type = typeof(T);
			if (!_repositories.ContainsKey(type))
			{
				var repository = new GenericRepository<T>(_context);
				_repositories[type] = repository;
			}
			return (IGenericRepository<T>)_repositories[type];
		}

		public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
		public void Dispose() => _context.Dispose();
	}
}
