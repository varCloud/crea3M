﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CREA3M.Helpers
{
    public class validation
    {
        public Boolean validateSession(HttpSessionStateBase session)
        {
            return session["status"] != null;
        }
    }
}