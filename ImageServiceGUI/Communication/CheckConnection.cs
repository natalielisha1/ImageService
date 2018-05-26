using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.Communication
{
    public class CheckConnection
    {
        private CommunicationSingleton m_comm;

        public bool Connected { get { return m_comm.Connected; } }

        public CheckConnection()
        {
            m_comm = CommunicationSingleton.Instance;
        }
    }
}
