namespace FaaS.Application.Queries
{
	public sealed class GetFunctionQuery : IQuery
	{
		public int FunctionId { get; }

		public GetFunctionQuery(int functionId)
		{
			FunctionId = functionId;
		}
	}
}
