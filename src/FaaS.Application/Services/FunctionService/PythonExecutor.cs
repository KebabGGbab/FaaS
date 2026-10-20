using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Diagnostics;

namespace FaaS.Application.Services.FunctionService
{
	public sealed class PythonExecutor : IFunctionExecutor
	{
		private readonly string _filePath;

		public PythonExecutor(string filePath)
		{
			_filePath = filePath;
		}

		public Task<FunctionResult> ExecuteAsync(Dictionary<string, object> args, CancellationToken cancellation = default)
		{
			ScriptEngine engine = Python.CreateEngine();
			ScriptScope scope = engine.CreateScope();
			engine.ExecuteFile(_filePath, scope);
			dynamic method = scope.GetVariable("execute");
			Stopwatch stopwatch = Stopwatch.StartNew();
			dynamic result = method(args).ToString();
			stopwatch.Stop();
			FunctionResult functionResult = new(result, stopwatch.ElapsedMilliseconds);

			return Task.FromResult(functionResult);
		}
	}
}
