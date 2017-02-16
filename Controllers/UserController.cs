using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ease.Extract.Web.Models;
using Ease.Data;
using Ease.Model.Extract;
using Ease.Data.Sql;
using System.IO;
using Owin;
using Ease.Util;
using System.Web.Configuration;
using System.Configuration;
using System.Text;
using Ease.Extract.Web.Classes;


namespace Ease.Extract.Web.Controllers
{
    [RoutePrefix("User")]
    public class UserController : Controller
    {
        
        [Route("Index"), HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }



        [Route("Login"), HttpPost]
        public ActionResult Login(UserModel user)
        {
            if (ModelState.IsValid)
            {
                if (IsValid(user.UserName, user.Password))
                {
                    return RedirectToAction("Index", "user");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "user");
        }
        private bool IsValid(string username, string password)
        {
            string hashedPassword = HashUtilWrapper.Hash(password);
            string systemUsername = ConfigurationManager.AppSettings["PFUserName"];
            string systemPassword = ConfigurationManager.AppSettings["PFPassword"];
#if DEBUG
            Console.WriteLine(systemPassword);
            Console.WriteLine(systemUsername);
#endif

            if (string.Equals(hashedPassword, systemPassword, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}