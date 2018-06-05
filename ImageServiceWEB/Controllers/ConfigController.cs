using ImageServiceWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWEB.Controllers
{
    public class ConfigController : Controller
    {
        private static ConfigModel settings = new ConfigModel();

        public ActionResult Index()
        {
            settings.UpdateConfig();
            ViewBag.Handlers = settings.Handlers;
            ViewBag.OutputDir = settings.OutputDir;
            ViewBag.SourceName = settings.SourceName;
            ViewBag.LogName = settings.LogName;
            ViewBag.ThumbSize = settings.ThumbSize;
            return View(settings.Handlers);
        }
    }
}