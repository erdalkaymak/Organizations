using DataAccesLayer.Repositorys;
using OrganizationMvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OrganizationMvc.CustomContoller
{
    public class MyControllerFactory: DefaultControllerFactory
    {
        static  UserRepository userRep;
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (userRep == null)
            {
                userRep = new UserRepository();
            }
            if (controllerName == "Home")
            {
                return new HomeController(userRep);
            }
            return base.CreateController(requestContext, controllerName);
        }
    }
}