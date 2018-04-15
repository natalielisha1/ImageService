/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex1
 */
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

        /// <summary>
        /// The function is handling the given directory 
        /// </summary>
        /// <param name="dirPath">The directory to handle</param>
        void StartHandleDirectory(string dirPath);

        /// <summary>
        /// The function is handling the command according to the
        /// given sender and event arguments
        /// </summary>
        /// <param name="sender">The sender of the command</param>
        /// <param name="e">The arguments that are given with the command</param>
        void OnCommandRecieved(object sender, CommandRecievedEventArgs e);
    }
}
