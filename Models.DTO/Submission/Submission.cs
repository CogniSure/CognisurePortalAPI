using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class Submission
    {
        public int SubmissionId { get; set; }
        public string? MessageId { get; set; }
        public string? SubmissionGUID { get; set; }
        public string? ClientSubmissionGUID { get; set; }
        public string? EmailMessage { get; set; }
        public string SubmissionDate { get; set; }
        public int FileReceivedChanelId { get; set; }
        public string? FileReceivedChanelName { get; set; }
        public string? AddedByName { get; set; }
        public Nullable<DateTime> AddedOnDate { get; set; }
        public int AccountId { get; set; }
        public string? AccountName { get; set; }
        public string? InsureName { get; set; }
        public int SubmissionStatusId { get; set; }
        public string? SubmissionStatusName { get; set; }
        public string? EffectiveDate { get; set; }
        public string? TypeOfBusiness { get; set; }
        public string? AgencyName { get; set; }
        public string? LineOfBusiness { get; set; }
        public string? Priority { get; set; }
        public string? RiskScore { get; set; }
        public string? ExtractionComplete { get; set; }
        public bool? Completeness { get; set; }
        public bool? RiskClearance { get; set; }
        public string? AddedOn { get; set; }
        public int TotalNoOfAttachment { get; set; }
        public int TotalNoOfValidAttachment { get; set; }
        public Agency Agency { get; set; }
        public BusinessOperation BusinessOperation { get; set; }
        public List<SubmissionLosses> TotalLosses { get; set; }
        public Exposure PropertyExposure { get; set; }
        public List<Coverages> PropertyCoverages { get; set; }
        public List<SubmissionLosses> PropertyLosses { get; set; }
        public List<Exposure> AutoExposure { get; set; }
        public List<Coverages> AutoCoverages { get; set; }
        public List<SubmissionLosses> AutoLosses { get; set; }
        public List<Exposure> WorkersCompExposure { get; set; }
        public List<Coverages> WorkersCompCoverages { get; set; }
        public List<SubmissionLosses> WorkersCompLosses { get; set; }
        public List<Exposure> GeneralLiablityExposure { get; set; }
        public List<Coverages> GeneralLiablityCoverages { get; set; }
        public List<SubmissionLosses> GeneralLiablityLosses { get; set; }
        public List<Exposure> UmbrellaExposure { get; set; }
        public List<Coverages> UmbrellaCoverages { get; set; }
        public List<SubmissionLosses> UmbrellaLosses { get; set; }
    }

}
