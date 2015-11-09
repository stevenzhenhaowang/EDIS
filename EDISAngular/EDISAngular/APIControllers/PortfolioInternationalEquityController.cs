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
using SqlRepository;
using Domain.Portfolio.AggregateRoots;
using Domain.Portfolio.Values.Ratios;
using Domain.Portfolio.AggregateRoots.Asset;
using Domain.Portfolio.Services;
using Domain.Portfolio.Values.Cashflow;
using Shared;
using Domain.Portfolio.AggregateRoots.Accounts;


namespace EDISAngular.APIControllers
{
    public class PortfolioInternationalEquityController : ApiController
    {
        private PortfolioRepository repo = new PortfolioRepository();
        private EdisRepository edisRepo = new EdisRepository();

        [HttpGet, Route("api/Adviser/InternationalEquityPortfolio/General")]
        public SummaryGeneralInfo GetGeneralInfo_Adviser(string clientGroupId = null)
        {
            return GenerateGeneralInforModel(getInternationalAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/InternationalEquityPortfolio/General")]
        public SummaryGeneralInfo GetGeneralInfo_Client()
        {
            return GenerateGeneralInforModel(getInternationalAssetForClient());
        }

        [HttpGet, Route("api/Adviser/InternationalEquityPortfolio/EvaluationModel")]
        public IEnumerable<EvaluationModel> GetEvaluationModel_Adviser(string clientGroupId = null)
        {
            return GenerateEvaluationModel(getInternationalAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/InternationalEquityPortfolio/EvaluationModel")]
        public IEnumerable<EvaluationModel> GetEvaluationModel_Client()
        {
            return GenerateEvaluationModel(getInternationalAssetForClient());
        }

        [HttpGet, Route("api/Adviser/InternationalEquityPortfolio/Cashflow")]
        public CashflowBriefModel GetCashflowSummary_Adviser(string clientGroupId = null)
        {
            return GenerateCashflowSummaryModel(getInternationalAssetForAdviser(clientGroupId).GetMonthlyCashflows());
        }
        [HttpGet, Route("api/Client/InternationalEquityPortfolio/Cashflow")]
        public CashflowBriefModel GetCashflowSummary_Client()
        {
            return GenerateCashflowSummaryModel(getInternationalAssetForClient().GetMonthlyCashflows());
        }

        [HttpGet, Route("api/Adviser/InternationalEquityPortfolio/CompanyProfiles")]
        public EquityCompanyProfileModel GetCompanyProfiles_Adviser(string clientGroupId = null)
        {
            return GenerateProfilesModel(getInternationalAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/InternationalEquityPortfolio/CompanyProfiles")]
        public EquityCompanyProfileModel GetCompanyProfiles_Client()
        {
            return GenerateProfilesModel(getInternationalAssetForClient());
        }

        [HttpGet, Route("api/Adviser/InternationalEquityPortfolio/QuickStats")]
        public IEnumerable<PortfolioQuickStatsModel> GetQuickStats_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.InternationalEquity_GetQuickStats_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.InternationalEquity_GetQuickStats_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/InternationalEquityPortfolio/QuickStats")]
        public IEnumerable<PortfolioQuickStatsModel> GetQuickStats_Client()
        {
            return repo.InternationalEquity_GetQuickStats_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/InternationalEquityPortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Adviser(string clientGroupId = null)
        {
            return GenerateRatingsModel(getInternationalAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/InternationalEquityPortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Client()
        {
            return GenerateRatingsModel(getInternationalAssetForClient());
        }

        [HttpGet, Route("api/Adviser/InternationalEquityPortfolio/Diversification")]
        public EquityDiversificationModel GetDiversification_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.InternationalEquity_GetDiversification_Client(User.Identity.GetUserId());
            }
            else
            {
                return repo.InternationalEquity_GetDiversification_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/InternationalEquityPortfolio/Diversification")]
        public EquityDiversificationModel GetDiversification_Client()
        {
            return repo.InternationalEquity_GetDiversification_Adviser(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/InternationalEquityPortfolio/CashflowDetail")]
        public EquityCashflowDetailedModel GetCashflowDetailed_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.InternationalEquity_GetSummaryCashflowDetailed_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.InternationalEquity_GetSummaryCashflowDetailed_Client(clientUserId);
            }
            
        }
        [HttpGet, Route("api/Client/InternationalEquityPortfolio/CashflowDetail")]
        public EquityCashflowDetailedModel GetCashflowDetailed_Client()
        {
          
            return repo.InternationalEquity_GetSummaryCashflowDetailed_Client(User.Identity.GetUserId());
        }

        public List<AssetBase> getInternationalAssetForAdviser(string clientGroupId) {
            List<AssetBase> assets = new List<AssetBase>();

            if (string.IsNullOrEmpty(clientGroupId)) {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                groupAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList()));

                return assets;
            } else {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = clientGroup.GetAccountsSync(DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

                accounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList()));

                return assets;
            }
        }

        public List<AssetBase> getInternationalAssetForClient() {
            List<AssetBase> assets = new List<AssetBase>();

            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id) {
                clientGroup.GetAccountsSync();

                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                groupAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList()));
            } else {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                accounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList()));
            }

            return assets;
        }

        public SummaryGeneralInfo GenerateGeneralInforModel(List<AssetBase> assets) {
            double totalCost = 0;
            double totalMarketValue = 0;
            double capitalGain = 0;

            foreach (var asset in assets) {
                totalCost += asset.GetCost().Total;
                totalMarketValue += asset.GetTotalMarketValue();
                capitalGain += asset.GetCost().CapitalGain;
            }
            SummaryGeneralInfo summary = new SummaryGeneralInfo {
                cost = totalCost,
                marketValue = totalMarketValue,
                pl = totalMarketValue - totalCost,
                plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100,
                capitalGain = capitalGain
            };
            return summary;
        }

        public IEnumerable<EvaluationModel> GenerateEvaluationModel(List<AssetBase> assets) {

            List<EvaluationModel> models = new List<EvaluationModel>();
            Ratios ratios = assets.GetAverageRatiosFor<InternationalEquity>();
            Recommendation expected = assets.GetAverageExpectedFor<InternationalEquity>();

            foreach (EvaluationInfo evaluation in Enum.GetValues(typeof(EvaluationInfo))) {
                EvaluationModel eachModel = new EvaluationModel { title = edisRepo.GetEnumDescription(evaluation) };

                switch (evaluation) {
                    case EvaluationInfo.OneYearReturn:
                        eachModel.actual += ratios.OneYearReturn;
                        eachModel.expected += expected.OneYearReturn == null ? 0 : (double)expected.OneYearReturn;
                        break;
                    case EvaluationInfo.FiveYearReturn:
                        eachModel.actual += ratios.FiveYearReturn;
                        eachModel.expected += expected.FiveYearTotalReturn == null ? 0 : (double)expected.OneYearReturn;
                        break;
                    case EvaluationInfo.DebtEquityRatio:
                        eachModel.actual += ratios.DebtEquityRatio;
                        eachModel.expected += expected.DebtEquityRatio;
                        break;
                    case EvaluationInfo.EpsGrowth:
                        eachModel.actual += ratios.EpsGrowth;
                        eachModel.expected += expected.EpsGrowth;
                        break;
                    case EvaluationInfo.DividendYield:
                        eachModel.actual += ratios.DividendYield;
                        eachModel.expected += expected.DividendYield;
                        break;
                    case EvaluationInfo.Frank:
                        eachModel.actual += ratios.Frank;
                        eachModel.expected += expected.Frank;
                        break;
                    case EvaluationInfo.InterestCover:
                        eachModel.actual += ratios.InterestCover;
                        eachModel.expected += expected.InterestCover;
                        break;
                    case EvaluationInfo.PriceEarningRatio:
                        eachModel.actual += ratios.PriceEarningRatio;
                        eachModel.expected += expected.PriceEarningRatio;
                        break;
                    case EvaluationInfo.ReturnOnAsset:
                        eachModel.actual += ratios.ReturnOnAsset;
                        eachModel.expected += expected.ReturnOnAsset;
                        break;
                    case EvaluationInfo.ReturnOnEquity:
                        eachModel.actual += ratios.ReturnOnEquity;
                        eachModel.expected += expected.ReturnOnEquity;
                        break;
                }
                models.Add(eachModel);
            }
            return models;
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

        public EquityCompanyProfileModel GenerateProfilesModel(List<AssetBase> assets) {
            List<EquityCompanyProfileItemModel> itemList = new List<EquityCompanyProfileItemModel>();

            foreach (var asset in assets.OfType<InternationalEquity>()) {
                var itemIndex = itemList.FindIndex(i => i.asx == asset.Ticker);

                if (itemIndex < 0) {
                    itemList.Add(new EquityCompanyProfileItemModel {
                        asx = asset.Ticker,
                        beta = asset.F0Ratios.Beta,
                        company = asset.Name,
                        currentRatio = asset.F0Ratios.CurrentRatio,
                        debtEquityRatio = asset.F0Ratios.DebtEquityRatio,
                        earningsStability = asset.F0Ratios.EarningsStability,
                        fiveYearReturn = asset.F0Ratios.FiveYearReturn,
                        threeYearReturn = asset.F0Ratios.ThreeYearReturn,
                        oneYearReturn = asset.F0Ratios.OneYearReturn,
                        franking = asset.F0Ratios.Frank,
                        interestCover = asset.F0Ratios.InterestCover,
                        payoutRatio = asset.F0Ratios.PayoutRatio,
                        priceEarningsRatio = asset.F0Ratios.PriceEarningRatio,
                        quickRatio = asset.F0Ratios.QuickRatio,
                        returnOnAsset = asset.F0Ratios.ReturnOnAsset,
                        returnOnEquity = asset.F0Ratios.ReturnOnEquity,
                        marketPrice = asset.LatestPrice,
                        marketValue = asset.GetTotalMarketValue(),
                        totalCostValue = asset.GetCost().Total,
                        costValue = asset.GetCost().AssetCost,
                        companySuitabilityToInvestor = asset.GetRating().TotalScore
                    });
                } else {
                    itemList[itemIndex].marketValue += asset.GetTotalMarketValue();
                    itemList[itemIndex].totalCostValue += asset.GetCost().Total;
                    itemList[itemIndex].costValue += asset.GetCost().AssetCost;
                }
            }

            EquityCompanyProfileModel model = new EquityCompanyProfileModel {
                data = itemList,
                totalCostInvestment = assets.GetTotalCost().Total,
                totalMarketValue = assets.GetTotalMarketValue()
            };
            return model;
        }

        public PortfolioRatingModel GenerateRatingsModel(List<AssetBase> assets) {
            List<InternationalEquity> equityWithResearchValues = assets.OfType<InternationalEquity>().ToList();
            var weightings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();
            double assetsSuitability = weightings.Sum(w => w.Percentage * ((InternationalEquity)w.Weightable).GetRating().TotalScore);
            double percentage = equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());

            PortfolioRatingModel model = new PortfolioRatingModel {
                suitability = assetsSuitability,
                notSuited = percentage,
                data = new List<PortfolioRatingItemModel>()
            };

            foreach (var weighting in weightings) {
                Equity equity = (Equity)weighting.Weightable;

                if (model.data.Any(m => m.name == equity.Ticker) == false) {
                    model.data.Add(new PortfolioRatingItemModel {
                        name = equity.Ticker,
                        weighting = weighting.Percentage,
                        score = equity.GetRating().TotalScore
                    });
                }
            }
            return model;
        }
    }
}
