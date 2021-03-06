﻿using System;
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
using Domain.Portfolio.TransactionModels;
using Domain.Portfolio.Entities.CreationModels.Transaction;
using EDISAngular.Models.ServiceModels.TransactionModels;
using Domain.Portfolio.Entities.CreationModels.Cost;

namespace EDISAngular.APIControllers
{
    public class AdviserController : ApiController
    {
        private AdviserRepository advisorRepo;
        private EdisRepository edisRepo;
        private Random rdm = new Random();

        public AdviserController()
        {
            edisRepo = new EdisRepository();
            advisorRepo = new AdviserRepository();
            //randomMoney = new Random();
        }
        [HttpGet, Route("api/adviser/accountNumber")]
        public string getAdviserAccountNumber()
        {
            var userid = User.Identity.GetUserId();
            return edisRepo.GetAdviserSync(userid, DateTime.Now).AdviserNumber;

        }


        [HttpPost, Route("api/adviser/getAllAccountForGroup")]
        public List<AccountView> getAllCertainGroupAllAssociatedAccount(ClientAccountCreationBindingModel ClientGroupID) {
            ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(ClientGroupID.clientGroup);
            List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
            List<ClientAccount> clientAccounts = new List<ClientAccount>();
            clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

            List<AccountView> result = new List<AccountView>();
            accounts.ForEach(a => result.Add(new AccountView { id = a.Id, name = a.AccountNameOrInfo, accountCatagory = AccountCatergories.GroupAccount.ToString()}));
            clientAccounts.ForEach(a => result.Add(new AccountView { id = a.Id, name = a.AccountNameOrInfo, accountCatagory = AccountCatergories.ClientAccount.ToString()}));

            //var ClientGroupId = ClientGroupID.clientGroup;
            ////Here we retrieve the group account then add to the result
            //List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupByIdSync(ClientGroupId, DateTime.Now);
            //foreach (var groupAccount in accounts) {
            //    result.Add(new AccountView {
            //        id = groupAccount.Id,
            //        name = groupAccount.AccountNameOrInfo
            //    });
            //}
            ////then we get all the clients' accounts
            //ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(ClientGroupId);
            //List<ClientAccount> clientAccounts = new List<ClientAccount>();
            //clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));
            ////add to the result
            //foreach (var clientAccount in clientAccounts) {
            //    result.Add(new AccountView {
            //            id = clientAccount.Id,
            //            name = clientAccount.AccountNameOrInfo
            //    });
            //}
            return result;
          
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
                        shareAmount = client.FirstName + " " + client.LastName
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
            edisRepo.insertData3();

            return "success";

        }
         

        [HttpPost, Route("api/adviser/makeEquityTransactions")]
        public IHttpActionResult adviserMakeEquityTransactions(EquityTransactionModel model) {
            edisRepo.AdviserMakeEquityTransactions(model);    
            return Ok();
        }


        [HttpPost, Route("api/adviser/makeBondTransactions")]
        public IHttpActionResult adviserMakeBondsTransactions(EquityTransactionModel model)
        {
            edisRepo.AdviserMakeBondsTransactions(model);
            return Ok();
        }

        //"api/adviser/makeInsuranceTransactions"
        [HttpPost, Route("api/adviser/makeInsuranceTransactions")]
        public IHttpActionResult adviserMakeInsuranceTransactions(InsuranceTransactionModel model)
        {
            // edisRepo.AdviserMakeBondsTransactions(model);
            AccountBase account = null;

            if (model.account.accountCatagory == AccountCatergories.GroupAccount.ToString())
            {
                account = edisRepo.GetGroupAccountById(model.account.id);
            }
            else
            {
                account = edisRepo.GetClientAccountById(model.account.id);
            }

            account.MakeTransactionSync(new InsuranceTransactionCreation()
            {
                AmountInsured = Convert.ToDouble(model.insuranceAmount),
                EntitiesInsured = model.insuredEntity,
                ExpiryDate = model.expiryDate,
                GrantedOn = model.grantedDate,
                InsuranceType = model.insuranceType,
                NameOfPolicy = model.insuranceType.ToString(),
                PolicyType = model.policyType,
                Premium = Convert.ToDouble(model.premium),
                IsAcquire = model.isAquired,
                Issuer = model.issuer,
                PolicyAddress = model.policyAddress,
                PolicyNumber = model.policyNumber
            });

            return Ok();
        }


