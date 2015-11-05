﻿using System;
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
using Domain.Portfolio.Services;
using Domain.Portfolio.AggregateRoots.Asset;
using Domain.Portfolio.Values.Ratios;
using Domain.Portfolio.Values.Weighting;
using Shared;
using Domain.Portfolio.Values.Cashflow;
using Domain.Portfolio.AggregateRoots.Accounts;
using System.Threading.Tasks;

namespace EDISAngular.APIControllers
{
    public class PortfolioAustralianEquityController : ApiController
    {


        private PortfolioRepository repo = new PortfolioRepository();
        private EdisRepository edisRepo = new EdisRepository();

        [HttpGet, Route("api/Adviser/AustralianEquityPortfolio/General")]
        public SummaryGeneralInfo GetGeneralInfo_Adviser(string clientGroupId = "")
        {
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<AssetBase> assets = new List<AssetBase>();
                double totalCost = 0;
                double totalMarketValue = 0;
                double capitalGain = 0;
                groupAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList()));

                foreach (var asset in assets) {
                    totalCost += asset.GetCost().Total;
                    totalMarketValue += asset.GetTotalMarketValue();
                    capitalGain += asset.GetCost().CapitalGain;
                }
                SummaryGeneralInfo summary = new SummaryGeneralInfo
                {
                    cost = totalCost,
                    marketValue = totalMarketValue,
                    pl = totalMarketValue - totalCost,
                    plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100,
                    capitalGain = capitalGain
                };

