namespace FaaS.Application.Commands
{
	public sealed class UploadFunctionCommand : ICommand
	{
		public int UserId { get; }

		public string Name { get; }

		public string FileName { get; }

		public string? Description { get; }

		public string Language { get; }

		public Stream Stream { get; }

		public UploadFunctionCommand(int userId, string name, string fileName, string? description, string language, Stream stream)
		{
			UserId = userId;
			Name = name;
			FileName = fileName;
			Description = description;
			Language = language;
			Stream = stream;
		}
	}
}
