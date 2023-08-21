﻿using ApiServices.Interface;
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
        readonly SimpleCache cacheProvider;
        public IConfiguration Configuration { get; }

        public SubmissionService(
                IApiHelper apiDataHelper,
                IMsSqlDataHelper msSqlDataHelper,
                IHttpClientFactory clientFactory
              //SimpleCache cacheProvider,
              //IConfiguration configuration,
              //ILogger<NotificationService> logger
              )
        {
            //this.cacheProvider = cacheProvider;
            this.apiDataHelper = apiDataHelper;
            this.msSqlDataHelper = msSqlDataHelper;
            this.clientFactory = clientFactory;
            //this.Configuration = configuration;
        }
        public async Task<OperationResult<SubmissionData>> GetSubmissionData(string submissionId, string userEmail)
        {
            string apiToken = await GetUserToken(userEmail);
            SubmissionData submissionData = await getSubmissionData(apiToken, submissionId);
            // using (var httpClient = getClient("https://pcqa.cognisure.ai:1099"))
            // {
            //     using (var response = httpClient.GetAsync("/token?"+
            //         "grant_type=password&username=niranjansn@cognisure.ai&password=Iamniru@1"))
            //     {
            //         var apiResponse =  response.Result;
            //         //if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //         //{
            //         //    //string apiResponse = await response.Content.ReadAsStringAsync();
            //         //    //reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
            //         //}
            //         //else
            //         //ViewBag.StatusCode = response.StatusCode;
            //     }
            //}
            return new OperationResult<SubmissionData>(submissionData, true); ;
        }
        private async Task<SubmissionData> getSubmissionData(string token,string submissionId)
        {
            //string submissionId = "A1_Id";
            SubmissionData submissions = new SubmissionData();
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://pcqa.cognisure.ai:1099/api/submission/commonjson/?id=" + submissionId)
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
                "https://pcqa.cognisure.ai:1099/token")
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
    }
}