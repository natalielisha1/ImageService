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
            //string outputDir = System.Configuration.ConfigurationManager.AppSettings["OutputDir"]; //There is a member for that
            m_OutputFolder = System.Configuration.ConfigurationManager.AppSettings["OutputDir"];
            ///check if outputdir exists, if not create one
            if (!System.IO.Directory.Exists(m_OutputFolder))
            {
                try
                {
                    //System.IO.Directory.CreateDirectory(@"C:\OutputDir"); //We need the outputdir to be the one from AppConfig (not C:\Outputdir)
                    System.IO.Directory.CreateDirectory(m_OutputFolder);
                }
                catch(IOException)
                {
                    result = false;
                    msg = @"creating OutputDir directory failed";
                    return msg;
                }
            }
            if (!System.IO.Directory.Exists(m_OutputFolder + @"\Thumbnails"))
            {
                try
                {
                    //System.IO.Directory.CreateDirectory(@"c:\OutputDir\Thumbnails");
                    System.IO.Directory.CreateDirectory(m_OutputFolder + @"\Thumbnails"); //We need the outputdir to be the one from AppConfig (not C:\Outputdir)
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
            if (!System.IO.Directory.Exists(m_OutputFolder + @"\" + year))
            {
                //creating new folder for year and month
                if (!(CreateFolder(year) && CreateFolder(year, month)))
                {
                    result = false;
                    msg = @"creating month/year folder failed";
                    return msg;
                }
            }
            else
            {
                if (!System.IO.Directory.Exists(m_OutputFolder + @"\" + year + @"\" + month))
                {
                    //creating new folder for month
                    if (!CreateFolder(year, month))
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
                Directory.Move(path, m_OutputFolder + @"\" + year + @"\" + month);
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
                string strSize = System.Configuration.ConfigurationManager.AppSettings["ThumbnailSize"];
                //int size = Int32.Parse(strSize); //There is a member for that
                m_thumbnailSize = Int32.Parse(strSize);
                Image newImage = Image.FromFile(path);
                Bitmap smallerImage = new Bitmap(newImage, new Size(m_thumbnailSize, m_thumbnailSize));
                string fileName = Path.GetFileName(path);
                smallerImage.Save(m_OutputFolder + @"\Thumbnails\" + fileName);
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

        public bool CreateFolder(string year)
        {
            //create a folder of the specified year
            bool succeed = false;
            try
            {
                System.IO.Directory.CreateDirectory(m_OutputFolder + @"\" + year);
                succeed = true;
            }
            catch(IOException)
            {
                succeed = false;
            }
            return succeed;
        }

        public bool CreateFolder(string year, string month)
        {
            //create a folder of the specified month in the folder of the specified year
            bool succeed = false;
            try
            {
                System.IO.Directory.CreateDirectory(m_OutputFolder + @"\" + year + @"\" + month);
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
