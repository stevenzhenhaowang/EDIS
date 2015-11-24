using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EDISAngular.Models.ServiceModels;
using EDISAngular.Infrastructure.DatabaseAccess;
using EDISAngular.Models.ServiceModels.PortfolioModels;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Domain.Portfolio.AggregateRoots.Accounts;
using SqlRepository;
using Domain.Portfolio.AggregateRoots.Asset;
using Domain.Portfolio.AggregateRoots;
using Domain.Portfolio.Services;
using Shared;
using Domain.Portfolio.Values.Cashflow;

namespace EDISAngular.APIControllers
{
    public class PortfolioCashTermDepositController : ApiController
    {
        private PortfolioRepository repo = new PortfolioRepository();
        private EdisRepository edisRepo = new EdisRepository();
        [HttpGet, Route("api/Adviser/CashTermDepositPortfolio/General")]
        public CashTermDepositGeneralInfoModel GetGeneralInfo_Adviser(string clientGroupId = null)
        {
            return GenerateGeneralInfo(getCashAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/CashTermDepositPortfolio/General")]
        public CashTermDepositGeneralInfoModel GetGeneralInfo_Client()
        {
            return GenerateGeneralInfo(getCashAssetForClient());
        }

        [HttpGet, Route("api/Adviser/CashTermDepositPortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Adviser(string clientGroupId = null)
        {
            return null;

        }
        [HttpGet, Route("api/Client/CashTermDepositPortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Client()
        {
            return repo.TermDeposit_GetPortfolioRating_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/CashTermDepositPortfolio/Price")]
        public TermDepositPriceChartModel GetPrice_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.TermDeposit_GetPriceData_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.TermDeposit_GetPriceData_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/CashTermDepositPortfolio/Price")]
        public TermDepositPriceChartModel GetPrice_Client()
        {
            return repo.TermDeposit_GetPriceData_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/CashTermDepositPortfolio/Cashflow")]
        public CashflowBriefModel GetCashflowSummary_Adviser(string clientGroupId = "")
        {
            return GenerateCashflowSummaryModel(getCashAssetForAdviser(clientGroupId).GetMonthlyCashflows());
        }
        [HttpGet, Route("api/Client/CashTermDepositPortfolio/Cashflow")]
        public CashflowBriefModel GetCashflowSummary_Client()
        {
            return GenerateCashflowSummaryModel(getCashAssetForClient().GetMonthlyCashflows());
        }

        [HttpGet, Route("api/Adviser/CashTermDepositPortfolio/Profiles")]
        public CashTermDepositProfileModel GetProfiles_Adviser(string clientGroupId = null)
        {
            return GenerateProfiles(getCashAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/CashTermDepositPortfolio/Profiles")]
        public CashTermDepositProfileModel GetProfiles_Client()
        {
            return GenerateProfiles(getCashAssetForClient());
        }

        [HttpGet, Route("api/Adviser/CashTermDepositPortfolio/Stats")]
        public IEnumerable<IncomeStatisticsModel> GetStats_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.TermDeposit_GetStats_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.TermDeposit_GetStats_Client(clientUserId);
            }
        }
        [HttpGet, Route("api/Client/CashTermDepositPortfolio/Stats")]
        public IEnumerable<IncomeStatisticsModel> GetStats_Client()
        {
            return repo.TermDeposit_GetStats_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/CashTermDepositPortfolio/Diversifications")]
        public IncomeDiversificationModel GetDiversification_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.TermDeposit_GetDiversification_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.TermDeposit_GetDiversification_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/CashTermDepositPortfolio/Diversifications")]
        public IncomeDiversificationModel GetDiversification_Client()
        {
            return repo.TermDeposit_GetDiversification_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/CashTermDepositPortfolio/CashflowDetailed")]
        public CashTermDepositCashflowDetailedModel GetDetailedCashflow_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.TermDeposit_GetCashflowDetails_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.TermDeposit_GetCashflowDetails_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/CashTermDepositPortfolio/CashflowDetailed")]
        public CashTermDepositCashflowDetailedModel GetDetailedCashflow_Client()
        {
            return repo.TermDeposit_GetCashflowDetails_Client(User.Identity.GetUserId());
        }

        public List<AssetBase> getCashAssetForAdviser(string clientGroupId) {
            List<AssetBase> assets = new List<AssetBase>();

            if (string.IsNullOrEmpty(clientGroupId)) {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                groupAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<Cash>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<Cash>().Cast<AssetBase>().ToList()));

                return assets;
            } else {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = clientGroup.GetAccountsSync(DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

                accounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<Cash>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<Cash>().Cast<AssetBase>().ToList()));

                return assets;
            }
        }

        public List<AssetBase> getCashAssetForClient() {
            List<AssetBase> assets = new List<AssetBase>();

            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id) {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(edisRepo.GetAccountsForClientSync(c.ClientNumber, DateTime.Now)));

                groupAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<Cash>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<Cash>().Cast<AssetBase>().ToList()));
            } else {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                accounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<Cash>().Cast<AssetBase>().ToList()));
            }

            return assets;
        }

        public CashTermDepositGeneralInfoModel GenerateGeneralInfo(List<AssetBase> assets) {
            double annualInterest = 0;
            double consumerPriceIndex = 0;
            
            var cashes = assets.OfType<Cash>();
            assets.OfType<Cash>().ToList().ForEach(c => { 
                annualInterest += c.AnnualInterest == null? 0 : (double)c.AnnualInterest; 
                consumerPriceIndex += c.LatestPrice; 
            });

            CashTermDepositGeneralInfoModel model = new CashTermDepositGeneralInfoModel{
                annualInterest = annualInterest,
                consumerPriceIndex = consumerPriceIndex
            };
            return model;
        }

        public CashflowBriefModel GenerateCashflowSummaryModel(List<Cashflow> cashFlows) {

            List<CashFlowBriefItem> items = new List<CashFlowBriefItem>();

            foreach (Months month in Enum.GetValues(typeof(Months))) {
                string currentMonth = edisRepo.GetEnumDescription(month);

                var currentFlows = cashFlows.Where(c => c.Month == currentMonth);

                CashFlowBriefItem newItem = new CashFlowBriefItem {
                    month = currentMonth,
                    expense = currentFlows.Sum(c => c.Expenses),
                    income = currentFlows.Sum(c => c.Income)
                };

                items.Add(newItem);
            }

            CashflowBriefModel model = new CashflowBriefModel {
                totalExpense = items.Sum(i => i.expense),
                totalIncome = items.Sum(i => i.income),
                data = items
            };
            return model;
        }

        public CashTermDepositProfileModel GenerateProfiles(List<AssetBase> assets) {
            CashTermDepositProfileModel model = new CashTermDepositProfileModel { data = new List<CashTermDepositProfileItem>() };
            var cashes = assets.OfType<Cash>();
            foreach (var cash in cashes) {
                CashTermDepositProfileItem item = new CashTermDepositProfileItem {
                    accountName = cash.CashAccountName,
                    accountNumber = cash.CashAccountNumber,
                    accountType = cash.CashAccountType.ToString(),
                    accruedInterest = cash.InterestRate == null ? 0 : (double)cash.InterestRate,
                    annualInterest = cash.AnnualInterest == null ? 0 : (double)cash.AnnualInterest,
                    faceValue = cash.FaceValue,
                    interestFrequency = cash.InterestFrequency.ToString(),
                    bsb = cash.Bsb,
                    maturityDate = cash.MaturityDate == null ? DateTime.Now : (DateTime)cash.MaturityDate,
                    termOfRates = cash.TermOfRatesMonth.ToString(),
                    interestRate = cash.InterestRate == null ? 0 : (double)cash.InterestRate
                };
                model.data.Add(item);
            }

            return model;
        }
    }
}
