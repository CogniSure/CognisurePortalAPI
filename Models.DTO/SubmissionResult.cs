using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class SubmissionResult
    {
        public string ProducerFullName { get; set; }
        public string InsurerProducerIdentifier { get; set; }
        //public int MyProperty { get; set; }
    }
    public class Agency
    {
        public string AgencyName { get; set; }
        public string AgencyCode { get; set; }
        public string Producer { get; set; }
        public string ProducerEmail { get; set; }
        public string ProducerPhoneNo { get; set; }
        public string ActivityRank { get; set; }
    }
    public class BusinessOperation
    {
        public string Sic { get; set; }
        public string Naics { get; set; }
        public string Descriptions { get; set; }
    }
    public class SubmissionLosses
    {
        public string? Year { get; set; }
        public string? GrossAmount { get; set; }
        public string? TotalNoOfClaims { get; set; }
        public string? NoOfOpenClaims { get; set; }
    }
    public class Exposure
    {
        public string? Total { get; set; }
        public string? ExposureValue1 { get; set; }
        public string? ExposureValue2 { get; set; }
        public string? ExposureValue3 { get; set; }

        //public string? BuildingsCount { get; set; }
        //public string? LocationsCount { get; set; }
        //public string? StatesCount { get; set; }
    }
    //public class AutoExposure
    //{
    //    public string? BodyType { get; set; }
    //    public string? VehicleCount { get; set; }
    //    public string? DriverCount { get; set; }
    //    public string? BodyTypeCount { get; set; }
    //}
    //public class WorkerCompExposure
    //{
    //    public string? TotalCount { get; set; }
    //    public string? ClassCode { get; set; }
    //    public string? ClassCodeDescription { get; set; }
    //    public string? Payroll { get; set; }
    //}
    public class Coverages
    {
        public string CoverageName { get; set; }
        public string CoverageValue { get; set; }
        public string CoverageType { get; set; }
        //public string? BuildingLimit { get; set; }
        //public string? ContentLimit { get; set; }
        //public string? BusinessLimit { get; set; }
        //public string? OtherLimit { get; set; }
    }
}
