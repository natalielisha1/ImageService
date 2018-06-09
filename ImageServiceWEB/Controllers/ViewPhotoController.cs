using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWEB.Controllers
{
    public class ViewPhotoController : Controller
    {
        public ActionResult Index(Photo photo)
        {
            ViewBag.Photo = photo;
            ViewBag.Year = photo.Year;
            ViewBag.Month = photo.Month;
            ViewBag.Name = photo.Name;
            ViewBag.Path = @"App_Data\Output\" + photo.Year + @"\" + photo.Month + @"\" + photo.Name;
            return View();
        }
    }
}