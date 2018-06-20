/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex4
 */
using ImageService.Communication.Model;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public class GetConfigCommand : ICommand
    {
        #region Members
        private IImageServiceModal m_modal;
        private HandlerManager m_handlerManager;
        #endregion

        /// <summary>
        /// Constructor for GetConfigCommand class
        /// </summary>
        /// <param name="modal">an Image Service Modal instance</param>
        public GetConfigCommand(IImageServiceModal modal)
        {
            //Storing the modal
            m_modal = modal;
            m_handlerManager = HandlerManager.Instance;
        }

        public string Execute(string[] args, out bool result)
        {
            result = true;
            CommandMessage msg = new CommandMessage
            {
                Status = true,
                Type = CommandEnum.ConfigMessage,
                Message = @"Current config settings",

                OutputDir = System.Configuration.ConfigurationManager.AppSettings["OutputDir"],
                LogSource = System.Configuration.ConfigurationManager.AppSettings["SourceName"],
                LogName = System.Configuration.ConfigurationManager.AppSettings["LogName"],
                ThumbSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ThumbnailSize"]),
                Handlers = m_handlerManager.GetHandlers()
            };
            return msg.ToJSONString();
        }
    }
}
