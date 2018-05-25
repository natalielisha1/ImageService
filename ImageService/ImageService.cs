/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex1
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using ImageService.Controller;
using ImageService.Modal;
using ImageService.Server;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Infrastructure.Enums;

namespace ImageService
{
    public enum ServiceState
    {
        SERVICE_STOPPED = 0x00000001,
        SERVICE_START_PENDING = 0x00000002,
        SERVICE_STOP_PENDING = 0x00000003,
        SERVICE_RUNNING = 0x00000004,
        SERVICE_CONTINUE_PENDING = 0x00000005,
        SERVICE_PAUSE_PENDING = 0x00000006,
        SERVICE_PAUSED = 0x00000007,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ServiceStatus
    {
        public int dwServiceType;
        public ServiceState dwCurrentState;
        public int dwControlsAccepted;
        public int dwWin32ExitCode;
        public int dwServiceSpecificExitCode;
        public int dwCheckPoint;
        public int dwWaitHint;
    };

    public partial class ImageService : ServiceBase
    {
        //The image server
        private ImageServer m_imageServer;
        private ILoggingService logging;

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        /// <summary>
        /// Constructor for ImageService, initializing image service components
        /// </summary>
        public ImageService()
        {
            InitializeComponent();
            eventLog = new System.Diagnostics.EventLog();

            string sourceName = System.Configuration.ConfigurationManager.AppSettings["SourceName"];
            string logName = System.Configuration.ConfigurationManager.AppSettings["LogName"];
            
            if (!System.Diagnostics.EventLog.SourceExists(sourceName))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    sourceName, logName);
            }
            eventLog.Source = sourceName;
            eventLog.Log = logName;
        }

        /// <summary>
        /// The Function is passing the given message using the event log
        /// </summary>
        /// <param name="sender">The sender of the message</param>
        /// <param name="e">The arguments of the message</param>
        void Logging_MessageRecieved(object sender, MessageRecievedEventArgs e)
        {
            eventLog.WriteEntry(e.Message, (EventLogEntryType) e.Status);
        }

        /// <summary>
        /// The Function starts the service
        /// </summary>
        /// <param name="args">The arguments that are given with the request of starting the service</param>
        protected override void OnStart(string[] args)
        {
            eventLog.WriteEntry("Start Pending");
            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            logging = new LoggingService();
            logging.MessageRecieved += Logging_MessageRecieved;
            eventLog.WriteEntry("LoggingService created.", (EventLogEntryType) LogMessageTypeEnum.INFO);

            m_imageServer = new ImageServer(logging, 5432);
            eventLog.WriteEntry("ImageServer created.", (EventLogEntryType) LogMessageTypeEnum.INFO);
            m_imageServer.StartServer();
            eventLog.WriteEntry("ImageServer started.", (EventLogEntryType) LogMessageTypeEnum.INFO);

            // Update the service state to Running.  
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            eventLog.WriteEntry("Running");
        }

        /// <summary>
        /// The Function stops the service
        /// </summary>
        protected override void OnStop()
        {
            eventLog.WriteEntry("Stopping");
            // Update the service state to Stopping.
            m_imageServer.SendCommand(); //closing the server
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        /// <summary>
        /// The Function sends a log saying that the service is continuing
        /// </summary>
        protected override void OnContinue()
        {
            eventLog.WriteEntry("Continuing");
        }
    }
}
