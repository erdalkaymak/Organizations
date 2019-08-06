using DataAccesLayer;
using DataAccesLayer.Repositorys;
using OrganizationMvc.Controllers;
using System.Web.Mvc;
using System.Web.Routing;

namespace OrganizationMvc.CustomContoller
{
    public class MyControllerFactory : DefaultControllerFactory
    {
        static ImageRepository repImage;
        static OrganizationRepository repOrg;
        static OrgUserRepository repOrgUser;
        static OrgImageRepository repOrgImage;
        static IUserRepository userRep;
        static MyOrganizationEntities db;
        static CommentRepository repComment;
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (db == null)
            {
                db = new MyOrganizationEntities();
            }
            if (userRep == null)
                userRep = new UserRepository(db);
            if (repImage == null)
                repImage = new ImageRepository(db);
            if (repOrg == null)
                repOrg = new OrganizationRepository(db);
            if (repOrgUser == null)
                repOrgUser = new OrgUserRepository(db);
            if (repOrgImage == null)
                repOrgImage = new OrgImageRepository(db);
            if (repComment == null)
                repComment = new CommentRepository(db);


            if (controllerName == "Home")
            {
                IController cnt1 = new HomeController(userRep);
                return cnt1;
            }

            if (controllerName == "OrganizationFinal")
            {
                IController cnt1 = new OrganizationFinalController(userRep, repImage, repOrg, repOrgUser, repOrgImage, repComment);
                return cnt1;
            }

            return base.CreateController(requestContext, controllerName);

        }
    }
}