using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Modal.Event;

namespace ImageService.Controller.Handlers
{
    public class DirectoryHandler : IDirectoryHandler
    {
        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;

        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            //TODO: Fill
            throw new NotImplementedException();
        }

        public void StartHandleDirectory(string dirPath)
        {
            //TODO: Fill
            throw new NotImplementedException();
        }
    }
}
