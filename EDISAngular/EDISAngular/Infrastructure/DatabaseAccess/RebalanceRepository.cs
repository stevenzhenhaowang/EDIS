using EDIS_DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDISAngular.Models.ServiceModels.RebalanceModels;
using EDISAngular.Infrastructure.DbFirst;

namespace EDISAngular.Infrastructure.DatabaseAccess
{
    public class RebalanceRepository : IDisposable
    {
        private edisDbEntities db;
        private CommonReferenceDataRepository comRepo;
        private Random rdm = new Random();

        public RebalanceRepository(edisDbEntities database)
        {
            db = database;
            comRepo = new CommonReferenceDataRepository(db);
        }
        public RebalanceRepository()
        {
            db = new edisDbEntities();
            comRepo = new CommonReferenceDataRepository(db);
        }


        public List<RebalanceModel> GetAllModelsForAdviser(string adviserUserId)
        {
            return new List<RebalanceModel>
            {
                new RebalanceModel{
                    modelId="001",
                    modelName="Model 001 for Mr. & Mrs. X",
                    profile=GetModelProfile(""),
                    diversificationData=GetDiversificationData(""),
                    balanceSheet=GetBalanceSheetAgainstModel(""),
                    rebalancedDataAnalysis=GetRebalancedDataAnalysisData(""),
                    regionalData=GetRegionalComparisonModel(""),
                    sectorialData=GetSectorialComparisonModel(""),
                    monthlyCashflow=GetmonthlyCashflowComparisonModel(""),
                    monthlyInvestmentIncome=GetMonthlyInvestmentIncome(""),
                    anticipatedFirstYearManagementCost=GetAnticipatedFirstYearmanagementCost(""),
                    anticipatedSecondYearManagementCost=GetAnticipatedSecondYearmanagementCost(""),
                    anticipatedFirstYearOngoingServiceFee=GetAnticipatedFirstYearOngoingServiceFee(""),
                    anticipatedSecondYearOngoingServiceFee=GetAnticipatedSecondYearOngoingServiceFee(""),
                    transactionCost=GetTransactionCost(""),
                    templateDetails=GetTemplateDetails("")
                },
                new RebalanceModel{
                    modelId="002",
                    modelName="Model 002 for Mr. & Mrs. Y",
                    profile=GetModelProfile(""),
                    diversificationData=GetDiversificationData(""),
                    balanceSheet=GetBalanceSheetAgainstModel(""),
                    rebalancedDataAnalysis=GetRebalancedDataAnalysisData(""),
                    regionalData=GetRegionalComparisonModel(""),
                    sectorialData=GetSectorialComparisonModel(""),
                    monthlyCashflow=GetmonthlyCashflowComparisonModel(""),
                    monthlyInvestmentIncome=GetMonthlyInvestmentIncome(""),
                    anticipatedFirstYearManagementCost=GetAnticipatedFirstYearmanagementCost(""),
                    anticipatedSecondYearManagementCost=GetAnticipatedSecondYearmanagementCost(""),
                    anticipatedFirstYearOngoingServiceFee=GetAnticipatedFirstYearOngoingServiceFee(""),
                    anticipatedSecondYearOngoingServiceFee=GetAnticipatedSecondYearOngoingServiceFee(""),
                    transactionCost=GetTransactionCost(""),
                    templateDetails=GetTemplateDetails("")
                },
                new RebalanceModel{
                    modelId="003",
                    modelName="Model 003 for Mr. & Mrs. Z",
                    profile=GetModelProfile(""),
                    diversificationData=GetDiversificationData(""),
                    balanceSheet=GetBalanceSheetAgainstModel(""),
                    rebalancedDataAnalysis=GetRebalancedDataAnalysisData(""),
                    regionalData=GetRegionalComparisonModel(""),
                    sectorialData=GetSectorialComparisonModel(""),
                    monthlyCashflow=GetmonthlyCashflowComparisonModel(""),
                    monthlyInvestmentIncome=GetMonthlyInvestmentIncome(""),
                    anticipatedFirstYearManagementCost=GetAnticipatedFirstYearmanagementCost(""),
                    anticipatedSecondYearManagementCost=GetAnticipatedSecondYearmanagementCost(""),
                    anticipatedFirstYearOngoingServiceFee=GetAnticipatedFirstYearOngoingServiceFee(""),
                    anticipatedSecondYearOngoingServiceFee=GetAnticipatedSecondYearOngoingServiceFee(""),
                    transactionCost=GetTransactionCost(""),
                    templateDetails=GetTemplateDetails("")
                },
            };
        }
        public List<RebalanceModel> GetAllModelsForClient(string clientUserId)
        {
            return new List<RebalanceModel>
            {
                new RebalanceModel{
                    modelId="001",
                    modelName="Model 001 for Mr. & Mrs. X",
                    profile=GetModelProfile(""),
                    diversificationData=GetDiversificationData(""),
                    balanceSheet=GetBalanceSheetAgainstModel(""),
                    rebalancedDataAnalysis=GetRebalancedDataAnalysisData(""),
                    regionalData=GetRegionalComparisonModel(""),
                    sectorialData=GetSectorialComparisonModel(""),
                    monthlyCashflow=GetmonthlyCashflowComparisonModel(""),
                    monthlyInvestmentIncome=GetMonthlyInvestmentIncome(""),
                    anticipatedFirstYearManagementCost=GetAnticipatedFirstYearmanagementCost(""),
                    anticipatedSecondYearManagementCost=GetAnticipatedSecondYearmanagementCost(""),
                    anticipatedFirstYearOngoingServiceFee=GetAnticipatedFirstYearOngoingServiceFee(""),
                    anticipatedSecondYearOngoingServiceFee=GetAnticipatedSecondYearOngoingServiceFee(""),
                    transactionCost=GetTransactionCost(""),
                    templateDetails=GetTemplateDetails("")
                },
                new RebalanceModel{
                    modelId="002",
                    modelName="Model 002 for Mr. & Mrs. Y",
                    profile=GetModelProfile(""),
                    diversificationData=GetDiversificationData(""),
                    balanceSheet=GetBalanceSheetAgainstModel(""),
                    rebalancedDataAnalysis=GetRebalancedDataAnalysisData(""),
                    regionalData=GetRegionalComparisonModel(""),
                    sectorialData=GetSectorialComparisonModel(""),
                    monthlyCashflow=GetmonthlyCashflowComparisonModel(""),
                    monthlyInvestmentIncome=GetMonthlyInvestmentIncome(""),
                    anticipatedFirstYearManagementCost=GetAnticipatedFirstYearmanagementCost(""),
                    anticipatedSecondYearManagementCost=GetAnticipatedSecondYearmanagementCost(""),
                    anticipatedFirstYearOngoingServiceFee=GetAnticipatedFirstYearOngoingServiceFee(""),
                    anticipatedSecondYearOngoingServiceFee=GetAnticipatedSecondYearOngoingServiceFee(""),
                    transactionCost=GetTransactionCost(""),
                    templateDetails=GetTemplateDetails("")
                },
                new RebalanceModel{
                    modelId="003",
                    modelName="Model 003 for Mr. & Mrs. Z",
                    profile=GetModelProfile(""),
                    diversificationData=GetDiversificationData(""),
                    balanceSheet=GetBalanceSheetAgainstModel(""),
                    rebalancedDataAnalysis=GetRebalancedDataAnalysisData(""),
                    regionalData=GetRegionalComparisonModel(""),
                    sectorialData=GetSectorialComparisonModel(""),
                    monthlyCashflow=GetmonthlyCashflowComparisonModel(""),
                    monthlyInvestmentIncome=GetMonthlyInvestmentIncome(""),
                    anticipatedFirstYearManagementCost=GetAnticipatedFirstYearmanagementCost(""),
                    anticipatedSecondYearManagementCost=GetAnticipatedSecondYearmanagementCost(""),
                    anticipatedFirstYearOngoingServiceFee=GetAnticipatedFirstYearOngoingServiceFee(""),
                    anticipatedSecondYearOngoingServiceFee=GetAnticipatedSecondYearOngoingServiceFee(""),
                    transactionCost=GetTransactionCost(""),
                    templateDetails=GetTemplateDetails("")
                },
            };
        }



