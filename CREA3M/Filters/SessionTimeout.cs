using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CREA3M.Filters
{
    public class SessionTimeout : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            /************************************************************************
             SI LA SESION ES DIFERENTE DE NULL PASA A EJECUTAR LA SIGUIENTE ACCION
             SI LA SESION ES IGUAL A NULL ENTRA A LA SIGUIENTE FUNCION LO CUAL LO REDIRIGE
             AL LOGIN
            ****************************************************************************/
            var rd = httpContext.Request.RequestContext.RouteData;
            string currentAction = rd.GetRequiredString("action");
            string currentController = rd.GetRequiredString("controller");
            if (currentAction== "GeneraCSVProductosGet" && currentController== "Products")
                return true;
            else
                return httpContext.Session["username"] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
            {
                { "action", "Index" },
                { "controller", "Login" }
            });
        }
    }
}