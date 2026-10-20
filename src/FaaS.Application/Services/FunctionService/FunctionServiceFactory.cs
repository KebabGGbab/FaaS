using FaaS.Application.Services.FileResolverService;
using FaaS.Domain.Aggregates.FunctionAggregate;

namespace FaaS.Application.Services.FunctionService
{
	public sealed class FunctionServiceFactory : IFunctionServiceFactory
	{
		private readonly IFileResolver _fileResolver;

		public FunctionServiceFactory(IFileResolver fileResolver)
		{
			ArgumentNullException.ThrowIfNull(fileResolver);

			_fileResolver = fileResolver;
		}

		public IFunctionService Create(Function function)
		{
			return new FunctionService(_fileResolver, function);
		}
	}
}
