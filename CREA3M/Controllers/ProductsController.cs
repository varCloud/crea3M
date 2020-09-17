using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CREA3M.Models;
using System.Web.Mvc;
using CREA3M.DAO;
using Newtonsoft.Json;
using System.IO;

namespace CREA3M.Controllers
{
    public class ProductsController : Controller
    {
        ProductDAO productDAO;
        // GET: Products
        public ActionResult Index(int idMarca = 1)
        {
            
            ViewBag.username = Session["username"];
            string selectedDB = "sucursal" + Session["defaultDB"];
            ResponseList<Marca> marcas = new ProductDAO().getMarcas(selectedDB);
            ViewBag.marcas = marcas.model;
            ResponseList<Product> response = new ProductDAO().getProductos(selectedDB, idMarca);
            ViewBag.productos = response.model;
            ViewBag.vistaMarca = marcas.model.Find(x => x.idMarcaEcommerce == idMarca);
            return View();
        }

        [HttpPost]
        public ActionResult registrarProductos(List<Product> productos, int idMarca)
        {
            productDAO = new ProductDAO();
            String xml = Utils.ToXML(productos);
            string selectedDB = "sucursal" + Session["defaultDB"];
            return Json(productDAO.insertProduct(xml, selectedDB, idMarca));

        }

        [HttpPost]
        public ActionResult deleteImgProduct(string idProduct, string pathImg)
        {
            productDAO = new ProductDAO();
            string selectedDB = "sucursal" + Session["defaultDB"];
            return Json(productDAO.deleteImgProduct(idProduct, pathImg, selectedDB));

        }



        [HttpPost]
        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            string path = "";
            string idProduct = "";
            productDAO = new ProductDAO();

            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    idProduct = Request.Form["idProducto"];

                    fName = file.FileName;
                    
                    if (file != null && file.ContentLength > 0)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}ImgProductos", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "1");

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);
                    }
                }
                string selectedDB = "sucursal" + Session["defaultDB"];
                return Json(productDAO.insertImg(path, selectedDB, idProduct));
            }
            catch(Exception ex)
		    {
                throw ex;
            }
        }


        [HttpPost]
        public ActionResult getProduct(string idProductoEcommerce)
        {

            try
            {
                productDAO = new ProductDAO();
                string selectedDB = "sucursal" + Session["defaultDB"];
                return Json(productDAO.getProduct(idProductoEcommerce, selectedDB), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult getProductImg(string idProductoEcommerce)
        {
            try
            {
                productDAO = new ProductDAO();
                string selectedDB = "sucursal" + Session["defaultDB"];
                return Json(productDAO.getProductosImg(idProductoEcommerce, selectedDB), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult cambiarEstadoProductos(int idProducto, bool activo)
        {

            try
            {
                productDAO = new ProductDAO();
                string selectedDB = "sucursal" + Session["defaultDB"];
                return Json(productDAO.updateCambioEstado(idProducto, selectedDB, activo), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult EditarProduct(detalleProducto producto)
        {
            try
            {
                productDAO = new ProductDAO();
                string selectedDB = "sucursal" + Session["defaultDB"];
                return Json(productDAO.updateProduct(selectedDB, producto), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}