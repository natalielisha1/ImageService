using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImageServiceWEB.Models;
using ImageService.Communication.Model;
using ImageServiceWEB.Models.Instances;

namespace ImageServiceWEB.Controllers
{
    public class LogsController : Controller
    {
        public List<Log> Logs { get; set; }

        public ActionResult Index()
        {
            Logs = new List<Log>();
            LogModel model = new LogModel();
            foreach (LogMessage log in model.Logs)
            {
                Logs.Add(new Log(log));
            }
            return View(Logs);
        }
    }
}