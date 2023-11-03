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
using System.Net.Http.Json;

namespace Repository
{
    public class ChatService : IChatService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly IMsSqlDataHelper msSqlDataHelper;
        private readonly IApiHelper apiDataHelper;
        public IConfiguration Configuration { get; }

        public string ApiURl;
        public ChatService(
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
            ApiURl = Configuration["ChatbotAPI"];
            //this.Configuration = configuration;
        }
        //public Task<OperationResult<string>> UploadFiles(List<UploadData> data)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<OperationResult<string>> UploadFiles(List<UploadData> data)
        {
            //string apiToken = await GetUserToken(userEmail);
            string chatData = await UploadCopilotFile(data);

            return new OperationResult<string>(chatData, true); ;
        }
        private async Task<string> UploadCopilotFile(List<UploadData> data)
        {
            try
            {
                string chatToken = "";
                var custparams = new
                {
                    value = data[0].FileContent.Split(',')[data[0].FileContent.Split(',').Length-1]
                };

                //string body = JsonConvert.SerializeObject(custparams);

                //var dataItemJson = new StringContent(body);
                ////dataItemJson.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //var request = new HttpRequestMessage(HttpMethod.Post,
                //    ApiURl+ "api/upload_document")
                //{
                //    Headers =
                //{
                //    { HeaderNames.Accept, "application/json" }
                //},
                //    Content = dataItemJson
                //};


                //var clientHandler = new HttpClientHandler();
                var client = clientFactory.CreateClient("chatclient");
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await client.PostAsJsonAsync($"api/upload_document", custparams);

                if (response.IsSuccessStatusCode)
                {
                    chatToken = await response.Content.ReadAsStringAsync();

                    //var obj = JObject.Parse(result);

                    //submissions = JsonConvert.DeserializeObject<Submission360>(obj.ToString());

                }
                return chatToken;

            }
            catch (Exception ex)
            {
                return "";
            }
            
        }

        public async Task<OperationResult<string>> AskCoPilot(string uniqId, string message)
        {
            string chatData = await MessageCoPilot(uniqId, message);
            
            return new OperationResult<string>(chatData, true); ;
        }
        private async Task<string> MessageCoPilot(string uniqId, string message)
        {
            string chatToken = "";
            var custparams = new
            {
                fileGUID = uniqId,
                question = message
            };

            string body = JsonConvert.SerializeObject(custparams);

            var client = clientFactory.CreateClient("chatclient");
            HttpResponseMessage response = await client.PostAsJsonAsync($"api/get_chat_history", custparams);

            if (response.IsSuccessStatusCode)
            {
                chatToken = await response.Content.ReadAsStringAsync();
            }
            return chatToken;
        }

       
    }
}
