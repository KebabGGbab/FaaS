using FaaS.Application.Commands;
using FaaS.Application.Queries;
using FaaS.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FaaS.Application
{
	public static class ApplicationExtensions
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			return services
				.AddCommands()
				.AddQueries()
				.AddServices();
		}
	}
}
