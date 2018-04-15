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

namespace ImageService.Modal.Event
{
    public class CommandRecievedEventArgs : EventArgs
    {
        //The command ID
        public int CommandID { get; set; }

        public string[] Args { get; set; }

        //The request's directory
        public string RequestDirPath { get; set; }

        /// <summary>
        /// Constructor for CommandRecievedEventArgs class
        /// </summary>
        /// <param name="id">The id of the command</param>
        /// <param name="args">The arguments that came with the command</param>
        /// <param name="path">The Path of the requested directory</param>
        public CommandRecievedEventArgs(int id, string[] args, string path)
        {
            CommandID = id;
            Args = args;
            RequestDirPath = path;
        }
    }
}
