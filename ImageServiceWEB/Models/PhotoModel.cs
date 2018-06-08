using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageServiceWEB.Models
{
    public class PhotoModel
    {
        public PhotoModel() {}

        public List<String> Images
        {
            get { return GetListOfImages(); }
            set { Images = value; }
        }

        /// <summary>
        /// The function returns a list of the paths of the images
        /// in the output file.
        /// </summary>
        /// <returns>list of images(paths)</returns>
        public List<String> GetListOfImages()
        {
            String outputDirPath = System.IO.Directory.GetCurrentDirectory() + "/App_Data/Output";
            String currentDirPath;
            DirectoryInfo folder, yearFolder, monthFolder;
            FileInfo[] images, years, months;
            List<String> imagesList = new List<String>();

            folder = new DirectoryInfo(outputDirPath);
            years = folder.GetFiles();

            for (int i = 0; i < years.Length; i++)
            {
                currentDirPath = outputDirPath + "/" + years[i].Name;
                yearFolder = new DirectoryInfo(currentDirPath);
                months = yearFolder.GetFiles();

                for (int k = 0; k < months.Length; k++)
                {
                    currentDirPath = currentDirPath + "/" + months[k].Name;
                    monthFolder = new DirectoryInfo(currentDirPath);
                    images = monthFolder.GetFiles();

                    for (int j = 0; j < images.Length; j++)
                    {
                        imagesList.Add(currentDirPath + "/" + images[j].Name);
                    }
                }
            }
            return imagesList;
        }
    }
}