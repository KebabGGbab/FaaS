using DddSharedKernel.Domain.Specification;
using FaaS.Domain.Aggregates.UserAggregate;
using System.Linq.Expressions;

namespace FaaS.Infrastructure.Specifications
{
	public sealed class UserByUserCredentialsSpecification : Specification<User>
	{
		public UserByUserCredentialsSpecification(int credentialsId) 
			: base(GetExpression(credentialsId))
		{
		}

		private static Expression<Func<User, bool>> GetExpression(int credentialsId)
		{
			return u => u.CredentialsId == credentialsId;
		}
	}
}
