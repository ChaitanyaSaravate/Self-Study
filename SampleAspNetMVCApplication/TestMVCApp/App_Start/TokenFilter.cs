using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;

namespace TestMVCApp.App_Start
{
	public class TokenFilter : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);

			if (string.IsNullOrWhiteSpace(filterContext.RequestContext.HttpContext.Request.QueryString["token"]))
			{
				throw new AuthenticationException("No token available.");
			}
		}
	}
}