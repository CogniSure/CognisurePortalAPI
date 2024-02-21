using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.Interface
{
    public interface ISubmissionInboxService
    {
        Task<OperationResult<IEnumerable<Submission>>> GetAllSubmission(InboxFilter ObjinboxFilter);
        Task<OperationResult<SubmissionMessage>> GetSubmissionMessageBodyById(long submissionId);
        
    }
}
