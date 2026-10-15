using DddSharedKernel.Domain.Results;
using FaaS.Application.Resources;

namespace FaaS.Application.Errors
{
	internal sealed class LoginIsBusyError : Error
	{
		public LoginIsBusyError() 
			: base(ErrorMessages.LoginIsBusy)
		{
		}
	}
}
