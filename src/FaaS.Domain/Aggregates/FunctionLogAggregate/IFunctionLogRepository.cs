using DddSharedKernel.Domain.Data;

namespace FaaS.Domain.Aggregates.FunctionLogAggregate
{
	public interface IFunctionLogRepository : IRepository
	{
		void Add(FunctionLog log);

		Task<FunctionLog> GetAsync(int id, CancellationToken cancellation = default);
	}
}
