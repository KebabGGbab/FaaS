using DddSharedKernel.Domain.Results;
using FaaS.Application.Errors;
using FaaS.Domain.Aggregates.FunctionAggregate;
using FaaS.Infrastructure.Specifications;

namespace FaaS.Application.Queries
{
	public sealed class GetUserFunctionsQueryHandler : IQueryHandler<GetUserFunctionsQuery, Result<IEnumerable<Function>>>
	{
		private readonly IFunctionRepository _repository;

		public GetUserFunctionsQueryHandler(IFunctionRepository repository)
		{
			ArgumentNullException.ThrowIfNull(repository);

			_repository = repository;
		}

		public async Task<Result<IEnumerable<Function>>> HandleAsync(GetUserFunctionsQuery query, CancellationToken cancellation = default)
		{
			try
			{
				IEnumerable<Function> functions = await _repository.GetAsync(new FunctionByUserIdSpecification(query.UserId).ToExpression(), cancellation);
				
				return Result.Ok(functions);
			}
			catch
			{
				return Result.Fail<IEnumerable<Function>>(new GetUserFunctionsError());
			}
		}
	}
}