        [HttpPost, Route("api/adviser/makePropertyTransactions")]
        public IHttpActionResult adviserMakePropertyTransactions(PropertyTransactionModel model) {
            AccountBase account = null;

            if (model.Account.accountCatagory == AccountCatergories.GroupAccount.ToString()) {
                account = edisRepo.GetGroupAccountById(model.Account.id);
            } else {
                account = edisRepo.GetClientAccountById(model.Account.id);
            }

            List<TransactionFeeRecordCreation> feeRecords = new List<TransactionFeeRecordCreation>();
            feeRecords.Add(new TransactionFeeRecordCreation {
                Amount = model.TransactionFee,
                TransactionExpenseType = TransactionExpenseType.LiabilityProcessingFee
            });

            HomeLoanTransactionCreation homeLoan = new HomeLoanTransactionCreation {
                GrantedOn = model.GrantedDate,
                LoanAmount = model.LoanAmount,
                LoanRate = model.LoanRate,
                TypeOfMortgageRates = (TypeOfMortgageRates)Enum.Parse(typeof(TypeOfMortgageRates), model.TypeOfRate),
                ExpiryDate = model.ExpiryDate,
                LoanRepaymentType = LoanRepaymentType.DirectDebt,
                IsAcquire = true,
                Institution = model.Institution
            };

            account.MakeTransactionSync(new PropertyTransactionCreation {
                FullAddress = model.PropertyAddress,
                Price = model.PropertyPrice,
                PropertyType = model.PropertyType.ToString(),
                TransactionDate = model.TransactionDate,
                FeesRecords = feeRecords,
                IsBuy = true,
                loan = homeLoan
            });

            return Ok();
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
            foreach (AccountType type in Enum.GetValues(typeof(AccountType))) {
                clients.Add(new ClientView
                {
                    name = type.ToString(),
                });
            }
            return clients;
        }


        [HttpPost, Route("api/adviser/createClientAccount")]
        [Authorize(Roles = AuthorizationRoles.Role_Adviser)]
        public void createClientAccount(ClientAccountCreationBindingModel model)
        {
            edisRepo.CreateNewClientAccountSync(model.client, model.accountName, model.accountType, model.marginLenderId);

        }

