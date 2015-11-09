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
using Domain.Portfolio.AggregateRoots.Accounts;
using Domain.Portfolio.AggregateRoots;
using Domain.Portfolio.Services;
using Domain.Portfolio.AggregateRoots.Asset;
using Domain.Portfolio.Values.Cashflow;
using Shared;
namespace EDISAngular.APIControllers
{
    public class PortfolioManagedInvestmentController : ApiController
    {
        private PortfolioRepository repo = new PortfolioRepository();
        private EdisRepository edisRepo = new EdisRepository();

        [HttpGet, Route("api/Adviser/ManagedInvestmentPortfolio/General")]
        public SummaryGeneralInfo GetGeneralInfo_Adviser(string clientGroupId = null)
        {
            return GenerateGeneralInforModel(getInvestmentAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/ManagedInvestmentPortfolio/General")]
        public SummaryGeneralInfo GetGeneralInfo_Client()
        {
            return GenerateGeneralInforModel(getInvestmentAssetForClient());
        }
        [HttpGet, Route("api/Adviser/ManagedInvestmentPortfolio/Cashflow")]
        public CashflowBriefModel GetCashflowSummary_Adviser(string clientGroupId = null)
        {
            return GenerateCashflowSummaryModel(getInvestmentAssetForAdviser(clientGroupId).GetMonthlyCashflows());
        }
        [HttpGet, Route("api/Client/ManagedInvestmentPortfolio/Cashflow")]
        public CashflowBriefModel GetCashflowSummary_Client()
        {
            return GenerateCashflowSummaryModel(getInvestmentAssetForClient().GetMonthlyCashflows());
        }

        [HttpGet, Route("api/Adviser/ManagedInvestmentPortfolio/AssetAllocation")]
        public InvestmentPortfolioModel GetAssetAllocationSummary_Adviser(string clientGroupId = null)
        {

            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

                double totalMarketValueAE = 0;
                double totalMarketValueIE = 0;
                double totalMarketValueMI = 0;
                double totalMarketValueDP = 0;
                double totalMarketValueFI = 0;
                double totalMarketValueCD = 0;

                foreach (var account in groupAccounts)
                {
                    totalMarketValueAE += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<AustralianEquity>();
                    totalMarketValueIE += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<InternationalEquity>();
                    totalMarketValueMI += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<ManagedInvestment>();
                    totalMarketValueDP += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<DirectProperty>();
                    totalMarketValueFI += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<FixedIncome>();
                    totalMarketValueCD += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<Cash>();
                }
                foreach (var account in clientAccounts)
                {
                    totalMarketValueAE += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<AustralianEquity>();
                    totalMarketValueIE += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<InternationalEquity>();
                    totalMarketValueMI += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<ManagedInvestment>();
                    totalMarketValueDP += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<DirectProperty>();
                    totalMarketValueFI += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<FixedIncome>();
                    totalMarketValueCD += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<Cash>();
                }

                InvestmentPortfolioModel model = new InvestmentPortfolioModel
                {
                    data = new List<DataNameAmountPair>{

                        new DataNameAmountPair{name="Australian Equity", amount= totalMarketValueAE},
                        new DataNameAmountPair{name="International Equity", amount= totalMarketValueIE},
                        new DataNameAmountPair{name="Managed Investments", amount= totalMarketValueMI},
                        new DataNameAmountPair{name="Direct & Listed Property", amount= totalMarketValueDP},
                        new DataNameAmountPair{name="Miscellaneous Investments", amount= totalMarketValueAE},
                        new DataNameAmountPair{name="Fixed Income Investments", amount= totalMarketValueFI},
                        new DataNameAmountPair{name="Cash & Term Deposit", amount= totalMarketValueCD},
                    },
                };

                return model;
            }
            else {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));


                double totalMarketValueAE = 0;
                double totalMarketValueIE = 0;
                double totalMarketValueMI = 0;
                double totalMarketValueDP = 0;
                double totalMarketValueFI = 0;
                double totalMarketValueCD = 0;

                foreach (var account in accounts)
                {
                    totalMarketValueAE += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<AustralianEquity>();
                    totalMarketValueIE += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<InternationalEquity>();
                    totalMarketValueMI += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<ManagedInvestment>();
                    totalMarketValueDP += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<DirectProperty>();
                    totalMarketValueFI += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<FixedIncome>();
                    totalMarketValueCD += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<Cash>();
                }
                foreach (var account in clientAccounts)
                {
                    totalMarketValueAE += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<AustralianEquity>();
                    totalMarketValueIE += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<InternationalEquity>();
                    totalMarketValueMI += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<ManagedInvestment>();
                    totalMarketValueDP += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<DirectProperty>();
                    totalMarketValueFI += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<FixedIncome>();
                    totalMarketValueCD += account.GetAssetsSync().GetTotalMarketValue_ByAssetType<Cash>();
                }

                InvestmentPortfolioModel model = new InvestmentPortfolioModel
                {
                    data = new List<DataNameAmountPair>{

                        new DataNameAmountPair{name="Australian Equity", amount= totalMarketValueAE},
                        new DataNameAmountPair{name="International Equity", amount= totalMarketValueIE},
                        new DataNameAmountPair{name="Managed Investments", amount= totalMarketValueMI},
                        new DataNameAmountPair{name="Direct & Listed Property", amount= totalMarketValueDP},
                        new DataNameAmountPair{name="Miscellaneous Investments", amount= totalMarketValueAE},
                        new DataNameAmountPair{name="Fixed Income Investments", amount= totalMarketValueFI},
                        new DataNameAmountPair{name="Cash & Term Deposit", amount= totalMarketValueCD},
                    },
                };

                return model;
            }
            
