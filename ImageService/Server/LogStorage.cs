/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex4
 */
using ImageService.Communication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Logging.Modal;

namespace ImageService.Server
{
    public class LogStorage
    {
        #region Singleton_Members
        private static volatile LogStorage instance;
        private static object mutex = new object();
        #endregion

        #region Properties
        public List<LogMessage> StoredLogs = new List<LogMessage>();
        #endregion

        private LogStorage() { }

        //Returning an instance of LogStorage
        public static LogStorage Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (mutex)
                    {
                        if (instance == null)
                        {
                            instance = new LogStorage();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// The function adds a log to the stored logs
        /// </summary>
        /// <param name="sender">The sender of the command</param>
        /// <param name="e">The arguments that are given with the command</param>
        public void AddLog(object sender, MessageRecievedEventArgs e)
        {
            LogMessage newLog = new LogMessage
            {
                Type = e.Status,
                Message = e.Message
            };
            StoredLogs.Add(newLog);
        }
    }
}
