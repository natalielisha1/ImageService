/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex4
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    public interface IImageServiceModal
    {
        /// <summary>
        /// The Function Addes A file to the system
        /// </summary>
        /// <param name="path">The Path of the Image from the file</param>
        /// <param name="result">The boolean result of the action</param>
        /// <returns>Indication if the Addition Was Successful</returns>
        string AddFile(string path, out bool result);

        /// <summary>
        /// This function removes an image from the output folder
        /// </summary>
        /// <param name="year">The year of the image</param>
        /// <param name="month">The month of the image</param>
        /// <param name="fileName">The file name</param>
        /// <param name="result">The boolean result of the action</param>
        /// <returns>Indication if the Addition Was Successful</returns>
        string RemoveImage(string year, string month, string fileName, out bool result);

        /// <summary>
        /// The Function creates a folder of a month in OutputDir
        /// </summary>
        /// <param name="year">The year of the file will's create the month file in</param>
        /// <param name="month">The name of the month file</param>
        /// <returns>boolean indication of wether the creation sas successful</returns>
        bool CreateFolder(string year, string month);

        /// <summary>
        /// The Function creates a folder of a year in OutputDir
        /// </summary>
        /// <param name="year">The name of the new year-file</param>
        /// <returns>boolean indication of wether the creation sas successful</returns>
        bool CreateFolder(string year);
    }
}
