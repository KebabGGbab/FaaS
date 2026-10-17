using DddSharedKernel.Domain.Results;
using FaaS.Application.Resources;

namespace FaaS.Application.Errors
{
	public sealed class FailDeleteFunctionError : Error
	{
		public FailDeleteFunctionError() 
			: base(ErrorMessages.FailDeleteFunction)
		{
		}
	}
}
