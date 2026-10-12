using DddSharedKernel.Domain.BaseModels;
using DddSharedKernel.Domain.Results;
using FaaS.Domain.Aggregates.UserAggregate.Errors;

namespace FaaS.Domain.Aggregates.UserAggregate
{
	public class Name : ValueObject
	{
		private const int MIN_VALUE = 3;
		private const int MAX_VALUE = 30;

		public string Value { get; }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
		private Name()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
		{
		}

		private Name(string name)
		{
			Value = name;
		}

		public static Result CanCreate(string name)
		{
			if (name.Length < MIN_VALUE)
			{
				return Result.Fail(new NameIsSmallError(MIN_VALUE));
			}

			if (name.Length > MAX_VALUE)
			{
				return Result.Fail(new NameIsBigError(MAX_VALUE));
			}

			return Result.Ok();
		}

		public static Result<Name> Create(string name)
		{
			Result canCreate = CanCreate(name);

			if (canCreate.IsSuccess)
			{
				return Result.Ok(new Name(name));
			}

			return Result.Fail<Name>(canCreate.Errors);
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}
	}
}
