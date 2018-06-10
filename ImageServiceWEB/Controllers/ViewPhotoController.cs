using ImageServiceWEB.Models.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWEB.Controllers
{
    public class ViewPhotoController : Controller
    {
        public ViewPhotoController() { }
        public ActionResult Index(Photo photo)
        {
            ViewBag.ThumbnailPath = photo.ThumbnailPath;
            ViewBag.Year = photo.Year;
            ViewBag.Month = photo.Month;
            ViewBag.Name = photo.Name;
            ViewBag.Path = photo.Path;
            return View();
        }
    }
}