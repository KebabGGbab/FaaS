using FaaS.Application.Commands;
using FaaS.Web.Dto;

namespace FaaS.Web.DtoExtensions
{
	public static class FunctionUploadDtoExtensions
	{
		public static UploadFunctionCommand ToUploadFunctionCommand(this FunctionUploadDto dto, int userId)
		{
			return new UploadFunctionCommand(userId, dto.Name, dto.File.FileName, dto.Description, dto.Language.ToString(), dto.File.OpenReadStream());
		}
	}
}