                return summary;
            }
            else
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

                List<AssetBase> assets = new List<AssetBase>();

                double totalCost = 0;
                double totalMarketValue = 0;
                double capitalGain = 0;
                accounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList()));

                foreach (var asset in assets)
                {
                    totalCost += asset.GetCost().Total;
                    totalMarketValue += asset.GetTotalMarketValue();
                    capitalGain += asset.GetCost().CapitalGain;
                }
                SummaryGeneralInfo summary = new SummaryGeneralInfo
                {
                    cost = totalCost,
                    marketValue = totalMarketValue,
                    pl = totalMarketValue - totalCost,
                    plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100,
                    capitalGain = capitalGain
                };

                return summary;
            }
        }
        [HttpGet, Route("api/Client/AustralianEquityPortfolio/General")]
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
                double capitalGain = 0;
                foreach (var account in groupAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList();

                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                        capitalGain += asset.GetCost().CapitalGain;
                    }
                }
                foreach (var account in clientAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList();
                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                        capitalGain += asset.GetCost().CapitalGain;
                    }
                }
                SummaryGeneralInfo summary = new SummaryGeneralInfo
                {
                    cost = totalCost,
                    marketValue = totalMarketValue,
                    pl = totalMarketValue - totalCost,
                    plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100,
                    capitalGain = capitalGain
            };

                return summary;

            }
            else 
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                

                double totalCost = 0;
                double totalMarketValue = 0;
                double capitalGain = 0;
                foreach (var account in accounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList();
                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                        capitalGain += asset.GetCost().CapitalGain;
                    }
                }
                SummaryGeneralInfo summary = new SummaryGeneralInfo
                {
                    cost = totalCost,
                    marketValue = totalMarketValue,
                    pl = totalMarketValue - totalCost,
                    plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100,
                    capitalGain = capitalGain
                };

                return summary;
            }
        }

        [HttpGet, Route("api/Adviser/AustralianEquityPortfolio/EvaluationModel")]
        public IEnumerable<EvaluationModel> GetEvaluationModel_Adviser(string clientGroupId = null)
        {

            if (string.IsNullOrEmpty(clientGroupId))
            {

                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

                EvaluationModel model1 = new EvaluationModel { title = "One Year Return" };
                EvaluationModel model2 = new EvaluationModel { title = "Five Year Return" };
                EvaluationModel model3 = new EvaluationModel { title = "Debt Equity Ratio" };
                EvaluationModel model4 = new EvaluationModel { title = "Eps Growth" };
                EvaluationModel model5 = new EvaluationModel { title = "Dividend Yield" };
                EvaluationModel model6 = new EvaluationModel { title = "Franking" };
                EvaluationModel model7 = new EvaluationModel { title = "Interest Cover" };
                EvaluationModel model8 = new EvaluationModel { title = "Price Earning Ratio" };
                EvaluationModel model9 = new EvaluationModel { title = "Return On Asset" };
                EvaluationModel model10 = new EvaluationModel { title = "Return On Equity" };
                EvaluationModel model12 = new EvaluationModel { title = "Beta" };
                EvaluationModel model15 = new EvaluationModel { title = "Payout Ratio" };
                EvaluationModel model16 = new EvaluationModel { title = "Earnings Stability" };


                List<AssetBase> assets = new List<AssetBase>();
                groupAccounts.ForEach(g => assets.AddRange(g.GetAssetsSync()));
                clientAccounts.ForEach(g => assets.AddRange(g.GetAssetsSync()));

                Ratios ratios = assets.GetAverageRatiosFor<AustralianEquity>();
                Recommendation expected = assets.GetAverageExpectedFor<AustralianEquity>();

                model1.actual += ratios.OneYearReturn;
                model1.expected += expected.OneYearReturn == null ? 0 : (double)expected.OneYearReturn;

                model2.actual += ratios.FiveYearReturn;
                model2.expected += expected.FiveYearTotalReturn == null ? 0 : (double)expected.FiveYearTotalReturn;

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


                List<EvaluationModel> models = new List<EvaluationModel>();
                models.Add(model1);
                models.Add(model2);
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
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

                List<AssetBase> assets = new List<AssetBase>();

                EvaluationModel model1 = new EvaluationModel { title = "One Year Return" };
                EvaluationModel model2 = new EvaluationModel { title = "Five Year Return" };
                EvaluationModel model3 = new EvaluationModel { title = "Debt Equity Ratio" };
                EvaluationModel model4 = new EvaluationModel { title = "Eps Growth" };
                EvaluationModel model5 = new EvaluationModel { title = "Dividend Yield" };
                EvaluationModel model6 = new EvaluationModel { title = "Franking" };
                EvaluationModel model7 = new EvaluationModel { title = "Interest Cover" };
                EvaluationModel model8 = new EvaluationModel { title = "Price Earning Ratio" };
                EvaluationModel model9 = new EvaluationModel { title = "Return On Asset" };
                EvaluationModel model10 = new EvaluationModel { title = "Return On Equity" };

                accounts.ForEach(c => assets.AddRange(c.GetAssetsSync()));

                clientAccounts.ForEach(c => assets.AddRange(c.GetAssetsSync()));
                

                Ratios ratios = assets.GetAverageRatiosFor<AustralianEquity>();
                Recommendation expected = assets.GetAverageExpectedFor<AustralianEquity>();

                model1.actual += ratios.OneYearReturn;
                model1.expected += expected.OneYearReturn == null ? 0 : (double)expected.OneYearReturn;

                model2.actual += ratios.FiveYearReturn;
                model2.expected += expected.FiveYearTotalReturn == null ? 0 : (double)expected.FiveYearTotalReturn;

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

                List<EvaluationModel> models = new List<EvaluationModel>();
                models.Add(model1);
                models.Add(model2);
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

        [HttpGet, Route("api/Client/AustralianEquityPortfolio/EvaluationModel")]
        public IEnumerable<EvaluationModel> GetEvaluationModel_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                EvaluationModel model1 = new EvaluationModel { title = "One Year Return" };
                EvaluationModel model2 = new EvaluationModel { title = "Five Year Return" };
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

                    Ratios ratios = assets.GetAverageRatiosFor<AustralianEquity>();
                    Recommendation expected = assets.GetAverageExpectedFor<AustralianEquity>();

                    model1.actual += ratios.OneYearReturn;
                    model1.expected += expected.OneYearReturn == null ? 0 : (double)expected.OneYearReturn;

                    model2.actual += ratios.FiveYearReturn;
                    model2.expected += expected.FiveYearTotalReturn == null ? 0 : (double)expected.FiveYearTotalReturn;

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

                    Ratios ratios = assets.GetAverageRatiosFor<AustralianEquity>();
                    Recommendation expected = assets.GetAverageExpectedFor<AustralianEquity>();

                    model1.actual += ratios.OneYearReturn;
                    model1.expected += expected.OneYearReturn == null ? 0 : (double)expected.OneYearReturn;

                    model2.actual += ratios.FiveYearReturn;
                    model2.expected += expected.FiveYearTotalReturn == null ? 0 : (double)expected.FiveYearTotalReturn;

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
                models.Add(model2);
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
                EvaluationModel model2 = new EvaluationModel { title = "Five Year Return" };
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

                    Ratios ratios = assets.GetAverageRatiosFor<AustralianEquity>();
                    Recommendation expected = assets.GetAverageExpectedFor<AustralianEquity>();

                    model1.actual += ratios.OneYearReturn;
                    model1.expected += expected.OneYearReturn == null ? 0 : (double)expected.OneYearReturn;

                    model2.actual += ratios.FiveYearReturn;
                    model2.expected += expected.FiveYearTotalReturn == null ? 0 : (double)expected.FiveYearTotalReturn;

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
                models.Add(model2);
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

        [HttpGet, Route("api/Adviser/AustralianEquityPortfolio/Cashflow")]
        public CashflowBriefModel GetCashflowSummary_Adviser(string clientGroupId = "")
        {
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

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

                List<Cashflow> cashFlows = new List<Cashflow>();

                groupAccounts.ForEach(a => cashFlows.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList().GetMonthlyCashflows()));
                clientAccounts.ForEach(a => cashFlows.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList().GetMonthlyCashflows()));


                //groupAccounts.ForEach(a => cashFlows.AddRange(a.GetAssetsSync().GetMonthlyCashflows()));
                //clientAccounts.ForEach(a => cashFlows.AddRange(a.GetAssetsSync().GetMonthlyCashflows()));

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
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));
                List<Cashflow> cashFlows = new List<Cashflow>();

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

                accounts.ForEach(a => cashFlows.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList().GetMonthlyCashflows()));
                clientAccounts.ForEach(a => cashFlows.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList().GetMonthlyCashflows()));

                //accounts.ForEach(a => cashFlows.AddRange(a.GetAssetsSync().GetMonthlyCashflows()));
                //clientAccounts.ForEach(a => cashFlows.AddRange(a.GetAssetsSync().GetMonthlyCashflows()));


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
        [HttpGet, Route("api/Client/AustralianEquityPortfolio/Cashflow")]
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


                foreach (var account in groupAccounts)
                {

                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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
                foreach (var account in clientAccounts)
                {
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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

                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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

        [HttpGet, Route("api/Adviser/AustralianEquityPortfolio/CompanyProfiles")]
        public EquityCompanyProfileModel GetCompanyProfiles_Adviser(string clientGroupId = null)
        {
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

                List<EquityCompanyProfileItemModel> itemList = new List<EquityCompanyProfileItemModel>();

                List<AssetBase> assets = new List<AssetBase>();

                groupAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList()));

                var australianAssets = assets.OfType<AustralianEquity>();

                var ratios = assets.GetAverageRatiosFor<AustralianEquity>();

                foreach (var asset in australianAssets)
                {
                    bool isExist = false;
                    int index = 0;
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (itemList[i].asx == asset.Ticker)
                        {
                            isExist = true;
                            index = i;
                        }
                    }
                    if (isExist == false) {
                        itemList.Add(new EquityCompanyProfileItemModel
                        {
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
                    }
                    else
                    {
                        itemList[index].marketValue += asset.GetTotalMarketValue();
                        itemList[index].totalCostValue += asset.GetCost().Total;
                        itemList[index].costValue += asset.GetCost().AssetCost;
                    }

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
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));                

                List<EquityCompanyProfileItemModel> itemList = new List<EquityCompanyProfileItemModel>();

                List<AssetBase> assets = new List<AssetBase>();

                accounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList()));
                var australianAssets = assets.OfType<AustralianEquity>();

                var ratios = assets.GetAverageRatiosFor<AustralianEquity>();

                foreach (var asset in australianAssets)
                {
                    bool isExist = false;
                    int index = 0;
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (itemList[i].asx == asset.Ticker)
                        {
                            isExist = true;
                            index = i;
                        }
                    }
                    if (isExist == false)
                    {
                        itemList.Add(new EquityCompanyProfileItemModel
                        {
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
                    }
                    else
                    {
                        itemList[index].marketValue += asset.GetTotalMarketValue();
                        itemList[index].totalCostValue += asset.GetCost().Total;
                        itemList[index].costValue += asset.GetCost().AssetCost;
                    }

                }

                EquityCompanyProfileModel model = new EquityCompanyProfileModel {
                    data = itemList,
                    totalCostInvestment = assets.GetTotalCost().Total,
                    totalMarketValue = assets.GetTotalMarketValue()
                };
                return model;
            }
        }
        [HttpGet, Route("api/Client/AustralianEquityPortfolio/CompanyProfiles")]
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
                    assets.AddRange(account.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList());
                }
                foreach (var account in clientAccounts)
                {
                    assets.AddRange(account.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList());
                }
                var australianAssets = assets.OfType<AustralianEquity>();

                var ratios = assets.GetAverageRatiosFor<AustralianEquity>();

                foreach (var asset in australianAssets)
                {
                    bool isExist = false;
                    int index = 0;
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (itemList[i].asx == asset.Ticker)
                        {
                            isExist = true;
                            index = i;
                        }
                    }
                    if (isExist == false)
                    {
                        itemList.Add(new EquityCompanyProfileItemModel
                        {
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
                    }
                    else
                    {
                        itemList[index].marketValue += asset.GetTotalMarketValue();
                        itemList[index].totalCostValue += asset.GetCost().Total;
                        itemList[index].costValue += asset.GetCost().AssetCost;
                    }

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
                    assets.AddRange(account.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList());
                }
                var australianAssets = assets.OfType<AustralianEquity>();

                var ratios = assets.GetAverageRatiosFor<AustralianEquity>();

                foreach (var asset in australianAssets) {
                    bool isExist = false;
                    int index = 0;
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (itemList[i].asx == asset.Ticker)
                        {
                            isExist = true;
                            index = i;
                        }
                    }
                    if (isExist == false)
                    {
                        itemList.Add(new EquityCompanyProfileItemModel
                        {
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
                    }
                    else {
                        itemList[index].marketValue += asset.GetTotalMarketValue();
                        itemList[index].totalCostValue += asset.GetCost().Total;
                        itemList[index].costValue += asset.GetCost().AssetCost;
                    }

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


        [HttpGet, Route("api/Adviser/AustralianEquityPortfolio/QuickStats")]
        public IEnumerable<PortfolioQuickStatsModel> GetQuickStats_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.AustralianEquity_GetQuickStats_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.AustralianEquity_GetQuickStats_Client(clientUserId);
            }

        }
        //[HttpGet, Route("api/Client/AustralianEquityPortfolio/QuickStats")]
        //public IEnumerable<PortfolioQuickStatsModel> GetQuickStats_Client()
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

        [HttpGet, Route("api/Adviser/AustralianEquityPortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Adviser(string clientGroupId = null)
        {
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

                double assetsSuitability = 0;
                double percentage = 0;

                List<Equity> equityWithResearchValues = new List<Equity>();

                groupAccounts.ForEach(a => equityWithResearchValues.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().ToList()));
                clientAccounts.ForEach(a => equityWithResearchValues.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().ToList()));

                var weightings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();

                assetsSuitability += weightings.Sum(w => w.Percentage * ((AustralianEquity)w.Weightable).GetRating().TotalScore);

                percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                    / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());

                PortfolioRatingModel model = new PortfolioRatingModel
                {
                    suitability = assetsSuitability,
                    notSuited = percentage,
                    data = new List<PortfolioRatingItemModel>()
                };

                foreach (var weighting in weightings) {
                    Equity equity = (Equity)weighting.Weightable;
                    bool isExist = false;
                    for(int i = 0; i< model.data.Count; i++) {
                        if (equity.Ticker == model.data[i].name) {
                            isExist = true;
                        }
                    }
                    if (isExist == false) {
                        model.data.Add(new PortfolioRatingItemModel
                        {
                            name = equity.Ticker,
                            weighting = weighting.Percentage,
                            score = equity.GetRating().TotalScore
                        });
                    }
                }
                return model;
            }
            else
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

                double assetsSuitability = 0;
                double percentage = 0;

                List<Equity> equityWithResearchValues = new List<Equity>();

                accounts.ForEach(a => equityWithResearchValues.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().ToList()));
                clientAccounts.ForEach(a => equityWithResearchValues.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().ToList()));

                var weightings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();

                assetsSuitability += weightings.Sum(w => w.Percentage * ((AustralianEquity)w.Weightable).GetRating().TotalScore);

                percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                    / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());

                PortfolioRatingModel model = new PortfolioRatingModel {
                    suitability = assetsSuitability,
                    notSuited = percentage,
                    data = new List<PortfolioRatingItemModel>()
                };

                foreach (var weighting in weightings) {
                    Equity equity = (Equity)weighting.Weightable;
                    model.data.Add(new PortfolioRatingItemModel {
                        name = equity.Name,
                        weighting = weighting.Percentage,
                        score = equity.GetRating().TotalScore
                    });
                }

                return model;
            }

        }
        [HttpGet, Route("api/Client/AustralianEquityPortfolio/Rating")]
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
                    var equityWithResearchValues = account.GetAssetsSync().OfType<AustralianEquity>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((AustralianEquity)w.Weightable).GetRating().TotalScore);

                    percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                        / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());
                }

                foreach (var account in clientAccounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<AustralianEquity>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((AustralianEquity)w.Weightable).GetRating().TotalScore);

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
                    var equityWithResearchValues = account.GetAssetsSync().OfType<AustralianEquity>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((AustralianEquity)w.Weightable).GetRating().TotalScore);

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

        //[HttpGet, Route("api/Adviser/AustralianEquityPortfolio/Diversification")]
        //public EquityDiversificationModel GetDiversification_Adviser(string clientUserId = null)
        //{
        //    if (string.IsNullOrEmpty(clientUserId))
        //    {
        //        return repo.AustralianEquity_GetDiversification_Adviser(User.Identity.GetUserId());
        //    }
        //    else
        //    {
        //        return repo.AustralianEquity_GetDiversification_Client(clientUserId);
        //    }

        //}
        //[HttpGet, Route("api/Client/AustralianEquityPortfolio/Diversification")]
        //public EquityDiversificationModel GetDiversification_Client()
        //{
        //    return repo.AustralianEquity_GetDiversification_Client(User.Identity.GetUserId());
        //}

        //[HttpGet, Route("api/Adviser/AustralianEquityPortfolio/CashflowDetail")]
        //public EquityCashflowDetailedModel GetCashflowDetailed_Adviser(string clientUserId = null)
        //{
        //    if (string.IsNullOrEmpty(clientUserId))
        //    {
        //        return repo.AustralianEquity_GetSummaryCashflowDetailed_Adviser(User.Identity.GetUserId());
        //    }
        //    else
        //    {
        //        return repo.AustralianEquity_GetSummaryCashflowDetailed_Client(clientUserId);
        //    }

        //}
        //[HttpGet, Route("api/Client/AustralianEquityPortfolio/CashflowDetail")]
        //public EquityCashflowDetailedModel GetCashflowDetailed_Client()
        //{
        //    return repo.AustralianEquity_GetSummaryCashflowDetailed_Client(User.Identity.GetUserId());
        //}

    }
}
