using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FileUploadTest.Models
{
    public class FileUpload
    {
        public FileUpload()
        {
            
        }
        public FileUpload(string localFileName, string fileName, string origin)
        {
            this.FileName = fileName.Trim('"');
            var fileFolder = System.Web.Hosting.HostingEnvironment.MapPath(Controllers.UploadController.FileName);
            var fileFullPath = Path.Combine(fileFolder, this.FileName);

            File.Copy(localFileName, fileFullPath,true);
            File.Delete(localFileName);

            this.DownloadLink = origin + "/" + Controllers.UploadController.FileName.TrimStart('~') + "/" + this.FileName;
        }

        public string FileName { get; set; }
        public string DownloadLink { get; set; }
    }
}