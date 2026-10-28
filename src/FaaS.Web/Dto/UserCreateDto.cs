using System.ComponentModel.DataAnnotations;

namespace FaaS.Web.Dto
{
	public sealed class UserCreateDto : UserLogInDto
	{
		[Length(3, 30)]
		[Required]
		public string Name { get; set; } = null!;

		public UserCreateDto()
		{
		}
	}
}
