using DddSharedKernel.Domain.BaseModels;
using DddSharedKernel.Domain.Results;
using FaaS.Domain.Aggregates.UserCredentialsAggregate.Errors;

namespace FaaS.Domain.Aggregates.UserCredentialsAggregate
{
	public sealed class Password : ValueObject
	{
		private const int MIN_LENGTH = 8;

		public string Value { get; }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
		private Password()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
		{
		}

		private Password(string password)
		{
			Value = password;
		}

		public static Result CanCreate(string password)
		{
			if (password == null)
			{
				return Result.Fail(new PasswordIsNullError());
			}

			if (password.Length < MIN_LENGTH)
			{
				return Result.Fail(new PasswordIsSmallError(MIN_LENGTH));
			}

			return Result.Ok();
		}

		public static Result<Password> Create(string password)
		{
			Result canCreate = CanCreate(password);

			if (canCreate.IsSuccess)
			{
				return Result.Ok(new Password(password));
			}

			return Result.Fail<Password>(canCreate.Errors);
		}
			 
		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		public static implicit operator string(Password password)
		{
			return password.Value;
		}
	}
}
