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
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                double totalCost = 0;
                double totalMarketValue = 0;
                foreach (var account in groupAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList();

                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                    }
                }
                foreach (var account in clientAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList();
                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                    }
                }
                SummaryGeneralInfo summary = new SummaryGeneralInfo
                {
                    cost = totalCost,
                    marketValue = totalMarketValue,
                    pl = totalMarketValue - totalCost,
                    plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100
                };

                return summary;
            }
            else
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                double totalCost = 0;
                double totalMarketValue = 0;
                foreach (var account in accounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList();
                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                    }
                }
                SummaryGeneralInfo summary = new SummaryGeneralInfo
                {
                    cost = totalCost,
                    marketValue = totalMarketValue,
                    pl = totalMarketValue - totalCost,
                    plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100
                };

                return summary;
            }

        }
        [HttpGet, Route("api/Client/InternationalEquityPortfolio/General")]
        public SummaryGeneralInfo GetGeneralInfo_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                double totalCost = 0;
                double totalMarketValue = 0;
                foreach (var account in groupAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList();

                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                    }
                }
                foreach (var account in clientAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList();
                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                    }
                }
                SummaryGeneralInfo summary = new SummaryGeneralInfo
                {
                    cost = totalCost,
                    marketValue = totalMarketValue,
                    pl = totalMarketValue - totalCost,
                    plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100
                };

                return summary;
            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                double totalCost = 0;
                double totalMarketValue = 0;
                foreach (var account in accounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList();
                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                    }
                }
                SummaryGeneralInfo summary = new SummaryGeneralInfo
                {
                    cost = totalCost,
                    marketValue = totalMarketValue,
                    pl = totalMarketValue - totalCost,
                    plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100
                };

                return summary;
            }
        }

        [HttpGet, Route("api/Adviser/InternationalEquityPortfolio/EvaluationModel")]
        public IEnumerable<EvaluationModel> GetEvaluationModel_Adviser(string clientGroupId = null)
        {
            if (string.IsNullOrEmpty(clientGroupId))
            {

                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

                EvaluationModel model1 = new EvaluationModel { title = "One Year Return" };
                EvaluationModel model3 = new EvaluationModel { title = "Debt Equity Ratio" };
                EvaluationModel model4 = new EvaluationModel { title = "Eps Growth" };
                EvaluationModel model5 = new EvaluationModel { title = "Dividend Yield" };
                EvaluationModel model6 = new EvaluationModel { title = "Franking" };
                EvaluationModel model7 = new EvaluationModel { title = "Interest Cover" };
                EvaluationModel model8 = new EvaluationModel { title = "Price Earning Ratio" };
                EvaluationModel model9 = new EvaluationModel { title = "Return On Asset" };
                EvaluationModel model10 = new EvaluationModel { title = "Return On Equity" };
                foreach (var account in groupAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync();

                    Ratios ratios = assets.GetAverageRatiosFor<InternationalEquity>();
                    Recommendation expected = assets.GetAverageExpectedFor<InternationalEquity>();

                    model1.actual += ratios.OneYearReturn;
                    model1.expected += expected.OneYearReturn == null ? 0 : (double)expected.OneYearReturn;

                    model3.actual += ratios.DebtEquityRatio;
                    model3.expected += expected.DebtEquityRatio;

                    model4.actual += ratios.EpsGrowth;
                    model4.expected += expected.EpsGrowth;

                    model5.actual += ratios.DividendYield;
                    model5.expected += expected.DividendYield;

                    model6.actual += ratios.Frank;
                    model6.expected += expected.Frank;

                    model7.actual += ratios.InterestCover;
                    model7.expected += expected.InterestCover;

                    model8.actual += ratios.PriceEarningRatio;
                    model8.expected += expected.PriceEarningRatio;

                    model9.actual += ratios.ReturnOnAsset;
                    model9.expected += expected.ReturnOnAsset;

                    model10.actual += ratios.ReturnOnEquity;
                    model10.expected += expected.ReturnOnEquity;
                }
                foreach (var account in clientAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync();

                    Ratios ratios = assets.GetAverageRatiosFor<InternationalEquity>();
                    Recommendation expected = assets.GetAverageExpectedFor<InternationalEquity>();

                    model1.actual += ratios.OneYearReturn;
                    model1.expected += expected.OneYearReturn == null ? 0 : (double)expected.OneYearReturn;

                    model3.actual += ratios.DebtEquityRatio;
                    model3.expected += expected.DebtEquityRatio;

                    model4.actual += ratios.EpsGrowth;
                    model4.expected += expected.EpsGrowth;

                    model5.actual += ratios.DividendYield;
                    model5.expected += expected.DividendYield;

                    model6.actual += ratios.Frank;
                    model6.expected += expected.Frank;

                    model7.actual += ratios.InterestCover;
                    model7.expected += expected.InterestCover;

                    model8.actual += ratios.PriceEarningRatio;
                    model8.expected += expected.PriceEarningRatio;

                    model9.actual += ratios.ReturnOnAsset;
                    model9.expected += expected.ReturnOnAsset;

                    model10.actual += ratios.ReturnOnEquity;
                    model10.expected += expected.ReturnOnEquity;
                }

                List<EvaluationModel> models = new List<EvaluationModel>();
                models.Add(model1);
                models.Add(model3);
                models.Add(model4);
                models.Add(model5);
                models.Add(model6);
                models.Add(model7);
                models.Add(model8);
                models.Add(model9);
                models.Add(model10);

                return models;

            }
            else
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);

                EvaluationModel model1 = new EvaluationModel { title = "One Year Return" };
                EvaluationModel model3 = new EvaluationModel { title = "Debt Equity Ratio" };
                EvaluationModel model4 = new EvaluationModel { title = "Eps Growth" };
                EvaluationModel model5 = new EvaluationModel { title = "Dividend Yield" };
                EvaluationModel model6 = new EvaluationModel { title = "Franking" };
                EvaluationModel model7 = new EvaluationModel { title = "Interest Cover" };
                EvaluationModel model8 = new EvaluationModel { title = "Price Earning Ratio" };
                EvaluationModel model9 = new EvaluationModel { title = "Return On Asset" };
                EvaluationModel model10 = new EvaluationModel { title = "Return On Equity" };


                foreach (var account in accounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync();

                    Ratios ratios = assets.GetAverageRatiosFor<InternationalEquity>();
                    Recommendation expected = assets.GetAverageExpectedFor<InternationalEquity>();

                    model1.actual += ratios.OneYearReturn;
                    model1.expected += expected.OneYearReturn == null ? 0 : (double)expected.OneYearReturn;

                    model3.actual += ratios.DebtEquityRatio;
                    model3.expected += expected.DebtEquityRatio;

                    model4.actual += ratios.EpsGrowth;
                    model4.expected += expected.EpsGrowth;

                    model5.actual += ratios.DividendYield;
                    model5.expected += expected.DividendYield;

                    model6.actual += ratios.Frank;
                    model6.expected += expected.Frank;

                    model7.actual += ratios.InterestCover;
                    model7.expected += expected.InterestCover;

                    model8.actual += ratios.PriceEarningRatio;
                    model8.expected += expected.PriceEarningRatio;

                    model9.actual += ratios.ReturnOnAsset;
                    model9.expected += expected.ReturnOnAsset;

                    model10.actual += ratios.ReturnOnEquity;
                    model10.expected += expected.ReturnOnEquity;
                }
                List<EvaluationModel> models = new List<EvaluationModel>();
                models.Add(model1);
                models.Add(model3);
                models.Add(model4);
                models.Add(model5);
                models.Add(model6);
                models.Add(model7);
                models.Add(model8);
                models.Add(model9);
                models.Add(model10);

                return models;

            }

        }
        [HttpGet, Route("api/Client/InternationalEquityPortfolio/EvaluationModel")]
        public IEnumerable<EvaluationModel> GetEvaluationModel_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                EvaluationModel model1 = new EvaluationModel { title = "One Year Return" };
                EvaluationModel model3 = new EvaluationModel { title = "Debt Equity Ratio" };
                EvaluationModel model4 = new EvaluationModel { title = "Eps Growth" };
                EvaluationModel model5 = new EvaluationModel { title = "Dividend Yield" };
                EvaluationModel model6 = new EvaluationModel { title = "Franking" };
                EvaluationModel model7 = new EvaluationModel { title = "Interest Cover" };
                EvaluationModel model8 = new EvaluationModel { title = "Price Earning Ratio" };
                EvaluationModel model9 = new EvaluationModel { title = "Return On Asset" };
                EvaluationModel model10 = new EvaluationModel { title = "Return On Equity" };
                foreach (var account in groupAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync();

                    Ratios ratios = assets.GetAverageRatiosFor<InternationalEquity>();
                    Recommendation expected = assets.GetAverageExpectedFor<InternationalEquity>();

                    model1.actual += ratios.OneYearReturn;
                    model1.expected += expected.OneYearReturn == null ? 0 : (double)expected.OneYearReturn;

                    model3.actual += ratios.DebtEquityRatio;
                    model3.expected += expected.DebtEquityRatio;

                    model4.actual += ratios.EpsGrowth;
                    model4.expected += expected.EpsGrowth;

                    model5.actual += ratios.DividendYield;
                    model5.expected += expected.DividendYield;

                    model6.actual += ratios.Frank;
                    model6.expected += expected.Frank;

                    model7.actual += ratios.InterestCover;
                    model7.expected += expected.InterestCover;

                    model8.actual += ratios.PriceEarningRatio;
                    model8.expected += expected.PriceEarningRatio;

                    model9.actual += ratios.ReturnOnAsset;
                    model9.expected += expected.ReturnOnAsset;

                    model10.actual += ratios.ReturnOnEquity;
                    model10.expected += expected.ReturnOnEquity;
                }
                foreach (var account in clientAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync();

                    Ratios ratios = assets.GetAverageRatiosFor<InternationalEquity>();
                    Recommendation expected = assets.GetAverageExpectedFor<InternationalEquity>();

                    model1.actual += ratios.OneYearReturn;
                    model1.expected += expected.OneYearReturn == null ? 0 : (double)expected.OneYearReturn;

                    model3.actual += ratios.DebtEquityRatio;
                    model3.expected += expected.DebtEquityRatio;

                    model4.actual += ratios.EpsGrowth;
                    model4.expected += expected.EpsGrowth;

                    model5.actual += ratios.DividendYield;
                    model5.expected += expected.DividendYield;

                    model6.actual += ratios.Frank;
                    model6.expected += expected.Frank;

                    model7.actual += ratios.InterestCover;
                    model7.expected += expected.InterestCover;

                    model8.actual += ratios.PriceEarningRatio;
                    model8.expected += expected.PriceEarningRatio;

                    model9.actual += ratios.ReturnOnAsset;
                    model9.expected += expected.ReturnOnAsset;

                    model10.actual += ratios.ReturnOnEquity;
                    model10.expected += expected.ReturnOnEquity;
                }

                List<EvaluationModel> models = new List<EvaluationModel>();
                models.Add(model1);
                models.Add(model3);
                models.Add(model4);
                models.Add(model5);
                models.Add(model6);
                models.Add(model7);
                models.Add(model8);
                models.Add(model9);
                models.Add(model10);

                return models;
            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);


                EvaluationModel model1 = new EvaluationModel { title = "One Year Return" };
                EvaluationModel model3 = new EvaluationModel { title = "Debt Equity Ratio" };
                EvaluationModel model4 = new EvaluationModel { title = "Eps Growth" };
                EvaluationModel model5 = new EvaluationModel { title = "Dividend Yield" };
                EvaluationModel model6 = new EvaluationModel { title = "Franking" };
                EvaluationModel model7 = new EvaluationModel { title = "Interest Cover" };
                EvaluationModel model8 = new EvaluationModel { title = "Price Earning Ratio" };
                EvaluationModel model9 = new EvaluationModel { title = "Return On Asset" };
                EvaluationModel model10 = new EvaluationModel { title = "Return On Equity" };


                foreach (var account in accounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync();

                    Ratios ratios = assets.GetAverageRatiosFor<InternationalEquity>();
                    Recommendation expected = assets.GetAverageExpectedFor<InternationalEquity>();

                    model1.actual += ratios.OneYearReturn;
                    model1.expected += expected.OneYearReturn == null ? 0 : (double)expected.OneYearReturn;

                    model3.actual += ratios.DebtEquityRatio;
                    model3.expected += expected.DebtEquityRatio;

                    model4.actual += ratios.EpsGrowth;
                    model4.expected += expected.EpsGrowth;

                    model5.actual += ratios.DividendYield;
                    model5.expected += expected.DividendYield;

                    model6.actual += ratios.Frank;
                    model6.expected += expected.Frank;

                    model7.actual += ratios.InterestCover;
                    model7.expected += expected.InterestCover;

                    model8.actual += ratios.PriceEarningRatio;
                    model8.expected += expected.PriceEarningRatio;

                    model9.actual += ratios.ReturnOnAsset;
                    model9.expected += expected.ReturnOnAsset;

                    model10.actual += ratios.ReturnOnEquity;
                    model10.expected += expected.ReturnOnEquity;
                }
                List<EvaluationModel> models = new List<EvaluationModel>();
                models.Add(model1);
                models.Add(model3);
                models.Add(model4);
                models.Add(model5);
                models.Add(model6);
                models.Add(model7);
                models.Add(model8);
                models.Add(model9);
                models.Add(model10);

                return models;

            }
        }

        [HttpGet, Route("api/Adviser/InternationalEquityPortfolio/Cashflow")]
        public CashflowBriefModel GetCashflowSummary_Adviser(string clientGroupId = null)
        {
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

                double totalExpenseInAssets = 0;
                double totalIncomeInAssets = 0;

                List<CashFlowBriefItem> items = new List<CashFlowBriefItem>();

                CashFlowBriefItem jan = new CashFlowBriefItem();
                CashFlowBriefItem feb = new CashFlowBriefItem();
                CashFlowBriefItem mar = new CashFlowBriefItem();
                CashFlowBriefItem apr = new CashFlowBriefItem();
                CashFlowBriefItem may = new CashFlowBriefItem();
                CashFlowBriefItem jun = new CashFlowBriefItem();
                CashFlowBriefItem jul = new CashFlowBriefItem();
                CashFlowBriefItem aug = new CashFlowBriefItem();
                CashFlowBriefItem sep = new CashFlowBriefItem();
                CashFlowBriefItem oct = new CashFlowBriefItem();
                CashFlowBriefItem nov = new CashFlowBriefItem();
                CashFlowBriefItem dec = new CashFlowBriefItem();


                foreach (var account in groupAccounts)
                {
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

                    foreach (var cashflow in cashFlows)
                    {
                        switch (cashflow.Month)
                        {
                            case "Jan": jan.date = DateTime.Now; jan.expense += cashflow.Expenses; jan.income += cashflow.Income; jan.month = cashflow.Month; break;
                            case "Feb": feb.date = DateTime.Now; feb.expense += cashflow.Expenses; feb.income += cashflow.Income; feb.month = cashflow.Month; break;
                            case "Mar": mar.date = DateTime.Now; mar.expense += cashflow.Expenses; mar.income += cashflow.Income; mar.month = cashflow.Month; break;
                            case "Apr": apr.date = DateTime.Now; apr.expense += cashflow.Expenses; apr.income += cashflow.Income; apr.month = cashflow.Month; break;
                            case "May": may.date = DateTime.Now; may.expense += cashflow.Expenses; may.income += cashflow.Income; may.month = cashflow.Month; break;
                            case "Jun": jun.date = DateTime.Now; jun.expense += cashflow.Expenses; jun.income += cashflow.Income; jun.month = cashflow.Month; break;
                            case "Jul": jul.date = DateTime.Now; jul.expense += cashflow.Expenses; jul.income += cashflow.Income; jul.month = cashflow.Month; break;
                            case "Aug": aug.date = DateTime.Now; aug.expense += cashflow.Expenses; aug.income += cashflow.Income; aug.month = cashflow.Month; break;
                            case "Sep": sep.date = DateTime.Now; sep.expense += cashflow.Expenses; sep.income += cashflow.Income; sep.month = cashflow.Month; break;
                            case "Oct": oct.date = DateTime.Now; oct.expense += cashflow.Expenses; oct.income += cashflow.Income; oct.month = cashflow.Month; break;
                            case "Nov": nov.date = DateTime.Now; nov.expense += cashflow.Expenses; nov.income += cashflow.Income; nov.month = cashflow.Month; break;
                            case "Dec": dec.date = DateTime.Now; dec.expense += cashflow.Expenses; dec.income += cashflow.Income; dec.month = cashflow.Month; break;
                            default: break;
                        }
                        totalExpenseInAssets += cashflow.Expenses;
                        totalIncomeInAssets += cashflow.Income;
                    }
                }
                foreach (var account in clientAccounts)
                {
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

                    foreach (var cashflow in cashFlows)
                    {
                        switch (cashflow.Month)
                        {
                            case "Jan": jan.date = DateTime.Now; jan.expense += cashflow.Expenses; jan.income += cashflow.Income; jan.month = cashflow.Month; break;
                            case "Feb": feb.date = DateTime.Now; feb.expense += cashflow.Expenses; feb.income += cashflow.Income; feb.month = cashflow.Month; break;
                            case "Mar": mar.date = DateTime.Now; mar.expense += cashflow.Expenses; mar.income += cashflow.Income; mar.month = cashflow.Month; break;
                            case "Apr": apr.date = DateTime.Now; apr.expense += cashflow.Expenses; apr.income += cashflow.Income; apr.month = cashflow.Month; break;
                            case "May": may.date = DateTime.Now; may.expense += cashflow.Expenses; may.income += cashflow.Income; may.month = cashflow.Month; break;
                            case "Jun": jun.date = DateTime.Now; jun.expense += cashflow.Expenses; jun.income += cashflow.Income; jun.month = cashflow.Month; break;
                            case "Jul": jul.date = DateTime.Now; jul.expense += cashflow.Expenses; jul.income += cashflow.Income; jul.month = cashflow.Month; break;
                            case "Aug": aug.date = DateTime.Now; aug.expense += cashflow.Expenses; aug.income += cashflow.Income; aug.month = cashflow.Month; break;
                            case "Sep": sep.date = DateTime.Now; sep.expense += cashflow.Expenses; sep.income += cashflow.Income; sep.month = cashflow.Month; break;
                            case "Oct": oct.date = DateTime.Now; oct.expense += cashflow.Expenses; oct.income += cashflow.Income; oct.month = cashflow.Month; break;
                            case "Nov": nov.date = DateTime.Now; nov.expense += cashflow.Expenses; nov.income += cashflow.Income; nov.month = cashflow.Month; break;
                            case "Dec": dec.date = DateTime.Now; dec.expense += cashflow.Expenses; dec.income += cashflow.Income; dec.month = cashflow.Month; break;
                            default: break;
                        }
                        totalExpenseInAssets += cashflow.Expenses;
                        totalIncomeInAssets += cashflow.Income;
                    }
                }


                items.Add(jan);
                items.Add(feb);
                items.Add(mar);
                items.Add(apr);
                items.Add(may);
                items.Add(jun);
                items.Add(jul);
                items.Add(aug);
                items.Add(sep);
                items.Add(oct);
                items.Add(nov);
                items.Add(dec);


                CashflowBriefModel model = new CashflowBriefModel
                {
                    totalExpense = totalExpenseInAssets,
                    totalIncome = totalIncomeInAssets,
                    data = items
                };
                return model;
            }
            else
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                double totalExpenseInAssets = 0;
                double totalIncomeInAssets = 0;

                List<CashFlowBriefItem> items = new List<CashFlowBriefItem>();

                CashFlowBriefItem jan = new CashFlowBriefItem { month = "Jan" };
                CashFlowBriefItem feb = new CashFlowBriefItem { month = "Feb" };
                CashFlowBriefItem mar = new CashFlowBriefItem { month = "Mar" };
                CashFlowBriefItem apr = new CashFlowBriefItem { month = "Apr" };
                CashFlowBriefItem may = new CashFlowBriefItem { month = "May" };
                CashFlowBriefItem jun = new CashFlowBriefItem { month = "Jun" };
                CashFlowBriefItem jul = new CashFlowBriefItem { month = "Jul" };
                CashFlowBriefItem aug = new CashFlowBriefItem { month = "Aug" };
                CashFlowBriefItem sep = new CashFlowBriefItem { month = "Sep" };
                CashFlowBriefItem oct = new CashFlowBriefItem { month = "Oct" };
                CashFlowBriefItem nov = new CashFlowBriefItem { month = "Nov" };
                CashFlowBriefItem dec = new CashFlowBriefItem { month = "Dec" };

                foreach (var account in accounts)
                {
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

                    foreach (var cashflow in cashFlows)
                    {
                        switch (cashflow.Month)
                        {
                            case "Jan": jan.date = DateTime.Now; jan.expense += cashflow.Expenses; jan.income += cashflow.Income; break;
                            case "Feb": feb.date = DateTime.Now; feb.expense += cashflow.Expenses; feb.income += cashflow.Income; break;
                            case "Mar": mar.date = DateTime.Now; mar.expense += cashflow.Expenses; mar.income += cashflow.Income; break;
                            case "Apr": apr.date = DateTime.Now; apr.expense += cashflow.Expenses; apr.income += cashflow.Income; break;
                            case "May": may.date = DateTime.Now; may.expense += cashflow.Expenses; may.income += cashflow.Income; break;
                            case "Jun": jun.date = DateTime.Now; jun.expense += cashflow.Expenses; jun.income += cashflow.Income; break;
                            case "Jul": jul.date = DateTime.Now; jul.expense += cashflow.Expenses; jul.income += cashflow.Income; break;
                            case "Aug": aug.date = DateTime.Now; aug.expense += cashflow.Expenses; aug.income += cashflow.Income; break;
                            case "Sep": sep.date = DateTime.Now; sep.expense += cashflow.Expenses; sep.income += cashflow.Income; break;
                            case "Oct": oct.date = DateTime.Now; oct.expense += cashflow.Expenses; oct.income += cashflow.Income; break;
                            case "Nov": nov.date = DateTime.Now; nov.expense += cashflow.Expenses; nov.income += cashflow.Income; break;
                            case "Dec": dec.date = DateTime.Now; dec.expense += cashflow.Expenses; dec.income += cashflow.Income; break;
                            default: break;
                        }
                        totalExpenseInAssets += cashflow.Expenses;
                        totalIncomeInAssets += cashflow.Income;
                    }
                }

                items.Add(jan);
                items.Add(feb);
                items.Add(mar);
                items.Add(apr);
                items.Add(may);
                items.Add(jun);
                items.Add(jul);
                items.Add(aug);
                items.Add(sep);
                items.Add(oct);
                items.Add(nov);
                items.Add(dec);


                CashflowBriefModel model = new CashflowBriefModel
                {
                    totalExpense = totalExpenseInAssets,
                    totalIncome = totalIncomeInAssets,
                    data = items
                };
                return model;

            }
        }
        [HttpGet, Route("api/Client/InternationalEquityPortfolio/Cashflow")]
        public CashflowBriefModel GetCashflowSummary_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                double totalExpenseInAssets = 0;
                double totalIncomeInAssets = 0;

                List<CashFlowBriefItem> items = new List<CashFlowBriefItem>();

                CashFlowBriefItem jan = new CashFlowBriefItem();
                CashFlowBriefItem feb = new CashFlowBriefItem();
                CashFlowBriefItem mar = new CashFlowBriefItem();
                CashFlowBriefItem apr = new CashFlowBriefItem();
                CashFlowBriefItem may = new CashFlowBriefItem();
                CashFlowBriefItem jun = new CashFlowBriefItem();
                CashFlowBriefItem jul = new CashFlowBriefItem();
                CashFlowBriefItem aug = new CashFlowBriefItem();
                CashFlowBriefItem sep = new CashFlowBriefItem();
                CashFlowBriefItem oct = new CashFlowBriefItem();
                CashFlowBriefItem nov = new CashFlowBriefItem();
                CashFlowBriefItem dec = new CashFlowBriefItem();


                foreach (var account in groupAccounts)
                {
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

                    foreach (var cashflow in cashFlows)
                    {
                        switch (cashflow.Month)
                        {
                            case "Jan": jan.date = DateTime.Now; jan.expense += cashflow.Expenses; jan.income += cashflow.Income; jan.month = cashflow.Month; break;
                            case "Feb": feb.date = DateTime.Now; feb.expense += cashflow.Expenses; feb.income += cashflow.Income; feb.month = cashflow.Month; break;
                            case "Mar": mar.date = DateTime.Now; mar.expense += cashflow.Expenses; mar.income += cashflow.Income; mar.month = cashflow.Month; break;
                            case "Apr": apr.date = DateTime.Now; apr.expense += cashflow.Expenses; apr.income += cashflow.Income; apr.month = cashflow.Month; break;
                            case "May": may.date = DateTime.Now; may.expense += cashflow.Expenses; may.income += cashflow.Income; may.month = cashflow.Month; break;
                            case "Jun": jun.date = DateTime.Now; jun.expense += cashflow.Expenses; jun.income += cashflow.Income; jun.month = cashflow.Month; break;
                            case "Jul": jul.date = DateTime.Now; jul.expense += cashflow.Expenses; jul.income += cashflow.Income; jul.month = cashflow.Month; break;
                            case "Aug": aug.date = DateTime.Now; aug.expense += cashflow.Expenses; aug.income += cashflow.Income; aug.month = cashflow.Month; break;
                            case "Sep": sep.date = DateTime.Now; sep.expense += cashflow.Expenses; sep.income += cashflow.Income; sep.month = cashflow.Month; break;
                            case "Oct": oct.date = DateTime.Now; oct.expense += cashflow.Expenses; oct.income += cashflow.Income; oct.month = cashflow.Month; break;
                            case "Nov": nov.date = DateTime.Now; nov.expense += cashflow.Expenses; nov.income += cashflow.Income; nov.month = cashflow.Month; break;
                            case "Dec": dec.date = DateTime.Now; dec.expense += cashflow.Expenses; dec.income += cashflow.Income; dec.month = cashflow.Month; break;
                            default: break;
                        }
                        totalExpenseInAssets += cashflow.Expenses;
                        totalIncomeInAssets += cashflow.Income;
                    }
                }
                foreach (var account in clientAccounts)
                {
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

                    foreach (var cashflow in cashFlows)
                    {
                        switch (cashflow.Month)
                        {
                            case "Jan": jan.date = DateTime.Now; jan.expense += cashflow.Expenses; jan.income += cashflow.Income; jan.month = cashflow.Month; break;
                            case "Feb": feb.date = DateTime.Now; feb.expense += cashflow.Expenses; feb.income += cashflow.Income; feb.month = cashflow.Month; break;
                            case "Mar": mar.date = DateTime.Now; mar.expense += cashflow.Expenses; mar.income += cashflow.Income; mar.month = cashflow.Month; break;
                            case "Apr": apr.date = DateTime.Now; apr.expense += cashflow.Expenses; apr.income += cashflow.Income; apr.month = cashflow.Month; break;
                            case "May": may.date = DateTime.Now; may.expense += cashflow.Expenses; may.income += cashflow.Income; may.month = cashflow.Month; break;
                            case "Jun": jun.date = DateTime.Now; jun.expense += cashflow.Expenses; jun.income += cashflow.Income; jun.month = cashflow.Month; break;
                            case "Jul": jul.date = DateTime.Now; jul.expense += cashflow.Expenses; jul.income += cashflow.Income; jul.month = cashflow.Month; break;
                            case "Aug": aug.date = DateTime.Now; aug.expense += cashflow.Expenses; aug.income += cashflow.Income; aug.month = cashflow.Month; break;
                            case "Sep": sep.date = DateTime.Now; sep.expense += cashflow.Expenses; sep.income += cashflow.Income; sep.month = cashflow.Month; break;
                            case "Oct": oct.date = DateTime.Now; oct.expense += cashflow.Expenses; oct.income += cashflow.Income; oct.month = cashflow.Month; break;
                            case "Nov": nov.date = DateTime.Now; nov.expense += cashflow.Expenses; nov.income += cashflow.Income; nov.month = cashflow.Month; break;
                            case "Dec": dec.date = DateTime.Now; dec.expense += cashflow.Expenses; dec.income += cashflow.Income; dec.month = cashflow.Month; break;
                            default: break;
                        }
                        totalExpenseInAssets += cashflow.Expenses;
                        totalIncomeInAssets += cashflow.Income;
                    }
                }


                items.Add(jan);
                items.Add(feb);
                items.Add(mar);
                items.Add(apr);
                items.Add(may);
                items.Add(jun);
                items.Add(jul);
                items.Add(aug);
                items.Add(sep);
                items.Add(oct);
                items.Add(nov);
                items.Add(dec);


                CashflowBriefModel model = new CashflowBriefModel
                {
                    totalExpense = totalExpenseInAssets,
                    totalIncome = totalIncomeInAssets,
                    data = items
                };
                return model;
            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                double totalExpenseInAssets = 0;
                double totalIncomeInAssets = 0;

                List<CashFlowBriefItem> items = new List<CashFlowBriefItem>();

                CashFlowBriefItem jan = new CashFlowBriefItem { month = "Jan" };
                CashFlowBriefItem feb = new CashFlowBriefItem { month = "Feb" };
                CashFlowBriefItem mar = new CashFlowBriefItem { month = "Mar" };
                CashFlowBriefItem apr = new CashFlowBriefItem { month = "Apr" };
                CashFlowBriefItem may = new CashFlowBriefItem { month = "May" };
                CashFlowBriefItem jun = new CashFlowBriefItem { month = "Jun" };
                CashFlowBriefItem jul = new CashFlowBriefItem { month = "Jul" };
                CashFlowBriefItem aug = new CashFlowBriefItem { month = "Aug" };
                CashFlowBriefItem sep = new CashFlowBriefItem { month = "Sep" };
                CashFlowBriefItem oct = new CashFlowBriefItem { month = "Oct" };
                CashFlowBriefItem nov = new CashFlowBriefItem { month = "Nov" };
                CashFlowBriefItem dec = new CashFlowBriefItem { month = "Dec" };

                foreach (var account in accounts)
                {
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

                    foreach (var cashflow in cashFlows)
                    {
                        switch (cashflow.Month)
                        {
                            case "Jan": jan.date = DateTime.Now; jan.expense += cashflow.Expenses; jan.income += cashflow.Income; break;
                            case "Feb": feb.date = DateTime.Now; feb.expense += cashflow.Expenses; feb.income += cashflow.Income; break;
                            case "Mar": mar.date = DateTime.Now; mar.expense += cashflow.Expenses; mar.income += cashflow.Income; break;
                            case "Apr": apr.date = DateTime.Now; apr.expense += cashflow.Expenses; apr.income += cashflow.Income; break;
                            case "May": may.date = DateTime.Now; may.expense += cashflow.Expenses; may.income += cashflow.Income; break;
                            case "Jun": jun.date = DateTime.Now; jun.expense += cashflow.Expenses; jun.income += cashflow.Income; break;
                            case "Jul": jul.date = DateTime.Now; jul.expense += cashflow.Expenses; jul.income += cashflow.Income; break;
                            case "Aug": aug.date = DateTime.Now; aug.expense += cashflow.Expenses; aug.income += cashflow.Income; break;
                            case "Sep": sep.date = DateTime.Now; sep.expense += cashflow.Expenses; sep.income += cashflow.Income; break;
                            case "Oct": oct.date = DateTime.Now; oct.expense += cashflow.Expenses; oct.income += cashflow.Income; break;
                            case "Nov": nov.date = DateTime.Now; nov.expense += cashflow.Expenses; nov.income += cashflow.Income; break;
                            case "Dec": dec.date = DateTime.Now; dec.expense += cashflow.Expenses; dec.income += cashflow.Income; break;
                            default: break;
                        }
                        totalExpenseInAssets += cashflow.Expenses;
                        totalIncomeInAssets += cashflow.Income;
                    }
                }

                items.Add(jan);
                items.Add(feb);
                items.Add(mar);
                items.Add(apr);
                items.Add(may);
                items.Add(jun);
                items.Add(jul);
                items.Add(aug);
                items.Add(sep);
                items.Add(oct);
                items.Add(nov);
                items.Add(dec);


                CashflowBriefModel model = new CashflowBriefModel
                {
                    totalExpense = totalExpenseInAssets,
                    totalIncome = totalIncomeInAssets,
                    data = items
                };
                return model;
            }
        }

        [HttpGet, Route("api/Adviser/InternationalEquityPortfolio/CompanyProfiles")]
        public EquityCompanyProfileModel GetCompanyProfiles_Adviser(string clientGroupId = null)
        {
            if (string.IsNullOrEmpty(clientGroupId))
            {
                //AustralianEquity australianEquity = new AustralianEquity(edisRepo);


                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

                List<EquityCompanyProfileItemModel> itemList = new List<EquityCompanyProfileItemModel>();

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in groupAccounts)
                {
                    assets.AddRange(account.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList());
                }
                foreach (var account in clientAccounts)
                {
                    assets.AddRange(account.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList());
                }
                var internationalAssets = assets.OfType<InternationalEquity>();

                var ratios = assets.GetAverageRatiosFor<InternationalEquity>();

                foreach (var asset in internationalAssets)
                {
                    itemList.Add(new EquityCompanyProfileItemModel
                    {
                        asx = asset.Ticker,
                        beta = ratios.Beta,
                        company = asset.Name,
                        currentRatio = ratios.CurrentRatio,
                        debtEquityRatio = ratios.DebtEquityRatio,
                        earningsStability = ratios.EarningsStability,
                        fiveYearReturn = ratios.FiveYearReturn,
                        threeYearReturn = ratios.ThreeYearReturn,
                        oneYearReturn = ratios.OneYearReturn,
                        franking = ratios.Frank,
                        interestCover = ratios.InterestCover,
                        payoutRatio = ratios.PayoutRatio,
                        priceEarningsRatio = ratios.PriceEarningRatio,
                        quickRatio = ratios.QuickRatio,
                        returnOnAsset = ratios.ReturnOnAsset,
                        returnOnEquity = ratios.ReturnOnEquity
                    });
                }

                EquityCompanyProfileModel model = new EquityCompanyProfileModel
                {
                    data = itemList,
                    totalCostInvestment = assets.GetTotalCost().Total,
                    totalMarketValue = assets.GetTotalMarketValue()
                };
                return model;
                //return repo.AustralianEquity_GetCompanyProfiles_Adviser(User.Identity.GetUserId());
            }
            else
            {

                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<EquityCompanyProfileItemModel> itemList = new List<EquityCompanyProfileItemModel>();

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in accounts)
                {
                    assets.AddRange(account.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList());
                }
                var internationalAssets = assets.OfType<InternationalEquity>();

                var ratios = assets.GetAverageRatiosFor<InternationalEquity>();

                foreach (var asset in internationalAssets)
                {
                    itemList.Add(new EquityCompanyProfileItemModel
                    {
                        asx = asset.Ticker,
                        beta = ratios.Beta,
                        company = asset.Name,
                        currentRatio = ratios.CurrentRatio,
                        debtEquityRatio = ratios.DebtEquityRatio,
                        earningsStability = ratios.EarningsStability,
                        fiveYearReturn = ratios.FiveYearReturn,
                        threeYearReturn = ratios.ThreeYearReturn,
                        oneYearReturn = ratios.OneYearReturn,
                        franking = ratios.Frank,
                        interestCover = ratios.InterestCover,
                        payoutRatio = ratios.PayoutRatio,
                        priceEarningsRatio = ratios.PriceEarningRatio,
                        quickRatio = ratios.QuickRatio,
                        returnOnAsset = ratios.ReturnOnAsset,
                        returnOnEquity = ratios.ReturnOnEquity
                    });
                }

                EquityCompanyProfileModel model = new EquityCompanyProfileModel
                {
                    data = itemList,
                    totalCostInvestment = assets.GetTotalCost().Total,
                    totalMarketValue = assets.GetTotalMarketValue()
                };
                return model;

                //return repo.AustralianEquity_GetCompanyProfiles_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/InternationalEquityPortfolio/CompanyProfiles")]
        public EquityCompanyProfileModel GetCompanyProfiles_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);


                List<EquityCompanyProfileItemModel> itemList = new List<EquityCompanyProfileItemModel>();

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in groupAccounts)
                {
                    assets.AddRange(account.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList());
                }
                foreach (var account in clientAccounts)
                {
                    assets.AddRange(account.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList());
                }
                var internationalAssets = assets.OfType<InternationalEquity>();

                var ratios = assets.GetAverageRatiosFor<InternationalEquity>();

                foreach (var asset in internationalAssets)
                {
                    itemList.Add(new EquityCompanyProfileItemModel
                    {
                        asx = asset.Ticker,
                        beta = ratios.Beta,
                        company = asset.Name,
                        currentRatio = ratios.CurrentRatio,
                        debtEquityRatio = ratios.DebtEquityRatio,
                        earningsStability = ratios.EarningsStability,
                        fiveYearReturn = ratios.FiveYearReturn,
                        threeYearReturn = ratios.ThreeYearReturn,
                        oneYearReturn = ratios.OneYearReturn,
                        franking = ratios.Frank,
                        interestCover = ratios.InterestCover,
                        payoutRatio = ratios.PayoutRatio,
                        priceEarningsRatio = ratios.PriceEarningRatio,
                        quickRatio = ratios.QuickRatio,
                        returnOnAsset = ratios.ReturnOnAsset,
                        returnOnEquity = ratios.ReturnOnEquity
                    });
                }

                EquityCompanyProfileModel model = new EquityCompanyProfileModel
                {
                    data = itemList,
                    totalCostInvestment = assets.GetTotalCost().Total,
                    totalMarketValue = assets.GetTotalMarketValue()
                };
                return model;
            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                List<EquityCompanyProfileItemModel> itemList = new List<EquityCompanyProfileItemModel>();

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in accounts)
                {
                    assets.AddRange(account.GetAssetsSync().OfType<InternationalEquity>().Cast<AssetBase>().ToList());
                }
                var internationalAssets = assets.OfType<InternationalEquity>();

                var ratios = assets.GetAverageRatiosFor<InternationalEquity>();

                foreach (var asset in internationalAssets)
                {
                    itemList.Add(new EquityCompanyProfileItemModel
                    {
                        asx = asset.Ticker,
                        beta = ratios.Beta,
                        company = asset.Name,
                        currentRatio = ratios.CurrentRatio,
                        debtEquityRatio = ratios.DebtEquityRatio,
                        earningsStability = ratios.EarningsStability,
                        fiveYearReturn = ratios.FiveYearReturn,
                        threeYearReturn = ratios.ThreeYearReturn,
                        oneYearReturn = ratios.OneYearReturn,
                        franking = ratios.Frank,
                        interestCover = ratios.InterestCover,
                        payoutRatio = ratios.PayoutRatio,
                        priceEarningsRatio = ratios.PriceEarningRatio,
                        quickRatio = ratios.QuickRatio,
                        returnOnAsset = ratios.ReturnOnAsset,
                        returnOnEquity = ratios.ReturnOnEquity
                    });
                }

                EquityCompanyProfileModel model = new EquityCompanyProfileModel
                {
                    data = itemList,
                    totalCostInvestment = assets.GetTotalCost().Total,
                    totalMarketValue = assets.GetTotalMarketValue()
                };
                return model;
            }
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
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

                double assetsSuitability = 0;
                double percentage = 0;

                foreach (var account in groupAccounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<InternationalEquity>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((InternationalEquity)w.Weightable).GetRating().TotalScore);

                    //var weigthings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();

                    //% of not suitable assets
                    percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                        / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());
                }

                foreach (var account in clientAccounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<InternationalEquity>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((InternationalEquity)w.Weightable).GetRating().TotalScore);

                    //var weigthings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();

                    //% of not suitable assets
                    percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                        / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());
                }

                PortfolioRatingModel model = new PortfolioRatingModel
                {
                    suitability = assetsSuitability,
                    notSuited = percentage
                };

                return model;
            }
            else
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);

                double assetsSuitability = 0;
                double percentage = 0;
                foreach (var account in accounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<InternationalEquity>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((InternationalEquity)w.Weightable).GetRating().TotalScore);

                    //var weigthings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();

                    //% of not suitable assets
                    percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                        / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());
                }

                PortfolioRatingModel model = new PortfolioRatingModel
                {
                    suitability = assetsSuitability,
                    notSuited = percentage
                };

                return model;
            }
        }
        [HttpGet, Route("api/Client/InternationalEquityPortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);


                double assetsSuitability = 0;
                double percentage = 0;

                foreach (var account in groupAccounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<InternationalEquity>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((InternationalEquity)w.Weightable).GetRating().TotalScore);

                    //var weigthings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();

                    //% of not suitable assets
                    percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                        / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());
                }

                foreach (var account in clientAccounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<InternationalEquity>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((InternationalEquity)w.Weightable).GetRating().TotalScore);

                    //var weigthings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();

                    //% of not suitable assets
                    percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                        / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());
                }

                PortfolioRatingModel model = new PortfolioRatingModel
                {
                    suitability = assetsSuitability,
                    notSuited = percentage
                };

                return model;
            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                double assetsSuitability = 0;
                double percentage = 0;
                foreach (var account in accounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<InternationalEquity>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((InternationalEquity)w.Weightable).GetRating().TotalScore);

                    //var weigthings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();

                    //% of not suitable assets
                    percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                        / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());
                }

                PortfolioRatingModel model = new PortfolioRatingModel
                {
                    suitability = assetsSuitability,
                    notSuited = percentage
                };

                return model;
            }
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


    }
}
