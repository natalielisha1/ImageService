/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex4
 */
using ImageService.Communication.Model;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public class RemoveHandlerCommand : ICommand
    {
        #region Members
        private IImageServiceModal m_modal;
        private HandlerManager m_handlerManager;
        #endregion

        /// <summary>
        /// Constructor for GetConfigCommand class
        /// </summary>
        /// <param name="modal">an Image Service Modal instance</param>
        public RemoveHandlerCommand(IImageServiceModal modal)
        {
            //Storing the modal
            m_modal = modal;
            m_handlerManager = HandlerManager.Instance;
        }

        public string Execute(string[] args, out bool result)
        {
            CommandMessage msg;
            if (args.Length < 1)
            {
                result = false;
                msg = new CommandMessage
                {
                    Status = false,
                    Type = CommandEnum.OK,
                    Message = @"Ilegal arguments for command"
                };
                return msg.ToJSONString();
            }
            result = m_handlerManager.RemoveHandler(args[0]);
            msg = new CommandMessage
            {
                Status = result,
                Type = CommandEnum.OK,
                Message = @"Sent remove handler request"
            };
            return msg.ToJSONString();
        }
    }
}
