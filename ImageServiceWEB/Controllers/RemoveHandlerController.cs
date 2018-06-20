/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex4
 */
using ImageService.Communication.Model;
using ImageService.Infrastructure.Enums;
using ImageServiceWEB.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace ImageServiceWEB.Controllers
{
    public class RemoveHandlerController : Controller
    {
        private static Communicator comm = Communicator.Instance;
        private static object waitForUpdateLock = new object();
        private static List<string> removedHandlers = new List<string>();
        private static bool addedProcessor = false;


        /// <summary>
        /// Indexes the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Index(string handler)
        {
            if (!addedProcessor)
            {
                comm.MessageArrived += ProcessMessage;
                addedProcessor = true;
            }
            ViewBag.Handler = handler;
            ViewBag.SplittedHandler = handler.Split('\\');
            return View();
        }

        /// <summary>
        /// Removes the handler.
        /// </summary>
        /// <param name="splittedHandler">The splitted handler.</param>
        /// <returns>an indicator for wether of not the function has succeed</returns>
        [HttpPost]
        public bool RemoveHandler(string[] splittedHandler)
        {
            string handler = "";
            foreach (string splitted in splittedHandler)
            {
                handler += splitted + "\\";
            }
            handler = handler.Substring(0, handler.Length - 1);
            if (!comm.Connected)
            {
                return false;
            }
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

        /// <summary>
        /// Processes the message.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">instance containing the event data, arguments</param>
        public void ProcessMessage(object sender, CommandMessageEventArgs e)
        {
            //Extract the message from the EventArgs
            CommandMessage msg = e.Message;
            switch (msg.Type)
            {
                //Checking if the message is interesting to RemoveHandler
                case CommandEnum.RemoveHandler:
                    foreach (string handler in msg.Handlers)
                    {
                        removedHandlers.Add(handler);
                    }
                    lock (waitForUpdateLock)
                    {
                        Monitor.PulseAll(waitForUpdateLock);
                    }
                    break;
            }
        }
    }
}