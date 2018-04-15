using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Modal.Event
{
    public class DirectoryCloseEventArgs : EventArgs
    {

        public string DirectoryPath { get; set; }

        //The message that goes to the logger
        public string Message { get; set; }

        /// <summary>
        /// Constructor for DirectoryCloseEventArgs class
        /// </summary>
        /// <param name="dirPath">The path to the directory we would like to close</param>
        /// <param name="message">The message that will be passed to the logger</param>
        public DirectoryCloseEventArgs(string dirPath, string message)
        {
            //Setting the directory name
            DirectoryPath = dirPath;

            //Storing the message
            Message = message;
        }
    }
}
