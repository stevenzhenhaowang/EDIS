using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EDISAngular;
using EDISAngular.Models.ViewModels;
using EDISAngular.Services;
using Microsoft.AspNet.Identity;
using EDISAngular.Infrastructure.Authorization;
using EDIS_DOMAIN;
using EDISAngular.Infrastructure.DatabaseAccess;
using EDISAngular.Models.ServiceModels.AdviserProfile;
using EDISAngular.Models.ServiceModels.CorporateActions;




namespace EDISAngular.APIControllers
{
    public class ClientController : ApiController
    {
        private ClientRepository clientRepo;
        public ClientController()
        {
            clientRepo = new ClientRepository();
        }
        [HttpGet, Route("api/client/id")]
        [Authorize(Roles = AuthorizationRoles.Role_Client)]
        public string getCientId()
        {
            var userid = User.Identity.GetUserId();
            return userid;
        }
        [HttpGet, Route("api/client/adviser")]
        [Authorize(Roles = AuthorizationRoles.Role_Client)]
        public AdviserView getAdviserAccountNumber()
        {
            var userid = User.Identity.GetUserId();
            return clientRepo.GetAdviserAccountNumberForClient(userid);
        }


        #region methods added 18/05/2015
        [HttpGet, Route("api/client/businessRevenueBrief")]
        public BusinessPortfolioOverviewBriefModel GetBriefBusinessRevenue()
        {
            return clientRepo.GetBusinessRevenueData(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/client/debtInstruments")]
        public BusinessPortfolioOverviewBriefModel GetInstrumentsData()
        {
            return clientRepo.GetDebtInstrumentsData(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/client/insuranceStatistics")]
        public ProfileInsuranceStatisticsModel GetInsuranceStatistics()
        {
            return clientRepo.GetInsuranceStatisticsData(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/client/worldMarkets")]
        public List<WordMarketItemModel> GetWorldMarkets()
        {
            return clientRepo.GetWorldMarketData(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/client/currencies")]
        public List<WordMarketItemModel> GetCurrencies()
        {
            return clientRepo.GetCurrencies(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/client/historicalrevenue")]
        public HistoricalRevenueModel GetHistoricalRevenue()
        {
            return clientRepo.GetHistoricalRevenueData(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/client/investmentstat")]
        public BusinessStatDetailModel GetInvestmentStat()
        {
            return clientRepo.GetInvestmentStat(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/client/lendingstat")]
        public BusinessStatDetailModel GetLendingStat()
        {
            return clientRepo.GetLendingStat(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/client/InsuranceStat")]
        public InsuranceStatModel GetInsuranceStats()
        {
            return clientRepo.GetInsuranceStatDetailed(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/client/clientPositionsMonitor")]
        public List<ClientPositionMonitorModel> GetClientPositionMonitor()
        {
            return clientRepo.GetClientPositionMonitor(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/client/GeoLocations")]
        public List<GeoGraphicalLocations> GetGeoLocations()
        {
            return clientRepo.GetGeoLocations(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/client/GeoDetails")]
        public GeoStatsModel GetGeoStat([FromUri] string[] locations)
        {
            if (locations == null)
            {
                return clientRepo.GetGeoStats(User.Identity.GetUserId(), new[] { "" });
            }
            else
            {
                return clientRepo.GetGeoStats(User.Identity.GetUserId(), new[] { "" });
            }
        }
        [HttpGet, Route("api/client/BusinessReenueDetails")]
        public BuisnessRevenueDetailsDataModel GetBusinessRevenueDetails()
        {
            return clientRepo.GetBusinessRevenueDetails(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/client/ComplianceDetails")]
        public CompliantModel GetComplianceDetails()
        {
            return clientRepo.GetComplianceDetails(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/client/reminders")]
        public ReminderModel GetReminders()
        {
            return clientRepo.GetReminders(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/client/companyList")]
        public List<AnalysisCityBrief> GetCityList()
        {
            return clientRepo.GetAnalysisCompaniesList(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/client/research/companyProfile")]
        public CompanyProfileDataItem GetCompanyProfile(string companyId)
        {
            return clientRepo.GetCompanyProfile(User.Identity.GetUserId(), companyId);
        }
        #endregion
        #region methods added 26/05/2015

        [HttpGet, Route("api/client/clientaccounts")]
        [Authorize(Roles = AuthorizationRoles.Role_Client)]
        public List<CorporateActionClientAccountModel> GetAllClientAccounts()
        {
            return clientRepo.GetAllClientAccounts(User.Identity.GetUserId());
        }
        #endregion




    }
}
