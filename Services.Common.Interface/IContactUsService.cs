using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.Interface
{
    public interface IContactUsService
    {
        Task<OperationResult<string>> ContactUs(ContactUs contactUs);
    }
}
