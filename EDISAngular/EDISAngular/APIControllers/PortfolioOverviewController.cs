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
using Domain.Portfolio.Services;
using Domain.Portfolio.AggregateRoots.Asset;
using Domain.Portfolio.AggregateRoots;
using Domain.Portfolio.AggregateRoots.Accounts;
using Shared;
using Domain.Portfolio.Values.Cashflow;
using Domain.Portfolio.AggregateRoots.Liability;


namespace EDISAngular.APIControllers
{
    public class PortfolioOverviewController : ApiController
    {
        private EdisRepository edisRepo = new EdisRepository();
        private PortfolioRepository repo = new PortfolioRepository();
        
        [HttpGet, Route("api/Adviser/PortfolioOverview/Summary")]
        public PortfolioSummary GetPortfolioSummary_Adviser(string clientGroupId = null)
        {
            return GenerateSummary(getAssetsAndLiabilitiesForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/PortfolioOverview/Summary")]
        public PortfolioSummary GetPortfolioSummary_Client()
        {
            return GenerateSummary(getAssetsAndLiabilitiesForClient());
        }
        
        [HttpGet, Route("api/Adviser/PortfolioOverview/Cashflow")]
        public CashflowBriefModel GetCashflowSummary_Adviser(string clientGroupId = null)
        {
            return GenerateCashflowSummaryModel(getAllAssetForAdviser(clientGroupId).GetMonthlyCashflows());
        }
        [HttpGet, Route("api/Client/PortfolioOverview/Cashflow")]
        public CashflowBriefModel GetCashflowSummary_Client()
        {
            return GenerateCashflowSummaryModel(getAllAssetForClient().GetMonthlyCashflows());
        }
        
        [HttpGet, Route("api/Adviser/PortfolioOverview/BestPerforming")]
        public IEnumerable<AssetPerformanceModel> GetBestPerforming_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.Overview_GetBestPerformingSummary_Adviser(User.Identity.GetUserId());
            }
            else
            {







                return repo.Overview_GetBestPerformingSummary_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Adviser/portfolioOverview/WorstPerforming")]
        public IEnumerable<AssetPerformanceModel> GetWorstPerforming_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.Overview_GetWorstPerformingSummary_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.Overview_GetWorstPerformingSummary_Client(clientUserId);
            }

        }
        
