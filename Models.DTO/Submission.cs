using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class InboxFilter
    {
        public int UserId { get; set; }
        public int UploadedUserID { get; set; }
        public int SubmissionId { get; set; }
        public string MessageId { get; set; }
        public string keyword { get; set; }
        public Nullable<DateTime> SubmissionFromDate { get; set; }
        public Nullable<DateTime> SubmissionToDate { get; set; }
        public int FileReceivedChanelId { get; set; }
        public int AddedById { get; set; }
    }
    public class Submission
    {
        public int SubmissionId { get; set; }
        public string? MessageId { get; set; }
        public string SubmissionDate { get; set; }
        public int FileReceivedChanelId { get; set; }
        public string? FileReceivedChanelName { get; set; }
        public string? AddedByName { get; set; }
        public Nullable<DateTime> AddedByDate { get; set; }
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
        public bool? ExtractionComplete { get; set; }
        public bool? Completeness { get; set; }
        public bool? RiskClearance { get; set; }

        public SubmissionData[]? SubmissionData { get; set; }
    }

    public class SubmissionData
    {
        public _Id _id { get; set; }
        public Metadata metaData { get; set; }
        public SubmissionFile[] Files { get; set; }
        public GL_Policy_Info_Schedule_Of_Hazards[] GL_Policy_Info_Schedule_Of_Hazards { get; set; }
        public GL_Policy_Info[] GL_Policy_Info { get; set; }
        public Property_Policy_Info_Blanket_Summary[] Property_Policy_Info_Blanket_Summary { get; set; }
        public Property_Policy_Info_Premises_Information[] Property_Policy_Info_Premises_Information { get; set; }
        public Property_Other_Info[] Property_Other_Info { get; set; }
        public Claim_Info[] Claim_Info { get; set; }
        public WC_Policy_Info_State_Rating_Worksheet[] WC_Policy_Info_State_Rating_Worksheet { get; set; }
        public WC_Policy_Info_Locations[] WC_Policy_Info_Locations { get; set; }
        public WC_Policy_Info_Individuals_Included_Or_Excluded[] WC_Policy_Info_Individuals_Included_Or_Excluded { get; set; }
        public WC_Policy_Info_Prior_Carrier_Information_Or_Loss_History[] WC_Policy_Info_Prior_Carrier_Information_Or_Loss_History { get; set; }
        public WC_Policy_Info_General_Information[] WC_Policy_Info_General_Information { get; set; }
        public Auto_Policy_Driver_Info[] Auto_Policy_Driver_Info { get; set; }
        public Auto_Policy_Vehicle_Info[] Auto_Policy_Vehicle_Info { get; set; }
        public Auto_Other_Info[] Auto_Other_Info { get; set; }
        public Umbrella_Policy_Info[] Umbrella_Policy_Info { get; set; }
        public Umbrella_Locations[] Umbrella_Locations { get; set; }
        public Umbrella_Underlying_Info[] Umbrella_Underlying_Info { get; set; }
        public Umbrella_Other_Info[] Umbrella_Other_Info { get; set; }
        public Crime_Policy_Info[] Crime_Policy_Info { get; set; }
        public Crime_Employee_Classification[] Crime_Employee_Classification { get; set; }
        public Crime_Other_Info[] Crime_Other_Info { get; set; }
        public Crime_Employee_Schedule[] Crime_Employee_Schedule { get; set; }
        public EB_Policy_Info[] EB_Policy_Info { get; set; }
        public EB_Premises_Info[] EB_Premises_Info { get; set; }
        public Account_Level_Info[] Account_Level_Info { get; set; }
    }

    public class _Id
    {
        public string oid { get; set; }
    }

    public class Metadata
    {
        public int submission_id { get; set; }
        public string CreatedAt { get; set; }
        public string clientId { get; set; }
        public int seqNbr { get; set; }
    }

    public class SubmissionFile
    {
        public string FileOriginalName { get; set; }
        public string FileStatusName { get; set; }
        public string IsMerged { get; set; }
        public string IsScanned { get; set; }
        public string IsOCRed { get; set; }
        public string StatusFlag { get; set; }
    }

    public class GL_Policy_Info_Schedule_Of_Hazards
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public string Generalliability_Hazard_Locationproduceridentifier { get; set; }
        public string Generalliability_Hazard_Hazardproduceridentifier { get; set; }
        public string Generalliability_Hazard_Classcode { get; set; }
        public string Generalliability_Hazard_Premiumbasiscode { get; set; }
        public string Generalliability_Hazard_Exposure { get; set; }
        public string Generalliability_Hazard_Territorycode { get; set; }
        public string Generalliability_Hazard_Premisesoperationsrate { get; set; }
        public string Generalliability_Hazard_Productsrate { get; set; }
        public string Generalliability_Hazard_Premisesoperationspremiumamount { get; set; }
        public string Generalliability_Hazard_Productspremiumamount { get; set; }
        public string Generalliability_Hazard_Classification { get; set; }
        public string recordSeq { get; set; }
    }

    public class GL_Policy_Info
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public Coverages_And_Limits Coverages_And_Limits { get; set; }
        public Claims_Made Claims_Made { get; set; }
        public Employee_Benefits_Liability Employee_Benefits_Liability { get; set; }
        public GL_Other_Info GL_Other_Info { get; set; }
    }

    public class Coverages_And_Limits
    {
        public string Generalliability_Coverageindicator { get; set; }
        public string Generalliability_Claimsmadeindicator { get; set; }
        public string Generalliability_Occurrenceindicator { get; set; }
        public string Generalliability_Ownersandcontractorsprotectiveindicator { get; set; }
        public string Generalliability_Othercoverageindicator { get; set; }
        public string Generalliability_Othercoveragedescription { get; set; }
        public string Generalliability_Propertydamage_Deductibleindicator { get; set; }
        public string Generalliability_Propertydamage_Deductibleamount { get; set; }
        public string Generalliability_Bodilyinjury_Deductibleindicator { get; set; }
        public string Generalliability_Bodilyinjury_Deductibleamount { get; set; }
        public string Generalliability_Otherdeductibleindicator { get; set; }
        public string Generalliability_Otherdeductibledescription { get; set; }
        public string Generalliability_Otherdeductibleamount { get; set; }
        public string Generalliability_Deductibleperclaimindicator { get; set; }
        public string Generalliability_Deductibleperoccurrenceindicator { get; set; }
        public string Generalliability_Generalaggregate_Limitamount { get; set; }
        public string Generalliability_Generalaggregate_Limitappliesperpolicyindicator { get; set; }
        public string Generalliability_Generalaggregate_Limitappliesperprojectindicator { get; set; }
        public string Generalliability_Generalaggregate_Limitappliesperlocationindicator { get; set; }
        public string Generalliability_Generalaggregate_Limitappliestootherindicator { get; set; }
        public string Generalliability_Generalaggregate_Limitappliestocode { get; set; }
        public string Generalliability_Productsandcompletedoperations_Aggregatelimitamount { get; set; }
        public string Generalliability_Personalandadvertisinginjury_Limitamount { get; set; }
        public string Generalliability_Eachoccurrence_Limitamount { get; set; }
        public string Generalliability_Firedamagerentedpremises_Eachoccurrencelimitamount { get; set; }
        public string Generalliability_Medicalexpense_Eachpersonlimitamount { get; set; }
        public string Generalliability_Employeebenefits_Limitamount { get; set; }
        public string Generalliability_Othercoveragelimitdescription { get; set; }
        public string Generalliability_Othercoveragelimitamount { get; set; }
        public string Generalliability_Premisesoperations_Premiumamount { get; set; }
        public string Generalliability_Products_Premiumamount { get; set; }
        public string Generalliability_Othercoveragepremiumamount { get; set; }
        public string Generalliabilitylineofbusiness_Totalpremiumamount { get; set; }
        public string Generalliabilitylineofbusiness_Remarktext { get; set; }
        public string Generalliability_Uninsuredunderinsuredmotorists_Coverageavailableindicator { get; set; }
        public string Generalliability_Uninsuredunderinsuredmotorists_Coverageavailablenoindicator { get; set; }
        public string Generalliability_Medicalpayments_Coverageavailableindicator { get; set; }
        public string Generalliability_Medicalpayments_Coverageavailablenoindicator { get; set; }
    }

    public class Claims_Made
    {
        public string Generalliability_Claimsmade_Proposedretroactivedate { get; set; }
        public string Generalliability_Claimsmade_Uninterruptedcoverageentrydate { get; set; }
        public string Generalliabilitylineofbusiness_Question_Aahcode { get; set; }
        public string Generalliabilitylineofbusiness_Anyproductworkaccidentlocationexcludeduninsuredpreviouscoverageexplanation { get; set; }
        public string Generalliabilitylineofbusiness_Question_Aaicode { get; set; }
        public string Generalliabilitylineofbusiness_Tailcoveragepurchasedpreviouspolicyexplanation { get; set; }
    }

    public class Employee_Benefits_Liability
    {
        public string Generalliability_Employeebenefits_Perclaimdeductibleamount { get; set; }
        public string Generalliability_Employeebenefits_Employeecoveredcount { get; set; }
        public string Generalliability_Employeebenefits_Employeecount { get; set; }
        public string Generalliability_Employeebenefits_Retroactivedate { get; set; }
    }

    public class GL_Other_Info
    {
        public string Generalliabilitylineofbusiness_Question_Abjcode { get; set; }
        public string Generalliabilitylineofbusiness_Anymedicalfacilitiesprovidedmedicalprofessionalsemployedexplanation { get; set; }
        public string Generalliabilitylineofbusiness_Question_Acacode { get; set; }
        public string Generalliabilitylineofbusiness_Anyexposureradioactivematerialsexplanation { get; set; }
        public string Generalliabilitylineofbusiness_Question_Acjcode { get; set; }
        public string Generalliabilitylineofbusiness_Operationsinvolvestoringdisposingtransportinghazardousmaterialexplanation { get; set; }
        public string Generalliabilitylineofbusiness_Question_Acbcode { get; set; }
        public string Generalliabilitylineofbusiness_Anylistedoperationssoldacquiredlastfiveyearsexplanation { get; set; }
        public string Generalliabilitylineofbusiness_Question_Acccode { get; set; }
        public string Generalliabilitylineofbusiness_Machineryorequipmentloanedrentedothersexplanation { get; set; }
        public string Commercialinlandmarineproperty_Propertysubclass_Smalltoolsindicator { get; set; }
        public string Commercialinlandmarineproperty_Propertysubclass_Largeequipmentindicator { get; set; }
        public string Propertyitem_Itemdetail_Instructiongivencode { get; set; }
        public string Contractors_Question_Aahcode { get; set; }
        public string Generalliabilitylineofbusiness_Watercraftdocksfloatsownedhiredleasedexplanation { get; set; }
        public string Generalliabilitylineofbusiness_Question_Acdcode { get; set; }
        public string Generalliabilitylineofbusiness_Anyparkingfacilitiesownedrentedexplanation { get; set; }
        public string Generalliabilitylineofbusiness_Question_Kagcode { get; set; }
        public string Generalliabilitylineofbusiness_Feechargedforparkingexplanation { get; set; }
        public string Generalliabilitylineofbusiness_Question_Acecode { get; set; }
        public string Generalliabilitylineofbusiness_Recreationalfacilitiesprovidedexplanation { get; set; }
        public string Generalliabilitylineofbusiness_Question_Kaacode { get; set; }
        public string Buildingoccupancy_Apartmentcount { get; set; }
        public string Buildingoccupancy_Apartmentarea { get; set; }
        public string Generalliability_Otherlodgingoperationsdescription { get; set; }
        public string Generalliabilitylineofbusiness_Isthereaswimmingpoolonthepremises_YesOrNo_Code { get; set; }
        public string Swimmingpool_Approvedfenceindicator { get; set; }
        public string Swimmingpool_Limitedaccessindicator { get; set; }
        public string Swimmingpool_Divingboardindicator { get; set; }
        public string Swimmingpool_Slideindicator { get; set; }
        public string Swimmingpool_Abovegroundindicator { get; set; }
        public string Swimmingpool_Ingroundindicator { get; set; }
        public string Swimmingpool_Lifeguardindicator { get; set; }
        public string Generalliabilitylineofbusiness_Question_Acfcode { get; set; }
        public string Generalliabilitylineofbusiness_Sportingorsocialeventssponsoredexplanation { get; set; }
        public string Generalliabilitylineofbusiness_Question_Kabcode { get; set; }
        public string Athleticteam_Sportdescription { get; set; }
        public string Athleticteam_Agegroup_Thirteenthrougheighteenindicator { get; set; }
        public string Athleticteam_Contactsportcode { get; set; }
        public string Athleticteam_Agegroup_Twelveandunderindicator { get; set; }
        public string Athleticteam_Agegroup_Overeighteenindicator { get; set; }
        public string Athleticteam_Sponsorshipextentdescription { get; set; }
        public string Generalliabilitylineofbusiness_Question_Acgcode { get; set; }
        public string Generalliabilitylineofbusiness_Structuralalterationscontemplatedexplanation { get; set; }
        public string Generalliabilitylineofbusiness_Question_Achcode { get; set; }
        public string Generalliabilitylineofbusiness_Demolitionexposurecontemplatedexplanation { get; set; }
        public string Generalliabilitylineofbusiness_Question_Aabcode { get; set; }
        public string Generalliabilitylineofbusiness_Applicantactivejointventuresexplanation { get; set; }
        public string Generalliabilitylineofbusiness_Question_Aaccode { get; set; }
        public string Additionalinterest_Fullname { get; set; }
        public string Additionalinterest_Workerscompensationcarriedcode { get; set; }
        public string Generalliabilitylineofbusiness_Question_Aadcode { get; set; }
        public string Generalliabilitylineofbusiness_Labourinterchangeotherbusinesssubsidiariesexplanation { get; set; }
        public string Generalliabilitylineofbusiness_Question_Aaecode { get; set; }
        public string Generalliabilitylineofbusiness_Daycarefacilitiesoperatedexplanation { get; set; }
        public string Generalliabilitylineofbusiness_Question_Aafcode { get; set; }
        public string Generalliabilitylineofbusiness_Crimesoccurredorattemptedlastthreeyearsexplanation { get; set; }
        public string Contractors_Question_Aagcode { get; set; }
        public string Generalliabilitylineofbusiness_Formalsafetysecuritypolicyineffectexplanation { get; set; }
        public string Generalliabilitylineofbusiness_Question_Aagcode { get; set; }
        public string Generalliabilitylineofbusiness_Businesspromotionalliteraturesafetyrepresentationsexplanation { get; set; }
        public string Generalliabilitylineofbusiness_Attachment_Additionalinterestindicator { get; set; }
        public string Additionalinterest_Interestrank { get; set; }
        public string Additionalinterest_Certificaterequiredindicator { get; set; }
        public string Additionalinterest_Interest_Additionalinsuredindicator { get; set; }
        public string Additionalinterest_Mailingaddress_Lineone { get; set; }
        public string Additionalinterest_Item_Locationproduceridentifier { get; set; }
        public string Additionalinterest_Item_Buildingproduceridentifier { get; set; }
        public string Additionalinterest_Interest_Employeeaslessorindicator { get; set; }
        public string Additionalinterest_Item_Scheduleditemclasscode { get; set; }
        public string Additionalinterest_Item_Scheduleditemproduceridentifier { get; set; }
        public string Additionalinterest_Interest_Lienholderindicator { get; set; }
        public string Additionalinterest_Itemdescription { get; set; }
        public string Additionalinterest_Interest_Losspayeeindicator { get; set; }
        public string Additionalinterest_Interest_Mortgageeindicator { get; set; }
        public string Additionalinterest_Interest_Otherindicator { get; set; }
        public string Additionalinterest_Interest_Otherdescription { get; set; }
        public string Additionalinterest_Accountnumberidentifier { get; set; }
    }

    public class Property_Policy_Info_Blanket_Summary
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public string Commercialproperty_Summary_Blanketnumberidentifier { get; set; }
        public string Commercialcoverage_Summary_Blankettypedescription { get; set; }
        public string Commercialproperty_Summary_Blanketlimitamount { get; set; }
        public string recordSeq { get; set; }
    }

    public class Property_Policy_Info_Premises_Information
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public string Commercialstructure_Location_Produceridentifier { get; set; }
        public string Commercialstructure_Building_Produceridentifier { get; set; }
        public string Commercialstructure_Physicaladdress_Lineone { get; set; }
        public string Buildingoccupancy_Otheroccupanciesdescription { get; set; }
        public string Commercialstructure_Building_Sublocationdescription { get; set; }
        public string Construction_Constructioncode { get; set; }
        public string Construction_Buildingarea { get; set; }
        public string Construction_Roofmaterialcode { get; set; }
        public string Commercialstructure_Builtyear { get; set; }
        public string Buildingfireprotection_Protectionclasscode { get; set; }
        public string Construction_Buildingcodeeffectivenessgradecode { get; set; }
        public string Buildingfireprotection_Alarm_Sprinklerpercent { get; set; }
        public string Construction_Storeycount { get; set; }
        public string Commercialproperty_Premises_Subjectofinsurancecode { get; set; }
        public string Commercialproperty_Premises_Limitamount { get; set; }
        public string Commercialproperty_Premises_Coinsurancepercent { get; set; }
        public string Commercialproperty_Premises_Valuationcode { get; set; }
        public string Commercialproperty_Premises_Causeoflosscode { get; set; }
        public string Commercialproperty_Premises_Causeoflosscode_1_Default { get; set; }
        public string Commercialproperty_Premises_Inflationguardpercent { get; set; }
        public string Commercialproperty_Premises_Deductibleamount { get; set; }
        public string Commercialproperty_Premises_Deductibleamount_1_Default { get; set; }
        public string Commercialproperty_Premises_Deductibletypecode { get; set; }
        public string Commercialproperty_Premises_Blanketnumber { get; set; }
        public string Commercialproperty_Premises_Formsandconditions { get; set; }
        public string Commercialproperty_Attachment_Businessincomeextraexpenseindicator { get; set; }
        public string Commercialproperty_Attachment_Valuereportingindicator { get; set; }
        public string Commercialproperty_Spoilage_Propertydescription { get; set; }
        public string Commercialproperty_Spoilage_Limitamount { get; set; }
        public string Commercialproperty_Premises_Breakdownorcontaminationindicator { get; set; }
        public string Commercialproperty_Spoilage_Yesnocode { get; set; }
        public string Commercialproperty_Spoilage_Deductibleamount { get; set; }
        public string Commercialproperty_Spoilage_Refrigeratormaintenancecode { get; set; }
        public string Commercialproperty_Premises_Poweroutageindicator { get; set; }
        public string Commercialproperty_Premises_Sellingpriceindicator { get; set; }
        public string Commercialproperty_Premises_Otherindicator { get; set; }
        public string Commercialproperty_Premises_Optionsdescription { get; set; }
        public string Commercialpropertycoverage_Sinkholecollapse_Yesindicator { get; set; }
        public string Commercialpropertycoverage_Sinkholecollapse_Noindicator { get; set; }
        public string Commercialpropertycoverage_Sinkholecollapse_Limitamount { get; set; }
        public string Commercialpropertycoverage_Minesubsidence_Yesindicator { get; set; }
        public string Commercialpropertycoverage_Minesubsidenceoption_Noindicator { get; set; }
        public string Commercialpropertycoverage_Minesubsidence_Limitamount { get; set; }
        public string Buildingfeatures_Historicalpropertyindicator { get; set; }
        public string Construction_Opensidescount { get; set; }
        public string Commercialproperty_Premises_Remarktext { get; set; }
        public string Construction_Constructioncode_1_Default { get; set; }
        public string Buildingfireprotection_Hydrantdistancefeetcount { get; set; }
        public string Buildingfireprotection_Hydrantdistancefeetcount_1_Default { get; set; }
        public string Buildingfireprotection_Firestationdistancemilecount { get; set; }
        public string Buildingfireprotection_Firestationdistancemilecount_1_Default { get; set; }
        public string Buildingfireprotection_Firedistrictname { get; set; }
        public string Buildingfireprotection_Firedistrictcode { get; set; }
        public string Construction_Storycount_1_Default { get; set; }
        public string Construction_Basementcount { get; set; }
        public string Commercialstructure_Builtyear_1_Default { get; set; }
        public string Buildingimprovement_Wiringindicator { get; set; }
        public string Buildingimprovement_Wiringyear { get; set; }
        public string Buildingimprovement_Plumbingindicator { get; set; }
        public string Buildingimprovement_Plumbingyear { get; set; }
        public string Commercialstructure_Taxcode { get; set; }
        public string Construction_Roofmaterialcode_1_Default { get; set; }
        public string Buildingimprovement_Roofingindicator { get; set; }
        public string Buildingimprovement_Roofingyear { get; set; }
        public string Buildingimprovement_Heatingindicator { get; set; }
        public string Buildingimprovement_Heatingyear { get; set; }
        public string Commercialstructure_Windclass_Semiresistiveindicator { get; set; }
        public string Buildingfeatures_Solidfuelheaterindicator { get; set; }
        public string Buildingfeatures_Solidfuelheaterinstallationdate { get; set; }
        public string Buildingimprovement_Otherindicator { get; set; }
        public string Buildingimprovement_Otherdescription { get; set; }
        public string Buildingimprovement_Otheryear { get; set; }
        public string Commercialstructure_Windclass_Resistiveindicator { get; set; }
        public string Commercialstructure_Windclass_Otherindicator { get; set; }
        public string Commercialstructure_Windclass_Otherdescription { get; set; }
        public string Buildingfeatures_Solidfuelheatermanufacturername { get; set; }
        public string Commercialstructure_Primaryheat_Boilerindicator { get; set; }
        public string Commercialstructure_Primaryheat_Solidfuelindicator { get; set; }
        public string Commercialstructure_Primaryheat_Otherindicator { get; set; }
        public string Commercialstructure_Primaryheat_Otherdescription { get; set; }
        public string Commercialstructure_Secondaryheat_Boilerindicator { get; set; }
        public string Commercialstructure_Secondaryheat_Solidfuelindicator { get; set; }
        public string Commercialstructure_Secondaryheat_Otherindicator { get; set; }
        public string Commercialstructure_Secondaryheat_Otherdescription { get; set; }
        public string Commercialstructure_Heatingboilerinsuredelsewherecode { get; set; }
        public string Buildingexposure_Rightdescription { get; set; }
        public string Buildingexposure_Rightdistance { get; set; }
        public string Buildingexposure_Leftdescription { get; set; }
        public string Buildingexposure_Leftdistance { get; set; }
        public string Buildingexposure_Frontdescription { get; set; }
        public string Buildingexposure_Frontdistance { get; set; }
        public string Buildingexposure_Reardescription { get; set; }
        public string Buildingexposure_Reardistance { get; set; }
        public string Alarm_Burglar_Alarmdescription { get; set; }
        public string Alarm_Burglar_Alarmdescription_1_Default { get; set; }
        public string Alarm_Burglar_Certificateidentifier { get; set; }
        public string Alarm_Burglar_Certificateexpirationdate { get; set; }
        public string Alarm_Burglar_Centralstationindicator { get; set; }
        public string Burglar_Localgongindicator { get; set; }
        public string Alarm_Burglar_Withkeysindicator { get; set; }
        public string Alarm_Burglar_Installedandservicedbyname { get; set; }
        public string Alarm_Burglar_Protectionextentcode { get; set; }
        public string Alarm_Burglar_Gradecode { get; set; }
        public string Buildingsecurity_Guardwatchmencount { get; set; }
        public string Buildingsecurity_Guardwatchmenclockhourlyindicator { get; set; }
        public string Buildingsecurity_Guardwatchmenotherindicator { get; set; }
        public string Buildingsecurity_Guardwatchmenotherdescription { get; set; }
        public string Buildingfireprotection_Alarm_Protectiondescription { get; set; }
        public string Buildingfireprotection_Alarm_Protectiondescription_1_Default { get; set; }
        public string Buildingfireprotection_Alarm_Sprinklerpercent_1_Default { get; set; }
        public string Buildingfireprotection_Alarm_Manufacturername { get; set; }
        public string Buildingfireprotection_Alarm_Centralstationindicator { get; set; }
        public string Buildingfireprotection_Alarm_Localgongindicator { get; set; }
        public string Generalliabilitylineofbusiness_Attachment_Additionalinterestindicator { get; set; }
        public string Commercialstructure_Physicaladdress_Stateorprovincecode { get; set; }
        public string Commercialstructure_Physicaladdress_Cityname { get; set; }
        public string Commercialstructure_Physicaladdress_Postalcode { get; set; }
        public string recordSeq { get; set; }
    }

    public class Property_Other_Info
    {
        public string StatementOfValues_Effective_Date { get; set; }
        public string StatementOfValues_Expiration_Date { get; set; }
        public string StatementOfValues_Policy_Number { get; set; }
        public string StatementOfValues_Location_Number { get; set; }
        public string StatementOfValues_Building_Number { get; set; }
        public string Occupancy_Description { get; set; }
        public string StatementOfValues_Location_Address { get; set; }
        public string StatementOfValues_Location_City { get; set; }
        public string StatementOfValues_Location_State { get; set; }
        public string StatementOfValues_Location_Zipcode { get; set; }
        public string StatementOfValues_Insured_Name { get; set; }
        public string Building_Description { get; set; }
        public string StatementOfValues_Year_Built { get; set; }
        public string StatementOfValues_Number_Of_Stories { get; set; }
        public string StatementOfValues_Improvement_Year_Roof { get; set; }
        public string StatementOfValues_Premises_ClassCode { get; set; }
        public string StatementOfValues_Premises_ClassCodeDescription { get; set; }
        public string Premises_BI_Limit { get; set; }
        public string Premises_BPP_Limit { get; set; }
        public string Total_Insured_Value { get; set; }
        public string Number_Of_Stairs { get; set; }
        public string Construction_Type { get; set; }
        public string Square_Footage { get; set; }
        public string Building_Condition { get; set; }
        public string CommercialStructure_StructureType { get; set; }
        public string Lot_Size { get; set; }
        public string Wall_Type { get; set; }
        public string Floor_Type { get; set; }
        public string Roof_Type_Description { get; set; }
        public string Protection_Class_Code { get; set; }
        public string Protection_Class_Description { get; set; }
        public string Hazard_Grade_Risk { get; set; }
        public string Roof_Geometry { get; set; }
        public string Sprinkler_Type { get; set; }
        public string Occupancy_Percentage { get; set; }
        public string StatementOfValues_Premises_RateOrLossCost { get; set; }
        public string Contents_Value { get; set; }
        public string Stock_Limit { get; set; }
        public string Real_Property { get; set; }
        public string Location_County { get; set; }
        public string Tier1_Wind { get; set; }
        public string Building_Limit { get; set; }
        public string Contents_Limit { get; set; }
        public string BI_Limit { get; set; }
        public string Other_Limit { get; set; }
        public string Other_Limit_Description { get; set; }
        public string StatementOfValues_TotalPremiumAmount { get; set; }
        public string Surcharges { get; set; }
        public string CommercialProperty_Premises_PremiumAmount { get; set; }
        public string CommercialPropertyCoverage_Flood_LimitAmount { get; set; }
        public string CommercialPropertyCoverage_EarthMovement_LimitAmount { get; set; }
        public string Cripple_Walls { get; set; }
        public string Earthquake_Zone { get; set; }
        public string Equipment_EQ_Bracing { get; set; }
        public string Number_Of_Buildings { get; set; }
        public string Roof_Anchor { get; set; }
        public string Roof_Covering { get; set; }
        public string Roof_Equipment_Hurricane_Bracking { get; set; }
        public string Wind_Hail_Zone { get; set; }
        public string Building_Sprinklered { get; set; }
        public string recordSeq { get; set; }
    }

    public class Claim_Info
    {
        public string Policy_Nbr { get; set; }
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Line_Of_Business { get; set; }
        public string Claim_Nbr { get; set; }
        public string Claim_Occurence_Nbr { get; set; }
        public string Insured_Nm { get; set; }
        public string Agency_Nm { get; set; }
        public string Claim_Type { get; set; }
        public string Claim_Status { get; set; }
        public string Claimant_Nm { get; set; }
        public string Driver_Nm { get; set; }
        public string Loss_Amount { get; set; }
        public string Loss_Desc { get; set; }
        public string Cause_Of_Loss { get; set; }
        public string Body_Part { get; set; }
        public string Nature_Of_Injury { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Loss_Date { get; set; }
        public string Reported_Date { get; set; }
        public string Lag_Days { get; set; }
        public string Paid_Loss { get; set; }
        public string Paid_Alae { get; set; }
        public string Reserve_Alae { get; set; }
        public string Total_Paid { get; set; }
        public string Total_Incurred { get; set; }
        public string Net_Of_Deductible { get; set; }
        public string Valuation_Date { get; set; }
        public string Closed_Date { get; set; }
        public string Recoveries { get; set; }
        public string Litigation_Status { get; set; }
        public string Gross_Reserved { get; set; }
        public string Gross_Incurred { get; set; }
        public string Gross_Total_Incurred { get; set; }
        public string Net_Reserved { get; set; }
        public string Net_Total_Incurred { get; set; }
        public string Bi_Incurred { get; set; }
        public string Pd_Paid { get; set; }
        public string Pd_Reserves { get; set; }
        public string Bi_Paid { get; set; }
        public string Bi_Reserves { get; set; }
        public string Carrier_Nm { get; set; }
        public string recordSeq { get; set; }
    }

    public class WC_Policy_Info_State_Rating_Worksheet
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public string Workerscompensationemployersliability_Employersliability_Eachaccidentlimitamount { get; set; }
        public string Workerscompensationemployersliability_Employersliability_Diseasepolicylimitamount { get; set; }
        public string Workerscompensationemployersliability_Employersliability_Diseaseeachemployeelimitamount { get; set; }
        public string Workerscompensationstatecoverage_Terrorism_Premiumamount { get; set; }
        public string Workerscompensationstatecoverage_Catastrophe_Premiumamount { get; set; }
        public string Workerscompensation_Deductibletype_Medicalindicator { get; set; }
        public string Workerscompensation_Deductibletype_Indemnityindicator { get; set; }
        public string Workerscompensation_Deductibletype_Otherindicator { get; set; }
        public string Workerscompensation_Deductibletype_Otherdescription { get; set; }
        public string Workerscompensation_Deductibleamount { get; set; }
        public string Workerscompensation_Coverage_Uslhindicator { get; set; }
        public string Workerscompensation_Coverage_Voluntarycompensationindicator { get; set; }
        public string Workerscompensation_Coverage_Foreigncoverageindicator { get; set; }
        public string Workerscompensation_Coverage_Managedcareoptionindicator { get; set; }
        public string Workerscompensation_Coverage_Otherindicator { get; set; }
        public string Workerscompensation_Coverage_Otherdescription { get; set; }
        public string Workerscompensationstatecoverage_Expenseconstant_Premiumamount { get; set; }
        public string Workerscompensationlineofbusiness_Totalestimatedannualpremiumallstatesamount { get; set; }
        public string Workerscompensation_Rateclass_Classificationcode { get; set; }
        public string Workerscompensation_Rateclass_Descriptioncode { get; set; }
        public string Workerscompensation_Rateclass_Remunerationamount { get; set; }
        public string Workerscompensation_Ratestate_Stateorprovincename { get; set; }
        public string Workerscompensationstatecoverage_Totalfactoredpremiumamount { get; set; }
        public string Workerscompensation_Rateclass_Fulltimeemployeecount { get; set; }
        public string Workerscompensation_Rateclass_Parttimeemployeecount { get; set; }
        public string Workerscompensation_Ratestate_Pagenumber { get; set; }
        public string Workerscompensation_Ratestate_Totalpagenumber { get; set; }
        public string Workerscompensation_Rateclass_Locationproduceridentifier { get; set; }
        public string Workerscompensation_Rateclass_Dutiesdescription { get; set; }
        public string Workerscompensation_Rateclass_Siccode { get; set; }
        public string Workerscompensation_Rateclass_Naicscode { get; set; }
        public string Workerscompensation_Rateclass_Rate { get; set; }
        public string Workerscompensation_Rateclass_Estimatedmanualpremiumamount { get; set; }
        public string recordSeq { get; set; }
    }

    public class WC_Policy_Info_Locations
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public string Location_Produceridentifier { get; set; }
        public string Location_Highestfloorcount { get; set; }
        public string Location_Physicaladdress_Lineone { get; set; }
        public string Location_Physicaladdress_CityName { get; set; }
        public string Location_Physicaladdress_StateOrProvinceCode { get; set; }
        public string Location_Physicaladdress_PostalCode { get; set; }
        public string recordSeq { get; set; }
    }

    public class WC_Policy_Info_Individuals_Included_Or_Excluded
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public string Workerscompensation_Individual_Stateorprovincecode { get; set; }
        public string Workerscompensation_Individual_Locationproduceridentifier { get; set; }
        public string Workerscompensation_Individual_Fullname { get; set; }
        public string Workerscompensation_Individual_Birthdate { get; set; }
        public string Workerscompensation_Individual_Titlerelationshipcode { get; set; }
        public string Workerscompensation_Individual_Ownershippercent { get; set; }
        public string Workerscompensation_Individual_Dutiesdescription { get; set; }
        public string Workerscompensation_Individual_Includedexcludedcode { get; set; }
        public string Workerscompensation_Individual_Remunerationamount { get; set; }
        public string recordSeq { get; set; }
    }

    public class WC_Policy_Info_Prior_Carrier_Information_Or_Loss_History
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public string Priorcoverage_Effectiveyear { get; set; }
        public string Priorcoverage_Insurerfullname { get; set; }
        public string Priorcoverage_Policynumberidentifier { get; set; }
        public string Priorcoverage_Totalpremiumamount { get; set; }
        public string Priorcoverage_Modificationfactor { get; set; }
        public string Losshistory_Claimcount { get; set; }
        public string Losshistory_Paidamount { get; set; }
        public string Losshistory_Reservedamount { get; set; }
        public string recordSeq { get; set; }
    }

    public class WC_Policy_Info_General_Information
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public string Workerscompensationlineofbusiness_Question_Aaecode { get; set; }
        public string Workerscompensationlineofbusiness_Anyemployeesundersixteenoroversixtyyearsexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Abecode { get; set; }
        public string Workerscompensationlineofbusiness_Applicantownleaseaircraftorwatercraftexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Aabcode { get; set; }
        public string Workerscompensationlineofbusiness_Athleticteamssponsoredexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Abfcode { get; set; }
        public string Workerscompensationlineofbusiness_Employeehealthplansprovidedexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Aadcode { get; set; }
        public string Workerscompensationlineofbusiness_Workperformedundergroundorabovefifteenfeetexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Abicode { get; set; }
        public string Workerscompensationlineofbusiness_Anygrouptransportationprovidedexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Abjcode { get; set; }
        public string Workerscompensationlineofbusiness_Employeeswithphysicalhandicapsexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Acdcode { get; set; }
        public string Workerscompensationlineofbusiness_Pastpresentdiscontinuedoperationshazardousmaterialexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Kawcode { get; set; }
        public string Workerscompensationlineofbusiness_Anyemployeesperformworkforotherbusinessesexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Aagcode { get; set; }
        public string Workerscompensationlineofbusiness_Leaseemployeestoorfromotheremployersexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Kascode { get; set; }
        public string Workerscompensationlineofbusiness_Applicantengagedanyothertypeofbusinessexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Abacode { get; set; }
        public string Workerscompensationlineofbusiness_Otherinsurancewiththisinsurerexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Abhcode { get; set; }
        public string Workerscompensationlineofbusiness_Employeestraveloutofstateexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Karcode { get; set; }
        public string Workerscompensationlineofbusiness_Workperformedonvesselsdocksbridgesexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Acbcode { get; set; }
        public string Workerscompensationlineofbusiness_Physicalsrequiredafteroffersofemploymentaremadeexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Aaicode { get; set; }
        public string Workerscompensationlineofbusiness_Priorcoveragedeclinedcancellednonrenewedlastthreeyearsexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Abccode { get; set; }
        public string Workerscompensationlineofbusiness_Writtensafetyprograminoperationexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Kavcode { get; set; }
        public string Workerscompensationlineofbusiness_Anyseasonalemployeesexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Katcode { get; set; }
        public string Workerscompensationlineofbusiness_Subcontractorsusedexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Kaxcode { get; set; }
        public string Workerscompensationlineofbusiness_Taxliensorbankruptcywithinlastfiveyearsexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Kaycode { get; set; }
        public string Workerscompensationlineofbusiness_Undisputedunpaidworkerscompensationpremiumdueexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Aafcode { get; set; }
        public string Workerscompensationlineofbusiness_Volunteerordonatedlabourexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Abgcode { get; set; }
        public string Workerscompensationlineofbusiness_Question_Athomeemployeecount { get; set; }
        public string Workerscompensationlineofbusiness_Employeespredominantlyworkathomeexplanation { get; set; }
        public string Workerscompensationlineofbusiness_Question_Kaucode { get; set; }
        public string Workerscompensationlineofbusiness_Anyworksubletwithoutcertificatesofinsuranceexplanation { get; set; }
    }

    public class Auto_Policy_Driver_Info
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public string Driver_Produceridentifier { get; set; }
        public string Driver_Givenname { get; set; }
        public string Driver_Othergivennameinitial { get; set; }
        public string Driver_Surname { get; set; }
        public string Driver_Mailingaddress_Cityname { get; set; }
        public string Driver_Mailingaddress_Stateorprovincecode { get; set; }
        public string Driver_Mailingaddress_Postalcode { get; set; }
        public string Driver_Gendercode { get; set; }
        public string Driver_Maritalstatuscode { get; set; }
        public string Driver_Birthdate { get; set; }
        public string Driver_Experienceyearcount { get; set; }
        public string Driver_Licensedyear { get; set; }
        public string Driver_Licensenumberidentifier { get; set; }
        public string Driver_Taxidentifier { get; set; }
        public string Driver_Licensedstateorprovincecode { get; set; }
        public string Driver_Hireddate { get; set; }
        public string Driver_Coverage_Broadenednofaultcode { get; set; }
        public string Driver_Coverage_Driverothercarcode { get; set; }
        public string Driver_Vehicle_Produceridentifier { get; set; }
        public string Driver_Vehicle_Usepercent { get; set; }
        public string recordSeq { get; set; }
    }

    public class Auto_Policy_Vehicle_Info
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public string Vehicle_Produceridentifier { get; set; }
        public string Vehicle_Modelyear { get; set; }
        public string Vehicle_Manufacturersname { get; set; }
        public string Vehicle_Modelname { get; set; }
        public string Vehicle_Bodycode { get; set; }
        public string Vehicle_Vinidentifier { get; set; }
        public string Vehicle_Vehicletype_Privatepassengerindicator { get; set; }
        public string Vehicle_Vehicletype_Specialindicator { get; set; }
        public string Vehicle_Vehicletype_Commercialindicator { get; set; }
        public string Vehicle_Symbolcode { get; set; }
        public string Vehicle_Comprehensivesymbolcode { get; set; }
        public string Vehicle_Collisionsymbolcode { get; set; }
        public string Vehicle_Physicaladdress_Lineone { get; set; }
        public string Vehicle_Physicaladdress_Cityname { get; set; }
        public string Vehicle_Physicaladdress_Countyname { get; set; }
        public string Vehicle_Physicaladdress_Stateorprovincecode { get; set; }
        public string Vehicle_Physicaladdress_Postalcode { get; set; }
        public string Vehicle_Registration_Stateorprovincecode { get; set; }
        public string Vehicle_Ratingterritorycode { get; set; }
        public string Vehicle_Grossvehicleweight { get; set; }
        public string Vehicle_Rateclasscode { get; set; }
        public string Vehicle_Specialindustryclasscode { get; set; }
        public string Vehicle_Primaryliabilityratingfactor { get; set; }
        public string Vehicle_Seatingcapacitycount { get; set; }
        public string Vehicle_Radiusofuse { get; set; }
        public string Vehicle_Farthestzonecode { get; set; }
        public string Vehicle_Costnewamount { get; set; }
        public string Vehicle_Use_Pleasureindicator { get; set; }
        public string Vehicle_Use_Farmindicator { get; set; }
        public string Vehicle_Use_Commercialindicator { get; set; }
        public string Vehicle_Use_Retailindicator { get; set; }
        public string Vehicle_Use_Serviceindicator { get; set; }
        public string Vehicle_Use_Forhireindicator { get; set; }
        public string Vehicle_Use_Otherindicator { get; set; }
        public string Vehicle_Use_Otherdescription { get; set; }
        public string Vehicle_Coverage_Liabilityindicator { get; set; }
        public string Vehicle_Coverage_Nofaultindicator { get; set; }
        public string Vehicle_Coverage_Additionalnofaultindicator { get; set; }
        public string Vehicle_Coverage_Medicalpaymentsindicator { get; set; }
        public string Vehicle_Coverage_Uninsuredmotoristsindicator { get; set; }
        public string Vehicle_Coverage_Underinsuredmotoristsindicator { get; set; }
        public string Vehicle_Coverage_Towingandlabourindicator { get; set; }
        public string Vehicle_Coverage_Specifiedcauseoflossindicator { get; set; }
        public string Vehicle_Coverage_Fireindicator { get; set; }
        public string Vehicle_Coverage_Firetheftindicator { get; set; }
        public string Vehicle_Coverage_Firetheftwindstormindicator { get; set; }
        public string Vehicle_Coverage_Limitedspecifiedperilsindicator { get; set; }
        public string Vehicle_Coverage_Comprehensiveindicator { get; set; }
        public string Vehicle_Coverage_Collisionindicator { get; set; }
        public string Vehicle_Coverage_Rentalreimbursementindicator { get; set; }
        public string Vehicle_Coverage_Fullglassindicator { get; set; }
        public string Vehicle_Coverage_Otherindicator { get; set; }
        public string Vehicle_Coverage_Otherdescription { get; set; }
        public string Vehicle_Coverage_Valuationactualcashvalueindicator { get; set; }
        public string Vehicle_Coverage_Valuationagreedamountindicator { get; set; }
        public string Vehicle_Coverage_Valuationstatedamountindicator { get; set; }
        public string Vehicle_Coverage_Agreedorstatedamount { get; set; }
        public string Vehicle_Coverage_Comprehensivedeductibleindicator { get; set; }
        public string Vehicle_Coverage_Specifiedcauseoflossdeductibleindicator { get; set; }
        public string Vehicle_Coverage_Comprehensiveorspecifiedcauseoflossdeductibleamount { get; set; }
        public string Vehicle_Collision_Deductibleamount { get; set; }
        public string Vehicle_Use_Underfifteenmilesindicator { get; set; }
        public string Vehicle_Use_Fifteenmilesoroverindicator { get; set; }
        public string Vehicle_Netratingfactor { get; set; }
        public string Vehicle_Totalpremiumamount { get; set; }
        public string recordSeq { get; set; }
    }

    public class Auto_Other_Info
    {
        public string Commercialvehiclelineofbusiness_Question_Aajcode { get; set; }
        public string Vehicle_Produceridentifier { get; set; }
        public string Additionalinterest_Fullname { get; set; }
        public string Commercialvehiclelineofbusiness_Question_Abacode { get; set; }
        public string Commercialvehiclelineofbusiness_Question_Kadcode { get; set; }
        public string Commercialvehiclelineofbusiness_Vehiclemaintenanceprograminoperationexplanation { get; set; }
        public string Commercialvehiclelineofbusiness_Question_Abccode { get; set; }
        public string Commercialvehiclelineofbusiness_Vehiclesleasedtoothersexplanation { get; set; }
        public string Commercialvehiclelineofbusiness_Question_Aagcode { get; set; }
        public string Vehicle_Question_Modifiedequipmentdescription { get; set; }
        public string Vehicle_Question_Modifiedequipmentcostamount { get; set; }
        public string Commercialvehiclelineofbusiness_Question_Aaecode { get; set; }
        public string Commercialvehiclelineofbusiness_Question_Aafcode { get; set; }
        public string Commercialvehiclelineofbusiness_Operationinvolvetransportinghazardousmaterialsexplanation { get; set; }
        public string Producer_Customeridentifier { get; set; }
        public string Commercialvehiclelineofbusiness_Question_Aabcode { get; set; }
        public string Commercialvehiclelineofbusiness_Holdharmlessagreementsexplanation { get; set; }
        public string Commercialvehiclelineofbusiness_Question_Aaccode { get; set; }
        public string Commercialvehiclelineofbusiness_Vehiclesusedbyfamilymembersexplanation { get; set; }
        public string Commercialvehiclelineofbusiness_Question_Kaecode { get; set; }
        public string Commercialvehiclelineofbusiness_Applicantobtainmvrverificationsexplanation { get; set; }
        public string Commercialvehiclelineofbusiness_Question_Kafcode { get; set; }
        public string Commercialvehiclelineofbusiness_Applicantspecificdriverrecruitingmethodexplanation { get; set; }
        public string Commercialvehiclelineofbusiness_Question_Aahcode { get; set; }
        public string Commercialvehiclelineofbusiness_Anydriversnotcoveredworkerscompensationexplanation { get; set; }
        public string Commercialvehiclelineofbusiness_Question_Aadcode { get; set; }
        public string Commercialvehiclelineofbusiness_Anyvehiclesownednotscheduledexplanation { get; set; }
        public string Commercialvehiclelineofbusiness_Question_Aaicode { get; set; }
        public string Accidentconviction_Driverproduceridentifier { get; set; }
        public string Accidentconviction_Trafficviolationdate { get; set; }
        public string Accidentconviction_Trafficviolationdescription { get; set; }
        public string Accidentconviction_Placeofincident { get; set; }
        public string Accidentconviction_Violationyearcount { get; set; }
        public string Commercialvehiclelineofbusiness_Question_Abbcode { get; set; }
        public string Commercialvehiclelineofbusiness_Agentinspectedvehiclesexplanation { get; set; }
        public string Commercialvehiclelineofbusiness_Question_Kagcode { get; set; }
        public string Commercialvehiclelineofbusiness_Kahcode { get; set; }
        public string Commercialvehiclelineofbusiness_Electronicdatamonitoringdevice_Fleetmonitoredpercent { get; set; }
        public string Commercialvehiclelineofbusiness_Electronicdatamonitoringdevice_Monitordriversafetyindicator { get; set; }
        public string Commercialvehiclelineofbusiness_Electronicdatamonitoringdevice_Trackfuelconsumptionindicator { get; set; }
        public string Commercialvehiclelineofbusiness_Electronicdatamonitoringdevice_Monitorvehiclemaintenanceindicator { get; set; }
        public string Commercialvehiclelineofbusiness_Electronicdatamonitoringdevice_Mileagetrackingdeviceindicator { get; set; }
        public string Commercialvehiclelineofbusiness_Locationtrackingdeviceindicator { get; set; }
        public string Commercialvehiclelineofbusiness_Electronicdatamonitoringdevice_Navigationindicator { get; set; }
        public string Commercialvehiclelineofbusiness_Electronicdatamonitoringdevice_Otherindicator { get; set; }
        public string Commercialvehiclelineofbusiness_Electronicdatamonitoringdevice_Otherdesciption { get; set; }
        public string Commercialvehiclelineofbusiness_Electronicdatamonitoringdevice_Additionaldescription { get; set; }
        public string Commercialvehiclelineofbusiness_Garagestoragedescription { get; set; }
        public string Commercialvehiclelineofbusiness_Maximumexposureallvehiclesamount { get; set; }
        public string recordSeq { get; set; }
    }

    public class Umbrella_Policy_Info
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public string Policy_Status_Newindicator { get; set; }
        public string Policy_Status_Renewindicator { get; set; }
        public string Policy_Policytype_Umbrellaindicator { get; set; }
        public string Policy_Policytype_Excessindicator { get; set; }
        public string Excessumbrella_Occurrenceindicator { get; set; }
        public string Excessumbrella_Claimsmadeindicator { get; set; }
        public string Excessumbrella_Voluntaryindicator { get; set; }
        public string Excessumbrella_Transactiontype_Otherindicator { get; set; }
        public string Excessumbrella_Transactiontype_Otherdescription { get; set; }
        public string Priorcoverage_Policynumberidentifier { get; set; }
        public string Excessumbrella_Proposedretroactivedate { get; set; }
        public string Excessumbrella_Currentretroactivedate { get; set; }
        public string Excessumbrella_Umbrella_Eachoccurrenceamount { get; set; }
        public string Excessumbrella_Umbrella_Aggregateamount { get; set; }
        public string Excessumbrella_Othercoveragelimitamount { get; set; }
        public string Excessumbrella_Othercoveragedescription { get; set; }
        public string Excessumbrella_Umbrella_Deductibleorretentionamount { get; set; }
        public string Excessumbrella_Firstdollardefencecode { get; set; }
        public string Excessumbrella_Employeebenefits_Eachemployeelimitamount { get; set; }
        public string Excessumbrella_Employeebenefits_Aggregatelimitamount { get; set; }
        public string Excessumbrella_Employeebenefits_Deductibleorretentionamount { get; set; }
        public string Excessumbrella_Employeebenefits_Retroactivedate { get; set; }
        public string Excessumbrella_Employeebenefits_Programname { get; set; }
    }

    public class Umbrella_Locations
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public string Commercialstructure_Location_Produceridentifier { get; set; }
        public string Commercialstructure_Location_Fullname { get; set; }
        public string Commercialstructure_Physicaladdress_Lineone { get; set; }
        public string Commercialstructure_Physicaladdress_Cityname { get; set; }
        public string Commercialstructure_Physicaladdress_Stateorprovincecode { get; set; }
        public string Commercialstructure_Physicaladdress_Postalcode { get; set; }
        public string Businessinformation_Operationsdescription { get; set; }
        public string Businessinformation_Totalpayrollamount { get; set; }
        public string Businessinformation_Annualgrossreceiptsamount { get; set; }
        public string Businessinformation_Foreigngrosssalesamount { get; set; }
        public string Businessinformation_Employeecount { get; set; }
        public string recordSeq { get; set; }
    }

    public class Umbrella_Underlying_Info
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public string Underlyingpolicy_Automobile_Insurerfullname { get; set; }
        public string Underlyingpolicy_Automobile_Policynumberidentifier { get; set; }
        public string Underlyingpolicy_Automobile_Policyeffectivedate { get; set; }
        public string Underlyingpolicy_Automobile_Policyexpirationdate { get; set; }
        public string Vehicle_Combinedsinglelimit_Eachaccidentamount { get; set; }
        public string Vehicle_Bodilyinjury_Peraccidentlimitamount { get; set; }
        public string Vehicle_Bodilyinjury_Perpersonlimitamount { get; set; }
        public string Vehicle_Propertydamage_Peraccidentlimitamount { get; set; }
        public string Underlyingpolicy_Automobile_Combinedsinglelimitpremiumamount { get; set; }
        public string Underlyingpolicy_Automobile_Bodilyinjurypremiumamount { get; set; }
        public string Underlyingpolicy_Automobile_Propertydamageeachaccidentpremiumamount { get; set; }
        public string Underlyingpolicy_Automobile_Modificationfactor { get; set; }
        public string Generalliability_Occurrenceindicator { get; set; }
        public string Generalliability_Claimsmadeindicator { get; set; }
        public string Underlyingpolicy_Generalliability_Insurerfullname { get; set; }
        public string Underlyingpolicy_Generalliability_Policynumberidentifier { get; set; }
        public string Underlyingpolicy_Generalliability_Policyeffectivedate { get; set; }
        public string Underlyingpolicy_Generalliability_Policyexpirationdate { get; set; }
        public string Generalliability_Eachoccurrence_Limitamount { get; set; }
        public string Generalliability_Generalaggregate_Limitamount { get; set; }
        public string Generalliability_Productsandcompletedoperations_Aggregatelimitamount { get; set; }
        public string Generalliability_Personalandadvertisinginjury_Limitamount { get; set; }
        public string Generalliability_Firedamagerentedpremises_Eachoccurrencelimitamount { get; set; }
        public string Generalliability_Medicalexpense_Eachpersonlimitamount { get; set; }
        public string Underlyingpolicy_Generalliability_Premisesoperationspremiumamount { get; set; }
        public string Underlyingpolicy_Generalliability_Productspremiumamount { get; set; }
        public string Underlyingpolicy_Generalliability_Otherpremiumamount { get; set; }
        public string Underlyingpolicy_Generalliability_Modificationfactor { get; set; }
        public string Underlyingpolicy_Employersliability_Insurerfullname { get; set; }
        public string Underlyingpolicy_Employersliability_Policynumberidentifier { get; set; }
        public string Underlyingpolicy_Employersliability_Policyeffectivedate { get; set; }
        public string Underlyingpolicy_Employersliability_Policyexpirationdate { get; set; }
        public string Workerscompensationemployersliability_Employersliability_Eachaccidentlimitamount { get; set; }
        public string Workerscompensationemployersliability_Employersliability_Diseaseeachemployeelimitamount { get; set; }
        public string Workerscompensationemployersliability_Employersliability_Diseasepolicylimitamount { get; set; }
        public string Underlyingpolicy_Employersliability_Premiumamount { get; set; }
        public string Underlyingpolicy_Employersliability_Modificationfactor { get; set; }
        public string Underlyingpolicy_Otherpolicy_Policytypedescription { get; set; }
        public string Underlyingpolicy_Otherpolicy_Insurerfullname { get; set; }
        public string Underlyingpolicy_Otherpolicy_Policynumberidentifier { get; set; }
        public string Underlyingpolicy_Otherpolicy_Policyeffectivedate { get; set; }
        public string Underlyingpolicy_Otherpolicy_Policyexpirationdate { get; set; }
        public string Underlyingpolicy_Otherpolicy_Coveragedescription { get; set; }
        public string Underlyingpolicy_Otherpolicy_Combinedsinglelimitamount { get; set; }
        public string Underlyingpolicy_Otherpolicy_Premiumamount { get; set; }
        public string Underlyingpolicy_Otherpolicy_Modificationfactor { get; set; }
    }

    public class Umbrella_Other_Info
    {
        public string Carecustodyandcontrol_Location_Produceridentifier { get; set; }
        public string Carecustodyandcontrol_Property_Indicator { get; set; }
        public string Carecustodyandcontrol_Property_Valueamount { get; set; }
        public string Carecustodyandcontrol_Insuredliability { get; set; }
        public string Carecustodyandcontrol_Location_Occupiedarea { get; set; }
        public string Carecustodyandcontrol_Property_Propertydescription { get; set; }
        public string Losshistory_Claimdate { get; set; }
        public string Losshistory_Lineofbusiness { get; set; }
        public string Losshistory_Occurrencedescription { get; set; }
        public string Losshistory_Paidamount { get; set; }
        public string Losshistory_Reservedamount { get; set; }
        public string Vehiclefleet_Privatepassenger_Ownedcount { get; set; }
        public string Vehiclefleet_Privatepassenger_Nonownedcount { get; set; }
        public string Vehiclefleet_Privatepassenger_Leasedcount { get; set; }
        public string Vehiclefleet_Privatepassenger_Propertyhauleddescription { get; set; }
        public string Vehiclefleet_Privatepassenger_Localradiusvehiclecount { get; set; }
        public string Vehiclefleet_Privatepassenger_Intermediateradiusvehiclecount { get; set; }
        public string Vehiclefleet_Privatepassenger_Longdistanceradiusvehiclecount { get; set; }
        public string Vehiclefleet_Lightweighttruck_Ownedcount { get; set; }
        public string Vehiclefleet_Lightweighttruck_Nonownedcount { get; set; }
        public string Vehiclefleet_Lightweighttruck_Leasedcount { get; set; }
        public string Vehiclefleet_Lightweighttruck_Propertyhauleddescription { get; set; }
        public string Vehiclefleet_Lightweighttruck_Localradiusvehiclecount { get; set; }
        public string Vehiclefleet_Lightweighttruck_Intermediateradiusvehiclecount { get; set; }
        public string Vehiclefleet_Lightweighttruck_Longdistanceradiusvehiclecount { get; set; }
        public string Vehiclefleet_Mediumweighttruck_Ownedcount { get; set; }
        public string Vehiclefleet_Mediumweighttruck_Nonownedcount { get; set; }
        public string Vehiclefleet_Mediumweighttruck_Leasedcount { get; set; }
        public string Vehiclefleet_Mediumweighttruck_Propertyhauleddescription { get; set; }
        public string Vehiclefleet_Mediumweighttruck_Localradiusvehiclecount { get; set; }
        public string Vehiclefleet_Mediumweighttruck_Intermediateradiusvehiclecount { get; set; }
        public string Vehiclefleet_Mediumweighttruck_Longdistanceradiusvehiclecount { get; set; }
        public string Vehiclefleet_Heavyweighttruck_Ownedcount { get; set; }
        public string Vehiclefleet_Heavyweighttruck_Nonownedcount { get; set; }
        public string Vehiclefleet_Heavyweighttruck_Leasedcount { get; set; }
        public string Vehiclefleet_Heavyweighttruck_Propertyhauleddescription { get; set; }
        public string Vehiclefleet_Heavyweighttruck_Localradiusvehiclecount { get; set; }
        public string Vehiclefleet_Heavyweighttruck_Intermediateradiusvehiclecount { get; set; }
        public string Vehiclefleet_Heavyweighttruck_Longdistanceradiusvehiclecount { get; set; }
        public string Vehiclefleet_Extraheavyweighttruck_Ownedcount { get; set; }
        public string Vehiclefleet_Extraheavyweighttruck_Nonownedcount { get; set; }
        public string Vehiclefleet_Extraheavyweighttruck_Leasedcount { get; set; }
        public string Vehiclefleet_Extraheavyweighttruck_Propertyhauleddescription { get; set; }
        public string Vehiclefleet_Extraheavyweighttruck_Localradiusvehiclecount { get; set; }
        public string Vehiclefleet_Extraheavyweighttruck_Intermediateradiusvehiclecount { get; set; }
        public string Vehiclefleet_Extraheavyweighttruck_Longdistanceradiusvehiclecount { get; set; }
        public string Vehiclefleet_Heavyweighttrucktractor_Ownedcount { get; set; }
        public string Vehiclefleet_Heavyweighttrucktractor_Nonownedcount { get; set; }
        public string Vehiclefleet_Heavyweighttrucktractor_Leasedcount { get; set; }
        public string Vehiclefleet_Heavyweighttrucktractor_Propertyhauleddescription { get; set; }
        public string Vehiclefleet_Heavyweighttrucktractor_Localradiusvehiclecount { get; set; }
        public string Vehiclefleet_Heavyweighttrucktractor_Intermediateradiusvehiclecount { get; set; }
        public string Vehiclefleet_Heavyweighttrucktractor_Longdistanceradiusvehiclecount { get; set; }
        public string Vehiclefleet_Extraheavyweighttrucktractor_Ownedcount { get; set; }
        public string Vehiclefleet_Extraheavyweighttrucktractor_Nonownedcount { get; set; }
        public string Vehiclefleet_Extraheavyweighttrucktractor_Leasedcount { get; set; }
        public string Vehiclefleet_Extraheavyweighttrucktractor_Propertyhauleddescription { get; set; }
        public string Vehiclefleet_Extraheavyweighttrucktractor_Localradiusvehiclecount { get; set; }
        public string Vehiclefleet_Extraheavyweighttrucktractor_Intermediateradiusvehiclecount { get; set; }
        public string Vehiclefleet_Extraheavyweighttrucktractor_Longdistanceradiusvehiclecount { get; set; }
        public string Vehiclefleet_Bus_Ownedcount { get; set; }
        public string Vehiclefleet_Bus_Nonownedcount { get; set; }
        public string Vehiclefleet_Bus_Leasedcount { get; set; }
        public string Vehiclefleet_Bus_Propertyhauleddescription { get; set; }
        public string Vehiclefleet_Bus_Localradiusvehiclecount { get; set; }
        public string Vehiclefleet_Bus_Intermediateradiusvehiclecount { get; set; }
        public string Vehiclefleet_Bus_Longdistanceradiusvehiclecount { get; set; }
    }

    public class Crime_Policy_Info
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public string Crimecoverage_Employeetheft_Blanketindicator { get; set; }
        public string Crimecoverage_Employeetheft_Scheduleindicator { get; set; }
        public string Crimecoverage_Employeetheft_Limitamount { get; set; }
        public string Crimecoverage_Employeetheft_Deductibleamount { get; set; }
        public string Crimecoverage_Employeeretirementincomesecurityact_Coverageindicator { get; set; }
        public string Crimecoverage_Employeeretirementincomesecurityact_Limitamount { get; set; }
        public string Crimecoverage_Employeeretirementincomesecurityact_Aggregatelimitamount { get; set; }
        public string Crimecoverage_Employeeretirementincomesecurityact_Excesslimitamount { get; set; }
        public string Crimecoverage_Employeeretirementincomesecurityact_Totalassetvaluelimitamount { get; set; }
        public string Crimecoverage_Employeeretirementincomesecurityact_Totalassetvalueperplanlimitamount { get; set; }
        public string Crimecoverage_Employeetheftgovernmentalcrime_Blanketindicator { get; set; }
        public string Crimecoverage_Employeetheftgovernmentalcrime_Scheduleindicator { get; set; }
        public string Crimecoverage_Employeetheftgovernmentalcrime_Perlossindicator { get; set; }
        public string Crimecoverage_Employeetheftgovernmentalcrime_Peremployeeindicator { get; set; }
        public string Crimecoverage_Employeetheftgovernmentalcrime_Limitamount { get; set; }
        public string Crimecoverage_Employeetheftgovernmentalcrime_Deductibleamount { get; set; }
        public string Crimecoverage_Forgeryoralteration_Limitamount { get; set; }
        public string Crimecoverage_Forgeryoralteration_Deductibleamount { get; set; }
        public string Crimecoverage_Insidepremisestheft_Blanketindicator { get; set; }
        public string Crimecoverage_Insidepremisestheft_Scheduleindicator { get; set; }
        public string Crimecoverage_Insidepremisestheft_Limitamount { get; set; }
        public string Crimecoverage_Insidepremisestheft_Deductibleamount { get; set; }
        public string Crimecoverage_Insidepremisesrobbery_Blanketindicator { get; set; }
        public string Crimecoverage_Insidepremisesrobbery_Scheduleindicator { get; set; }
        public string Crimecoverage_Insidepremisesrobbery_Limitamount { get; set; }
        public string Crimecoverage_Insidepremisesrobbery_Deductibleamount { get; set; }
        public string Crimecoverage_Outsidepremisesmoneyandsecurities_Limitamount { get; set; }
        public string Crimecoverage_Outsidepremisesmoneyandsecurities_Deductibleamount { get; set; }
        public string Crimecoverage_Outsidepremisesotherproperty_Blanketindicator { get; set; }
        public string Crimecoverage_Outsidepremisesotherproperty_Scheduleindicator { get; set; }
        public string Crimecoverage_Outsidepremisesotherproperty_Limitamount { get; set; }
        public string Crimecoverage_Outsidepremisesotherproperty_Deductibleamount { get; set; }
        public string Crimecoverage_Computerfraud_Limitamount { get; set; }
        public string Crimecoverage_Computerfraud_Deductibleamount { get; set; }
        public string Crimecoverage_Fundstransferfraud_Limitamount { get; set; }
        public string Crimecoverage_Fundstransferfraud_Deductibleamount { get; set; }
        public string Crimecoverage_Moneyorders_Limitamount { get; set; }
        public string Crimecoverage_Moneyorders_Deductibleamount { get; set; }
        public string Crimecoverage_Othercoverage_Coveragedescription { get; set; }
        public string Crimecoverage_Othercoverage_Limitamount { get; set; }
        public string Crimecoverage_Othercoverage_Deductibleamount { get; set; }
        public string Crimecoverage_Coveragebasis_Discoveryindicator { get; set; }
        public string Crimecoverage_Coveragebasis_Losssustainedindicator { get; set; }
    }

    public class Crime_Employee_Classification
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public string Employeeclass_Accountants_Employeecount { get; set; }
        public string Employeeclass_Adjusters_Employeecount { get; set; }
        public string Employeeclass_Administrators_Employeecount { get; set; }
        public string Employeeclass_Appraisers_Employeecount { get; set; }
        public string Employeeclass_Attorneys_Employeecount { get; set; }
        public string Employeeclass_Auditors_Employeecount { get; set; }
        public string Employeeclass_Bookkeepers_Employeecount { get; set; }
        public string Employeeclass_Busdrivers_Employeecount { get; set; }
        public string Employeeclass_Buyers_Employeecount { get; set; }
        public string Employeeclass_Canvassers_Employeecount { get; set; }
        public string Employeeclass_Cashiers_Employeecount { get; set; }
        public string Employeeclass_Chairpersons_Employeecount { get; set; }
        public string Employeeclass_Chefs_Employeecount { get; set; }
        public string Employeeclass_Collectors_Employeecount { get; set; }
        public string Employeeclass_Computerprogrammers_Employeecount { get; set; }
        public string Employeeclass_Comptrollers_Employeecount { get; set; }
        public string Employeeclass_Creditclerks_Employeecount { get; set; }
        public string Employeeclass_Custodians_Employeecount { get; set; }
        public string Employeeclass_Deliverypersons_Employeecount { get; set; }
        public string Employeeclass_Demonstrators_Employeecount { get; set; }
        public string Employeeclass_Dieticians_Employeecount { get; set; }
        public string Employeeclass_Drivers_Employeecount { get; set; }
        public string Employeeclass_Foodinspectors_Employeecount { get; set; }
        public string Employeeclass_Headpharmacists_Employeecount { get; set; }
        public string Employeeclass_Instructorswithmoneyorsecurities_Employeecount { get; set; }
        public string Employeeclass_Janitors_Employeecount { get; set; }
        public string Employeeclass_Lockerroomattendants_Employeecount { get; set; }
        public string Employeeclass_Maitreds_Employeecount { get; set; }
        public string Employeeclass_Managers_Employeecount { get; set; }
        public string Employeeclass_Medicaldirectors_Employeecount { get; set; }
        public string Employeeclass_Messengersoutside_Employeecount { get; set; }
        public string Employeeclass_Payrolldistributors_Employeecount { get; set; }
        public string Employeeclass_Purchasingagents_Employeecount { get; set; }
        public string Employeeclass_Receivingclerks_Employeecount { get; set; }
        public string Employeeclass_Refinerygaugers_Employeecount { get; set; }
        public string Employeeclass_Salespeople_Employeecount { get; set; }
        public string Employeeclass_Securitypersonnel_Employeecount { get; set; }
        public string Employeeclass_Servicestationattendants_Employeecount { get; set; }
        public string Employeeclass_Shippingclerks_Employeecount { get; set; }
        public string Employeeclass_Stockclerks_Employeecount { get; set; }
        public string Employeeclass_Storekeepers_Employeecount { get; set; }
        public string Employeeclass_Storeroompersonnel_Employeecount { get; set; }
        public string Employeeclass_Superintendents_Employeecount { get; set; }
        public string Employeeclass_Supervisors_Employeecount { get; set; }
        public string Employeeclass_Taxidrivers_Employeecount { get; set; }
        public string Employeeclass_Teacherswithmoneyorsecurities_Employeecount { get; set; }
        public string Employeeclass_Timekeepers_Employeecount { get; set; }
        public string Employeeclass_Truckdrivers_Employeecount { get; set; }
        public string Employeeclass_Warehousepersonnel_Employeecount { get; set; }
        public string Employeeclass_Winecellarpersonnel_Employeecount { get; set; }
        public string Employeeclass_Winestewards_Employeecount { get; set; }
        public string Employeeclass_Otherofficersandemployees_Employeecount { get; set; }
        public string Employeeclass_Officers_Employeecount { get; set; }
        public string Employeeclass_Other_Employeecount { get; set; }
        public string Crimelineofbusiness_Retaillocationcount { get; set; }
        public string Crimelineofbusiness_Locationcount { get; set; }
        public string recordSeq { get; set; }
    }

    public class Crime_Other_Info
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public string Employeebenefitplan_Planname { get; set; }
        public string Employeebenefitplan_Principleaddress_Lineone { get; set; }
        public string Employeebenefitplan_Principleaddress_Linetwo { get; set; }
        public string Employeebenefitplan_Principleaddress_Cityname { get; set; }
        public string Employeebenefitplan_Principleaddress_Stateorprovincecode { get; set; }
        public string Employeebenefitplan_Principleaddress_Postalcode { get; set; }
        public string Employeebenefitplan_Trusteecount { get; set; }
        public string Employeebenefitplan_Participantcount { get; set; }
        public string Crimelineofbusiness_Question_Aabcode { get; set; }
        public string Crimelineofbusiness_Question_Kaacode { get; set; }
        public string Employeeclass_Volunteercount { get; set; }
        public string Crimelineofbusiness_Question_Kabcode { get; set; }
        public string Employeeclass_Leasedtoothersemployeecount { get; set; }
        public string Crimelineofbusiness_Anyemployeesleasedtoothersexplanation { get; set; }
        public string Crimelineofbusiness_Question_Kaccode { get; set; }
        public string Employeeclass_Leasedfromothersemployeecount { get; set; }
        public string Crimelineofbusiness_Anyemployeesleasedfromothersexplanation { get; set; }
        public string Crimelineofbusiness_Question_Kadcode { get; set; }
        public string Crimelineofbusiness_Anyemployeesperformmoneyinvestingexplanation { get; set; }
        public string Crimelineofbusiness_Question_Kaecode { get; set; }
        public string Crimelineofbusiness_Anyemployeesreceiveorissueswarehousereceiptsexplanation { get; set; }
        public string Crimelineofbusiness_Question_Kafcode { get; set; }
        public string Crimelineofbusiness_Anyemployeescancelledforcrimecoveragebyinsurerexplanation { get; set; }
        public string Crimelineofbusiness_Question_Kagcode { get; set; }
        public string Crimelineofbusiness_Applicantwrittenagreementwithclientsexplanation { get; set; }
        public string Crimelineofbusiness_Question_Kahcode { get; set; }
        public string Crimelineofbusiness_Applicanttransferfundsviaphoneorfaxexplanation { get; set; }
        public string Crimelineofbusiness_Question_Kaicode { get; set; }
        public string Crimelineofbusiness_Anyexposurefromlosstoguestpropertyexplanation { get; set; }
        public string Crimelineofbusiness_Question_Kajcode { get; set; }
        public string Crimelineofbusiness_Question_Kakcode { get; set; }
        public string Crimelineofbusiness_Question_Kalcode { get; set; }
        public string Crimelineofbusiness_Question_Kamcode { get; set; }
        public string Crimelineofbusiness_Question_Kancode { get; set; }
        public string Crimelineofbusiness_Question_Kaocode { get; set; }
        public string Crimelineofbusiness_Question_Kapcode { get; set; }
        public string Crimelineofbusiness_Question_Kaqcode { get; set; }
        public string Auditinformation_Performedby_Cpaindicator { get; set; }
        public string Auditinformation_Performedby_Publicaccountantindicator { get; set; }
        public string Auditinformation_Performedby_Staff { get; set; }
        public string Auditinformation_Performedby_Otherindicator { get; set; }
        public string Auditinformation_Performedby_Otherdescription { get; set; }
        public string Auditor_Fullname { get; set; }
        public string Auditor_Mailingaddress_Lineone { get; set; }
        public string Auditor_Mailingaddress_Linetwo { get; set; }
        public string Auditor_Mailingaddress_Cityname { get; set; }
        public string Auditor_Mailingaddress_Stateorprovincecode { get; set; }
        public string Auditor_Mailingaddress_Postalcode { get; set; }
        public string Auditinformation_Cashandaccounts_Auditdate { get; set; }
        public string Auditinformation_Inventory_Auditdate { get; set; }
        public string Auditinformation_Frequency_Annualindicator { get; set; }
        public string Auditinformation_Frequency_Semiannualindicator { get; set; }
        public string Auditinformation_Frequency_Quarterlyindicator { get; set; }
        public string Auditinformation_Frequency_Otherindicator { get; set; }
        public string Auditinformation_Frequency_Otherdescription { get; set; }
        public string Auditinformation_Renderedto_Ownerindicator { get; set; }
        public string Auditinformation_Renderedto_Partnersindicator { get; set; }
        public string Auditinformation_Renderedto_Boardofdirectorsindicator { get; set; }
        public string Auditinformation_Renderedto_Otherindicator { get; set; }
        public string Auditinformation_Renderedto_Otherdescription { get; set; }
        public string Auditinformation_Financialformat_Auditindicator { get; set; }
        public string Auditinformation_Financialformat_Reviewindicator { get; set; }
        public string Auditinformation_Financialformat_Compilationindicator { get; set; }
        public string Auditinformation_Financialformat_Taxreturnonlyindicator { get; set; }
        public string Crimelineofbusiness_Question_Aahcode { get; set; }
        public string Crimelineofbusiness_Question_Aaicode { get; set; }
        public string Crimelineofbusiness_Auditmadeinaccordancewithauditstandardsexplanation { get; set; }
        public string Crimelineofbusiness_Question_Aajcode { get; set; }
        public string Crimelineofbusiness_Question_Aaccode { get; set; }
        public string Crimelineofbusiness_Question_Karcode { get; set; }
        public string Crimelineofbusiness_Question_Kascode { get; set; }
        public string Crimelineofbusiness_Question_Katcode { get; set; }
        public string Crimelineofbusiness_Question_Kaucode { get; set; }
        public string Auditinformation_Physicalinventory_Frequencydescription { get; set; }
        public string Crimelineofbusiness_Question_Kavcode { get; set; }
        public string Crimelineofbusiness_Question_Kawcode { get; set; }
        public string Crimelineofbusiness_Question_Aadcode { get; set; }
        public string Crimelineofbusiness_Question_Aaecode { get; set; }
        public string Audit_Signscontrolsfullname { get; set; }
        public string Crimelineofbusiness_Question_Aafcode { get; set; }
        public string Crimelineofbusiness_Question_Aagcode { get; set; }
        public string Crimelineofbusiness_Question_Kaxcode { get; set; }
        public string Audit_Maximumeftamount { get; set; }
        public string Crimelineofbusiness_Question_Kaycode { get; set; }
        public string Crimelineofbusiness_Question_Kazcode { get; set; }
        public string Crimelineofbusiness_Question_Kbacode { get; set; }
        public string Crimemoneyandsecurities_Moneyamount { get; set; }
        public string Crimemoneyandsecurities_Checksfordepositamount { get; set; }
        public string Crimemoneyandsecurities_Checksforaccountspayableamount { get; set; }
        public string Crimemoneyandsecurities_Payrollchecks { get; set; }
        public string Crimemoneyandsecurities_Moneyovernightamount { get; set; }
        public string Crimemoneyandsecurities_Securitiesamount { get; set; }
        public string Crimelineofbusiness_Question_Kbbcode { get; set; }
        public string Crimelineofbusiness_Question_Kbccode { get; set; }
        public string Crimelineofbusiness_Question_Kbdcode { get; set; }
        public string Crimelineofbusiness_Question_Kbecode { get; set; }
        public string Crimelineofbusiness_Question_Kbfcode { get; set; }
        public string Crimelineofbusiness_Question_Kbgcode { get; set; }
        public string Crimelineofbusiness_Question_Kbhcode { get; set; }
        public string Crimelineofbusiness_Question_Kbicode { get; set; }
        public string Crimelineofbusiness_Question_Kbjcode { get; set; }
        public string Crimeinformation_Propertydescription { get; set; }
        public string Crimeinformation_Propertymaximumvalueamount { get; set; }
        public string Crimeinformation_Businesshoursstarttime { get; set; }
        public string Crimeinformation_Businesshoursclosetime { get; set; }
        public string Crimeinformation_Averageemployeesondutycount { get; set; }
        public string Crimeinformation_Checksstampedcode { get; set; }
        public string Crimeinformation_Depositfrequency_Dailyindicator { get; set; }
        public string Crimeinformation_Depositfrequency_Otherindicator { get; set; }
        public string Crimeinformation_Depositfrequencycode { get; set; }
        public string Crimeinformation_Nightdepositoryusedcode { get; set; }
        public string Crimeinformation_Annualgrossreceiptsamount { get; set; }
        public string Buildingprotection_Doublecylinderdoorlockcode { get; set; }
        public string Crimeinformation_Otherdescription { get; set; }
        public string Safevault_Manufacturerfullname { get; set; }
        public string Safevault_Label_Ulindicator { get; set; }
        public string Safevault_Label_Smnaindicator { get; set; }
        public string Safevault_Classcode { get; set; }
        public string Safevault_Doortype_Roundindicator { get; set; }
        public string Safevault_Doortype_Squareindicator { get; set; }
        public string Safevault_Combinationlock_Outerindicator { get; set; }
        public string Safevault_Combinationlock_Innerindicator { get; set; }
        public string Safevault_Combinationlock_Chestindicator { get; set; }
        public string Safevault_Doorthickness { get; set; }
        public string Safevault_Wallthickness { get; set; }
        public string Messengerprotection_Messengercount { get; set; }
        public string Messengerprotection_Guardcount { get; set; }
        public string Messengerprotection_Armouredvehiclecount { get; set; }
        public string Messengerprotection_Privateconveyancecode { get; set; }
        public string Messengerprotection_Safetysatchelcode { get; set; }
        public string Alarm_Alarmtype_Holdupindicator { get; set; }
        public string Alarm_Alarmtype_Premisesindicator { get; set; }
        public string Alarm_Alarmtype_Safeindicator { get; set; }
        public string Alarm_Burglar_Localgongindicator { get; set; }
        public string Alarm_Burglar_Centralstationindicator { get; set; }
        public string Alarm_Burglar_Policeconnectindicator { get; set; }
        public string Alarm_Burglar_Withkeysindicator { get; set; }
        public string Alarm_Burglar_Gradecode { get; set; }
        public string Alarm_Burglar_Protectionextentsafevaultpartialindicator { get; set; }
        public string Alarm_Burglar_Protectionextentsafevaultcompleteindicator { get; set; }
        public string Alarm_Burglar_Protectionextentpremisesoneindicator { get; set; }
        public string Alarm_Burglar_Protectionextentpremisestwoindicator { get; set; }
        public string Alarm_Burglar_Protectionextentpremisesthreeindicator { get; set; }
        public string Alarm_Burglar_Installedandservicedbyname { get; set; }
        public string Buildingsecurity_Guardcount { get; set; }
        public string Buildingsecurity_Watchpersoncount { get; set; }
        public string Buildingsecurity_Watchpersonfactorcredit_Centralstationhourlyindicator { get; set; }
        public string Buildingsecurity_Watchpersonfactorcredit_Clockhourlyindicator { get; set; }
        public string Buildingsecurity_Watchpersonfactorcredit_Doesnotsignalindicator { get; set; }
        public string Alarm_Burglar_Certificateidentifier { get; set; }
        public string Alarm_Burglar_Certificateexpirationdate { get; set; }
        public string Buildingsecurity_Accessibleopeningsdescription { get; set; }
        public string Buildingsecurity_Otherprotectiondescription { get; set; }
    }

    public class Crime_Employee_Schedule
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public string Employee_Fullname { get; set; }
        public string Employmentinformation_Employmenttitle { get; set; }
        public string Crimecoverage_Employeetheft_Limitamount { get; set; }
        public string Crimecoverage_Employeetheft_Deductibleamount { get; set; }
    }

    public class EB_Policy_Info
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public EB_Additional_Interest EB_Additional_Interest { get; set; }
        public EB_Other_Info EB_Other_Info { get; set; }
    }

    public class EB_Additional_Interest
    {
        public string Additionalinterest_Interest_Additionalinsuredindicator { get; set; }
        public string Additionalinterest_Interest_Breachofwarrantyindicator { get; set; }
        public string Additionalinterest_Interest_Coownerindicator { get; set; }
        public string Additionalinterest_Interest_Employeeaslessorindicator { get; set; }
        public string Additionalinterest_Interest_Leasebackownerindicator { get; set; }
        public string Additionalinterest_Interest_Lenderslosspayableindicator { get; set; }
        public string Additionalinterest_Interest_Lienholderindicator { get; set; }
        public string Additionalinterest_Interest_Losspayeeindicator { get; set; }
        public string Additionalinterest_Interest_Mortgageeindicator { get; set; }
        public string Additionalinterest_Interest_Ownerindicator { get; set; }
        public string Additionalinterest_Interest_Registrantindicator { get; set; }
        public string Additionalinterest_Interest_Trusteeindicator { get; set; }
        public string Additionalinterest_Interest_Otherindicator { get; set; }
        public string Additionalinterest_Interest_Otherdescription { get; set; }
        public string Additionalinterest_Interestreasondescription { get; set; }
        public string Additionalinterest_Interestrank { get; set; }
        public string Additionalinterest_Certificaterequiredindicator { get; set; }
        public string Additionalinterest_Policyrequiredindicator { get; set; }
        public string Additionalinterest_Sendbillindicator { get; set; }
        public string Additionalinterest_Fullname { get; set; }
        public string Additionalinterest_Mailingaddress_Lineone { get; set; }
        public string Additionalinterest_Mailingaddress_Linetwo { get; set; }
        public string Additionalinterest_Mailingaddress_Cityname { get; set; }
        public string Additionalinterest_Mailingaddress_Stateorprovincecode { get; set; }
        public string Additionalinterest_Mailingaddress_Postalcode { get; set; }
        public string Additionalinterest_Mailingaddress_Countrycode { get; set; }
        public string Additionalinterest_Accountnumberidentifier { get; set; }
        public string Additionalinterest_Interestenddate { get; set; }
        public string Additionalinterest_Loanamount { get; set; }
        public string Additionalinterest_Primary_Phonenumber { get; set; }
        public string Additionalinterest_Primary_Faxnumber { get; set; }
        public string Additionalinterest_Primary_Emailaddress { get; set; }
        public string Additionalinterest_Item_Locationproduceridentifier { get; set; }
        public string Additionalinterest_Item_Buildingproduceridentifier { get; set; }
        public string Additionalinterest_Item_Vehicleproduceridentifier { get; set; }
        public string Additionalinterest_Item_Boatproduceridentifier { get; set; }
        public string Additionalinterest_Item_Airportidentifier { get; set; }
        public string Additionalinterest_Item_Aircraftproduceridentifier { get; set; }
        public string Additionalinterest_Item_Scheduleditemclasscode { get; set; }
        public string Additionalinterest_Item_Scheduleditemproduceridentifier { get; set; }
        public string Additionalinterest_Itemdescription { get; set; }
    }

    public class EB_Other_Info
    {
        public string Boilerandmachinerylineofbusiness_Question_Aabcode { get; set; }
        public string Boilerandmachinerylineofbusiness_Equipmentmaintenanceoverhaulrepairnotconductedmanufacturerinstructionsexplanation { get; set; }
        public string Boilerandmachinerylineofbusiness_Question_Aaccode { get; set; }
        public string Boilerandmachinerylineofbusiness_Allequipmentnotaccessibletorepairorreplacementexplanation { get; set; }
        public string Boilerandmachinerylineofbusiness_Question_Aadcode { get; set; }
        public string Boilerandmachinerylineofbusiness_Allequipmentinstumentationandcontrolsnotaccordancewithmanufacturesspeificationexplanation { get; set; }
        public string Boilerandmachinerylineofbusiness_Question_Aaecode { get; set; }
        public string Boilerandmachinerylineofbusiness_Chlorofluorocarbonrefrigerantsusedinmachinerytocoolpremisesorprocessexplanation { get; set; }
        public string Boilerandmachinerylineofbusiness_Question_Aafcode { get; set; }
        public string Boilerandmachinerylineofbusiness_Allmachineryandequipmentnotingoodconditionexplanation { get; set; }
        public string Boilerandmachinerylineofbusiness_Remarktext { get; set; }
    }

    public class EB_Premises_Info
    {
        public string Policy_Effectivedate { get; set; }
        public string Policy_Expirydate { get; set; }
        public string Policy_Policynumberidentifier { get; set; }
        public string Commercialstructure_Location_Produceridentifier { get; set; }
        public string Commercialstructure_Building_Produceridentifier { get; set; }
        public string Boilerandmachinerycoverage_Equipmentbreakdown_Limitamount { get; set; }
        public string Boilerandmachinerycoverage_Equipmentbreakdown_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Pressureequipment_Propertydamagelimitamount { get; set; }
        public string Boilerandmachinerycoverage_Pressureequipment_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Mechanicalandelectricalequipment_Propertydamagelimitamount { get; set; }
        public string Boilerandmachinerycoverage_Mechanicalandelectricalequipment_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Productionmachinery_Propertydamagelimitamount { get; set; }
        public string Boilerandmachinerycoverage_Productionmachinery_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Diagnosticequipment_Propertydamagelimitamount { get; set; }
        public string Boilerandmachinerycoverage_Diagnosticequipment_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Expeditingexpense_Limitamount { get; set; }
        public string Boilerandmachinerycoverage_Expeditingexpense_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Businessincomeextraexpense_Limitamount { get; set; }
        public string Boilerandmachinerycoverage_Businessincomeextraexpense_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Extraexpense_Daycount { get; set; }
        public string Boilerandmachinerycoverage_Extraexpense_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Extendedperiodofrestoration_Daycount { get; set; }
        public string Boilerandmachinerycoverage_Extendedperiodofrestoration_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Dataormedia_Limitamount { get; set; }
        public string Boilerandmachinerycoverage_Dataormedia_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Spoilage_Limitamount { get; set; }
        public string Boilerandmachinerycoverage_Spoilage_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Utilityserviceinterruption_Hourcount { get; set; }
        public string Boilerandmachinerycoverage_Utilityserviceinterruption_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Newlyacquiredpremises_Daycount { get; set; }
        public string Boilerandmachinerycoverage_Newlyacquiredpremises_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Ordinanceorlaw_Limitamount { get; set; }
        public string Boilerandmachinerycoverage_Ordinanceorlaw_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Errorsandomissions_Limitamount { get; set; }
        public string Boilerandmachinerycoverage_Errorsandomissions_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Brandsandlabels_Limitamount { get; set; }
        public string Boilerandmachinerycoverage_Brandsandlabels_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Contingentbusinessincomeextraexpense_Limitamount { get; set; }
        public string Boilerandmachinerycoverage_Contingentbusinessincomeextraexpense_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Coveredpremises_Limitamount { get; set; }
        public string Boilerandmachinerycoverage_Coveredpremises_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Salesservicematerials_Limitamount { get; set; }
        public string Boilerandmachinerycoverage_Salesservicematerials_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Demolition_Limitamount { get; set; }
        public string Boilerandmachinerycoverage_Demolition_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Offpremisespropertydamage_Limitamount { get; set; }
        public string Boilerandmachinerycoverage_Offpremisespropertydamage_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Othercoverage_Coveragedescription { get; set; }
        public string Boilerandmachinerycoverage_Othercoverage_Limitamount { get; set; }
        public string Boilerandmachinerycoverage_Othercoverage_Deductibleamount { get; set; }
        public string Boilerandmachinerycoverage_Ammoniacontamination_Limitamount { get; set; }
        public string Boilerandmachinerycoverage_Consequentialloss_Limitamount { get; set; }
        public string Boilerandmachinerycoverage_Hazardoussubstance_Limitamount { get; set; }
        public string Boilerandmachinerycoverage_Waterdamage_Limitamount { get; set; }
        public string Boilerandmachinerycoverage_Businessincome_Reportdate { get; set; }
        public string Boilerandmachinerycoverage_Businessincome_Annualvalueamount { get; set; }
        public string Boilerandmachinerycoverage_Businessincome_Coinsurancepercent { get; set; }
        public string Boilerandmachinerycoverage_Diagnosticequipmentoption_Includedexcludedcode { get; set; }
        public string recordSeq { get; set; }
    }

    public class Account_Level_Info
    {
        public string Namedinsured_Fullname { get; set; }
        public string NamedInsured_MailingAddress_LineOne { get; set; }
        public string NamedInsured_MailingAddress_CityName { get; set; }
        public string NamedInsured_MailingAddress_StateOrProvinceCode { get; set; }
        public string NamedInsured_MailingAddress_PostalCode { get; set; }
        public string Namedinsured_Naicscode { get; set; }
        public string Naics_Description { get; set; }
        public string Producer_Fullname { get; set; }
        public string Insurer_Produceridentifier { get; set; }
        public string Producer_Mailingaddress_Lineone { get; set; }
        public string Producer_Mailingaddress_Postalcode { get; set; }
        public string Namedinsured_Taxidentifier { get; set; }
    }

}
