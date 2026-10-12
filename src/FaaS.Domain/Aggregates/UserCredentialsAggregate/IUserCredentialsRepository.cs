using DddSharedKernel.Domain.Data;
using System.Linq.Expressions;

namespace FaaS.Domain.Aggregates.UserCredentialsAggregate
{
	public interface IUserCredentialsRepository : IRepository
	{
		Task AddAsync(UserCredentials credentials, CancellationToken cancellation = default);

		void Delete(UserCredentials credentials);

		Task DeleteAsync(int id, CancellationToken cancellation = default);

		Task<UserCredentials> GetAsync(int id, CancellationToken cancellation = default);

		Task<bool> AnyAsync(Expression<Func<UserCredentials, bool>> rule, CancellationToken cancellation = default);

		Task<UserCredentials?> FirstOrDefaultAsync(Expression<Func<UserCredentials, bool>> rule, CancellationToken cancellation = default);
	}
}
