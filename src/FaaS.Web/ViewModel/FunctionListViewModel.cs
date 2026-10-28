namespace FaaS.Web.ViewModel
{
	public sealed class FunctionListViewModel
	{
		public IEnumerable<FunctionViewModel> Functions { get; } 

		public FunctionListViewModel(IEnumerable<FunctionViewModel> functions)
		{
			Functions = functions;
		}
	}
}
