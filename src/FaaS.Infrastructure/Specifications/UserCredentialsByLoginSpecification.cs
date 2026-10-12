using DddSharedKernel.Domain.Specification;
using FaaS.Domain.Aggregates.UserCredentialsAggregate;
using System.Linq.Expressions;

namespace FaaS.Infrastructure.Specifications
{
	public sealed class UserCredentialsByLoginSpecification : Specification<UserCredentials>
	{
		public UserCredentialsByLoginSpecification(string login) 
			: base(GetExpression(login))
		{
		}

		private static Expression<Func<UserCredentials, bool>> GetExpression(string login)
		{
			return u => u.Login.Value == login;
		}
	}
}
