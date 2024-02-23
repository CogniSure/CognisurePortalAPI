using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class SubmissionFile
    {
        //public string SlNo { get; set; }
        //public string FileGUID { get; set; }
        //public string FileName { get; set; }
        //public string DocumentType { get; set; }
        //public string LineOfBusiness { get; set; }
        //public string Carrier { get; set; }
        //public string Status { get; set; }
        //public string FileData { get; set; }
        public int ID { get; set; }
        public string FileGUID { get; set; }
        public string FileOriginalName { get; set; }
        public string Rule_InsuredName { get; set; }
        public string Rule_CarrierName { get; set; }
        public string Carriers { get; set; }
        public string LineOfBusinesses { get; set; }
        public int FileStatusID { get; set; }
        public string FileStatus { get; set; }
        public string ExtractionTime { get; set; }
        public string DocumentType { get; set; }
        public int DocumentCategoryID { get; set; }
        public string DocumentCategory { get; set; }
        public string FormNumber { get; set; }
        public string FormVersion { get; set; }
        public string FormEdition { get; set; }
        public string StatusFlag { get; set; }
        public string ValidationMessages { get; set; }
        public bool IsOCRed { get; set; }
        public bool IsMerged { get; set; }
        public bool IsScanned { get; set; }
        public bool IsJ2E7Succeeded { get; set; }
        public bool IsJ2E5Succeeded { get; set; }
        public bool IsOrigamiFileGenerated { get; set; }
        public bool IsUnknownMetaDataGenerated { get; set; }
        public bool IsMongoJsonDownloaded { get; set; }
        public bool IsInsightsReportDownloaded { get; set; }
        public bool IsInsightsReportDownloadAttempted { get; set; }
        public bool IsAcord130Flag { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string FileData { get; set; }
        //public FileType FileType { get; set; }
        public List<DownloadOption> Options { get; set; }
    }
}
