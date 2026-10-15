namespace FaaS.Application.Services.FileResolverService
{
	public sealed class FileResolver : IFileResolver
	{
		private const string STORAGE_DIRECTORY_NAME = "storage";

		private const string FUNCTIONS_DIRECTORY_NAME = "functions";

		private static readonly string s_functions_directory_path = Path.Combine(Environment.CurrentDirectory, STORAGE_DIRECTORY_NAME, FUNCTIONS_DIRECTORY_NAME);

		public DirectoryInfo GetDirectoryFunction(string directoryName)
		{
			return new DirectoryInfo(Path.Combine(s_functions_directory_path, directoryName));
		}

		public FileInfo GetFunction(string directoryName, string fileName)
		{
			return new FileInfo(Path.Combine(s_functions_directory_path, directoryName, fileName));
		}
	}
}
