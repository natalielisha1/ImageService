using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageServiceWEB.Models
{
    public class PhotoModel
    {
        public List<String> Images
        {
            get { return GetListOfImages(); }
            set { Images = value; }
        }

        public int NumOfImages
        {
            get { return Images.Count; }
        }

        public PhotoModel() { }

        /// <summary>
        /// The function returns a list of the paths of the images
        /// in the output file.
        /// </summary>
        /// <returns>list of images(paths)</returns>
        public List<String> GetListOfImages()
        {
            string outputDirPath = @"~\Images\Output";
            string currentDirPath;
            string physicalCurrentDirPath;
            DirectoryInfo folder, yearFolder, monthFolder;
            DirectoryInfo[] years, months;
            FileInfo[] images;
            List<string> imagesList = new List<string>();
            physicalCurrentDirPath = HttpContext.Current.Server.MapPath(outputDirPath);
            folder = new DirectoryInfo(physicalCurrentDirPath);
            years = folder.GetDirectories();

            for (int i = 0; i < years.Length; i++)
            {
                if (years[i].Name.Equals("Thumbnails"))
                {
                    continue;
                }
                currentDirPath = outputDirPath + @"\" + years[i].Name;
                physicalCurrentDirPath = HttpContext.Current.Server.MapPath(currentDirPath);
                yearFolder = new DirectoryInfo(physicalCurrentDirPath);
                months = yearFolder.GetDirectories();

                for (int k = 0; k < months.Length; k++)
                {
                    currentDirPath = outputDirPath + @"\" + years[i].Name;
                    currentDirPath = currentDirPath + @"\" + months[k].Name;
                    physicalCurrentDirPath = HttpContext.Current.Server.MapPath(currentDirPath);
                    monthFolder = new DirectoryInfo(physicalCurrentDirPath);
                    images = monthFolder.GetFiles();

                    for (int j = 0; j < images.Length; j++)
                    {
                        currentDirPath = outputDirPath + @"\" + years[i].Name;
                        currentDirPath = currentDirPath + @"\" + months[k].Name;
                        currentDirPath = currentDirPath + @"\" + images[j].Name;
                        currentDirPath = currentDirPath.Substring(1);
                        imagesList.Add(currentDirPath);
                    }
                }
            }
            return imagesList;
        }
    }
}