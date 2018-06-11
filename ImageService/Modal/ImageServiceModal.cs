/**
 * Names: Ofek Segal & Natalie Elisha
 * IDs: 315638288 & 209475458
 * Exercise: Ex3
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
            string oldPath = string.Copy(path);

            m_OutputFolder = System.Configuration.ConfigurationManager.AppSettings["OutputDir"];
            ///check if outputdir exists, if not create one
            if (!System.IO.Directory.Exists(m_OutputFolder))
            {
                try
                {
                    //create a hidden outputDir
                    DirectoryInfo dirInfo = System.IO.Directory.CreateDirectory(m_OutputFolder);
                    dirInfo.Attributes = FileAttributes.Directory | FileAttributes.Hidden;

                }
                catch (IOException)
                {
                    //if failed, return error message
                    result = false;
                    msg = @"creating OutputDir directory failed";
                    return msg;
                }
            }
            if (!System.IO.Directory.Exists(m_OutputFolder + @"\Thumbnails"))
            {
                try
                {
                    //create thumbnails
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
                //move the file to the suitable directory
                File.Move(path, m_OutputFolder + @"\" + year + @"\" + month + @"\" + newFileName);
            }
            catch (IOException)
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
                //creating a thumbnail version and moving it to it's suitable directory
                string strSize = System.Configuration.ConfigurationManager.AppSettings["ThumbnailSize"];
                m_thumbnailSize = Int32.Parse(strSize);
                Image newImage = Image.FromFile(path);
                Bitmap smallerImage = new Bitmap(newImage, new Size(m_thumbnailSize, m_thumbnailSize));
                smallerImage.Save(m_OutputFolder + @"\Thumbnails\" + year + @"\" + month + @"\" + newFileName);
                //disposing irrelevnts
                smallerImage.Dispose();
                newImage.Dispose();
            }
            catch (IOException)
            {
                result = false;
                msg = @"failed to create and move thumbnail";
                return msg;
            }

            result = true;
            msg = @"operation succeeded - AddFile of " + oldPath;
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
                //increase the number until you'll get an available one for an original name
                while (File.Exists(folder + @"\" + name + @"_" + index.ToString() + ext))
                {
                    ++index;
                }
                return name + @"_" + index.ToString() + ext;
            }
            else
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
            catch (IOException)
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
                    //in case there's a date taken - pull it off and return it
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

        public string RemoveImage(string year, string month, string fileName, out bool result)
        {
            string msg;
            m_OutputFolder = System.Configuration.ConfigurationManager.AppSettings["OutputDir"];
            ///Deleting the full-sized image file
            if (!System.IO.Directory.Exists(m_OutputFolder))
            {
                msg = @"Output doesn't exist, no image should exist.";
                result = false;
                return msg;
            }
            if (!System.IO.Directory.Exists(m_OutputFolder + @"\" + year))
            {
                msg = @"Year folder doens't exist, no image should exist here.";
                result = false;
                return msg;
            }
            if (!System.IO.Directory.Exists(m_OutputFolder + @"\" + year + @"\" + month))
            {
                msg = @"Month folder doesn't exist, no image should exist here.";
                result = false;
                return msg;
            }
            if (!System.IO.File.Exists(m_OutputFolder + @"\" + year + @"\" + month + @"\" + fileName))
            {
                msg = @"Image file doesn't exist.";
                result = false;
                return msg;
            }
            DeleteFile(m_OutputFolder + @"\" + year + @"\" + month + @"\" + fileName);

            //Deleting the thumbnail image file
            if (!System.IO.Directory.Exists(m_OutputFolder + @"\Thumbnails"))
            {
                msg = @"Thumbnails doesn't exist, no image should exist.";
                result = false;
                return msg;
            }
            if (!System.IO.Directory.Exists(m_OutputFolder + @"\Thumbnails\" + year))
            {
                msg = @"Year folder doens't exist, no image should exist here.";
                result = false;
                return msg;
            }
            if (!System.IO.Directory.Exists(m_OutputFolder + @"\Thumbnails\" + year + @"\" + month))
            {
                msg = @"Month folder doesn't exist, no image should exist here.";
                result = false;
                return msg;
            }
            if (!System.IO.File.Exists(m_OutputFolder + @"\Thumbnails\" + year + @"\" + month + @"\" + fileName))
            {
                msg = @"Image file doesn't exist.";
                result = false;
                return msg;
            }
            DeleteFile(m_OutputFolder + @"\Thumbnails\" + year + @"\" + month + @"\" + fileName);

            msg = @"Image " + fileName + " deleted successfully.";
            result = true;
            return msg;
        }

        private void DeleteFile(string pathToFile)
        {
            while (true)
            {
                try
                {
                    File.Delete(pathToFile);
                    return;
                } catch (Exception ex)
                {
                    Thread.Sleep(500);
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                }
            }
        }
    }
}
