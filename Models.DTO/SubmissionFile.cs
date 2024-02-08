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
        public string SlNo { get; set; }
        public string FileGUID { get; set; }
        public string FileName { get; set; }
        public string DocumentType { get; set; }
        public string LineOfBusiness { get; set; }
        public string Carrier { get; set; }
        public string Status { get; set; }
        public string FileData { get; set; }
    }
}
