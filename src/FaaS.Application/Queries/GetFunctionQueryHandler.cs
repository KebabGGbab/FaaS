using DddSharedKernel.Domain.Results;
using FaaS.Application.Errors;
using FaaS.Domain.Aggregates.FunctionAggregate;

namespace FaaS.Application.Queries
{

	public sealed class GetFunctionQueryHandler : IQueryHandler<GetFunctionQuery, Result<Function>>
	{
		private readonly IFunctionRepository _repository;

		public GetFunctionQueryHandler(IFunctionRepository repository)
		{
			ArgumentNullException.ThrowIfNull(repository);

			_repository = repository;
		}

		public async Task<Result<Function>> HandleAsync(GetFunctionQuery query, CancellationToken cancellation = default)
		{
			try
			{
				Function function = await _repository.GetAsync(query.FunctionId, cancellation);

				return Result.Ok(function);
			}
			catch
			{
				return Result.Fail<Function>(new GetFunctionError());
			}
		}
	}
}
