/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex4
 */
using ImageServiceWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWEB.Controllers
{
    /// <summary>
    /// Class ConfigController.
    /// </summary>
    public class ConfigController : Controller
    {
        private static ConfigModel settings = new ConfigModel();

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
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