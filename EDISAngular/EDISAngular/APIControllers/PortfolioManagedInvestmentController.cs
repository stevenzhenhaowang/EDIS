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
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                double totalCost = 0;
                double totalMarketValue = 0;
                foreach (var account in groupAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList();

                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                    }
                }
                foreach (var account in clientAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList();
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

                    List<AssetBase> assets = account.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList();
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


            //return repo.ManagedInvestment_GetGeneral_Adviser(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/ManagedInvestmentPortfolio/General")]
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
                    List<AssetBase> assets = account.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList();

                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                    }
                }
                foreach (var account in clientAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList();
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

                    List<AssetBase> assets = account.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList();
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
        [HttpGet, Route("api/Adviser/ManagedInvestmentPortfolio/Cashflow")]
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
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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

            //return repo.ManagedInvestment_GetCashflowSummary_Adviser(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/ManagedInvestmentPortfolio/Cashflow")]
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
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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

        [HttpGet, Route("api/Adviser/ManagedInvestmentPortfolio/AssetAllocation")]
        public InvestmentPortfolioModel GetAssetAllocationSummary_Adviser(string clientGroupId = null)
        {
            List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
            List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

            double totalMarketValueAE = 0;
            double totalMarketValueIE = 0;
            double totalMarketValueMI = 0;
            double totalMarketValueDP = 0;
            double totalMarketValueFI = 0;
            double totalMarketValueCD = 0;

            foreach (var account in groupAccounts) {
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

            InvestmentPortfolioModel model = new InvestmentPortfolioModel{
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
            //return repo.ManagedInvestment_GetAssetAllocation_Adviser(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/ManagedInvestmentPortfolio/CompanyProfiles")]
        public ManagedInvestmentCompanyProfileModel GetCompanyProfiles_Adviser(string clientGroupId = null)
        {

            if (string.IsNullOrEmpty(clientGroupId))
            {
                //AustralianEquity australianEquity = new AustralianEquity(edisRepo);


                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

                List<ManagedInvestmentCompanyProfileItem> itemList = new List<ManagedInvestmentCompanyProfileItem>();

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in groupAccounts)
                {
                    assets.AddRange(account.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList());
                }
                foreach (var account in clientAccounts)
                {
                    assets.AddRange(account.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList());
                }
                var managedAssets = assets.OfType<ManagedInvestment>();

                var ratios = assets.GetAverageRatiosFor<ManagedInvestment>();

                foreach (var asset in managedAssets)
                {
                    itemList.Add(new ManagedInvestmentCompanyProfileItem
                    {
                        ticker = asset.Ticker,
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

                ManagedInvestmentCompanyProfileModel model = new ManagedInvestmentCompanyProfileModel
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
                List<ManagedInvestmentCompanyProfileItem> itemList = new List<ManagedInvestmentCompanyProfileItem>();

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in accounts)
                {
                    assets.AddRange(account.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList());
                }
                var managedAssets = assets.OfType<ManagedInvestment>();

                var ratios = assets.GetAverageRatiosFor<ManagedInvestment>();

                foreach (var asset in managedAssets)
                {
                    itemList.Add(new ManagedInvestmentCompanyProfileItem
                    {
                        ticker = asset.Ticker,
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

                ManagedInvestmentCompanyProfileModel model = new ManagedInvestmentCompanyProfileModel
                {
                    data = itemList,
                    totalCostInvestment = assets.GetTotalCost().Total,
                    totalMarketValue = assets.GetTotalMarketValue()
                };
                return model;

                //return repo.AustralianEquity_GetCompanyProfiles_Client(clientUserId);
            }

            //return repo.ManagedInvestment_GetCompanyProfiles_Adviser(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/ManagedInvestmentPortfolio/CompanyProfiles")]
        public ManagedInvestmentCompanyProfileModel GetCompanyProfiles_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);


                List<ManagedInvestmentCompanyProfileItem> itemList = new List<ManagedInvestmentCompanyProfileItem>();

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in groupAccounts)
                {
                    assets.AddRange(account.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList());
                }
                foreach (var account in clientAccounts)
                {
                    assets.AddRange(account.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList());
                }
                var managedAssets = assets.OfType<ManagedInvestment>();

                var ratios = assets.GetAverageRatiosFor<ManagedInvestment>();

                foreach (var asset in managedAssets)
                {
                    itemList.Add(new ManagedInvestmentCompanyProfileItem
                    {
                        ticker = asset.Ticker,
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

                ManagedInvestmentCompanyProfileModel model = new ManagedInvestmentCompanyProfileModel
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

                List<ManagedInvestmentCompanyProfileItem> itemList = new List<ManagedInvestmentCompanyProfileItem>();

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in accounts)
                {
                    assets.AddRange(account.GetAssetsSync().OfType<ManagedInvestment>().Cast<AssetBase>().ToList());
                }
                var managedAssets = assets.OfType<ManagedInvestment>();

                var ratios = assets.GetAverageRatiosFor<ManagedInvestment>();

                foreach (var asset in managedAssets)
                {
                    itemList.Add(new ManagedInvestmentCompanyProfileItem
                    {
                        ticker = asset.Ticker,
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

                ManagedInvestmentCompanyProfileModel model = new ManagedInvestmentCompanyProfileModel
                {
                    data = itemList,
                    totalCostInvestment = assets.GetTotalCost().Total,
                    totalMarketValue = assets.GetTotalMarketValue()
                };
                return model;
            }
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
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

                double assetsSuitability = 0;
                double percentage = 0;

                foreach (var account in groupAccounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<ManagedInvestment>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((ManagedInvestment)w.Weightable).GetRating().TotalScore);

                    //var weigthings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();

                    //% of not suitable assets
                    percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                        / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());
                }

                foreach (var account in clientAccounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<ManagedInvestment>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((ManagedInvestment)w.Weightable).GetRating().TotalScore);

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
                    var equityWithResearchValues = account.GetAssetsSync().OfType<ManagedInvestment>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((ManagedInvestment)w.Weightable).GetRating().TotalScore);

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
        [HttpGet, Route("api/Client/ManagedInvestmentPortfolio/Rating")]
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
                    var equityWithResearchValues = account.GetAssetsSync().OfType<ManagedInvestment>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((ManagedInvestment)w.Weightable).GetRating().TotalScore);

                    //var weigthings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();

                    //% of not suitable assets
                    percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                        / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());
                }

                foreach (var account in clientAccounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<ManagedInvestment>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((ManagedInvestment)w.Weightable).GetRating().TotalScore);

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
                    var equityWithResearchValues = account.GetAssetsSync().OfType<ManagedInvestment>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((ManagedInvestment)w.Weightable).GetRating().TotalScore);

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





    }
}