            //return repo.ManagedInvestment_GetAssetAllocation_Adviser(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/ManagedInvestmentPortfolio/CompanyProfiles")]
        public ManagedInvestmentCompanyProfileModel GetCompanyProfiles_Adviser(string clientGroupId = null)
        {
            return GenerateProfilesModel(getInvestmentAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/ManagedInvestmentPortfolio/CompanyProfiles")]
        public ManagedInvestmentCompanyProfileModel GetCompanyProfiles_Client()
        {
            return GenerateProfilesModel(getInvestmentAssetForClient());
        }
        
        [HttpGet, Route("api/Adviser/ManagedInvestmentPortfolio/QuickStats")]
        public IEnumerable<PortfolioQuickStatsModel> GetQuickStats_Adviser()
        {
            return repo.ManagedInvestment_GetQuickStats_Adviser(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/ManagedInvestmentPortfolio/QuickStats")]
        public IEnumerable<PortfolioQuickStatsModel> GetQuickStats_Client()
        {
            return repo.ManagedInvestment_GetQuickStats_Client(User.Identity.GetUserId());
        }
        
        [HttpGet, Route("api/Adviser/ManagedInvestmentPortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Adviser(string clientGroupId = null)
        {
            return GenerateRatingsModel(getInvestmentAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/ManagedInvestmentPortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Client()
        {
            return GenerateRatingsModel(getInvestmentAssetForClient());
        }
        
        [HttpGet, Route("api/Adviser/ManagedInvestmentPortfolio/Diversification")]
        public EquityDiversificationModel GetDiversification_Adviser()
        {
            return repo.ManagedInvestment_GetDiversification_Adviser(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/ManagedInvestmentPortfolio/Diversification")]
        public EquityDiversificationModel GetDiversification_Client()
        {
            return repo.ManagedInvestment_GetDiversification_Client(User.Identity.GetUserId());
        }
        
        [HttpGet, Route("api/Adviser/ManagedInvestmentPortfolio/DiversificationGroup")]
        public DiversificationGroupModel GetDiversificationGroupSummary_Adviser()
        {
            return repo.ManagedInvestment_GetDiversificationGroup_Adviser(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/ManagedInvestmentPortfolio/CashflowDetail")]
        public InvestmentCashflowDetailedModel GetCashflowDetailed_Adviser()
        {
            return repo.ManagedInvestment_GetSummaryCashflowDetailed_Adviser(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/ManagedInvestmentPortfolio/CashflowDetail")]
        public InvestmentCashflowDetailedModel GetCashflowDetailed_Client()
        {
            return repo.ManagedInvestment_GetSummaryCashflowDetailed_Client(User.Identity.GetUserId());
        }

        public List<AssetBase> getInvestmentAssetForAdviser(string clientGroupId) {
            List<AssetBase> assets = new List<AssetBase>();

            if (string.IsNullOrEmpty(clientGroupId)) {

                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                groupAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList()));

                return assets;
            } else {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = clientGroup.GetAccountsSync(DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

                accounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList()));

                return assets;
            }
        }

        public List<AssetBase> getInvestmentAssetForClient() {
            List<AssetBase> assets = new List<AssetBase>();

            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id) {
                clientGroup.GetAccountsSync();

                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                groupAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList()));
            } else {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                accounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList()));
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

        public ManagedInvestmentCompanyProfileModel GenerateProfilesModel(List<AssetBase> assets) {
            List<ManagedInvestmentCompanyProfileItem> itemList = new List<ManagedInvestmentCompanyProfileItem>();

            foreach (var asset in assets.OfType<ManagedInvestment>()) {
                var itemIndex = itemList.FindIndex(i => i.ticker == asset.Ticker);

                if (itemIndex < 0) {
                    itemList.Add(new ManagedInvestmentCompanyProfileItem {
                        ticker = asset.Ticker,
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

            ManagedInvestmentCompanyProfileModel model = new ManagedInvestmentCompanyProfileModel {
                data = itemList,
                totalCostInvestment = assets.GetTotalCost().Total,
                totalMarketValue = assets.GetTotalMarketValue(),
            };
            return model;
        }

        public PortfolioRatingModel GenerateRatingsModel(List<AssetBase> assets) {
            List<ManagedInvestment> equityWithResearchValues = assets.OfType<ManagedInvestment>().ToList();
            var weightings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();
            double assetsSuitability = weightings.Sum(w => w.Percentage * ((ManagedInvestment)w.Weightable).GetRating().TotalScore);
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
