using DataAccesLayer;
using DataAccesLayer.Repositorys;
using Newtonsoft.Json;
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

        public HomeController(IUserRepository userRep)
        {
            _UserRep = userRep;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            bool varmi=_UserRep.Filter(model.UserName, model.Passsword);
            if (varmi == true)
            {
                Session["UserName"] = model.UserName;
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Hata", "Username or Password is wrong");
                return View(model);
            }
            
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
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

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}