using CREA3M.Helpers;
using CREA3M.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CREA3M.Controllers
{
    public class SessionController : Controller
    {
        // GET: Session
        public JsonResult Logout()
        {
            Session.Clear();
            return Json(new Response<LoginModel>
            {
                alertType = "success",
                msg = "success",
                status = "success"
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Validate()
        {
            SimpleResponse response = new SimpleResponse() { status = "invalid" };
            if (new validation().validateSession(Session))
            {
                response.status = "valid";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}