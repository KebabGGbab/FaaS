using DddSharedKernel.Domain.Results;
using FaaS.Domain.Resources;
using System.Globalization;
using System.Text;

namespace FaaS.Domain.Aggregates.UserCredentialsAggregate.Errors;

internal sealed class PasswordIsNullError : Error
{
	public PasswordIsNullError() 
		: base(ErrorMessages.PasswordIsNull)
	{
	}
}

internal sealed class PasswordIsSmallError : Error
{
	private static readonly CompositeFormat s_message = CompositeFormat.Parse(ErrorMessages.PasswordIsSmall);

	public PasswordIsSmallError(int expectedLength)
		: base(string.Format(CultureInfo.CurrentCulture, s_message, expectedLength))
	{
	}
}
