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
            m_OutputFolder = System.Configuration.ConfigurationManager.AppSettings["OutputDir"];
            ///check if outputdir exists, if not create one
            if (!System.IO.Directory.Exists(m_OutputFolder))
            {
                try
                {
                    DirectoryInfo dirInfo = System.IO.Directory.CreateDirectory(m_OutputFolder);
                    dirInfo.Attributes = FileAttributes.Directory | FileAttributes.Hidden;

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
                    System.IO.Directory.CreateDirectory(m_OutputFolder + @"\Thumbnails");
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
            string newFileName = GetAvailableFileName(Path.GetFileName(path), m_OutputFolder + @"\" + year + @"\" + month);
            try
            {
                File.Move(path, m_OutputFolder + @"\" + year + @"\" + month + @"\" + newFileName);
            }
            catch(IOException)
            {
                result = false;
                msg = @"moving image failed";
                return msg;
            }
            //if i'm here, path changed
            path = m_OutputFolder + @"\" + year + @"\" + month + @"\" + newFileName;
            //creating a smaller copy in thumbnails
            try
            {
                string strSize = System.Configuration.ConfigurationManager.AppSettings["ThumbnailSize"];
                //int size = Int32.Parse(strSize); //There is a member for that
                m_thumbnailSize = Int32.Parse(strSize);
                Image newImage = Image.FromFile(path);
                Bitmap smallerImage = new Bitmap(newImage, new Size(m_thumbnailSize, m_thumbnailSize));
                smallerImage.Save(m_OutputFolder + @"\Thumbnails\" + year + @"\" + month + @"\" + newFileName);
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

        /// <summary>
        /// The Function returns an available file name
        /// </summary>
        /// <param name="original">The original file name</param>
        /// <param name="folder">The folder that will contain the file</param>
        /// <returns>file name</returns>
        public string GetAvailableFileName(string original, string folder)
        {
            if (File.Exists(folder + @"\" + original))
            {
                int index = 0;
                string name = Path.GetFileNameWithoutExtension(original);
                string ext = Path.GetExtension(original);
                while (File.Exists(folder + @"\" + name + @"_" + index.ToString() + ext))
                {
                    ++index;
                }
                return name + @"_" + index.ToString() + ext;
            } else
            {
                return original;
            }
        }

        public bool CreateFolder(string year)
        {
            //create a folder of the specified year
            bool succeed = false;
            try
            {
                System.IO.Directory.CreateDirectory(m_OutputFolder + @"\" + year);
                System.IO.Directory.CreateDirectory(m_OutputFolder + @"\Thumbnails\" + year);
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
                System.IO.Directory.CreateDirectory(m_OutputFolder + @"\Thumbnails\" + year + @"\" + month);
                succeed = true;
            }
            catch (IOException)
            {
                succeed = false;
            }
            return succeed;
        }

        /// <summary>
        /// The function returns the date that the image was taken from
        /// </summary>
        /// <param name="path">The Path of the Image</param>
        /// <returns>DateTime object</returns>
        ///
        public static DateTime GetDateTakenFromImage(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                try
                {
                    PropertyItem propItem = myImage.GetPropertyItem(36867);
                    string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    return DateTime.Parse(dateTaken);
                }
                catch (ArgumentException)
                {
                    //There is no date taken, so we'll use last-modified date instead
                    return System.IO.File.GetLastWriteTime(path);
                }
            }
        }
    }
}
