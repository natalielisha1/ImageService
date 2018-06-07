using ImageServiceWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageServiceWEB.Communication;

namespace ImageServiceWEB.Controllers
{
    public class ImageWebController : Controller
    {
        //private static StudentDetailsDbContext db = new StudentDetailsDbContext();
        private static Communicator comm = Communicator.Instance;

        public ActionResult Index()
        {
            ViewBag.Connected = comm.Connected ? "Connected" : "Not Connected";
            //TODO: Add real number
            ViewBag.ImageAmount = 0;
            List<StudentDetails> students = new List<StudentDetails>();
            using (StudentDetailsDbContext db = new StudentDetailsDbContext())
            {
                students = db.Students.ToList();
            }
            return View(students);
        }
    }
}