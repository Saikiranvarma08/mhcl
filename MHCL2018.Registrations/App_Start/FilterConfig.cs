using System.Web;
using System.Web.Mvc;

namespace MHCL2018.Registrations
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
