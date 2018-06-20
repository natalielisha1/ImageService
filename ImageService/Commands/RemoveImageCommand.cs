/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex4
 */
using ImageService.Communication.Model;
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
    public class RemoveImageCommand : ICommand
    {
        private IImageServiceModal m_modal;

        /// <summary>
        /// Constructor for RemoveImageCommand class
        /// </summary>
        /// <param name="modal">an Image Service Modal instance</param>
        public RemoveImageCommand(IImageServiceModal modal)
        {
            //Storing the modal
            m_modal = modal;
        }

        public string Execute(string[] args, out bool result)
        {
            CommandMessage msg;
            if (args.Length < 3)
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
            string msgStr = m_modal.RemoveImage(args[0], args[1], args[2], out result);
            msg = new CommandMessage
            {
                Status = result,
                Type = CommandEnum.OK,
                Message = msgStr
            };
            return msg.ToJSONString();
        }
    }
}
