using FaaS.Domain.Aggregates.FunctionAggregate;

namespace FaaS.Application.Services.FunctionService
{
	public interface IFunctionServiceFactory
	{
		IFunctionService Create(Function function);
	}
}
