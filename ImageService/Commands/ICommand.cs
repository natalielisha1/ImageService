/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex1
 */
using ImageService.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public interface ICommand
    {
        /// <summary>
        /// The Function Executes the command
        /// </summary>
        /// <param name="args">The arguments related to the command</param>
        /// <param name="result">The boolean result of the action</param>
        /// <returns>The string will return the new path if result = true, otherwise, will return the error message</returns>
        string Execute(string[] args, out bool result);
    }
}
