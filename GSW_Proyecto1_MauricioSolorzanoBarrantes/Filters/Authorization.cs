using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GSW_Proyecto1_MauricioSolorzanoBarrantes.Filters
{
    //Clase personalizada para la autorización de usuarios
    //Hereda de la clase AuthorizeAttribute para implementar la lógica de autorización
    //Se utiliza para proteger las acciones de los controladores y permitir el acceso solo a usuarios autenticados
    public class Authorization : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Session != null &&
                   httpContext.Session["Autenticado"] != null &&
                   (bool)httpContext.Session["Autenticado"];
        }

        protected override void HandleUnauthorizedRequest(
            AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Account" },
                    { "action", "Login" }
                });
        }
    }
}