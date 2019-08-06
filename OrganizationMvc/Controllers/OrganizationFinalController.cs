using DataAccesLayer;
using DataAccesLayer.Repositorys;
using Newtonsoft.Json;
using OrganizationMvc.CustomActionFilter;
using OrganizationMvc.CustomExceptionFilter;
using OrganizationMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrganizationMvc.Controllers
{
    public class OrganizationFinalController : Controller
    {
        ImageRepository _repImage;
        OrganizationRepository _repOrg;
        IUserRepository _repUser;
        OrgUserRepository _repOrgUser;
        OrgImageRepository _repOrgImage;
        CommentRepository _repComment;
        MyOrganizationEntities _db;
        public OrganizationFinalController(IUserRepository repUser, ImageRepository repImage,
         OrganizationRepository repOrg, OrgUserRepository repOrgUser, OrgImageRepository repOrgImage, CommentRepository repComment)
        {
            _repImage = repImage;
            _repOrg = repOrg;
            _repUser = repUser;
            _repOrgUser = repOrgUser;
            _repOrgImage = repOrgImage;
            _repComment = repComment;
        }

        //public OrganizationFinalController()
        //{
        //    _db = new MyOrganizationEntities();
        //    _repImage = new ImageRepository(_db);
        //    _repOrg = new OrganizationRepository(_db);
        //    _repUser = new UserRepository(_db);
        //    _repOrgUser = new OrgUserRepository(_db);
        //    _repOrgImage = new OrgImageRepository(_db);
        //    _repComment = new CommentRepository(_db);
        //}
        [LogExceptionFilter]
        public ActionResult Index()
        {
            var list = _repOrg.GetAll().OrderBy(c=>c.OrganizationDate);
            //List<OrganizationModel> modelList = new List<OrganizationModel>();
            //modelList = JsonConvert.DeserializeObject<List<OrganizationModel>>(JsonConvert.SerializeObject(list));
            return View(list);
        }
       
        [CheckLogin]
        [LogExceptionFilter]
        public ActionResult AddImage()
        {
            ImageModel model=new ImageModel();
     
            return View(); 
        }

        [HttpPost]
        [LogExceptionFilter]
        [CheckLogin]
        public int AddImage(ImageModel model)
        {
            Images entity = new Images();
            entity.imageUrl = Request.Files[0].FileName;
            _repImage.Insert(entity);
            string name = entity.imageUrl+entity.Id + "_" + ".jpg";
            entity.imageUrl = name;
            string filename = Server.MapPath("/Images/" + name);
            var file = Request.Files["file"];
            file.SaveAs(filename);
            _repImage.Update(entity);
            return entity.Id;          
        }
   
       
        public void AddComment(string CommentStr,int id)
        {
            int myUserId;
            if (Session["UserName"] != null)
            {
                myUserId=Convert.ToInt32(Session["UserId"].ToString());
               var myOrg = _repOrg.Find(id);
                var myUser = _repUser.Find(myUserId);
                Comments myComment = new Comments();
                myComment.Organization = myOrg;
                myComment.User = myUser;
                myComment.CommnetStr = CommentStr;
                _repComment.Insert(myComment);
            }

            
        }

        public ActionResult PartialComment(int orgId)
        {
            var listComment=_repComment.getAllById(orgId);
            listComment=listComment.OrderByDescending(c => c.Id).ToList();
            ViewBag.org = orgId;
            return View(listComment);
        }

        [LogExceptionFilter]
        [CheckLogin]
        
        public ActionResult UpdateOrg(int id)
        {
            var org = _repOrg.Find(id);
            return View(org);
        }

        [HttpPost]
        [LogExceptionFilter]
        [CheckLogin]
        public ActionResult UpdateOrg(Organization org)
        {
            var orgentity = _repOrg.Find(org.Id);

            if (Request.Files.Count <= 0)
            {
                ModelState.AddModelError("ImageValidation", "Choose Image");
            }
            if (Request.Files[0].ContentLength <= 0)
            {
                ModelState.AddModelError("ImageValidation", "Choose Image");
            }

            if (!ModelState.IsValid)
                return View(org);
            else
            {
                ViewBag.isChoose = "1";
                Images entity = _repImage.Find(orgentity.MainImageId);
                string name = Request.Files[0].FileName + entity.Id + "_" + ".jpg";
                entity.imageUrl = name;
                string filename = Server.MapPath("/Images/" + name);
                var file = Request.Files[0];
                file.SaveAs(filename);
                _repImage.Update(entity);
                org.MainImageId = entity.Id;
                //org.Organizer = ((User)Session["User"]).Id;
                _repOrg.Update(org);
                return RedirectToAction("Index");
            }
        }

        [CheckLogin]
        [LogExceptionFilter]
        public ActionResult Delete(int id)
        {
         
            var entity=_repOrgImage.Find(id);
            _repOrgImage.Delete(entity);
            return View();
        }
        [CheckLogin]
        [LogExceptionFilter]
        public ActionResult AddOrganization()
        {           
            //ViewBag.images = _repImage.GetAll();
            return View();
        }

        [HttpPost]
        [LogExceptionFilter]
        [CheckLogin]
        public ActionResult AddOrganization(Organization org)
        {
            if (!ModelState.IsValid)
                return View(org);

            else
            {
                if (Request.Files.Count <= 0)
                {
                    ModelState.AddModelError("ImageValidation", "Choose Image");
                }
                if (Request.Files[0].ContentLength <= 0)
                {
                    ModelState.AddModelError("ImageValidation", "Choose Image");
                }
                Images entity = new Images();
                entity.imageUrl = Request.Files[0].FileName;
                _repImage.Insert(entity);
                string name = entity.imageUrl + entity.Id + "_" + ".jpg";
                entity.imageUrl = name;
                string filename = Server.MapPath("/Images/" + name);
                var file = Request.Files[0];
                file.SaveAs(filename);
                _repImage.Update(entity);

                org.MainImageId = entity.Id;
                //org.Organizer = ((User)Session["User"]).Id;

                _repOrg.Insert(org);

                return RedirectToAction("Index");
            }
        }

        [CheckLogin]
        [LogExceptionFilter]
        public ActionResult DeleteOrg(int id)
        {
           var org= _repOrg.Find(id);
            var orgUser = _repOrgUser.GetAll();
            var list = orgUser.Where(c => c.OrgId == id).ToList();
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    _repOrgUser.Delete(item);
                }
            }
            var OrgImage = _repOrgImage.GetAll();
            var listImage = OrgImage.Where(c => c.OrgId == id).ToList();
            if (listImage.Count > 0)
            {
                foreach (var item in listImage)
                {
                    _repOrgImage.Delete(item);
                }
            }
            _repOrg.Delete(org);
            return RedirectToAction("Index");
        }

        [LogExceptionFilter]
        public ActionResult Detail(int id)
        {
            var org = _repOrg.Find(id);
            //o  organizyonda olmayan userları getiriyorum 
            ViewData["All Users"] = _repUser.GetAll().Where(c => !org.OrgUser.Any(x => x.UserId == c.Id)).ToList();
            return View(org);
        }

        [CheckLogin]
        [LogExceptionFilter]
        public ActionResult AddOrgImage()
        {
            return View();
        }

        [CheckLogin]
        [LogExceptionFilter]
        public ActionResult DeleteUsers(int id,int orgId)
        {
            OrgUser entity = _repOrgUser.Find(id);
            _repOrgUser.Delete(entity);
            return RedirectToAction("Detail", new { id = orgId });
        }

        [HttpPost]
        [LogExceptionFilter]
        [CheckLogin]      
        public ActionResult AddMember(int orgId,int selectedUserId)
        {
            OrgUser entitiy = new OrgUser();
            entitiy.OrgId = orgId;
            entitiy.UserId = selectedUserId;
            _repOrgUser.Insert(entitiy);            
            return RedirectToAction("Detail", new { id = orgId });
        }

        //[ChildActionOnly]
        [CheckLogin]
        [LogExceptionFilter]
        public ActionResult ImageListParital()
        {
            var list = _repOrgImage.GetAll();
            return View(list);
        }

    }
}