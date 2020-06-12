using CREA3M.DAO;
using CREA3M.Models;
using System;
using System.Web.Mvc;

namespace CREA3M.Controllers
{
    public class SalesController : Controller
    {
        // GET: Ventas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Filtered(String initDate, String endDate)
        {
            DateTime date = DateTime.Today;

            if (initDate == null)
            {
                initDate = date.ToString("yyyy-MM-dd");
            }

            if (endDate == null)
            {
                endDate = date.ToString("yyyy-MM") + "-01";
            }
            //initDate = "2020-01-01";
            //endDate = "2020-01-31";
            ResponseList<SaleModel> response = new SalesDAO().getVentas(initDate, endDate);
            return PartialView("Filtered", response);
        }

        //[HttpPost]
        //public ActionResult Filtered()
        //{
        //    DateTime date = DateTime.Today;
        //    String initDate = date.ToString("yyyy-MM-dd");
        //    String endDate = date.ToString("yyyy-MM") + "-01";

        //    String date_init = "2020-01-01";
        //    String date_end = "2020-01-31";
        //    ResponseList<SaleModel> response = new SalesDAO().getVentas(initDate, endDate);
        //    return PartialView("_Sales", response);
        //}
    }
}