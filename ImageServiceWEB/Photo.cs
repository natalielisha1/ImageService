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
        #region Members
        public String thumbnailPath { get; set; }
        public String name { get; set; }
        public String year { get; set; }
        public String month { get; set; }
        #endregion

        public Photo(String path)
        {
            String[] arr = path.Split('\\');
            year = arr[1];
            month = arr[2];
            name = arr[3];
            thumbnailPath = @"Output\thumbnails\" + name + @".thumbnail"; //maybe correct later
        }
    }
}