using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using EDISAngular;
using EDISAngular.Models.ViewModels;
using EDISAngular.Services;
using EDISAngular.Infrastructure.Authorization;
using EDISAngular.Infrastructure.DatabaseAccess;
using EDISAngular.Models.ServiceModels.AdviserProfile;
using EDIS_DOMAIN;
using EDISAngular.Models.ServiceModels.CorporateActions;
using System.Web.Http.Filters;
using System.Threading.Tasks;
using Domain.Portfolio.AggregateRoots;
using SqlRepository;
using EDISAngular.Models.BindingModels;
using Shared;
using Domain.Portfolio.AggregateRoots.Accounts;
using EDISAngular.Models.ServiceModels;
using Domain.Portfolio.AggregateRoots.Liability;
using Domain.Portfolio.AggregateRoots.Asset;

namespace EDISAngular.APIControllers
{
    public class AdviserController : ApiController
    {
        private AdviserRepository advisorRepo;
        private EdisRepository edisRepo;

        public AdviserController()
        {
            edisRepo = new EdisRepository();
            advisorRepo = new AdviserRepository();
        }
        [HttpGet, Route("api/adviser/accountNumber")]
        public string getAdviserAccountNumber()
        {
            var userid = User.Identity.GetUserId();
            return edisRepo.GetAdviserSync(userid, DateTime.Now).AdviserNumber;

        }

        //#region added actions 13/05/2015
        [HttpGet, Route("api/adviser/clientaccounts")]
        [Authorize(Roles = AuthorizationRoles.Role_Adviser)]
        public List<CorporateActionClientAccountModel> GetAllClientAccounts()
        {
            //return advisorRepo.GetAllClientAccounts(User.Identity.GetUserId());
            var userid = User.Identity.GetUserId();
            var allGroups = edisRepo.GetAllClientGroupsForAdviserSync(userid, DateTime.Now);
            var clients = new List<Client>();
            List<CorporateActionClientAccountModel> allClients = new List<CorporateActionClientAccountModel>();
            foreach (var group in allGroups)
            {
                clients.AddRange(group.GetClientsSync());
            }
            //to get account number
            foreach (var client in clients)
            {
                var accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                foreach (var account in accounts)
                {
                    allClients.Add(new CorporateActionClientAccountModel
                    {

                        edisAccountNumber = account.AccountNumber,
                        type = account.AccountType.ToString(),
                        name = client.FirstName + " " + client.LastName
                    });
                }
            }
            return allClients;
        }
        //[HttpGet,Route("api/adviser/clientaccounts")]
        //[Authorize(Roles=AuthorizationRoles.Role_Adviser)]
        //public List<CorporateActionClientAccountModel> GetClientAccountsForCompany(string companyTicker)
        //{
        //    return advisorRepo.GetClientAccountsForCompany(User.Identity.GetUserId(), companyTicker);
        //}
        //#endregion

        [HttpGet, Route("api/adviser/insertAssetsData")]
        public string insertAssetsData()
        {
            //edisRepo.InsertRandomDataIntoAssets();
            return "success";

        }

        [HttpPost, Route("api/adviser/getAllClientGroups")]
        [Authorize(Roles = AuthorizationRoles.Role_Adviser)]
        public List<ClientView> getAllAdviserClientsForGroup(ClientAccountCreationBindingModel clientGroupNumber)
        {
            var clientGroup = edisRepo.getClientGroupByGroupId(clientGroupNumber.clientGroup);              //Add this statement .............
            var clientList = edisRepo.GetClientsForGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);

            List<ClientView> clients = new List<ClientView>();
            foreach (var client in clientList)
            {
                if (client.ClientType == "person")
                {
                    clients.Add(new ClientView
                    {
                        id = client.Id,
                        name = client.FirstName + " " + client.LastName,
                    });
                }
                else
                {
                    clients.Add(new ClientView
                    {
                        id = client.Id,
                        name = client.EntityName,
                    });
                }

            }
            return clients;
        }



