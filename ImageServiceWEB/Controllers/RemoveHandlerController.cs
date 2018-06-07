using ImageService.Communication.Model;
using ImageService.Infrastructure.Enums;
using ImageServiceWEB.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWEB.Controllers
{
    public class RemoveHandlerController : Controller
    {
        private static Communicator comm = Communicator.Instance;
        private static object waitForUpdateLock = new object();
        private static List<string> removedHandlers = new List<string>();

        // GET: RemoveHandler
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index(string handler)
        {
            ViewBag.Handler = handler;
            return View();
        }

        [HttpPost]
        public bool RemoveHandler(string handler)
        {
            if (!comm.Connected)
            {
                return false;
            }
            comm.MessageArrived += delegate (object sender, CommandMessageEventArgs e)
            {
                CommandMessage msg = e.Message;
                switch (msg.Type)
                {
                    case CommandEnum.RemoveHandler:
                        if (msg.Handlers.Contains(handler))
                        {
                            removedHandlers.Add(handler);
                            lock (waitForUpdateLock)
                            {
                                Monitor.PulseAll(waitForUpdateLock);
                            }
                        }
                        break;
                }
            };
            if (removedHandlers.Contains(handler))
            {
                return true;
            }
            comm.SendCommandToServer(CommandEnum.RemoveHandler, new string[] { handler });
            lock (waitForUpdateLock)
            {
                while (!removedHandlers.Contains(handler))
                {
                    Monitor.Wait(waitForUpdateLock);
                }
            }
            return true;
        }
    }
}