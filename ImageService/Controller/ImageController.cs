using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Commands;
using ImageService.Modal;

namespace ImageService.Controller
{
    public class ImageController : IImageController
    {
        //The modal object
        private IImageServiceModal m_modal;

        private Dictionary<int, ICommand> commands;

        public string ExecuteCommand(int commandID, string[] args, out bool result)
        {
            //TODO: Fill
            throw new NotImplementedException();
        }
    }
}
