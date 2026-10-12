using DddSharedKernel.Domain.Data;
using FaaS.Domain.Aggregates.FunctionAggregate;
using FaaS.Domain.Aggregates.FunctionLogAggregate;
using FaaS.Domain.Aggregates.UserAggregate;
using FaaS.Domain.Aggregates.UserCredentialsAggregate;

namespace FaaS.Infrastructure.DataAccess
{
	public interface IApplicationUnitOfWork : IUnitOfWork
	{
		IUserCredentialsRepository CredentialsRepository { get; }

		IUserRepository UserRepository { get; }

		IFunctionRepository FunctionRepository { get; }

		IFunctionLogRepository FunctionLogRepository { get; }
	}
}
