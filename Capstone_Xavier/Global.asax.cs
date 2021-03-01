using Capstone_Xavier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Capstone_Xavier
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs events)
        {
            string Username = Session["Username"] as string;
            string Sessroles = Session["Role"] as string;
            string UserID = Session["UserID"] as string;
            UserModel user = Session["User"] as UserModel;
            //string CurrentCharacter = Session["Character"] as string;
            CharacterModel characterModel = Session["CurrentCharacter"] as CharacterModel;
            ClassModel classes = Session["Class"] as ClassModel;
            GameModel game = Session["Game"] as GameModel;

            if (string.IsNullOrEmpty(Username))
            {
                return;
            }

            GenericIdentity identity = new GenericIdentity(Username, "MyType");
            if (Sessroles == null)
            {
                Sessroles = "";
            }else if (Sessroles == "0") {
                Sessroles = "User";
            }else if (Sessroles == "1") {
                Sessroles = "Admin";
            }else if(Sessroles == "3"){
                Sessroles = "GameMaster";
            }

            string[] roles = Sessroles.Split(',');
            GenericPrincipal principal = new GenericPrincipal(identity, roles);
            HttpContext.Current.User = principal;
        }
    }
}
