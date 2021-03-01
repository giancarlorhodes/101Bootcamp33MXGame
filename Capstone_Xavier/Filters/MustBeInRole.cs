
namespace Capstone_Xavier.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class MustBeInRole: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (this.Roles.Split(',').Any(filterContext.HttpContext.User.IsInRole)) {
                base.OnAuthorization(filterContext);
            }else{
                string returnUrl = filterContext.RequestContext.HttpContext.Request.Path.ToString();
                System.Web.Routing.RouteValueDictionary dict = new System.Web.Routing.RouteValueDictionary();
                dict.Add("Controller", "Home");
                dict.Add("Action","Login");
                filterContext.Result = new RedirectToRouteResult(dict);
            }
        }
    }
}