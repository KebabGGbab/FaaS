using FaaS.Application.Services.FileResolverService;
using FaaS.Application.Services.FunctionService;
using Microsoft.Extensions.DependencyInjection;

namespace FaaS.Application.Services
{
	internal static class ServicesExtensions
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			return services
				.AddSingleton<IFileResolver, FileResolver>()
				.AddSingleton<IFunctionServiceFactory, FunctionServiceFactory>();
		}
	}
}
