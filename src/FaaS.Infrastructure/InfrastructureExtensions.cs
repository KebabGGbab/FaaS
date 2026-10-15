using DddSharedKernel.Domain.Events;
using DddSharedKernel.Infrastructure;
using FaaS.Domain.Aggregates.FunctionAggregate;
using FaaS.Domain.Aggregates.FunctionLogAggregate;
using FaaS.Domain.Aggregates.UserAggregate;
using FaaS.Domain.Aggregates.UserCredentialsAggregate;
using FaaS.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace FaaS.Infrastructure
{
	public static class InfrastructureExtensions
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
		{
			ArgumentNullException.ThrowIfNull(services);
			ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

			return services
				.AddScoped<IUserRepository, UserRepository>()
				.AddScoped<IUserCredentialsRepository, UserCredentialsRepository>()
				.AddScoped<IFunctionRepository, FunctionRepository>()
				.AddScoped<IFunctionLogRepository, FunctionLogRepository>()
				.AddScoped<IApplicationUnitOfWork, ApplicationUnitOfWork>()
				.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>()
				.AddDbContextPool<ApplicationDbContext>(
					(s, b) => b.UseSqlite(s.GetRequiredService<IConfiguration>().GetConnectionString(connectionString)));
		}
	}
}
