using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using BlueImpTestApp.Code;
using BlueImpTestApp.Models;

namespace BlueImpTestApp.Controllers.Api
{
    public class FilesController : ApiController
    {

        public HttpResponseMessage Put()
        {
            return ProcessFile();
        }

        public HttpResponseMessage Post()
        {
            return ProcessFile();
        }

        private HttpResponseMessage ProcessFile()
        {
            int userId = 1;

            var files = HttpContext.Current.Request.Files;

            string fileTypeIdStr = HttpContext.Current.Request.Form.Get("fileType");
            int fileTypeId = 1;
            try
            {
                fileTypeId = Int32.Parse(fileTypeIdStr);
            }
            catch { }

            var statuses = new List<ViewDataUploadFileResult>();

            FilesBL bl = new FilesBL();

            for (var i = 0; i < files.Count; i++)
            {
                var st = bl.SaveFile(x =>
                {
                    var file = new HttpPostedFileWrapper(files[i]);

                    x.FileContentLength = file.ContentLength;
                    x.FileData = new byte[x.FileContentLength];
                    file.InputStream.Read(x.FileData, 0, x.FileContentLength);

                    x.FileTypeId = fileTypeId;

                    x.DeleteUrl = "/api/Files";
                    x.StorageDirectory = "/Files/";
                    x.UrlPrefix = "/Files/Download";

                    //overriding defaults
                    x.FileName = files[i].FileName;
                    x.ThrowExceptions = true;
                }, userId);

                statuses.Add(st);
            }

            var response = Request.CreateResponse(HttpStatusCode.OK, new { files = statuses });

            //for IE9 which does not accept application/json
            if (HttpContext.Current.Request.Headers["Accept"] != null && !HttpContext.Current.Request.Headers["Accept"].Contains("application/json"))
            {
                if (response.Content == null)
                {
                    response.Content = new StringContent("");
                    // The media type for the StringContent created defaults to text/plain.
                }
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            }

            return response;
        }
    }
}
