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
                new SelectListItem{ Text = "Morelia", Value = "Morelia", Selected = selectedDB == "Morelia"},
                new SelectListItem{ Text = "Guadalajara" , Value = "Guadalajara", Selected = selectedDB == "Guadalajara"}
            };
        }

        public List<SelectListItem> buildList()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem{ Text = "Morelia", Value = "Morelia"},
                new SelectListItem{ Text = "Guadalajara" , Value = "Guadalajara"}
            };
        }
    }
}