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
using Domain.Portfolio.AggregateRoots;
using Domain.Portfolio.Services;
using Domain.Portfolio.Values.Cashflow;
using Domain.Portfolio.AggregateRoots.Asset;
using Shared;


namespace EDISAngular.APIControllers
{
    public class PortfolioFixedIncomeController : ApiController
    {
        private PortfolioRepository repo = new PortfolioRepository();
        private EdisRepository edisRepo = new EdisRepository();
        [HttpGet, Route("api/Adviser/FixedIncomePortfolio/General")]
        public SummaryGeneralInfoFCL GetGeneralInfo_Adviser(string clientGroupId = null)
        {
            return GenerateGeneralInforModel(getFixedIncomeAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/FixedIncomePortfolio/General")]
        public SummaryGeneralInfoFCL GetGeneralInfo_Client()
        {
            return GenerateGeneralInforModel(getFixedIncomeAssetForClient());
        }

        [HttpGet, Route("api/Adviser/FixedIncomePortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.FixedIncome_GetPortfolioRating_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.FixedIncome_GetPortfolioRating_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/FixedIncomePortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Client()
        {
            return repo.FixedIncome_GetPortfolioRating_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/FixedIncomePortfolio/Price")]
        public CashPriceChartModel GetPrice_Adviser(string clientGroupId = null)
        {
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);




                return repo.FixedIncome_GetPriceData_Adviser(User.Identity.GetUserId());
            }
            else
            {

                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

                foreach (var account in accounts)
                {
                    account.GetAssetsSync();
                }



                return repo.FixedIncome_GetPriceData_Client(clientGroupId);
            }

        }
        //[HttpGet, Route("api/Client/FixedIncomePortfolio/Price")]
        //public CashPriceChartModel GetPrice_Client()
        //{
        //    Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
        //    ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
        //    if (clientGroup.MainClientId == client.Id)
        //    {
        //        List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
        //        List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);


        //    }
        //    else
        //    {
        //        List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

        //    }
        //}

        [HttpGet, Route("api/Adviser/FixedIncomePortfolio/Cashflow")]
        public CashflowBriefModel GetCashflowSummary_Adviser(string clientGroupId = null)
        {
            return GenerateCashflowSummaryModel(getFixedIncomeAssetForAdviser(clientGroupId).GetMonthlyCashflows());
        }
        [HttpGet, Route("api/Client/FixedIncomePortfolio/Cashflow")]
        public CashflowBriefModel GetCashflowSummary_Client()
        {
            return GenerateCashflowSummaryModel(getFixedIncomeAssetForClient().GetMonthlyCashflows());
        }

        [HttpGet, Route("api/Adviser/FixedIncomePortfolio/Profiles")]
        public FixedIncomeProfileModel GetProfiles_Adviser(string clientGroupId = null)
        {
            return GenerateProfile(getFixedIncomeAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/FixedIncomePortfolio/Profiles")]
        public FixedIncomeProfileModel GetProfiles_Client()
        {
            return GenerateProfile(getFixedIncomeAssetForClient());
        }

        [HttpGet, Route("api/Adviser/FixedIncomePortfolio/Stats")]
        public IEnumerable<IncomeStatisticsModel> GetStats_Adviser(string clientGroupId = null)
        {
            return GenerateStats(getFixedIncomeAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/FixedIncomePortfolio/Stats")]
        public IEnumerable<IncomeStatisticsModel> GetStats_Client()
        {
            return GenerateStats(getFixedIncomeAssetForClient());
        }

        [HttpGet, Route("api/Adviser/FixedIncomePortfolio/Diversifications")]
        public IncomeDiversificationModel GetDiversification_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.FixedIncome_GetDiversification_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.FixedIncome_GetDiversification_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/FixedIncomePortfolio/Diversifications")]
        public IncomeDiversificationModel GetDiversification_Client()
        {
            return repo.FixedIncome_GetDiversification_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/FixedIncomePortfolio/CashflowDetailed")]
        public FixedIncomeCashflowDetailedModel GetDetailedCashflow_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.FixedIncome_GetCashflowDetails_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.FixedIncome_GetCashflowDetails_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/FixedIncomePortfolio/CashflowDetailed")]
        public FixedIncomeCashflowDetailedModel GetDetailedCashflow_Client()
        {
            return repo.FixedIncome_GetCashflowDetails_Client(User.Identity.GetUserId());
        }

        public List<AssetBase> getFixedIncomeAssetForAdviser(string clientGroupId) {
            List<AssetBase> assets = new List<AssetBase>();

            if (string.IsNullOrEmpty(clientGroupId)) {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                groupAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList()));
            } else {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = clientGroup.GetAccountsSync(DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));
                accounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList()));
            }
            return assets;
        }

        public List<AssetBase> getFixedIncomeAssetForClient() {
            List<AssetBase> assets = new List<AssetBase>();

            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id) {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                groupAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList()));
            } else {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                accounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList()));
            }

            return assets;
        }

        public SummaryGeneralInfoFCL GenerateGeneralInforModel(List<AssetBase> assets) {
            double totalCost = 0;
            double totalMarketValue = 0;
            double faceValue = 0;
            
            foreach (var asset in assets) {
                totalCost += asset.GetCost().Total;
                totalMarketValue += asset.GetTotalMarketValue();
                faceValue += asset.LatestPrice;
            }

            SummaryGeneralInfoFCL summary = new SummaryGeneralInfoFCL {
                cost = totalCost,
                marketValue = totalMarketValue,
                pl = totalMarketValue - totalCost,
                plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100,
                aveCoupon = assets.OfType<FixedIncome>().Sum(i => i.CouponRate == null? 0 : (double)i.CouponRate)
            };
            return summary;
        }

        public FixedIncomeProfileModel GenerateProfile(List<AssetBase> assets) {
            FixedIncomeProfileModel model = new FixedIncomeProfileModel { data = new List<FixedIncomeProfileItem>() };
            foreach (var income in assets.OfType<FixedIncome>()) {
                FixedIncomeProfileItem item = new FixedIncomeProfileItem {
                    ticker = income.Ticker,
                    fixedIncomeName = income.FixedIncomeName,
                    faceValue = income.LatestPrice,
                    coupon = income.CouponRate == null ? 0 : (double)income.CouponRate,
                    couponFrequency = income.CouponFrequency.ToString(),
                    issuer = income.Issuer,
                    costValue = income.GetCost().AssetCost,
                    totalCostValue = income.GetCost().Total,
                    marketValue = income.GetTotalMarketValue(),
                    priority = (int)income.BoundDetails.Priority,
                    redemptionFeatures = income.BoundDetails.RedemptionFeatures.ToString(),
                    bondRating = (double)income.BoundDetails.BondRating,
                    ratingAgency = income.BoundDetails.RatingAgency,
                };
                model.data.Add(item);
            }
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

        public IEnumerable<IncomeStatisticsModel> GenerateStats(List<AssetBase> assets) {
            List<IncomeStatisticsModel> incomeModelList = new List<IncomeStatisticsModel>();

            foreach (var incomeGroup in assets.OfType<FixedIncome>().GroupBy(i => i.BondType)) {
                var income = incomeGroup.FirstOrDefault();
                
                incomeModelList.Add(new IncomeStatisticsModel { 
                    value = incomeGroup.Sum(i => i.GetTotalMarketValue()),
                    type = income.BondType,
                });
            }
            incomeModelList.ForEach(m => m.percentage = incomeModelList.Sum(i => i.value) == 0 ? 0 : m.value / incomeModelList.Sum(i => i.value) * 100);
            return incomeModelList;
        }
    }
}
