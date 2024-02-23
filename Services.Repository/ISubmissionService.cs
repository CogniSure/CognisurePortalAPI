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
        Task<OperationResult<SubmissionData>> GetSubmissionData(string submissionId,string userEmail);
        Task<OperationResult<Submission360>> DownloadSubmission360(string submissionId, string userEmail);
        Task<OperationResult<List<SubmissionFile>>> GetSubmissionFiles(long submissionId, int userId, bool s360Requiredx);
        
    }
}
