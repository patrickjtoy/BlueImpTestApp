using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueImpTestApp.Models
{
    public class DocumentUploadModel
    {
        //public HttpPostedFileBase File { get; set; }

        public int FileContentLength { get; set; }
        public string FileContentType { get; set; }
        public byte[] FileData { get; set; }

        public string StorageDirectory { get; set; }

        public string UrlPrefix { get; set; }

        public string DeleteUrl { get; set; }

        public DateTime FileTimeStamp { get; set; }


        public string FileName { get; set; }

        public int FileTypeId { get; set; }


        public string DbId { get; set; }


        public bool ThrowExceptions { get; set; }

        public void AddFileUriParamToDeleteUrl(string paramName, string fileUrl)
        {

            if (!string.IsNullOrEmpty(this.DeleteUrl))
            {
                // means has query
                if (DeleteUrl.Contains("?") && !DeleteUrl.Contains("&" + paramName))
                {
                    this.DeleteUrl += string.Format("&{0}={1}", paramName, fileUrl);
                }
                else
                {
                    this.DeleteUrl += string.Format("?{0}={1}", paramName, fileUrl);
                }
            }

        }
    }
}