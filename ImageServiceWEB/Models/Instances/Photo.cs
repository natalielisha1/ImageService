/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex4
 */
using ImageService.Communication;
using ImageService.Communication.Interfaces;
using ImageService.Communication.Model;
using ImageService.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ImageServiceWEB.Models.Instances
{
    public class Photo
    {
        #region Properties
        public string ThumbnailPath { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the Photo class.
        /// </summary>
        public Photo() { }

        /// <summary>
        /// Initializes a new instance of the Photo class.
        /// </summary>
        /// <param name="path">The path of the photo.</param>
        public Photo(string path)
        {
            string[] arr = path.Split('\\');
            Year = arr[arr.Length - 3];
            Month = arr[arr.Length - 2];
            Name = arr[arr.Length - 1];
            ThumbnailPath = @"\Images\Output\Thumbnails\" + Year + @"\" + Month + @"\" + Name;
            Path = path;
        }
    }
}