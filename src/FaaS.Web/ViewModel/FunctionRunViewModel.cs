using FaaS.Domain.Aggregates.FunctionAggregate;

namespace FaaS.Web.ViewModel
{
	public class FunctionRunViewModel
	{
		public int FunctionId { get; }

		public int UserId { get; }

		public string FunctionName { get; }

		public string? Args { get; }

		public string? Result { get; }

		public long? ExecutionTime { get; }

		public FunctionRunViewModel()
		{
		}

		public FunctionRunViewModel(int functionId, int userId, string functionName)
		{
			FunctionId = functionId;
			UserId = userId;
			FunctionName = functionName;
		}

		public FunctionRunViewModel(int functionId, int userId, string functionName, string args, string result, long executionTime)
		{
			FunctionId = functionId;
			UserId = userId;
			FunctionName = functionName;
			Args = args;
			Result = result;
			ExecutionTime = executionTime;
		}

		public static FunctionRunViewModel From(Function function)
		{
			return new FunctionRunViewModel(function.Id, function.UserId, function.Name);
		}
	}
}
