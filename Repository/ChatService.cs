using ApiServices.Interface;
using Extention;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Models.DTO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Portal.Repository.Inbox;
using Services.MsSqlServices.Interface;
using Services.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using System.Reflection.Metadata;

namespace Repository
{
    public class ChatService : IChatService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly IMsSqlDataHelper msSqlDataHelper;
        private readonly IApiHelper apiDataHelper;
        readonly SimpleCache cacheProvider;
        public IConfiguration Configuration { get; }


        public string ApiURl;
        public ChatService(
                IApiHelper apiDataHelper,
                IMsSqlDataHelper msSqlDataHelper,
                IHttpClientFactory clientFactory,
              //SimpleCache cacheProvider,
              IConfiguration configuration
              //ILogger<NotificationService> logger
              )
        {
            //this.cacheProvider = cacheProvider;
            this.apiDataHelper = apiDataHelper;
            this.msSqlDataHelper = msSqlDataHelper;
            this.clientFactory = clientFactory;
            this.Configuration = configuration;
            ApiURl = Configuration["ChatbotAPI"];
            //this.Configuration = configuration;
        }
        public Task<OperationResult<string>> UploaFiles(List<UploadData> data)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult<string>> DownloadSubmission360(List<UploadData> data)
        {
            //string apiToken = await GetUserToken(userEmail);
            string chatData = await UploadCopilotFile(data);

            return new OperationResult<string>(chatData, true); ;
        }
        private async Task<string> UploadCopilotFile(List<UploadData> data)
        {
            string chatToken = "";
            var custparams = new
            {
                value = data[0].FileContent
            };

            string body = JsonConvert.SerializeObject(custparams);

            var dataItemJson = new StringContent(body);
            var request = new HttpRequestMessage(HttpMethod.Post,
                ApiURl)
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" }
                },
                Content = dataItemJson
            };


            var clientHandler = new HttpClientHandler();
            var client = clientFactory.CreateClient();
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                chatToken = await response.Content.ReadAsStringAsync();

                //var obj = JObject.Parse(result);

                //submissions = JsonConvert.DeserializeObject<Submission360>(obj.ToString());

            }
            return chatToken;
        }

        public Task<OperationResult<string>> AskCoPilot(string uniqId, string message)
        {
            throw new NotImplementedException();
        }
        private async Task<string> MessageCoPilot(string uniqId, string message)
        {
            string chatToken = "";
            var custparams = new
            {
                uniqId = uniqId,
                message = message
            };

            string body = JsonConvert.SerializeObject(custparams);

            var dataItemJson = new StringContent(body);
            var request = new HttpRequestMessage(HttpMethod.Post,ApiURl)
               // ApiURl+ "/? uniqId = " + uniqId + "message = ?" + message)
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" }
                },
                Content = dataItemJson
            };


            var clientHandler = new HttpClientHandler();
            var client = clientFactory.CreateClient();
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                chatToken = await response.Content.ReadAsStringAsync();

                //var obj = JObject.Parse(result);

                //submissions = JsonConvert.DeserializeObject<Submission360>(obj.ToString());

            }
            return chatToken;
        }
    }
}
