using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDISAngular.Infrastructure.DbFirst;
using EDISAngular.Models.ServiceModels.PortfolioModels;
using EDISAngular.Models.ServiceModels;
using System.Runtime.InteropServices;
using EDISAngular.Services;

namespace EDISAngular.Infrastructure.DatabaseAccess
{
    //public class DBWrapper : IDisposable
    //{
    //    private edisDbEntities db = null;

    //    public DBWrapper()
    //    {
    //        this.db = new edisDbEntities();
    //    }

    //    public IEnumerable<PortfolioQuickStatsModel> GetPortfolio_QuickStats(string adviserUserId, int AssetTypeID)
    //    {
    //        #warning not correct
    //        var result = db.usp_calc_portfolio_averages().ToList();

    //        List<PortfolioQuickStatsModel> lstQuickStat = new List<PortfolioQuickStatsModel>(){

    //                    new PortfolioQuickStatsModel {
    //                        name="Average Earnings Per Share",
    //                        value=result.Select(x=>x.Average_Earning_Per_Share).SingleOrDefault().Value.ToString()
    //                    },
    //                    new PortfolioQuickStatsModel {
    //                        name="Average Earnings Per Share Growth",
    //                        value=result.Select(x=>x.Average_Earning_Per_Share_Growth).SingleOrDefault().Value.ToString()
    //                    },
    //                    new PortfolioQuickStatsModel {
    //                        name="Average Beta",
    //                        value=result.Select(x=>x.Average_Beta).SingleOrDefault().Value.ToString()
    //                    },
    //                    new PortfolioQuickStatsModel {
    //                        name="Average Current Ratio",
    //                        value=result.Select(x=>x.Average_Current_Ratio).SingleOrDefault().Value.ToString()
    //                    }
    //            };
    //        return lstQuickStat;


    //    }

    //    public SummaryGeneralInfo GetCostValueMarketValue(string adviserUserId, int AssetTypeID)
    //    {

    //        var result = db.usp_calc_cost_market_value_by_assettype_advisorid(AssetTypeID, adviserUserId).ToList();
    //        var costValue = double.Parse(result.Sum(x => x.costvalue).ToString());
    //        var marketValue = double.Parse(result.Sum(x => x.marketvalue).ToString());
    //        var profit = marketValue - costValue;

    //        return new SummaryGeneralInfo
    //        {
    //            cost = costValue,
    //            marketValue = marketValue,
    //            pl = profit,
    //            plp = (profit / costValue) * 100
    //        };

    //    }

    //    public double RandomMoney()
    //    {

    //        return Math.Round((new Random().NextDouble() * 100000), 2);
    //    }

    //    public EquityCompanyProfileModel GetCompanyProfiles_Advisor(string adviserUserId, int AssetTypeID)
    //    {

    //        var portfoliostat = db.usp_calc_portfolio_statistics(AssetTypeID).ToList();
    //        var portfolioratio = db.Usp_calc_get_portfolio_ratio(AssetTypeID).ToList();
    //        var portfolioriskreturn = db.Usp_calc_get_portfolio_risk_return(AssetTypeID).ToList();


    //        List<EquityCompanyProfileItemModel> lstCompanyProfileModel = new List<EquityCompanyProfileItemModel>();

