using DddSharedKernel.Domain.BaseModels;
using DddSharedKernel.Domain.Results;
using FaaS.Domain.Aggregates.UserCredentialsAggregate.Errors;

namespace FaaS.Domain.Aggregates.UserCredentialsAggregate
{
	public sealed class Login : ValueObject
	{
		private const int MIN_LENGTH = 3;

		public string Value { get; }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
		private Login()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
		{
		}

		private Login(string login)
		{
			Value = login;
		}

		public static Result CanCreate(string login)
		{
			if (login == null)
			{
				return Result.Fail(new LoginIsNullError());
			}

			if (login.Length < MIN_LENGTH)
			{
				return Result.Fail(new LoginIsSmallError(MIN_LENGTH));
			}

			return Result.Ok();
		}

		public static Result<Login> Create(string login)
		{
			Result canCreate = CanCreate(login);

			if (canCreate.IsSuccess)
			{
				return Result.Ok(new Login(login));
			}

			return Result.Fail<Login>(canCreate.Errors);
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		public static implicit operator string(Login login)
		{
			return login.Value;
		}
	}
}
