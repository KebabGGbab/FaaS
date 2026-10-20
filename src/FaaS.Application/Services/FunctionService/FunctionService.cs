using FaaS.Application.Services.FileResolverService;
using FaaS.Domain.Aggregates.FunctionAggregate;
using FaaS.Domain.Aggregates.FunctionLogAggregate;

namespace FaaS.Application.Services.FunctionService
{
	public class FunctionService : IFunctionService
	{
		private readonly IFileResolver _fileResolver;

		public Function Function { get; }

		public FunctionService(IFileResolver fileResolver, Function function)
		{
			ArgumentNullException.ThrowIfNull(fileResolver);
			ArgumentNullException.ThrowIfNull(function);

			_fileResolver = fileResolver;
			Function = function;
		}

		public async Task<FunctionLog> ExecuteAsync(string args, CancellationToken cancellation = default)
		{
			string filePath = _fileResolver.GetFunction(Function.DirectoryName, Function.FileName).FullName;
			Dictionary<string, object> arguments = ExtractArguments(args);
			DateTime executedAt = DateTime.UtcNow;
			FunctionResult functionResult;

			switch (Function.Language)
			{
				case Languages.CSharp:
				{
					using CSharpExecutor cSharpExecutor = new(filePath);
					functionResult = await cSharpExecutor.ExecuteAsync(arguments, cancellation)
						.ConfigureAwait(false);
					
					break;
				}
				case Languages.Python:
					{
						PythonExecutor pythonExecutor = new(filePath);
						functionResult = await pythonExecutor.ExecuteAsync(arguments, cancellation);

						break;
					}
				default:
				{
					throw new InvalidOperationException();
				}
			}

			FunctionLog log = new(Function.Id, true, executedAt, functionResult.ExecutionTime, functionResult.Result);

			return log;
		}

		private static Dictionary<string, object> ExtractArguments(string args)
		{
			return args.Split(',').Select(x =>
			{
				string[] kv = x.Split('=');

				return new KeyValuePair<string, object>(kv[0].Trim(), kv[1].Trim());
			}).ToDictionary();
		}
	}
}
