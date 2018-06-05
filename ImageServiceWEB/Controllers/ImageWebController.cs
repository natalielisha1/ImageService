using ImageServiceWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWEB.Controllers
{
    public class ImageWebController : Controller
    {
        private static StudentDetailsDbContext db = new StudentDetailsDbContext();
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }
    }
}