using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CREA3M.Models
{
    public class CaptchaModel
    {
        public Boolean success { get; set; }
        public String challenge_ts { get; set; }
        public String hostname { get; set; }
        public Double score { get; set; }
    }
}