using DddSharedKernel.Domain.Results;
using FaaS.Application.Dto;
using FaaS.Application.Services.FunctionService;
using FaaS.Domain.Aggregates.FunctionAggregate;
using FaaS.Domain.Aggregates.FunctionLogAggregate;

namespace FaaS.Application.Commands
{
	public sealed class RunFunctionCommandHandler : ICommandHandler<RunFunctionCommand, Result<FunctionLogRunDto>>
	{
		private readonly IFunctionServiceFactory _factory;

		private readonly IFunctionRepository _repository;

		public RunFunctionCommandHandler(IFunctionServiceFactory factory, IFunctionRepository repository)
		{
			ArgumentNullException.ThrowIfNull(factory);
			ArgumentNullException.ThrowIfNull(repository);

			_factory = factory;
			_repository = repository;
		}

		public async Task<Result<FunctionLogRunDto>> HandleAsync(RunFunctionCommand command, CancellationToken cancellation = default)
		{
			Function function = await _repository.GetAsync(command.FunctionId, cancellation);
			IFunctionService functionService = _factory.Create(function);
			FunctionLog log = await functionService.ExecuteAsync(command.Args, cancellation);

			return Result.Ok(new FunctionLogRunDto(function.Id, function.Name, log.ExecutionTime, log.Result));
		}
	}
}
