using DddSharedKernel.Domain.Results;
using FaaS.Application.Commands;
using FaaS.Domain.Aggregates.UserAggregate;
using FaaS.Web.Dto;
using FaaS.Web.DtoExtensions;
using FaaS.Web.Internal;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FaaS.Web.Controllers
{
	public class AuthController : Controller
	{
		[HttpGet]
		[AllowAnonymous]
		public IActionResult LogIn()
		{
			if (User.Identity?.IsAuthenticated == true)
			{
				return RedirectToAction(nameof(ProjectController.List), ControllerHelper.ProfileControllerName);
			}

			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> LogIn([FromForm] UserLogInDto userLogIn, [FromServices] ICommandHandler<LoginCommand, Result<User>> handler)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			Result<User> result = await handler.HandleAsync(userLogIn.ToLoginCommand());

			if (result.IsFailure)
			{
				ControllerHelper.FillModelStateErrors(ModelState, result);

				return View();
			}

			await Auth(result.Value);

			return RedirectToAction(nameof(ProjectController.List), ControllerHelper.ProfileControllerName);
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult SignIn()
		{
			if (User.Identity?.IsAuthenticated == true)
			{
				return RedirectToAction(nameof(ProjectController.List), ControllerHelper.ProfileControllerName);
			}

			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SignIn([FromForm] UserCreateDto userCreate, [FromServices] ICommandHandler<RegisterCommand, Result<User>> handler)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			Result<User> result = await handler.HandleAsync(userCreate.ToRegisterCommand());

			if (result.IsFailure)
			{
				ControllerHelper.FillModelStateErrors(ModelState, result);

				return View();
			}

			await Auth(result.Value);

			return RedirectToAction(nameof(ProjectController.List), ControllerHelper.ProfileControllerName);
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> LogOut()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return RedirectToAction(nameof(LogIn));
		}

		private async Task Auth(User user)
		{
			List<Claim> claims = 
			[
				new Claim(ClaimTypes.Name, user.Id.ToString()),
			];
			ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
		}
	}
}
