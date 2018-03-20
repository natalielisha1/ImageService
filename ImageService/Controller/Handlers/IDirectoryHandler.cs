using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Modal.Event;

namespace ImageService.Controller.Handlers
{
    public interface IDirectoryHandler
    {
        //The event that notifies that the directory is being closed
        event EventHandler<DirectoryCloseEventArgs> DirectoryClose;

        //The function recieves the directory to handle
        void StartHandleDirectory(string dirPath);

        //The event that will be activated upon a new command
        void OnCommandRecieved(object sender, CommandRecievedEventArgs e);
    }
}
