using DddSharedKernel.Domain.Results;
using FaaS.Application.Errors;
using FaaS.Application.Services.FileResolverService;
using FaaS.Domain.Aggregates.FunctionAggregate;
using FaaS.Infrastructure.DataAccess;

namespace FaaS.Application.Commands
{
	public sealed class UploadFunctionCommandHandler : ICommandHandler<UploadFunctionCommand, Result>
	{
		private readonly IFileResolver _fileResolver;

		private readonly IApplicationUnitOfWork _unitOfWork;

		public UploadFunctionCommandHandler(IFileResolver fileResolver, IApplicationUnitOfWork unitOfWork)
		{
			ArgumentNullException.ThrowIfNull(fileResolver);
			ArgumentNullException.ThrowIfNull(unitOfWork);

			_fileResolver = fileResolver;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> HandleAsync(UploadFunctionCommand command, CancellationToken cancellation = default)
		{
			string directoryName = Guid.NewGuid().ToString();
			_fileResolver.GetDirectoryFunction(directoryName).Create();
			string filePath = _fileResolver.GetFunction(directoryName, command.FileName).FullName;

			try
			{
				using FileStream fs = new(filePath, FileMode.CreateNew);
				await command.Stream.CopyToAsync(fs, cancellation);
				Function function = new(command.UserId, command.Name, command.Description, command.FileName, directoryName, Enum.Parse<Languages>(command.Language), DateTime.UtcNow);
				await _unitOfWork.BeginTransactionAsync(cancellation);
				_unitOfWork.FunctionRepository.Add(function);
				await _unitOfWork.CommitTransactionAsync(cancellation);

				return Result.Ok();
			}
			catch
			{
				File.Delete(filePath);

				if (_unitOfWork.HasActiveTransaction())
				{
					await _unitOfWork.RollbackTransactionAsync(cancellation);
				}

				return Result.Fail(new FailUploadFunctionError());
			}
		}
	}
}
