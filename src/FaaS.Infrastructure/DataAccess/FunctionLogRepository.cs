using FaaS.Domain.Aggregates.FunctionLogAggregate;
using Microsoft.EntityFrameworkCore;

namespace FaaS.Infrastructure.DataAccess
{
	public sealed class FunctionLogRepository : IFunctionLogRepository
	{
		private readonly ApplicationDbContext _db;

		public FunctionLogRepository(ApplicationDbContext db)
		{
			ArgumentNullException.ThrowIfNull(db);

			_db = db;
		}

		public void Add(FunctionLog log)
		{
			_db.FunctionLogs.Add(log);
		}

		public async Task<FunctionLog> GetAsync(int id, CancellationToken cancellation = default)
		{
			return await _db.FunctionLogs.FirstAsync(f => f.Id == id, cancellation);
		}
	}
}
