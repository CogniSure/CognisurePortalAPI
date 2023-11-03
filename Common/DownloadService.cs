using Extention;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Services.Common.Interface;
using Services.MsSqlServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DownloadService:IDownloadService
    {
        private readonly IMsSqlDataHelper msSqlDataHelper;
        public IConfiguration Configuration { get; }

        public DownloadService(
                IMsSqlDataHelper msSqlDataHelper,
                IConfiguration configuration,
                 ILogger<DownloadService> logger
              )
        {
            this.msSqlDataHelper = msSqlDataHelper;
            this.Configuration = configuration;
        }

        //public async Task<OperationResult<DownloadFileData>> Download(string type, string guid)
        //{
        //    var downloadFileData = msSqlDataHelper.Download(type, guid);
        //    return new OperationResult<DownloadFileData>(downloadFileData, true);
        //}
    }
}
