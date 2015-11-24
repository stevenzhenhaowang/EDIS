using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EDISAngular.Infrastructure.DatabaseAccess;
using EDISAngular.Models.ServiceModels.CorporateActions;
using EDISAngular.Models.ServiceModels;
using Microsoft.AspNet.Identity;
using EDISAngular.Models.ServiceModels.PortfolioModels;
using EDISAngular.Models.ViewModels;
using SqlRepository;
using Domain.Portfolio.AggregateRoots;
using Domain.Portfolio.AggregateRoots.Accounts;
using Domain.Portfolio.AggregateRoots.Liability;
using Shared;
using Domain.Portfolio.Services;
using Domain.Portfolio.AggregateRoots.Asset;





namespace EDISAngular.APIControllers
{
    public class PortfolioMarginLendingController : ApiController
    {
        private CommonReferenceDataRepository comRepo = new CommonReferenceDataRepository();
        private PortfolioRepository PortRepo = new PortfolioRepository();
        private EdisRepository edisRepo = new EdisRepository();


        [HttpGet, Route("api/Adviser/MarginLendingPortfolio/RatingInfo")]
        public PortfolioRatingModel GetRatingInfo_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return PortRepo.Overview_GetPortfolioRating_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return PortRepo.Overview_GetPortfolioRating_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/MarginLendingPortfolio/RatingInfo")]
        public PortfolioRatingModel GetRatingInfo_Client()
        {
            return PortRepo.Overview_GetPortfolioRating_Client(User.Identity.GetUserId());
        }




        [HttpGet, Route("api/adviser/MarginLendingPortfolio/CashflowDetails")]
        public object GetCashflow_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return PortRepo.Overview_GetCashflowSummary_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return PortRepo.Overview_GetCashflowSummary_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/MarginLendingPortfolio/CashflowDetails")]
        public object GetCashflow_Client()
        {
            return PortRepo.Overview_GetCashflowSummary_Client(User.Identity.GetUserId());
        }


        //[HttpGet, Route("api/Adviser/MarginLendingPortfolio/Stats")]
        //public object GetStats_Adviser(string clientUserId = null)
        //{
        //    if (string.IsNullOrEmpty(clientUserId))
        //    {
        //        return PortRepo.Overview_GetQuickStats_Adviser(User.Identity.GetUserId());
        //    }
        //    else
        //    {
        //        return PortRepo.Overview_GetQuickStats_Client(clientUserId);
        //    }
        //}
        //[HttpGet, Route("api/Client/MarginLendingPortfolio/Stats")]
        //public object GetStats_Client()
        //{
        //    return PortRepo.MarginLending_GetQuickStats_Client(User.Identity.GetUserId());
        //}




        //[HttpGet, Route("api/Adviser/MarginLendingPortfolio/AccountLoanInfo")]
        //public object GetAccountLoanInfo(string clientAccountNumber)
        //{
        //    return PortRepo.MarginLending_GetLoanDetailsForClientAccount(clientAccountNumber);
        //}



        //[HttpGet, Route("api/Adviser/MarginLendingPortfolio/allCompanies")]
        //public object GetAllCompanyProfiles_Adviser(string clientUserId = null)
        //{
        //    if (string.IsNullOrEmpty(clientUserId))
        //    {
        //        return PortRepo.MarginLending_GetAllCompanyProfiles_Adviser(User.Identity.GetUserId());
        //    }
        //    else
        //    {
        //        return PortRepo.MarginLending_GetAllCompanyProfiles_Client(clientUserId);
        //    }

        //}
        //[HttpGet, Route("api/Client/MarginLendingPortfolio/allCompanies")]
        //public object GetAllCompanyProfiles_Client()
        //{
        //    return PortRepo.MarginLending_GetAllCompanyProfiles_Client(User.Identity.GetUserId());

        //}



        //[HttpGet, Route("api/Adviser/MarginLendingPortfolio/AccountLoanCompanyDetails")]
        //public object GetAccountCompanyDetails(string clientAccountNumber)
        //{
        //    return PortRepo.MarginLending_GetAccountMarginLoanDetails(User.Identity.GetUserId());
        //}


