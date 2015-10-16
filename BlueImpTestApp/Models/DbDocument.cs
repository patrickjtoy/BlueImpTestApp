

using System;
namespace BlueImpTestApp.Models
{
    public class DbDocument
    {
        public int Id { get; set; }

        public string Location { get; set; }

        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public int FileSize { get; set; }

        public byte[] FileData { get; set; }

        public int CreatedByUserId { get; set; }
        public int DeletedByUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime SubmitDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public int DocumentTypeId { get; set; }

    }

}