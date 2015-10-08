using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDISAngular.Models;
using EDISAngular.Models.ServiceModels;
using EDISAngular.Models.ServiceModels.PortfolioModels;
using EDIS_DOMAIN;
using EDIS_DOMAIN.Enum.Enums;
using EDISAngular.Infrastructure.DbFirst;


namespace EDISAngular.Infrastructure.DatabaseAccess
{
    public class PortfolioRepository : BaseRepository
    {
        private Random rdm = new Random();
       // public DBWrapper dbService;
        public PortfolioRepository(edisDbEntities database)
            : base(database)
        {
        }
        public PortfolioRepository() :
            base()
        {
        }

        #region implemented services


        #endregion



        #region portfolio overview services
        public PortfolioSummary Overview_GetSummary_Adviser(string adviserUserId)
        {
            PortfolioSummary result = new PortfolioSummary()
            {
                investment = new SummaryItem()
                {
                    data = new List<DataNameAmountPair>(),
                },
                liability = new SummaryItem() { data = new List<DataNameAmountPair>() },
                networth = new SummaryItem() { data = new List<DataNameAmountPair>() }
            };

            //foreach (var assetType in db.BalanceTypes)
            //{

            //    var clientTransactions = this
            //        .GetAllTransactionsForAdviserWithAssetType(adviserUserId, assetType.ID);
            //    result.investment.data.Add(new DataNameAmountPair
            //    {
            //         //amount=clientTransactions



            //    });


            //}



            return new PortfolioSummary
            {
                investment = new SummaryItem
                {
                    data = new List<DataNameAmountPair>
                    {
                        new DataNameAmountPair{ amount= 12000, name="Australian Equity"},
                        new DataNameAmountPair { amount=2000, name="International Equity"}
                    },
                    total = 50000
                },
                liability = new SummaryItem
                {
                    data = new List<DataNameAmountPair>
                    {
                        new DataNameAmountPair{amount=20000,name="Mortgage & Investment Loans"},
                        new DataNameAmountPair{amount=30000,name="Margin Loans"}
                    },
                    total = 60000
                },
                networth = new SummaryItem
                {
                    data = new List<DataNameAmountPair>
                    {
                        new DataNameAmountPair{amount=30000, name="Investor Equity"},
                        new DataNameAmountPair{amount=500000, name="Non-Investment Asset"}
                    }
                }
            };
        }
        public PortfolioSummary Overview_GetSummary_Client(string clientUserId)
        {
            return new PortfolioSummary
            {
                investment = new SummaryItem
                {
                    data = new List<DataNameAmountPair>
                    {
                        new DataNameAmountPair{ amount= 12000, name="Australian Equity"},
                        new DataNameAmountPair { amount=2000, name="International Equity"}
                    },
                    total = 50000
                },
                liability = new SummaryItem
                {
                    data = new List<DataNameAmountPair>
                    {
                        new DataNameAmountPair{amount=20000,name="Mortgage & Investment Loans"},
                        new DataNameAmountPair{amount=30000,name="Margin Loans"}
                    },
                    total = 60000
                },
                networth = new SummaryItem
                {
                    data = new List<DataNameAmountPair>
                    {
                        new DataNameAmountPair{amount=30000, name="Investor Equity"},
                        new DataNameAmountPair{amount=500000, name="Non-Investment Asset"}
                    }
                }
            };
        }
        public CashflowBriefModel Overview_GetCashflowSummary_Adviser(string adviserUserId)
        {
            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
        }
        public CashflowBriefModel Overview_GetCashflowSummary_Client(string clientUserId)
        {
            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
        }
        public IEnumerable<AssetPerformanceModel> Overview_GetBestPerformingSummary_Adviser(string adviserUserId)
        {
            return new List<AssetPerformanceModel>
            {
                new AssetPerformanceModel{ topic="Best Performing Asset Class", rturn= 10, asset="Asset Name", value= 30000, weighting= 29},
                new AssetPerformanceModel{ topic="Best Performing Investment", weighting= 33, value=75000, rturn=13, asset="Investment Name"}
            };
        }
        public IEnumerable<AssetPerformanceModel> Overview_GetBestPerformingSummary_Client(string clientUserId)
        {
            return new List<AssetPerformanceModel>
            {
                new AssetPerformanceModel{ topic="Best Performing Asset Class", rturn= 10, asset="Asset Name", value= 30000, weighting= 29},
                new AssetPerformanceModel{ topic="Best Performing Investment", weighting= 33, value=75000, rturn=13, asset="Investment Name"}
            };
        }
        public IEnumerable<AssetPerformanceModel> Overview_GetWorstPerformingSummary_Adviser(string adviserUserId)
        {
            return new List<AssetPerformanceModel>
            {
                new AssetPerformanceModel{ topic="Worst Performing Asset Class", rturn= 10, asset="Asset Name", value= 30000, weighting= 29},
                new AssetPerformanceModel{ topic="Worst Performing Investment", weighting= 33, value=75000, rturn=13, asset="Investment Name"}
            };
        }
        public IEnumerable<AssetPerformanceModel> Overview_GetWorstPerformingSummary_Client(string clientUserId)
        {
            return new List<AssetPerformanceModel>
            {
                new AssetPerformanceModel{ topic="Worst Performing Asset Class", rturn= 10, asset="Asset Name", value= 30000, weighting= 29},
                new AssetPerformanceModel{ topic="Worst Performing Investment", weighting= 33, value=75000, rturn=13, asset="Investment Name"}
            };
        }
        public SummaryGeneralInfo Overview_GetGeneral_Adviser(string adviserUserId)
        {
            return new SummaryGeneralInfo
            {
                cost = 10000,
                marketValue = 20000,
                pl = 500000,
                plp = 35
            };
           // throw new NotImplementedException();
            //return dbService.GetCostValueMarketValue(adviserUserId, (int)AssetTypes.AustralianEquity);
        }
        public SummaryGeneralInfo Overview_GetGeneral_Client(string clientUserId)
        {
            return new SummaryGeneralInfo
            {
                cost = 10000,
                marketValue = 20000,
                pl = 500000,
                plp = 35
            };
        }
        public PortfolioStasticsModel Overview_GetPortfolioStat_Adviser(string adviserUserId)
        {
            return new PortfolioStasticsModel
            {
                data = new List<PortfolioStasticsItem>
                {
                    new PortfolioStasticsItem{
                           assetClass= "Australian Equity",
                            costInvestment= 5000,
                            costWeighting= 15,
                            marketValue= 600.20,
                            marketWeighting= 29,
                            pl= 5000,
                            plp= 65,
                            suitability= 25,
                            oneYearReturn= 25,
                            threeYearReturn= 36,
                            fiveYearReturn= 45,
                            earningsPerShare= 500,
                            dividends= 17,
                            dividend= 56,
                            beta= "beta",
                            returnOnAsset= 655,
                            returnOnEquity= 488,
                            priceEarningsRatio= 25,
                            priceBook= "pricebook",
                            priceCashflow= "pricecashflow",
                            avMarketCap= "cap"
                    },
                    new PortfolioStasticsItem{
                           assetClass= "International Equity",
                            costInvestment= 65555,
                            costWeighting= 18,
                            marketValue= 600.20,
                            marketWeighting= 35,
                            pl= 5000,
                            plp= 65,
                            suitability= 60,
                            oneYearReturn= 25,
                            threeYearReturn= 36,
                            fiveYearReturn= 45,
                            earningsPerShare= 500,
                            dividends= 17,
                            dividend= 56,
                            beta= "beta",
                            returnOnAsset= 655,
                            returnOnEquity= 488,
                            priceEarningsRatio= 25,
                            priceBook= "pricebook",
                            priceCashflow= "pricecashflow",
                            avMarketCap= "cap"

                    }
                },
                totalCostInvestment = 505550,
                totalCostWeighting = 100,
                totalMarketValue = 600000,
                totalMarketValueWeighting = 100,
                totalpl = 500000

            };
        }
        public PortfolioRatingModel Overview_GetPortfolioRating_Adviser(string advsierUserId)
        {
            return new PortfolioRatingModel
            {
                risk = 80,
                suitability = 88,
                notSuited = 20
            };
        }
        public PortfolioRatingModel Overview_GetPortfolioRating_Client(string clientUserId)
        {
            return new PortfolioRatingModel
            {
                risk = 80,
                suitability = 88,
                notSuited = 20
            };
        }
        public IEnumerable<StockDataModel> Overview_GetRecentStockData()
        {
            List<StockDataModel> result = new List<StockDataModel>();
            var startYear = 2003;
            Random rdm = new Random();
            for (int i = 0; i < 12; i++)
            {
                result.Add(new StockDataModel
                {

                    AssetName = "TestAsset",
                    year = startYear + i,
                    AssetUnitPrice = RandomMoney(),
                    asxbhp = RandomPercentage(),
                    asxtls= RandomPercentage(),
                    nysewbc= RandomPercentage()
                
                });
            }
            return result;
        }
        public InvestmentPortfolioModel Overview_GetInvestmentData_Adviser(string adviserUserId)
        {
            return new InvestmentPortfolioModel
            {
                data = new List<DataNameAmountPair>
                {
                    new DataNameAmountPair{name="Australian Equity", amount=RandomMoney()},
                    new DataNameAmountPair{name="International Equity", amount=RandomMoney()},
                    new DataNameAmountPair{name="Managed Investments", amount=RandomMoney()},
                    new DataNameAmountPair{name="Direct & Listed Property",amount=RandomMoney()},
                    new DataNameAmountPair{name="Miscellaneous Investments", amount=RandomMoney()},
                    new DataNameAmountPair{name="Fixed Income Investments",amount=RandomMoney()},
                    new DataNameAmountPair{name="Cash & Term Deposit", amount=RandomMoney()},
                },
                total = RandomMoney()
            };
        }
        public InvestmentPortfolioModel Overview_GetInvestmentData_Client(string clientUserId)
        {
            return new InvestmentPortfolioModel
            {
                data = new List<DataNameAmountPair>
                {
                    new DataNameAmountPair{name="Australian Equity", amount=RandomMoney()},
                    new DataNameAmountPair{name="International Equity", amount=RandomMoney()},
                    new DataNameAmountPair{name="Managed Investments", amount=RandomMoney()},
                    new DataNameAmountPair{name="Direct & Listed Property",amount=RandomMoney()},
                    new DataNameAmountPair{name="Miscellaneous Investments", amount=RandomMoney()},
                    new DataNameAmountPair{name="Fixed Income Investments",amount=RandomMoney()},
                    new DataNameAmountPair{name="Cash & Term Deposit", amount=RandomMoney()},
                },
                total = RandomMoney()
            };
        }
        public PortfolioRegionalExposureModel Overview_GetRegionSummary_Adviser(string adviserUserId)
        {
            return new PortfolioRegionalExposureModel
            {
                data = new List<RegionModel>
                {
                    new RegionModel{
                        name="Africa/Middle East",
                        percentage=RandomPercentage(),
                        totalAsset=RandomMoney(),
                        assets=new List<RegionalAssetModel>{
                            new RegionalAssetModel{
                                  country= "Country 1",
                                  assetValue= 10000,
                                  assetWeighting= 25,
                                  latitude= 45,
                                  longitude= 5
                            }
                        }
                    },new RegionModel{
                        name="Europe",
                        percentage=RandomPercentage(),
                        totalAsset=RandomMoney(),
                        assets=new List<RegionalAssetModel>{
                            new RegionalAssetModel{
                                  country= "Country 2",
                                  assetValue= 10000,
                                  assetWeighting= 25,
                                  latitude= 50,
                                  longitude= 10
                            }
                        }
                    },new RegionModel{
                        name="Australia & Pacific Island",
                        percentage=RandomPercentage(),
                        totalAsset=RandomMoney(),
                        assets=new List<RegionalAssetModel>{
                            new RegionalAssetModel{
                                  country= "Country 3",
                                  assetValue= 10000,
                                  assetWeighting= 25,
                                  latitude= 40,
                                  longitude= 8
                            }
                        }
                    },new RegionModel{
                        name="Asia",
                        percentage=RandomPercentage(),
                        totalAsset=RandomMoney(),
                        assets=new List<RegionalAssetModel>{
                            new RegionalAssetModel{
                                  country= "Country 4",
                                  assetValue= 10000,
                                  assetWeighting= 25,
                                  latitude= 43,
                                  longitude= 6
                            }
                        }
                    },new RegionModel{
                        name="North America",
                        percentage=RandomPercentage(),
                        totalAsset=RandomMoney(),
                        assets=new List<RegionalAssetModel>{
                            new RegionalAssetModel{
                                  country= "Country 5",
                                  assetValue= 10000,
                                  assetWeighting= 25,
                                  latitude= 40,
                                  longitude= 15
                            }
                        }
                    }

                },
                total = RandomMoney()
            };
        }
        public PortfolioRegionalExposureModel Overview_GetRegionSummary_Client(string clientUserId)
        {
            return new PortfolioRegionalExposureModel
            {
                data = new List<RegionModel>
                {
                    new RegionModel{
                        name="Africa/Middle East",
                        percentage=RandomPercentage(),
                        totalAsset=RandomMoney(),
                        assets=new List<RegionalAssetModel>{
                            new RegionalAssetModel{
                                  country= "Country 1",
                                  assetValue= 10000,
                                  assetWeighting= 25,
                                  latitude= 45,
                                  longitude= 5
                            }
                        }
                    },new RegionModel{
                        name="Europe",
                        percentage=RandomPercentage(),
                        totalAsset=RandomMoney(),
                        assets=new List<RegionalAssetModel>{
                            new RegionalAssetModel{
                                  country= "Country 2",
                                  assetValue= 10000,
                                  assetWeighting= 25,
                                  latitude= 50,
                                  longitude= 10
                            }
                        }
                    },new RegionModel{
                        name="Australia & Pacific Island",
                        percentage=RandomPercentage(),
                        totalAsset=RandomMoney(),
                        assets=new List<RegionalAssetModel>{
                            new RegionalAssetModel{
                                  country= "Country 3",
                                  assetValue= 10000,
                                  assetWeighting= 25,
                                  latitude= 40,
                                  longitude= 8
                            }
                        }
                    },new RegionModel{
                        name="Asia",
                        percentage=RandomPercentage(),
                        totalAsset=RandomMoney(),
                        assets=new List<RegionalAssetModel>{
                            new RegionalAssetModel{
                                  country= "Country 4",
                                  assetValue= 10000,
                                  assetWeighting= 25,
                                  latitude= 43,
                                  longitude= 6
                            }
                        }
                    },new RegionModel{
                        name="North America",
                        percentage=RandomPercentage(),
                        totalAsset=RandomMoney(),
                        assets=new List<RegionalAssetModel>{
                            new RegionalAssetModel{
                                  country= "Country 5",
                                  assetValue= 10000,
                                  assetWeighting= 25,
                                  latitude= 40,
                                  longitude= 15
                            }
                        }
                    }

                },
                total = RandomMoney()
            };
        }
        public SectorialPortfolioModel Overview_GetSectorialSummary_Adviser(string adviserUserId)
        {
            return new SectorialPortfolioModel
            {
                data = new List<SectorItem>
                {
                    new SectorItem{sector="Business Service", value=RandomMoney(), percentage=RandomPercentage()},
                    new SectorItem{sector="Consumer Goods", value=RandomMoney(), percentage=RandomPercentage()},
                    new SectorItem{sector="Consumer Services", value=RandomMoney(), percentage=RandomPercentage()},
                    new SectorItem{sector="Energy", value=RandomMoney(), percentage=RandomPercentage()},
                    new SectorItem{sector="Financial Services", value=RandomMoney(), percentage=RandomPercentage()},
                    new SectorItem{sector="Hardware", value=RandomMoney(), percentage=RandomPercentage()},
                },
                total = RandomMoney(),
                percentage = RandomPercentage()
            };
        }
        public SectorialPortfolioModel Overview_GetSectorialSummary_Client(string clientUserId)
        {
            return new SectorialPortfolioModel
            {
                data = new List<SectorItem>
                {
                    new SectorItem{sector="Business Service", value=RandomMoney(), percentage=RandomPercentage()},
                    new SectorItem{sector="Consumer Goods", value=RandomMoney(), percentage=RandomPercentage()},
                    new SectorItem{sector="Consumer Services", value=RandomMoney(), percentage=RandomPercentage()},
                    new SectorItem{sector="Energy", value=RandomMoney(), percentage=RandomPercentage()},
                    new SectorItem{sector="Financial Services", value=RandomMoney(), percentage=RandomPercentage()},
                    new SectorItem{sector="Hardware", value=RandomMoney(), percentage=RandomPercentage()},
                },
                total = RandomMoney(),
                percentage = RandomPercentage()
            };
        }
        public CashflowDetailedModel Overview_GetSummaryCashflowDetailed_Adviser(string adviserUserId)
        {
            return new CashflowDetailedModel
            {
                dateSchema = new List<string> { "July", "August", "September", "October" },
                total = new List<CashflowValue>
                {
                    new CashflowValue{income=RandomMoney(),expenses=RandomMoney()},
                    new CashflowValue{income=RandomMoney(),expenses=RandomMoney()},
                    new CashflowValue{income=RandomMoney(),expenses=RandomMoney()},
                    new CashflowValue{income=RandomMoney(),expenses=RandomMoney()},
                },
                data = new List<CashflowDetailedItemGroup>
                {
                   new CashflowDetailedItemGroup{
                       items=new List<CashflowDetailedItem>{
                           new CashflowDetailedItem{ 
                               name="Australian Equity",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           },new CashflowDetailedItem{ 
                               name="International Equity",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           },new CashflowDetailedItem{ 
                               name="Managed Investment",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           },
                           new CashflowDetailedItem{ 
                               name="Direct & Unlisted Property Trust",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           }
                       },
                       type="Investment",
                       total=new List<CashflowValue>{
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                       }
                   },new CashflowDetailedItemGroup{
                       items=new List<CashflowDetailedItem>{
                           new CashflowDetailedItem{ 
                               name="Mortgage & Investment Loans",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           },new CashflowDetailedItem{ 
                               name="Margin Loans",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           },new CashflowDetailedItem{ 
                               name="Credit Card & Miscellaneous Loans",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           }
                       },
                       type="Liability Cashflow",
                       total=new List<CashflowValue>{
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                       }
                   },new CashflowDetailedItemGroup{
                       items=new List<CashflowDetailedItem>{
                           new CashflowDetailedItem{ 
                               name="Non-Investment Cashflow",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           },new CashflowDetailedItem{ 
                               name="Insurance Cashflow",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           },new CashflowDetailedItem{ 
                               name="Miscellaneous Item Cashflow",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           }
                       },
                       type="Miscellaneous Cashflow",
                       total=new List<CashflowValue>{
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                       }
                   }
                }
            };
        }
        public CashflowDetailedModel Overview_GetSummaryCashflowDetailed_Client(string clientUserId)
        {
            return new CashflowDetailedModel
            {
                dateSchema = new List<string> { "July", "August", "September", "October" },
                total = new List<CashflowValue>
                {
                    new CashflowValue{income=RandomMoney(),expenses=RandomMoney()},
                    new CashflowValue{income=RandomMoney(),expenses=RandomMoney()},
                    new CashflowValue{income=RandomMoney(),expenses=RandomMoney()},
                    new CashflowValue{income=RandomMoney(),expenses=RandomMoney()},
                },
                data = new List<CashflowDetailedItemGroup>
                {
                   new CashflowDetailedItemGroup{
                       items=new List<CashflowDetailedItem>{
                           new CashflowDetailedItem{ 
                               name="Australian Equity",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           },new CashflowDetailedItem{ 
                               name="International Equity",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           },new CashflowDetailedItem{ 
                               name="Managed Investment",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           },
                           new CashflowDetailedItem{ 
                               name="Direct & Unlisted Property Trust",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           }
                       },
                       type="Investment",
                       total=new List<CashflowValue>{
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                       }
                   },new CashflowDetailedItemGroup{
                       items=new List<CashflowDetailedItem>{
                           new CashflowDetailedItem{ 
                               name="Mortgage & Investment Loans",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           },new CashflowDetailedItem{ 
                               name="Margin Loans",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           },new CashflowDetailedItem{ 
                               name="Credit Card & Miscellaneous Loans",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           }
                       },
                       type="Liability Cashflow",
                       total=new List<CashflowValue>{
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                       }
                   },new CashflowDetailedItemGroup{
                       items=new List<CashflowDetailedItem>{
                           new CashflowDetailedItem{ 
                               name="Non-Investment Cashflow",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           },new CashflowDetailedItem{ 
                               name="Insurance Cashflow",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           },new CashflowDetailedItem{ 
                               name="Miscellaneous Item Cashflow",
                               data=new List<CashflowValue>{
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                                   new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                               }
                           }
                       },
                       type="Miscellaneous Cashflow",
                       total=new List<CashflowValue>{
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                           new CashflowValue{expenses=RandomMoney(),income=RandomMoney()},
                       }
                   }
                }
            };
        }

        
        #endregion
        #region australian equity portfolio services
        public SummaryGeneralInfo AustralianEquity_GetGeneral_Adviser(string adviserUserId)
        {
            return new SummaryGeneralInfo
            {
                cost = RandomMoney(),
                marketValue = RandomMoney(),
                pl = RandomMoney(),
                plp = RandomPercentage()
            };
           // throw new NotImplementedException();
           //  return dbService.GetCostValueMarketValue(adviserUserId, (int)AssetTypes.AustralianEquity);
        }
        public SummaryGeneralInfo AustralianEquity_GetGeneral_Client(string clientUserId)
        {
            return new SummaryGeneralInfo
            {
                cost = RandomMoney(),
                marketValue = RandomMoney(),
                pl = RandomMoney(),
                plp = RandomPercentage()
            };
        }
        public IEnumerable<EvaluationModel> AustralianEquity_GetModelEvaluation_Adviser(string adviserUserId)
        {
            return new List<EvaluationModel>
            {
                new EvaluationModel{title="Ave 5 Year Return",expected=RandomMoney(), actual=RandomMoney()},
                new EvaluationModel{title="Ave 3 Year Return",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave 1 Year Return",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave Payout Ratio",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave Franking",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave Beta",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave Quick Ratio",expected=RandomMoney(),actual=RandomMoney()},

            };
        }
        public IEnumerable<EvaluationModel> AustralianEquity_GetModelEvaluation_Client(string clientUserId)
        {
            return new List<EvaluationModel>
            {
                new EvaluationModel{title="Ave 5 Year Return",expected=RandomMoney(), actual=RandomMoney()},
                new EvaluationModel{title="Ave 3 Year Return",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave 1 Year Return",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave Payout Ratio",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave Franking",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave Beta",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave Quick Ratio",expected=RandomMoney(),actual=RandomMoney()},
            };
        }
        public CashflowBriefModel AustralianEquity_GetCashflowSummary_Adviser(string adviserUserId)
        {

            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
            //throw new NotImplementedException();
            //return dbService.GetCashflowSummary_Advisor(adviserUserId, (int)AssetTypes.AustralianEquity);
        }
        public CashflowBriefModel AustralianEquity_GetCashflowSummary_Client(string clientUserId)
        {
            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
        }
        public EquityCompanyProfileModel AustralianEquity_GetCompanyProfiles_Adviser(string adviserUserId)
        {
           //throw new NotImplementedException();
            //return dbService.GetCompanyProfiles_Advisor(adviserUserId, (int)AssetTypes.AustralianEquity);
            return new EquityCompanyProfileModel
            {
                data = new List<EquityCompanyProfileItemModel>
                {
                    new EquityCompanyProfileItemModel{
                        asx= "0221",
                        company= "company 1",
                        costValue=RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueWeighting=RandomMoney(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueWeighting=RandomMoney(),
                        marketValueAssetAllocationWeighting=RandomMoney(),
                        companySuitabilityToInvestor=RandomMoney(),
                        earningsPerShare=RandomMoney(),
                        earningsPerShareWeighting=RandomMoney(),
                        earningsPerShareGrowth=RandomMoney(),
                        earningsPerShareGrowthWeighting=RandomMoney(),
                        dividend=RandomMoney(),
                        dividendWeighting=RandomMoney(),
                        franking=RandomMoney(),
                        frankingWeighting=RandomMoney(),
                        dividendYield=RandomMoney(),
                        dividendYieldWeighting=RandomMoney(),
                        priceEarningsRatio=RandomMoney(),
                        priceEarningsRatioWeighting=RandomMoney(),
                        returnOnAsset=RandomMoney(),
                        returnOnAssetWeighting=RandomMoney(),
                        returnOnEquity=RandomMoney(),
                        returnOnEquityWeighting=RandomMoney(),
                        oneYearReturn=RandomMoney(),
                        oneYearReturnWeighting=RandomMoney(),
                        threeYearReturn=RandomMoney(),
                        threeYearReturnWeighting=RandomMoney(),
                        fiveYearReturn=RandomMoney(),
                        fiveYearReturnWeighting=RandomMoney(),
                        beta=RandomMoney(),
                        betaWeighting=RandomMoney(),
                        currentRatio=RandomMoney(),
                        currentRatioWeighting=RandomMoney(),
                        quickRatio=RandomMoney(),
                        quickRatioWeighting=RandomMoney(),
                        debtEquityRatio=RandomMoney(),
                        debtEquityRatioWeighting=RandomMoney(),
                        interestCover=RandomMoney(),
                        interestCoverWeighting=RandomMoney(),
                        payoutRatio=RandomMoney(),
                        payoutRatioWeighting=RandomMoney(),
                        earningsStability=RandomMoney(),
                        earningsStabilityWeighting=RandomMoney(),
                    }, new EquityCompanyProfileItemModel{
                        asx= "0222",
                        company= "company 2",
                        costValue=RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueWeighting=RandomMoney(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueWeighting=RandomMoney(),
                        marketValueAssetAllocationWeighting=RandomMoney(),
                        companySuitabilityToInvestor=RandomMoney(),
                        earningsPerShare=RandomMoney(),
                        earningsPerShareWeighting=RandomMoney(),
                        earningsPerShareGrowth=RandomMoney(),
                        earningsPerShareGrowthWeighting=RandomMoney(),
                        dividend=RandomMoney(),
                        dividendWeighting=RandomMoney(),
                        franking=RandomMoney(),
                        frankingWeighting=RandomMoney(),
                        dividendYield=RandomMoney(),
                        dividendYieldWeighting=RandomMoney(),
                        priceEarningsRatio=RandomMoney(),
                        priceEarningsRatioWeighting=RandomMoney(),
                        returnOnAsset=RandomMoney(),
                        returnOnAssetWeighting=RandomMoney(),
                        returnOnEquity=RandomMoney(),
                        returnOnEquityWeighting=RandomMoney(),
                        oneYearReturn=RandomMoney(),
                        oneYearReturnWeighting=RandomMoney(),
                        threeYearReturn=RandomMoney(),
                        threeYearReturnWeighting=RandomMoney(),
                        fiveYearReturn=RandomMoney(),
                        fiveYearReturnWeighting=RandomMoney(),
                        beta=RandomMoney(),
                        betaWeighting=RandomMoney(),
                        currentRatio=RandomMoney(),
                        currentRatioWeighting=RandomMoney(),
                        quickRatio=RandomMoney(),
                        quickRatioWeighting=RandomMoney(),
                        debtEquityRatio=RandomMoney(),
                        debtEquityRatioWeighting=RandomMoney(),
                        interestCover=RandomMoney(),
                        interestCoverWeighting=RandomMoney(),
                        payoutRatio=RandomMoney(),
                        payoutRatioWeighting=RandomMoney(),
                        earningsStability=RandomMoney(),
                        earningsStabilityWeighting=RandomMoney(),
                    }, new EquityCompanyProfileItemModel{
                        asx= "0223",
                        company= "company 3",
                        costValue=RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueWeighting=RandomMoney(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueWeighting=RandomMoney(),
                        marketValueAssetAllocationWeighting=RandomMoney(),
                        companySuitabilityToInvestor=RandomMoney(),
                        earningsPerShare=RandomMoney(),
                        earningsPerShareWeighting=RandomMoney(),
                        earningsPerShareGrowth=RandomMoney(),
                        earningsPerShareGrowthWeighting=RandomMoney(),
                        dividend=RandomMoney(),
                        dividendWeighting=RandomMoney(),
                        franking=RandomMoney(),
                        frankingWeighting=RandomMoney(),
                        dividendYield=RandomMoney(),
                        dividendYieldWeighting=RandomMoney(),
                        priceEarningsRatio=RandomMoney(),
                        priceEarningsRatioWeighting=RandomMoney(),
                        returnOnAsset=RandomMoney(),
                        returnOnAssetWeighting=RandomMoney(),
                        returnOnEquity=RandomMoney(),
                        returnOnEquityWeighting=RandomMoney(),
                        oneYearReturn=RandomMoney(),
                        oneYearReturnWeighting=RandomMoney(),
                        threeYearReturn=RandomMoney(),
                        threeYearReturnWeighting=RandomMoney(),
                        fiveYearReturn=RandomMoney(),
                        fiveYearReturnWeighting=RandomMoney(),
                        beta=RandomMoney(),
                        betaWeighting=RandomMoney(),
                        currentRatio=RandomMoney(),
                        currentRatioWeighting=RandomMoney(),
                        quickRatio=RandomMoney(),
                        quickRatioWeighting=RandomMoney(),
                        debtEquityRatio=RandomMoney(),
                        debtEquityRatioWeighting=RandomMoney(),
                        interestCover=RandomMoney(),
                        interestCoverWeighting=RandomMoney(),
                        payoutRatio=RandomMoney(),
                        payoutRatioWeighting=RandomMoney(),
                        earningsStability=RandomMoney(),
                        earningsStabilityWeighting=RandomMoney(),
                    }
                },
                totalCostInvestment = RandomMoney(),
                totalCostWeighting = RandomMoney(),
                totalMarketValue = RandomMoney(),
                totalMarketValueWeighting = RandomMoney(),
                totalpl = RandomMoney(),
            };
           

        }
        public EquityCompanyProfileModel AustralianEquity_GetCompanyProfiles_Client(string clientUserId)
        {
            return new EquityCompanyProfileModel
            {
                data = new List<EquityCompanyProfileItemModel>
                {
                    new EquityCompanyProfileItemModel{
                        asx= "0221",
                        company= "company 1",
                        costValue=RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueWeighting=RandomMoney(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueWeighting=RandomMoney(),
                        marketValueAssetAllocationWeighting=RandomMoney(),
                        companySuitabilityToInvestor=RandomMoney(),
                        earningsPerShare=RandomMoney(),
                        earningsPerShareWeighting=RandomMoney(),
                        earningsPerShareGrowth=RandomMoney(),
                        earningsPerShareGrowthWeighting=RandomMoney(),
                        dividend=RandomMoney(),
                        dividendWeighting=RandomMoney(),
                        franking=RandomMoney(),
                        frankingWeighting=RandomMoney(),
                        dividendYield=RandomMoney(),
                        dividendYieldWeighting=RandomMoney(),
                        priceEarningsRatio=RandomMoney(),
                        priceEarningsRatioWeighting=RandomMoney(),
                        returnOnAsset=RandomMoney(),
                        returnOnAssetWeighting=RandomMoney(),
                        returnOnEquity=RandomMoney(),
                        returnOnEquityWeighting=RandomMoney(),
                        oneYearReturn=RandomMoney(),
                        oneYearReturnWeighting=RandomMoney(),
                        threeYearReturn=RandomMoney(),
                        threeYearReturnWeighting=RandomMoney(),
                        fiveYearReturn=RandomMoney(),
                        fiveYearReturnWeighting=RandomMoney(),
                        beta=RandomMoney(),
                        betaWeighting=RandomMoney(),
                        currentRatio=RandomMoney(),
                        currentRatioWeighting=RandomMoney(),
                        quickRatio=RandomMoney(),
                        quickRatioWeighting=RandomMoney(),
                        debtEquityRatio=RandomMoney(),
                        debtEquityRatioWeighting=RandomMoney(),
                        interestCover=RandomMoney(),
                        interestCoverWeighting=RandomMoney(),
                        payoutRatio=RandomMoney(),
                        payoutRatioWeighting=RandomMoney(),
                        earningsStability=RandomMoney(),
                        earningsStabilityWeighting=RandomMoney(),
                    }, new EquityCompanyProfileItemModel{
                        asx= "0222",
                        company= "company 2",
                        costValue=RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueWeighting=RandomMoney(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueWeighting=RandomMoney(),
                        marketValueAssetAllocationWeighting=RandomMoney(),
                        companySuitabilityToInvestor=RandomMoney(),
                        earningsPerShare=RandomMoney(),
                        earningsPerShareWeighting=RandomMoney(),
                        earningsPerShareGrowth=RandomMoney(),
                        earningsPerShareGrowthWeighting=RandomMoney(),
                        dividend=RandomMoney(),
                        dividendWeighting=RandomMoney(),
                        franking=RandomMoney(),
                        frankingWeighting=RandomMoney(),
                        dividendYield=RandomMoney(),
                        dividendYieldWeighting=RandomMoney(),
                        priceEarningsRatio=RandomMoney(),
                        priceEarningsRatioWeighting=RandomMoney(),
                        returnOnAsset=RandomMoney(),
                        returnOnAssetWeighting=RandomMoney(),
                        returnOnEquity=RandomMoney(),
                        returnOnEquityWeighting=RandomMoney(),
                        oneYearReturn=RandomMoney(),
                        oneYearReturnWeighting=RandomMoney(),
                        threeYearReturn=RandomMoney(),
                        threeYearReturnWeighting=RandomMoney(),
                        fiveYearReturn=RandomMoney(),
                        fiveYearReturnWeighting=RandomMoney(),
                        beta=RandomMoney(),
                        betaWeighting=RandomMoney(),
                        currentRatio=RandomMoney(),
                        currentRatioWeighting=RandomMoney(),
                        quickRatio=RandomMoney(),
                        quickRatioWeighting=RandomMoney(),
                        debtEquityRatio=RandomMoney(),
                        debtEquityRatioWeighting=RandomMoney(),
                        interestCover=RandomMoney(),
                        interestCoverWeighting=RandomMoney(),
                        payoutRatio=RandomMoney(),
                        payoutRatioWeighting=RandomMoney(),
                        earningsStability=RandomMoney(),
                        earningsStabilityWeighting=RandomMoney(),
                    }, new EquityCompanyProfileItemModel{
                        asx= "0223",
                        company= "company 3",
                        costValue=RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueWeighting=RandomMoney(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueWeighting=RandomMoney(),
                        marketValueAssetAllocationWeighting=RandomMoney(),
                        companySuitabilityToInvestor=RandomMoney(),
                        earningsPerShare=RandomMoney(),
                        earningsPerShareWeighting=RandomMoney(),
                        earningsPerShareGrowth=RandomMoney(),
                        earningsPerShareGrowthWeighting=RandomMoney(),
                        dividend=RandomMoney(),
                        dividendWeighting=RandomMoney(),
                        franking=RandomMoney(),
                        frankingWeighting=RandomMoney(),
                        dividendYield=RandomMoney(),
                        dividendYieldWeighting=RandomMoney(),
                        priceEarningsRatio=RandomMoney(),
                        priceEarningsRatioWeighting=RandomMoney(),
                        returnOnAsset=RandomMoney(),
                        returnOnAssetWeighting=RandomMoney(),
                        returnOnEquity=RandomMoney(),
                        returnOnEquityWeighting=RandomMoney(),
                        oneYearReturn=RandomMoney(),
                        oneYearReturnWeighting=RandomMoney(),
                        threeYearReturn=RandomMoney(),
                        threeYearReturnWeighting=RandomMoney(),
                        fiveYearReturn=RandomMoney(),
                        fiveYearReturnWeighting=RandomMoney(),
                        beta=RandomMoney(),
                        betaWeighting=RandomMoney(),
                        currentRatio=RandomMoney(),
                        currentRatioWeighting=RandomMoney(),
                        quickRatio=RandomMoney(),
                        quickRatioWeighting=RandomMoney(),
                        debtEquityRatio=RandomMoney(),
                        debtEquityRatioWeighting=RandomMoney(),
                        interestCover=RandomMoney(),
                        interestCoverWeighting=RandomMoney(),
                        payoutRatio=RandomMoney(),
                        payoutRatioWeighting=RandomMoney(),
                        earningsStability=RandomMoney(),
                        earningsStabilityWeighting=RandomMoney(),
                    }
                },
                totalCostInvestment = RandomMoney(),
                totalCostWeighting = RandomMoney(),
                totalMarketValue = RandomMoney(),
                totalMarketValueWeighting = RandomMoney(),
                totalpl = RandomMoney(),
            };
        }
        public IEnumerable<PortfolioQuickStatsModel> AustralianEquity_GetQuickStats_Adviser(string adviserUserId)
        {
            return new List<PortfolioQuickStatsModel>
            {
                new PortfolioQuickStatsModel{name="Ave Earnings Per Share", value="$5.66"},
                new PortfolioQuickStatsModel{name="Ave Earnings Per Share Growth", value="8%"},
                new PortfolioQuickStatsModel{name="Ave Dividend", value="$8.20"},
                new PortfolioQuickStatsModel{name="Ave Beta", value="2.88"},
                new PortfolioQuickStatsModel{name="Ave Current Ratio", value="8.88"},
            };
            //throw new NotImplementedException();
            //return dbService.GetPortfolio_QuickStats(adviserUserId, (int)EDIS_DOMAIN.Enum.Enums.AssetTypes.AustralianEquity);
        }
        public IEnumerable<PortfolioQuickStatsModel> AustralianEquity_GetQuickStats_Client(string clientUserId)
        {
            return new List<PortfolioQuickStatsModel>
            {
                new PortfolioQuickStatsModel{name="Ave Earnings Per Share", value="$5.66"},
                new PortfolioQuickStatsModel{name="Ave Earnings Per Share Growth", value="8%"},
                new PortfolioQuickStatsModel{name="Ave Dividend", value="$8.20"},
                new PortfolioQuickStatsModel{name="Ave Beta", value="2.88"},
                new PortfolioQuickStatsModel{name="Ave Current Ratio", value="8.88"},
            };
        }
        public PortfolioRatingModel AustralianEquity_GetPortfolioRating_Adviser(string adviserUserId)
        {
            return new PortfolioRatingModel
            {
                data = new List<PortfolioRatingItemModel>
                {
                    new PortfolioRatingItemModel{name="Market Capitalisation", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Dividend Yield", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Price Earnings Ratio", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Return on Asset", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Return on Equity", score=RandomPercentage(), weighting=RandomPercentage()},
                },
                suitability = RandomPercentage(),
                notSuited = RandomPercentage(),
                risk = RandomPercentage()
            };
            //throw new NotImplementedException();
            // return dbService.GetPortfolioRating_Adviser(adviserUserId, (int)AssetTypes.AustralianEquity);
        }
        public PortfolioRatingModel AustralianEquity_GetPortfolioRating_Client(string clientUserId)
        {
            return new PortfolioRatingModel
            {
                data = new List<PortfolioRatingItemModel>
                {
                    new PortfolioRatingItemModel{name="Market Capitalisation", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Dividend Yield", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Price Earnings Ratio", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Return on Asset", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Return on Equity", score=RandomPercentage(), weighting=RandomPercentage()},
                },
                suitability = RandomPercentage(),
                notSuited = RandomPercentage(),
                risk = RandomPercentage()
            };
        }
        public EquityDiversificationModel AustralianEquity_GetDiversification_Adviser(string adviserUserId)
        {
            return new EquityDiversificationModel
            {
                total = new EquityDiversificationTotalModel
                {
                    actualPercentage = RandomPercentage(),
                    sectorialExposureValue = RandomMoney(),
                    sectorialExposurePercentage = RandomPercentage(),
                    asxSectorialExposurePercentage = RandomPercentage(),
                    asxDifferencesPercentage = RandomPercentage(),
                    profileAllocationPercentage = RandomPercentage(),
                    indicator = "E/Unsuitable",
                    profileDifferencesPercentage = RandomPercentage(),
                    suitabilityIndicator = RandomPercentage()
                },
                data = new List<EquityDiversificationDataItem>
                {
                    new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Consumer Goods"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Consumer Services"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Energy"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Financial Services"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Hardware"
                    }
                }

            };
        }
        public EquityDiversificationModel AustralianEquity_GetDiversification_Client(string clientUserId)
        {
            return new EquityDiversificationModel
            {
                total = new EquityDiversificationTotalModel
                {
                    actualPercentage = RandomPercentage(),
                    sectorialExposureValue = RandomMoney(),
                    sectorialExposurePercentage = RandomPercentage(),
                    asxSectorialExposurePercentage = RandomPercentage(),
                    asxDifferencesPercentage = RandomPercentage(),
                    profileAllocationPercentage = RandomPercentage(),
                    indicator = "E/Unsuitable",
                    profileDifferencesPercentage = RandomPercentage(),
                    suitabilityIndicator = RandomPercentage()
                },
                data = new List<EquityDiversificationDataItem>
                {
                    new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Consumer Goods"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Consumer Services"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Energy"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Financial Services"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Hardware"
                    }
                }

            };
        }
        public EquityCashflowDetailedModel AustralianEquity_GetSummaryCashflowDetailed_Adviser(string adviserUserId)
        {
            return new EquityCashflowDetailedModel
            {
                dateSchema = new List<string> { "July", "August", "September", "October" },
                total = new List<EquityCashflowDetailedValue>
                {
                    new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                },
                data = new List<EquityCashflowDetailedItem>
                {
                   new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 1",
                       asx="20001"
                   },new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 2",
                       asx="20002"
                   },new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 3",
                       asx="20003"
                   },new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 4",
                       asx="20004"
                   },new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 5",
                       asx="20005"
                   }
                }
            };
        }
        public EquityCashflowDetailedModel AustralianEquity_GetSummaryCashflowDetailed_Client(string clientUserId)
        {
            return new EquityCashflowDetailedModel
            {
                dateSchema = new List<string> { "July", "August", "September", "October" },
                total = new List<EquityCashflowDetailedValue>
                {
                    new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                },
                data = new List<EquityCashflowDetailedItem>
                {
                   new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 1",
                       asx="20001"
                   },new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 2",
                       asx="20002"
                   },new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 3",
                       asx="20003"
                   },new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 4",
                       asx="20004"
                   },new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 5",
                       asx="20005"
                   }
                }
            };
        }
        #endregion
        #region international equity portfolio services
        public SummaryGeneralInfo InternationalEquity_GetGeneral_Adviser(string adviserUserId)
        {
            return new SummaryGeneralInfo
            {
                cost = RandomMoney(),
                marketValue = RandomMoney(),
                pl = RandomMoney(),
                plp = RandomPercentage()
            };
            //throw new NotImplementedException();
            //return dbService.GetCostValueMarketValue(adviserUserId, (int)AssetTypes.InternationalEquity);
        }
        public SummaryGeneralInfo InternationalEquity_GetGeneral_Client(string clientUserId)
        {
            return new SummaryGeneralInfo
            {
                cost = RandomMoney(),
                marketValue = RandomMoney(),
                pl = RandomMoney(),
                plp = RandomPercentage()
            };
        }
        public IEnumerable<EvaluationModel> InternationalEquity_GetModelEvaluation_Adviser(string adviserUserId)
        {
            return new List<EvaluationModel>
            {
                new EvaluationModel{title="Ave 5 Year Return",expected=RandomMoney(), actual=RandomMoney()},
                new EvaluationModel{title="Ave 3 Year Return",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave 1 Year Return",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave Payout Ratio",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave Franking",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave Beta",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave Quick Ratio",expected=RandomMoney(),actual=RandomMoney()},

            };
        }
        public IEnumerable<EvaluationModel> InternationalEquity_GetModelEvaluation_Client(string clientUserId)
        {
            return new List<EvaluationModel>
            {
                new EvaluationModel{title="Ave 5 Year Return",expected=RandomMoney(), actual=RandomMoney()},
                new EvaluationModel{title="Ave 3 Year Return",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave 1 Year Return",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave Payout Ratio",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave Franking",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave Beta",expected=RandomMoney(),actual=RandomMoney()},
                new EvaluationModel{title="Ave Quick Ratio",expected=RandomMoney(),actual=RandomMoney()},
            };
        }
        public CashflowBriefModel InternationalEquity_GetCashflowSummary_Adviser(string adviserUserId)
        {
            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
            //throw new NotImplementedException();
            //return dbService.GetCashflowSummary_Advisor(adviserUserId, (int)AssetTypes.InternationalEquity);

        }
        public CashflowBriefModel InternationalEquity_GetCashflowSummary_Client(string clientUserId)
        {
            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
        }
        public EquityCompanyProfileModel InternationalEquity_GetCompanyProfiles_Adviser(string adviserUserId)
        {
            return new EquityCompanyProfileModel
            {
                data = new List<EquityCompanyProfileItemModel>
                {
                    new EquityCompanyProfileItemModel{
                        asx= "0221",
                        company= "company 1",
                        costValue=RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueWeighting=RandomMoney(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueWeighting=RandomMoney(),
                        marketValueAssetAllocationWeighting=RandomMoney(),
                        companySuitabilityToInvestor=RandomMoney(),
                        earningsPerShare=RandomMoney(),
                        earningsPerShareWeighting=RandomMoney(),
                        earningsPerShareGrowth=RandomMoney(),
                        earningsPerShareGrowthWeighting=RandomMoney(),
                        dividend=RandomMoney(),
                        dividendWeighting=RandomMoney(),
                        franking=RandomMoney(),
                        frankingWeighting=RandomMoney(),
                        dividendYield=RandomMoney(),
                        dividendYieldWeighting=RandomMoney(),
                        priceEarningsRatio=RandomMoney(),
                        priceEarningsRatioWeighting=RandomMoney(),
                        returnOnAsset=RandomMoney(),
                        returnOnAssetWeighting=RandomMoney(),
                        returnOnEquity=RandomMoney(),
                        returnOnEquityWeighting=RandomMoney(),
                        oneYearReturn=RandomMoney(),
                        oneYearReturnWeighting=RandomMoney(),
                        threeYearReturn=RandomMoney(),
                        threeYearReturnWeighting=RandomMoney(),
                        fiveYearReturn=RandomMoney(),
                        fiveYearReturnWeighting=RandomMoney(),
                        beta=RandomMoney(),
                        betaWeighting=RandomMoney(),
                        currentRatio=RandomMoney(),
                        currentRatioWeighting=RandomMoney(),
                        quickRatio=RandomMoney(),
                        quickRatioWeighting=RandomMoney(),
                        debtEquityRatio=RandomMoney(),
                        debtEquityRatioWeighting=RandomMoney(),
                        interestCover=RandomMoney(),
                        interestCoverWeighting=RandomMoney(),
                        payoutRatio=RandomMoney(),
                        payoutRatioWeighting=RandomMoney(),
                        earningsStability=RandomMoney(),
                        earningsStabilityWeighting=RandomMoney(),
                    }, new EquityCompanyProfileItemModel{
                        asx= "0222",
                        company= "company 2",
                        costValue=RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueWeighting=RandomMoney(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueWeighting=RandomMoney(),
                        marketValueAssetAllocationWeighting=RandomMoney(),
                        companySuitabilityToInvestor=RandomMoney(),
                        earningsPerShare=RandomMoney(),
                        earningsPerShareWeighting=RandomMoney(),
                        earningsPerShareGrowth=RandomMoney(),
                        earningsPerShareGrowthWeighting=RandomMoney(),
                        dividend=RandomMoney(),
                        dividendWeighting=RandomMoney(),
                        franking=RandomMoney(),
                        frankingWeighting=RandomMoney(),
                        dividendYield=RandomMoney(),
                        dividendYieldWeighting=RandomMoney(),
                        priceEarningsRatio=RandomMoney(),
                        priceEarningsRatioWeighting=RandomMoney(),
                        returnOnAsset=RandomMoney(),
                        returnOnAssetWeighting=RandomMoney(),
                        returnOnEquity=RandomMoney(),
                        returnOnEquityWeighting=RandomMoney(),
                        oneYearReturn=RandomMoney(),
                        oneYearReturnWeighting=RandomMoney(),
                        threeYearReturn=RandomMoney(),
                        threeYearReturnWeighting=RandomMoney(),
                        fiveYearReturn=RandomMoney(),
                        fiveYearReturnWeighting=RandomMoney(),
                        beta=RandomMoney(),
                        betaWeighting=RandomMoney(),
                        currentRatio=RandomMoney(),
                        currentRatioWeighting=RandomMoney(),
                        quickRatio=RandomMoney(),
                        quickRatioWeighting=RandomMoney(),
                        debtEquityRatio=RandomMoney(),
                        debtEquityRatioWeighting=RandomMoney(),
                        interestCover=RandomMoney(),
                        interestCoverWeighting=RandomMoney(),
                        payoutRatio=RandomMoney(),
                        payoutRatioWeighting=RandomMoney(),
                        earningsStability=RandomMoney(),
                        earningsStabilityWeighting=RandomMoney(),
                    }, new EquityCompanyProfileItemModel{
                        asx= "0223",
                        company= "company 3",
                        costValue=RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueWeighting=RandomMoney(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueWeighting=RandomMoney(),
                        marketValueAssetAllocationWeighting=RandomMoney(),
                        companySuitabilityToInvestor=RandomMoney(),
                        earningsPerShare=RandomMoney(),
                        earningsPerShareWeighting=RandomMoney(),
                        earningsPerShareGrowth=RandomMoney(),
                        earningsPerShareGrowthWeighting=RandomMoney(),
                        dividend=RandomMoney(),
                        dividendWeighting=RandomMoney(),
                        franking=RandomMoney(),
                        frankingWeighting=RandomMoney(),
                        dividendYield=RandomMoney(),
                        dividendYieldWeighting=RandomMoney(),
                        priceEarningsRatio=RandomMoney(),
                        priceEarningsRatioWeighting=RandomMoney(),
                        returnOnAsset=RandomMoney(),
                        returnOnAssetWeighting=RandomMoney(),
                        returnOnEquity=RandomMoney(),
                        returnOnEquityWeighting=RandomMoney(),
                        oneYearReturn=RandomMoney(),
                        oneYearReturnWeighting=RandomMoney(),
                        threeYearReturn=RandomMoney(),
                        threeYearReturnWeighting=RandomMoney(),
                        fiveYearReturn=RandomMoney(),
                        fiveYearReturnWeighting=RandomMoney(),
                        beta=RandomMoney(),
                        betaWeighting=RandomMoney(),
                        currentRatio=RandomMoney(),
                        currentRatioWeighting=RandomMoney(),
                        quickRatio=RandomMoney(),
                        quickRatioWeighting=RandomMoney(),
                        debtEquityRatio=RandomMoney(),
                        debtEquityRatioWeighting=RandomMoney(),
                        interestCover=RandomMoney(),
                        interestCoverWeighting=RandomMoney(),
                        payoutRatio=RandomMoney(),
                        payoutRatioWeighting=RandomMoney(),
                        earningsStability=RandomMoney(),
                        earningsStabilityWeighting=RandomMoney(),
                    }
                },
                totalCostInvestment = RandomMoney(),
                totalCostWeighting = RandomMoney(),
                totalMarketValue = RandomMoney(),
                totalMarketValueWeighting = RandomMoney(),
                totalpl = RandomMoney(),
            };
            //throw new NotImplementedException();
            //return dbService.GetCompanyProfiles_Advisor(adviserUserId, (int)AssetTypes.InternationalEquity);

        }
        public EquityCompanyProfileModel InternationalEquity_GetCompanyProfiles_Client(string clientUserId)
        {
            return new EquityCompanyProfileModel
            {
                data = new List<EquityCompanyProfileItemModel>
                {
                    new EquityCompanyProfileItemModel{
                        asx= "0221",
                        company= "company 1",
                        costValue=RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueWeighting=RandomMoney(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueWeighting=RandomMoney(),
                        marketValueAssetAllocationWeighting=RandomMoney(),
                        companySuitabilityToInvestor=RandomMoney(),
                        earningsPerShare=RandomMoney(),
                        earningsPerShareWeighting=RandomMoney(),
                        earningsPerShareGrowth=RandomMoney(),
                        earningsPerShareGrowthWeighting=RandomMoney(),
                        dividend=RandomMoney(),
                        dividendWeighting=RandomMoney(),
                        franking=RandomMoney(),
                        frankingWeighting=RandomMoney(),
                        dividendYield=RandomMoney(),
                        dividendYieldWeighting=RandomMoney(),
                        priceEarningsRatio=RandomMoney(),
                        priceEarningsRatioWeighting=RandomMoney(),
                        returnOnAsset=RandomMoney(),
                        returnOnAssetWeighting=RandomMoney(),
                        returnOnEquity=RandomMoney(),
                        returnOnEquityWeighting=RandomMoney(),
                        oneYearReturn=RandomMoney(),
                        oneYearReturnWeighting=RandomMoney(),
                        threeYearReturn=RandomMoney(),
                        threeYearReturnWeighting=RandomMoney(),
                        fiveYearReturn=RandomMoney(),
                        fiveYearReturnWeighting=RandomMoney(),
                        beta=RandomMoney(),
                        betaWeighting=RandomMoney(),
                        currentRatio=RandomMoney(),
                        currentRatioWeighting=RandomMoney(),
                        quickRatio=RandomMoney(),
                        quickRatioWeighting=RandomMoney(),
                        debtEquityRatio=RandomMoney(),
                        debtEquityRatioWeighting=RandomMoney(),
                        interestCover=RandomMoney(),
                        interestCoverWeighting=RandomMoney(),
                        payoutRatio=RandomMoney(),
                        payoutRatioWeighting=RandomMoney(),
                        earningsStability=RandomMoney(),
                        earningsStabilityWeighting=RandomMoney(),
                    }, new EquityCompanyProfileItemModel{
                        asx= "0222",
                        company= "company 2",
                        costValue=RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueWeighting=RandomMoney(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueWeighting=RandomMoney(),
                        marketValueAssetAllocationWeighting=RandomMoney(),
                        companySuitabilityToInvestor=RandomMoney(),
                        earningsPerShare=RandomMoney(),
                        earningsPerShareWeighting=RandomMoney(),
                        earningsPerShareGrowth=RandomMoney(),
                        earningsPerShareGrowthWeighting=RandomMoney(),
                        dividend=RandomMoney(),
                        dividendWeighting=RandomMoney(),
                        franking=RandomMoney(),
                        frankingWeighting=RandomMoney(),
                        dividendYield=RandomMoney(),
                        dividendYieldWeighting=RandomMoney(),
                        priceEarningsRatio=RandomMoney(),
                        priceEarningsRatioWeighting=RandomMoney(),
                        returnOnAsset=RandomMoney(),
                        returnOnAssetWeighting=RandomMoney(),
                        returnOnEquity=RandomMoney(),
                        returnOnEquityWeighting=RandomMoney(),
                        oneYearReturn=RandomMoney(),
                        oneYearReturnWeighting=RandomMoney(),
                        threeYearReturn=RandomMoney(),
                        threeYearReturnWeighting=RandomMoney(),
                        fiveYearReturn=RandomMoney(),
                        fiveYearReturnWeighting=RandomMoney(),
                        beta=RandomMoney(),
                        betaWeighting=RandomMoney(),
                        currentRatio=RandomMoney(),
                        currentRatioWeighting=RandomMoney(),
                        quickRatio=RandomMoney(),
                        quickRatioWeighting=RandomMoney(),
                        debtEquityRatio=RandomMoney(),
                        debtEquityRatioWeighting=RandomMoney(),
                        interestCover=RandomMoney(),
                        interestCoverWeighting=RandomMoney(),
                        payoutRatio=RandomMoney(),
                        payoutRatioWeighting=RandomMoney(),
                        earningsStability=RandomMoney(),
                        earningsStabilityWeighting=RandomMoney(),
                    }, new EquityCompanyProfileItemModel{
                        asx= "0223",
                        company= "company 3",
                        costValue=RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueWeighting=RandomMoney(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueWeighting=RandomMoney(),
                        marketValueAssetAllocationWeighting=RandomMoney(),
                        companySuitabilityToInvestor=RandomMoney(),
                        earningsPerShare=RandomMoney(),
                        earningsPerShareWeighting=RandomMoney(),
                        earningsPerShareGrowth=RandomMoney(),
                        earningsPerShareGrowthWeighting=RandomMoney(),
                        dividend=RandomMoney(),
                        dividendWeighting=RandomMoney(),
                        franking=RandomMoney(),
                        frankingWeighting=RandomMoney(),
                        dividendYield=RandomMoney(),
                        dividendYieldWeighting=RandomMoney(),
                        priceEarningsRatio=RandomMoney(),
                        priceEarningsRatioWeighting=RandomMoney(),
                        returnOnAsset=RandomMoney(),
                        returnOnAssetWeighting=RandomMoney(),
                        returnOnEquity=RandomMoney(),
                        returnOnEquityWeighting=RandomMoney(),
                        oneYearReturn=RandomMoney(),
                        oneYearReturnWeighting=RandomMoney(),
                        threeYearReturn=RandomMoney(),
                        threeYearReturnWeighting=RandomMoney(),
                        fiveYearReturn=RandomMoney(),
                        fiveYearReturnWeighting=RandomMoney(),
                        beta=RandomMoney(),
                        betaWeighting=RandomMoney(),
                        currentRatio=RandomMoney(),
                        currentRatioWeighting=RandomMoney(),
                        quickRatio=RandomMoney(),
                        quickRatioWeighting=RandomMoney(),
                        debtEquityRatio=RandomMoney(),
                        debtEquityRatioWeighting=RandomMoney(),
                        interestCover=RandomMoney(),
                        interestCoverWeighting=RandomMoney(),
                        payoutRatio=RandomMoney(),
                        payoutRatioWeighting=RandomMoney(),
                        earningsStability=RandomMoney(),
                        earningsStabilityWeighting=RandomMoney(),
                    }
                },
                totalCostInvestment = RandomMoney(),
                totalCostWeighting = RandomMoney(),
                totalMarketValue = RandomMoney(),
                totalMarketValueWeighting = RandomMoney(),
                totalpl = RandomMoney(),
            };
        }
        public IEnumerable<PortfolioQuickStatsModel> InternationalEquity_GetQuickStats_Adviser(string adviserUserId)
        {
            return new List<PortfolioQuickStatsModel>
            {
                new PortfolioQuickStatsModel{name="Ave Earnings Per Share", value="$5.66"},
                new PortfolioQuickStatsModel{name="Ave Earnings Per Share Growth", value="8%"},
                new PortfolioQuickStatsModel{name="Ave Dividend", value="$8.20"},
                new PortfolioQuickStatsModel{name="Ave Beta", value="2.88"},
                new PortfolioQuickStatsModel{name="Ave Current Ratio", value="8.88"},
            };
        }
        public IEnumerable<PortfolioQuickStatsModel> InternationalEquity_GetQuickStats_Client(string clientUserId)
        {
            return new List<PortfolioQuickStatsModel>
            {
                new PortfolioQuickStatsModel{name="Ave Earnings Per Share", value="$5.66"},
                new PortfolioQuickStatsModel{name="Ave Earnings Per Share Growth", value="8%"},
                new PortfolioQuickStatsModel{name="Ave Dividend", value="$8.20"},
                new PortfolioQuickStatsModel{name="Ave Beta", value="2.88"},
                new PortfolioQuickStatsModel{name="Ave Current Ratio", value="8.88"},
            };
        }
        public PortfolioRatingModel InternationalEquity_GetPortfolioRating_Adviser(string adviserUserId)
        {
            return new PortfolioRatingModel
            {
                data = new List<PortfolioRatingItemModel>
                {
                    new PortfolioRatingItemModel{name="Market Capitalisation", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Dividend Yield", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Price Earnings Ratio", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Return on Asset", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Return on Equity", score=RandomPercentage(), weighting=RandomPercentage()},
                },
                suitability = RandomPercentage(),
                notSuited = RandomPercentage(),
                risk = RandomPercentage()
            };
            //throw new NotImplementedException();
            //return dbService.GetPortfolioRating_Adviser(adviserUserId, (int)AssetTypes.InternationalEquity);
        }
        public PortfolioRatingModel InternationalEquity_GetPortfolioRating_Client(string clientUserId)
        {
            return new PortfolioRatingModel
            {
                data = new List<PortfolioRatingItemModel>
                {
                    new PortfolioRatingItemModel{name="Market Capitalisation", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Dividend Yield", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Price Earnings Ratio", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Return on Asset", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Return on Equity", score=RandomPercentage(), weighting=RandomPercentage()},
                },
                suitability = RandomPercentage(),
                notSuited = RandomPercentage(),
                risk = RandomPercentage()
            };
        }
        public EquityDiversificationModel InternationalEquity_GetDiversification_Adviser(string adviserUserId)
        {
            return new EquityDiversificationModel
            {
                total = new EquityDiversificationTotalModel
                {
                    actualPercentage = RandomPercentage(),
                    sectorialExposureValue = RandomMoney(),
                    sectorialExposurePercentage = RandomPercentage(),
                    asxSectorialExposurePercentage = RandomPercentage(),
                    asxDifferencesPercentage = RandomPercentage(),
                    profileAllocationPercentage = RandomPercentage(),
                    indicator = "E/Unsuitable",
                    profileDifferencesPercentage = RandomPercentage(),
                    suitabilityIndicator = RandomPercentage()
                },
                data = new List<EquityDiversificationDataItem>
                {
                    new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Consumer Goods"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Consumer Services"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Energy"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Financial Services"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Hardware"
                    }
                }

            };
        }
        public EquityDiversificationModel InternationalEquity_GetDiversification_Client(string clientUserId)
        {
            return new EquityDiversificationModel
            {
                total = new EquityDiversificationTotalModel
                {
                    actualPercentage = RandomPercentage(),
                    sectorialExposureValue = RandomMoney(),
                    sectorialExposurePercentage = RandomPercentage(),
                    asxSectorialExposurePercentage = RandomPercentage(),
                    asxDifferencesPercentage = RandomPercentage(),
                    profileAllocationPercentage = RandomPercentage(),
                    indicator = "E/Unsuitable",
                    profileDifferencesPercentage = RandomPercentage(),
                    suitabilityIndicator = RandomPercentage()
                },
                data = new List<EquityDiversificationDataItem>
                {
                    new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Consumer Goods"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Consumer Services"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Energy"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Financial Services"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Hardware"
                    }
                }

            };
        }
        public EquityCashflowDetailedModel InternationalEquity_GetSummaryCashflowDetailed_Adviser(string adviserUserId)
        {
            return new EquityCashflowDetailedModel
            {
                dateSchema = new List<string> { "July", "August", "September", "October" },
                total = new List<EquityCashflowDetailedValue>
                {
                    new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                },
                data = new List<EquityCashflowDetailedItem>
                {
                   new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 1",
                       asx="20001"
                   },new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 2",
                       asx="20002"
                   },new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 3",
                       asx="20003"
                   },new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 4",
                       asx="20004"
                   },new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 5",
                       asx="20005"
                   }
                }
            };
        }
        public EquityCashflowDetailedModel InternationalEquity_GetSummaryCashflowDetailed_Client(string clientUserId)
        {
            return new EquityCashflowDetailedModel
            {
                dateSchema = new List<string> { "July", "August", "September", "October" },
                total = new List<EquityCashflowDetailedValue>
                {
                    new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                },
                data = new List<EquityCashflowDetailedItem>
                {
                   new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 1",
                       asx="20001"
                   },new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 2",
                       asx="20002"
                   },new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 3",
                       asx="20003"
                   },new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 4",
                       asx="20004"
                   },new EquityCashflowDetailedItem{
                       data=new List<EquityCashflowDetailedValue>{
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new EquityCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 5",
                       asx="20005"
                   }
                }
            };
        }
        #endregion
        #region managed investment portfolio services
        public SummaryGeneralInfo ManagedInvestment_GetGeneral_Adviser(string adviserUserId)
        {
            return new SummaryGeneralInfo { 
               cost  =100, marketValue =100 , pl=50, plp=50
            };
            //throw new NotImplementedException();
            //return dbService.GetCostValueMarketValue(adviserUserId, (int)AssetTypes.ManagedInvestments);
        }
        public SummaryGeneralInfo ManagedInvestment_GetGeneral_Client(string clientUserId)
        {
            return new SummaryGeneralInfo
            {
                cost = RandomMoney(),
                marketValue = RandomMoney(),
                pl = RandomMoney(),
                plp = RandomPercentage()
            };
        }
        public CashflowBriefModel ManagedInvestment_GetCashflowSummary_Adviser(string adviserUserId)
        {
            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
            //throw new NotImplementedException();
            //return dbService.GetCashflowSummary_Advisor(adviserUserId, (int)AssetTypes.ManagedInvestments);
        }
        public CashflowBriefModel ManagedInvestment_GetCashflowSummary_Client(string clientUserId)
        {
            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
        }
        public ManagedInvestmentCompanyProfileModel ManagedInvestment_GetCompanyProfiles_Adviser(string adviserUserId)
        {
            return new ManagedInvestmentCompanyProfileModel
            {
                data = new List<ManagedInvestmentCompanyProfileItem>
                {
                    new ManagedInvestmentCompanyProfileItem{
                        ticker= "0221",
                        company= "company 1",
                        costValue=RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueWeighting=RandomMoney(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueWeighting=RandomMoney(),
                        marketValueAssetAllocationWeighting=RandomMoney(),
                        companySuitabilityToInvestor=RandomMoney(),
                        earningsPerShare=RandomMoney(),
                        earningsPerShareWeighting=RandomMoney(),
                        earningsPerShareGrowth=RandomMoney(),
                        earningsPerShareGrowthWeighting=RandomMoney(),
                        dividend=RandomMoney(),
                        dividendWeighting=RandomMoney(),
                        franking=RandomMoney(),
                        frankingWeighting=RandomMoney(),
                        dividendYield=RandomMoney(),
                        dividendYieldWeighting=RandomMoney(),
                        priceEarningsRatio=RandomMoney(),
                        priceEarningsRatioWeighting=RandomMoney(),
                        returnOnAsset=RandomMoney(),
                        returnOnAssetWeighting=RandomMoney(),
                        returnOnEquity=RandomMoney(),
                        returnOnEquityWeighting=RandomMoney(),
                        oneYearReturn=RandomMoney(),
                        oneYearReturnWeighting=RandomMoney(),
                        threeYearReturn=RandomMoney(),
                        threeYearReturnWeighting=RandomMoney(),
                        fiveYearReturn=RandomMoney(),
                        fiveYearReturnWeighting=RandomMoney(),
                        beta=RandomMoney(),
                        betaWeighting=RandomMoney(),
                        currentRatio=RandomMoney(),
                        currentRatioWeighting=RandomMoney(),
                        quickRatio=RandomMoney(),
                        quickRatioWeighting=RandomMoney(),
                        debtEquityRatio=RandomMoney(),
                        debtEquityRatioWeighting=RandomMoney(),
                        interestCover=RandomMoney(),
                        interestCoverWeighting=RandomMoney(),
                        payoutRatio=RandomMoney(),
                        payoutRatioWeighting=RandomMoney(),
                        earningsStability=RandomMoney(),
                        earningsStabilityWeighting=RandomMoney(),
                    }, new ManagedInvestmentCompanyProfileItem{
                        ticker= "0222",
                        company= "company 2",
                        costValue=RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueWeighting=RandomMoney(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueWeighting=RandomMoney(),
                        marketValueAssetAllocationWeighting=RandomMoney(),
                        companySuitabilityToInvestor=RandomMoney(),
                        earningsPerShare=RandomMoney(),
                        earningsPerShareWeighting=RandomMoney(),
                        earningsPerShareGrowth=RandomMoney(),
                        earningsPerShareGrowthWeighting=RandomMoney(),
                        dividend=RandomMoney(),
                        dividendWeighting=RandomMoney(),
                        franking=RandomMoney(),
                        frankingWeighting=RandomMoney(),
                        dividendYield=RandomMoney(),
                        dividendYieldWeighting=RandomMoney(),
                        priceEarningsRatio=RandomMoney(),
                        priceEarningsRatioWeighting=RandomMoney(),
                        returnOnAsset=RandomMoney(),
                        returnOnAssetWeighting=RandomMoney(),
                        returnOnEquity=RandomMoney(),
                        returnOnEquityWeighting=RandomMoney(),
                        oneYearReturn=RandomMoney(),
                        oneYearReturnWeighting=RandomMoney(),
                        threeYearReturn=RandomMoney(),
                        threeYearReturnWeighting=RandomMoney(),
                        fiveYearReturn=RandomMoney(),
                        fiveYearReturnWeighting=RandomMoney(),
                        beta=RandomMoney(),
                        betaWeighting=RandomMoney(),
                        currentRatio=RandomMoney(),
                        currentRatioWeighting=RandomMoney(),
                        quickRatio=RandomMoney(),
                        quickRatioWeighting=RandomMoney(),
                        debtEquityRatio=RandomMoney(),
                        debtEquityRatioWeighting=RandomMoney(),
                        interestCover=RandomMoney(),
                        interestCoverWeighting=RandomMoney(),
                        payoutRatio=RandomMoney(),
                        payoutRatioWeighting=RandomMoney(),
                        earningsStability=RandomMoney(),
                        earningsStabilityWeighting=RandomMoney(),
                    }, new ManagedInvestmentCompanyProfileItem{
                        ticker = "0223",
                        company= "company 3",
                        costValue=RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueWeighting=RandomMoney(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueWeighting=RandomMoney(),
                        marketValueAssetAllocationWeighting=RandomMoney(),
                        companySuitabilityToInvestor=RandomMoney(),
                        earningsPerShare=RandomMoney(),
                        earningsPerShareWeighting=RandomMoney(),
                        earningsPerShareGrowth=RandomMoney(),
                        earningsPerShareGrowthWeighting=RandomMoney(),
                        dividend=RandomMoney(),
                        dividendWeighting=RandomMoney(),
                        franking=RandomMoney(),
                        frankingWeighting=RandomMoney(),
                        dividendYield=RandomMoney(),
                        dividendYieldWeighting=RandomMoney(),
                        priceEarningsRatio=RandomMoney(),
                        priceEarningsRatioWeighting=RandomMoney(),
                        returnOnAsset=RandomMoney(),
                        returnOnAssetWeighting=RandomMoney(),
                        returnOnEquity=RandomMoney(),
                        returnOnEquityWeighting=RandomMoney(),
                        oneYearReturn=RandomMoney(),
                        oneYearReturnWeighting=RandomMoney(),
                        threeYearReturn=RandomMoney(),
                        threeYearReturnWeighting=RandomMoney(),
                        fiveYearReturn=RandomMoney(),
                        fiveYearReturnWeighting=RandomMoney(),
                        beta=RandomMoney(),
                        betaWeighting=RandomMoney(),
                        currentRatio=RandomMoney(),
                        currentRatioWeighting=RandomMoney(),
                        quickRatio=RandomMoney(),
                        quickRatioWeighting=RandomMoney(),
                        debtEquityRatio=RandomMoney(),
                        debtEquityRatioWeighting=RandomMoney(),
                        interestCover=RandomMoney(),
                        interestCoverWeighting=RandomMoney(),
                        payoutRatio=RandomMoney(),
                        payoutRatioWeighting=RandomMoney(),
                        earningsStability=RandomMoney(),
                        earningsStabilityWeighting=RandomMoney(),
                    }
                },
                totalCostInvestment = RandomMoney(),
                totalCostWeighting = RandomMoney(),
                totalMarketValue = RandomMoney(),
                totalMarketValueWeighting = RandomMoney(),
                totalpl = RandomMoney(),
            };
            //throw new NotImplementedException();
            //return dbService.ManagedInvestment_GetCompanyProfiles_Advisor(adviserUserId, (int)AssetTypes.ManagedInvestments);
        }
        public ManagedInvestmentCompanyProfileModel ManagedInvestment_GetCompanyProfiles_Client(string clientUserId)
        {
            return new ManagedInvestmentCompanyProfileModel
            {
                data = new List<ManagedInvestmentCompanyProfileItem>
                {
                    new ManagedInvestmentCompanyProfileItem{
                        ticker= "0221",
                        company= "company 1",
                        costValue=RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueWeighting=RandomMoney(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueWeighting=RandomMoney(),
                        marketValueAssetAllocationWeighting=RandomMoney(),
                        companySuitabilityToInvestor=RandomMoney(),
                        earningsPerShare=RandomMoney(),
                        earningsPerShareWeighting=RandomMoney(),
                        earningsPerShareGrowth=RandomMoney(),
                        earningsPerShareGrowthWeighting=RandomMoney(),
                        dividend=RandomMoney(),
                        dividendWeighting=RandomMoney(),
                        franking=RandomMoney(),
                        frankingWeighting=RandomMoney(),
                        dividendYield=RandomMoney(),
                        dividendYieldWeighting=RandomMoney(),
                        priceEarningsRatio=RandomMoney(),
                        priceEarningsRatioWeighting=RandomMoney(),
                        returnOnAsset=RandomMoney(),
                        returnOnAssetWeighting=RandomMoney(),
                        returnOnEquity=RandomMoney(),
                        returnOnEquityWeighting=RandomMoney(),
                        oneYearReturn=RandomMoney(),
                        oneYearReturnWeighting=RandomMoney(),
                        threeYearReturn=RandomMoney(),
                        threeYearReturnWeighting=RandomMoney(),
                        fiveYearReturn=RandomMoney(),
                        fiveYearReturnWeighting=RandomMoney(),
                        beta=RandomMoney(),
                        betaWeighting=RandomMoney(),
                        currentRatio=RandomMoney(),
                        currentRatioWeighting=RandomMoney(),
                        quickRatio=RandomMoney(),
                        quickRatioWeighting=RandomMoney(),
                        debtEquityRatio=RandomMoney(),
                        debtEquityRatioWeighting=RandomMoney(),
                        interestCover=RandomMoney(),
                        interestCoverWeighting=RandomMoney(),
                        payoutRatio=RandomMoney(),
                        payoutRatioWeighting=RandomMoney(),
                        earningsStability=RandomMoney(),
                        earningsStabilityWeighting=RandomMoney(),
                    }, new ManagedInvestmentCompanyProfileItem{
                        ticker= "0222",
                        company= "company 2",
                        costValue=RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueWeighting=RandomMoney(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueWeighting=RandomMoney(),
                        marketValueAssetAllocationWeighting=RandomMoney(),
                        companySuitabilityToInvestor=RandomMoney(),
                        earningsPerShare=RandomMoney(),
                        earningsPerShareWeighting=RandomMoney(),
                        earningsPerShareGrowth=RandomMoney(),
                        earningsPerShareGrowthWeighting=RandomMoney(),
                        dividend=RandomMoney(),
                        dividendWeighting=RandomMoney(),
                        franking=RandomMoney(),
                        frankingWeighting=RandomMoney(),
                        dividendYield=RandomMoney(),
                        dividendYieldWeighting=RandomMoney(),
                        priceEarningsRatio=RandomMoney(),
                        priceEarningsRatioWeighting=RandomMoney(),
                        returnOnAsset=RandomMoney(),
                        returnOnAssetWeighting=RandomMoney(),
                        returnOnEquity=RandomMoney(),
                        returnOnEquityWeighting=RandomMoney(),
                        oneYearReturn=RandomMoney(),
                        oneYearReturnWeighting=RandomMoney(),
                        threeYearReturn=RandomMoney(),
                        threeYearReturnWeighting=RandomMoney(),
                        fiveYearReturn=RandomMoney(),
                        fiveYearReturnWeighting=RandomMoney(),
                        beta=RandomMoney(),
                        betaWeighting=RandomMoney(),
                        currentRatio=RandomMoney(),
                        currentRatioWeighting=RandomMoney(),
                        quickRatio=RandomMoney(),
                        quickRatioWeighting=RandomMoney(),
                        debtEquityRatio=RandomMoney(),
                        debtEquityRatioWeighting=RandomMoney(),
                        interestCover=RandomMoney(),
                        interestCoverWeighting=RandomMoney(),
                        payoutRatio=RandomMoney(),
                        payoutRatioWeighting=RandomMoney(),
                        earningsStability=RandomMoney(),
                        earningsStabilityWeighting=RandomMoney(),
                    }, new ManagedInvestmentCompanyProfileItem{
                        ticker = "0223",
                        company= "company 3",
                        costValue=RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueWeighting=RandomMoney(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueWeighting=RandomMoney(),
                        marketValueAssetAllocationWeighting=RandomMoney(),
                        companySuitabilityToInvestor=RandomMoney(),
                        earningsPerShare=RandomMoney(),
                        earningsPerShareWeighting=RandomMoney(),
                        earningsPerShareGrowth=RandomMoney(),
                        earningsPerShareGrowthWeighting=RandomMoney(),
                        dividend=RandomMoney(),
                        dividendWeighting=RandomMoney(),
                        franking=RandomMoney(),
                        frankingWeighting=RandomMoney(),
                        dividendYield=RandomMoney(),
                        dividendYieldWeighting=RandomMoney(),
                        priceEarningsRatio=RandomMoney(),
                        priceEarningsRatioWeighting=RandomMoney(),
                        returnOnAsset=RandomMoney(),
                        returnOnAssetWeighting=RandomMoney(),
                        returnOnEquity=RandomMoney(),
                        returnOnEquityWeighting=RandomMoney(),
                        oneYearReturn=RandomMoney(),
                        oneYearReturnWeighting=RandomMoney(),
                        threeYearReturn=RandomMoney(),
                        threeYearReturnWeighting=RandomMoney(),
                        fiveYearReturn=RandomMoney(),
                        fiveYearReturnWeighting=RandomMoney(),
                        beta=RandomMoney(),
                        betaWeighting=RandomMoney(),
                        currentRatio=RandomMoney(),
                        currentRatioWeighting=RandomMoney(),
                        quickRatio=RandomMoney(),
                        quickRatioWeighting=RandomMoney(),
                        debtEquityRatio=RandomMoney(),
                        debtEquityRatioWeighting=RandomMoney(),
                        interestCover=RandomMoney(),
                        interestCoverWeighting=RandomMoney(),
                        payoutRatio=RandomMoney(),
                        payoutRatioWeighting=RandomMoney(),
                        earningsStability=RandomMoney(),
                        earningsStabilityWeighting=RandomMoney(),
                    }
                },
                totalCostInvestment = RandomMoney(),
                totalCostWeighting = RandomMoney(),
                totalMarketValue = RandomMoney(),
                totalMarketValueWeighting = RandomMoney(),
                totalpl = RandomMoney(),
            };
        }
        public IEnumerable<PortfolioQuickStatsModel> ManagedInvestment_GetQuickStats_Adviser(string adviserUserId)
        {
            return new List<PortfolioQuickStatsModel>
            {
                new PortfolioQuickStatsModel{name="Ave Earnings Per Share", value="$5.66"},
                new PortfolioQuickStatsModel{name="Ave Earnings Per Share Growth", value="8%"},
                new PortfolioQuickStatsModel{name="Ave Dividend", value="$8.20"},
                new PortfolioQuickStatsModel{name="Ave Beta", value="2.88"},
                new PortfolioQuickStatsModel{name="Ave Current Ratio", value="8.88"},
            };
            //throw new NotImplementedException();
            //return dbService.GetPortfolio_QuickStats(adviserUserId, (int)AssetTypes.ManagedInvestments);

        }
        public IEnumerable<PortfolioQuickStatsModel> ManagedInvestment_GetQuickStats_Client(string clientUserId)
        {
            return new List<PortfolioQuickStatsModel>
            {
                new PortfolioQuickStatsModel{name="Ave Earnings Per Share", value="$5.66"},
                new PortfolioQuickStatsModel{name="Ave Earnings Per Share Growth", value="8%"},
                new PortfolioQuickStatsModel{name="Ave Dividend", value="$8.20"},
                new PortfolioQuickStatsModel{name="Ave Beta", value="2.88"},
                new PortfolioQuickStatsModel{name="Ave Current Ratio", value="8.88"},
            };
        }
        public InvestmentPortfolioModel ManagedInvestment_GetAssetAllocation_Adviser(string adviserUserId)
        {
            return new InvestmentPortfolioModel
            {
                data = new List<DataNameAmountPair>
                {
                    new DataNameAmountPair{name="Australian Equity", amount=RandomMoney()},
                    new DataNameAmountPair{name="International Equity", amount=RandomMoney()},
                    new DataNameAmountPair{name="Managed Investments", amount=RandomMoney()},
                    new DataNameAmountPair{name="Direct & Listed Property",amount=RandomMoney()},
                    new DataNameAmountPair{name="Miscellaneous Investments", amount=RandomMoney()},
                    new DataNameAmountPair{name="Fixed Income Investments",amount=RandomMoney()},
                    new DataNameAmountPair{name="Cash & Term Deposit", amount=RandomMoney()},
                },
                total = RandomMoney()
            };

            //throw new NotImplementedException();
            //return dbService.ManagedInvestment_GetAssetAllocation_Adviser(adviserUserId, (int)AssetTypes.ManagedInvestments);
        }
        public PortfolioRatingModel ManagedInvestment_GetPortfolioRating_Adviser(string adviserUserId)
        {
            return new PortfolioRatingModel
            {
                data = new List<PortfolioRatingItemModel>
                {
                    new PortfolioRatingItemModel{name="Market Capitalisation", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Dividend Yield", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Price Earnings Ratio", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Return on Asset", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Return on Equity", score=RandomPercentage(), weighting=RandomPercentage()},
                },
                suitability = RandomPercentage(),
                notSuited = RandomPercentage(),
                risk = RandomPercentage()
            };
            //throw new NotImplementedException();
            //return dbService.ManagedInvestment_GetPortfolioRating_Adviser(adviserUserId, (int)AssetTypes.ManagedInvestments);
        }
        public PortfolioRatingModel ManagedInvestment_GetPortfolioRating_Client(string clientUserId)
        {
            return new PortfolioRatingModel
            {
                data = new List<PortfolioRatingItemModel>
                {
                    new PortfolioRatingItemModel{name="Market Capitalisation", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Dividend Yield", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Price Earnings Ratio", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Return on Asset", score=RandomPercentage(), weighting=RandomPercentage()},
                    new PortfolioRatingItemModel{name="Return on Equity", score=RandomPercentage(), weighting=RandomPercentage()},
                },
                suitability = RandomPercentage(),
                notSuited = RandomPercentage(),
                risk = RandomPercentage()
            };
        }
        public EquityDiversificationModel ManagedInvestment_GetDiversification_Adviser(string adviserUserId)
        {
            return new EquityDiversificationModel
            {
                total = new EquityDiversificationTotalModel
                {
                    actualPercentage = RandomPercentage(),
                    sectorialExposureValue = RandomMoney(),
                    sectorialExposurePercentage = RandomPercentage(),
                    asxSectorialExposurePercentage = RandomPercentage(),
                    asxDifferencesPercentage = RandomPercentage(),
                    profileAllocationPercentage = RandomPercentage(),
                    indicator = "E/Unsuitable",
                    profileDifferencesPercentage = RandomPercentage(),
                    suitabilityIndicator = RandomPercentage()
                },
                data = new List<EquityDiversificationDataItem>
                {
                    new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Consumer Goods"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Consumer Services"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Energy"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Financial Services"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Hardware"
                    }
                }

            };
        }
        public EquityDiversificationModel ManagedInvestment_GetDiversification_Client(string clientUserId)
        {
            return new EquityDiversificationModel
            {
                total = new EquityDiversificationTotalModel
                {
                    actualPercentage = RandomPercentage(),
                    sectorialExposureValue = RandomMoney(),
                    sectorialExposurePercentage = RandomPercentage(),
                    asxSectorialExposurePercentage = RandomPercentage(),
                    asxDifferencesPercentage = RandomPercentage(),
                    profileAllocationPercentage = RandomPercentage(),
                    indicator = "E/Unsuitable",
                    profileDifferencesPercentage = RandomPercentage(),
                    suitabilityIndicator = RandomPercentage()
                },
                data = new List<EquityDiversificationDataItem>
                {
                    new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Consumer Goods"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Consumer Services"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Energy"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Financial Services"
                    },new EquityDiversificationDataItem{ 
                        actualPercentage = RandomPercentage(),
                        sectorialExposureValue = RandomMoney(),
                        sectorialExposurePercentage = RandomPercentage(),
                        asxSectorialExposurePercentage = RandomPercentage(),
                        asxDifferencesPercentage = RandomPercentage(),
                        profileAllocationPercentage = RandomPercentage(),
                        indicator = "E/Unsuitable",
                        profileDifferencesPercentage = RandomPercentage(),
                        suitabilityIndicator = RandomPercentage(),
                        sector="Hardware"
                    }
                }

            };
        }
        public DiversificationGroupModel ManagedInvestment_GetDiversificationGroup_Adviser(string adviserUserId)
        {
            return new DiversificationGroupModel
            {
                total = RandomPercentage(),
                data = new List<DiversificationGroupItem>
                {
                    new DiversificationGroupItem{ 
                     group="Allocation",
                     data=new List<DiversificationGroupData>{ 
                      new DiversificationGroupData{ title="Aggressive Allocation", value=RandomPercentage()},
                      new DiversificationGroupData{ title="Aggressive Allocation", value=RandomPercentage()},
                      new DiversificationGroupData{ title="Aggressive Allocation", value=RandomPercentage()},
                      new DiversificationGroupData{ title="Aggressive Allocation", value=RandomPercentage()},
                     }
                    },new DiversificationGroupItem{ 
                     group="Alternative",
                     data=new List<DiversificationGroupData>{ 
                      new DiversificationGroupData{ title="Hedge Fund", value=RandomPercentage()},
                      new DiversificationGroupData{ title="Other Alternative", value=RandomPercentage()},
                     }
                    },new DiversificationGroupItem{ 
                     group="Equity",
                     data=new List<DiversificationGroupData>{ 
                      new DiversificationGroupData{ title="Australia Equity", value=RandomPercentage()},
                      new DiversificationGroupData{ title="Asia", value=RandomPercentage()},
                      new DiversificationGroupData{ title="Emerging Markets Equity", value=RandomPercentage()},
                      new DiversificationGroupData{ title="Europe Equity", value=RandomPercentage()},
                     }
                    }
                }
            };
        }
        public InvestmentCashflowDetailedModel ManagedInvestment_GetSummaryCashflowDetailed_Adviser(string adviserUserId)
        {
            return new InvestmentCashflowDetailedModel
            {
                dateSchema = new List<string> { "July", "August", "September", "October" },
                total = new List<InvestmentCashflowDetailedValue>
                {
                    new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                },
                data = new List<InvestmentCashflowDetailedItem>
                {
                   new InvestmentCashflowDetailedItem{
                       data=new List<InvestmentCashflowDetailedValue>{
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 1",
                       ticker="20001"
                   },new InvestmentCashflowDetailedItem{
                       data=new List<InvestmentCashflowDetailedValue>{
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 2",
                       ticker="20002"
                   },new InvestmentCashflowDetailedItem{
                       data=new List<InvestmentCashflowDetailedValue>{
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 3",
                       ticker="20003"
                   },new InvestmentCashflowDetailedItem{
                       data=new List<InvestmentCashflowDetailedValue>{
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 4",
                       ticker="20004"
                   },new InvestmentCashflowDetailedItem{
                       data=new List<InvestmentCashflowDetailedValue>{
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 5",
                       ticker="20005"
                   }
                }
            };
        }
        public InvestmentCashflowDetailedModel ManagedInvestment_GetSummaryCashflowDetailed_Client(string clientUserId)
        {
            return new InvestmentCashflowDetailedModel
            {
                dateSchema = new List<string> { "July", "August", "September", "October" },
                total = new List<InvestmentCashflowDetailedValue>
                {
                    new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                },
                data = new List<InvestmentCashflowDetailedItem>
                {
                   new InvestmentCashflowDetailedItem{
                       data=new List<InvestmentCashflowDetailedValue>{
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 1",
                       ticker="20001"
                   },new InvestmentCashflowDetailedItem{
                       data=new List<InvestmentCashflowDetailedValue>{
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 2",
                       ticker="20002"
                   },new InvestmentCashflowDetailedItem{
                       data=new List<InvestmentCashflowDetailedValue>{
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 3",
                       ticker="20003"
                   },new InvestmentCashflowDetailedItem{
                       data=new List<InvestmentCashflowDetailedValue>{
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 4",
                       ticker="20004"
                   },new InvestmentCashflowDetailedItem{
                       data=new List<InvestmentCashflowDetailedValue>{
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new InvestmentCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       name="Company 5",
                       ticker="20005"
                   }
                }
            };
        }
        #endregion
        #region direct property services
        public SummaryGeneralInfo DirectProperty_GetGeneral_Adviser(string adviserUserId)
        {
            return new SummaryGeneralInfo
            {
                cost = RandomMoney(),
                marketValue = RandomMoney(),
                pl = RandomMoney(),
                plp = RandomPercentage()
            };
            //throw new NotImplementedException();
            //return dbService.DirectProperty_GetGeneral_Adviser(adviserUserId);
        }
        public SummaryGeneralInfo DirectProperty_GetGeneral_Client(string clientUserId)
        {
            return new SummaryGeneralInfo
            {
                cost = RandomMoney(),
                marketValue = RandomMoney(),
                pl = RandomMoney(),
                plp = RandomPercentage()
            };
        }

        public PortfolioRatingModel DirectProperty_GetPortfolioRating_Adviser(string advsierUserId)
        {
            return new PortfolioRatingModel
            {
                risk = 80,
                suitability = 88,
                notSuited = 20
            };
            //throw new NotImplementedException();
            //return dbService.DirectProperty_GetPortfolioRating_Adviser(advsierUserId, (int)AssetTypes.DirectAndListedProperty);
        }
        public PortfolioRatingModel DirectProperty_GetPortfolioRating_Client(string clientUserId)
        {
            return new PortfolioRatingModel
            {
                risk = 80,
                suitability = 88,
                notSuited = 20
            };
        }
        public DirectPropertyGeoModel DirectProperty_GetGeoInfo_Adviser(string adviserUserId)
        {
            return new DirectPropertyGeoModel
            {
                data = new List<DirectPropertyGeoItem>
                {
                    new DirectPropertyGeoItem{ 
                        id= "1",
                        address= "DieSachbearbeiter Schönhauser Allee 167c",
                        latitude= -26.274985 + (rdm.NextDouble() * 10 - 5),
                        longitude= 134.775464 + (rdm.NextDouble() * 10 - 5),
                        country= "Australia",
                        state= "VIC",
                        type= "Office",
                        value= RandomMoney()
                    },new DirectPropertyGeoItem{ 
                        id= "2",
                        address= "DieSachbearbeiter Schönhauser Allee 167c",
                        latitude= -26.274985 + (rdm.NextDouble() * 10 - 5),
                        longitude= 134.775464 + (rdm.NextDouble() * 10 - 5),
                        country= "Australia",
                        state= "VIC",
                        type= "Office",
                        value= RandomMoney()
                    },new DirectPropertyGeoItem{ 
                        id= "3",
                        address= "DieSachbearbeiter Schönhauser Allee 167c",
                        latitude= -26.274985 + (rdm.NextDouble() * 10 - 5),
                        longitude= 134.775464 + (rdm.NextDouble() * 10 - 5),
                        country= "Australia",
                        state= "VIC",
                        type= "Office",
                        value= RandomMoney()
                    },new DirectPropertyGeoItem{ 
                        id= "4",
                        address= "DieSachbearbeiter Schönhauser Allee 167c",
                        latitude= -26.274985 + (rdm.NextDouble() * 10 - 5),
                        longitude= 134.775464 + (rdm.NextDouble() * 10 - 5),
                        country= "Australia",
                        state= "VIC",
                        type= "Office",
                        value= RandomMoney()
                    },new DirectPropertyGeoItem{ 
                        id= "5",
                        address= "DieSachbearbeiter Schönhauser Allee 167c",
                        latitude= -26.274985 + (rdm.NextDouble() * 10 - 5),
                        longitude= 134.775464 + (rdm.NextDouble() * 10 - 5),
                        country= "Australia",
                        state= "VIC",
                        type= "Office",
                        value= RandomMoney()
                    },new DirectPropertyGeoItem{ 
                        id= "6",
                        address= "DieSachbearbeiter Schönhauser Allee 167c",
                        latitude= -26.274985 + (rdm.NextDouble() * 10 - 5),
                        longitude= 134.775464 + (rdm.NextDouble() * 10 - 5),
                        country= "Australia",
                        state= "VIC",
                        type= "Office",
                        value= RandomMoney()
                    }
                }
            };
            //throw new NotImplementedException();
            //return dbService.DirectProperty_GetGeoInfo_Adviser(adviserUserId);
        }
        public DirectPropertyGeoModel DirectProperty_GetGeoInfo_Client(string clientUserId)
        {

            return new DirectPropertyGeoModel
            {
                data = new List<DirectPropertyGeoItem>
                {
                    new DirectPropertyGeoItem{ 
                        id= "1",
                        address= "DieSachbearbeiter Schönhauser Allee 167c",
                        latitude= -26.274985 + (rdm.NextDouble() * 10 - 5),
                        longitude= 134.775464 + (rdm.NextDouble() * 10 - 5),
                        country= "Australia",
                        state= "VIC",
                        type= "Office",
                        value= RandomMoney()
                    },new DirectPropertyGeoItem{ 
                        id= "2",
                        address= "DieSachbearbeiter Schönhauser Allee 167c",
                        latitude= -26.274985 + (rdm.NextDouble() * 10 - 5),
                        longitude= 134.775464 + (rdm.NextDouble() * 10 - 5),
                        country= "Australia",
                        state= "VIC",
                        type= "Office",
                        value= RandomMoney()
                    },new DirectPropertyGeoItem{ 
                        id= "3",
                        address= "DieSachbearbeiter Schönhauser Allee 167c",
                        latitude= -26.274985 + (rdm.NextDouble() * 10 - 5),
                        longitude= 134.775464 + (rdm.NextDouble() * 10 - 5),
                        country= "Australia",
                        state= "VIC",
                        type= "Office",
                        value= RandomMoney()
                    },new DirectPropertyGeoItem{ 
                        id= "4",
                        address= "DieSachbearbeiter Schönhauser Allee 167c",
                        latitude= -26.274985 + (rdm.NextDouble() * 10 - 5),
                        longitude= 134.775464 + (rdm.NextDouble() * 10 - 5),
                        country= "Australia",
                        state= "VIC",
                        type= "Office",
                        value= RandomMoney()
                    },new DirectPropertyGeoItem{ 
                        id= "5",
                        address= "DieSachbearbeiter Schönhauser Allee 167c",
                        latitude= -26.274985 + (rdm.NextDouble() * 10 - 5),
                        longitude= 134.775464 + (rdm.NextDouble() * 10 - 5),
                        country= "Australia",
                        state= "VIC",
                        type= "Office",
                        value= RandomMoney()
                    },new DirectPropertyGeoItem{ 
                        id= "6",
                        address= "DieSachbearbeiter Schönhauser Allee 167c",
                        latitude= -26.274985 + (rdm.NextDouble() * 10 - 5),
                        longitude= 134.775464 + (rdm.NextDouble() * 10 - 5),
                        country= "Australia",
                        state= "VIC",
                        type= "Office",
                        value= RandomMoney()
                    }
                }
            };
        }
        public IEnumerable<PortfolioQuickStatsModel> DirectProperty_GetQuickStats_Adviser(string adviserUserId)
        {
            return new List<PortfolioQuickStatsModel>
            {
                new PortfolioQuickStatsModel{name="Ave Earnings Per Share", value="$5.66"},
                new PortfolioQuickStatsModel{name="Ave Earnings Per Share Growth", value="8%"},
                new PortfolioQuickStatsModel{name="Ave Dividend", value="$8.20"},
                new PortfolioQuickStatsModel{name="Ave Beta", value="2.88"},
                new PortfolioQuickStatsModel{name="Ave Current Ratio", value="8.88"},
            };
        }
        public IEnumerable<PortfolioQuickStatsModel> DirectProperty_GetQuickStats_Client(string clientUserId)
        {
            return new List<PortfolioQuickStatsModel>
            {
                new PortfolioQuickStatsModel{name="Ave Earnings Per Share", value="$5.66"},
                new PortfolioQuickStatsModel{name="Ave Earnings Per Share Growth", value="8%"},
                new PortfolioQuickStatsModel{name="Ave Dividend", value="$8.20"},
                new PortfolioQuickStatsModel{name="Ave Beta", value="2.88"},
                new PortfolioQuickStatsModel{name="Ave Current Ratio", value="8.88"},
            };
        }
        public CashflowBriefModel DirectProperty_GetSummaryCashflowDetailed_Adviser(string adviserUserId)
        {
            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
            //throw new NotImplementedException();
            //return dbService.DirectProperty_GetCashflowSummary_Advisor(adviserUserId);
        }
        public DirectPropertyCashflowDetailedModel DirectProperty_GetSummaryCashflowDetailed_Client(string clientUserId)
        {
            return new DirectPropertyCashflowDetailedModel
            {
                dateSchema = new List<string> { "July", "August", "September", "October" },
                total = new List<DirectPropertyCashflowDetailedValue>
                {
                    new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                    new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                },
                data = new List<DirectPropertyCashflowDetailedItem>
                {
                   new DirectPropertyCashflowDetailedItem{
                       data=new List<DirectPropertyCashflowDetailedValue>{
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       address="Address 1",
                        propertyName="20001"
                   },new DirectPropertyCashflowDetailedItem{
                       data=new List<DirectPropertyCashflowDetailedValue>{
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       address="Address 2",
                       propertyName="20002"
                   },new DirectPropertyCashflowDetailedItem{
                       data=new List<DirectPropertyCashflowDetailedValue>{
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       address="Company 3",
                       propertyName="20003"
                   },new DirectPropertyCashflowDetailedItem{
                       data=new List<DirectPropertyCashflowDetailedValue>{
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       address="Company 4",
                       propertyName="20004"
                   },new DirectPropertyCashflowDetailedItem{
                       data=new List<DirectPropertyCashflowDetailedValue>{
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                        new DirectPropertyCashflowDetailedValue{income=RandomMoney(),expenses=RandomMoney(), franking=RandomMoney()},
                       },
                       address="Company 5",
                       propertyName="20005"
                   }
                }
            };
        }
        #endregion
        #region fixed income services
        public SummaryGeneralInfoFCL FixedIncome_GetGeneral_Adviser(string adviserUserId)
        {
            return new SummaryGeneralInfoFCL
            {
                cost = RandomMoney(),
                marketValue = RandomMoney(),
                pl = RandomMoney(),
                plp = RandomPercentage(),
                faceValue = RandomMoney(),
                aveCoupon = RandomPercentage()
            };
            //throw new NotImplementedException();
            //return dbService.FixedIncomeGetGeneral(adviserUserId, (int)AssetTypes.FixedIncomeInvestments);
        }
        public SummaryGeneralInfoFCL FixedIncome_GetGeneral_Client(string clientUserId)
        {
            return new SummaryGeneralInfoFCL
            {
                cost = RandomMoney(),
                marketValue = RandomMoney(),
                pl = RandomMoney(),
                plp = RandomPercentage(),
                faceValue = RandomMoney(),
                aveCoupon = RandomPercentage()
            };
        }
        public PortfolioRatingModel FixedIncome_GetPortfolioRating_Adviser(string advsierUserId)
        {
            return new PortfolioRatingModel
            {
                risk = 80,
                suitability = 88,
                notSuited = 20
            };
        }
        public PortfolioRatingModel FixedIncome_GetPortfolioRating_Client(string clientUserId)
        {
            return new PortfolioRatingModel
            {
                risk = 80,
                suitability = 88,
                notSuited = 20
            };
        }
        public CashPriceChartModel FixedIncome_GetPriceData_Adviser(string adviserUserId)
        {
            CashPriceChartModel result = new CashPriceChartModel();
            result.data = new List<CashPriceChartItem>();
            var date = DateTime.Now;
            for (int i = 0; i < 12; i++)
            {
                var dt = date.AddMonths(i);
                result.data.Add(new CashPriceChartItem
                {
                    bondYield = RandomPercentage(),
                    date = dt,
                    month = dt.ToString("MMM"),
                    rba = RandomPercentage()
                });
            }
            return result;
        }
        public CashPriceChartModel FixedIncome_GetPriceData_Client(string clientUserId)
        {
            CashPriceChartModel result = new CashPriceChartModel();
            result.data = new List<CashPriceChartItem>();
            var date = DateTime.Now;
            for (int i = 0; i < 12; i++)
            {
                var dt = date.AddMonths(i);
                result.data.Add(new CashPriceChartItem
                {
                    bondYield = RandomPercentage(),
                    date = dt,
                    month = dt.ToString("MMM"),
                    rba = RandomPercentage()
                });
            }
            return result;
        }
        public CashflowBriefModel FixedIncome_GetCashflowSummary_Adviser(string adviserUserId)
        {
            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
            //throw new NotImplementedException();
            //return dbService.FixedIncome_GetCashflowSummary_Advisor(adviserUserId, (int)AssetTypes.FixedIncomeInvestments);
        }
        public CashflowBriefModel FixedIncome_GetCashflowSummary_Client(string clientUserId)
        {
            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
        }
        public FixedIncomeProfileModel FixedIncome_GetProfiles_Adviser(string adviserUserId)
        {
            FixedIncomeProfileModel result = new FixedIncomeProfileModel
            {
                data = new List<FixedIncomeProfileItem>
                {
                    new FixedIncomeProfileItem{ 
                        ticker= "123444678",
                        fixedIncomeName= "income 1",
                        currency= "USD",
                        faceValue= RandomMoney(),
                        coupon=RandomPercentage(),
                        couponFrequency= "weekly",
                        issuer="issuer 2",
                        costValue= RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueAUD=RandomMoney(),
                        totalCostValueWeighting=RandomPercentage(),
                        totalCostValueAssetAllocationWeighting=RandomPercentage(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueAUD=RandomMoney(),
                        marketValueWeighting=RandomPercentage(),
                        marketValueAssetAllocationWeighting=RandomPercentage(),
                        incomeSuitabilityToInvestor=RandomPercentage(),
                        instrumentType="type 2",
                        bondTerm=rdm.Next() * 12,
                        maturityDate=DateTime.Now,
                        presentValue=RandomMoney(),
                        yieldToMaturity=RandomMoney(),
                        couponRate=RandomPercentage(),
                        interestAccrued=RandomMoney(),
                        bondRating=RandomPercentage(),
                        ratingAgency="Agency",
                        priority= 1,
                        redemptionFeatures= "features"
                    },new FixedIncomeProfileItem{ 
                        ticker= "123444678",
                        fixedIncomeName= "income 1",
                        currency= "USD",
                        faceValue= RandomMoney(),
                        coupon=RandomPercentage(),
                        couponFrequency= "weekly",
                        issuer="issuer 2",
                        costValue= RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueAUD=RandomMoney(),
                        totalCostValueWeighting=RandomPercentage(),
                        totalCostValueAssetAllocationWeighting=RandomPercentage(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueAUD=RandomMoney(),
                        marketValueWeighting=RandomPercentage(),
                        marketValueAssetAllocationWeighting=RandomPercentage(),
                        incomeSuitabilityToInvestor=RandomPercentage(),
                        instrumentType="type 2",
                        bondTerm=rdm.Next() * 12,
                        maturityDate=DateTime.Now,
                        presentValue=RandomMoney(),
                        yieldToMaturity=RandomMoney(),
                        couponRate=RandomPercentage(),
                        interestAccrued=RandomMoney(),
                        bondRating=RandomPercentage(),
                        ratingAgency="Agency",
                        priority= 1,
                        redemptionFeatures= "features"
                    },new FixedIncomeProfileItem{ 
                        ticker= "123444678",
                        fixedIncomeName= "income 1",
                        currency= "USD",
                        faceValue= RandomMoney(),
                        coupon=RandomPercentage(),
                        couponFrequency= "weekly",
                        issuer="issuer 2",
                        costValue= RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueAUD=RandomMoney(),
                        totalCostValueWeighting=RandomPercentage(),
                        totalCostValueAssetAllocationWeighting=RandomPercentage(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueAUD=RandomMoney(),
                        marketValueWeighting=RandomPercentage(),
                        marketValueAssetAllocationWeighting=RandomPercentage(),
                        incomeSuitabilityToInvestor=RandomPercentage(),
                        instrumentType="type 2",
                        bondTerm=rdm.Next() * 12,
                        maturityDate=DateTime.Now,
                        presentValue=RandomMoney(),
                        yieldToMaturity=RandomMoney(),
                        couponRate=RandomPercentage(),
                        interestAccrued=RandomMoney(),
                        bondRating=RandomPercentage(),
                        ratingAgency="Agency",
                        priority= 1,
                        redemptionFeatures= "features"
                    },
                }
            };
            return result;
        }
        public FixedIncomeProfileModel FixedIncome_GetProfiles_Client(string clientUserId)
        {
            FixedIncomeProfileModel result = new FixedIncomeProfileModel
            {
                data = new List<FixedIncomeProfileItem>
                {
                    new FixedIncomeProfileItem{ 
                        ticker= "123444678",
                        fixedIncomeName= "income 1",
                        currency= "USD",
                        faceValue= RandomMoney(),
                        coupon=RandomPercentage(),
                        couponFrequency= "weekly",
                        issuer="issuer 2",
                        costValue= RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueAUD=RandomMoney(),
                        totalCostValueWeighting=RandomPercentage(),
                        totalCostValueAssetAllocationWeighting=RandomPercentage(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueAUD=RandomMoney(),
                        marketValueWeighting=RandomPercentage(),
                        marketValueAssetAllocationWeighting=RandomPercentage(),
                        incomeSuitabilityToInvestor=RandomPercentage(),
                        instrumentType="type 2",
                        bondTerm=rdm.Next() * 12,
                        maturityDate=DateTime.Now,
                        presentValue=RandomMoney(),
                        yieldToMaturity=RandomMoney(),
                        couponRate=RandomPercentage(),
                        interestAccrued=RandomMoney(),
                        bondRating=RandomPercentage(),
                        ratingAgency="Agency",
                        priority= 1,
                        redemptionFeatures= "features"
                    },new FixedIncomeProfileItem{ 
                        ticker= "123444678",
                        fixedIncomeName= "income 1",
                        currency= "USD",
                        faceValue= RandomMoney(),
                        coupon=RandomPercentage(),
                        couponFrequency= "weekly",
                        issuer="issuer 2",
                        costValue= RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueAUD=RandomMoney(),
                        totalCostValueWeighting=RandomPercentage(),
                        totalCostValueAssetAllocationWeighting=RandomPercentage(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueAUD=RandomMoney(),
                        marketValueWeighting=RandomPercentage(),
                        marketValueAssetAllocationWeighting=RandomPercentage(),
                        incomeSuitabilityToInvestor=RandomPercentage(),
                        instrumentType="type 2",
                        bondTerm=rdm.Next() * 12,
                        maturityDate=DateTime.Now,
                        presentValue=RandomMoney(),
                        yieldToMaturity=RandomMoney(),
                        couponRate=RandomPercentage(),
                        interestAccrued=RandomMoney(),
                        bondRating=RandomPercentage(),
                        ratingAgency="Agency",
                        priority= 1,
                        redemptionFeatures= "features"
                    },new FixedIncomeProfileItem{ 
                        ticker= "123444678",
                        fixedIncomeName= "income 1",
                        currency= "USD",
                        faceValue= RandomMoney(),
                        coupon=RandomPercentage(),
                        couponFrequency= "weekly",
                        issuer="issuer 2",
                        costValue= RandomMoney(),
                        brokerage=RandomMoney(),
                        totalCostValue=RandomMoney(),
                        totalCostValueAUD=RandomMoney(),
                        totalCostValueWeighting=RandomPercentage(),
                        totalCostValueAssetAllocationWeighting=RandomPercentage(),
                        marketPrice=RandomMoney(),
                        marketValue=RandomMoney(),
                        marketValueAUD=RandomMoney(),
                        marketValueWeighting=RandomPercentage(),
                        marketValueAssetAllocationWeighting=RandomPercentage(),
                        incomeSuitabilityToInvestor=RandomPercentage(),
                        instrumentType="type 2",
                        bondTerm=rdm.Next() * 12,
                        maturityDate=DateTime.Now,
                        presentValue=RandomMoney(),
                        yieldToMaturity=RandomMoney(),
                        couponRate=RandomPercentage(),
                        interestAccrued=RandomMoney(),
                        bondRating=RandomPercentage(),
                        ratingAgency="Agency",
                        priority= 1,
                        redemptionFeatures= "features"
                    },
                }
            };
            return result;
        }
        public IEnumerable<IncomeStatisticsModel> FixedIncome_GetStats_Adviser(string adviserUserId)
        {
            return new List<IncomeStatisticsModel>
            {
                new IncomeStatisticsModel{type="Australian Government Bond", value=RandomMoney(), percentage=RandomPercentage()},
                new IncomeStatisticsModel{type="Australian State Government Bond", value=RandomMoney(), percentage=RandomPercentage()},
                new IncomeStatisticsModel{type="Australian Municipal Bond", value=RandomMoney(), percentage=RandomPercentage()},
                new IncomeStatisticsModel{type="Australian Corporate Bond", value=RandomMoney(), percentage=RandomPercentage()},
                new IncomeStatisticsModel{type="Australian Infrastructure Bond", value=RandomMoney(), percentage=RandomPercentage()},
            };
        }
        public IEnumerable<IncomeStatisticsModel> FixedIncome_GetStats_Client(string clientUserId)
        {
            return new List<IncomeStatisticsModel>
            {
                new IncomeStatisticsModel{type="Australian Government Bond", value=RandomMoney(), percentage=RandomPercentage()},
                new IncomeStatisticsModel{type="Australian State Government Bond", value=RandomMoney(), percentage=RandomPercentage()},
                new IncomeStatisticsModel{type="Australian Municipal Bond", value=RandomMoney(), percentage=RandomPercentage()},
                new IncomeStatisticsModel{type="Australian Corporate Bond", value=RandomMoney(), percentage=RandomPercentage()},
                new IncomeStatisticsModel{type="Australian Infrastructure Bond", value=RandomMoney(), percentage=RandomPercentage()},
            };
        }
        public IncomeDiversificationModel FixedIncome_GetDiversification_Adviser(string adviserUserId)
        {
            return new IncomeDiversificationModel
            {
                total = new IncomeDiversificationItem { name = "", value = RandomMoney(), percentage = RandomPercentage() },
                data = new List<IncomeDiversificationItem>
                {
                    new IncomeDiversificationItem{name="Government Bonds",value=RandomMoney(), percentage=RandomPercentage()},
                    new IncomeDiversificationItem{name="Semi - Government Bonds",value=RandomMoney(), percentage=RandomPercentage()},
                    new IncomeDiversificationItem{name="Corporate Bonds",value=RandomMoney(), percentage=RandomPercentage()},
                    new IncomeDiversificationItem{name="Debentures",value=RandomMoney(), percentage=RandomPercentage()}
                }
            };
        }
        public IncomeDiversificationModel FixedIncome_GetDiversification_Client(string clientUserId)
        {
            return new IncomeDiversificationModel
            {
                total = new IncomeDiversificationItem { name = "", value = RandomMoney(), percentage = RandomPercentage() },
                data = new List<IncomeDiversificationItem>
                {
                    new IncomeDiversificationItem{name="Government Bonds",value=RandomMoney(), percentage=RandomPercentage()},
                    new IncomeDiversificationItem{name="Semi - Government Bonds",value=RandomMoney(), percentage=RandomPercentage()},
                    new IncomeDiversificationItem{name="Corporate Bonds",value=RandomMoney(), percentage=RandomPercentage()},
                    new IncomeDiversificationItem{name="Debentures",value=RandomMoney(), percentage=RandomPercentage()}
                }
            };
        }
        public FixedIncomeCashflowDetailedModel FixedIncome_GetCashflowDetails_Adviser(string adviserUserId)
        {
            return new FixedIncomeCashflowDetailedModel
            {
                dateSchema = new List<string> { "July", "August", "September", "October" },
                total = new List<FixedIncomeCashflowDetailedItem>
                {
                    new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                    new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                    new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                    new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                },
                data = new List<FixedIncomeCashflowDetailedGroup>
                {
                    new FixedIncomeCashflowDetailedGroup{ 
                     currency="USD",
                     name="Income 1",
                     ticker="20001",
                     data=new List<FixedIncomeCashflowDetailedItem>
                        {
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    },new FixedIncomeCashflowDetailedGroup{ 
                     currency="USD",
                     name="Income 1",
                     ticker="20001",
                     data=new List<FixedIncomeCashflowDetailedItem>
                        {
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    },new FixedIncomeCashflowDetailedGroup{ 
                     currency="USD",
                     name="Income 1",
                     ticker="20001",
                     data=new List<FixedIncomeCashflowDetailedItem>
                        {
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    },new FixedIncomeCashflowDetailedGroup{ 
                     currency="USD",
                     name="Income 1",
                     ticker="20001",
                     data=new List<FixedIncomeCashflowDetailedItem>
                        {
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    }
                }
            };
        }
        public FixedIncomeCashflowDetailedModel FixedIncome_GetCashflowDetails_Client(string clientUserId)
        {
            return new FixedIncomeCashflowDetailedModel
            {
                dateSchema = new List<string> { "July", "August", "September", "October" },
                total = new List<FixedIncomeCashflowDetailedItem>
                {
                    new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                    new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                    new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                    new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                },
                data = new List<FixedIncomeCashflowDetailedGroup>
                {
                    new FixedIncomeCashflowDetailedGroup{ 
                     currency="USD",
                     name="Income 1",
                     ticker="20001",
                     data=new List<FixedIncomeCashflowDetailedItem>
                        {
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    },new FixedIncomeCashflowDetailedGroup{ 
                     currency="USD",
                     name="Income 1",
                     ticker="20001",
                     data=new List<FixedIncomeCashflowDetailedItem>
                        {
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    },new FixedIncomeCashflowDetailedGroup{ 
                     currency="USD",
                     name="Income 1",
                     ticker="20001",
                     data=new List<FixedIncomeCashflowDetailedItem>
                        {
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    },new FixedIncomeCashflowDetailedGroup{ 
                     currency="USD",
                     name="Income 1",
                     ticker="20001",
                     data=new List<FixedIncomeCashflowDetailedItem>
                        {
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new FixedIncomeCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    }
                }
            };
        }
        #endregion
        #region cash term deposit services
        public CashTermDepositGeneralInfoModel TermDeposit_GetGeneral_Adviser(string adviserUserId)
        {
            return new CashTermDepositGeneralInfoModel
            {
                annualInterest = RandomPercentage(),
                annualYield = RandomPercentage(),
                cashAtMaturity = RandomMoney(),
                cashDeposit = RandomMoney(),
                consumerPriceIndex = RandomPercentage(),
                rbaRate = RandomPercentage()
            };
        }
        public CashTermDepositGeneralInfoModel TermDeposit_GetGeneral_Client(string clientUserId)
        {
            return new CashTermDepositGeneralInfoModel
            {
                annualInterest = RandomPercentage(),
                annualYield = RandomPercentage(),
                cashAtMaturity = RandomMoney(),
                cashDeposit = RandomMoney(),
                consumerPriceIndex = RandomPercentage(),
                rbaRate = RandomPercentage()
            };
        }
        public PortfolioRatingModel TermDeposit_GetPortfolioRating_Adviser(string advsierUserId)
        {
            return new PortfolioRatingModel
            {
                risk = 80,
                suitability = 88,
                notSuited = 20
            };
        }
        public PortfolioRatingModel TermDeposit_GetPortfolioRating_Client(string clientUserId)
        {
            return new PortfolioRatingModel
            {
                risk = 80,
                suitability = 88,
                notSuited = 20
            };
        }
        public TermDepositPriceChartModel TermDeposit_GetPriceData_Adviser(string adviserUserId)
        {
            TermDepositPriceChartModel result = new TermDepositPriceChartModel();
            result.data = new List<TermDepositPriceChartItem>();
            var date = DateTime.Now;
            for (int i = 0; i < 12; i++)
            {
                var dt = date.AddMonths(i);
                result.data.Add(new TermDepositPriceChartItem
                {
                    cashYield = RandomPercentage(),
                    date = dt,
                    month = dt.ToString("MMM"),
                    rba = RandomPercentage()
                });
            }
            return result;
        }
        public TermDepositPriceChartModel TermDeposit_GetPriceData_Client(string clientUserId)
        {
            TermDepositPriceChartModel result = new TermDepositPriceChartModel();
            result.data = new List<TermDepositPriceChartItem>();
            var date = DateTime.Now;
            for (int i = 0; i < 12; i++)
            {
                var dt = date.AddMonths(i);
                result.data.Add(new TermDepositPriceChartItem
                {
                    cashYield = RandomPercentage(),
                    date = dt,
                    month = dt.ToString("MMM"),
                    rba = RandomPercentage()
                });
            }
            return result;
        }
        public CashflowBriefModel TermDeposit_GetCashflowSummary_Adviser(string adviserUserId)
        {
            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
        }
        public CashflowBriefModel TermDeposit_GetCashflowSummary_Client(string clientUserId)
        {
            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
        }
        public CashTermDepositProfileModel TermDeposit_GetProfiles_Adviser(string adviserUserId)
        {
            return new CashTermDepositProfileModel
            {
                data = new List<CashTermDepositProfileItem>
                {
                    new CashTermDepositProfileItem{ 
                        accountType= "type 1",
                        institution= "institution 1",
                        currency= "USD",
                        accountName= "Name",
                        daysToMaturity= rdm.Next() * 100,
                        faceValue=RandomMoney(),
                        interestFrequency= "6 Months",
                        accruedInterest=RandomMoney(),
                        bsb= "123456",
                        accountNumber= "12345678",
                        depositDate=DateTime.Now,
                        depositAmount=RandomMoney(),
                        maturityDate=DateTime.Now,
                        maturityValue=RandomMoney(),
                        termOfRates= "term",
                        interestRate=RandomPercentage(),
                        annualInterest=RandomMoney(),
                        suitabilityToInvestorProfile= RandomMoney()                 
                    },new CashTermDepositProfileItem{ 
                        accountType= "type 1",
                        institution= "institution 1",
                        currency= "USD",
                        accountName= "Name",
                        daysToMaturity= rdm.Next() * 100,
                        faceValue=RandomMoney(),
                        interestFrequency= "6 Months",
                        accruedInterest=RandomMoney(),
                        bsb= "123456",
                        accountNumber= "12345678",
                        depositDate=DateTime.Now,
                        depositAmount=RandomMoney(),
                        maturityDate=DateTime.Now,
                        maturityValue=RandomMoney(),
                        termOfRates= "term",
                        interestRate=RandomPercentage(),
                        annualInterest=RandomMoney(),
                        suitabilityToInvestorProfile= RandomMoney()                 
                    },new CashTermDepositProfileItem{ 
                        accountType= "type 1",
                        institution= "institution 1",
                        currency= "USD",
                        accountName= "Name",
                        daysToMaturity= rdm.Next() * 100,
                        faceValue=RandomMoney(),
                        interestFrequency= "6 Months",
                        accruedInterest=RandomMoney(),
                        bsb= "123456",
                        accountNumber= "12345678",
                        depositDate=DateTime.Now,
                        depositAmount=RandomMoney(),
                        maturityDate=DateTime.Now,
                        maturityValue=RandomMoney(),
                        termOfRates= "term",
                        interestRate=RandomPercentage(),
                        annualInterest=RandomMoney(),
                        suitabilityToInvestorProfile= RandomMoney()                 
                    },new CashTermDepositProfileItem{ 
                        accountType= "type 1",
                        institution= "institution 1",
                        currency= "USD",
                        accountName= "Name",
                        daysToMaturity= rdm.Next() * 100,
                        faceValue=RandomMoney(),
                        interestFrequency= "6 Months",
                        accruedInterest=RandomMoney(),
                        bsb= "123456",
                        accountNumber= "12345678",
                        depositDate=DateTime.Now,
                        depositAmount=RandomMoney(),
                        maturityDate=DateTime.Now,
                        maturityValue=RandomMoney(),
                        termOfRates= "term",
                        interestRate=RandomPercentage(),
                        annualInterest=RandomMoney(),
                        suitabilityToInvestorProfile= RandomMoney()                 
                    }

                }
            };
        }
        public CashTermDepositProfileModel TermDeposit_GetProfiles_Client(string clientUserId)
        {
            return new CashTermDepositProfileModel
            {
                data = new List<CashTermDepositProfileItem>
                {
                    new CashTermDepositProfileItem{ 
                        accountType= "type 1",
                        institution= "institution 1",
                        currency= "USD",
                        accountName= "Name",
                        daysToMaturity= rdm.Next() * 100,
                        faceValue=RandomMoney(),
                        interestFrequency= "6 Months",
                        accruedInterest=RandomMoney(),
                        bsb= "123456",
                        accountNumber= "12345678",
                        depositDate=DateTime.Now,
                        depositAmount=RandomMoney(),
                        maturityDate=DateTime.Now,
                        maturityValue=RandomMoney(),
                        termOfRates= "term",
                        interestRate=RandomPercentage(),
                        annualInterest=RandomMoney(),
                        suitabilityToInvestorProfile= RandomMoney()                 
                    },new CashTermDepositProfileItem{ 
                        accountType= "type 1",
                        institution= "institution 1",
                        currency= "USD",
                        accountName= "Name",
                        daysToMaturity= rdm.Next() * 100,
                        faceValue=RandomMoney(),
                        interestFrequency= "6 Months",
                        accruedInterest=RandomMoney(),
                        bsb= "123456",
                        accountNumber= "12345678",
                        depositDate=DateTime.Now,
                        depositAmount=RandomMoney(),
                        maturityDate=DateTime.Now,
                        maturityValue=RandomMoney(),
                        termOfRates= "term",
                        interestRate=RandomPercentage(),
                        annualInterest=RandomMoney(),
                        suitabilityToInvestorProfile= RandomMoney()                 
                    },new CashTermDepositProfileItem{ 
                        accountType= "type 1",
                        institution= "institution 1",
                        currency= "USD",
                        accountName= "Name",
                        daysToMaturity= rdm.Next() * 100,
                        faceValue=RandomMoney(),
                        interestFrequency= "6 Months",
                        accruedInterest=RandomMoney(),
                        bsb= "123456",
                        accountNumber= "12345678",
                        depositDate=DateTime.Now,
                        depositAmount=RandomMoney(),
                        maturityDate=DateTime.Now,
                        maturityValue=RandomMoney(),
                        termOfRates= "term",
                        interestRate=RandomPercentage(),
                        annualInterest=RandomMoney(),
                        suitabilityToInvestorProfile= RandomMoney()                 
                    },new CashTermDepositProfileItem{ 
                        accountType= "type 1",
                        institution= "institution 1",
                        currency= "USD",
                        accountName= "Name",
                        daysToMaturity= rdm.Next() * 100,
                        faceValue=RandomMoney(),
                        interestFrequency= "6 Months",
                        accruedInterest=RandomMoney(),
                        bsb= "123456",
                        accountNumber= "12345678",
                        depositDate=DateTime.Now,
                        depositAmount=RandomMoney(),
                        maturityDate=DateTime.Now,
                        maturityValue=RandomMoney(),
                        termOfRates= "term",
                        interestRate=RandomPercentage(),
                        annualInterest=RandomMoney(),
                        suitabilityToInvestorProfile= RandomMoney()                 
                    }

                }
            };
        }
        public IEnumerable<IncomeStatisticsModel> TermDeposit_GetStats_Adviser(string adviserUserId)
        {
            return new List<IncomeStatisticsModel>
            {
                new IncomeStatisticsModel{type="Australian Government Bond", value=RandomMoney(), percentage=RandomPercentage()},
                new IncomeStatisticsModel{type="Australian State Government Bond", value=RandomMoney(), percentage=RandomPercentage()},
                new IncomeStatisticsModel{type="Australian Municipal Bond", value=RandomMoney(), percentage=RandomPercentage()},
                new IncomeStatisticsModel{type="Australian Corporate Bond", value=RandomMoney(), percentage=RandomPercentage()},
                new IncomeStatisticsModel{type="Australian Infrastructure Bond", value=RandomMoney(), percentage=RandomPercentage()},
            };
        }
        public IEnumerable<IncomeStatisticsModel> TermDeposit_GetStats_Client(string clientUserId)
        {
            return new List<IncomeStatisticsModel>
            {
                new IncomeStatisticsModel{type="Australian Government Bond", value=RandomMoney(), percentage=RandomPercentage()},
                new IncomeStatisticsModel{type="Australian State Government Bond", value=RandomMoney(), percentage=RandomPercentage()},
                new IncomeStatisticsModel{type="Australian Municipal Bond", value=RandomMoney(), percentage=RandomPercentage()},
                new IncomeStatisticsModel{type="Australian Corporate Bond", value=RandomMoney(), percentage=RandomPercentage()},
                new IncomeStatisticsModel{type="Australian Infrastructure Bond", value=RandomMoney(), percentage=RandomPercentage()},
            };
        }
        public IncomeDiversificationModel TermDeposit_GetDiversification_Adviser(string adviserUserId)
        {
            return new IncomeDiversificationModel
            {
                total = new IncomeDiversificationItem { name = "", value = RandomMoney(), percentage = RandomPercentage() },
                data = new List<IncomeDiversificationItem>
                {
                    new IncomeDiversificationItem{name="Government Bonds",value=RandomMoney(), percentage=RandomPercentage()},
                    new IncomeDiversificationItem{name="Semi - Government Bonds",value=RandomMoney(), percentage=RandomPercentage()},
                    new IncomeDiversificationItem{name="Corporate Bonds",value=RandomMoney(), percentage=RandomPercentage()},
                    new IncomeDiversificationItem{name="Debentures",value=RandomMoney(), percentage=RandomPercentage()}
                }
            };
        }
        public IncomeDiversificationModel TermDeposit_GetDiversification_Client(string clientUserId)
        {
            return new IncomeDiversificationModel
            {
                total = new IncomeDiversificationItem { name = "", value = RandomMoney(), percentage = RandomPercentage() },
                data = new List<IncomeDiversificationItem>
                {
                    new IncomeDiversificationItem{name="Government Bonds",value=RandomMoney(), percentage=RandomPercentage()},
                    new IncomeDiversificationItem{name="Semi - Government Bonds",value=RandomMoney(), percentage=RandomPercentage()},
                    new IncomeDiversificationItem{name="Corporate Bonds",value=RandomMoney(), percentage=RandomPercentage()},
                    new IncomeDiversificationItem{name="Debentures",value=RandomMoney(), percentage=RandomPercentage()}
                }
            };
        }
        public CashTermDepositCashflowDetailedModel TermDeposit_GetCashflowDetails_Adviser(string adviserUserId)
        {
            return new CashTermDepositCashflowDetailedModel
            {
                dateSchema = new List<string> { "July", "August", "September", "October" },
                total = new List<CashTermDepositCashflowDetailedItem>
                {
                    new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                    new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                    new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                    new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                },
                data = new List<CashTermDepositCashflowDetailedGroup>
                {
                    new CashTermDepositCashflowDetailedGroup{ 
                     currency="USD",
                     name="Income 1",
                     type="20001",
                     data=new List<CashTermDepositCashflowDetailedItem>
                        {
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    },new CashTermDepositCashflowDetailedGroup{ 
                     currency="USD",
                     name="Income 1",
                     type="20001",
                     data=new List<CashTermDepositCashflowDetailedItem>
                        {
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    },new CashTermDepositCashflowDetailedGroup{ 
                     currency="USD",
                     name="Income 1",
                     type="20001",
                     data=new List<CashTermDepositCashflowDetailedItem>
                        {
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    },new CashTermDepositCashflowDetailedGroup{ 
                     currency="USD",
                     name="Income 1",
                     type="20001",
                     data=new List<CashTermDepositCashflowDetailedItem>
                        {
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    }
                }
            };
        }
        public CashTermDepositCashflowDetailedModel TermDeposit_GetCashflowDetails_Client(string clientUserId)
        {
            return new CashTermDepositCashflowDetailedModel
            {
                dateSchema = new List<string> { "July", "August", "September", "October" },
                total = new List<CashTermDepositCashflowDetailedItem>
                {
                    new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                    new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                    new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                    new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                },
                data = new List<CashTermDepositCashflowDetailedGroup>
                {
                    new CashTermDepositCashflowDetailedGroup{ 
                     currency="USD",
                     name="Income 1",
                     type="20001",
                     data=new List<CashTermDepositCashflowDetailedItem>
                        {
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    },new CashTermDepositCashflowDetailedGroup{ 
                     currency="USD",
                     name="Income 1",
                     type="20001",
                     data=new List<CashTermDepositCashflowDetailedItem>
                        {
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    },new CashTermDepositCashflowDetailedGroup{ 
                     currency="USD",
                     name="Income 1",
                     type="20001",
                     data=new List<CashTermDepositCashflowDetailedItem>
                        {
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    },new CashTermDepositCashflowDetailedGroup{ 
                     currency="USD",
                     name="Income 1",
                     type="20001",
                     data=new List<CashTermDepositCashflowDetailedItem>
                        {
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new CashTermDepositCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    }
                }
            };
        }
        #endregion
        #region mortgage / investment home loans services

        public MortgageInvestmentGeneralInfo Mortgate_GetGeneralInfo_Adviser(string adviserUserId)
        {
            return new MortgageInvestmentGeneralInfo
            {
                annualInterestRepayment = RandomMoney(),
                aveTermOfLoans = RandomPercentage(),
                marketValue = RandomMoney(),
                monthlyRepayment = RandomMoney(),
                outstandingLoans = RandomMoney(),
                propertyGearingRatio = RandomPercentage()
            };
            //throw new NotImplementedException();
            //return dbService.Mortgate_GetGeneralInfo_Adviser(adviserUserId);
        }
        public MortgageInvestmentGeneralInfo Mortgate_GetGeneralInfo_Client(string clientUserId)
        {
            return new MortgageInvestmentGeneralInfo
            {
                annualInterestRepayment = RandomMoney(),
                aveTermOfLoans = RandomPercentage(),
                marketValue = RandomMoney(),
                monthlyRepayment = RandomMoney(),
                outstandingLoans = RandomMoney(),
                propertyGearingRatio = RandomPercentage()
            };
        }
        public CashflowBriefModel Mortgate_GetCashflowSummary_Adviser(string adviserUserId)
        {
            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
            //throw new NotImplementedException();
            //return dbService.Loan_GetCashflowSummary_Advisor(adviserUserId);
        }
        public CashflowBriefModel Mortgate_GetCashflowSummary_Client(string clientUserId)
        {
            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
        }
        public CashflowBriefModel Mortgate_GetCashflowDetails_Adviser(string adviserUserId)
        {
            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
            //throw new NotImplementedException();
            //return dbService.Loan_GetCashflowSummary_Advisor(adviserUserId);
        }
        public MortgageCashflowDetailedModel Mortgate_GetCashflowDetails_Client(string adviserUserId)
        {
            return new MortgageCashflowDetailedModel
            {
                dateSchema = new List<string> { "July", "August", "September", "October" },
                total = new List<MortgageCashflowDetailedItem>
                {
                    new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                    new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                    new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                    new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                },
                data = new List<MortgageCashflowDetailedGroup>
                {
                    new MortgageCashflowDetailedGroup{ 
                     propertyName="property 1",
                     address="add",
                     data=new List<MortgageCashflowDetailedItem>
                        {
                            new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    },new MortgageCashflowDetailedGroup{ 
                     propertyName="property 2",
                     address="add",
                     data=new List<MortgageCashflowDetailedItem>
                        {
                            new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    },new MortgageCashflowDetailedGroup{ 
                     propertyName="property 3",
                     address="add",
                     data=new List<MortgageCashflowDetailedItem>
                        {
                            new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    },new MortgageCashflowDetailedGroup{ 
                     propertyName="property 4",
                     address="add",
                     data=new List<MortgageCashflowDetailedItem>
                        {
                            new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                            new MortgageCashflowDetailedItem{income=RandomMoney(),expenses=RandomMoney(),franking=RandomMoney()},
                        },
                    }
                }
            };
        }
        public MortgageInvestmentStatModel Mortgage_GetStats_Adviser(string adviserUserId)
        {
            //return new MortgageInvestmentStatModel
            //{
            //    debtLevel = rdm.Next() * 10,
            //    cashfFlowLevel = RandomMoney(),
            //    incomeLevel = RandomMoney(),
            //    marketValue = RandomMoney(),
            //    repayments = RandomMoney()
            //};
            return null;
        }
        public MortgageInvestmentStatModel Mortgage_GetStats_Client(string clientUserId)
        {
            //return new MortgageInvestmentStatModel
            //{
            //    debtLevel = rdm.Next() * 10,
            //    cashfFlowLevel = RandomMoney(),
            //    incomeLevel = RandomMoney(),
            //    marketValue = RandomMoney(),
            //    repayments = RandomMoney()
            //};
            return null;
        }
        public PortfolioRatingModel Mortgage_GetPortfolioRating_Adviser(string advsierUserId)
        {
            return new PortfolioRatingModel
            {
                risk = 80,
                suitability = 88,
                notSuited = 20
            };
        }
        public PortfolioRatingModel Mortgage_GetPortfolioRating_Client(string clientUserId)
        {
            return new PortfolioRatingModel
            {
                risk = 80,
                suitability = 88,
                notSuited = 20
            };




        }
        public MortgageInvestmentProfileModel Mortgage_GetProfiles_Adviser(string adviserUserId)
        {
            return new MortgageInvestmentProfileModel
            {
                data = new List<MortgageInvestmentProfileItem>
                {
                    new MortgageInvestmentProfileItem{ 
                        propertyName= "name 1",
                        address= "address one ",
                        currency= "USD",
                        marketValue= RandomMoney(),
                        outstandingLoan=RandomMoney(),
                        currentPropertyGearingRatio=RandomPercentage(),
                        institution= "CBA",
                        typeOfRates="Fixed",
                        interestRates= RandomPercentage(),
                        monthlyRepaymentAmount=RandomMoney(),
                        montlyRepaymentDate= rdm.Next() * 30,
                        loanContractTerm=rdm.Next() * 10,
                        startDate=DateTime.Now,
                        loanExpiryDate=DateTime.Now,
                        numberOfYearsToDate=12,
                        NumberOfYearsToExpiry= 3,
                        RepaymentType= "type",
                        maximumLoanLimit= RandomMoney(),
                        currentLoanBalance=RandomMoney(),
                        avaliableFunds=RandomMoney(),
                        repaymentMethod= "Direct Debt",
                        currentFinancialYearInterest=RandomMoney(),
                        lastFinancialYearInterest=RandomMoney(),
                        suitability=RandomPercentage(),
                        id= "1",
                        latitude= -26.274985,
                        longitude= 134.775464,
                        country= "Australia",
                        state="VIC",
                        type="Office"                    
                    
                    },new MortgageInvestmentProfileItem{ 
                        propertyName= "name 1",
                        address= "address one ",
                        currency= "USD",
                        marketValue= RandomMoney(),
                        outstandingLoan=RandomMoney(),
                        currentPropertyGearingRatio=RandomPercentage(),
                        institution= "CBA",
                        typeOfRates="Fixed",
                        interestRates= RandomPercentage(),
                        monthlyRepaymentAmount=RandomMoney(),
                        montlyRepaymentDate= rdm.Next() * 30,
                        loanContractTerm=rdm.Next() * 10,
                        startDate=DateTime.Now,
                        loanExpiryDate=DateTime.Now,
                        numberOfYearsToDate=12,
                        NumberOfYearsToExpiry= 3,
                        RepaymentType= "type",
                        maximumLoanLimit= RandomMoney(),
                        currentLoanBalance=RandomMoney(),
                        avaliableFunds=RandomMoney(),
                        repaymentMethod= "Direct Debt",
                        currentFinancialYearInterest=RandomMoney(),
                        lastFinancialYearInterest=RandomMoney(),
                        suitability=RandomPercentage(),
                        id= "1",
                        latitude= -26.274985,
                        longitude= 134.775464,
                        country= "Australia",
                        state="VIC",
                        type="Office"                    
                    
                    },new MortgageInvestmentProfileItem{ 
                        propertyName= "name 1",
                        address= "address one ",
                        currency= "USD",
                        marketValue= RandomMoney(),
                        outstandingLoan=RandomMoney(),
                        currentPropertyGearingRatio=RandomPercentage(),
                        institution= "CBA",
                        typeOfRates="Fixed",
                        interestRates= RandomPercentage(),
                        monthlyRepaymentAmount=RandomMoney(),
                        montlyRepaymentDate= rdm.Next() * 30,
                        loanContractTerm=rdm.Next() * 10,
                        startDate=DateTime.Now,
                        loanExpiryDate=DateTime.Now,
                        numberOfYearsToDate=12,
                        NumberOfYearsToExpiry= 3,
                        RepaymentType= "type",
                        maximumLoanLimit= RandomMoney(),
                        currentLoanBalance=RandomMoney(),
                        avaliableFunds=RandomMoney(),
                        repaymentMethod= "Direct Debt",
                        currentFinancialYearInterest=RandomMoney(),
                        lastFinancialYearInterest=RandomMoney(),
                        suitability=RandomPercentage(),
                        id= "1",
                        latitude= -26.274985,
                        longitude= 134.775464,
                        country= "Australia",
                        state="VIC",
                        type="Office"                    
                    
                    }
                }
            };
            //throw new NotImplementedException();
            //return dbService.Mortgage_GetProfiles_Adviser(adviserUserId);
        }
        public MortgageInvestmentProfileModel Mortgage_GetProfiles_Client(string clientUserId)
        {
            return new MortgageInvestmentProfileModel
            {
                data = new List<MortgageInvestmentProfileItem>
                {
                    new MortgageInvestmentProfileItem{ 
                        propertyName= "name 1",
                        address= "address one ",
                        currency= "USD",
                        marketValue= RandomMoney(),
                        outstandingLoan=RandomMoney(),
                        currentPropertyGearingRatio=RandomPercentage(),
                        institution= "CBA",
                        typeOfRates="Fixed",
                        interestRates= RandomPercentage(),
                        monthlyRepaymentAmount=RandomMoney(),
                        montlyRepaymentDate= rdm.Next() * 30,
                        loanContractTerm=rdm.Next() * 10,
                        startDate=DateTime.Now,
                        loanExpiryDate=DateTime.Now,
                        numberOfYearsToDate=12,
                        NumberOfYearsToExpiry= 3,
                        RepaymentType= "type",
                        maximumLoanLimit= RandomMoney(),
                        currentLoanBalance=RandomMoney(),
                        avaliableFunds=RandomMoney(),
                        repaymentMethod= "Direct Debt",
                        currentFinancialYearInterest=RandomMoney(),
                        lastFinancialYearInterest=RandomMoney(),
                        suitability=RandomPercentage(),
                        id= "1",
                        latitude= -26.274985,
                        longitude= 134.775464,
                        country= "Australia",
                        state="VIC",
                        type="Office"                    
                    
                    },new MortgageInvestmentProfileItem{ 
                        propertyName= "name 1",
                        address= "address one ",
                        currency= "USD",
                        marketValue= RandomMoney(),
                        outstandingLoan=RandomMoney(),
                        currentPropertyGearingRatio=RandomPercentage(),
                        institution= "CBA",
                        typeOfRates="Fixed",
                        interestRates= RandomPercentage(),
                        monthlyRepaymentAmount=RandomMoney(),
                        montlyRepaymentDate= rdm.Next() * 30,
                        loanContractTerm=rdm.Next() * 10,
                        startDate=DateTime.Now,
                        loanExpiryDate=DateTime.Now,
                        numberOfYearsToDate=12,
                        NumberOfYearsToExpiry= 3,
                        RepaymentType= "type",
                        maximumLoanLimit= RandomMoney(),
                        currentLoanBalance=RandomMoney(),
                        avaliableFunds=RandomMoney(),
                        repaymentMethod= "Direct Debt",
                        currentFinancialYearInterest=RandomMoney(),
                        lastFinancialYearInterest=RandomMoney(),
                        suitability=RandomPercentage(),
                        id= "1",
                        latitude= -26.274985,
                        longitude= 134.775464,
                        country= "Australia",
                        state="VIC",
                        type="Office"                    
                    
                    },new MortgageInvestmentProfileItem{ 
                        propertyName= "name 1",
                        address= "address one ",
                        currency= "USD",
                        marketValue= RandomMoney(),
                        outstandingLoan=RandomMoney(),
                        currentPropertyGearingRatio=RandomPercentage(),
                        institution= "CBA",
                        typeOfRates="Fixed",
                        interestRates= RandomPercentage(),
                        monthlyRepaymentAmount=RandomMoney(),
                        montlyRepaymentDate= rdm.Next() * 30,
                        loanContractTerm=rdm.Next() * 10,
                        startDate=DateTime.Now,
                        loanExpiryDate=DateTime.Now,
                        numberOfYearsToDate=12,
                        NumberOfYearsToExpiry= 3,
                        RepaymentType= "type",
                        maximumLoanLimit= RandomMoney(),
                        currentLoanBalance=RandomMoney(),
                        avaliableFunds=RandomMoney(),
                        repaymentMethod= "Direct Debt",
                        currentFinancialYearInterest=RandomMoney(),
                        lastFinancialYearInterest=RandomMoney(),
                        suitability=RandomPercentage(),
                        id= "1",
                        latitude= -26.274985,
                        longitude= 134.775464,
                        country= "Australia",
                        state="VIC",
                        type="Office"                    
                    
                    }
                }
            };
        }

        #endregion
        #region credit card services
        public IEnumerable<DataNameAmountPair> CreditCard_GetDiversification_Adviser(string adviserUserId)
        {
            throw new NotImplementedException();
            //return dbService.CreditCard_GetDiversification_Adviser(adviserUserId);
        }
        public IEnumerable<DataNameAmountPair> CreditCard_GetDiversification_Client(string clientUserId)
        {
            return new List<DataNameAmountPair>
            {
                new DataNameAmountPair{name="Secured Personal Loan Amount", amount=RandomMoney()},
                new DataNameAmountPair{name="Unsecured Personal Loan Amount", amount=RandomMoney()},
                new DataNameAmountPair{name="Visa Credit Card Amount", amount=RandomMoney()},
                new DataNameAmountPair{name="Mastercard Credit Card Amount", amount=RandomMoney()},
                new DataNameAmountPair{name="American Express Amount", amount=RandomMoney()},
                new DataNameAmountPair{name="Miscellaneous Credit Cards Amount", amount=RandomMoney()},
            };
        }
        public CashflowBriefModel CreditCard_GetCashflowSummary_Adviser(string adviserUserId)
        {
            throw new NotImplementedException();
            //return dbService.GetCashflowSummary_Advisor(adviserUserId, (int)AssetTypes.PersonalAndCreditCardLoan);
        }
        public CashflowBriefModel CreditCard_GetCashflowSummary_Client(string clientUserId)
        {
            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
        }
        public CreditCardQuickStatsModel CreditCard_GetQuickStats_Adviser(string adviserUserId)
        {
            throw new NotImplementedException();
            //return dbService.CreditCard_GetQuickStats_Adviser(adviserUserId);
        }
        public CreditCardQuickStatsModel CreditCard_GetQuickStats_Client(string clientUserId)
        {
            return new CreditCardQuickStatsModel
            {
                totalAvailableFunds = RandomMoney(),
                totalAvailableShortTermCredit = RandomMoney(),
                totalOutstandingShortTermDebt = RandomMoney()
            };
        }
        public IEnumerable<CreditCardDetailsModel> CreditCard_GetCardDetails_Adviser(string adviserUserId)
        {
            throw new NotImplementedException();
            //return dbService.CreditCard_GetCardDetails_Adviser(adviserUserId);
        }
        public IEnumerable<CreditCardDetailsModel> CreditCard_GetCardDetails_Client(string adviserUserId)
        {
            return new List<CreditCardDetailsModel>
            {
                new CreditCardDetailsModel{ 
                    type= "Secured Personal Loan",
                    issuer="Bendigo Bank",
                    assetSecurity= "No Asset",
                    maximumCreditLimit= RandomMoney(),
                    accountBalance=RandomMoney(),
                    availableFunds=RandomMoney(),
                    termOfLoan=RandomPercentage(),
                    expiryDate=DateTime.Now,
                    interestRate=RandomPercentage()                
                },new CreditCardDetailsModel{ 
                    type= "Secured Personal Loan",
                    issuer="Bendigo Bank",
                    assetSecurity= "No Asset",
                    maximumCreditLimit= RandomMoney(),
                    accountBalance=RandomMoney(),
                    availableFunds=RandomMoney(),
                    termOfLoan=RandomPercentage(),
                    expiryDate=DateTime.Now,
                    interestRate=RandomPercentage()                
                },new CreditCardDetailsModel{ 
                    type= "Secured Personal Loan",
                    issuer="Bendigo Bank",
                    assetSecurity= "No Asset",
                    maximumCreditLimit= RandomMoney(),
                    accountBalance=RandomMoney(),
                    availableFunds=RandomMoney(),
                    termOfLoan=RandomPercentage(),
                    expiryDate=DateTime.Now,
                    interestRate=RandomPercentage()                
                },new CreditCardDetailsModel{ 
                    type= "Secured Personal Loan",
                    issuer="Bendigo Bank",
                    assetSecurity= "No Asset",
                    maximumCreditLimit= RandomMoney(),
                    accountBalance=RandomMoney(),
                    availableFunds=RandomMoney(),
                    termOfLoan=RandomPercentage(),
                    expiryDate=DateTime.Now,
                    interestRate=RandomPercentage()                
                },new CreditCardDetailsModel{ 
                    type= "Secured Personal Loan",
                    issuer="Bendigo Bank",
                    assetSecurity= "No Asset",
                    maximumCreditLimit= RandomMoney(),
                    accountBalance=RandomMoney(),
                    availableFunds=RandomMoney(),
                    termOfLoan=RandomPercentage(),
                    expiryDate=DateTime.Now,
                    interestRate=RandomPercentage()                
                },new CreditCardDetailsModel{ 
                    type= "Secured Personal Loan",
                    issuer="Bendigo Bank",
                    assetSecurity= "No Asset",
                    maximumCreditLimit= RandomMoney(),
                    accountBalance=RandomMoney(),
                    availableFunds=RandomMoney(),
                    termOfLoan=RandomPercentage(),
                    expiryDate=DateTime.Now,
                    interestRate=RandomPercentage()                
                },

            };
        }
        public IEnumerable<CreditCardStatModel> CreditCard_GetCardStats_Adviser(string adviserUserId)
        {
            return new List<CreditCardStatModel>
            {
                new CreditCardStatModel{
                    accountName= "name",
                    bsb= "123456",
                    accountNumber= "123456789",
                    repaymentFrequency= "weekly",
                    lastPaymentDate=DateTime.Now,
                    lastRepaymentAmount=RandomMoney(),
                    nextRepaymentDate=DateTime.Now,
                    nextRepaymentAmount=RandomMoney(),
                    daysToLoanExpiry=RandomMoney(),
                    awardsMember= "member",
                    currentAwardPoints=888,
                    purchasedInterestRate=RandomPercentage(),
                    cashAdvanceRate=RandomPercentage()                
                },new CreditCardStatModel{
                    accountName= "name",
                    bsb= "123456",
                    accountNumber= "123456789",
                    repaymentFrequency= "weekly",
                    lastPaymentDate=DateTime.Now,
                    lastRepaymentAmount=RandomMoney(),
                    nextRepaymentDate=DateTime.Now,
                    nextRepaymentAmount=RandomMoney(),
                    daysToLoanExpiry=RandomMoney(),
                    awardsMember= "member",
                    currentAwardPoints=888,
                    purchasedInterestRate=RandomPercentage(),
                    cashAdvanceRate=RandomPercentage()                
                },new CreditCardStatModel{
                    accountName= "name",
                    bsb= "123456",
                    accountNumber= "123456789",
                    repaymentFrequency= "weekly",
                    lastPaymentDate=DateTime.Now,
                    lastRepaymentAmount=RandomMoney(),
                    nextRepaymentDate=DateTime.Now,
                    nextRepaymentAmount=RandomMoney(),
                    daysToLoanExpiry=RandomMoney(),
                    awardsMember= "member",
                    currentAwardPoints=888,
                    purchasedInterestRate=RandomPercentage(),
                    cashAdvanceRate=RandomPercentage()                
                },new CreditCardStatModel{
                    accountName= "name",
                    bsb= "123456",
                    accountNumber= "123456789",
                    repaymentFrequency= "weekly",
                    lastPaymentDate=DateTime.Now,
                    lastRepaymentAmount=RandomMoney(),
                    nextRepaymentDate=DateTime.Now,
                    nextRepaymentAmount=RandomMoney(),
                    daysToLoanExpiry=RandomMoney(),
                    awardsMember= "member",
                    currentAwardPoints=888,
                    purchasedInterestRate=RandomPercentage(),
                    cashAdvanceRate=RandomPercentage()                
                },new CreditCardStatModel{
                    accountName= "name",
                    bsb= "123456",
                    accountNumber= "123456789",
                    repaymentFrequency= "weekly",
                    lastPaymentDate=DateTime.Now,
                    lastRepaymentAmount=RandomMoney(),
                    nextRepaymentDate=DateTime.Now,
                    nextRepaymentAmount=RandomMoney(),
                    daysToLoanExpiry=RandomMoney(),
                    awardsMember= "member",
                    currentAwardPoints=888,
                    purchasedInterestRate=RandomPercentage(),
                    cashAdvanceRate=RandomPercentage()                
                },
            };
        }
        public IEnumerable<CreditCardStatModel> CreditCard_GetCardStats_Client(string adviserUserId)
        {
            return new List<CreditCardStatModel>
            {
                new CreditCardStatModel{
                    accountName= "name",
                    bsb= "123456",
                    accountNumber= "123456789",
                    repaymentFrequency= "weekly",
                    lastPaymentDate=DateTime.Now,
                    lastRepaymentAmount=RandomMoney(),
                    nextRepaymentDate=DateTime.Now,
                    nextRepaymentAmount=RandomMoney(),
                    daysToLoanExpiry=RandomMoney(),
                    awardsMember= "member",
                    currentAwardPoints=888,
                    purchasedInterestRate=RandomPercentage(),
                    cashAdvanceRate=RandomPercentage()                
                },new CreditCardStatModel{
                    accountName= "name",
                    bsb= "123456",
                    accountNumber= "123456789",
                    repaymentFrequency= "weekly",
                    lastPaymentDate=DateTime.Now,
                    lastRepaymentAmount=RandomMoney(),
                    nextRepaymentDate=DateTime.Now,
                    nextRepaymentAmount=RandomMoney(),
                    daysToLoanExpiry=RandomMoney(),
                    awardsMember= "member",
                    currentAwardPoints=888,
                    purchasedInterestRate=RandomPercentage(),
                    cashAdvanceRate=RandomPercentage()                
                },new CreditCardStatModel{
                    accountName= "name",
                    bsb= "123456",
                    accountNumber= "123456789",
                    repaymentFrequency= "weekly",
                    lastPaymentDate=DateTime.Now,
                    lastRepaymentAmount=RandomMoney(),
                    nextRepaymentDate=DateTime.Now,
                    nextRepaymentAmount=RandomMoney(),
                    daysToLoanExpiry=RandomMoney(),
                    awardsMember= "member",
                    currentAwardPoints=888,
                    purchasedInterestRate=RandomPercentage(),
                    cashAdvanceRate=RandomPercentage()                
                },new CreditCardStatModel{
                    accountName= "name",
                    bsb= "123456",
                    accountNumber= "123456789",
                    repaymentFrequency= "weekly",
                    lastPaymentDate=DateTime.Now,
                    lastRepaymentAmount=RandomMoney(),
                    nextRepaymentDate=DateTime.Now,
                    nextRepaymentAmount=RandomMoney(),
                    daysToLoanExpiry=RandomMoney(),
                    awardsMember= "member",
                    currentAwardPoints=888,
                    purchasedInterestRate=RandomPercentage(),
                    cashAdvanceRate=RandomPercentage()                
                },new CreditCardStatModel{
                    accountName= "name",
                    bsb= "123456",
                    accountNumber= "123456789",
                    repaymentFrequency= "weekly",
                    lastPaymentDate=DateTime.Now,
                    lastRepaymentAmount=RandomMoney(),
                    nextRepaymentDate=DateTime.Now,
                    nextRepaymentAmount=RandomMoney(),
                    daysToLoanExpiry=RandomMoney(),
                    awardsMember= "member",
                    currentAwardPoints=888,
                    purchasedInterestRate=RandomPercentage(),
                    cashAdvanceRate=RandomPercentage()                
                },
            };
        }
        #endregion
        #region insurance services
        public CashflowBriefModel Insurance_GetCashflowSummary_Adviser(string adviserUserId)
        {
            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
            //throw new NotImplementedException();
            //return dbService.GetCashflowSummary_Advisor(adviserUserId, (int)AssetTypes.Insurance);
        }


#warning need implementation
        public CashflowBriefModel Insurance_GetCashflowSummary_Client(string clientUserId)
        {
            return new CashflowBriefModel
            {
                data = RandomCashflow(),
                totalExpense = 100000,
                totalIncome = 100000
            };
        }
        public InsuranceStatisticsModel Insurance_GetStatistics_Adviser(string adviserUserId)
        {
            return new InsuranceStatisticsModel
            {
                totalAnnualPremium = RandomMoney(),
                totalNumberOfPolicies = RandomMoney()
            };
            //throw new NotImplementedException();
            //return dbService.Insurance_GetStatistics_Adviser(adviserUserId);
        }
        public InsuranceStatisticsModel Insurance_GetStatistics_Client(string clientUserId)
        {
            return new InsuranceStatisticsModel
            {
                totalAnnualPremium = RandomMoney(),
                totalNumberOfPolicies = RandomMoney()
            };
        }
        public List<InsuranceListItemModel> Insurance_GetInsuranceList_Adviser(string adviserUserId)
        {
            return new List<InsuranceListItemModel>
            {
                new InsuranceListItemModel{
                    insuranceMetaType="Asset Insurance",
                    data=new List<InsuranceListItemDetailModel>{
                        new InsuranceListItemDetailModel{
                            type= "Motor Vehicle Insurance",
                            insurer= "AAMI Insurance",
                            typeOfPolicy= "Car Insurance",
                            policyNumber= "123456785",
                            nameOfPolicy= "Peter",
                            policyAddress= "some address",
                            assetInsured= "94 SAAB 93 Convertible",
                            amountInsured= RandomMoney(),
                            premium= RandomMoney(),
                            premiumExpiry=DateTime.Now,
                            days= ""
                        },new InsuranceListItemDetailModel{
                            type= "Motor Vehicle Insurance 2",
                            insurer= "AAMI Insurance",
                            typeOfPolicy= "Car Insurance",
                            policyNumber= "123456785",
                            nameOfPolicy= "Peter",
                            policyAddress= "some address",
                            assetInsured= "94 SAAB 93 Convertible",
                            amountInsured= RandomMoney(),
                            premium= RandomMoney(),
                            premiumExpiry=DateTime.Now,
                            days= ""
                        },new InsuranceListItemDetailModel{
                            type= "Motor Vehicle Insurance 3",
                            insurer= "AAMI Insurance",
                            typeOfPolicy= "Car Insurance",
                            policyNumber= "123456785",
                            nameOfPolicy= "Peter",
                            policyAddress= "some address",
                            assetInsured= "94 SAAB 93 Convertible",
                            amountInsured= RandomMoney(),
                            premium= RandomMoney(),
                            premiumExpiry=DateTime.Now,
                            days= ""
                        },new InsuranceListItemDetailModel{
                            type= "Motor Vehicle Insurance 4",
                            insurer= "AAMI Insurance",
                            typeOfPolicy= "Car Insurance",
                            policyNumber= "123456785",
                            nameOfPolicy= "Peter",
                            policyAddress= "some address",
                            assetInsured= "94 SAAB 93 Convertible",
                            amountInsured= RandomMoney(),
                            premium= RandomMoney(),
                            premiumExpiry=DateTime.Now,
                            days= ""
                        },
                    }
                },new InsuranceListItemModel{
                    insuranceMetaType="Personal Insurance",
                    data=new List<InsuranceListItemDetailModel>{
                        new InsuranceListItemDetailModel{
                            type= "Insurance",
                            insurer= "AAMI Insurance",
                            typeOfPolicy= "Car Insurance",
                            policyNumber= "123456785",
                            nameOfPolicy= "Peter",
                            policyAddress= "some address",
                            assetInsured= "94 SAAB 93 Convertible",
                            amountInsured= RandomMoney(),
                            premium= RandomMoney(),
                            premiumExpiry=DateTime.Now,
                            days= ""
                        },new InsuranceListItemDetailModel{
                            type= "Insurance 2",
                            insurer= "AAMI Insurance",
                            typeOfPolicy= "Car Insurance",
                            policyNumber= "123456785",
                            nameOfPolicy= "Peter",
                            policyAddress= "some address",
                            assetInsured= "94 SAAB 93 Convertible",
                            amountInsured= RandomMoney(),
                            premium= RandomMoney(),
                            premiumExpiry=DateTime.Now,
                            days= ""
                        },new InsuranceListItemDetailModel{
                            type= "Insurance 3",
                            insurer= "AAMI Insurance",
                            typeOfPolicy= "Car Insurance",
                            policyNumber= "123456785",
                            nameOfPolicy= "Peter",
                            policyAddress= "some address",
                            assetInsured= "94 SAAB 93 Convertible",
                            amountInsured= RandomMoney(),
                            premium= RandomMoney(),
                            premiumExpiry=DateTime.Now,
                            days= ""
                        },new InsuranceListItemDetailModel{
                            type= "Insurance 4",
                            insurer= "AAMI Insurance",
                            typeOfPolicy= "Car Insurance",
                            policyNumber= "123456785",
                            nameOfPolicy= "Peter",
                            policyAddress= "some address",
                            assetInsured= "94 SAAB 93 Convertible",
                            amountInsured= RandomMoney(),
                            premium= RandomMoney(),
                            premiumExpiry=DateTime.Now,
                            days= ""
                        },
                    }
                },
            };  
            //throw new NotImplementedException();
            //return dbService.Insurance_GetInsuranceList_Adviser(adviserUserId);
        }
        public List<InsuranceListItemModel> Insurance_GetInsuranceList_Client(string clientUserId)
        {
            return new List<InsuranceListItemModel>
            {
                new InsuranceListItemModel{
                    insuranceMetaType="Asset Insurance",
                    data=new List<InsuranceListItemDetailModel>{
                        new InsuranceListItemDetailModel{
                            type= "Motor Vehicle Insurance",
                            insurer= "AAMI Insurance",
                            typeOfPolicy= "Car Insurance",
                            policyNumber= "123456785",
                            nameOfPolicy= "Peter",
                            policyAddress= "some address",
                            assetInsured= "94 SAAB 93 Convertible",
                            amountInsured= RandomMoney(),
                            premium= RandomMoney(),
                            premiumExpiry=DateTime.Now,
                            days= ""
                        },new InsuranceListItemDetailModel{
                            type= "Motor Vehicle Insurance 2",
                            insurer= "AAMI Insurance",
                            typeOfPolicy= "Car Insurance",
                            policyNumber= "123456785",
                            nameOfPolicy= "Peter",
                            policyAddress= "some address",
                            assetInsured= "94 SAAB 93 Convertible",
                            amountInsured= RandomMoney(),
                            premium= RandomMoney(),
                            premiumExpiry=DateTime.Now,
                            days= ""
                        },new InsuranceListItemDetailModel{
                            type= "Motor Vehicle Insurance 3",
                            insurer= "AAMI Insurance",
                            typeOfPolicy= "Car Insurance",
                            policyNumber= "123456785",
                            nameOfPolicy= "Peter",
                            policyAddress= "some address",
                            assetInsured= "94 SAAB 93 Convertible",
                            amountInsured= RandomMoney(),
                            premium= RandomMoney(),
                            premiumExpiry=DateTime.Now,
                            days= ""
                        },new InsuranceListItemDetailModel{
                            type= "Motor Vehicle Insurance 4",
                            insurer= "AAMI Insurance",
                            typeOfPolicy= "Car Insurance",
                            policyNumber= "123456785",
                            nameOfPolicy= "Peter",
                            policyAddress= "some address",
                            assetInsured= "94 SAAB 93 Convertible",
                            amountInsured= RandomMoney(),
                            premium= RandomMoney(),
                            premiumExpiry=DateTime.Now,
                            days= ""
                        },
                    }
                },new InsuranceListItemModel{
                    insuranceMetaType="Personal Insurance",
                    data=new List<InsuranceListItemDetailModel>{
                        new InsuranceListItemDetailModel{
                            type= "Insurance",
                            insurer= "AAMI Insurance",
                            typeOfPolicy= "Car Insurance",
                            policyNumber= "123456785",
                            nameOfPolicy= "Peter",
                            policyAddress= "some address",
                            assetInsured= "94 SAAB 93 Convertible",
                            amountInsured= RandomMoney(),
                            premium= RandomMoney(),
                            premiumExpiry=DateTime.Now,
                            days= ""
                        },new InsuranceListItemDetailModel{
                            type= "Insurance 2",
                            insurer= "AAMI Insurance",
                            typeOfPolicy= "Car Insurance",
                            policyNumber= "123456785",
                            nameOfPolicy= "Peter",
                            policyAddress= "some address",
                            assetInsured= "94 SAAB 93 Convertible",
                            amountInsured= RandomMoney(),
                            premium= RandomMoney(),
                            premiumExpiry=DateTime.Now,
                            days= ""
                        },new InsuranceListItemDetailModel{
                            type= "Insurance 3",
                            insurer= "AAMI Insurance",
                            typeOfPolicy= "Car Insurance",
                            policyNumber= "123456785",
                            nameOfPolicy= "Peter",
                            policyAddress= "some address",
                            assetInsured= "94 SAAB 93 Convertible",
                            amountInsured= RandomMoney(),
                            premium= RandomMoney(),
                            premiumExpiry=DateTime.Now,
                            days= ""
                        },new InsuranceListItemDetailModel{
                            type= "Insurance 4",
                            insurer= "AAMI Insurance",
                            typeOfPolicy= "Car Insurance",
                            policyNumber= "123456785",
                            nameOfPolicy= "Peter",
                            policyAddress= "some address",
                            assetInsured= "94 SAAB 93 Convertible",
                            amountInsured= RandomMoney(),
                            premium= RandomMoney(),
                            premiumExpiry=DateTime.Now,
                            days= ""
                        },
                    }
                },
            };
        }
        //public List<InsuranceConditionModel> Insurance_GetConditions_Adviser(string adviserUserId)
        //{
        //    return new List<InsuranceConditionModel>
        //    {
        //        new InsuranceConditionModel{ 
        //            number= "001",
        //            typeOfInsurance= "Type",
        //            policyNumber= "123456789",
        //            benefit1= "One",
        //            benefit2= "Two",
        //            benefit3= "Three",
        //            benefit4= "Four",
        //            benefit5= "Five",
        //            condition1= "One",
        //            condition2= "Two",
        //            condition3= "Three",
        //            condition4= "Four"
        //        },new InsuranceConditionModel{ 
        //            number= "001",
        //            typeOfInsurance= "Type",
        //            policyNumber= "123456789",
        //            benefit1= "One",
        //            benefit2= "Two",
        //            benefit3= "Three",
        //            benefit4= "Four",
        //            benefit5= "Five",
        //            condition1= "One",
        //            condition2= "Two",
        //            condition3= "Three",
        //            condition4= "Four"
        //        }
        //    };
        //}
        //public List<InsuranceConditionModel> Insurance_GetConditions_Client(string adviserUserId)
        //{
        //    return new List<InsuranceConditionModel>
        //    {
        //        new InsuranceConditionModel{ 
        //            number= "001",
        //            typeOfInsurance= "Type",
        //            policyNumber= "123456789",
        //            benefit1= "One",
        //            benefit2= "Two",
        //            benefit3= "Three",
        //            benefit4= "Four",
        //            benefit5= "Five",
        //            condition1= "One",
        //            condition2= "Two",
        //            condition3= "Three",
        //            condition4= "Four"
        //        },new InsuranceConditionModel{ 
        //            number= "001",
        //            typeOfInsurance= "Type",
        //            policyNumber= "123456789",
        //            benefit1= "One",
        //            benefit2= "Two",
        //            benefit3= "Three",
        //            benefit4= "Four",
        //            benefit5= "Five",
        //            condition1= "One",
        //            condition2= "Two",
        //            condition3= "Three",
        //            condition4= "Four"
        //        }
        //    };
        //}


        #endregion


        #region margin lending services

        #endregion






        #region mockup generation helpers, to be replaced in production
        public List<CashFlowBriefItem> RandomCashflow()
        {
            List<CashFlowBriefItem> result = new List<CashFlowBriefItem>();
            var date = DateTime.Now;
            for (int i = 0; i < 12; i++)
            {
                var dt = date.AddMonths(i);
                result.Add(new CashFlowBriefItem
                {
                    date = dt,
                    expense = RandomMoney(),
                    income = RandomMoney(),
                    month = dt.ToString("MMM")
                });

            }

            return result;
        }
        public double RandomMoney()
        {

            return Math.Round((rdm.NextDouble() * 100000), 2);
        }
        public double RandomPercentage()
        {
            return Math.Round(rdm.NextDouble() * 100, 2);
        }
        #endregion



        internal PortfolioStasticsModel Overview_GetPortfolioStat_Client(string p)
        {
            throw new NotImplementedException();
        }
    }
}