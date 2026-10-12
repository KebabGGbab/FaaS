using DddSharedKernel.Domain.Specification;
using FaaS.Domain.Aggregates.FunctionAggregate;
using System.Linq.Expressions;

namespace FaaS.Infrastructure.Specifications
{
	public sealed class FunctionByUserIdSpecification : Specification<Function>
	{
		public FunctionByUserIdSpecification(int userId) 
			: base(GetExpression(userId))
		{
		}

		private static Expression<Func<Function, bool>> GetExpression(int userId)
		{
			return f => f.UserId == userId;
		}
	}
}
