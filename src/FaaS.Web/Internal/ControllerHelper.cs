using DddSharedKernel.Domain.Results;
using FaaS.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FaaS.Web.Internal
{
	internal static class ControllerHelper
	{
		private const string SUFFIX = nameof(Controller);

		public static string AuthControllerName { get; } = GetControllerNameWithoutSuffix(nameof(AuthController));

		public static string ProfileControllerName { get; } = GetControllerNameWithoutSuffix(nameof(ProjectController));

		private static string GetControllerNameWithoutSuffix(string name)
		{
			return name[..^SUFFIX.Length];
		}

		public static void FillModelStateErrors(ModelStateDictionary modelState, Result result)
		{
			foreach (Error error in result.Errors)
			{
				modelState.AddModelError(string.Empty, error.Message);
			}
		}
	}
}
