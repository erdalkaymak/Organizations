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
    public class HomeController : Controller
    {
        IUserRepository _UserRep;
        MyOrganizationEntities _db;
        public HomeController(IUserRepository userRep)
        {
            _UserRep = userRep;
        }

        //public HomeController()
        //{
        //    _db = new MyOrganizationEntities();
        //    _UserRep = new UserRepository(_db);
        //}

        [LogExceptionFilter]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "OrganizationFinal");
        }

        [LogExceptionFilter]
        public ActionResult Login()
        {
            return View();
        }

        [LogExceptionFilter]
        public ActionResult Error()
        {
            return View();
        }

        [HttpPost]
        [LogExceptionFilter]
        public ActionResult Login(LoginModel model)
        {
            bool varmi = _UserRep.Filter(model.UserName, model.Passsword);
            var entity = _UserRep.FilerWithUser(model.UserName, model.Passsword);
            if (varmi == true)
            {
                Session["UserName"] = entity.UserName;
                Session["UserId"] = entity.Id;
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Hata", "Username or Password is wrong");
                return View(model);
            }

        }

        [LogExceptionFilter]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [LogExceptionFilter]
        public ActionResult Register(RegisterModel model)
        {
            model.UserType = 0;
            if (ModelState.IsValid)
            {
                User entity = new User();
                entity = JsonConvert.DeserializeObject<User>(JsonConvert.SerializeObject(model));
                _UserRep.Insert(entity);
                return RedirectToAction("Login");
            }
            else
            {
                return View(model);
            }
        }

        [LogExceptionFilter]
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        [LogExceptionFilter]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [LogExceptionFilter]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}