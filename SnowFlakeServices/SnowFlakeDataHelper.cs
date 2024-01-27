using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.DTO;
using Services.SnowFlakeServices.Interface;
using SnowFlakeAdapter.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFlakeServices
{
    public class SnowFlakeDataHelper : ISnowFlakeDataHelper
    {
        private readonly ISnowFlakeDatabase Database;
        public IConfiguration Configuration { get; }
        public SnowFlakeDataHelper(ISnowFlakeDatabase Database, IConfiguration Configuration) : base()
        {
            this.Database = Database;
            this.Configuration = Configuration;
        }
        public List<DataResult> GetDashboardGraphData(DashboardFilter dashboardFilter, string Type)
        {
            List<DataResult> lstDasboardgraph = new List<DataResult>();
            DataSet DS = new DataSet();
            switch (Type.ToLower())
            {
                case "countbyturnaroundtime":
                    {
                        DS = Database.DashboardGraph_CountByTurnaroundTime(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("TAT")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("SUBMISSIONID"))
                                    }).ToList();
                        List<DataResult> dashboard = new List<DataResult>();
                        List<DataResult> distinctDashboard = new List<DataResult>();
                        distinctDashboard = lstDasboardgraph.DistinctBy(x => x.Dimension).ToList();
                        foreach (var row in distinctDashboard)
                        {
                            DataResult graph = new DataResult
                            {
                                Category = "Days",
                                Dimension = row.Dimension + " min",
                                Measure = lstDasboardgraph.Where(msr => msr.Dimension == row.Dimension).ToList().Count().ToString()
                            };

                            dashboard.Add(graph);

                        }
                        int a = 1;
                        lstDasboardgraph = dashboard;
                    }
                    break;
                case "countbylob":
                    {
                        DS = Database.DashboardGraph_CountByLOB(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("LOB")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFSUBMISSIONID"))
                                    }).ToList();
                    }
                    break;
                case "countbybroker":
                    {
                        DS = Database.DashboardGraph_CountByByBroker(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                   .Select(dataRow => new DataResult
                                   {
                                       Dimension = string.Format("{0}", dataRow.Field<string>("BROKERNAME")),
                                       Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFSUBMISSIONID"))
                                   }).ToList();
                    }
                    break;
                case "countbycity":
                    {
                        DS = Database.DashboardGraph_CountByCity(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                   .Select(dataRow => new DataResult
                                   {
                                       Dimension = string.Format("{0}", dataRow.Field<string>("CITYNAME")),
                                       Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFSUBMISSIONID"))
                                   }).ToList();
                    }
                    break;
                case "countbystate":
                    {
                        DS = Database.DashboardGraph_CountByState(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                   .Select(dataRow => new DataResult
                                   {
                                       Dimension = string.Format("{0}", dataRow.Field<string>("STATENAME")),
                                       Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFSUBMISSIONID"))
                                   }).ToList();
                    }
                    break;
                case "countbyindustries":
                    {
                        DS = Database.DashboardGraph_CountByIndustries(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                   .Select(dataRow => new DataResult
                                   {
                                       Dimension = string.Format("{0}", dataRow.Field<string>("NAICCODE")),
                                       Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFSUBMISSIONID"))
                                   }).ToList();
                    }
                    break;
                case "countofsubmissionprofileandvolume":
                    {
                        DS = Database.DashboardGraph_CountOfSubmissionId(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            dashboardFilter.StartDate, dashboardFilter.EndDate);
                        //DS = Database.DashboardGraph_CountOfSubmissionId(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                        //    Convert.ToDateTime(dashboardFilter.StartDate), Convert.ToDateTime(dashboardFilter.EndDate));
                        DataSet DSDocType = new DataSet();
                        DSDocType = Database.DashboardGraph_CountOfDocType(dashboardFilter.TopNumber, dashboardFilter.CLIENTID, dashboardFilter.UserEmailId,
                            dashboardFilter.StartDate, dashboardFilter.EndDate);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                   .Select(dataRow => new DataResult
                                   {
                                       Dimension = "SubmissionIdCount",
                                       Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFSUBMISSIONID"))
                                   }).ToList();
                        lstDasboardgraph.AddRange(
                            DSDocType.Tables[0].AsEnumerable()
                                   .Select(dataRow => new DataResult
                                   {
                                       Dimension = string.Format("{0}", dataRow.Field<string>("DOCUMENTTYPE")) == ""?"Others": string.Format("{0}", dataRow.Field<string>("DOCUMENTTYPE")),
                                       Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFSUBMISSIONID"))
                                   }).ToList()
                            );
                       
                    }
                    break;

            }
            return lstDasboardgraph;
        }
        public List<DataResult> GetSubmissionHeader(string type, string email, string clientId, string subGuid)
        {
            List<DataResult> lstDasboardgraph = new List<DataResult>();
            DataSet DS = new DataSet();
            DS = Database.GetSubmissionHeader(clientId, email, subGuid);
            lstDasboardgraph = DS.Tables[0].AsEnumerable()
                        .Select(dataRow => new DataResult
                        {
                            Category = "",
                            Dimension = string.Format("{0}", dataRow.Field<string>("HEADERS")),
                            Measure = string.Format("{0}", dataRow.Field<string>("RESULT"))
                        }).ToList();
            return lstDasboardgraph;
        }
        public List<DataResult> GetExposerSummary(string type, string email, string clientId, string subGuid)
        {
            List<DataResult> lstDasboardgraph = new List<DataResult>();
            DataSet DS = new DataSet();
            switch (type.ToLower())
            {
                case "exposure_tiv":
                    {
                        DS = Database.Sub_Exposure_TIV(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("SUBMISSIONID")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("SUMOFTIV"))
                                    }).ToList();
                    }
                    break;
                case "exposure_locationcount":
                    {
                        DS = Database.Sub_Exposure_LocationsCount(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("SUBMISSIONID")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFLOCATIONS"))
                                    }).ToList();
                    }
                    break;
                case "exposure_buildingscount":
                    {
                        DS = Database.Sub_Exposure_BuildingsCount(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("SUBMISSIONID")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFBUILDINGS"))
                                    }).ToList();
                    }
                    break;
                case "exposure_constructiontype":
                    {
                        DS = Database.Sub_Exposure_ConstructionType(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("CONSTRUCTIONTYPE")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFCONSTRUCTIONTYPE"))
                                    }).ToList();
                    }
                    break;
                case "exposure_occupancytype":
                    {
                        DS = Database.Sub_Exposure_OccupancyType(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("OCCUPANYTYPE")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFOCCUPANYTYPE"))
                                    }).ToList();
                    }
                    break;
                case "exposure_yearbuild":
                    {
                        DS = Database.Sub_Exposure_YearBuild(clientId, email, subGuid);
                        var rawData = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("RANGES")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFBUIDLINGAGE"))
                                    }).ToList();
                        lstDasboardgraph = rawData;
                        //    new List<DataResult>()
                        //{
                        //    new DataResult { Dimension = "0-5", Measure = rawData.Where(x=> Convert.ToInt64(x.Measure) >= 0 && Convert.ToInt64(x.Measure) <= 5).Count().ToString() },
                        //    new DataResult { Dimension = "6-10", Measure = rawData.Where(x=> Convert.ToInt64(x.Measure) >= 6 && Convert.ToInt64(x.Measure) <= 10).Count().ToString() },
                        //    new DataResult { Dimension = "11-15", Measure = rawData.Where(x=> Convert.ToInt64(x.Measure) >= 11 && Convert.ToInt64(x.Measure) <= 15).Count().ToString() },
                        //    new DataResult { Dimension = "16-25", Measure = rawData.Where(x=> Convert.ToInt64(x.Measure) >= 16 && Convert.ToInt64(x.Measure) <= 25).Count().ToString() },
                        //    new DataResult { Dimension = "26-75", Measure = rawData.Where(x=> Convert.ToInt64(x.Measure) >= 26 && Convert.ToInt64(x.Measure) <= 75).Count().ToString() },
                        //    new DataResult { Dimension = "Greater than 75", Measure = rawData.Where(x=> Convert.ToInt64(x.Measure) > 75).Count().ToString() },
                        //};

                    }
                    break;
                case "exposure_protectionclass":
                    {
                        DS = Database.Sub_Exposure_ProtectionClass(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Dimension = string.Format("{0}", dataRow.Field<string>("PROTECTIONCLASSCODE")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFPROTECTIONCLASSCODE"))
                                    }).ToList();
                    }
                    break;

            }
            return lstDasboardgraph;
        }
        public List<DataResult> GetLossSummary(string type, string email, string clientId, string subGuid)
        {
            List<DataResult> lstDasboardgraph = new List<DataResult>();
            DataSet DS = new DataSet();
            switch (type.ToLower())
            {
                case "loss_claimsbylobbyyear":
                    {
                        DS = Database.Sub_Loss_ClaimByLobByYear(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = string.Format("{0}", dataRow.Field<string>("LOB")),
                                        Dimension = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFCLAIMS"))
                                    }).ToList();
                    }
                    break;
                case "loss_incurredbylobbyyear":
                    {
                        DS = Database.Sub_Loss_IncurredByLobByYear(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = string.Format("{0}", dataRow.Field<string>("LOB")),
                                        Dimension = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("INCURRED"))
                                    }).ToList();
                    }
                    break;
                case "loss_incurredrangecount":
                    {
                        DS = Database.Sub_Loss_IncurredRangeCount(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = string.Format("{0}", dataRow.Field<string>("RANGES")),
                                        Dimension = string.Format("{0}", dataRow.Field<string>("RANGES")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("INCURREDCOUNT"))
                                    }).ToList();
                    }
                    break;
                case "loss_claimbyclaimtypebyyear":
                    {
                        DS = Database.Sub_Loss_ClaimByClaimTypeByYear(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = string.Format("{0}", dataRow.Field<string>("CLAIMTYPE")),
                                        Dimension = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFCLAIMS"))
                                    }).ToList();
                    }
                    break;
                case "loss_incurredbyclaimtypebyyear":
                    {
                        DS = Database.Sub_Loss_IncurredByClaimTypeByYear(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = string.Format("{0}", dataRow.Field<string>("CLAIMTYPE")),
                                        Dimension = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("INCURRED"))
                                    }).ToList();
                    }
                    break;
                case "loss_claimsbyclaimtype":
                    {
                        DS = Database.Sub_Loss_ClaimByClaimType(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = string.Format("{0}", dataRow.Field<string>("CLAIMTYPE")),
                                        Dimension = string.Format("{0}", dataRow.Field<string>("CLAIMTYPE")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFCLAIMS"))
                                    }).ToList();
                    }
                    break;
                case "loss_claimstatus":
                    {
                        DS = Database.Sub_Loss_ClaimStatus(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = "",
                                        Dimension = string.Format("{0}", dataRow.Field<string>("CLAIMSTATUS")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("PERCENTAGEOFCLAIMSTATUS"))
                                    }).ToList();
                    }
                    break;
                case "loss_totalincurred":
                    {
                        DS = Database.Sub_Loss_TotalIncurred(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = "",
                                        Dimension = string.Format("{0}", dataRow.Field<string>("FIELDNAMES")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("AMOUNTS"))
                                    }).ToList();
                    }
                    break;
                case "loss_toplocations":
                    {
                        DS = Database.Sub_Loss_ClaimByLobByYear(clientId, email, subGuid);
                        lstDasboardgraph = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new DataResult
                                    {
                                        Category = string.Format("{0}", dataRow.Field<string>("CLAIMTYPE")),
                                        Dimension = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        Measure = string.Format("{0}", dataRow.Field<string>("COUNTOFCLAIMS"))
                                    }).ToList();
                    }
                    break;
            }
            return lstDasboardgraph;
        }

        public Submission GetSubmissionSummaryByLOB(string type, string email, string clientId, string subGuid)
        {
            Submission submissionData = new Submission();
            DataSet DS = new DataSet();
            switch (type.ToLower())
            {
                case "sub_agencies_all":
                    {
                        DS = Database.Sub_Summary_Agency(clientId, email, subGuid);
                        Agency agency = new Agency();

                        DS.Tables[0].AsEnumerable().ToList().ForEach(data =>
                        {
                            var key = data.Field<string>("HEADERS").ToLower();
                            switch (key)
                            {
                                case "producerfullname":
                                    agency.AgencyName = data.Field<string>("RESULT");
                                    break;
                                case "insurerproduceridentifier": 
                                    agency.Producer = data.Field<string>("RESULT");
                                    break;
                            }
                            var v = data;
                        });
                        submissionData.Agency = agency;
                    }
                    break;
                case "sub_businessoperations_all":
                    {
                        DS = Database.Sub_Summary_BusinessOperations(clientId, email, subGuid);
                        BusinessOperation bussOperation = new BusinessOperation();

                        DS.Tables[0].AsEnumerable().ToList().ForEach(data =>
                        {
                            var key = data.Field<string>("HEADERS").ToLower();
                            switch (key)
                            {
                                case "namedinsurednaiccode":
                                    bussOperation.Naics = data.Field<string>("RESULT");
                                    break;
                                //case "insurerproduceridentifier":
                                //    agency.Producer = data.Field<string>("RESULT");
                                //    break;
                            }
                            var v = data;
                        });
                        submissionData.BusinessOperation = bussOperation;
                    }
                    break;
                case "sub_totallosses_all":
                    {
                        DS = Database.Sub_Summary_TotalLosses(clientId, email, subGuid);
                        submissionData.TotalLosses = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new SubmissionLosses
                                    {
                                        Year = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        GrossAmount = string.Format("{0}", dataRow.Field<string>("GROSSINCURRED")),
                                        TotalNoOfClaims = string.Format("{0}", dataRow.Field<string>("CLAIMNUMBER")),
                                        NoOfOpenClaims = string.Format("{0}", dataRow.Field<string>("COUNTOFOPENCLAIMS"))
                                    }).ToList();
                    }
                    break;
                case "sub_exposure_property":
                    {
                        DS = Database.Sub_Summary_Property_Exposure(clientId, email, subGuid);
                        
                        var data = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new PropertyExposure
                                    {
                                        TIV = string.Format("{0}", dataRow.Field<string>("TIV")),
                                        BuildingsCount = string.Format("{0}", dataRow.Field<string>("COUNTOFBUILDINGS")),
                                        LocationsCount = string.Format("{0}", dataRow.Field<string>("COUNTOFLOCATIONS")),
                                        StatesCount = string.Format("{0}", dataRow.Field<string>("COUNTOFSTATES"))
                                    });
                        if (data != null)
                        {
                            submissionData.PropertyExposure = data.FirstOrDefault();
                        }
                    }
                    break;
                case "sub_coverage_property":
                    {
                        DS = Database.Sub_Summary_Property_Coverages(clientId, email, subGuid);
                        List<PropertyCoverages> propertyCoverages = new List<PropertyCoverages>();

                        DS.Tables[0].AsEnumerable().ToList().ForEach(data =>
                        {
                            //var key = data.Field<string>("HEADERS").ToLower();
                            if (data.Field<string>("BUILDINGLIMIT") != null)
                            {
                                propertyCoverages.Add(new PropertyCoverages
                                {
                                    CoverageName = "Building",
                                    CoverageValue = string.Format("{0}", data.Field<string>("BUILDINGLIMIT")),
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("CONTENTLIMIT") != null)
                            {
                                propertyCoverages.Add(new PropertyCoverages
                                {
                                    CoverageName = "Content",
                                    CoverageValue = string.Format("{0}", data.Field<string>("CONTENTLIMIT")),
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("BUSINESSINCOMELIMIT") != null)
                            {
                                propertyCoverages.Add(new PropertyCoverages
                                {
                                    CoverageName = "Business Income",
                                    CoverageValue = string.Format("{0}", data.Field<string>("BUSINESSINCOMELIMIT")),
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("OTHERLIMIT") != null)
                            {
                                propertyCoverages.Add(new PropertyCoverages
                                {
                                    CoverageName = "Others",
                                    CoverageValue = string.Format("{0}", data.Field<string>("OTHERLIMIT")),
                                    CoverageType = ""
                                });
                            }
                           
                            
                        });
                        if (propertyCoverages != null)
                        {
                            submissionData.PropertyCoverages = propertyCoverages;
                        }
                    }
                    break;
                case "sub_losses_property":
                    {
                        DS = Database.Sub_Summary_Property_Losses(clientId, email, subGuid);
                        submissionData.PropertyLosses = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new SubmissionLosses
                                    {
                                        Year = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        GrossAmount = string.Format("{0}", dataRow.Field<string>("GROSSINCURRED")),
                                        TotalNoOfClaims = string.Format("{0}", dataRow.Field<string>("CLAIMNUMBER")),
                                        NoOfOpenClaims = string.Format("{0}", dataRow.Field<string>("COUNTOFOPENCLAIMS"))
                                    }).ToList();
                    }
                    break;
                case "sub_exposure_auto":
                    {
                        DS = Database.Sub_Summary_Auto_Exposure(clientId, email, subGuid);

                        var data = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new AutoExposure
                                    {
                                        BodyType = "",//string.Format("{0}", dataRow.Field<string>("TIV")),
                                        VehicleCount = string.Format("{0}", dataRow.Field<string>("TOTALCOUNTOFVEHICLES")),
                                        DriverCount = string.Format("{0}", dataRow.Field<string>("TOTALCOUNTOFDRIVERS")),
                                        BodyTypeCount = string.Format("{0}", dataRow.Field<string>("TOTALCOUNTOFBODYTYPE"))
                                    });
                        if (data != null)
                        {
                            submissionData.AutoExposure = data.ToList();
                        }
                    }
                    break;
                case "sub_vehicle_auto":
                    {
                        DS = Database.Sub_Summary_Auto_Coverages(clientId, email, subGuid);
                        List<PropertyCoverages> propertyCoverages = new List<PropertyCoverages>();

                        DS.Tables[0].AsEnumerable().ToList().ForEach(data =>
                        {
                            //var key = data.Field<string>("HEADERS").ToLower();
                            if (data.Field<string>("BUILDINGLIMIT") != null)
                            {
                                propertyCoverages.Add(new PropertyCoverages
                                {
                                    CoverageName = "Building",
                                    CoverageValue = string.Format("{0}", data.Field<string>("BUILDINGLIMIT")),
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("CONTENTLIMIT") != null)
                            {
                                propertyCoverages.Add(new PropertyCoverages
                                {
                                    CoverageName = "Content",
                                    CoverageValue = string.Format("{0}", data.Field<string>("CONTENTLIMIT")),
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("BUSINESSINCOMELIMIT") != null)
                            {
                                propertyCoverages.Add(new PropertyCoverages
                                {
                                    CoverageName = "Business Income",
                                    CoverageValue = string.Format("{0}", data.Field<string>("BUSINESSINCOMELIMIT")),
                                    CoverageType = ""
                                });
                            }
                            if (data.Field<string>("OTHERLIMIT") != null)
                            {
                                propertyCoverages.Add(new PropertyCoverages
                                {
                                    CoverageName = "Others",
                                    CoverageValue = string.Format("{0}", data.Field<string>("OTHERLIMIT")),
                                    CoverageType = ""
                                });
                            }


                        });
                        if (propertyCoverages != null)
                        {
                            submissionData.AutoCoverages = propertyCoverages;
                        }
                    }
                    break;
                case "sub_losses_auto":
                    {
                        DS = Database.Sub_Summary_Auto_Losses(clientId, email, subGuid);
                        submissionData.AutoLosses = DS.Tables[0].AsEnumerable()
                                    .Select(dataRow => new SubmissionLosses
                                    {
                                        Year = string.Format("{0}", dataRow.Field<string>("YEAR")),
                                        GrossAmount = string.Format("{0}", dataRow.Field<string>("GROSSINCURRED")),
                                        TotalNoOfClaims = string.Format("{0}", dataRow.Field<string>("CLAIMNUMBER")),
                                        NoOfOpenClaims = string.Format("{0}", dataRow.Field<string>("COUNTOFOPENCLAIMS"))
                                    }).ToList();
                    }
                    break;

            }
            return submissionData;
        }
    }
}