        public RebalanceModel GetModelById(string modelId)
        {
            return new RebalanceModel
            {
                modelId = modelId,
                modelName = "Model "+modelId,
                profile = GetModelProfile(modelId),
                diversificationData = GetDiversificationData(modelId),
                balanceSheet = GetBalanceSheetAgainstModel(modelId),
                rebalancedDataAnalysis = GetRebalancedDataAnalysisData(modelId),
                regionalData = GetRegionalComparisonModel(modelId),
                sectorialData = GetSectorialComparisonModel(modelId),
                monthlyCashflow = GetmonthlyCashflowComparisonModel(modelId),
                monthlyInvestmentIncome = GetMonthlyInvestmentIncome(modelId),
                anticipatedFirstYearManagementCost = GetAnticipatedFirstYearmanagementCost(modelId),
                anticipatedSecondYearManagementCost = GetAnticipatedSecondYearmanagementCost(modelId),
                anticipatedFirstYearOngoingServiceFee = GetAnticipatedFirstYearOngoingServiceFee(modelId),
                anticipatedSecondYearOngoingServiceFee = GetAnticipatedSecondYearOngoingServiceFee(modelId),
                transactionCost = GetTransactionCost(modelId),
                templateDetails = GetTemplateDetails(modelId),
                compliance = GetComplianceData(modelId)
            };
        }
        public void CreateNewModel(RebalanceCreationModel model, string adviserUserId)
        {
            //add model and link it to the corresponding client group
            throw new NotImplementedException();
        }
        public void RemoveModel(string modelId)
        {
            throw new NotImplementedException();
        }
        public List<TemplateDetailsItemParameter> GetAllParametersForGroup(string groupId)
        {
            return new List<TemplateDetailsItemParameter>
            {
                new TemplateDetailsItemParameter{
                    groupId=new List<string>{"asset00", "asset01", "europe00", "europe01"},
                    itemName="Item Name 1",
                    id="T03",
                    marketValue=RandomWeighting(),
                    currentValue=RandomWeighting(),
                    currentWeighting=RandomWeighting(),
                    identityMetaKey=new List<string>{"STOCK"}
                },
                new TemplateDetailsItemParameter{
                    groupId=new List<string>{"asset00", "asset01", "europe00", "europe01"},
                    itemName="Item Name 2",
                    id="T04",
                    marketValue=RandomWeighting(),
                    currentValue=RandomWeighting(),
                    currentWeighting=RandomWeighting(),
                    identityMetaKey=new List<string>{"STOCK"}
                }
            };
        }
        public List<FilterGroupModel> GetFilterGroups(string adviserUserId)
        {
            return new List<FilterGroupModel>
            {
                new FilterGroupModel{
                filters = new List<FilterGroupFilter>
                    {
                        new FilterGroupFilter{
                            groupId="group 1",
                            groupName="Non-investable Asset",
                            identityMetaKey= new List<string>{"ASSET#NONINVESTABLE"},
                            currentWeighting=RandomWeighting(),
                            groupType="Asset"
                        },new FilterGroupFilter{
                            groupId="group 2",
                            groupName="Australian Equity",
                            identityMetaKey= new List<string>{"ASSET#NONINVESTABLE"},
                            currentWeighting=RandomWeighting(),
                            groupType="Asset"
                        },new FilterGroupFilter{
                            groupId="group 3",
                            groupName="International Equity",
                            identityMetaKey= new List<string>{"ASSET#NONINVESTABLE"},
                            currentWeighting=RandomWeighting(),
                            groupType="Asset"
                        },new FilterGroupFilter{
                            groupId="group 4",
                            groupName="Mortgate & Investment Home Loan",
                            identityMetaKey= new List<string>{"ASSET#NONINVESTABLE"},
                            currentWeighting=RandomWeighting(),
                            groupType="Asset"
                        },
                    },
                    classificationType="Regional and Country"
                }, new FilterGroupModel{
                filters = new List<FilterGroupFilter>
                    {
                        new FilterGroupFilter{
                            groupId="group 1",
                            groupName="Non-investable Asset",
                            identityMetaKey= new List<string>{"ASSET#NONINVESTABLE"},
                            currentWeighting=RandomWeighting(),
                            groupType="Asset"
                        },new FilterGroupFilter{
                            groupId="group 2",
                            groupName="Australian Equity",
                            identityMetaKey= new List<string>{"ASSET#NONINVESTABLE"},
                            currentWeighting=RandomWeighting(),
                            groupType="Asset"
                        },new FilterGroupFilter{
                            groupId="group 3",
                            groupName="International Equity",
                            identityMetaKey= new List<string>{"ASSET#NONINVESTABLE"},
                            currentWeighting=RandomWeighting(),
                            groupType="Asset"
                        },new FilterGroupFilter{
                            groupId="group 4",
                            groupName="Mortgate & Investment Home Loan",
                            identityMetaKey= new List<string>{"ASSET#NONINVESTABLE"},
                            currentWeighting=RandomWeighting(),
                            groupType="Asset"
                        },
                    },
                     classificationType="Sectors"
                },

            };
        }
        public List<ModelProfile> GetAllModelProfiles()
        {
            return new List<ModelProfile>
            {
                new ModelProfile{ profileId="001", profileName="Aggressive"},
                new ModelProfile{ profileId="002", profileName="Neutral"},
                new ModelProfile{ profileId="003", profileName="Defensive"},
            };
        }



