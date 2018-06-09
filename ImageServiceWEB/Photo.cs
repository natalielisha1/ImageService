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

namespace ImageServiceWEB
{
    public class Photo
    {
        #region Properties
        public string ThumbnailPath { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        #endregion

        public Photo(string path)
        {
            string[] arr = path.Split('\\');
            Year = arr[1];
            Month = arr[2];
            Name = arr[3];
            ThumbnailPath = @"Output\Thumbnails\" + Name;
        }
    }
}