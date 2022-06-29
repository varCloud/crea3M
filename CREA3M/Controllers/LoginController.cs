using System.Web.Mvc;
using CREA3M.DAO;
using CREA3M.Models;
using System.Net.Http;
using CREA3M.Helpers;
using RestSharp;

namespace CREA3M.Controllers
{
    public class LoginController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        // GET: Login
        public ActionResult Index()
        {
            ViewBag.sucursales = sucursales.buildList();
            return View();
        }        

        public ActionResult Forbidden()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(LoginModel Credentials)
        {

            //var client = new RestClient("https://www.google.com/recaptcha/api/siteverify");
            //client.Timeout = -1;
            //var request = new RestRequest(Method.POST);
            //request.AlwaysMultipartFormData = true;
            //request.AddParameter("secret", "6Lf3xqsZAAAAAL-D_42KGwtt8pHAeafTZ1GOpycF");
            //request.AddParameter("response", Credentials.Token);
            //IRestResponse<CaptchaModel> response = client.Execute<CaptchaModel>(request);

            //if (!response.Data.success)
            //{
            //    return Json(new Response<LoginModel>
            //    {
            //        msg = "Captcha invalido",
            //        status = "captcha",
            //        alertType = "",
            //    }, JsonRequestBehavior.AllowGet);
            //}

            Response<LoginModel> result = new LoginDAO().login(Credentials);
            if (result.status.Equals("success"))
            {
                Session["status"] = "valid";
                Session["username"] = result.model.NombreCompleto;
                Session["defaultDB"] = Credentials.defaultDB;
                Session["LoginModel"] = result.model;
                Session["admin"] = result.model.idUsuario == 12;
                Session.Timeout = 30;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}