        #region helper methods
        private ModelProfile GetModelProfile(string modelId)
        {
            return new ModelProfile
            {
                profileId = "MD" + rdm.Next().ToString(),
                profileName = "Profile Name"
            };
        }
        private List<DiversificationDatas> GetDiversificationData(string modelId)
        {
            return new List<DiversificationDatas>
            {
                new DiversificationDatas{group="Group 1",portfolioWeighting=RandomWeighting(),modelWeighting=RandomWeighting()},
                new DiversificationDatas{group="Group 2",portfolioWeighting=RandomWeighting(),modelWeighting=RandomWeighting()},
                new DiversificationDatas{group="Group 3",portfolioWeighting=RandomWeighting(),modelWeighting=RandomWeighting()},
                new DiversificationDatas{group="Group 4",portfolioWeighting=RandomWeighting(),modelWeighting=RandomWeighting()},
                new DiversificationDatas{group="Group 5",portfolioWeighting=RandomWeighting(),modelWeighting=RandomWeighting()},
                new DiversificationDatas{group="Group 6",portfolioWeighting=RandomWeighting(),modelWeighting=RandomWeighting()},
            };
        }
        private BalanceSheetAgainstModel GetBalanceSheetAgainstModel(string modelId)
        {
            return new BalanceSheetAgainstModel
            {
                current = RandomWeighting(),
                currentWeighting = RandomWeighting(),
                proposed = RandomWeighting(),
                proposedWeighting = RandomWeighting(),
                difference = RandomWeighting(),
                data = new List<BalanceSheetAgainstModelData>
                {
                    new BalanceSheetAgainstModelData{ 
                        groupName="Group 1",
                        current=RandomWeighting(),
                        currentWeighting=RandomWeighting(),
                        proposed=RandomWeighting(),
                        proposedWeighting=RandomWeighting(),
                        difference=RandomWeighting(),
                        items=new List<BalanceSheetAgainstModelItem>{
                            new BalanceSheetAgainstModelItem{
                                itemName="Item 1",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            },
                            new BalanceSheetAgainstModelItem{
                                itemName="Item 2",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            },
                            new BalanceSheetAgainstModelItem{
                                itemName="Item 3",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            },
                        }
                    },
                    new BalanceSheetAgainstModelData{ 
                        groupName="Group 2",
                        current=RandomWeighting(),
                        currentWeighting=RandomWeighting(),
                        proposed=RandomWeighting(),
                        proposedWeighting=RandomWeighting(),
                        difference=RandomWeighting(),
                        items=new List<BalanceSheetAgainstModelItem>{
                            new BalanceSheetAgainstModelItem{
                                itemName="Item 1",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            },
                            new BalanceSheetAgainstModelItem{
                                itemName="Item 2",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            },
                            new BalanceSheetAgainstModelItem{
                                itemName="Item 3",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            },
                        }
                    }
                }
            };
        }
        private List<RebalanceDataAnalysisModel> GetRebalancedDataAnalysisData(string modelId)
        {
            return new List<RebalanceDataAnalysisModel>
            {
                new RebalanceDataAnalysisModel{
                    groupName="Group 1",
                    data=new List<RebalanceDataAnalysisData>{
                        new RebalanceDataAnalysisData{
                            ticker="Ticker 1",
                            name="Name",
                            currentPrice=RandomWeighting(),
                            currentExposure=new RebalanceDataAnalysisDataItem{
                                units=rdm.Next(),
                                value=RandomWeighting(),
                                profitAndLoss=RandomWeighting()
                            },
                            rebalance=new RebalanceDataAnalysisDataItem{
                                units=rdm.Next(),
                                value=RandomWeighting(),
                                profitAndLoss=RandomWeighting()                            
                            },
                            advantageousAndDisadvantageous=new RebalanceDataAnalysisAdvantageDisadvantage{
                                differences=RandomWeighting(),
                                suitability=rdm.Next(),
                                target=RandomWeighting(),
                                differenceToTarget=RandomWeighting(),
                                apl="apl"
                            }
                        },new RebalanceDataAnalysisData{
                            ticker="Ticker 2",
                            name="Name",
                            currentPrice=RandomWeighting(),
                            currentExposure=new RebalanceDataAnalysisDataItem{
                                units=rdm.Next(),
                                value=RandomWeighting(),
                                profitAndLoss=RandomWeighting()
                            },
                            rebalance=new RebalanceDataAnalysisDataItem{
                                units=rdm.Next(),
                                value=RandomWeighting(),
                                profitAndLoss=RandomWeighting()                            
                            },
                            advantageousAndDisadvantageous=new RebalanceDataAnalysisAdvantageDisadvantage{
                                differences=RandomWeighting(),
                                suitability=rdm.Next(),
                                target=RandomWeighting(),
                                differenceToTarget=RandomWeighting(),
                                apl="apl"
                            }
                        },new RebalanceDataAnalysisData{
                            ticker="Ticker 3",
                            name="Name",
                            currentPrice=RandomWeighting(),
                            currentExposure=new RebalanceDataAnalysisDataItem{
                                units=rdm.Next(),
                                value=RandomWeighting(),
                                profitAndLoss=RandomWeighting()
                            },
                            rebalance=new RebalanceDataAnalysisDataItem{
                                units=rdm.Next(),
                                value=RandomWeighting(),
                                profitAndLoss=RandomWeighting()                            
                            },
                            advantageousAndDisadvantageous=new RebalanceDataAnalysisAdvantageDisadvantage{
                                differences=RandomWeighting(),
                                suitability=rdm.Next(),
                                target=RandomWeighting(),
                                differenceToTarget=RandomWeighting(),
                                apl="apl"
                            }
                        },
                    }
                }
            };
        }
        private RegionalComparisonModel GetRegionalComparisonModel(string modelId)
        {
            return new RegionalComparisonModel
            {
                data = new List<RegionalComparisonData>
                {
                    new RegionalComparisonData{
                        groupName="Group 1",
                        current=RandomWeighting(),
                        currentWeighting=RandomWeighting(),
                        proposed=RandomWeighting(),
                        proposedWeighting=RandomWeighting(),
                        difference=RandomWeighting(),
                        items=new List<RegionalComparisonItem>{
                            new RegionalComparisonItem{
                                itemName="Item 1",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            },new RegionalComparisonItem{
                                itemName="Item 2",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            },new RegionalComparisonItem{
                                itemName="Item 3",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            },new RegionalComparisonItem{
                                itemName="Item 4",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            }
                        }
                    },new RegionalComparisonData{
                        groupName="Group 1",
                        current=RandomWeighting(),
                        currentWeighting=RandomWeighting(),
                        proposed=RandomWeighting(),
                        proposedWeighting=RandomWeighting(),
                        difference=RandomWeighting(),
                        items=new List<RegionalComparisonItem>{
                            new RegionalComparisonItem{
                                itemName="Item 1",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            },new RegionalComparisonItem{
                                itemName="Item 2",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            },new RegionalComparisonItem{
                                itemName="Item 3",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            },new RegionalComparisonItem{
                                itemName="Item 4",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            }
                        }
                    },new RegionalComparisonData{
                        groupName="Group 1",
                        current=RandomWeighting(),
                        currentWeighting=RandomWeighting(),
                        proposed=RandomWeighting(),
                        proposedWeighting=RandomWeighting(),
                        difference=RandomWeighting(),
                        items=new List<RegionalComparisonItem>{
                            new RegionalComparisonItem{
                                itemName="Item 1",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            },new RegionalComparisonItem{
                                itemName="Item 2",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            },new RegionalComparisonItem{
                                itemName="Item 3",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            },new RegionalComparisonItem{
                                itemName="Item 4",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            }
                        }
                    }
                },
                current = RandomWeighting(),
                currentWeighting = RandomWeighting(),
                proposed = RandomWeighting(),
                proposedWeighting = RandomWeighting(),
                difference = RandomWeighting()
            };
        }
        private SectorialComparisonModel GetSectorialComparisonModel(string modelId)
        {
            return new SectorialComparisonModel
            {
                current = RandomWeighting(),
                currentWeighting = RandomWeighting(),
                proposed = RandomWeighting(),
                proposedWeighting = RandomWeighting(),
                difference = RandomWeighting(),
                data = new List<SectorialComparisonData>
                {
                    new SectorialComparisonData{
                        groupName="Group 1",
                        current=RandomWeighting(),
                        currentWeighting=RandomWeighting(),
                        proposed=RandomWeighting(),
                        proposedWeighting=RandomWeighting(),
                        difference=RandomWeighting(),
                        items=new List<SectorialComparisonItem>{
                            new SectorialComparisonItem{
                                itemName="Item 1",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            },
                             new SectorialComparisonItem{
                                itemName="Item 2",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            },
                             new SectorialComparisonItem{
                                itemName="Item 3",
                                current=RandomWeighting(),
                                currentWeighting=RandomWeighting(),
                                proposed=RandomWeighting(),
                                proposedWeighting=RandomWeighting(),
                                difference=RandomWeighting()
                            },
                        }
                    }
                }
            };
        }
        private MonthlyCashflowComparison GetmonthlyCashflowComparisonModel(string modelId)
        {
            return new MonthlyCashflowComparison
            {
                current = RandomWeighting(),
                proposed = RandomWeighting(),
                difference = RandomWeighting(),
                data = new List<MonthlyCashflowComparisonData>
                {
                    new MonthlyCashflowComparisonData{
                        groupName="Group 1",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting(),
                        items=new List<MonthlyCashflowComparisonItem>{
                            new MonthlyCashflowComparisonItem{
                                itemName="Item 1",
                                current=RandomWeighting(),
                                proposed=RandomWeighting(),
                                difference=RandomWeighting()
                            },
                            new MonthlyCashflowComparisonItem{
                                itemName="Item 2",
                                current=RandomWeighting(),
                                proposed=RandomWeighting(),
                                difference=RandomWeighting()
                            },
                            new MonthlyCashflowComparisonItem{
                                itemName="Item 3",
                                current=RandomWeighting(),
                                proposed=RandomWeighting(),
                                difference=RandomWeighting()
                            },
                        }
                    },
                    new MonthlyCashflowComparisonData{
                        groupName="Group 2",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting(),
                        items=new List<MonthlyCashflowComparisonItem>{
                            new MonthlyCashflowComparisonItem{
                                itemName="Item 1",
                                current=RandomWeighting(),
                                proposed=RandomWeighting(),
                                difference=RandomWeighting()
                            },
                            new MonthlyCashflowComparisonItem{
                                itemName="Item 2",
                                current=RandomWeighting(),
                                proposed=RandomWeighting(),
                                difference=RandomWeighting()
                            },
                            new MonthlyCashflowComparisonItem{
                                itemName="Item 3",
                                current=RandomWeighting(),
                                proposed=RandomWeighting(),
                                difference=RandomWeighting()
                            },
                        }
                    }
                }
            };
        }
        private MonthlyInvestmentIncomeModel GetMonthlyInvestmentIncome(string modelId)
        {
            return new MonthlyInvestmentIncomeModel
              {
                  current = RandomWeighting(),
                  proposed = RandomWeighting(),
                  difference = RandomWeighting(),
                  data = new List<MonthlyInvestmentIncomeData>{
                    new MonthlyInvestmentIncomeData{
                        name="Name 1",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                    new MonthlyInvestmentIncomeData{
                        name="Name 2",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                    new MonthlyInvestmentIncomeData{
                        name="Name 3",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                }
              };
        }
        private AnticipatedCostModel GetAnticipatedFirstYearmanagementCost(string modelId)
        {
            return new AnticipatedCostModel
            {
                current = RandomWeighting(),
                proposed = RandomWeighting(),
                difference = RandomWeighting(),
                data = new List<AnticipatedCostData>
                {
                    new AnticipatedCostData{
                        name="Name 1",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                     new AnticipatedCostData{
                        name="Name 2",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                     new AnticipatedCostData{
                        name="Name 3",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                     new AnticipatedCostData{
                        name="Name 4",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                }
            };
        }
        private AnticipatedCostModel GetAnticipatedSecondYearmanagementCost(string modelId)
        {
            return new AnticipatedCostModel
            {
                current = RandomWeighting(),
                proposed = RandomWeighting(),
                difference = RandomWeighting(),
                data = new List<AnticipatedCostData>
                {
                    new AnticipatedCostData{
                        name="Name 1",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                     new AnticipatedCostData{
                        name="Name 2",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                     new AnticipatedCostData{
                        name="Name 3",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                     new AnticipatedCostData{
                        name="Name 4",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                }
            };
        }
        private AnticipatedCostModel GetAnticipatedFirstYearOngoingServiceFee(string modelId)
        {
            return new AnticipatedCostModel
            {
                current = RandomWeighting(),
                proposed = RandomWeighting(),
                difference = RandomWeighting(),
                data = new List<AnticipatedCostData>
                {
                    new AnticipatedCostData{
                        name="Name 1",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                     new AnticipatedCostData{
                        name="Name 2",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                     new AnticipatedCostData{
                        name="Name 3",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                     new AnticipatedCostData{
                        name="Name 4",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                }
            };
        }
        private AnticipatedCostModel GetAnticipatedSecondYearOngoingServiceFee(string modelId)
        {
            return new AnticipatedCostModel
            {
                current = RandomWeighting(),
                proposed = RandomWeighting(),
                difference = RandomWeighting(),
                data = new List<AnticipatedCostData>
                {
                    new AnticipatedCostData{
                        name="Name 1",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                     new AnticipatedCostData{
                        name="Name 2",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                     new AnticipatedCostData{
                        name="Name 3",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                     new AnticipatedCostData{
                        name="Name 4",
                        current=RandomWeighting(),
                        proposed=RandomWeighting(),
                        difference=RandomWeighting()
                    },
                }
            };
        }
        private List<TransactionCostData> GetTransactionCost(string modelId)
        {
            return new List<TransactionCostData>
            {
                new TransactionCostData{
                    assetClass="Asset Class",
                    buySell=RandomWeighting(),
                    profitLoss=RandomWeighting(),
                    transactionCost=RandomWeighting(),
                    extraDividend=RandomWeighting(),
                    extraMER=RandomWeighting(),
                    netValue=RandomWeighting(),
                    items=new List<TransactionCostDataItem>{
                        new TransactionCostDataItem{
                             buySell=RandomWeighting(),
                             profitLoss=RandomWeighting(),
                             transactionCost=RandomWeighting(),
                             extraDividend=RandomWeighting(),
                             extraMER=RandomWeighting(),
                             netValue=RandomWeighting(),
                             name="Name 1"
                        },new TransactionCostDataItem{
                             buySell=RandomWeighting(),
                             profitLoss=RandomWeighting(),
                             transactionCost=RandomWeighting(),
                             extraDividend=RandomWeighting(),
                             extraMER=RandomWeighting(),
                             netValue=RandomWeighting(),
                             name="Name 2"
                        },new TransactionCostDataItem{
                             buySell=RandomWeighting(),
                             profitLoss=RandomWeighting(),
                             transactionCost=RandomWeighting(),
                             extraDividend=RandomWeighting(),
                             extraMER=RandomWeighting(),
                             netValue=RandomWeighting(),
                             name="Name 3"
                        },
                    }
                },new TransactionCostData{
                    assetClass="Asset Class 2",
                    buySell=RandomWeighting(),
                    profitLoss=RandomWeighting(),
                    transactionCost=RandomWeighting(),
                    extraDividend=RandomWeighting(),
                    extraMER=RandomWeighting(),
                    netValue=RandomWeighting(),
                    items=new List<TransactionCostDataItem>{
                        new TransactionCostDataItem{
                             buySell=RandomWeighting(),
                             profitLoss=RandomWeighting(),
                             transactionCost=RandomWeighting(),
                             extraDividend=RandomWeighting(),
                             extraMER=RandomWeighting(),
                             netValue=RandomWeighting(),
                             name="Name 1"
                        },new TransactionCostDataItem{
                             buySell=RandomWeighting(),
                             profitLoss=RandomWeighting(),
                             transactionCost=RandomWeighting(),
                             extraDividend=RandomWeighting(),
                             extraMER=RandomWeighting(),
                             netValue=RandomWeighting(),
                             name="Name 2"
                        },new TransactionCostDataItem{
                             buySell=RandomWeighting(),
                             profitLoss=RandomWeighting(),
                             transactionCost=RandomWeighting(),
                             extraDividend=RandomWeighting(),
                             extraMER=RandomWeighting(),
                             netValue=RandomWeighting(),
                             name="Name 3"
                        },
                    }
                }
            };
        }
        private List<ComplianceData> GetComplianceData(string modelId)
        {
            return new List<ComplianceData>
            {
                new ComplianceData{
                    groupName="Group 1",
                    items=new List<ComplianceDataItem>{
                        new ComplianceDataItem{
                            name="Name 1",
                            apl="apl",
                            suitability=RandomWeighting(),
                            compliant="compliant"
                        },
                        new ComplianceDataItem{
                            name="Name 2",
                            apl="apl",
                            suitability=RandomWeighting(),
                            compliant="compliant"
                        },
                        new ComplianceDataItem{
                            name="Name 3",
                            apl="apl",
                            suitability=RandomWeighting(),
                            compliant="compliant"
                        },
                    }
                },

            };
        }
        private TemplateDetails GetTemplateDetails(string modelId)
        {
            return new TemplateDetails
            {
                templateId = "001",
                templateName = "Template 1",
                correspondingProfile = new TemplateDetailsCorrespondingProfile
                {
                    profileId = "P001",
                    profileName = "Profile Name 1"
                },
                itemParameters = new List<TemplateDetailsItemParameter>
                {
                    new TemplateDetailsItemParameter{
                        groupId=new List<string>{"asset00", "asset01", "europe00", "europe01"},
                        itemName="Asset Item, in Europe",
                        id="T03",
                        marketValue=RandomWeighting(),
                        currentValue=RandomWeighting(),
                        currentWeighting=RandomWeighting(),
                        identityMetaKey=new List<string>{"STOCK"}
                    }, new TemplateDetailsItemParameter{
                        groupId=new List<string>{"asset00", "asset01", "africa01", "africa00"},
                        itemName="Asset Item 2",
                        id="T02",
                        marketValue=RandomWeighting(),
                        currentValue=RandomWeighting(),
                        currentWeighting=RandomWeighting(),
                        identityMetaKey=new List<string>{"STOCK"}
                    }
                }

            };
        }
        #endregion


        private double RandomWeighting()
        {
            return Math.Round(rdm.NextDouble() * 100, 2);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}