    //        foreach (usp_calc_portfolio_statistics_Result item in portfoliostat)
    //        {
    //            EquityCompanyProfileItemModel objModel = new EquityCompanyProfileItemModel
    //            {
    //                asx = item.ticker,
    //                company = item.institute,
    //                costValue = double.Parse(item.CostValue.Value.ToString()),
    //                brokerage = double.Parse(item.Brokerage.Value.ToString()),
    //                totalCostValue = double.Parse(item.TotalCostValue.Value.ToString()),
    //                totalCostValueWeighting = double.Parse(item.TotalCostValueWeighting.Value.ToString()),
    //                marketPrice = double.Parse(item.MarketPrice.Value.ToString()),
    //                marketValue = double.Parse(item.MarketValue.Value.ToString()),
    //                marketValueWeighting = double.Parse(item.MarketValueWeighting.Value.ToString()),
    //                marketValueAssetAllocationWeighting = RandomMoney(),
    //                companySuitabilityToInvestor = RandomMoney(),
    //                earningsPerShare = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Earnings_Per_Share.Value),
    //                earningsPerShareWeighting = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Earnings_Per_Share_Weighting.Value),
    //                earningsPerShareGrowth = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Earnings_Per_Share_Growth.Value),
    //                earningsPerShareGrowthWeighting = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Earnings_Per_Share_Growth_Weighting.Value),
    //                dividend = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Dividend.Value),
    //                dividendWeighting = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Dividend_Weighting.Value),
    //                franking = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Franking.Value),
    //                frankingWeighting = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Franking_Weighting.Value),
    //                dividendYield = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Dividend_Yield.Value),
    //                dividendYieldWeighting = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Dividend_Yield_Weighting.Value),
    //                priceEarningsRatio = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Price_Earnings_Ratio.Value),
    //                priceEarningsRatioWeighting = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Price_Earnings_Ratio_Weightage.Value),
    //                returnOnAsset = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Return_on_Asset.Value),
    //                returnOnAssetWeighting = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Return_on_Asset_Weighting.Value),
    //                returnOnEquity = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Return_on_Equity.Value),
    //                returnOnEquityWeighting = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Return_on_Equity_Weighting.Value),
    //                oneYearReturn = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().C1_Year_Return.Value),
    //                oneYearReturnWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().C1_Year_Return_Weighting.Value),
    //                threeYearReturn = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().C3_Year_Return.Value),
    //                threeYearReturnWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().C3_Year_Return_Weighting.Value),
    //                fiveYearReturn = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().C5_Year_Return.Value),
    //                fiveYearReturnWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().C5_Year_Return_Weighting.Value),
    //                beta = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Beta.Value),
    //                betaWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Beta_Weighting.Value),
    //                currentRatio = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Current_Ratio.Value),
    //                currentRatioWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Current_Ratio_Weighting.Value),
    //                quickRatio = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Quick_Ratio.Value),
    //                quickRatioWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Quick_Ratio_Weightage.Value),
    //                debtEquityRatio = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Debt_Equity_Ratio.Value),
    //                debtEquityRatioWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Debt_Equity_Ratio_Weighting.Value),
    //                interestCover = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Interest_Cover.Value),
    //                interestCoverWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Interest_Cover_Weighting.Value),
    //                payoutRatio = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Payout_Ratio.Value),
    //                payoutRatioWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Payout_Ratio_Weighting.Value),
    //                earningsStability = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Earnings_Stability.Value),
    //                earningsStabilityWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Earnings_Stability_Weighting.Value),
    //            };
    //            lstCompanyProfileModel.Add(objModel);
    //        }

    //        return new EquityCompanyProfileModel
    //        {
    //            data = lstCompanyProfileModel
    //        };


    //    }

    //    public CashflowBriefModel GetCashflowSummary_Advisor(string adviserUserId, int AssetTypeId)
    //    {

    //        var currentYear = CurrentYear().ToString();
    //        var cashflow = db.usp_calc_get_cashflow().ToList();

    //        var months = cashflow.GroupBy(x => x.Month).Select(x => x.First()).OrderBy(x => x.Month).ToList();

    //        List<CashFlowBriefItem> lstCashflow = new List<CashFlowBriefItem>();
    //        foreach (var month in months)
    //        {
    //            lstCashflow.Add(new CashFlowBriefItem
    //            {
    //                expense = cashflow.Where(x => x.Month == month.Month && x.CashFlowType == "Expense" && x.assettypeid == AssetTypeId).Any() ?
    //                    cashflow.SingleOrDefault(x => x.Month == month.Month && x.CashFlowType == "Expense" && x.assettypeid == AssetTypeId).TotalAmount.HasValue ?
    //                    cashflow.SingleOrDefault(x => x.Month == month.Month && x.CashFlowType == "Expense" && x.assettypeid == AssetTypeId).TotalAmount.Value : 0
    //                    : 0,
    //                income = cashflow.Where(x => x.Month == month.Month && x.CashFlowType == "Income" && x.assettypeid == AssetTypeId).Any() ?
    //                    cashflow.SingleOrDefault(x => x.Month == month.Month && x.CashFlowType == "Income" && x.assettypeid == AssetTypeId).TotalAmount.HasValue ?
    //                    cashflow.SingleOrDefault(x => x.Month == month.Month && x.CashFlowType == "Income" && x.assettypeid == AssetTypeId).TotalAmount.Value
    //                    : 0
    //                    : 0,
    //                month = new DateTime(int.Parse(currentYear), int.Parse(month.Month), 1).ToString("MMM"),
    //                date = new DateTime(int.Parse(currentYear), int.Parse(month.Month), 1)
    //            });
    //        }

    //        return new CashflowBriefModel
    //        {
    //            data = lstCashflow,
    //            totalExpense = lstCashflow.Sum(x => x.expense),
    //            totalIncome = lstCashflow.Sum(x => x.income)
    //        };



    //    }



    //    public PortfolioRatingModel GetPortfolioRating_Adviser(string adviserUserId, int AssetTypeId)
    //    {

    //        var historicalscores = db.usp_calc_suitability_historical_AE(AssetTypeId).ToList();
    //        var recommendedscores = db.usp_calc_suitability_recommended_AE(AssetTypeId).ToList();