        [HttpGet, Route("api/adviser/accountType")]
        public List<ClientView> getAdviserAccountTypes()
        {
            var userid = User.Identity.GetUserId();

            List<ClientView> clients = new List<ClientView>();
            foreach (AccountType type in Enum.GetValues(typeof(AccountType)))
                clients.Add(new ClientView
                {
                    name = type.ToString(),
                });

            return clients;

        }



        [HttpPost, Route("api/adviser/createClientAccount")]
        [Authorize(Roles = AuthorizationRoles.Role_Adviser)]
        public void createClientAccount(ClientAccountCreationBindingModel model)
        {
            edisRepo.CreateNewClientAccountSync(model.client, model.accountName, model.accountType);

        }

        [HttpPost, Route("api/adviser/createGroupAccount")]
        [Authorize(Roles = AuthorizationRoles.Role_Adviser)]
        public void createClientGroupAccount(ClientAccountCreationBindingModel model)
        {
            edisRepo.CreateNewClientGroupAccountSync(model.clientGroup, model.accountName, model.accountType);
        }

        [HttpGet, Route("api/adviser/clients")]
        [Authorize(Roles = AuthorizationRoles.Role_Adviser)]
        public List<ClientView> getAllAdviserClients()
        {

            var userid = User.Identity.GetUserId();

            List<Domain.Portfolio.AggregateRoots.ClientGroup> groups = edisRepo.GetAllClientGroupsForAdviserSync(User.Identity.GetUserId(), DateTime.Now);
            List<Domain.Portfolio.AggregateRoots.Client> clients = new List<Domain.Portfolio.AggregateRoots.Client>();
            List<ClientView> views = new List<ClientView>();
            foreach (var group in groups)
            {
                clients.AddRange(group.GetClientsSync(DateTime.Now));
            }

            foreach (var client in clients)
            {
                views.Add(new ClientView
                {
                    id = client.Id,
                    name = client.FirstName + " " + client.LastName
                });
            }


            //return advisorRepo.GetAdvisorClients(userid);
            return views;

        }

        ////////[HttpGet, Route("api/adviser/clientgroupsTest")]
        ////////[Authorize(Roles = AuthorizationRoles.Role_Adviser)]
        ////////public async Task<List<ClientGroup>> getAllClientGroups()
        ////////{

        ////////    var userid = User.Identity.GetUserId();

        ////////    return await repo.GetAllClientGroupsForAdviser(userid, DateTime.Now);

        ////////}