        //[HttpGet, Route("api/Adviser/MarginLendingPortfolio/SuitabilityDetails")]
        //public object GetSuitabilityDetails_Adviser(string clientUserId = null)
        //{
        //    if (string.IsNullOrEmpty(clientUserId))
        //    {
        //        return PortRepo.MarginLending_GetPortfolioSuitability_Adviser(User.Identity.GetUserId());
        //    }
        //    else
        //    {
        //        return PortRepo.MarginLending_GetPortfolioSuitability_Client(clientUserId);
        //    }

        //}

        //[HttpGet, Route("api/Client/MarginLendingPortfolio/SuitabilityDetails")]
        //public object GetSuitabilityDetails_Client()
        //{
        //    return PortRepo.MarginLending_GetPortfolioSuitability_Client(User.Identity.GetUserId());
        //}




        //[HttpGet, Route("api/Adviser/MarginLendingPortfolio/ImpactToCashflow")]
        //public object GetImpact_Adviser(string clientUserId = null)
        //{
        //    if (string.IsNullOrEmpty(clientUserId))
        //    {
        //        return PortRepo.MarginLending_GetImpactToCashflow_Adviser(User.Identity.GetUserId());
        //    }
        //    else
        //    {
        //        return PortRepo.MarginLending_GetImpactToCashflow_Client(clientUserId);
        //    }

        //}

        //[HttpGet, Route("api/Client/MarginLendingPortfolio/ImpactToCashflow")]
        //public object GetImpact_Client()
        //{
        //    return PortRepo.MarginLending_GetImpactToCashflow_Client(User.Identity.GetUserId());
        //}


        //[HttpGet, Route("api/Adviser/MarginLendingPortfolio/TypeOfGearing")]
        //public object GetGearing_Adviser(string clientUserId = null)
        //{
        //    if (string.IsNullOrEmpty(clientUserId))
        //    {
        //        return PortRepo.MarginLending_GetGearingStrategy_Adviser(User.Identity.GetUserId());
        //    }
        //    else
        //    {
        //        return PortRepo.MarginLending_GetGearingStrategy_Client(clientUserId);
        //    }

        //}

        //[HttpGet, Route("api/Client/MarginLendingPortfolio/TypeOfGearing")]
        //public object GetGearing_Client()
        //{
        //    return PortRepo.MarginLending_GetGearingStrategy_Client(User.Identity.GetUserId());
        //}


