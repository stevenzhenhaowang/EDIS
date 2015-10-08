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
using Domain.Portfolio.AggregateRoots.Asset;
using Domain.Portfolio.AggregateRoots;
using Domain.Portfolio.Services;
using Shared;
using Domain.Portfolio.Values.Cashflow;

namespace EDISAngular.APIControllers
{
    public class PortfolioCashTermDepositController : ApiController
    {
        private PortfolioRepository repo = new PortfolioRepository();
        private EdisRepository edisRepo = new EdisRepository();
        [HttpGet, Route("api/Adviser/CashTermDepositPortfolio/General")]
        public CashTermDepositGeneralInfoModel GetGeneralInfo_Adviser(string clientGroupId = null)
        {
            if (string.IsNullOrEmpty(clientGroupId))
            {

                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<AssetBase> assets = new List<AssetBase>();
                CashTermDepositGeneralInfoModel model = new CashTermDepositGeneralInfoModel
                {
                    annualInterest = 0,
                    annualYield = 0,
                    cashAtMaturity = 0,
                    cashDeposit = 0,
                    consumerPriceIndex = 0,
                    rbaRate = 0
                };

                foreach (var account in groupAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }
                foreach (var account in clientAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }

                var cashes = assets.OfType<Cash>();
                foreach (var cash in cashes)
                {
                    model.annualInterest += cash.AnnualInterest == 0 ? 0 : (double)cash.AnnualInterest;
                    model.consumerPriceIndex += cash.LatestPrice;
                }

                return model;
                //return repo.TermDeposit_GetGeneral_Adviser(User.Identity.GetUserId());
            }
            else
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<AssetBase> assets = new List<AssetBase>();
                CashTermDepositGeneralInfoModel model = new CashTermDepositGeneralInfoModel { 
                    annualInterest = 0,
                    annualYield = 0,
                    cashAtMaturity = 0,
                    cashDeposit = 0,
                    consumerPriceIndex = 0,
                    rbaRate = 0
                };

                foreach (var account in accounts) {
                    assets.AddRange(account.GetAssetsSync());
                }

                var cashes = assets.OfType<Cash>();
                foreach (var cash in cashes) {
                    model.annualInterest += cash.AnnualInterest == 0 ? 0 : (double)cash.AnnualInterest;
                    model.consumerPriceIndex += cash.LatestPrice;
                }

                return model;

                //return repo.TermDeposit_GetGeneral_Client(clientGroupId);
            }

        }
        [HttpGet, Route("api/Client/CashTermDepositPortfolio/General")]
        public CashTermDepositGeneralInfoModel GetGeneralInfo_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                List<AssetBase> assets = new List<AssetBase>();
                CashTermDepositGeneralInfoModel model = new CashTermDepositGeneralInfoModel
                {
                    annualInterest = 0,
                    annualYield = 0,
                    cashAtMaturity = 0,
                    cashDeposit = 0,
                    consumerPriceIndex = 0,
                    rbaRate = 0
                };

                foreach (var account in groupAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }
                foreach (var account in clientAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }

                var cashes = assets.OfType<Cash>();
                foreach (var cash in cashes)
                {
                    model.annualInterest += cash.AnnualInterest == 0 ? 0 : (double)cash.AnnualInterest;
                    model.consumerPriceIndex += cash.LatestPrice;
                }

                return model;
            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                List<AssetBase> assets = new List<AssetBase>();
                CashTermDepositGeneralInfoModel model = new CashTermDepositGeneralInfoModel
                {
                    annualInterest = 0,
                    annualYield = 0,
                    cashAtMaturity = 0,
                    cashDeposit = 0,
                    consumerPriceIndex = 0,
                    rbaRate = 0
                };

                foreach (var account in accounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }

                var cashes = assets.OfType<Cash>();
                foreach (var cash in cashes)
                {
                    model.annualInterest += cash.AnnualInterest == 0 ? 0 : (double)cash.AnnualInterest;
                    model.consumerPriceIndex += cash.LatestPrice;
                }