        [HttpGet, Route("api/Client/PortfolioOverview/BestPerforming")]
        public IEnumerable<AssetPerformanceModel> GetBestPerforming_Client()
        {
            return repo.Overview_GetBestPerformingSummary_Client(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/portfolioOverview/WorstPerforming")]
        public IEnumerable<AssetPerformanceModel> GetWorstPerforming_Client()
        {
            return repo.Overview_GetWorstPerformingSummary_Client(User.Identity.GetUserId());
        }
        
        [HttpGet, Route("api/Adviser/PortfolioOverview/General")]
        public SummaryGeneralInfo GetGeneralInfo_Adviser(string clientGroupId = null)
        {
            return GenerateGeneralInforModel(getAllAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/PortfolioOverview/General")]
        public SummaryGeneralInfo GetGeneralInfo_Client()
        {
            return GenerateGeneralInforModel(getAllAssetForClient());
        }
        
        [HttpGet, Route("api/Adviser/PortfolioOverview/Stastics")]
        public PortfolioStasticsModel GetStastics_Adviser(string clientGroupId = null)
        {
            return GenerateStasticsModel(getAssetsAndLiabilitiesForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/PortfolioOverview/Stastics")]
        public PortfolioStasticsModel GetStastics_Client()
        {
            return GenerateStasticsModel(getAssetsAndLiabilitiesForClient());
        }

        [HttpGet, Route("api/Adviser/PortfolioOverview/PortfolioRating")]
        public PortfolioRatingModel GetPortfolioRating_Adviser(string clientGroupId = null)
        {
            return GenerateRatingsModel(getAllAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/PortfolioOverview/PortfolioRating")]
        public PortfolioRatingModel GetPortfolioRating_Client()
        {
            return GenerateRatingsModel(getAllAssetForClient());
        }
        
        [HttpGet, Route("api/PortfolioOverview/RecentStock")]
        public IEnumerable<StockDataModel> GetRecentStock(string companyId = null, string periodId = null)
        {
            if (companyId == null) {
                return null;
            }

            Equity equity = edisRepo.getEquityById(companyId);
            List<StockDataModel> result = new List<StockDataModel>();

            foreach (var price in edisRepo.getPricesByEquityIdAndDates(companyId, periodId)) {
                result.Add(new StockDataModel
                {
                    AssetName = equity.Name,
                    year = price.CreatedOn.Value.Date.ToString("yy-MM-dd"),//price.CreatedOn.Value.Year + "/" + price.CreatedOn.Value.Month + "/" + price.CreatedOn.Value.Day,
                    AssetUnitPrice = price.Price == null ? 0 : (double)price.Price
                });
            }
            return result;
        }
        
        [HttpGet, Route("api/Adviser/PortfolioOverview/InvestmentPortfolio")]
        public InvestmentPortfolioModel GetInvestmentPortfolio_Adviser(string clientGroupId = null)
        {
            return GenerateInvestPortfolioModel(getAllAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/PortfolioOverview/InvestmentPortfolio")]
        public InvestmentPortfolioModel GetInvestmentPortfolio_Client()
        {
            return GenerateInvestPortfolioModel(getAllAssetForClient());
        }
        
        [HttpGet, Route("api/Adviser/PortfolioOverview/RegionalExposure")]
        public PortfolioRegionalExposureModel GetRegionalExposureSummary_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.Overview_GetRegionSummary_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.Overview_GetRegionSummary_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/PortfolioOverview/RegionalExposure")]
        public PortfolioRegionalExposureModel GetRegionalExposureSummary_Client()
        {
            return repo.Overview_GetRegionSummary_Client(User.Identity.GetUserId());
        }
        
        [HttpGet, Route("api/Adviser/PortfolioOverview/SectorialExposure")]             //.....
        public SectorialPortfolioModel GetSectorialExposureSummary_Adviser(string clientGroupId = null)
        {
            return GenerateSectorialModel(getAllAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/PortfolioOverview/SectorialExposure")]
        public SectorialPortfolioModel GetSectorialExposureSummary_Client()
        {
            return GenerateSectorialModel(getAllAssetForClient());
        }
        
        [HttpGet, Route("api/Adviser/PortfolioOverview/CashflowDetail")]
        public CashflowDetailedModel GetDetailedCashflowSummary_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.Overview_GetSummaryCashflowDetailed_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.Overview_GetSummaryCashflowDetailed_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/PortfolioOverview/CashflowDetail")]
        public CashflowDetailedModel GetDetailedCashflowSummary_Client()
        {
            return repo.Overview_GetSummaryCashflowDetailed_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/PortfolioOverview/EquityLocation")]
        public EquityLocationDetailsModel GetEquityLocation_Adviser(string clientUserId = null) {

            return null;
        }
        [HttpGet, Route("api/Client/PortfolioOverview/EquityLocation")]
        public EquityLocationDetailsModel GetEquityLocation_Client() {
            return GenerateEquityLocationModel(getAllAssetForClient());
        }


        public class AssetsAndLiabilites {
            public List<AssetBase> assets { get; set; }
            public List<LiabilityBase> liabilities { get; set; }
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
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                groupAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync()));
            } else {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                accounts.ForEach(a => assets.AddRange(a.GetAssetsSync()));
            }

            return assets;
        }

        public AssetsAndLiabilites getAssetsAndLiabilitiesForAdviser(string clientGroupId) {
            List<AssetBase> assets = new List<AssetBase>();
            List<LiabilityBase> liabilities = new List<LiabilityBase>();

            if (string.IsNullOrEmpty(clientGroupId)) {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                groupAccounts.ForEach(a => {
                    assets.AddRange(a.GetAssetsSync());
                    liabilities.AddRange(a.GetLiabilitiesSync());
                });
                clientAccounts.ForEach(a => {
                    assets.AddRange(a.GetAssetsSync());
                    liabilities.AddRange(a.GetLiabilitiesSync());
                });
            } else {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = clientGroup.GetAccountsSync(DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));
                accounts.ForEach(a => {
                    assets.AddRange(a.GetAssetsSync());
                    liabilities.AddRange(a.GetLiabilitiesSync());
                });
                clientAccounts.ForEach(a => {
                    assets.AddRange(a.GetAssetsSync());
                    liabilities.AddRange(a.GetLiabilitiesSync());
                });
            }
            return new AssetsAndLiabilites { assets = assets, liabilities = liabilities };
        }

        public AssetsAndLiabilites getAssetsAndLiabilitiesForClient() {
            List<AssetBase> assets = new List<AssetBase>();
            List<LiabilityBase> liabilities = new List<LiabilityBase>();

            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id) {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                groupAccounts.ForEach(a => {
                    assets.AddRange(a.GetAssetsSync());
                    liabilities.AddRange(a.GetLiabilitiesSync());
                });
                clientAccounts.ForEach(a => {
                    assets.AddRange(a.GetAssetsSync());
                    liabilities.AddRange(a.GetLiabilitiesSync());
                });
            } else {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                accounts.ForEach(a => {
                    assets.AddRange(a.GetAssetsSync());
                    liabilities.AddRange(a.GetLiabilitiesSync());
                });
            }
            return new AssetsAndLiabilites { assets = assets, liabilities = liabilities };
        }

        public CashflowBriefModel GenerateCashflowSummaryModel(List<Cashflow> cashflows) {
            List<CashFlowBriefItem> items = new List<CashFlowBriefItem>();

            foreach (Months month in Enum.GetValues(typeof(Months))) {
                string currentMonth = edisRepo.GetEnumDescription(month);
                var currentFlows = cashflows.Where(c => c.Month == currentMonth);

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

        public PortfolioSummary GenerateSummary(AssetsAndLiabilites assetsAndLiabilities) {
            List<AssetBase> assets = assetsAndLiabilities.assets;
            List<LiabilityBase> liabilities = assetsAndLiabilities.liabilities;

            PortfolioSummary summary = new PortfolioSummary {
                investment = new SummaryItem {
                    data = new List<DataNameAmountPair> {
                        new DataNameAmountPair{ amount = assets.OfType<AustralianEquity>().Cast<AssetBase>().ToList().GetTotalMarketValue(), name="Australian Equity"},
                        new DataNameAmountPair { amount =  assets.OfType<InternationalEquity>().Cast<AssetBase>().ToList().GetTotalMarketValue(), name="International Equity"},
                        new DataNameAmountPair { amount =  assets.OfType<ManagedInvestment>().Cast<AssetBase>().ToList().GetTotalMarketValue(), name="Managed Investment"},
                    }
                },
                liability = new SummaryItem {
                    data = new List<DataNameAmountPair> {
                        new DataNameAmountPair{amount =  liabilities.OfType<MortgageAndHomeLiability>().Cast<LiabilityBase>().ToList().GetTotalLiabilitiesValue(),name="Mortgage & Investment Loans"},
                        new DataNameAmountPair{amount =  liabilities.OfType<MarginLending>().Cast<LiabilityBase>().ToList().GetTotalLiabilitiesValue(),name="Margin Loans"},
                        new DataNameAmountPair{amount =  liabilities.OfType<Insurance>().Cast<LiabilityBase>().ToList().GetTotalLiabilitiesValue(),name="Insurance"},
                        //new DataNameAmountPair{amount=30000,name="Margin Loans"}
                    }
                },
                networth = new SummaryItem {
                    data = new List<DataNameAmountPair> {
                        //new DataNameAmountPair{amount=30000, name="Investor Equity"},
                        //new DataNameAmountPair{amount=500000, name="Non-Investment Asset"}
                    },
                }
            };

            summary.investment.total = summary.investment.data.Sum(d => d.amount);
            summary.liability.total = summary.liability.data.Sum(d => d.amount);
            summary.networth.total = summary.investment.total + summary.liability.total;

            return summary;
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

        public PortfolioStasticsModel GenerateStasticsModel(AssetsAndLiabilites assetsAndLiabilities) {
            List<AssetBase> assets = assetsAndLiabilities.assets;
            List<LiabilityBase> liabilities = assetsAndLiabilities.liabilities;

            PortfolioStasticsModel model = new PortfolioStasticsModel { data = new List<PortfolioStasticsItem>() };

            List<AssetBase> aeAssets = assets.OfType<AustralianEquity>().Cast<AssetBase>().ToList();
            List<AssetBase> ieAssets = assets.OfType<InternationalEquity>().Cast<AssetBase>().ToList();
            List<AssetBase> miAssets = assets.OfType<ManagedInvestment>().Cast<AssetBase>().ToList();

            var aeRatios = aeAssets.GetAverageRatiosFor<AustralianEquity>();
            var ieRatios = ieAssets.GetAverageRatiosFor<InternationalEquity>();
            var miRatios = miAssets.GetAverageRatiosFor<ManagedInvestment>();

            model.data.Add(new PortfolioStasticsItem {
                assetClass = edisRepo.GetEnumDescription(AssetTypes.AustralianEquity),
                costInvestment = aeAssets.Sum(a => a.GetCost().Total),
                marketValue = aeAssets.Sum(a => a.GetTotalMarketValue()),
                suitability = aeAssets.GetAssetWeightings().Sum(w => w.Percentage * ((AustralianEquity)w.Weightable).GetRating().TotalScore),
                oneYearReturn = aeRatios.OneYearReturn,
                threeYearReturn = aeRatios.ThreeYearReturn,
                fiveYearReturn = aeRatios.FiveYearReturn,
                earningsPerShare = aeRatios.EarningsStability,
                dividend = aeRatios.DividendYield,
                beta = aeRatios.Beta.ToString(),
                returnOnAsset = aeRatios.ReturnOnAsset,
                returnOnEquity = aeRatios.ReturnOnEquity,
                priceEarningsRatio = aeRatios.PriceEarningRatio,
                avMarketCap = aeRatios.Capitalisation.ToString()
            });

            model.data.Add(new PortfolioStasticsItem {
                assetClass = edisRepo.GetEnumDescription(AssetTypes.InternationalEquity),
                costInvestment = ieAssets.Sum(a => a.GetCost().Total),
                marketValue = ieAssets.Sum(a => a.GetTotalMarketValue()),
                suitability = ieAssets.GetAssetWeightings().Sum(w => w.Percentage * ((InternationalEquity)w.Weightable).GetRating().TotalScore),
                oneYearReturn = ieRatios.OneYearReturn,
                threeYearReturn = ieRatios.ThreeYearReturn,
                fiveYearReturn = ieRatios.FiveYearReturn,
                earningsPerShare = ieRatios.EarningsStability,
                dividend = ieRatios.DividendYield,
                beta = ieRatios.Beta.ToString(),
                returnOnAsset = ieRatios.ReturnOnAsset,
                returnOnEquity = ieRatios.ReturnOnEquity,
                priceEarningsRatio = ieRatios.PriceEarningRatio,
                avMarketCap = ieRatios.Capitalisation.ToString()
            });

            model.data.Add(new PortfolioStasticsItem {
                assetClass = edisRepo.GetEnumDescription(AssetTypes.ManagedInvestments),
                costInvestment = miAssets.Sum(a => a.GetCost().Total),
                marketValue = miAssets.Sum(a => a.GetTotalMarketValue()),
                suitability = miAssets.GetAssetWeightings().Sum(w => w.Percentage * ((ManagedInvestment)w.Weightable).GetRating().TotalScore),
                oneYearReturn = miRatios.OneYearReturn,
                threeYearReturn = miRatios.ThreeYearReturn,
                fiveYearReturn = miRatios.FiveYearReturn,
                earningsPerShare = miRatios.EarningsStability,
                dividend = miRatios.DividendYield,
                beta = miRatios.Beta.ToString(),
                returnOnAsset = miRatios.ReturnOnAsset,
                returnOnEquity = miRatios.ReturnOnEquity,
                priceEarningsRatio = miRatios.PriceEarningRatio,
                avMarketCap = miRatios.Capitalisation.ToString()
            });


            foreach (var item in model.data) {
                item.pl = item.marketValue - item.costInvestment;
                item.plp = item.costInvestment == 0 ? 0 : (item.marketValue - item.costInvestment) / item.costInvestment;
            }
            return model;
        }

        public PortfolioRatingModel GenerateRatingsModel(List<AssetBase> assets) {
            var equityWithResearchValues = assets.OfType<Equity>().ToList();
            var weightings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();
            double assetsSuitability = weightings.Sum(w => w.Percentage * ((Equity)w.Weightable).GetRating().TotalScore);
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

        public InvestmentPortfolioModel GenerateInvestPortfolioModel(List<AssetBase> assets) {
            InvestmentPortfolioModel model = new InvestmentPortfolioModel {
                data = new List<DataNameAmountPair>{
                    new DataNameAmountPair{name="Australian Equity", amount = assets.GetTotalMarketValue_ByAssetType<AustralianEquity>(), returnValue = assets.OfType<AustralianEquity>().Cast<AssetBase>().ToList().GetTotalCost().CapitalGain},
                    new DataNameAmountPair{name="International Equity", amount= assets.GetTotalMarketValue_ByAssetType<InternationalEquity>(), returnValue = assets.OfType<InternationalEquity>().Cast<AssetBase>().ToList().GetTotalCost().CapitalGain},
                    new DataNameAmountPair{name="Managed Investments", amount= assets.GetTotalMarketValue_ByAssetType<ManagedInvestment>(), returnValue = assets.OfType<ManagedInvestment>().Cast<AssetBase>().ToList().GetTotalCost().CapitalGain},
                    new DataNameAmountPair{name="Direct & Listed Property", amount= assets.GetTotalMarketValue_ByAssetType<DirectProperty>(), returnValue = assets.OfType<DirectProperty>().Cast<AssetBase>().ToList().GetTotalCost().CapitalGain},
                    new DataNameAmountPair{name="Fixed Income Investments", amount= assets.GetTotalMarketValue_ByAssetType<FixedIncome>(), returnValue = assets.OfType<FixedIncome>().Cast<AssetBase>().ToList().GetTotalCost().CapitalGain},
                    new DataNameAmountPair{name="Cash & Term Deposit", amount= assets.GetTotalMarketValue_ByAssetType<Cash>(), returnValue = assets.OfType<Cash>().Cast<AssetBase>().ToList().GetTotalCost().CapitalGain},
                },
            };

            double totalAmount = model.data.Sum(d => d.amount);

            model.data.ForEach(d => {
                d.percentage = (totalAmount == 0 ? 0 : d.amount / totalAmount) * 100;
                model.total += d.amount;
                model.totalReturn += d.returnValue;
            });
            model.totalPercentage = 100;
            return model;
        }

        public SectorialPortfolioModel GenerateSectorialModel(List<AssetBase> assets) {
            double totalValue = 0;
            List<SectorItem> sectorItems = new List<SectorItem>();

            edisRepo.GetAllSectorsSync().ForEach(s => sectorItems.Add(new SectorItem { sector = s }));

            foreach (var item in assets.GetAssetSectorialDiversificationSync<Equity>(edisRepo)) {
                totalValue += item.Value;
                sectorItems.FirstOrDefault(s => s.sector == item.Key).value += item.Value;
            }
            sectorItems.ForEach(s => s.percentage = s.value / totalValue);
            SectorialPortfolioModel model = new SectorialPortfolioModel {
                data = sectorItems,
                total = totalValue,
                percentage = 1
            };

            return model;
        }

        public EquityLocationDetailsModel GenerateEquityLocationModel(List<AssetBase> assets) {
            EquityLocationDetailsModel model = new EquityLocationDetailsModel { data = new List<EquityDetails>()};

            assets.OfType<Equity>().ToList().ForEach(e => {
                var country = edisRepo.GetStringResearchValueForEquitySync("Country", e.Ticker);
                var countryCode = edisRepo.GetCountryCodeByName(country);
                model.data.Add(new EquityDetails { 
                    Ticker = e.Ticker,
                    Name = e.Name,
                    MarketValue = e.GetTotalMarketValue(),
                    EquityType = edisRepo.GetEnumDescription(e.EquityType),
                    NumberOfUnit = (int)e.TotalNumberOfUnits,
                    Country = country
                });

                if(countryCode != null){
                    model.countryCodes += "'" + countryCode + "',";
                }
            });
            if (model.countryCodes != null) {
                model.countryCodes = model.countryCodes.TrimEnd(',');
            }

            return model;
        }
    }
}
