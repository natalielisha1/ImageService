using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        //The output folder
        private string m_OutputFolder;
        //The size of the thumbnail
        private int m_thumbnailSize;
        #endregion
        private static Regex r = new Regex(":");

        public string AddFile(string path, out bool result)
        {
            string msg;
            string outputDir = System.Configuration.ConfigurationManager.AppSettings["OutputDir"];
            ///check if outputdir exists, if not create one
            if (!System.IO.Directory.Exists(outputDir))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(@"c:\OutputDir");
                }
                catch(IOException)
                {
                    result = false;
                    msg = @"creating OutputDir directory failed";
                    return msg;
                }
            }
            if (!System.IO.Directory.Exists(outputDir + @"\Thumbnails"))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(@"c:\OutputDir\Thumbnails");
                }
                catch (IOException)
                {
                    result = false;
                    msg = @"creating Thumbnails directory failed";
                    return msg;
                }
            }
            DateTime date = GetDateTakenFromImage(path);
            string year = date.Year.ToString();
            string month = date.Month.ToString();
            if (!System.IO.Directory.Exists(outputDir + @"\" + year))
            {
                //creating new folder for year and month
                if (!(createFolder(year) && createFolder(year, month)))
                {
                    result = false;
                    msg = @"creating month/year folder failed";
                    return msg;
                }
            }
            else
            {
                if (!System.IO.Directory.Exists(outputDir + @"\" + year + @"\" + month))
                {
                    //creating new folder for month
                    if (!createFolder(year, month))
                    {
                        result = false;
                        msg = @"creating month folder failed";
                        return msg;
                    }
                }
            }
            //moving the image into the file
            try
            {
                Directory.Move(path, outputDir + @"\" + year + @"\" + month);
            }
            catch(IOException)
            {
                result = false;
                msg = @"moving image failed";
                return msg;
            }
            //creating a smaller copy in thumbnails
            try
            {
                //TO-DO: create a thumbnail version and move in into Thumbnails
                //Directory.Move(path, outputDir + @"\Thumbnails");
            }
            catch (IOException)
            {
                result = false;
                msg = @"failed to create and move thumbnail";
                return msg;
            }

            result = true;
            msg = @"operation succeeded";
            return msg;
        }

        public bool createFolder(string year)
        {
            //create a folder of the specified year
            bool succeed = false;
            try
            {
                System.IO.Directory.CreateDirectory(@"c:\OutputDir\" + year);
                succeed = true;
            }
            catch(IOException)
            {
                succeed = false;
            }
            return succeed;
        }

        public bool createFolder(string year, string month)
        {
            //create a folder of the specified month in the folder of the specified year
            bool succeed = false;
            try
            {
                System.IO.Directory.CreateDirectory(@"c:\OutputDir\" + year + @"\" + month);
                succeed = true;
            }
            catch (IOException)
            {
                succeed = false;
            }
            return succeed;
        }

        public static DateTime GetDateTakenFromImage(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                PropertyItem propItem = myImage.GetPropertyItem(36867);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }
        }
    }
}
