﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWEB.Controllers
{
    public class ImageWebController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}