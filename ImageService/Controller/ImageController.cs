﻿/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex4
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using ImageService.Communication.Model;

namespace ImageService.Controller
{
    public class ImageController : IImageController
    {
        //The modal object
        private IImageServiceModal m_modal;
        private Dictionary<int, ICommand> commands;

        /// <summary>
        /// Constructor for ImageController class
        /// </summary>
        /// <param name="modal">The modal of the service</param>
        public ImageController(IImageServiceModal modal)
        {
            //Storing the modal of the system
            m_modal = modal;
            commands = new Dictionary<int, ICommand>()
            {
                { (int) CommandEnum.NewFileCommand, new NewFileCommand(m_modal)},
                { (int) CommandEnum.GetConfigCommand, new GetConfigCommand(m_modal)},
                { (int) CommandEnum.LogRequest, new LogCommand(m_modal)},
                { (int) CommandEnum.RemoveHandler, new RemoveHandlerCommand(m_modal) },
                { (int) CommandEnum.RemoveImage, new RemoveImageCommand(m_modal) },
                { (int) CommandEnum.NewImageFileCommand, new NewImageFileCommand(m_modal) }
            };
        }

        public string ExecuteCommand(int commandID, string[] args, out bool result)
        {
            //check if the command's ID exists, and if exists execute it
            //otherwise, send an error message
            if (!commands.ContainsKey(commandID))
            {
                result = false;
                CommandMessage msg = new CommandMessage
                {
                    Status = false,
                    Type = CommandEnum.OK,
                    Message = @"Command not found"
                };
                return msg.ToJSONString();
            }
            ICommand command = commands[commandID];
            return command.Execute(args, out result);
        }
    }
}