        [HttpGet, Route("api/Adviser/MarginLendingPortfolio/Diversification")]
        public object GetDiversification_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return PortRepo.AustralianEquity_GetDiversification_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return PortRepo.AustralianEquity_GetDiversification_Client(clientUserId);
            }

        }

        [HttpGet, Route("api/Client/MarginLendingPortfolio/Diversification")]
        public object GetDiversification()
        {
            return PortRepo.AustralianEquity_GetDiversification_Client(User.Identity.GetUserId());
        }


        [HttpGet, Route("api/Adviser/MarginLendingPortfolio/ClientAccountsForGroup")]
        public List<ClientView> GetClientAccountsForGroup(string clientGroupId = null) {
            List<ClientView> views = new List<ClientView>();
            if (string.IsNullOrEmpty(clientGroupId)) {
                
            } else{
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = clientGroup.GetAccountsSync(DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

                accounts.ForEach(a => views.Add(new ClientView { id = a.Id, name = a.AccountNameOrInfo, accountCatergory = AccountCatergories.GroupAccount.ToString() }));
                clientAccounts.ForEach(a => views.Add(new ClientView { id = a.Id, name = a.AccountNameOrInfo, accountCatergory = AccountCatergories.ClientAccount.ToString() }));

            }
            return views;
        }

        [HttpGet, Route("api/Client/MarginLendingPortfolio/ClientAccountsForClient")]
        public List<ClientView> GetClientAccountsForClient() {
            List<ClientView> views = new List<ClientView>();
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id) {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(edisRepo.GetAccountsForClientSync(c.ClientNumber, DateTime.Now)));

                groupAccounts.ForEach(a => views.Add(new ClientView {
                    id = a.Id,
                    name = a.AccountNameOrInfo,
                    accountCatergory = AccountCatergories.GroupAccount.ToString()
                }));

                clientAccounts.ForEach(a => views.Add(new ClientView { 
                    id = a.Id,
                    name = a.AccountNameOrInfo,
                    accountCatergory = AccountCatergories.ClientAccount.ToString()
                }));

            } else {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                accounts.ForEach(a => views.Add(new ClientView { 
                    id = a.Id,
                    name = a.AccountNameOrInfo,
                    accountCatergory = AccountCatergories.ClientAccount.ToString()
                }));
            }
            return views;
        }


        [HttpGet, Route("api/Adviser/MarginLendingPortfolio/ProfolioDetails")]
        public MarginLendingPortfolioDetailsModel GetPortfolioDetailsForGroups(string clientGroupId = null) {
            if (clientGroupId == null) {
                return null;
            } else {
                return GenerateProtfolioDetailsModel(getAllAssetForAdviser(clientGroupId), clientGroupId);
            }
        }

        [HttpGet, Route("api/Adviser/MarginLendingPortfolio/AccountPortfolio")]
        public MarginLendingAccountPortfolioDetails GetAccountPortfolioDetails(string accountId = null, string accountCatergory = null) {
            return GenerateAccountPortfolioDetailsModel(accountId, accountCatergory);
        }

        [HttpGet, Route("api/Client/MarginLendingPortfolio/AccountPortfolio")]
        public MarginLendingAccountPortfolioDetails GetAccountPortfolioDetailsForClient(string accountId = null, string accountCatergory = null) {
            return GenerateAccountPortfolioDetailsModel(accountId, accountCatergory);
        }

        public MarginLendingAccountPortfolioDetails GenerateAccountPortfolioDetailsModel(string accountId, string accountCatergory) {
            MarginLendingAccountPortfolioDetails model = new MarginLendingAccountPortfolioDetails { data = new List<MarginLendingAccountPortfolioItem>(), marginLenders = new List<MarginLendersDetails>() };
            
            AccountBase account = null;
            if(accountCatergory == AccountCatergories.ClientAccount.ToString()){
                account = edisRepo.GetClientAccountById(accountId);
            }else if(accountCatergory == AccountCatergories.GroupAccount.ToString()){
                account = edisRepo.GetGroupAccountById(accountId);
            }

            List<AssetBase> allEquities = account.GetEquities().ToList();

            allEquities.ForEach(e =>
            {
                Equity equity = (Equity)e;
                MarginLending lending = edisRepo.GetMarginLendingForAccountAsset(e.Id, account.AccountNumber);
                double? maxRatio = edisRepo.GetMaxRatio(equity.Ticker, account.MarginLenderId);
                double marketValue = e.GetTotalMarketValue();

                if (lending != null) {
                    model.data.Add(new MarginLendingAccountPortfolioItem {
                        ticker = equity.Ticker,
                        companyName = equity.Name,
                        marketValue = marketValue,
                        loanAmount = lending.LoanAmount,
                        loanValueRatio = lending.LoanValueRatio * 100,
                        maxLoanValueRatio = maxRatio == null ? 0 : (double)maxRatio * 100,
                        netCostValue = lending.NetCostValue
                    });

                } else {
                    model.data.Add(new MarginLendingAccountPortfolioItem {
                        ticker = equity.Ticker,
                        companyName = equity.Name,
                        marketValue = marketValue,
                        loanAmount = 0,
                        loanValueRatio = 0,
                        maxLoanValueRatio = maxRatio == null ? 0 : (double)maxRatio * 100,
                        netCostValue = marketValue
                    });
                }

                var marginLenders = edisRepo.GetMarginLendersByTicker(equity.Ticker);

                marginLenders.ForEach(l => {
                    model.marginLenders.Add(new MarginLendersDetails {
                        companyName = l.LenderName,
                        maxLoanValueRatio = l.Ratios.Count == 0 ? 0 : l.Ratios.OrderByDescending(r => r.CreatedOn).SingleOrDefault().MaxRatio * 100,
                        ticker = equity.Ticker
                    });
                });
            });
            return model;
        }

        public MarginLendingPortfolioDetailsModel GenerateProtfolioDetailsModel(List<AssetBase> assets, string clientGroupId) {
            MarginLendingPortfolioDetailsModel model = new MarginLendingPortfolioDetailsModel{ data = new List<MarginLendingPortfolioDetailsItem>() };

            foreach (AssetTypes assetType in Enum.GetValues(typeof(AssetTypes))) {
                List<MarginLending> lendings = edisRepo.GetAllMarginLendingByAssetType(assetType, clientGroupId);

                if(lendings == null){
                    return null;
                }

                double marketValue = 0;

                switch (assetType) {
                    case AssetTypes.AustralianEquity:
                        marketValue = assets.GetTotalMarketValue_ByAssetType<AustralianEquity>();
                        break;
                    case AssetTypes.InternationalEquity:
                        marketValue = assets.GetTotalMarketValue_ByAssetType<InternationalEquity>();
                        break;
                    case AssetTypes.ManagedInvestments:
                        marketValue = assets.GetTotalMarketValue_ByAssetType<ManagedInvestment>();
                        break;
                    case AssetTypes.DirectAndListedProperty:
                        marketValue = assets.GetTotalMarketValue_ByAssetType<DirectProperty>();
                        break;
                    case AssetTypes.FixedIncomeInvestments:
                        marketValue = assets.GetTotalMarketValue_ByAssetType<FixedIncome>();
                        break;
                    case AssetTypes.CashAndTermDeposit:
                        marketValue = assets.GetTotalMarketValue_ByAssetType<Cash>();
                        break;
                }

                if (lendings.Count != 0) {
                    model.data.Add(new MarginLendingPortfolioDetailsItem {
                        assetCatargory = edisRepo.GetEnumDescription(assetType),
                        loanAmount = lendings.Sum(l => l.LoanAmount),
                        loanValueRatio = lendings.Average(l => l.LoanValueRatio) * 100,
                        marketValue = marketValue,
                        netCostValue = lendings.Sum(l => l.LoanValueRatio == 0? l.Asset.GetTotalMarketValue() : l.LoanAmount / l.LoanValueRatio - l.LoanAmount),
                        //maxLoanValueRatio = maxLvr == null ? 0 : (double)maxLvr
                    });
                } else{
                    model.data.Add(new MarginLendingPortfolioDetailsItem {
                        assetCatargory = edisRepo.GetEnumDescription(assetType),
                        marketValue = marketValue,
                        loanAmount = 0,
                        loanValueRatio = 0,
                        netCostValue = marketValue,
                        //maxLoanValueRatio = maxLvr == null ? 0 : (double)maxLvr
                    });
                }
            }
            return model;
        }

        public List<LiabilityBase> getMarginLendingLiabilitiesForAdviser(string clientGroupId) {
            List<LiabilityBase> liabilities = new List<LiabilityBase>();

            if (string.IsNullOrEmpty(clientGroupId)) {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                groupAccounts.ForEach(a => liabilities.AddRange(a.GetLiabilitiesSync().OfType<MarginLending>().Cast<LiabilityBase>().ToList()));
                clientAccounts.ForEach(a => liabilities.AddRange(a.GetLiabilitiesSync().OfType<MarginLending>().Cast<LiabilityBase>().ToList()));
            } else {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = clientGroup.GetAccountsSync(DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

                accounts.ForEach(a => liabilities.AddRange(a.GetLiabilitiesSync().OfType<MarginLending>().Cast<LiabilityBase>().ToList()));
                clientAccounts.ForEach(a => liabilities.AddRange(a.GetLiabilitiesSync().OfType<MarginLending>().Cast<LiabilityBase>().ToList()));
            }

            return liabilities;
        }

        public List<LiabilityBase> getMarginLendingLiabilitiesForClient() {
            List<LiabilityBase> liabilities = new List<LiabilityBase>();

            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id) {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                groupAccounts.ForEach(a => liabilities.AddRange(a.GetLiabilitiesSync().OfType<MarginLending>().Cast<LiabilityBase>().ToList()));
                clientAccounts.ForEach(a => liabilities.AddRange(a.GetLiabilitiesSync().OfType<MarginLending>().Cast<LiabilityBase>().ToList()));
            } else {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                accounts.ForEach(a => liabilities.AddRange(a.GetLiabilitiesSync().OfType<MarginLending>().Cast<LiabilityBase>().ToList()));
            }
            return liabilities;
        }

        public List<AssetBase> getAllAssetForAdviser(string clientGroupId) {
            List<AssetBase> assets = new List<AssetBase>();

            if (string.IsNullOrEmpty(clientGroupId)) {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                groupAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync()));
            } else {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = clientGroup.GetAccountsSync(DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

                accounts.ForEach(a => assets.AddRange(a.GetAssetsSync()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync()));
            }
            return assets;
        }

        public List<AssetBase> getAllAssetForClient() {
            List<AssetBase> assets = new List<AssetBase>();

            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id) {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

                groupAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync()));
            } else {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                accounts.ForEach(a => assets.AddRange(a.GetAssetsSync()));
            }

            return assets;
        }
    }
}
