using FaaS.Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FaaS.Infrastructure.DataAccess
{
	public sealed class UserRepository : IUserRepository
	{
		private readonly ApplicationDbContext _db;

		public UserRepository(ApplicationDbContext db)
		{
			ArgumentNullException.ThrowIfNull(db);

			_db = db;
		}

		public void Add(User user)
		{
			_db.Users.Add(user);
		}

		public async Task<User> GetAsync(int id, CancellationToken cancellation = default)
		{
			return await _db.Users.FirstAsync(u => u.Id == id, cancellation)
				.ConfigureAwait(false);
		}

		public async Task<User?> FirstOrDefaultAsync(Expression<Func<User, bool>> rule, CancellationToken cancellation = default)
		{
			return await _db.Users.FirstOrDefaultAsync(rule, cancellation)
				.ConfigureAwait(false);
		}
	}
}
