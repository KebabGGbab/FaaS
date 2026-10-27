namespace FaaS.Application.Commands
{
	public sealed class RunFunctionCommand : ICommand
	{
		public int FunctionId { get; }

		public string Args { get; } = null!;

		public RunFunctionCommand(int functionId, string args)
		{
			FunctionId = functionId;
			Args = args;
		}
	}
}