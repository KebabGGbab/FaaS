using System.ComponentModel.DataAnnotations;

namespace FaaS.Web.Dto
{
	public enum LanguagesDto
	{
		[Display(Name = "Python")]
		Python,

		[Display(Name = "C#")]
		CSharp
	}
}
