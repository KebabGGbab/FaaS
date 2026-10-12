using DddSharedKernel.Domain.Results;
using FaaS.Domain.Resources;
using System.Globalization;
using System.Text;

namespace FaaS.Domain.Aggregates.UserAggregate.Errors;

internal sealed class NameIsSmallError : Error
{
	private static readonly CompositeFormat s_message = CompositeFormat.Parse(ErrorMessages.NameIsSmall);

	public NameIsSmallError(int minLength) 
		: base(string.Format(CultureInfo.CurrentCulture, s_message, minLength))
	{
	}
}

internal sealed class NameIsBigError : Error
{
	private static readonly CompositeFormat s_message = CompositeFormat.Parse(ErrorMessages.NameIsBig);

	public NameIsBigError(int maxLength) 
		: base(string.Format(CultureInfo.CurrentCulture, s_message, maxLength))
	{
	}
}
