using DddSharedKernel.Domain.Results;
using FaaS.Domain.Resources;
using System.Globalization;
using System.Text;

namespace FaaS.Domain.Aggregates.UserCredentialsAggregate.Errors;

internal sealed class LoginIsNullError : Error
{
	public LoginIsNullError() 
		: base(ErrorMessages.LoginIsNull)
	{
	}
}

internal sealed class LoginIsSmallError : Error
{
	private static readonly CompositeFormat s_message = CompositeFormat.Parse(ErrorMessages.LoginIsSmall);

	public LoginIsSmallError(int expectedLength)
		: base(string.Format(CultureInfo.CurrentCulture, s_message, expectedLength))
	{
	}
}
