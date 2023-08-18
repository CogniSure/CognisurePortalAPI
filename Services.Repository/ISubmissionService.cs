using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.Interface
{
    public interface ISubmissionService
    {
        Task<OperationResult<Submission>> GetSubmissionData(string submissionId,string userEmail);
    }
}
