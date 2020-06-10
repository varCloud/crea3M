using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CREA3M.DAO;
using CREA3M.Models;
using System.Web.Script.Serialization;
using System.Net.Http;

namespace CREA3M.Controllers
{
    public class LoginController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(LoginModel Credentials)
        {
            Response<LoginModel> result = new LoginDAO().login(Credentials);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> CaptchaAsync(String captcha)
        {
            var values = new Dictionary<string, string>
                {
                    { "secret", "" },
                    { "reponse", captcha }
                };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);

            var responseString = await response.Content.ReadAsStringAsync();
            return Json(responseString, JsonRequestBehavior.AllowGet);
        }
    }
}