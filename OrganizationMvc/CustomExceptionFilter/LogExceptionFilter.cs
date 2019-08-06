using DataAccesLayer;
using DataAccesLayer.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizationMvc.CustomExceptionFilter
{
    public class LogExceptionFilter : FilterAttribute,IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            MyOrganizationEntities db = new MyOrganizationEntities();
            ExceptionRepository rep = new ExceptionRepository(db);
            DataAccesLayer.Exception entity = new DataAccesLayer.Exception();
            entity.LogTime = DateTime.Now;
            entity.Message = filterContext.Exception.Message;
            entity.StackTrace = filterContext.Exception.StackTrace;
            entity.ControllerName = filterContext.RouteData.Values["controller"].ToString();
            entity.ActionName = filterContext.RouteData.Values["action"].ToString();

            rep.Insert(entity);
            filterContext.ExceptionHandled = true;
            filterContext.RequestContext.HttpContext.Response.Redirect("/Home/Error");

        }
    }
}