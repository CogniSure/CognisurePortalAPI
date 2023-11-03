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
    public class ContactUsService : IContactUsService
    {
        private readonly IMsSqlDataHelper msSqlDataHelper;
        public IConfiguration Configuration { get; }

        public ContactUsService(
                IMsSqlDataHelper msSqlDataHelper,
                IConfiguration configuration,
                 ILogger<ContactUsService> logger
              )
        {
            this.msSqlDataHelper = msSqlDataHelper;
            this.Configuration = configuration;
        }

        public async Task<OperationResult<string>> ContactUs(ContactUs contactUs)
        {
            string gmessage = "", tmessage = "";
            if (!msSqlDataHelper.InsertContactUs(contactUs, out gmessage, out tmessage))
            {
                return new OperationResult<string>(gmessage, true);
            }
            else
            {
                return new OperationResult<string>(tmessage, true);
            }
        }
    }
}
