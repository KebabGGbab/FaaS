using FaaS.Domain.Aggregates.FunctionAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FaaS.Infrastructure.DataAccess
{
	public sealed class FunctionRepository : IFunctionRepository
	{
		private readonly ApplicationDbContext _db;

		public FunctionRepository(ApplicationDbContext db)
		{
			ArgumentNullException.ThrowIfNull(db);

			_db = db;
		}

		public void Add(Function function)
		{
			_db.Functions.Add(function);
		}

		public void Delete(Function function)
		{
			_db.Functions.Remove(function);
		}

		public async Task DeleteAsync(int id, CancellationToken cancellation = default)
		{
			Delete(await GetAsync(id, cancellation)
				.ConfigureAwait(false));
		}

		public async Task<Function> GetAsync(int id, CancellationToken cancellation = default)
		{
			return await _db.Functions
				.FirstAsync(f => f.Id == id, cancellation)
				.ConfigureAwait(false);
		}

		public async Task<IEnumerable<Function>> GetAsync(Expression<Func<Function, bool>> rule, CancellationToken cancellation = default)
		{
			return await _db.Functions
				.Where(rule)
				.ToListAsync(cancellation)
				.ConfigureAwait(false);
		}
	}
}
