using DddSharedKernel.Domain.Results;
using FaaS.Application.Resources;

namespace FaaS.Application.Errors
{
	internal sealed class LoginOrPasswordIncorrectError : Error
	{
		public LoginOrPasswordIncorrectError() 
			: base(ErrorMessages.LoginOrPasswordIncorrect)
		{
		}
	}
}
