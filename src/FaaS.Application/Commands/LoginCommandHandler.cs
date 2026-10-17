using DddSharedKernel.Domain.Results;
using DddSharedKernel.Domain.Specification;
using FaaS.Application.Errors;
using FaaS.Domain.Aggregates.UserAggregate;
using FaaS.Domain.Aggregates.UserCredentialsAggregate;
using FaaS.Infrastructure.DataAccess;
using FaaS.Infrastructure.Specifications;

namespace FaaS.Application.Commands
{
	public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, Result<User>>
	{
		private readonly IApplicationUnitOfWork _unitOfWork;

		public LoginCommandHandler(IApplicationUnitOfWork unitOfWork)
		{
			ArgumentNullException.ThrowIfNull(unitOfWork);

			_unitOfWork = unitOfWork;
		}

		public async Task<Result<User>> HandleAsync(LoginCommand command, CancellationToken cancellation = default)
		{
			Result<UserCredentials> credentialsResult = UserCredentials.Create(command.Login, command.Password);

			if (credentialsResult.IsFailure)
			{
				return Result.Fail<User>(credentialsResult.Errors);
			}

			UserCredentials? credentials = await _unitOfWork.CredentialsRepository.FirstOrDefaultAsync(
				new UserCredentialsByLoginSpecification(credentialsResult.Value.Login)
					.And(new UserCredentialsByPasswordSpecification(credentialsResult.Value.Password))
						.ToExpression(), 
				cancellation);

			if (credentials == null)
			{
				return Result.Fail<User>(new LoginOrPasswordIncorrectError());
			}

			User? user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(new UserByUserCredentialsSpecification(credentials.Id).ToExpression(), cancellation);

			if (user == null)
			{
				return Result.Fail<User>(new LoginOrPasswordIncorrectError());
			}

			return Result.Ok(user);
		}
	}
}
