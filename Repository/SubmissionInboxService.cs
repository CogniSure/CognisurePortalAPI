using Extention;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Portal.Repository.Login;
using Services.MsSqlServices.Interface;
using Services.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Repository.Inbox
{
    public class SubmissionInboxService:ISubmissionInboxService
    {
        private readonly IMsSqlDataHelper msSqlDataHelper;
        public IConfiguration Configuration { get; }

        public SubmissionInboxService(
                IMsSqlDataHelper msSqlDataHelper,
                IConfiguration configuration,
                 ILogger<SubmissionInboxService> logger
              )
        {
            this.msSqlDataHelper = msSqlDataHelper;
            this.Configuration = configuration;
        }
        public async Task<OperationResult<IEnumerable<Submission>>> GetAllSubmission(InboxFilter ObjinboxFilter)
        {
            var Data = msSqlDataHelper.GetAllSubmission(ObjinboxFilter);
            return new OperationResult<IEnumerable<Submission>>(Data, true);
        }
    }
}
