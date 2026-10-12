using DddSharedKernel.Domain.Data;
using System.Linq.Expressions;

namespace FaaS.Domain.Aggregates.UserAggregate
{
	public interface IUserRepository : IRepository
	{
		void Add(User user);

		Task<User> GetAsync(int id, CancellationToken cancellation = default);

		Task<User?> FirstOrDefaultAsync(Expression<Func<User, bool>> rule, CancellationToken cancellation = default);
	}
}
