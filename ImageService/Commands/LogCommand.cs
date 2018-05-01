/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex2
 */
using ImageService.Infrastructure;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public class LogCommand : ICommand
    {
        private IImageServiceModal m_modal;

        /// <summary>
        /// Constructor for LogCommand class
        /// </summary>
        /// <param name="modal">an Image Service Modal instance</param>
        public LogCommand(IImageServiceModal modal)
        {
            //Storing the modal
            m_modal = modal;
        }

        public string Execute(string[] args, out bool result)
        {
            //TODO: fiil
            result = true;
            return "";
        }
    }
}
