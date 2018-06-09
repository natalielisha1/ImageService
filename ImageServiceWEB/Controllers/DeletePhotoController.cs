using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWEB.Controllers
{
    public class DeletePhotoController : Controller
    {
        public ActionResult Index(Photo photo)
        {
            ViewBag.Title = photo.Name;
            ViewBag.ThumbnailPath = photo.ThumbnailPath;
            ViewBag.Photo = photo;
            return View();
        }

        public ActionResult OnDelete(Photo photo)
        {
            string path;
            path = @"App_Data\Output\" + photo.Year + @"\" + photo.Month + @"\" + photo.Name;
            System.IO.File.Delete(path);
            return View();
        }
    }
}