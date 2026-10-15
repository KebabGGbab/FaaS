namespace FaaS.Application.Services.FileResolverService
{
	public interface IFileResolver
	{
		DirectoryInfo GetDirectoryFunction(string directoryName);

		FileInfo GetFunction(string directoryName, string fileName);
	}
}
