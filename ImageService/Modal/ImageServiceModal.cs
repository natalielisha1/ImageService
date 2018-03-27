using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public string AddFile(string path, out bool result)
        {
            //TODO: Fill
            throw new NotImplementedException();
        }
    }
}
