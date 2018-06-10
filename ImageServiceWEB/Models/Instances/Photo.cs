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

        public Photo() { }

        public Photo(string path)
        {
            string[] arr = path.Split('\\');
            Year = arr[arr.Length - 3];
            Month = arr[arr.Length - 2];
            Name = arr[arr.Length - 1];
            ThumbnailPath = @"\Images\Output\Thumbnails\" + Year + @"\" + Month + @"\" + Name;
            //ThumbnailPath = HttpContext.Current.Server.MapPath(ThumbnailPath);
            Path = path;
        }
    }
}