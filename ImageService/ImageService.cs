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

        void Logging_MessageRecieved(object sender, MessageRecievedEventArgs e)
        {
            eventLog.WriteEntry(e.Message, (EventLogEntryType) e.Status);
        }

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

            m_imageServer = new ImageServer(logging);
            m_imageServer.StartServer();

            // Update the service state to Running.  
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            eventLog.WriteEntry("Running");
        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("Stopping");
            // Update the service state to Stopping.  
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        protected override void OnContinue()
        {
            eventLog.WriteEntry("Continuing");
        }
    }
}
