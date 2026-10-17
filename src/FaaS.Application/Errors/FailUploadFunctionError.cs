using DddSharedKernel.Domain.Results;
using FaaS.Application.Resources;

namespace FaaS.Application.Errors
{

	public sealed class FailUploadFunctionError : Error
	{
		public FailUploadFunctionError() 
			: base(ErrorMessages.FailUploadFunction)
		{
		}
	}
}
