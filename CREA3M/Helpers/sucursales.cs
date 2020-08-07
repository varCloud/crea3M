using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CREA3M.Helpers
{
    public class sucursales
    {
        public List<SelectListItem> buildList(String selectedDB)
        {
            return new List<SelectListItem>()
            {
                new SelectListItem{ Text = "Morelia", Value = "MOR", Selected = selectedDB == "MOR"},
                new SelectListItem{ Text = "Guadalajara" , Value = "GDL", Selected = selectedDB == "GDL"},
                new SelectListItem{ Text = "Queretaro" , Value = "QRO", Selected = selectedDB == "QRO"}
            };
        }

        public List<SelectListItem> buildList()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem{ Text = "Morelia", Value = "MOR"},
                new SelectListItem{ Text = "Guadalajara" , Value = "GDL"},
                new SelectListItem{ Text = "Queretaro" , Value = "QRO"}
            };
        }
    }
}