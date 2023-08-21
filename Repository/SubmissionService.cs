using ApiServices.Interface;
using Common;
using Extention;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Newtonsoft.Json;
using Services.MsSqlServices.Interface;
using Services.Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Portal.Repository.Inbox
{
   
    public class SubmissionService : ISubmissionService
    {
        private readonly IMsSqlDataHelper msSqlDataHelper;
        private readonly IApiHelper apiDataHelper;
        readonly SimpleCache cacheProvider;
        public IConfiguration Configuration { get; }

        public SubmissionService(
                IApiHelper apiDataHelper,
                IMsSqlDataHelper msSqlDataHelper
                //SimpleCache cacheProvider,
                //IConfiguration configuration,
                //ILogger<NotificationService> logger
              )
        {
            //this.cacheProvider = cacheProvider;
            this.apiDataHelper = apiDataHelper;
            this.msSqlDataHelper = msSqlDataHelper;
            //this.Configuration = configuration;
        }
        public Task<OperationResult<Submission>> GetSubmissionData(string submissionId, string userEmail)
        {
            string apiToken = GetUserToken(userEmail);
            User usr = msSqlDataHelper.GetUser("arabindam@cognisure.ai");
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync("https://pcqa.cognisure.ai:1099/token?"+
                    "grant_type=password & username = "
                    + usr.Email+ " & password = " + usr.Password))
                {
                    var apiResponse =  response.Result;
                    //if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    //{
                    //    //string apiResponse = await response.Content.ReadAsStringAsync();
                    //    //reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                    //}
                    //else
                    //ViewBag.StatusCode = response.StatusCode;
                }
           }
            return null;
        }
        private HttpClient getClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:64195/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private string GetUserToken(string userEmail)
        {

            return "";
        }
    }
}