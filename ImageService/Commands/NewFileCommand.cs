using ImageService.Infrastructure;
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
            //my assumption is that the args parameter conatins only the path in this command's case
            ImageServiceModal imgService = new ImageServiceModal();
            string path = args[0];
            return imgService.AddFile(path, out result);
        }
    }
}
