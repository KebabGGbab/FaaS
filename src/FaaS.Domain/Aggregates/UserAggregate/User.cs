using DddSharedKernel.Domain.BaseModels;
using DddSharedKernel.Domain.Results;

namespace FaaS.Domain.Aggregates.UserAggregate
{
	public class User : AggregateRoot<int>
	{
		public int CredentialsId { get; }

		public Name Name { get; }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
		private User()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
		{
		}

		private User(Name name, int credentialsId)
		{
			Name = name;
			CredentialsId = credentialsId;
		}

		public static Result<User> Create(string name, int credentialsId)
		{
			Result<Name> n = Name.Create(name);

			if (n.IsFailure)
			{
				return Result.Fail<User>(n.Errors);
			}

			return Result.Ok(new User(n.Value, credentialsId));
		}
	}
}
