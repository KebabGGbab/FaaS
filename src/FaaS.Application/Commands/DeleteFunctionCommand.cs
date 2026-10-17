namespace FaaS.Application.Commands
{
	public sealed class DeleteFunctionCommand : ICommand
	{
		public int FunctionId { get; }

		public DeleteFunctionCommand(int functionId)
		{
			FunctionId = functionId;
		}
	}
}
