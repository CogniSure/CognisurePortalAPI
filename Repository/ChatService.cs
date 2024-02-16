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
                if( data != null && data.Count>0 && data[0].FileType == "application/json")
                {
                    var custparams = new
                    {
                        filename = data[0].FileName,
                        value = data[0].FileContent
                    };

                    var client = clientFactory.CreateClient("chatclient");
                    var content = JsonContent.Create(custparams);
                    await content.LoadIntoBufferAsync();
                    //content.Headers.ContentLength = 10000000;
                    //client.DefaultRequestHeaders.AcceptEncoding = Encoding.UTF8;
                    client.DefaultRequestHeaders.TransferEncodingChunked = false;
                    HttpResponseMessage response = await client.PostAsync($"api/upload_json", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var val = await response.Content.ReadAsStringAsync();
                        chatToken = ((Newtonsoft.Json.Linq.JValue)(JsonConvert.DeserializeObject(val) as JArray).FirstOrDefault()).Value.ToString();
                        int a = 0;
                    }
                }
                else
                {
                    var custparams = new
                    {
                        filename = data[0].FileName,
                        value = data[0].FileContent.Split(',')[data[0].FileContent.Split(',').Length - 1]
                    };

                    var client = clientFactory.CreateClient("chatclient");
                    //client.DefaultRequestHeaders.Add("Co")
                    //var content = new StringContent(custparams.ToString());
                    var content = JsonContent.Create(custparams);
                    await content.LoadIntoBufferAsync();
                    HttpResponseMessage response = await client.PostAsync($"api/upload_document", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var val = await response.Content.ReadAsStringAsync();
                        chatToken = ((Newtonsoft.Json.Linq.JValue)(JsonConvert.DeserializeObject(val) as JArray).FirstOrDefault()).Value.ToString();
                        int a = 0;
                    }
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
            var content = JsonContent.Create(custparams);
            await content.LoadIntoBufferAsync();
            var client = clientFactory.CreateClient("chatclient");
            HttpResponseMessage response = await client.PostAsync($"api/get_answer", content);

            if (response.IsSuccessStatusCode)
            {
                chatToken = await response.Content.ReadAsStringAsync();
            }
            else
            {
                chatToken = "{answer:No Result Found}";
            }
            return chatToken;
        }

       
    }
}
