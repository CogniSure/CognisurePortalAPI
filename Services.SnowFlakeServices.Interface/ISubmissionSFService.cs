﻿using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SnowFlakeServices.Interface
{
    public interface ISubmissionSFService
    {
        Task<OperationResult<List<SFResult>>> GetSubmissionData(string type, string clientId, string submissionId, string userEmail);
    }
}
