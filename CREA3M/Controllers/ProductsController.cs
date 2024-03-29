﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CREA3M.Models;
using System.Web.Mvc;
using CREA3M.DAO;
using Newtonsoft.Json;
using System.IO;
using CREA3M.Filters;
using System.Text;
using System.Configuration;

namespace CREA3M.Controllers
{
    [SessionTimeout]
    public class ProductsController : Controller
    {
        ProductDAO productDAO;
        // GET: Products
        public ActionResult Index()
        {
            
            ViewBag.username = Session["username"];
            string selectedDB = "sucursal" + Session["defaultDB"];
            ResponseList<Marca> marcas = new ProductDAO().getMarcas(selectedDB);
           //marcas.model.Insert(0, new Marca() { marcaEcommerce = "Selecciona" });
            ViewBag.marcas = marcas.model;
            return View();
        }

        [HttpPost]
        public ActionResult _products(int idMarca, int idCategoria , int idSubcategoria)
        {
            ResponseList<Product> response = new ProductDAO().getProductos( idMarca, idCategoria , idSubcategoria);
            ViewBag.productos = response.model;
            return PartialView();
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
            ResponseList<Responce> response = productDAO.deleteImgProduct(idProduct, pathImg, selectedDB);
            if (response.status == "200")
                deleteImageServer(pathImg);
            return Json(response);

        }

        private void deleteImageServer(string path)
        {
            try
            {
                string pathImage = Server.MapPath("~" + path);
                System.IO.File.Delete(pathImage);
            }
            catch (Exception ex)
            {}
        }

        [HttpPost]
        public ActionResult SaveUploadedFile()
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            string path = "";
            string urlImagen = "";
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
                        var originalDirectory = new DirectoryInfo(string.Format("{0}ImgProductos/", Server.MapPath(@"\")));

                        string pathString = System.IO.Path.Combine(originalDirectory.ToString(), idProduct);

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);
                        urlImagen = "/ImgProductos/"+ idProduct+"/"+ fName;
                    }
                }
                string selectedDB = "sucursal" + Session["defaultDB"];
                return Json(productDAO.insertImg(urlImagen, selectedDB, idProduct));
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
               
                return Json(productDAO.getProduct(idProductoEcommerce), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult getCatEcommerce(int idMarca)
        {

            try
            {
                productDAO = new ProductDAO();
                string selectedDB = "sucursal" + Session["defaultDB"];
                return Json(productDAO.getCatEcommerce(selectedDB, idMarca), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult obtenerCategoriasXMarca(int idMarca)
        {

            try
            {
                productDAO = new ProductDAO();
                string selectedDB = "sucursal" + Session["defaultDB"];
                return Json(productDAO.obtenerCategoriasXMarca(idMarca), JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public ActionResult EditarProducto(DetalleProducto detalleProducto)
        {
            try
            {
                productDAO = new ProductDAO();
               
                return Json(productDAO.updateProduct( detalleProducto), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult GeneraCSVProductos()
        {
            try
            {
                String urlOut = GeneraCSVProductosGet();
                return Json(urlOut, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string getPath()
        {
            try
            {
                string ruta = System.Web.HttpContext.Current.Server.MapPath("~" + "/ReportesProductos");
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);                
                return ruta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String GeneraCSVProductosGet()
        {
            try
            {
                ProductDAO productDAO = new ProductDAO();
                ResponseList<Product> response = productDAO.getReporteProductos();
                String urlOut = "";

                if (response.status == "200")
                {
                    String fileName = "productos.csv";
                    String path = getPath() + "/" + fileName;
                    using (StreamWriter writer = new StreamWriter(new FileStream(path,
                        FileMode.Create, FileAccess.Write)))
                    {
                        writer.WriteLine("sep=;");
                        writer.WriteLine("Manufacturer partnumber;Brandname;price;product URL;stock;Productname;EAN/ GTIN;category;Optional: Image URL");
                        foreach (Product p in response.model)
                        {
                            List<string> row = new List<string>();
                            row.Add(p.SKU.ToString());
                            row.Add(p.marca.ToString());
                            row.Add(p.PrecioDeVenta.ToString());
                            row.Add(p.productoUrl.ToString());
                            row.Add(p.cantidad.ToString());
                            row.Add(p.Producto.ToString());
                            row.Add(p.codigoBarras.ToString());
                            row.Add(p.CategoriaEcommerce.ToString());
                            row.Add(p.imagenUrl.ToString());
                            writer.WriteLine(string.Join(";", row.ToArray()));
                        }

                    }

                    urlOut = ConfigurationManager.AppSettings["server"].ToString() + "ReportesProductos/" + fileName;
                }

                return urlOut;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}