using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;
using System.Web.Mvc;

namespace Rupor.Public.Helpers
{
    public static class CurrentUrlHelper
    {
        public static (string action, string controller) GetCurrentUrlComponents(this HtmlHelper helper, HttpRequest request)
        {
            var controller = request.RequestContext.RouteData.Values["controller"].ToString();
            var action = request.RequestContext.RouteData.Values["action"].ToString();
            return (action, controller);
        }
    }
}