using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;





namespace EDISAngular.Models.BindingModels
{
    public class AdviserRegistrationBindingModel
    {
        [Required]
        [Display()]
        [DisplayName("Adviser ID")]
        [HiddenInput(DisplayValue = false)]
        public string adviserUserId { get; set; }
        [Required]
        [DisplayName("Current Position Title")]
        public string currentPositionTitle { get; set; }
        [Required]
        [DisplayName("Company Name")]
        public string companyName { get; set; }
        [Required]
        [DisplayName("ABN/ACN")]
        public string ABN { get; set; }
        [Required]
        [DisplayName("Title")]
        public string title { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string firstName { get; set; }
        [DisplayName("Middle Name")]
        public string middleName { get; set; }
        [DisplayName("Last Name")]
        [Required]
        public string lastName { get; set; }
        [Required]
        [DisplayName("Gender")]
        public string gender { get; set; }
        [Required]
        [DisplayName("Industry Experience Start Date")]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime? industryExperienceStartDate { get; set; }
        [Required]
        [DisplayName("Business Phone")]
        public string businessPhone { get; set; }
        [Required]
        [DisplayName("Business Mobile")]
        public string businessMobile { get; set; }
        [DisplayName("Business Fax")]
        public string businessFax { get; set; }
        [Required]
        [DisplayName("Address Line 1")]
        public string addressLine1 { get; set; }
        [DisplayName("Address Line 2")]
        public string addressLine2 { get; set; }
        [DisplayName("Address Line 3")]
        public string addressLine3 { get; set; }
        [Required]
        [DisplayName("Suburb")]
        public string suburb { get; set; }
        [DisplayName("State")]
        [Required]
        public string state { get; set; }
        [DisplayName("Postcode")]
        [Required]
        public string postCode { get; set; }
        [DisplayName("Country")]
        [Required]
        public string country { get; set; }

        //[Required]
        [DisplayName("Role and Services Summary")]
        public string roleAndServicesSummary { get; set; }
        //cv document required
        [Required]
        [DisplayName("Dealer Group Name")]
        public string dealerGroupName { get; set; }
        [DisplayName("ASFL")]
        public string asfl { get; set; }
        [Required]
        [DisplayName("Dealer Group Address Line 1")]
        public string dealerGroup_addressLine1 { get; set; }
        [DisplayName("Dealer Group Address Line 2")]
        public string dealerGroup_addressLine2 { get; set; }
        [DisplayName("Dealer Group Address Line 3")]
        public string dealerGroup_addressLine3 { get; set; }
        [Required]
        [DisplayName("Dealer Group Suburb")]
        public string dealerGroup_suburb { get; set; }
        [DisplayName("Dealer Group State")]
        [Required]
        public string dealerGroup_state { get; set; }
        [DisplayName("Dealer Group Postcode")]
        [Required]
        public string dealerGroup_postCode { get; set; }
        [DisplayName("Dealer Group Country")]
        [Required]
        public string dealerGroup_country { get; set; }
        [DisplayName("Does the Dealer Group have a Derivatives License?")]
        [Required]
        public bool dealerGroupHasDerivativesLicense { get; set; }
        [Required]
        [DisplayName("An Authorized Representative?")]
        public bool isAuthorizedRepresentative { get; set; }
        [DisplayName("Authorized Representative Number")]
        public string authorizedRepresentativeNumber { get; set; }
        [DisplayName("Total Assets Under Management")]
        [Range(0, double.MaxValue, ErrorMessage = "Incorrect value input")]
        public string totalAssetUnderManagement { get; set; }
        [DisplayName("Total Managed Investments")]
        [Range(0, double.MaxValue, ErrorMessage = "Incorrect value input")]
        public string totalInvestmentUndermanagement { get; set; }
        [DisplayName("Total Direct Australian Equities Under Management")]
        [Range(0, double.MaxValue, ErrorMessage = "Incorrect value input")]
        public string totalDirectAustralianEquitiesUnderManagement { get; set; }
        [DisplayName("Total Direct International Equities Under Management")]
        [Range(0, double.MaxValue, ErrorMessage = "Incorrect value input")]
        public string totalDirectInterantionalEquitiesUnderManagement { get; set; }
        [DisplayName("Total Direct Fixed Interest Under Management")]
        [Range(0, double.MaxValue, ErrorMessage = "Incorrect value input")]
        public string totalDirectFixedInterestUnderManagement { get; set; }
        [DisplayName("Total Direct Lending Book Interest Under Management")]
        [Range(0, double.MaxValue, ErrorMessage = "Incorrect value input")]
        public string totalDirectLendingBookInterestUnderManagement { get; set; }

        [DisplayName("Approximate no. of clients")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter proper number")]
        public string approximateNumberOfClients { get; set; }
        [DisplayName("Profession Type")]
        public string professiontypeId { get; set; }
        [DisplayName("Professional Associations")]
        public List<int> professionAssociationIds { get; set; }

        [DisplayName("Education and Qualifications")]
        public List<EducationCreationModel> educations { get; set; }
        //[Required]                                                        //........................................changed
        [DisplayName("Services Provided")]
        public List<ServiceGroupModel> services { get; set; }



        #region deleted hard coded commission questions
        //[DisplayName("Do you receive trail commission from providers on investment products")]
        //public bool? receiveTrailCommissionFromProvidersOnInvestmentProducts { get; set; }
        //[Required]
        //[DisplayName("Details")]
        //public string detailsOnReceiveTrailCommission { get; set; }
        //[DisplayName("Do you charge clients an ongoing service Fees")]
        //public bool? chargeOnGoingFee { get; set; }
        //[DisplayName("Charge for Statement of Advice")]
        //public bool? chargeForStatementOfAdvice { get; set; }
        //[DisplayName("Charge for Record of Advice")]
        //public bool? chargeForRecordOfAdvice { get; set; }
        //[DisplayName("Charge for Implementation of Advice")]
        //public bool? chargeForImplementationOfAdvice { get; set; }
        //[DisplayName("Other Charges")]
        //public bool? otherCharges { get; set; }
        #endregion

        //[Required]                                                                     //........................................changed
        [DisplayName("Fees and Commissions")]
        public List<CommisionsAndFeesModel> commissionsAndFees { get; set; }




        [Required]
        [DisplayName("Do you want to disclose remuneration Method?")]
        public bool remunerationMethodSpecified { get; set; }
        [DisplayName("Remuneration Method")]
        public string remunerationMethod { get; set; }
        [Required]
        [DisplayName("Annual Income Level")]
        [Range(0, int.MaxValue, ErrorMessage = "Incorrect income level id")]
        public int annualIncomeLevelId { get; set; }
        [Required]
        [DisplayName("Investible Asset Level")]
        [Range(0, int.MaxValue, ErrorMessage = "Incorrect investible level id")]
        public int investibleAssetLevel { get; set; }
        [Required]
        [DisplayName("Total Asset Level")]
        [Range(0, int.MaxValue, ErrorMessage = "Incorrect asset level id")]
        public int totalAssetLevelId { get; set; }
        [DisplayName("News Letter Services")]
        public List<NewsletterServiceModel> newsLetterServices { get; set; }
        [Required]
        [DisplayName("Approx. Number of Clients")]
        public int numberOfClientsId { get; set; }



        #region view model properties, not required for post
        [DisplayName("Annual Income Level")]
        public string AnnualIncomeLevel { get; set; }
        [DisplayName("Total Asset Level")]
        public string TotalAssetLevel { get; set; }
        [DisplayName("Investible Asset Level")]
        public string InvestibleAssetLevelString { get; set; }
        [DisplayName("Approx. Number of Clients")]
        public string approxNumberOfClients { get; set; }
        [DisplayName("Provided Services")]
        public string ProvidedServicesString { get; set; }
        [DisplayName("Geographic Location")]
        public string GeoString { get; set; }
        #endregion

    }
    public class EducationCreationModel
    {
        [Required]
        [DisplayName("Institution")]
        public string institution { get; set; }
        [Required]
        [DisplayName("Education Level")]
        public int educationLevelId { get; set; }
        [Required]
        [DisplayName("Course Title")]
        public string courseTitle { get; set; }
        [DisplayName("Currently Studying")]
        [Required]
        public bool courseStatus { get; set; }
    }
    public class NewsletterServiceModel
    {
        public int newsLetterServiceId { get; set; }
        public string serviceName { get; set; }
        public bool selected { get; set; }
    }
    public class ServiceGroupModel
    {
        public string groupName { get; set; }
        public List<ServiceList> services { get; set; }
    }
    public class ServiceList
    {
        public int serviceId { get; set; }
        public string serviceName { get; set; }
        public bool providing { get; set; }

    }
    public class CommisionsAndFeesModel
    {
        public string description { get; set; }
        public int id { get; set; }
        public bool selected { get; set; }
    }
}