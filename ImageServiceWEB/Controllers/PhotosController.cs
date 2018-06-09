using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageServiceWEB.Models;

namespace ImageServiceWEB.Controllers
{
    public class PhotosController : Controller
    {
        public List<Photo> images { get; set; }

        public ActionResult Index()
        {
            PhotoModel model = new PhotoModel();
            List<string> imagePaths = model.GetListOfImages();
            foreach (string path in imagePaths)
            {
                images.Add(new Photo(path));
            }
            return View(images);
        }
    }
}