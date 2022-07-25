using SignUpLoginLogoutApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace SignUpLoginLogoutApplication.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {
            using (var Context = new Entities())
            {
                bool IsValid = Context.Users.Any(x => x.UserName == model.UserName && x.password == model.password);
                if (IsValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "employees");
                }
                ModelState.AddModelError("", "Invalid username and password");
                return View();
            }
           
        }

        // GET: Account
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User model)
        {
            using (var Context = new Entities())
            {
                Context.Users.Add(model);
                Context.SaveChanges();
            }
            return RedirectToAction("login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}