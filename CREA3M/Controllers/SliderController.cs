using CREA3M.DAO;
using CREA3M.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CREA3M.Controllers
{
    public class SliderController : Controller
    {
        SliderDAO sliderDAO = null;
        // GET: Slider
        public ActionResult Slider()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            string path = "";
            string urlImagen = "";
            string idProduct = "";
            SliderDAO sliderDAO = new SliderDAO();
            HttpPostedFileBase file = null;

            try
            {
                foreach (string fileName in Request.Files)
                {
                    file = Request.Files[fileName];
                    idProduct = Request.Form["idProducto"];
                    
                    fName = file.FileName;

                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}ImgSlider/", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), idProduct);

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);
                        urlImagen = "/ImgSlider/" + idProduct + "/" + fName;
                    }
                }
                string selectedDB = "sucursal" + Session["defaultDB"];
                return Json(sliderDAO.insertaImagen( new ImagenSlider() { path = urlImagen , nombre = fName , size = "0"}));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public JsonResult obtenerImagenes(int idImagen)
        {
            try
            {
                sliderDAO = new SliderDAO();
                return Json(sliderDAO.obtenerImagenes(idImagen), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult _obtenerImagenes(int idImagen) {
            try
            {
                sliderDAO = new SliderDAO();
                ViewBag.imagenesSlider = sliderDAO.obtenerImagenes(idImagen).modelo;
                return PartialView();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult EliminarImagen(int idImagen)
        {
            try
            {
                sliderDAO = new SliderDAO();
                return Json(sliderDAO.EliminarImagen(idImagen), JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult DesactivarImagen(int idImagen, Boolean estatus)
        {
            try
            {
                sliderDAO = new SliderDAO();
                return Json(sliderDAO.DesactivarImagen(idImagen , estatus), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}