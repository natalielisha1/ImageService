﻿using ImageService.Infrastructure;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public class NewFileCommand : ICommand
    {
        private IImageServiceModal m_modal;

        public NewFileCommand(IImageServiceModal modal)
        {
            //Storing the modal
            m_modal = modal;
        }

        public string Execute(string[] args, out bool result)
        {
            // The String Will Return the New Path if result = true, and will return the error message
            //TODO: Fill
            throw new NotImplementedException();
        }
    }
}
