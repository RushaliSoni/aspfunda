﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspfunda.Controllers
{

    public class DepartmentController : Controller
    {
        public string List()
        {
            return "List() of department controller";
        }
        public string Details()
        {
            return "Details() of department controller";
        }
    }
}
