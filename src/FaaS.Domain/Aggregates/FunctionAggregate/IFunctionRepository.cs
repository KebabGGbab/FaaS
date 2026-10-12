using DddSharedKernel.Domain.Data;
using System.Linq.Expressions;

namespace FaaS.Domain.Aggregates.FunctionAggregate
{
	public interface IFunctionRepository : IRepository
	{
		void Add(Function function);

		void Delete(Function function);

		Task DeleteAsync(int id, CancellationToken cancellation = default);

		Task<Function> GetAsync(int id, CancellationToken cancellation = default);

		Task<IEnumerable<Function>> GetAsync(Expression<Func<Function, bool>> rule, CancellationToken cancellation = default);
	}
}
