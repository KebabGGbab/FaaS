using DddSharedKernel.Domain.Results;
using FaaS.Application.Commands;
using FaaS.Application.Dto;
using FaaS.Application.Queries;
using FaaS.Domain.Aggregates.FunctionAggregate;
using FaaS.Web.Dto;
using FaaS.Web.DtoExtensions;
using FaaS.Web.Internal;
using FaaS.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FaaS.Web.Controllers
{
	[Authorize]
	public class ProjectController : Controller
	{
		[HttpGet]
		public async Task<IActionResult> List([FromServices] IQueryHandler<GetUserFunctionsQuery, Result<IEnumerable<Function>>> handler)
		{
			Result<IEnumerable<Function>> result = await handler.HandleAsync(new GetUserFunctionsQuery(GetUserId()))
				.ConfigureAwait(false);

			if (result.IsFailure)
			{
				ControllerHelper.FillModelStateErrors(ModelState, result);

				return View();
			}

			return View(new FunctionListViewModel(result.Value.Select(FunctionViewModel.From)));
		}

		[HttpGet]
		public IActionResult Upload()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Upload([FromForm] FunctionUploadDto dto, [FromServices] ICommandHandler<UploadFunctionCommand, Result> handler)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			Result result = await handler.HandleAsync(dto.ToUploadFunctionCommand(GetUserId()))
				.ConfigureAwait(false);

			if (result.IsFailure)
			{
				ControllerHelper.FillModelStateErrors(ModelState, result);
			}

			return View();
		}

		[HttpGet("[controller]/[action]/{functionId}")]
		public async Task<IActionResult> Run([FromRoute] int functionId, [FromServices] IQueryHandler<GetFunctionQuery, Result<Function>> handler)
		{
			Result<Function> result = await handler.HandleAsync(new GetFunctionQuery(functionId));

			if (result.IsFailure)
			{
				ControllerHelper.FillModelStateErrors(ModelState, result);

				return NotFound();
			}

			return View(FunctionRunViewModel.From(result.Value));
		}

		[HttpPost("[controller]/[action]/{functionId}")]
		public async Task<IActionResult> Run([FromRoute] int functionId, [FromForm] FunctionRunDto dto, [FromServices] ICommandHandler<RunFunctionCommand, Result<FunctionLogRunDto>> handler)
		{
			Result<FunctionLogRunDto> result = await handler.HandleAsync(new RunFunctionCommand(functionId, dto.Args))
				.ConfigureAwait(false);

			if (result.IsFailure)
			{
				ControllerHelper.FillModelStateErrors(ModelState, result);
			}

			FunctionLogRunDto log = result.Value;

			return View(new FunctionRunViewModel(log.FunctionId, GetUserId(), log.FunctionName, dto.Args, log.Result, log.ExecutionTime));
		}

		[HttpPost("[controller]/[action]/{functionId}")]
		public async Task<IActionResult> Delete([FromRoute] int functionId, [FromServices] ICommandHandler<DeleteFunctionCommand, Result> handler)
		{
			Result result = await handler.HandleAsync(new DeleteFunctionCommand(functionId))
				.ConfigureAwait(false);

			if (result.IsFailure)
			{
				ControllerHelper.FillModelStateErrors(ModelState, result);
			}

			return Ok();
		}

		private int GetUserId()
		{
			return int.Parse(User.Claims.First(c => c.Type == ClaimTypes.Name).Value);
		}
	}
}