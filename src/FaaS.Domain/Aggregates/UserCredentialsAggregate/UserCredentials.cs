using DddSharedKernel.Domain.BaseModels;
using DddSharedKernel.Domain.Results;

namespace FaaS.Domain.Aggregates.UserCredentialsAggregate
{
	public sealed class UserCredentials : AggregateRoot<int>
	{
		public Login Login { get; }

		public Password Password { get; }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
		private UserCredentials()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
		{
		}

		private UserCredentials(Login login, Password password)
		{
			Login = login;
			Password = password;
		}

		public static Result<UserCredentials> Create(string login, string password)
		{
			Result<Login> log = Login.Create(login);
			Result<Password> pas = Password.Create(password);

			if (log.IsSuccess && pas.IsSuccess)
			{
				return Result.Ok(new UserCredentials(log.Value, pas.Value));
			}

			return Result.Fail<UserCredentials>(log.Errors.Concat(pas.Errors));
		}
	}
}
