using System.ComponentModel.DataAnnotations;

namespace FaaS.Web.Dto
{
	public class UserLogInDto
	{
		[Required]
		public string Login { get; set; } = null!;

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		public UserLogInDto()
		{
		}
	}
}
