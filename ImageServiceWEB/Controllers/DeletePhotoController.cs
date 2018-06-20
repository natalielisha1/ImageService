/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex4
 */
using ImageService.Infrastructure.Enums;
using ImageServiceWEB.Communication;
using ImageServiceWEB.Models.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace ImageServiceWEB.Controllers
{
    public class DeletePhotoController : Controller
    {
        private static Communicator comm = Communicator.Instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeletePhotoController"/> class.
        /// </summary>
        public DeletePhotoController() { }


        /// <summary>
        /// Indexes the specified photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Index(Photo photo)
        {
            ViewBag.Name = photo.Name;
            ViewBag.ThumbnailPath = photo.ThumbnailPath;
            ViewBag.Photo = photo;
            ViewBag.Year = photo.Year;
            ViewBag.Month = photo.Month;
            return View();
        }

        /// <summary>
        /// Deletes the given photo.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="name">The name.</param>
        /// <returns>an indicator for wether of not the function has succeed</returns>
        [HttpPost]
        public bool DeletePhoto(string year, string month, string name)
        {
            comm.SendCommandToServer(CommandEnum.RemoveImage, new string[] { year, month, name });
            return true;
        }
    }
}