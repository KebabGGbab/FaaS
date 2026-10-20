using DddSharedKernel.Domain.Results;
using FaaS.Application.Resources;

namespace FaaS.Application.Errors
{
	public sealed class GetFunctionError : Error
	{
		public GetFunctionError() 
			: base(ErrorMessages.GetFunction)
		{
		}
	}
}