    //        var suitability = db.Usp_calc_getportfoliosuitability_for_asset(AssetTypeId).SingleOrDefault();

    //        return new PortfolioRatingModel
    //        {
    //            data = new List<PortfolioRatingItemModel>
    //            {
    //                new PortfolioRatingItemModel{name="EPS Growth F0", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.EPSGrowthF0))},
    //                new PortfolioRatingItemModel{name="Market Capitalization", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.MarketCapitalization))},
    //                new PortfolioRatingItemModel{name="Div Yield F0", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.DivYieldF0))},
    //                new PortfolioRatingItemModel{name="Frank F0", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.FrankF0))},
    //                new PortfolioRatingItemModel{name="ROA F0", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.ROAF0))},
    //                  new PortfolioRatingItemModel{name="ROE F0", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.ROEF0))},
    //                  new PortfolioRatingItemModel{name="Int Cover F0", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.IntCoverF0))},
    //                  new PortfolioRatingItemModel{name="Debt Equity F0", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.DebtEquityF0))},
    //                  new PortfolioRatingItemModel{name="PE F0", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.PEF0))},
    //                  new PortfolioRatingItemModel{name="Beta 5 F0", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.Beta5))},

    //                  new PortfolioRatingItemModel{name="EPS Growth F1", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.EPSGrowthF1))},
    //                new PortfolioRatingItemModel{name="MorningStar Reco", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.MorningStarRecommendation))},
    //                new PortfolioRatingItemModel{name="Div Yield F1", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.DivYieldF1))},
    //                new PortfolioRatingItemModel{name="Frank F1", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.FrankF1))},
    //                new PortfolioRatingItemModel{name="ROA F1", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.ROAF1))},
    //                  new PortfolioRatingItemModel{name="ROE F1", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.ROEF1))},
    //                  new PortfolioRatingItemModel{name="Int Cover F1", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.IntCoverF1))},
    //                  new PortfolioRatingItemModel{name="Debt Equity F1", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.DebtEquityF1))},
    //                  new PortfolioRatingItemModel{name="PE F1", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.PEF1))},
    //                  new PortfolioRatingItemModel{name="IVV", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.IVV))},

                      

    //            },
    //            suitability = Convert.ToDouble(suitability.WeightedForEquity),
    //            notSuited = 0,
    //            risk = 0,
    //            SuitabilityDesc = suitability.PortfolioSuitability
    //        };

    //    }

    //    public PortfolioRatingModel DirectProperty_GetPortfolioRating_Adviser(string adviserUserId, int AssetTypeId)
    //    {

    //        var historicalscores = db.usp_calc_suitability_historical_DP(AssetTypeId).ToList();            

    //        var suitability = db.Usp_calc_getportfoliosuitability_for_DP(AssetTypeId).SingleOrDefault();

    //        return new PortfolioRatingModel
    //        {
    //            data = new List<PortfolioRatingItemModel>
    //            {
    //                new PortfolioRatingItemModel{name="5 Year Total Ret.", weighting=Convert.ToDouble(historicalscores.Sum(x=>x.FiveYearTotalReturn)), score=Convert.ToDouble(historicalscores.Sum(x=>x.FiveYearTotalReturn))},
    //                new PortfolioRatingItemModel{name="5 Year Alpha", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.FiveYearAlpha))},
    //                new PortfolioRatingItemModel{name="5 Year Beta", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.FiveYearBeta))},
    //                new PortfolioRatingItemModel{name="5 Year Information", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.FiveYearInformationRatio))},
    //                new PortfolioRatingItemModel{name="5 Year SD", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.FiveYearSD))},
    //                  new PortfolioRatingItemModel{name="5 Year Skewness", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.FiveYearSkewnessRatio))},
    //                  new PortfolioRatingItemModel{name="5 Year Tracking Error", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.FiveYearTER))},
    //                  new PortfolioRatingItemModel{name="5 Year Sharp", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.FiveYearSR))},
    //                  new PortfolioRatingItemModel{name="Global Category", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.GlobalCategory))},
    //                  new PortfolioRatingItemModel{name="Fund Size", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.FundSize))},

                   
                      

    //            },
    //            suitability = Convert.ToDouble(suitability.WeightedForEquity),
    //            notSuited = 0,
    //            risk = 0,
    //            SuitabilityDesc = suitability.PortfolioSuitability
    //        };

    //    }

    //    public CashflowBriefModel DirectProperty_GetCashflowSummary_Advisor(string adviserUserId)
    //    {

    //        var currentYear = CurrentYear().ToString();
    //        var cashflow = db.vw_cashflow_DP.ToList();

    //        var months = cashflow.GroupBy(x => x.Month).Select(x => x.First()).OrderBy(x => x.Month).ToList();

