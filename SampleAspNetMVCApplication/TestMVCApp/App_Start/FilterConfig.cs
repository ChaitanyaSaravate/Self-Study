using System.Web;
using System.Web.Mvc;
using TestMVCApp.App_Start;

namespace TestMVCApp
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			// filters.Add(new TokenFilter());
		}
	}
}
