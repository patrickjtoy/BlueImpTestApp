using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using BlueImpTestApp.Models;

namespace BlueImpTestApp.Code
{
    public class FilesBL
    {
        public ViewDataUploadFileResult SaveFile(Action<DocumentUploadModel> action, int createUserId)
        {
            DocumentUploadModel docUploadModel = new DocumentUploadModel();
            docUploadModel.FileTimeStamp = DateTime.UtcNow;
            action(docUploadModel);

            ViewDataUploadFileResult status = null;

            var fileNameWithoutPath = Path.GetFileName(docUploadModel.FileName);
            var fileExtension = Path.GetExtension(fileNameWithoutPath).Trim(new char[] { ' ', '.' });
            var fileName = Path.GetFileNameWithoutExtension(Path.GetFileName(docUploadModel.FileName));
            var genName = fileName + "-" + docUploadModel.FileTimeStamp.ToFileTime();
            var genFileName = String.IsNullOrEmpty(docUploadModel.FileName) ? genName + fileExtension : docUploadModel.FileName;// get filename if set



                try
                {
                    //Database doc model
                    DbDocument doc = new DbDocument
                    {
                        FileName = fileName,
                        FileExtension = fileExtension,
                        FileData = docUploadModel.FileData,
                        CreatedByUserId = createUserId,
                        DocumentTypeId = docUploadModel.FileTypeId,
                        FileSize = docUploadModel.FileContentLength,
                        CreateDate = DateTime.UtcNow,
                        SubmitDate = DateTime.UtcNow
                    };

                    //save doc to db


                    DocumentTypeModel docTypeModel = new DocumentTypeModel { Id = 1, TypeName = "doctype" };

                    var viewDataUploadFileResult = new ViewDataUploadFileResult()
                    {
                        id = doc.Id,
                        name = doc.FileName,
                        size = doc.FileSize,
                        type = doc.FileExtension,
                        url = string.Format("{0}/{1}", docUploadModel.UrlPrefix, doc.Id),
                        Title = fileName,
                        FileType = docTypeModel,
                        SavedFileName = genFileName
                    };

                    //add delete url                           
                    docUploadModel.AddFileUriParamToDeleteUrl("id", doc.Id.ToString());
                    status = viewDataUploadFileResult;
                }
                catch (Exception exc)
                {
                    if (docUploadModel.ThrowExceptions)
                        throw;

                    status = new ViewDataUploadFileResult()
                    {
                        error = exc.Message,
                        name = docUploadModel.FileName,
                        size = docUploadModel.FileContentLength,
                        type = docUploadModel.FileContentType
                    };
                }
            


            return status;
        }
    }
}