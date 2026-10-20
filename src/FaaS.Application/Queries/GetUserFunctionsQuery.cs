namespace FaaS.Application.Queries
{
	public sealed class GetUserFunctionsQuery : IQuery
	{
		public int UserId { get; }

		public GetUserFunctionsQuery(int userId)
		{
			UserId = userId;
		}
	}
}
