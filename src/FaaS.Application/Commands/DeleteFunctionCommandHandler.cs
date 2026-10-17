using DddSharedKernel.Domain.Results;
using FaaS.Application.Errors;
using FaaS.Application.Services.FileResolverService;
using FaaS.Domain.Aggregates.FunctionAggregate;
using FaaS.Infrastructure.DataAccess;

namespace FaaS.Application.Commands
{
	public sealed class DeleteFunctionCommandHandler : ICommandHandler<DeleteFunctionCommand, Result>
	{
		private readonly IFileResolver _fileResolver;

		private readonly IApplicationUnitOfWork _unitOfWork;

		public DeleteFunctionCommandHandler(IFileResolver fileResolver, IApplicationUnitOfWork unitOfWork)
		{
			ArgumentNullException.ThrowIfNull(fileResolver);
			ArgumentNullException.ThrowIfNull(unitOfWork);

			_fileResolver = fileResolver;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> HandleAsync(DeleteFunctionCommand command, CancellationToken cancellation = default)
		{
			Function function = await _unitOfWork.FunctionRepository.GetAsync(command.FunctionId, cancellation)
				.ConfigureAwait(false);
			await _unitOfWork.BeginTransactionAsync(cancellation)
				.ConfigureAwait(false);

			try
			{
				_unitOfWork.FunctionRepository.Delete(function);
				_fileResolver.GetDirectoryFunction(function.DirectoryName).Delete(true);
				await _unitOfWork.CommitTransactionAsync(cancellation)
					.ConfigureAwait(false);

				return Result.Ok();
			}
			catch
			{
				await _unitOfWork.RollbackTransactionAsync(cancellation)
					.ConfigureAwait(false);

				return Result.Fail(new FailDeleteFunctionError());
			}
		}
	}
}
