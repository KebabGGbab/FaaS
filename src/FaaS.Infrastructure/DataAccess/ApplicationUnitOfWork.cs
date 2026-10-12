using DddSharedKernel.Domain.Events;
using DddSharedKernel.Infrastructure;
using FaaS.Domain.Aggregates.FunctionAggregate;
using FaaS.Domain.Aggregates.FunctionLogAggregate;
using FaaS.Domain.Aggregates.UserAggregate;
using FaaS.Domain.Aggregates.UserCredentialsAggregate;

namespace FaaS.Infrastructure.DataAccess
{
	public sealed class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
	{
		public IUserCredentialsRepository CredentialsRepository { get; }

		public IUserRepository UserRepository { get; }

		public IFunctionRepository FunctionRepository { get; }

		public IFunctionLogRepository FunctionLogRepository { get; }

		public ApplicationUnitOfWork(ApplicationDbContext db, IDomainEventDispatcher eventDispatcher) 
			: base(db, eventDispatcher)
		{
			ArgumentNullException.ThrowIfNull(db);
			ArgumentNullException.ThrowIfNull(eventDispatcher);

			CredentialsRepository = new UserCredentialsRepository(db);
			UserRepository = new UserRepository(db);
			FunctionRepository = new FunctionRepository(db);
			FunctionLogRepository = new FunctionLogRepository(db);
		}
	}
}