        [HttpPost, Route("api/adviser/createGroupAccount")]
        [Authorize(Roles = AuthorizationRoles.Role_Adviser)]
        public void createClientGroupAccount(ClientAccountCreationBindingModel model)
        {
            edisRepo.CreateNewClientGroupAccountSync(model.clientGroup, model.accountName, model.accountType, model.marginLenderId);
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



        [HttpGet, Route("api/adviser/marginLenders")]
        [Authorize(Roles = AuthorizationRoles.Role_Adviser)]
        public List<ClientView> getAllMarginLenders() {

            var userid = User.Identity.GetUserId();
            var adviser = edisRepo.GetAdviserSync(userid, DateTime.Now);

            var lenders = edisRepo.GetAllMarginLenders();
            List<ClientView> lenderView = new List<ClientView>();
            foreach (var lender in lenders) {
                lenderView.Add(new ClientView {
                    id = lender.LenderId,
                    name = lender.LenderName
                });
            }
            return lenderView;
        }

        [HttpGet, Route("api/adviser/businessRevenueBrief")]
        public BusinessPortfolioOverviewBriefModel GetBriefBusinessRevenue()
        {
            return advisorRepo.GetBusinessRevenueData(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/adviser/debtInstruments")]
        public BusinessPortfolioOverviewBriefModel GetInstrumentsData()
        {
            //List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
            //List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);



            //List<LiabilityBase> liabilities = new List<LiabilityBase>();
            //foreach (var account in groupAccounts)
            //{
            //    liabilities.AddRange(account.GetLiabilitiesSync());
            //}
            //foreach (var account in clientAccounts)
            //{
            //    liabilities.AddRange(account.GetLiabilitiesSync());
            //}
            //var insurancesGroups = liabilities.OfType<Insurance>().GroupBy(i => i.InsuranceType);
            //var mortgages = liabilities.OfType<MortgageAndHomeLiability>().GroupBy(m => m.CurrencyType);
            //var lanings = liabilities.OfType<MarginLending>().GroupBy(l => l.Asset);
            //double sumInsure = 0;
            //double sumMortgage = 0;
            //double sumLanding = 0;
            //foreach (var insurancesGroup in insurancesGroups) {
            //    var insure = insurancesGroup.FirstOrDefault();
            //    sumInsure += insure.AmountInsured;
            //}
            //foreach (var mortgage in mortgages) {
            //    var mor = mortgage.FirstOrDefault();
            //    sumMortgage += mor.CurrentBalance;
            //}
            //foreach (var landing in lanings) {
            //    var land = landing.FirstOrDefault();
            //}

            //var model = new BusinessPortfolioOverviewBriefModel
            //{
            //    data = new List<DataNameAmountPair>
            // {
            //     new DataNameAmountPair{name="Mortgage & Investment Home Loans", amount=randomMoney()},
            //     new DataNameAmountPair{name="Commercial Loans", amount=randomMoney()},
            //     new DataNameAmountPair{name="Margin Lending Loans", amount=randomMoney()},
            //     new DataNameAmountPair{name="Personal & Credit Card Loans", amount=randomMoney()},
            //     new DataNameAmountPair{name="Lending & Debt Statistics", amount=randomMoney()},
            // },

            //};
            //return model;
            var con = new PortfolioOverviewController();
            var model = con.GenerateSummary(con.getAssetsAndLiabilitiesForAdviser(null));

            var result = new BusinessPortfolioOverviewBriefModel
            {
                data = model.liability.data,
                total = model.liability.total
               
            };
            return result;
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

            //return advisorRepo.GetInsuranceStatisticsData(User.Identity.GetUserId());
        }


        [HttpGet, Route("api/adviser/allBondTickers")]
        public List<TickerBriefModel> GetAllBondTickers()
        {
            var bonds = edisRepo.GetAllBonds();
            var result = new List<TickerBriefModel>();
            foreach (var bond in bonds)
            {
                result.Add(new TickerBriefModel { tickerName = bond.Ticker, tickerNumber = bond.Ticker });
            }
            return result;
        }

        [HttpGet, Route("api/adviser/allProperties")]
        public List<PropertyBriefModel> GetAllProperties() {
            var properties = edisRepo.GetAllPropertyForApi();
            var result = new List<PropertyBriefModel>();
            foreach (var property in properties) {
                result.Add(new PropertyBriefModel { FullAddress = property.FullAddress, id = property.GooglePlaceId});
            }
            return result;
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

            return companies.OrderBy(c => c.name).ToList();
            //return advisorRepo.GetAnalysisCompaniesList(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/adviser/periodList")]
        public List<StockPeriodModel> GetPeriodList()
        {
            List<StockPeriodModel> periods = new List<StockPeriodModel>();

            foreach (Period period in Enum.GetValues(typeof(Period))) {
                periods.Add(new StockPeriodModel
                {
                    id = period.ToString(),
                    name = edisRepo.GetEnumDescription(period)
                });
            }

            return periods;
        }
        [HttpGet, Route("api/adviser/research/companyProfile")]
        public CompanyProfileDataItem GetCompanyProfile(string companyId)
        {
            Equity equity = edisRepo.getEquityById(companyId);
            DateTime? priceDate = edisRepo.GetLastPriceDateForEquity(companyId);

            

            CompanyProfileDataItem model = new CompanyProfileDataItem
            {
                id = equity.Id,
                ticker = equity.Ticker,
                fullName = equity.Name,
                closingPrice = equity.LatestPrice,
                sector = equity.Sector,
                priceDate = priceDate == null? DateTime.MinValue : (DateTime)priceDate,
                assetClass = equity.EquityType.ToString(),
                changeAmount = edisRepo.GetResearchValueForEquitySync(ResearchValueKeys.changeAmount, equity.Ticker) == null ? 0 : (double)edisRepo.GetResearchValueForEquitySync(ResearchValueKeys.changeAmount, equity.Ticker),
                changeRatePercentage = edisRepo.GetResearchValueForEquitySync(ResearchValueKeys.changeRatePercentage, equity.Ticker) == null ? 0 : (double)edisRepo.GetResearchValueForEquitySync(ResearchValueKeys.changeRatePercentage, equity.Ticker),
                weeksDifferenceAmount = (double)edisRepo.GetResearchValueForEquitySync(ResearchValueKeys.fivtyTwoWkHighPrice, equity.Ticker),
                weeksDifferenceRatePercentage = (double)(edisRepo.GetResearchValueForEquitySync(ResearchValueKeys.fivtyTwoWkLowPrice, equity.Ticker) / edisRepo.GetResearchValueForEquitySync(ResearchValueKeys.fivtyTwoWkHighPrice, equity.Ticker) == null ? 1 : edisRepo.GetResearchValueForEquitySync(ResearchValueKeys.fivtyTwoWkHighPrice, equity.Ticker)),
                suitabilityScore = equity.GetRating().TotalScore,
                suitsTypeOfClients = edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.suitsTypeOfClients, equity.Ticker),
                country = edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.Country, equity.Ticker),
                exchange = edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.Exchange, equity.Ticker),
                marketCapitalisation = edisRepo.GetResearchValueForEquitySync(ResearchValueKeys.MarketCap, equity.Ticker).ToString(),
                currencyType = edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.Currency, equity.Ticker),
                reasons = edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.reasons, equity.Ticker),
                companyBriefing = edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.companyBriefing, equity.Ticker),
                companyStrategies = edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.companyStrategies, equity.Ticker),
                investment = edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.investment, equity.Ticker),
                investmentName = edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.investmentName, equity.Ticker),
                

                indexData = new List<CompanyProfileIndexData>(),

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
                                    baseInformation= edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.baseInformationShort, equity.Ticker),
                                    morningstar= edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.morningstarShort, equity.Ticker),
                                    brokerX= edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.brokerXShort, equity.Ticker),
                                    ASX200Accumulation= edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.ASX200AccumulationShort, equity.Ticker),
                                },new AnalysisPayloadGroupDataItem{
                                    name= "Current Long Term Recommendation",
                                    baseInformation= edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.baseInformationLong, equity.Ticker),
                                    morningstar= edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.morningstarLong, equity.Ticker),
                                    brokerX= edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.brokerXLong, equity.Ticker),
                                    ASX200Accumulation= edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.ASX200AccumulationLong, equity.Ticker),
                                },new AnalysisPayloadGroupDataItem{
                                    name= "Price Target",
                                    baseInformation= edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.baseInformationPrice, equity.Ticker),
                                    morningstar= edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.morningstarPrice, equity.Ticker),
                                    brokerX= edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.brokerXPrice, equity.Ticker),
                                    ASX200Accumulation= edisRepo.GetStringResearchValueForEquitySync(ResearchValueKeys.ASX200AccumulationPrice, equity.Ticker),
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


            List<AssetPrice> assetPrices = edisRepo.getPricesByEquityIdAndDates(companyId, Period.LastSixMonths.ToString());
            
            foreach(var price in assetPrices){

                model.indexData.Add(new CompanyProfileIndexData { 
                    company = price.Price.Value,
                    month = price.CreatedOn.Value.Date.ToString("yy-MM-dd"),
                    date = (DateTime)price.CreatedOn
                });
            }

            return model;
            //return advisorRepo.GetCompanyProfile(User.Identity.GetUserId(), companyId);
        }



        [HttpPost, Route("api/adviser/CouponDividend")]
        public IHttpActionResult InsertCouponDividend(DevidendCreationModel model)
        {
            edisRepo.InsertCouponDividend(model);
            return Ok();
        }

        [HttpPost, Route("api/adviser/JustDividend")]
        public IHttpActionResult InsertJustDividend(DevidendCreationModel model)
        {
            edisRepo.InsertJustDividend(model);
            return Ok();
        }

        [HttpPost, Route("api/adviser/InterestDividend")]
        public IHttpActionResult InsertInterestDividend(DevidendCreationModel model)
        {
            edisRepo.InsertInterestDividend(model);
            return Ok();
        }

        [HttpPost, Route("api/adviser/RentalDividend")]
        public IHttpActionResult InsertRentalDividend(DevidendCreationModel model)
        {
            edisRepo.InsertRentalDividend(model);
            return Ok();
        }




    }
}
