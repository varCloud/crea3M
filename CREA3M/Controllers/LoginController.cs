using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CREA3M.Models;

namespace CREA3M.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel Login)
        {
            if (Login.username.Equals("admin") && Login.password.Equals("admin"))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.message = "Credenciales invalidas";
            }
            return View();
        }
    }
}