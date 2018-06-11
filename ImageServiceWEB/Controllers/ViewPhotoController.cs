/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex3
 */
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
        /// <summary>
        /// Initializes a new instance of the ViewPhotoController class.
        /// </summary>
        public ViewPhotoController() { }

        /// <summary>
        /// Indexes the specified photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns>ActionResult.</returns>
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