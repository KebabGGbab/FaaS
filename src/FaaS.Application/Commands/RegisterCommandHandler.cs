using DddSharedKernel.Domain.Results;
using FaaS.Application.Errors;
using FaaS.Domain.Aggregates.UserAggregate;
using FaaS.Domain.Aggregates.UserCredentialsAggregate;
using FaaS.Infrastructure.DataAccess;
using FaaS.Infrastructure.Specifications;

namespace FaaS.Application.Commands
{
	public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, Result<User>>
	{
		private readonly IApplicationUnitOfWork _unitOfWork;

		public RegisterCommandHandler(IApplicationUnitOfWork unitOFWork)
		{
			ArgumentNullException.ThrowIfNull(unitOFWork);

			_unitOfWork = unitOFWork;
		}

		public async Task<Result<User>> HandleAsync(RegisterCommand command, CancellationToken cancellation = default)
		{
			Result<UserCredentials> credentialsResult = UserCredentials.Create(command.Login, command.Password);

			if (credentialsResult.IsFailure)
			{
				return Result.Fail<User>(credentialsResult.Errors);
			}

			bool isExist = await _unitOfWork.CredentialsRepository
				.AnyAsync(new UserCredentialsByLoginSpecification(credentialsResult.Value.Login).ToExpression(), cancellation)
				.ConfigureAwait(false);

			if (isExist)
			{
				return Result.Fail<User>(new LoginIsBusyError());
			}

			await _unitOfWork.BeginTransactionAsync(cancellation)
				.ConfigureAwait(false);

			try
			{
				await _unitOfWork.CredentialsRepository.AddAsync(credentialsResult.Value, cancellation)
					.ConfigureAwait(false);
				await _unitOfWork.SaveAsync(cancellation)
					.ConfigureAwait(false);

				Result<User> userResult = User.Create(command.Name, credentialsResult.Value.Id);

				if (userResult.IsFailure)
				{
					await _unitOfWork.RollbackTransactionAsync(cancellation)
						.ConfigureAwait(false);
					return userResult;
				}

				_unitOfWork.UserRepository.Add(userResult.Value);
				await _unitOfWork.CommitTransactionAsync(cancellation)
					.ConfigureAwait(false);

				return userResult;
			}
			catch
			{
				await _unitOfWork.RollbackTransactionAsync(cancellation)
					.ConfigureAwait(false);

				throw;
			}
		}
	}
}
