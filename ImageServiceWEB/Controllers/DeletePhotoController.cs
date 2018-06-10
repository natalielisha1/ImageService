using ImageService.Infrastructure.Enums;
using ImageServiceWEB.Communication;
using ImageServiceWEB.Models.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWEB.Controllers
{
    public class DeletePhotoController : Controller
    {
        private static Communicator comm = Communicator.Instance;

        public DeletePhotoController() { }
        public ActionResult Index(Photo photo)
        {
            ViewBag.Name = photo.Name;
            ViewBag.ThumbnailPath = photo.ThumbnailPath;
            ViewBag.Photo = photo;
            ViewBag.Year = photo.Year;
            ViewBag.Month = photo.Month;
            return View();
        }

        [HttpPost]
        public bool DeletePhoto(string year, string month, string name)
        {
            comm.SendCommandToServer(CommandEnum.RemoveImage, new string[] { year, month, name });
            return true;
        }
    }
}