using FaaS.Shared;
using System.Diagnostics;
using System.Reflection;

namespace FaaS.Application.Services.FunctionService
{
	public sealed class CSharpExecutor : IFunctionExecutor, IDisposable
	{
		private readonly string _filePath;
		private readonly FunctionLoadContext _loadContext;

		private bool _disposed;

		public CSharpExecutor(string filePath)
		{
			_filePath = filePath;
			_loadContext = new FunctionLoadContext(filePath);
		}

		public async Task<FunctionResult> ExecuteAsync(Dictionary<string, object> args, CancellationToken cancellation = default)
		{
			ObjectDisposedException.ThrowIf(_disposed, this);

			Assembly assembly = LoadAssembly();
			IFunction function = GetFunction(assembly);
			Stopwatch stopwatch = Stopwatch.StartNew();
			string result = await function.ExecuteAsync(args, cancellation)
				.ConfigureAwait(false);
			stopwatch.Stop();
			FunctionResult functionResult = new(result, stopwatch.ElapsedMilliseconds);

			return functionResult;
		}

		private Assembly LoadAssembly()
		{
			return _loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(_filePath)));
		}

		private static IFunction GetFunction(Assembly assembly)
		{
			Type type = assembly.GetTypes().First(t => typeof(IFunction).IsAssignableFrom(t));

			return (IFunction)Activator.CreateInstance(type)!;
		}

		public void Dispose()
		{
			if (_disposed)
			{
				return;
			}

			_disposed = true;
			_loadContext.Unload();

			GC.SuppressFinalize(this);
		}
	}
}