    //        List<CashFlowBriefItem> lstCashflow = new List<CashFlowBriefItem>();
    //        foreach (var month in months)
    //        {
    //            lstCashflow.Add(new CashFlowBriefItem
    //            {
    //                expense = cashflow.Where(x => x.Month == month.Month && x.CashFlowType == "Expense" ).Any() ?
    //                    cashflow.SingleOrDefault(x => x.Month == month.Month && x.CashFlowType == "Expense" ).TotalAmount.HasValue ?
    //                    cashflow.SingleOrDefault(x => x.Month == month.Month && x.CashFlowType == "Expense" ).TotalAmount.Value : 0
    //                    : 0,
    //                income = cashflow.Where(x => x.Month == month.Month && x.CashFlowType == "Income").Any() ?
    //                    cashflow.SingleOrDefault(x => x.Month == month.Month && x.CashFlowType == "Income" ).TotalAmount.HasValue ?
    //                    cashflow.SingleOrDefault(x => x.Month == month.Month && x.CashFlowType == "Income" ).TotalAmount.Value
    //                    : 0
    //                    : 0,
    //                month = new DateTime(int.Parse(currentYear), int.Parse(month.Month), 1).ToString("MMM"),
    //                date = new DateTime(int.Parse(currentYear), int.Parse(month.Month), 1)
    //            });
    //        }

    //        return new CashflowBriefModel
    //        {
    //            data = lstCashflow,
    //            totalExpense = lstCashflow.Sum(x => x.expense),
    //            totalIncome = lstCashflow.Sum(x => x.income)
    //        };



    //    }

    //    #region fixed income
    //    public SummaryGeneralInfoFCL FixedIncomeGetGeneral(string adviserUserId, int AssetTypeID)
    //    {

    //        var result = db.usp_calc_cost_market_value_by_assettype_advisorid(AssetTypeID, adviserUserId).ToList();

    //        var costValue = double.Parse(result.Sum(x => x.costvalue).ToString());
    //        var marketValue = double.Parse(result.Sum(x => x.marketvalue).ToString());
    //        var profit = marketValue - costValue;
    //        var facevalue = db.usp_calc_face_present_value_fixedincome().Average(x=>x.FaceValue);
    //        var presentvalue = db.usp_calc_face_present_value_fixedincome().Average(x => x.PresentValue);

    //        return new SummaryGeneralInfoFCL
    //        {
    //            cost = costValue,
    //            marketValue = marketValue,
    //            pl = profit,
    //            plp = (profit / costValue) * 100,
    //            faceValue = Convert.ToDouble(facevalue),
    //            aveCoupon = Convert.ToDouble(presentvalue)
    //        };

    //    }

    //    public CashflowBriefModel FixedIncome_GetCashflowSummary_Advisor(string adviserUserId, int AssetTypeId)
    //    {

    //        var currentYear = CurrentYear().ToString();
    //        var cashflow = db.usp_calc_get_cashflow_by_asset(AssetTypeId).ToList();

    //        var months = cashflow.GroupBy(x => x.Month).Select(x => x.First()).OrderBy(x => x.Month).ToList();

    //        List<CashFlowBriefItem> lstCashflow = new List<CashFlowBriefItem>();
    //        foreach (var month in months)
    //        {
    //            lstCashflow.Add(new CashFlowBriefItem
    //            {
    //                expense = cashflow.Where(x => x.Month == month.Month && x.CashFlowType == "Expense" ).Any() ?
    //                    cashflow.SingleOrDefault(x => x.Month == month.Month && x.CashFlowType == "Expense" ).TotalAmount.HasValue ?
    //                    cashflow.SingleOrDefault(x => x.Month == month.Month && x.CashFlowType == "Expense" ).TotalAmount.Value : 0
    //                    : 0,
    //                income = cashflow.Where(x => x.Month == month.Month && x.CashFlowType == "Income" ).Any() ?
    //                    cashflow.SingleOrDefault(x => x.Month == month.Month && x.CashFlowType == "Income" ).TotalAmount.HasValue ?
    //                    cashflow.SingleOrDefault(x => x.Month == month.Month && x.CashFlowType == "Income" ).TotalAmount.Value
    //                    : 0
    //                    : 0,
    //                month = new DateTime(int.Parse(currentYear), int.Parse(month.Month), 1).ToString("MMM"),
    //                date = new DateTime(int.Parse(currentYear), int.Parse(month.Month), 1)
    //            });
    //        }

    //        return new CashflowBriefModel
    //        {
    //            data = lstCashflow,
    //            totalExpense = lstCashflow.Sum(x => x.expense),
    //            totalIncome = lstCashflow.Sum(x => x.income)
    //        };



