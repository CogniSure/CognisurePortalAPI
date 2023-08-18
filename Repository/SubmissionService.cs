using Models.DTO;
using Services.MsSqlServices.Interface;
using Services.Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Portal.Repository.Inbox
{
   
    public class SubmissionService : ISubmissionService
    {
        public Task<OperationResult<Submission>> GetSubmissionData(string submissionId, string userEmail)
        {
            string apiToken = GetUserToken(userEmail);
            return null;
        }

        private string GetUserToken(string userEmail)
        {

            return "";
        }
    }
}