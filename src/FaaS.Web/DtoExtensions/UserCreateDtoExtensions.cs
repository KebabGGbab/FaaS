using FaaS.Application.Commands;
using FaaS.Web.Dto;

namespace FaaS.Web.DtoExtensions
{
	internal static class UserCreateDtoExtensions
	{
		public static RegisterCommand ToRegisterCommand(this UserCreateDto dto)
		{
			return new RegisterCommand(dto.Login, dto.Password, dto.Name);
		}
	}
}
