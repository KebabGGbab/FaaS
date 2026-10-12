using FaaS.Domain.Aggregates.UserCredentialsAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FaaS.Infrastructure.DataAccess
{
	public sealed class UserCredentialsRepository : IUserCredentialsRepository
	{
		private readonly ApplicationDbContext _db;

		public UserCredentialsRepository(ApplicationDbContext db)
		{
			ArgumentNullException.ThrowIfNull(db);

			_db = db;
		}

		public async Task AddAsync(UserCredentials credentials, CancellationToken cancellation = default)
		{
			await _db.UserCredentials.AddAsync(credentials, cancellation)
				.ConfigureAwait(false);
		}

		public void Delete(UserCredentials credentials)
		{
			_db.UserCredentials.Remove(credentials);
		}

		public async Task DeleteAsync(int id, CancellationToken cancellation = default)
		{
			Delete(await GetAsync(id, cancellation)
				.ConfigureAwait(false));
		}

		public async Task<UserCredentials> GetAsync(int id, CancellationToken cancellation = default)
		{
			return await _db.UserCredentials.FirstAsync(u => u.Id == id, cancellation)
				.ConfigureAwait(false);
		}

		public async Task<bool> AnyAsync(Expression<Func<UserCredentials, bool>> rule, CancellationToken cancellation = default)
		{
			return await _db.UserCredentials.AnyAsync(rule, cancellation)
				.ConfigureAwait(false);
		}

		public async Task<UserCredentials?> FirstOrDefaultAsync(Expression<Func<UserCredentials, bool>> rule, CancellationToken cancellation = default)
		{
			return await _db.UserCredentials.FirstOrDefaultAsync(rule, cancellation)
				.ConfigureAwait(false);
		}
	}
}
