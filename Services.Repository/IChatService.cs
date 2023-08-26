using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.Interface
{
    public interface IChatService
    {

        Task<OperationResult<string>> UploaFiles(List<UploadData> data);
        Task<OperationResult<string>> AskCoPilot(string uniqId, string message);
    }
}
