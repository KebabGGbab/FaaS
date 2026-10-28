using FaaS.Application;
using FaaS.Infrastructure;
using FaaS.Web.Controllers;
using FaaS.Web.Internal;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FaaS.Web;

public partial class Program
{
	private static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		builder.Services.AddInfrastructure("ApplicationDB");
		builder.Services.AddApplication();
		builder.Services.AddControllersWithViews();
		builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie(o =>
			{
				o.LoginPath = $"/{ControllerHelper.AuthControllerName}/{nameof(AuthController.LogIn)}";
			});
		builder.Services.AddAuthorization();

		WebApplication app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}
		else
		{
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseRouting();
		app.UseAuthentication();
		app.UseAuthorization();

		app.MapStaticAssets().ShortCircuit();
		app.MapControllerRoute("default", "{controller}/{action}/{id?}", new { controller = "Project", action = "List" });

		app.Run();
	}
}