                return model;
            }
        }

        [HttpGet, Route("api/Adviser/CashTermDepositPortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Adviser(string clientGroupId = null)
        {
            return null;

        }
        [HttpGet, Route("api/Client/CashTermDepositPortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Client()
        {
            return repo.TermDeposit_GetPortfolioRating_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/CashTermDepositPortfolio/Price")]
        public TermDepositPriceChartModel GetPrice_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.TermDeposit_GetPriceData_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.TermDeposit_GetPriceData_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/CashTermDepositPortfolio/Price")]
        public TermDepositPriceChartModel GetPrice_Client()
        {
            return repo.TermDeposit_GetPriceData_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/CashTermDepositPortfolio/Cashflow")]
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


                foreach (var account in groupAccounts)
                {
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<Cash>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<Cash>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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

                //return repo.AustralianEquity_GetCashflowSummary_Adviser(User.Identity.GetUserId());
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
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<Cash>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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


                //return repo.AustralianEquity_GetCashflowSummary_Client(clientUserId);
            }
        }
        [HttpGet, Route("api/Client/CashTermDepositPortfolio/Cashflow")]
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
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<Cash>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<Cash>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<Cash>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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

        [HttpGet, Route("api/Adviser/CashTermDepositPortfolio/Profiles")]
        public CashTermDepositProfileModel GetProfiles_Adviser(string clientGroupId = null)
        {
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

                CashTermDepositProfileModel model = new CashTermDepositProfileModel { data = new List<CashTermDepositProfileItem>() };

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in groupAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }
                foreach (var account in clientAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }
                var cashes = assets.OfType<Cash>();
                foreach (var cash in cashes)
                {
                    CashTermDepositProfileItem item = new CashTermDepositProfileItem
                    {
                        accountName = cash.CashAccountName,
                        accountNumber = cash.CashAccountNumber,
                        accountType = cash.CashAccountType.ToString(),
                        accruedInterest = cash.InterestRate == null ? 0 : (double)cash.InterestRate,
                        annualInterest = cash.AnnualInterest == null ? 0 : (double)cash.AnnualInterest,
                        faceValue = cash.FaceValue,
                        interestFrequency = cash.InterestFrequency.ToString(),
                        bsb = cash.Bsb,
                        maturityDate = cash.MaturityDate == null ? DateTime.Now : (DateTime)cash.MaturityDate,
                        termOfRates = cash.TermOfRatesMonth.ToString(),
                        interestRate = cash.InterestRate == null ? 0 : (double)cash.InterestRate
                    };
                    model.data.Add(item);
                }

                return model;
                //return repo.TermDeposit_GetProfiles_Adviser(User.Identity.GetUserId());
            }
            else
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                CashTermDepositProfileModel model = new CashTermDepositProfileModel { data = new List<CashTermDepositProfileItem>() };

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in accounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }
                var cashes = assets.OfType<Cash>();
                foreach (var cash in cashes)
                {
                    CashTermDepositProfileItem item = new CashTermDepositProfileItem
                    {
                        accountName = cash.CashAccountName,
                        accountNumber = cash.CashAccountNumber,
                        accountType = cash.CashAccountType.ToString(),
                        accruedInterest = cash.InterestRate == null ? 0 : (double)cash.InterestRate,
                        annualInterest = cash.AnnualInterest == null ? 0 : (double)cash.AnnualInterest,
                        faceValue = cash.FaceValue,
                        interestFrequency = cash.InterestFrequency.ToString(),
                        bsb = cash.Bsb,
                        maturityDate = cash.MaturityDate == null ? DateTime.Now : (DateTime)cash.MaturityDate,
                        termOfRates = cash.TermOfRatesMonth.ToString(),
                        interestRate = cash.InterestRate == null ? 0 : (double)cash.InterestRate
                    };
                    model.data.Add(item);
                }

                return model;
                //return repo.TermDeposit_GetProfiles_Client(clientGroupId);
            }

        }
        [HttpGet, Route("api/Client/CashTermDepositPortfolio/Profiles")]
        public CashTermDepositProfileModel GetProfiles_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);


                CashTermDepositProfileModel model = new CashTermDepositProfileModel { data = new List<CashTermDepositProfileItem>() };

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in groupAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }
                foreach (var account in clientAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }
                var cashes = assets.OfType<Cash>();
                foreach (var cash in cashes)
                {
                    CashTermDepositProfileItem item = new CashTermDepositProfileItem
                    {
                        accountName = cash.CashAccountName,
                        accountNumber = cash.CashAccountNumber,
                        accountType = cash.CashAccountType.ToString(),
                        accruedInterest = cash.InterestRate == null ? 0 : (double)cash.InterestRate,
                        annualInterest = cash.AnnualInterest == null ? 0 : (double)cash.AnnualInterest,
                        faceValue = cash.FaceValue,
                        interestFrequency = cash.InterestFrequency.ToString(),
                        bsb = cash.Bsb,
                        maturityDate = cash.MaturityDate == null ? DateTime.Now : (DateTime)cash.MaturityDate,
                        termOfRates = cash.TermOfRatesMonth.ToString(),
                        interestRate = cash.InterestRate == null ? 0 : (double)cash.InterestRate
                    };
                    model.data.Add(item);
                }

                return model;
            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                CashTermDepositProfileModel model = new CashTermDepositProfileModel { data = new List<CashTermDepositProfileItem>() };

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in accounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }
                var cashes = assets.OfType<Cash>();
                foreach (var cash in cashes)
                {
                    CashTermDepositProfileItem item = new CashTermDepositProfileItem
                    {
                        accountName = cash.CashAccountName,
                        accountNumber = cash.CashAccountNumber,
                        accountType = cash.CashAccountType.ToString(),
                        accruedInterest = cash.InterestRate == null ? 0 : (double)cash.InterestRate,
                        annualInterest = cash.AnnualInterest == null ? 0 : (double)cash.AnnualInterest,
                        faceValue = cash.FaceValue,
                        interestFrequency = cash.InterestFrequency.ToString(),
                        bsb = cash.Bsb,
                        maturityDate = cash.MaturityDate == null ? DateTime.Now : (DateTime)cash.MaturityDate,
                        termOfRates = cash.TermOfRatesMonth.ToString(),
                        interestRate = cash.InterestRate == null ? 0 : (double)cash.InterestRate
                    };
                    model.data.Add(item);
                }

                return model;
            }
        }

        [HttpGet, Route("api/Adviser/CashTermDepositPortfolio/Stats")]
        public IEnumerable<IncomeStatisticsModel> GetStats_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.TermDeposit_GetStats_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.TermDeposit_GetStats_Client(clientUserId);
            }
        }
        [HttpGet, Route("api/Client/CashTermDepositPortfolio/Stats")]
        public IEnumerable<IncomeStatisticsModel> GetStats_Client()
        {
            return repo.TermDeposit_GetStats_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/CashTermDepositPortfolio/Diversifications")]
        public IncomeDiversificationModel GetDiversification_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.TermDeposit_GetDiversification_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.TermDeposit_GetDiversification_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/CashTermDepositPortfolio/Diversifications")]
        public IncomeDiversificationModel GetDiversification_Client()
        {
            return repo.TermDeposit_GetDiversification_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/CashTermDepositPortfolio/CashflowDetailed")]
        public CashTermDepositCashflowDetailedModel GetDetailedCashflow_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.TermDeposit_GetCashflowDetails_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.TermDeposit_GetCashflowDetails_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/CashTermDepositPortfolio/CashflowDetailed")]
        public CashTermDepositCashflowDetailedModel GetDetailedCashflow_Client()
        {
            return repo.TermDeposit_GetCashflowDetails_Client(User.Identity.GetUserId());
        }

    }
}
