using DddSharedKernel.Domain.Specification;
using FaaS.Domain.Aggregates.UserCredentialsAggregate;
using System.Linq.Expressions;

namespace FaaS.Infrastructure.Specifications
{
	public class UserCredentialsByPasswordSpecification : Specification<UserCredentials>
	{
		public UserCredentialsByPasswordSpecification(string password) 
			: base(GetExpression(password))
		{
		}

		private static Expression<Func<UserCredentials, bool>> GetExpression(string password)
		{
			return u => u.Password.Value == password;
		}
	}
}