    //    }
    //    #endregion


    //    public ManagedInvestmentCompanyProfileModel ManagedInvestment_GetCompanyProfiles_Advisor(string adviserUserId, int AssetTypeID)
    //    {

    //        var portfoliostat = db.usp_calc_portfolio_statistics(AssetTypeID).ToList();
    //        var portfolioratio = db.Usp_calc_get_portfolio_ratio(AssetTypeID).ToList();
    //        var portfolioriskreturn = db.Usp_calc_get_portfolio_risk_return(AssetTypeID).ToList();


    //        List<ManagedInvestmentCompanyProfileItem> lstCompanyProfileModel = new List<ManagedInvestmentCompanyProfileItem>();

    //        foreach (usp_calc_portfolio_statistics_Result item in portfoliostat)
    //        {
    //            ManagedInvestmentCompanyProfileItem objModel = new ManagedInvestmentCompanyProfileItem
    //            {
    //                ticker = item.ticker,
    //                company = item.institute,
    //                costValue = double.Parse(item.CostValue.Value.ToString()),
    //                brokerage = double.Parse(item.Brokerage.Value.ToString()),
    //                totalCostValue = double.Parse(item.TotalCostValue.Value.ToString()),
    //                totalCostValueWeighting = double.Parse(item.TotalCostValueWeighting.Value.ToString()),
    //                marketPrice = double.Parse(item.MarketPrice.Value.ToString()),
    //                marketValue = double.Parse(item.MarketValue.Value.ToString()),
    //                marketValueWeighting = double.Parse(item.MarketValueWeighting.Value.ToString()),
    //                marketValueAssetAllocationWeighting = RandomMoney(),
    //                companySuitabilityToInvestor = RandomMoney(),
    //                earningsPerShare = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Earnings_Per_Share.Value),
    //                earningsPerShareWeighting = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Earnings_Per_Share_Weighting.Value),
    //                earningsPerShareGrowth = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Earnings_Per_Share_Growth.Value),
    //                earningsPerShareGrowthWeighting = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Earnings_Per_Share_Growth_Weighting.Value),
    //                dividend = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Dividend.Value),
    //                dividendWeighting = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Dividend_Weighting.Value),
    //                franking = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Franking.Value),
    //                frankingWeighting = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Franking_Weighting.Value),
    //                dividendYield = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Dividend_Yield.Value),
    //                dividendYieldWeighting = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Dividend_Yield_Weighting.Value),
    //                priceEarningsRatio = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Price_Earnings_Ratio.Value),
    //                priceEarningsRatioWeighting = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Price_Earnings_Ratio_Weightage.Value),
    //                returnOnAsset = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Return_on_Asset.Value),
    //                returnOnAssetWeighting = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Return_on_Asset_Weighting.Value),
    //                returnOnEquity = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Return_on_Equity.Value),
    //                returnOnEquityWeighting = Convert.ToDouble(portfolioratio.Where(x => x.ticker == item.ticker).SingleOrDefault().Return_on_Equity_Weighting.Value),
    //                oneYearReturn = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().C1_Year_Return.Value),
    //                oneYearReturnWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().C1_Year_Return_Weighting.Value),
    //                threeYearReturn = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().C3_Year_Return.Value),
    //                threeYearReturnWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().C3_Year_Return_Weighting.Value),
    //                fiveYearReturn = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().C5_Year_Return.Value),
    //                fiveYearReturnWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().C5_Year_Return_Weighting.Value),
    //                beta = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Beta.Value),
    //                betaWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Beta_Weighting.Value),
    //                currentRatio = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Current_Ratio.Value),
    //                currentRatioWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Current_Ratio_Weighting.Value),
    //                quickRatio = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Quick_Ratio.Value),
    //                quickRatioWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Quick_Ratio_Weightage.Value),
    //                debtEquityRatio = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Debt_Equity_Ratio.Value),
    //                debtEquityRatioWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Debt_Equity_Ratio_Weighting.Value),
    //                interestCover = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Interest_Cover.Value),
    //                interestCoverWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Interest_Cover_Weighting.Value),
    //                payoutRatio = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Payout_Ratio.Value),
    //                payoutRatioWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Payout_Ratio_Weighting.Value),
    //                earningsStability = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Earnings_Stability.Value),
    //                earningsStabilityWeighting = Convert.ToDouble(portfolioriskreturn.Where(x => x.ticker == item.ticker).SingleOrDefault().Earnings_Stability_Weighting.Value),
    //            };
    //            lstCompanyProfileModel.Add(objModel);
    //        }

    //        return new ManagedInvestmentCompanyProfileModel
    //        {
    //            data = lstCompanyProfileModel
    //        };


    //    }

