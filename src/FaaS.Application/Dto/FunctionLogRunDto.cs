namespace FaaS.Application.Dto
{
	public sealed class FunctionLogRunDto
	{
		public int FunctionId { get; }

		public string FunctionName { get; }

		public long ExecutionTime { get; }

		public string Result { get; }

		public FunctionLogRunDto(int functionId, string functionName, long executionTime, string result)
		{
			FunctionId = functionId;
			FunctionName = functionName;
			ExecutionTime = executionTime;
			Result = result;
		}
	}
}
