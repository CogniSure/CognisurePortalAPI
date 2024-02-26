using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class DownloadResult
    {
        public string FileName { get; set; }
        public string Base64Result { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