    //    public PortfolioRatingModel ManagedInvestment_GetPortfolioRating_Adviser(string adviserUserId, int AssetTypeId)
    //    {

    //        var historicalscores = db.usp_calc_suitability_historical_MI(AssetTypeId).ToList();
    //        var recommendedscores = db.usp_calc_suitability_recommended_MI(AssetTypeId).ToList();

    //        var suitability = db.Usp_calc_getportfoliosuitability_for_MI(AssetTypeId).SingleOrDefault();

    //        return new PortfolioRatingModel
    //        {
    //            data = new List<PortfolioRatingItemModel>
    //            {
    //                new PortfolioRatingItemModel{name="5 Year Total Ret.", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.FiveYearTotalReturn))},
    //                new PortfolioRatingItemModel{name="5 Year Alpha", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.FiveYearAlpha))},
    //                new PortfolioRatingItemModel{name="5 Year Beta", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.FiveYearBeta))},
    //                new PortfolioRatingItemModel{name="5 Year Information", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.FiveYearInformationRatio))},
    //                new PortfolioRatingItemModel{name="5 Year SD", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.FiveYearSD))},
    //                  new PortfolioRatingItemModel{name="5 Year Skewness", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.FiveYearSkewnessRatio))},
    //                  new PortfolioRatingItemModel{name="5 Year Tracking Error", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.FiveYearTER))},
    //                  new PortfolioRatingItemModel{name="5 Year Sharp", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.FiveYearSR))},
    //                  new PortfolioRatingItemModel{name="Global Category", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.GlobalCategory))},
    //                  new PortfolioRatingItemModel{name="Fund Size", weighting=0, score=Convert.ToDouble(historicalscores.Sum(x=>x.FundSize))},

    //                  new PortfolioRatingItemModel{name="1 Year Total Return", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.OneYearTotalReturn))},
    //                new PortfolioRatingItemModel{name="MorningStar Reco", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.MorningStarRecommendation))},
    //                new PortfolioRatingItemModel{name="1 Year Alpha", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.OneYearAlpha))},
    //                new PortfolioRatingItemModel{name="1 Year Beta", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.OneYearBeta))},
    //                new PortfolioRatingItemModel{name="1 Year Information", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.OneYearInformationRatio))},
    //                  new PortfolioRatingItemModel{name="1 Year Tracking Error", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.OneYearTER))},
    //                  new PortfolioRatingItemModel{name="1 Year Sharp", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.OneYearSR))},
    //                  new PortfolioRatingItemModel{name="Max Management Expense", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.MaxManagementER))},
    //                  new PortfolioRatingItemModel{name="Performance Fee", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.PerformanceFee))},
    //                  new PortfolioRatingItemModel{name="Years Since Inception", weighting=0, score=Convert.ToDouble(recommendedscores.Sum(x=>x.YearsSinceInception))},

                      

    //            },
    //            suitability = Convert.ToDouble(suitability.WeightedForEquity),
    //            notSuited = 0,
    //            risk = 0,
    //            SuitabilityDesc = suitability.PortfolioSuitability
    //        };

    //    }

    //    public InvestmentPortfolioModel ManagedInvestment_GetAssetAllocation_Adviser(string adviserUserId, int AssetTypeID)
    //    {
    //        List<DataNameAmountPair> lstStat = new List<DataNameAmountPair>();
    //        db.usp_diversification_stat(AssetTypeID).ToList().ForEach(x=>{

    //            lstStat.Add(new DataNameAmountPair {
    //                 name=x.sector,
    //                 amount=Convert.ToDouble(x.TotalAmount)
    //            });
    //        });

    //        return new InvestmentPortfolioModel
    //        {
    //            data =lstStat,
    //            total = lstStat.Sum(x=>x.amount)
    //        };
    //    }


    //    public List<InsuranceListItemModel> Insurance_GetInsuranceList_Adviser(string adviserUserId)
    //    {
    //        List<InsuranceListItemDetailModel> lstInsurance = new List<InsuranceListItemDetailModel>();


    //        var result = db.Usp_insurance_list().ToList();
    //        foreach (Usp_insurance_list_Result insurance in result)
    //        {
    //            lstInsurance.Add(new InsuranceListItemDetailModel
    //            {
    //                type = insurance.sector,
    //                insurer = insurance.institute,
    //                typeOfPolicy = insurance.policytype,
    //                policyNumber = insurance.policyno,
    //                policyAddress = insurance.address,
    //                assetInsured = insurance.purposeinsured,
    //                amountInsured = double.Parse(insurance.amountinsured),
    //                premium = double.Parse(insurance.premium),
    //                premiumExpiry = DateTime.Parse(insurance.premiumexpiry),
    //                days = insurance.daysto
    //            });
    //        }

