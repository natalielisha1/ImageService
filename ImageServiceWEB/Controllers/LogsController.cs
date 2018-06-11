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
        public List<Log> logs { get; set; }

        public ActionResult Index()
        {
            logs = new List<Log>();
            LogModel model = new LogModel();
            foreach (LogMessage log in model.Logs)
            {
                logs.Add(new Log(log));
            }
            return View(logs);
        }

        public ActionResult Search(string type)
        {
            List<Log> newLogs = new List<Log>();
            foreach (Log log in logs)
            {
                if (log.Type == type)
                {
                    newLogs.Add(log);
                }
            }
            return View(newLogs);
        }
    }
}