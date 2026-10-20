namespace FaaS.Application.Services.FunctionService
{
	public sealed class FunctionResult
	{
		public string Result { get; }

		public long ExecutionTime { get; }

		public FunctionResult(string result, long executionTime)
		{
			Result = result;
			ExecutionTime = executionTime;
		}
	}
}