    //        return new List<InsuranceListItemModel>
    //            {
    //                new InsuranceListItemModel{
    //                    insuranceMetaType="Asset Insurance",
    //                    data=lstInsurance
    //                }
    //            };

    //    }

    //    public InsuranceStatisticsModel Insurance_GetStatistics_Adviser(string adviserUserId)
    //    {
    //        var result = db.Usp_insurance_list().ToList();
    //        return new InsuranceStatisticsModel
    //        {
    //            totalAnnualPremium = double.Parse(result.Sum(x => double.Parse(x.premium)).ToString()),
    //            totalNumberOfPolicies = result.Count()
    //        };
    //    }

    //    public IEnumerable<DataNameAmountPair> CreditCard_GetDiversification_Adviser(string adviserUserId)
    //    {

    //        var result = db.Usp_personal_loan_list().ToList();
    //        List<DataNameAmountPair> lstLoan = new List<DataNameAmountPair>();
    //        foreach (Usp_personal_loan_list_Result item in result)
    //        {
    //            lstLoan.Add(new DataNameAmountPair
    //            {
    //                name = item.sector,
    //                amount = Convert.ToDouble(item.LoanAmount)
    //            });
    //        }
    //        return lstLoan;

    //    }

    //    public IEnumerable<CreditCardDetailsModel> CreditCard_GetCardDetails_Adviser(string adviserUserId)
    //    {
    //        List<CreditCardDetailsModel> lstCreditCards = new List<CreditCardDetailsModel>();


    //        db.Usp_personal_loan_list_each_asset().ToList().ForEach(x =>
    //        {
    //            lstCreditCards.Add(new CreditCardDetailsModel
    //            {
    //                type = x.sector,
    //                issuer = x.institute,
    //                assetSecurity = x.assetsecurity,
    //                maximumCreditLimit = double.Parse(x.maximumcreditlimit),
    //                accountBalance = RandomMoney(),
    //                availableFunds = double.Parse(x.availablefunds),
    //                expiryDate = DateTime.Parse(x.expirydate),
    //                interestRate = double.Parse(x.interestrate),
    //                termOfLoan = double.Parse(x.termofloan)
    //            });


    //        });
    //        return lstCreditCards;


    //    }

    //    public CreditCardQuickStatsModel CreditCard_GetQuickStats_Adviser(string adviserUserId)
    //    {
    //        var result = db.Usp_personal_loan_list_each_asset().ToList();
    //        var totalBalance = result.Sum(x => double.Parse(x.accountbalance));
    //        var totalAvailableBalance = result.Sum(x => double.Parse(x.availablefunds));
    //        var availableCredit = totalAvailableBalance - totalBalance;

    //        return new CreditCardQuickStatsModel
    //        {
    //            totalAvailableFunds = totalBalance,
    //            totalAvailableShortTermCredit = availableCredit,
    //            totalOutstandingShortTermDebt = totalAvailableBalance
    //        };

    //    }

    //    public SummaryGeneralInfo DirectProperty_GetGeneral_Adviser(string adviserUserId)
    //    {
    //        var result = db.Usp_calc_summary_directproperty().ToList();

    //        var totalcost = result.Sum(x => x.CostValue);
    //        var totalMarketValue = result.Sum(x => x.MarketValue);
    //        var profitloss = totalMarketValue - totalcost;

    //        return new SummaryGeneralInfo
    //        {
    //            cost = Convert.ToDouble(totalcost),
    //            marketValue = Convert.ToDouble(totalMarketValue),
    //            pl = Convert.ToDouble(profitloss),
    //            plp = Convert.ToDouble((profitloss / totalcost) * 100)
    //        };
    //    }

    //    public DirectPropertyGeoModel DirectProperty_GetGeoInfo_Adviser(string adviserUserId)
    //    {
    //        var directproperty = db.vw_directproperty_list.ToList();
    //        List<DirectPropertyGeoItem> lstProperty = new List<DirectPropertyGeoItem>();
    //        foreach (var item in directproperty)
    //        {
    //            lstProperty.Add(new DirectPropertyGeoItem
    //            {
    //                id = directproperty.IndexOf(item).ToString(),
    //                address = item.Address,
    //                latitude = GoogleGeoService.GetCoordinatesLat(item.Address).Value,
    //                longitude = GoogleGeoService.GetCoordinatesLng(item.Address).Value,
    //                country = "",
    //                state = "",
    //                type = "Office",
    //                value = double.Parse(item.MarketValue.ToString())
    //            });
    //        }

    //        return new DirectPropertyGeoModel
    //       {
    //           data = lstProperty
    //       };
    //    }



    //    public MortgageInvestmentGeneralInfo Mortgate_GetGeneralInfo_Adviser(string adviserUserId)
    //    {

