using Microsoft.Extensions.Configuration;
using Models.DTO;
using Services.MsSqlServices.Interface;
using Services.Repository;
using Services.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class URLService : IURLService
    {
        private readonly IMsSqlDataHelper helper;
        public IConfiguration Configuration { get; }
        public URLService(IMsSqlDataHelper helper, IConfiguration Configuration) : base()
        {
            this.helper = helper;
            this.Configuration = Configuration;
        }
        public async Task<OperationResult<WidgetConfiguration>> GetURL(long userId, string pageName, string widgetCode, string action)
        {
            return new OperationResult<WidgetConfiguration>(helper.GetURL(userId, pageName, widgetCode, action),true); 
        }
    }
}
