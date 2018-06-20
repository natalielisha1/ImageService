/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex4
 */
using ImageService.Communication.Model;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using ImageService.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public class LogCommand : ICommand
    {
        #region Members
        private IImageServiceModal m_modal;
        private LogStorage m_logStorage;
        #endregion

        /// <summary>
        /// Constructor for LogCommand class
        /// </summary>
        /// <param name="modal">an Image Service Modal instance</param>
        public LogCommand(IImageServiceModal modal)
        {
            //Storing the modal
            m_modal = modal;
            m_logStorage = LogStorage.Instance;
        }

        public string Execute(string[] args, out bool result)
        {
            result = true;
            CommandMessage msg = new CommandMessage
            {
                Status = true,
                Type = CommandEnum.LogAdded,
                Message = @"Current recorded logs",
                LogMessages = m_logStorage.StoredLogs
            };
            return msg.ToJSONString();
        }
    }
}
