using DddSharedKernel.Domain.BaseModels;

namespace FaaS.Domain.Aggregates.FunctionAggregate
{
	public sealed class Function : AggregateRoot<int>
	{
		public int UserId { get; }

		public string Name { get; }

		public DateTime CreatedAt { get; }

		public string? Description { get; }

		public string FileName { get; }

		public string DirectoryName { get; }

		public Languages Language { get; }

#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
		private Function()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
		{
		}

		public Function(int userId, string name, string? description, string fileName, string directoryName, Languages language, DateTime createdAt)
		{
			UserId = userId;
			Name = name;
			Description = description;
			FileName = fileName;
			DirectoryName = directoryName;
			Language = language;
			CreatedAt = createdAt;
		}
	}
}