        [HttpGet, Route("api/adviser/clientgroups")]
        [Authorize(Roles = AuthorizationRoles.Role_Adviser)]
        public List<ClientView> getAllClientGroups()
        {

            var userid = User.Identity.GetUserId();
            var adviser = edisRepo.GetAdviserSync(userid, DateTime.Now);
            var groups = adviser.GetAllClientGroupsSync(DateTime.Now);
            List<ClientView> clients = new List<ClientView>();
            foreach (var group in groups)
            {
                clients.Add(new ClientView
                {
                    id = group.Id,
                    name = group.GroupName
                });
            }
            return clients;


            //return advisorRepo.GetClientGroupsByAdviserId(userid);

        }
        [HttpGet, Route("api/adviser/businessRevenueBrief")]
        public BusinessPortfolioOverviewBriefModel GetBriefBusinessRevenue()
        {
            return advisorRepo.GetBusinessRevenueData(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/adviser/debtInstruments")]
        public BusinessPortfolioOverviewBriefModel GetInstrumentsData()
        {
            return advisorRepo.GetDebtInstrumentsData(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/adviser/insuranceStatistics")]
        public ProfileInsuranceStatisticsModel GetInsuranceStatistics()
        {
            List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
            List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

            List<ProfileInsuranceStatisticsGroup> InsuranceGroup = new List<ProfileInsuranceStatisticsGroup>();
            InsuranceGroup.Add(new ProfileInsuranceStatisticsGroup
            {
                name = "Asset Insurance",
                stat = new List<DataNameAmountPair>()
            });
            InsuranceGroup.Add(new ProfileInsuranceStatisticsGroup
            {
                name = "Persoanl Insurance",
                stat = new List<DataNameAmountPair>()
            });
            InsuranceGroup.Add(new ProfileInsuranceStatisticsGroup
            {
                name = "Liability Insurance",
                stat = new List<DataNameAmountPair>()
            });
            InsuranceGroup.Add(new ProfileInsuranceStatisticsGroup
            {
                name = "Miscellaneous Insurance",
                stat = new List<DataNameAmountPair>()
            });

            ProfileInsuranceStatisticsModel model = new ProfileInsuranceStatisticsModel
            {
                data = new List<DataNameAmountPair>(),
                group = InsuranceGroup,
            };


            List<LiabilityBase> liabilities = new List<LiabilityBase>();
            foreach (var account in groupAccounts)
            {
                liabilities.AddRange(account.GetLiabilitiesSync());
            }
            foreach (var account in clientAccounts)
            {
                liabilities.AddRange(account.GetLiabilitiesSync());
            }

            var insurancesGroups = liabilities.OfType<Insurance>().GroupBy(i => i.InsuranceType);
            foreach (var insuranceMetaGroup in insurancesGroups)
            {
                var insurance = insuranceMetaGroup.FirstOrDefault();

                switch (insuranceMetaGroup.Key)
                {
                    case InsuranceType.LiabilityInsurance:
                        model.group.SingleOrDefault(i => i.name == "Liability Insurance").stat.Add(new DataNameAmountPair { name = insurance.PolicyType.ToString(), amount = insurance.AmountInsured });
                        break;
                    case InsuranceType.AssetInsurance:
                        model.group.SingleOrDefault(i => i.name == "Asset Insurance").stat.Add(new DataNameAmountPair { name = insurance.PolicyType.ToString(), amount = insurance.AmountInsured });
                        break;
                    case InsuranceType.MiscellaneousInsurance:
                        model.group.SingleOrDefault(i => i.name == "Miscellaneous Insurance").stat.Add(new DataNameAmountPair { name = insurance.PolicyType.ToString(), amount = insurance.AmountInsured });
                        break;
                    case InsuranceType.PersoanlInsurance:
                        model.group.SingleOrDefault(i => i.name == "Persoanl Insurance").stat.Add(new DataNameAmountPair { name = insurance.PolicyType.ToString(), amount = insurance.AmountInsured });
                        break;
                }
                model.data.Add(new DataNameAmountPair { name = insurance.PolicyType.ToString(), amount = insurance.AmountInsured });
                model.total += insurance.AmountInsured;
            }

            return model;

            return advisorRepo.GetInsuranceStatisticsData(User.Identity.GetUserId());
        }


        [HttpGet, Route("api/adviser/worldMarkets")]
        public List<WordMarketItemModel> GetWorldMarkets()
        {
            return advisorRepo.GetWorldMarketData(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/adviser/currencies")]
        public List<WordMarketItemModel> GetCurrencies()
        {
            return advisorRepo.GetCurrencies(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/adviser/historicalrevenue")]
        public HistoricalRevenueModel GetHistoricalRevenue()
        {
            return advisorRepo.GetHistoricalRevenueData(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/adviser/investmentstat")]
        public BusinessStatDetailModel GetInvestmentStat()
        {
            return advisorRepo.GetInvestmentStat(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/adviser/lendingstat")]
        public BusinessStatDetailModel GetLendingStat()
        {
            return advisorRepo.GetLendingStat(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/adviser/InsuranceStat")]
        public InsuranceStatModel GetInsuranceStats()
        {
            return advisorRepo.GetInsuranceStatDetailed(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/adviser/clientPositionsMonitor")]
        public List<ClientPositionMonitorModel> GetClientPositionMonitor()
        {
            return advisorRepo.GetClientPositionMonitor(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/adviser/GeoLocations")]
        public List<GeoGraphicalLocations> GetGeoLocations()
        {
            return advisorRepo.GetGeoLocations(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/adviser/GeoDetails")]
        public GeoStatsModel GetGeoStat([FromUri] string[] locations)
        {
            if (locations == null)
            {
                return advisorRepo.GetGeoStats(User.Identity.GetUserId(), new[] { "" });
            }
            else
            {
                return advisorRepo.GetGeoStats(User.Identity.GetUserId(), new[] { "" });
            }
        }
        [HttpGet, Route("api/adviser/BusinessReenueDetails")]
        public BuisnessRevenueDetailsDataModel GetBusinessRevenueDetails()
        {
            return advisorRepo.GetBusinessRevenueDetails(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/adviser/ComplianceDetails")]
        public CompliantModel GetComplianceDetails()
        {
            return advisorRepo.GetComplianceDetails(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/adviser/reminders")]
        public ReminderModel GetReminders()
        {
            return advisorRepo.GetReminders(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/adviser/companyList")]
        public List<AnalysisCityBrief> GetCityList()
        {
            List<Equity> equities = edisRepo.GetAllEquities();

            List<AnalysisCityBrief> companies = new List<AnalysisCityBrief>();

            foreach (var equity in equities)
            {
                companies.Add(new AnalysisCityBrief
                {
                    id = equity.Id,
                    name = equity.Name
                });
            }

            return companies;
            //return advisorRepo.GetAnalysisCompaniesList(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/adviser/research/companyProfile")]
        public CompanyProfileDataItem GetCompanyProfile(string companyId)
        {
            Equity equity = edisRepo.GetAllEquities().SingleOrDefault(eq => eq.Id == companyId);

            CompanyProfileDataItem model = new CompanyProfileDataItem
            {
                id = equity.Id,
                ticker = equity.Ticker,
                fullName = equity.Name,
                closingPrice = equity.LatestPrice,
                sector = equity.Sector,
                assetClass = equity.EquityType.ToString(),
                changeAmount = edisRepo.GetResearchValueForEquitySync("changeAmount", equity.Ticker) == null ? 0 : (double)edisRepo.GetResearchValueForEquitySync("changeAmount", equity.Ticker),
                changeRatePercentage = edisRepo.GetResearchValueForEquitySync("changeRatePercentage", equity.Ticker) == null ? 0 : (double)edisRepo.GetResearchValueForEquitySync("changeRatePercentage", equity.Ticker),
                weeksDifferenceAmount = edisRepo.GetResearchValueForEquitySync("weeksDifferenceAmount", equity.Ticker) == null ? 0 : (double)edisRepo.GetResearchValueForEquitySync("weeksDifferenceAmount", equity.Ticker),
                weeksDifferenceRatePercentage = edisRepo.GetResearchValueForEquitySync("weeksDifferenceRatePercentage", equity.Ticker) == null ? 0 : (double)edisRepo.GetResearchValueForEquitySync("weeksDifferenceRatePercentage", equity.Ticker),
                suitabilityScore = edisRepo.GetResearchValueForEquitySync("suitabilityScore", equity.Ticker) == null ? 0 : (double)edisRepo.GetResearchValueForEquitySync("suitabilityScore", equity.Ticker),
                suitsTypeOfClients = edisRepo.GetStringResearchValueForEquitySync("suitsTypeOfClients", equity.Ticker),
                country = edisRepo.GetStringResearchValueForEquitySync("country", equity.Ticker),
                exchange = edisRepo.GetStringResearchValueForEquitySync("exchange", equity.Ticker),
                marketCapitalisation = edisRepo.GetStringResearchValueForEquitySync("marketCapitalisation", equity.Ticker),
                currencyType = edisRepo.GetStringResearchValueForEquitySync("currencyType", equity.Ticker),
                reasons = edisRepo.GetStringResearchValueForEquitySync("reasons", equity.Ticker),
                companyBriefing = edisRepo.GetStringResearchValueForEquitySync("companyBriefing", equity.Ticker),
                companyStrategies = edisRepo.GetStringResearchValueForEquitySync("companyStrategies", equity.Ticker),
                investment = edisRepo.GetStringResearchValueForEquitySync("investment", equity.Ticker),
                investmentName = edisRepo.GetStringResearchValueForEquitySync("investmentName", equity.Ticker),
                //priceDate = DateTime.Now,

                currentAnalysis = new CurrentAnalysisPayload
                {
                    metaProperties = new List<AnalysisPayloadMetaProperty>
                    {
                        new AnalysisPayloadMetaProperty{propertyName="baseInformation",displayName="Base Information"},
                        new AnalysisPayloadMetaProperty{propertyName="morningstar",displayName="Morningstar"},
                        new AnalysisPayloadMetaProperty{propertyName="brokerX",displayName="Broker X"},
                        new AnalysisPayloadMetaProperty{propertyName="ASX200Accumulation",displayName="ASX 200 Accumulation"},
                    },
                    groups = new List<AnalysisPayloadGroupModel>
                    {
                        new AnalysisPayloadGroupModel{
                            name="Recommendation",
                            data=new List<AnalysisPayloadGroupDataItem>{
                                new AnalysisPayloadGroupDataItem{
                                    name= "Current Short Term Recommendation",
                                    baseInformation= edisRepo.GetStringResearchValueForEquitySync("baseInformationShort", equity.Ticker),
                                    morningstar= edisRepo.GetStringResearchValueForEquitySync("morningstarShort", equity.Ticker),
                                    brokerX= edisRepo.GetStringResearchValueForEquitySync("brokerXShort", equity.Ticker),
                                    ASX200Accumulation= edisRepo.GetStringResearchValueForEquitySync("ASX200AccumulationShort", equity.Ticker),
                                },new AnalysisPayloadGroupDataItem{
                                    name= "Current Long Term Recommendation",
                                    baseInformation= edisRepo.GetStringResearchValueForEquitySync("baseInformationLong", equity.Ticker),
                                    morningstar= edisRepo.GetStringResearchValueForEquitySync("morningstarLong", equity.Ticker),
                                    brokerX= edisRepo.GetStringResearchValueForEquitySync("brokerXLong", equity.Ticker),
                                    ASX200Accumulation= edisRepo.GetStringResearchValueForEquitySync("ASX200AccumulationLong", equity.Ticker),
                                },new AnalysisPayloadGroupDataItem{
                                    name= "Price Target",
                                    baseInformation= edisRepo.GetStringResearchValueForEquitySync("baseInformationPrice", equity.Ticker),
                                    morningstar= edisRepo.GetStringResearchValueForEquitySync("morningstarPrice", equity.Ticker),
                                    brokerX= edisRepo.GetStringResearchValueForEquitySync("brokerXPrice", equity.Ticker),
                                    ASX200Accumulation= edisRepo.GetStringResearchValueForEquitySync("ASX200AccumulationPrice", equity.Ticker),
                                },

                            }
                        },new AnalysisPayloadGroupModel{
                            name="Income",
                            data=new List<AnalysisPayloadGroupDataItem>{
                                new AnalysisPayloadGroupDataItem{
                                    name= "Current Short Term Recommendation",
                                    baseInformation= "base information",
                                    morningstar= "Morning star information",
                                    brokerX= "brokerX information",
                                    ASX200Accumulation= "Accumulation Details"
                                },new AnalysisPayloadGroupDataItem{
                                    name= "Current Long Term Recommendation",
                                    baseInformation= "base information",
                                    morningstar= "Morning star information",
                                    brokerX= "brokerX information",
                                    ASX200Accumulation= "Accumulation Details"
                                },new AnalysisPayloadGroupDataItem{
                                    name= "Price Target",
                                    baseInformation= "base information",
                                    morningstar= "Morning star information",
                                    brokerX= "brokerX information",
                                    ASX200Accumulation= "Accumulation Details"
                                },
                            }
                        }
                    }
                },
            };

            return model;
            return advisorRepo.GetCompanyProfile(User.Identity.GetUserId(), companyId);
        }
    }
}
