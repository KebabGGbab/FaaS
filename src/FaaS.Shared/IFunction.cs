namespace FaaS.Shared
{
	public interface IFunction
	{
		Task<string> ExecuteAsync(Dictionary<string, object> args, CancellationToken cancellation);
	}
}
