using FaaS.Domain.Aggregates.FunctionAggregate;
using FaaS.Domain.Aggregates.FunctionLogAggregate;

namespace FaaS.Application.Services.FunctionService
{
	public interface IFunctionService
	{
		Function Function { get; }

		Task<FunctionLog> ExecuteAsync(string args, CancellationToken cancellation = default);
	}
}
