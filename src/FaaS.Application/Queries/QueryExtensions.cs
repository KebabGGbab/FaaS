using DddSharedKernel.Domain.Results;
using FaaS.Domain.Aggregates.FunctionAggregate;
using Microsoft.Extensions.DependencyInjection;

namespace FaaS.Application.Queries
{
	public static class QueryExtensions
	{
		public static IServiceCollection AddQueries(this IServiceCollection services)
		{
			return services
				.AddScoped<IQueryHandler<GetUserFunctionsQuery, Result<IEnumerable<Function>>>, GetUserFunctionsQueryHandler>()
				.AddScoped<IQueryHandler<GetFunctionQuery, Result<Function>>, GetFunctionQueryHandler>();
		}
	}
}
