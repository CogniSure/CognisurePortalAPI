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
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Portal.Repository.Inbox
{

    public class SubmissionService : ISubmissionService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly IMsSqlDataHelper msSqlDataHelper;
        private readonly IApiHelper apiDataHelper;
        public IConfiguration Configuration { get; }


        public string ApiURl;
        public SubmissionService(
                IApiHelper apiDataHelper,
                IMsSqlDataHelper msSqlDataHelper,
                IHttpClientFactory clientFactory,
              IConfiguration configuration
              //ILogger<NotificationService> logger
              )
        {
            this.apiDataHelper = apiDataHelper;
            this.msSqlDataHelper = msSqlDataHelper;
            this.clientFactory = clientFactory;
            this.Configuration = configuration;
            ApiURl = Configuration["SubmissionApi"];
            //this.Configuration = configuration;
        }
        public async Task<OperationResult<SubmissionData>> GetSubmissionData(string submissionId, string userEmail)
        {
            string apiToken = await GetUserToken(userEmail);
            SubmissionData submissionData = await getSubmissionData(apiToken, submissionId);
            
            return new OperationResult<SubmissionData>(submissionData, true); ;
        }
        public async Task<OperationResult<Submission360>> DownloadSubmission360(string submissionId, string userEmail)
        {
            string apiToken = await GetUserToken(userEmail);
            Submission360 submissionData = await getSubmission360Data(apiToken, submissionId);

            return new OperationResult<Submission360 >(submissionData, true); ;
        }
        private async Task<Submission360> getSubmission360Data(string token, string submissionId)
        {
            Submission360 submissions = new Submission360();
            var request = new HttpRequestMessage(HttpMethod.Get,
                ApiURl+"api/submission/submission360/?Id=" + submissionId)
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    {HeaderNames.Authorization, "Bearer "+ token }
                },
            };
            var clientHandler = new HttpClientHandler();
            var client = clientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var obj = JObject.Parse(result);

                submissions = JsonConvert.DeserializeObject<Submission360>(obj.ToString());
                
            }
            return submissions;
        }
        private async Task<SubmissionData> getSubmissionData(string token,string submissionId)
        {
            //string submissionId = "A1_Id";
            SubmissionData submissions = new SubmissionData();
            var request = new HttpRequestMessage(HttpMethod.Get,
                ApiURl + "api/submission/commonjson/?id=" + submissionId)
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    {HeaderNames.Authorization, "Bearer "+ token }
                },
            };
            var clientHandler = new HttpClientHandler();
            var client = clientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();

                var obj = JObject.Parse(result);

                submissions = JsonConvert.DeserializeObject<SubmissionData>(obj["data"].ToString());
                //apiToken = obj["access_token"].ToString();
            }
            //return apiToken;
            return submissions;
        }
        private HttpClient getClient(string url)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            return client;
        }

        private async Task<string> GetUserToken(string userEmail)
        {
            string apiToken = "";
            User usr = msSqlDataHelper.GetUser(userEmail);
            var request = new HttpRequestMessage(HttpMethod.Post,
                ApiURl + "token")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/x-www-form-urlencoded" },
                }
            };
            request.Content = new StringContent("grant_type=password&username="+usr.Email+"&password="+usr.Password, Encoding.UTF8, "application/x-www-form-urlencoded");


            var client = clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();

                var obj = JObject.Parse(result);
                apiToken = obj["access_token"].ToString();
            }
            return apiToken;
        }

        public async Task<OperationResult<List<SubmissionFile>>> GetSubmissionFiles(long submissionId, int userId, bool s360Required)
        {
            List<SubmissionFile> submissionData = msSqlDataHelper.GetSubmissionFiles( submissionId,s360Required, userId);
            return new OperationResult<List<SubmissionFile>>(submissionData, true);
        }
        public async Task<OperationResult<DownloadResult>> DownloadSubmissionFiles(string submissionId, string filename, string downloadCode, string format,string extension)
        {
            DownloadResult dr = msSqlDataHelper.DownloadSubmissionFiles(submissionId, filename, downloadCode, format, extension);
           
            return new OperationResult<DownloadResult>(dr, true);
        }
    }
}