    //        var result = db.usp_calc_loan_quick_summary().ToList();
    //        var totalmarketvalue = result.Sum(x => x.MarketValue);
    //        var totalpaidloan = result.Sum(x => x.LoanPaid);
    //        var maxlimit = result.Sum(x => double.Parse(x.MaximumLoanLimit));
    //        var loanbalance = Convert.ToDecimal(maxlimit) - totalpaidloan;
    //        var avgloanterm = result.Average(x => double.Parse(x.LoanTerm));
    //        var gearingratio = totalpaidloan / totalmarketvalue;

    //        return new MortgageInvestmentGeneralInfo
    //        {
    //            annualInterestRepayment = RandomMoney(),
    //            aveTermOfLoans = avgloanterm,
    //            marketValue = Convert.ToDouble(totalmarketvalue),
    //            monthlyRepayment = RandomMoney(),
    //            outstandingLoans = Convert.ToDouble(loanbalance),
    //            propertyGearingRatio = Convert.ToDouble(gearingratio)
    //        };
    //    }

    //    public CashflowBriefModel Loan_GetCashflowSummary_Advisor(string adviserUserId)
    //    {

    //        var currentYear = CurrentYear().ToString();
    //        var result = db.usp_calc_cashflow_loan().ToList();

    //        var months = result.GroupBy(x => x.Month).Select(x => x.First()).OrderBy(x => x.Month).ToList();

    //        List<CashFlowBriefItem> lstCashflow = new List<CashFlowBriefItem>();
    //        foreach (var month in months)
    //        {
    //            lstCashflow.Add(new CashFlowBriefItem
    //            {
    //                expense = Convert.ToDouble(result.SingleOrDefault(x => x.Month == month.Month && x.CashFlowType == "Expense").TotalAmount.Value),
    //                month = new DateTime(int.Parse(currentYear), int.Parse(month.Month), 1).ToString("MMM"),
    //                date = new DateTime(int.Parse(currentYear), int.Parse(month.Month), 1)
    //            });
    //        }

    //        return new CashflowBriefModel
    //        {
    //            data = lstCashflow,
    //            totalExpense = lstCashflow.Sum(x => x.expense),
    //            totalIncome = lstCashflow.Sum(x => x.income)
    //        };



    //    }

    //    public MortgageInvestmentProfileModel Mortgage_GetProfiles_Adviser(string adviserUserId)
    //    {
    //        List<MortgageInvestmentProfileItem> lstProfile = new List<MortgageInvestmentProfileItem>();

    //        db.usp_calc_loan_details().ToList().ForEach(x =>
    //        {
    //            lstProfile.Add(
    //                new MortgageInvestmentProfileItem
    //                {
    //                    propertyName = x.PropertyName,
    //                    address = x.PropertyAddress,
    //                    currency = x.Currency,
    //                    marketValue = Convert.ToDouble(x.MarketValue.Value),
    //                    outstandingLoan = Convert.ToDouble(decimal.Parse(x.maximumloanlimit) - x.LoanPaid),
    //                    currentPropertyGearingRatio = 0,
    //                    institution = x.institute,
    //                    typeOfRates = x.typeofrate,
    //                    interestRates = double.Parse(x.interestrate),
    //                    monthlyRepaymentAmount = double.Parse(x.monthlyrepayment),                        
    //                    loanContractTerm = double.Parse(x.loancontractterm),
    //                    startDate = DateTime.Parse(x.loanstartdate),
    //                    loanExpiryDate = DateTime.Parse(x.loanexpirydate),
    //                    numberOfYearsToDate = DateTime.Now.Year-DateTime.Parse(x.loanstartdate).Year,
    //                    NumberOfYearsToExpiry = DateTime.Parse(x.loanexpirydate).Year - DateTime.Now.Year,
    //                    RepaymentType = x.repaymenttype,
    //                    maximumLoanLimit = double.Parse(x.maximumloanlimit),                                                
    //                    repaymentMethod = x.repaymentmethod,
    //                    currentFinancialYearInterest = double.Parse(x.interestrate),
    //                    lastFinancialYearInterest = double.Parse(x.interestrate),
    //                    suitability = 1.0,
    //                    id = "1",
    //                    latitude = -26.274985,
    //                    longitude = 134.775464,
    //                    country = "Australia",
    //                    state = "VIC",
    //                    type = "Office"  
    //                }
    //                );
    //        });

    //        return new MortgageInvestmentProfileModel
    //       {
    //           data = lstProfile
    //       };
                

            
    //    }


    //    public int CurrentYear()
    //    {
    //        return DateTime.Now.Year;
    //    }


    //    public void Dispose()
    //    {
    //        db.Dispose();
    //    }


    //}
}