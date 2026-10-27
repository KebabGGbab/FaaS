using DddSharedKernel.Domain.Results;
using FaaS.Application.Dto;
using FaaS.Domain.Aggregates.UserAggregate;
using Microsoft.Extensions.DependencyInjection;

namespace FaaS.Application.Commands
{
	public static class CommandExtensions
	{
		public static IServiceCollection AddCommands(this IServiceCollection services)
		{
			return services
				.AddScoped<ICommandHandler<RegisterCommand, Result<User>>, RegisterCommandHandler>()
				.AddScoped<ICommandHandler<LoginCommand, Result<User>>, LoginCommandHandler>()
				.AddScoped<ICommandHandler<UploadFunctionCommand, Result>, UploadFunctionCommandHandler>()
				.AddScoped<ICommandHandler<DeleteFunctionCommand, Result>, DeleteFunctionCommandHandler>()
				.AddScoped<ICommandHandler<RunFunctionCommand, Result<FunctionLogRunDto>>, RunFunctionCommandHandler>();
		}
	}
}
