using FaaS.Application.Commands;
using FaaS.Web.Dto;

namespace FaaS.Web.DtoExtensions
{
	internal static class UserLogInDtoExtensions
	{
		public static LoginCommand ToLoginCommand(this UserLogInDto dto)
		{
			return new LoginCommand(dto.Login, dto.Password);
		}
	}
}
