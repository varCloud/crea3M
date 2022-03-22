﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CREA3M.Models
{
    public class ResponseList<T> where T : class
    {

        public String status { get; set; }
        public String msg { get; set; }
        public String alertType { get; set; }

        public List<T> model { get; set; }
    }

    public class ResponseGeneral<T> where T : class
    {
        public int estatus { get; set; }
        public string mensaje { get; set; }
        public T modelo { get; set; }
    }
}