namespace FaaS.Application.Services.FunctionService
{
	public interface IFunctionExecutor
	{
		public Task<FunctionResult> ExecuteAsync(Dictionary<string, object> args, CancellationToken cancellation = default);
	}
}
