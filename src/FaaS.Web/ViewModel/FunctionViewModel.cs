using FaaS.Domain.Aggregates.FunctionAggregate;
using FaaS.Web.Dto;

namespace FaaS.Web.ViewModel
{
	public sealed class FunctionViewModel
	{
		public int Id { get; }

		public string Name { get; } = null!;

		public string? Description { get; }

		public LanguagesDto Language { get; }

		public FunctionViewModel(int id, string name, string? description, LanguagesDto language)
		{
			Id = id;
			Name = name;
			Description = description;
			Language = language;
		}

		public static FunctionViewModel From(Function function)
		{
			return new FunctionViewModel(
				function.Id, 
				function.Name, 
				function.Description, 
				Enum.Parse<LanguagesDto>(function.Language.ToString()));
		}
	}
}
