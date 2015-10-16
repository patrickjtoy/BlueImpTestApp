

namespace BlueImpTestApp.Models
{
    public class ViewDataUploadFileResult
    {
        public int id { get; set; }
        public string name { get; set; }
        public int size { get; set; }
        public string type { get; set; }

        public string url { get; set; }
        //public string deleteUrl { get; set; }
        //public string thumbnailUrl { get; set; }
        //public string deleteType { get; set; }

        private string _error;
        public string error
        {
            get { return _error; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _error = value;
                    //deleteUrl = string.Empty;
                    //thumbnailUrl = string.Empty;
                    url = string.Empty;
                }

            }
        }





        public string FullPath { get; set; }
        public string SavedFileName { get; set; }
        public string Title { get; set; }
        public DocumentTypeModel FileType { get; set; }

    }
}