using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class DownloadOption
    {
        public string DownloadCode { get; set; }
        public string Format { get; set; }
        public string DownloadText { get; set; }
        public string Tooltip { get; set; }
        public string DownloadPath { get; set; }
    }
}
