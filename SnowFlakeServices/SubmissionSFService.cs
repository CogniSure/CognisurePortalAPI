using ApiServices.Interface;
using Common;
using Extention;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Models.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.MsSqlServices.Interface;
using Services.Repository.Interface;
using Services.SnowFlakeServices.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace SnowFlakeServices
{

    public class SubmissionSFService : ISubmissionSFService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly IMsSqlDataHelper msSqlDataHelper;
        private readonly ISnowFlakeDataHelper sfDataHelper;
        public IConfiguration Configuration { get; }


        public string ApiURl;
        public SubmissionSFService(
                ISnowFlakeDataHelper _SnowFlakeDataHelper,
                IConfiguration configuration
              //ILogger<NotificationService> logger
              )
        {
            this.sfDataHelper = _SnowFlakeDataHelper;
            this.msSqlDataHelper = msSqlDataHelper;
            this.Configuration = configuration;
        }
        public async Task<OperationResult<List<DataResult>>> GetExposureSummary(string type, string clientId, string submissionId, string userEmail)
        {
            List<DataResult> submissionData = sfDataHelper.GetExposerSummary(type, userEmail, clientId, submissionId);

            return new OperationResult<List<DataResult>>(submissionData, true); ;
        }
        public async Task<OperationResult<List<DataResult>>> GetLossSummary(string type, string clientId, string submissionId, string userEmail)
        {
            List<DataResult> submissionData = sfDataHelper.GetLossSummary(type, userEmail, clientId, submissionId);

            return new OperationResult<List<DataResult>>(submissionData, true); ;
        }

        public async Task<OperationResult<List<DataResult>>> GetSubmissionHeader(string type, string clientId, string submissionId, string userEmail)
        {
            List<DataResult> submissionData = sfDataHelper.GetSubmissionHeader(type, userEmail, clientId, submissionId);

            return new OperationResult<List<DataResult>>(submissionData, true);
        }

        public async Task<OperationResult<Submission>> GetSubmissionSummary(string type, string clientId, string submissionId, string userEmail)
        {
            Submission submissionData = sfDataHelper.GetSubmissionSummaryByLOB(type, userEmail, clientId, submissionId);

            return new OperationResult<Submission>(submissionData, true);
        }
    }
}