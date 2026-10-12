using DddSharedKernel.Domain.BaseModels;

namespace FaaS.Domain.Aggregates.FunctionLogAggregate
{
	public sealed class FunctionLog : AggregateRoot<int>
	{
		public int FunctionId { get; }

		public bool IsSuccess { get; }

		public DateTime ExecutedAt { get; }

		public long ExecutionTime { get; }

		public string Result { get; } = null!;

		private FunctionLog()
		{
		}

		public FunctionLog(int functionId, bool isSuccess, DateTime executedAt, long executionTime, string result)
		{
			FunctionId = functionId;
			IsSuccess = isSuccess;
			ExecutedAt = executedAt;
			ExecutionTime = executionTime;
			Result = result;
		}
	}
}
