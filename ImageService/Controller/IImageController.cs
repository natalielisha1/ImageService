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

namespace ImageService.Controller
{
    public interface IImageController
    {
        /// <summary>
        /// The Function Executes the command request
        /// </summary>
        /// <param name="commandID">The ID number of the command we want to execute</param>
        /// <param name="args">The arguments related to the command</param>
        /// <param name="result">The boolean result of the action</param>
        /// <returns>Indication if the Addition Was Successful</returns>
        string ExecuteCommand(int commandID, string[] args, out bool result);
    }
}
