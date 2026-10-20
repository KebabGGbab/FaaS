using DddSharedKernel.Domain.Results;
using FaaS.Application.Resources;

namespace FaaS.Application.Errors
{
	public sealed class GetUserFunctionsError : Error
	{
		public GetUserFunctionsError() 
			: base(ErrorMessages.GetUserFunctions)
		{
		}
	}
}
