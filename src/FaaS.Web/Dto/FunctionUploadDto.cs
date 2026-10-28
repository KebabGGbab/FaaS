using System.ComponentModel.DataAnnotations;

namespace FaaS.Web.Dto
{
	public sealed class FunctionUploadDto
	{
		[Required]
		[Length(1, 100)]
		public string Name { get; set; } = null!;

		[MaxLength(1000)]
		public string? Description { get; set; }

		[Required]
		public LanguagesDto Language { get; set; }

		[Required]
		public IFormFile File { get; set; } = null!;

		public FunctionUploadDto()
		{
		}
	}
}
