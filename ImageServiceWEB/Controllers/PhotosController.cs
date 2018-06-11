/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex3
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageServiceWEB.Models;
using ImageServiceWEB.Models.Instances;

namespace ImageServiceWEB.Controllers
{
    public class PhotosController : Controller
    {
        public List<Photo> Images { get; set; }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            Images = new List<Photo>();
            PhotoModel model = new PhotoModel();
            List<string> imagePaths = model.GetListOfImages();
            foreach (string path in imagePaths)
            {
                Images.Add(new Photo(path));
            }
            return View(Images);
        }
    }
}