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
using Domain.Portfolio.AggregateRoots;
using SqlRepository;
using Domain.Portfolio.AggregateRoots.Accounts;
using Domain.Portfolio.AggregateRoots.Liability;
using Domain.Portfolio.Entities.Activity;
using Domain.Portfolio.Values.Cashflow;
using Domain.Portfolio.Services;
using Shared;

namespace EDISAngular.APIControllers
{
    public class PortfolioMortgageInvestmentController : ApiController
    {
        private PortfolioRepository repo = new PortfolioRepository();
        private EdisRepository edisRepo = new EdisRepository();
        [HttpGet, Route("api/Adviser/MortgageInvestmentPortfolio/General")]
        public MortgageInvestmentGeneralInfo GetGeneralInfo_Adviser(string clientGroupId = null)
        {
            if (string.IsNullOrEmpty(clientGroupId)) 
            {

                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

                List<LiabilityBase> liabilities = new List<LiabilityBase>();

                double marketValue = 0;
                double outstandingLoans = 0;
                double propertyGearingRatio = 0;
                double monthlyRepayment = 0;

                foreach (var account in groupAccounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                foreach (var account in clientAccounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                var mahs = liabilities.OfType<MortgageAndHomeLiability>();

                foreach (var mah in mahs)
                {
                    foreach (var activity in mah.GetActivitiesSync().OfType<FinancialActivity>())
                    {
                        monthlyRepayment += activity.Incomes.Sum(i => i.Amount);
                    }
                    marketValue += mah.Property.GetTotalMarketValue();
                    propertyGearingRatio += mah.CurrentPropertyGearingRatio;
                    outstandingLoans += mah.CurrentBalance;
                }
                MortgageInvestmentGeneralInfo info = new MortgageInvestmentGeneralInfo
                {
                    marketValue = marketValue,
                    propertyGearingRatio = propertyGearingRatio,
                    outstandingLoans = outstandingLoans,
                    monthlyRepayment = monthlyRepayment
                };

                return info;
            }
            else 
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);

                List<LiabilityBase> liabilities = new List<LiabilityBase>();

                double marketValue = 0;
                double outstandingLoans = 0;
                double propertyGearingRatio = 0;
                double monthlyRepayment = 0;

                foreach (var account in accounts) {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                var mahs = liabilities.OfType<MortgageAndHomeLiability>();

                foreach (var mah in mahs) {
                    foreach (var activity in mah.GetActivitiesSync().OfType<FinancialActivity>())
                    {
                        monthlyRepayment += activity.Incomes.Sum(i => i.Amount);
                    }
                    marketValue += mah.Property.GetTotalMarketValue();
                    propertyGearingRatio += mah.CurrentPropertyGearingRatio;
                    outstandingLoans += mah.CurrentBalance;
                }
                MortgageInvestmentGeneralInfo info = new MortgageInvestmentGeneralInfo { 
                    marketValue = marketValue,
                    propertyGearingRatio = propertyGearingRatio,
                    outstandingLoans = outstandingLoans,
                    monthlyRepayment = monthlyRepayment
                };

                return info;

            }
            //return repo.Mortgate_GetGeneralInfo_Adviser(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/MortgageInvestmentPortfolio/General")]
        public MortgageInvestmentGeneralInfo GetGeneralInfo_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                List<LiabilityBase> liabilities = new List<LiabilityBase>();

                double marketValue = 0;
                double outstandingLoans = 0;
                double propertyGearingRatio = 0;
                double monthlyRepayment = 0;

                foreach (var account in groupAccounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                foreach (var account in clientAccounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                var mahs = liabilities.OfType<MortgageAndHomeLiability>();

                foreach (var mah in mahs)
                {
                    foreach (var activity in mah.GetActivitiesSync().OfType<FinancialActivity>())
                    {
                        monthlyRepayment += activity.Incomes.Sum(i => i.Amount);
                    }
                    marketValue += mah.Property.GetTotalMarketValue();
                    propertyGearingRatio += mah.CurrentPropertyGearingRatio;
                    outstandingLoans += mah.CurrentBalance;
                }
                MortgageInvestmentGeneralInfo info = new MortgageInvestmentGeneralInfo
                {
                    marketValue = marketValue,
                    propertyGearingRatio = propertyGearingRatio,
                    outstandingLoans = outstandingLoans,
                    monthlyRepayment = monthlyRepayment
                };

                return info;
            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                List<LiabilityBase> liabilities = new List<LiabilityBase>();

                double marketValue = 0;
                double outstandingLoans = 0;
                double propertyGearingRatio = 0;
                double monthlyRepayment = 0;

                foreach (var account in accounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                var mahs = liabilities.OfType<MortgageAndHomeLiability>();

                foreach (var mah in mahs)
                {
                    foreach (var activity in mah.GetActivitiesSync().OfType<FinancialActivity>())
                    {
                        monthlyRepayment += activity.Incomes.Sum(i => i.Amount);
                    }
                    marketValue += mah.Property.GetTotalMarketValue();
                    propertyGearingRatio += mah.CurrentPropertyGearingRatio;
                    outstandingLoans += mah.CurrentBalance;
                }
                MortgageInvestmentGeneralInfo info = new MortgageInvestmentGeneralInfo
                {
                    marketValue = marketValue,
                    propertyGearingRatio = propertyGearingRatio,
                    outstandingLoans = outstandingLoans,
                    monthlyRepayment = monthlyRepayment
                };

                return info;
            }
        }

        [HttpGet, Route("api/Adviser/MortgageInvestmentPortfolio/Cashflow")]
        public CashflowBriefModel GetCashflowSummary_Adviser(string clientGroupId = null)
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

                    List<Cashflow> cashFlows = account.GetLiabilitiesSync().OfType<MortgageAndHomeLiability>().Cast<LiabilityBase>().ToList().GetMonthlyCashflows();

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
                    List<Cashflow> cashFlows = account.GetLiabilitiesSync().OfType<MortgageAndHomeLiability>().Cast<LiabilityBase>().ToList().GetMonthlyCashflows();

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
                    //List<Cashflow> cashFlows = account.GetAssetsSync().GetMonthlyCashflows();

                    List<Cashflow> cashFlows = account.GetLiabilitiesSync().OfType<MortgageAndHomeLiability>().Cast<LiabilityBase>().ToList().GetMonthlyCashflows();

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



            //return repo.Mortgate_GetCashflowSummary_Adviser(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/MortgageInvestmentPortfolio/Cashflow")]
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

                    List<Cashflow> cashFlows = account.GetLiabilitiesSync().OfType<MortgageAndHomeLiability>().Cast<LiabilityBase>().ToList().GetMonthlyCashflows();

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
                    List<Cashflow> cashFlows = account.GetLiabilitiesSync().OfType<MortgageAndHomeLiability>().Cast<LiabilityBase>().ToList().GetMonthlyCashflows();

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
                    //List<Cashflow> cashFlows = account.GetAssetsSync().GetMonthlyCashflows();

                    List<Cashflow> cashFlows = account.GetLiabilitiesSync().OfType<MortgageAndHomeLiability>().Cast<LiabilityBase>().ToList().GetMonthlyCashflows();

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

        [HttpGet, Route("api/Adviser/MortgageInvestmentPortfolio/CashflowDetailed")]
        public CashflowBriefModel GetDetailedCashflow_Adviser(string clientGroupId = null)
        {
            return repo.Mortgate_GetCashflowDetails_Adviser(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/MortgageInvestmentPortfolio/CashflowDetailed")]
        public MortgageCashflowDetailedModel GetDetailedCashflow_Client()
        {
            return repo.Mortgate_GetCashflowDetails_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/MortgageInvestmentPortfolio/Stats")]
        public MortgageInvestmentStatModel GetStats_Adviser(string clientGroupId = null)
        {
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);


                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in groupAccounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                foreach (var account in clientAccounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }

                MortgageInvestmentStatModel model = new MortgageInvestmentStatModel
                {
                    Combination = 0,
                    Fixed = 0,
                    NotSpecified = 0,
                    Variable = 0
                };

                var mortgageGroups = liabilities.OfType<MortgageAndHomeLiability>().GroupBy(l => l.TypeOfMortgageRates);
                foreach (var mortgageGroup in mortgageGroups)
                {
                    var mortgage = mortgageGroup.FirstOrDefault();
                    switch (mortgageGroup.Key)
                    {
                        case TypeOfMortgageRates.Combination:
                            model.Combination += mortgage.Property.GetTotalMarketValue();
                            break;
                        case TypeOfMortgageRates.Fixed:
                            model.Fixed += mortgage.Property.GetTotalMarketValue();
                            break;
                        case TypeOfMortgageRates.NotSpecified:
                            model.NotSpecified += mortgage.Property.GetTotalMarketValue();
                            break;
                        case TypeOfMortgageRates.Variable:
                            model.Variable += mortgage.Property.GetTotalMarketValue();
                            break;
                    }
                }
                return model;
            }
            else 
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);

                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in accounts) {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }

                MortgageInvestmentStatModel model = new MortgageInvestmentStatModel { 
                    Combination = 0,
                    Fixed = 0,
                    NotSpecified = 0,
                    Variable = 0
                };

                var mortgageGroups = liabilities.OfType<MortgageAndHomeLiability>().GroupBy(l => l.TypeOfMortgageRates);
                foreach (var mortgageGroup in mortgageGroups)
                {
                    var mortgage = mortgageGroup.FirstOrDefault();
                    switch(mortgageGroup.Key){
                        case TypeOfMortgageRates.Combination : 
                            model.Combination += mortgage.Property.GetTotalMarketValue();
                            break;
                        case TypeOfMortgageRates.Fixed :
                            model.Fixed += mortgage.Property.GetTotalMarketValue();
                            break;
                        case TypeOfMortgageRates.NotSpecified :
                            model.NotSpecified += mortgage.Property.GetTotalMarketValue();
                            break;
                        case TypeOfMortgageRates.Variable :
                            model.Variable += mortgage.Property.GetTotalMarketValue();
                            break;
                    }
                }

                return model;
            }
            //return repo.Mortgage_GetStats_Adviser(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/MortgageInvestmentPortfolio/Stats")]
        public MortgageInvestmentStatModel GetStats_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);


                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in groupAccounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                foreach (var account in clientAccounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }

                MortgageInvestmentStatModel model = new MortgageInvestmentStatModel
                {
                    Combination = 0,
                    Fixed = 0,
                    NotSpecified = 0,
                    Variable = 0
                };

                var mortgageGroups = liabilities.OfType<MortgageAndHomeLiability>().GroupBy(l => l.TypeOfMortgageRates);
                foreach (var mortgageGroup in mortgageGroups)
                {
                    var mortgage = mortgageGroup.FirstOrDefault();
                    switch (mortgageGroup.Key)
                    {
                        case TypeOfMortgageRates.Combination:
                            model.Combination += mortgage.Property.GetTotalMarketValue();
                            break;
                        case TypeOfMortgageRates.Fixed:
                            model.Fixed += mortgage.Property.GetTotalMarketValue();
                            break;
                        case TypeOfMortgageRates.NotSpecified:
                            model.NotSpecified += mortgage.Property.GetTotalMarketValue();
                            break;
                        case TypeOfMortgageRates.Variable:
                            model.Variable += mortgage.Property.GetTotalMarketValue();
                            break;
                    }
                }
                return model;
            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in accounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }

                MortgageInvestmentStatModel model = new MortgageInvestmentStatModel
                {
                    Combination = 0,
                    Fixed = 0,
                    NotSpecified = 0,
                    Variable = 0
                };

                var mortgageGroups = liabilities.OfType<MortgageAndHomeLiability>().GroupBy(l => l.TypeOfMortgageRates);
                foreach (var mortgageGroup in mortgageGroups)
                {
                    var mortgage = mortgageGroup.FirstOrDefault();
                    switch (mortgageGroup.Key)
                    {
                        case TypeOfMortgageRates.Combination:
                            model.Combination += mortgage.Property.GetTotalMarketValue();
                            break;
                        case TypeOfMortgageRates.Fixed:
                            model.Fixed += mortgage.Property.GetTotalMarketValue();
                            break;
                        case TypeOfMortgageRates.NotSpecified:
                            model.NotSpecified += mortgage.Property.GetTotalMarketValue();
                            break;
                        case TypeOfMortgageRates.Variable:
                            model.Variable += mortgage.Property.GetTotalMarketValue();
                            break;
                    }
                }

                return model;
            }
        }

        [HttpGet, Route("api/Adviser/MortgageInvestmentPortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Adviser(string clientGroupId = null)
        {
            return repo.Mortgage_GetPortfolioRating_Adviser(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/MortgageInvestmentPortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Client()
        {
            return repo.Mortgage_GetPortfolioRating_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/MortgageInvestmentPortfolio/Profiles")]
        public MortgageInvestmentProfileModel GetProfiles_Adviser(string clientGroupId = null)
        {

            if (string.IsNullOrEmpty(clientGroupId))
            {

                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                MortgageInvestmentProfileModel model = new MortgageInvestmentProfileModel { data = new List<MortgageInvestmentProfileItem>() };

                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in groupAccounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                foreach (var account in clientAccounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                var mortgageAndHomes = liabilities.OfType<MortgageAndHomeLiability>();
                foreach (var mortgageAndHome in mortgageAndHomes)
                {
                    double monthlyRepayment = 0;
                    foreach (var activity in mortgageAndHome.GetActivitiesSync().OfType<FinancialActivity>())
                    {
                        monthlyRepayment += activity.Incomes.Sum(i => i.Amount);
                    }
                    model.data.Add(new MortgageInvestmentProfileItem
                    {
                        propertyName = mortgageAndHome.Property.PropertyType,
                        address = mortgageAndHome.Property.FullAddress,
                        currency = mortgageAndHome.CurrencyType.ToString(),
                        marketValue = mortgageAndHome.Property.GetTotalMarketValue(),
                        outstandingLoan = mortgageAndHome.CurrentBalance,
                        currentPropertyGearingRatio = mortgageAndHome.CurrentPropertyGearingRatio,
                        institution = mortgageAndHome.LoanProviderInstitution,
                        typeOfRates = mortgageAndHome.TypeOfMortgageRates.ToString(),
                        monthlyRepaymentAmount = monthlyRepayment,
                        loanContractTerm = mortgageAndHome.LoanContractTermInYears,
                        loanExpiryDate = mortgageAndHome.ExpiryDate,
                        RepaymentType = mortgageAndHome.LoanRepaymentType.ToString(),
                        currentLoanBalance = mortgageAndHome.CurrentBalance,
                        currentFinancialYearInterest = mortgageAndHome.CurrentFiancialYearInterest
                    });
                }
                return model;
            }
            else 
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                MortgageInvestmentProfileModel model = new MortgageInvestmentProfileModel { data = new List<MortgageInvestmentProfileItem>() };

                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in accounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                var mortgageAndHomes = liabilities.OfType<MortgageAndHomeLiability>();
                foreach(var mortgageAndHome in mortgageAndHomes){
                    double monthlyRepayment = 0;
                    foreach (var activity in mortgageAndHome.GetActivitiesSync().OfType<FinancialActivity>())
                    {
                        monthlyRepayment += activity.Incomes.Sum(i => i.Amount);
                    }
                    model.data.Add(new MortgageInvestmentProfileItem { 
                        propertyName = mortgageAndHome.Property.PropertyType,
                        address = mortgageAndHome.Property.FullAddress,
                        currency = mortgageAndHome.CurrencyType.ToString(),
                        marketValue = mortgageAndHome.Property.GetTotalMarketValue(),
                        outstandingLoan = mortgageAndHome.CurrentBalance,
                        currentPropertyGearingRatio = mortgageAndHome.CurrentPropertyGearingRatio,
                        institution = mortgageAndHome.LoanProviderInstitution,
                        typeOfRates = mortgageAndHome.TypeOfMortgageRates.ToString(),
                        monthlyRepaymentAmount = monthlyRepayment,
                        loanContractTerm = mortgageAndHome.LoanContractTermInYears,
                        loanExpiryDate = mortgageAndHome.ExpiryDate,
                        RepaymentType = mortgageAndHome.LoanRepaymentType.ToString(),
                        currentLoanBalance = mortgageAndHome.CurrentBalance,
                        currentFinancialYearInterest = mortgageAndHome.CurrentFiancialYearInterest
                    });
                }
                return model;
            }
            //return repo.Mortgage_GetProfiles_Adviser(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/MortgageInvestmentPortfolio/Profiles")]
        public MortgageInvestmentProfileModel GetProfiles_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                MortgageInvestmentProfileModel model = new MortgageInvestmentProfileModel { data = new List<MortgageInvestmentProfileItem>() };

                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in groupAccounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                foreach (var account in clientAccounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                var mortgageAndHomes = liabilities.OfType<MortgageAndHomeLiability>();
                foreach (var mortgageAndHome in mortgageAndHomes)
                {
                    double monthlyRepayment = 0;
                    foreach (var activity in mortgageAndHome.GetActivitiesSync().OfType<FinancialActivity>())
                    {
                        monthlyRepayment += activity.Incomes.Sum(i => i.Amount);
                    }
                    model.data.Add(new MortgageInvestmentProfileItem
                    {
                        propertyName = mortgageAndHome.Property.PropertyType,
                        address = mortgageAndHome.Property.FullAddress,
                        currency = mortgageAndHome.CurrencyType.ToString(),
                        marketValue = mortgageAndHome.Property.GetTotalMarketValue(),
                        outstandingLoan = mortgageAndHome.CurrentBalance,
                        currentPropertyGearingRatio = mortgageAndHome.CurrentPropertyGearingRatio,
                        institution = mortgageAndHome.LoanProviderInstitution,
                        typeOfRates = mortgageAndHome.TypeOfMortgageRates.ToString(),
                        monthlyRepaymentAmount = monthlyRepayment,
                        loanContractTerm = mortgageAndHome.LoanContractTermInYears,
                        loanExpiryDate = mortgageAndHome.ExpiryDate,
                        RepaymentType = mortgageAndHome.LoanRepaymentType.ToString(),
                        currentLoanBalance = mortgageAndHome.CurrentBalance,
                        currentFinancialYearInterest = mortgageAndHome.CurrentFiancialYearInterest
                    });
                }
                return model;
            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                MortgageInvestmentProfileModel model = new MortgageInvestmentProfileModel { data = new List<MortgageInvestmentProfileItem>() };

                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in accounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                var mortgageAndHomes = liabilities.OfType<MortgageAndHomeLiability>();
                foreach (var mortgageAndHome in mortgageAndHomes)
                {
                    double monthlyRepayment = 0;
                    foreach (var activity in mortgageAndHome.GetActivitiesSync().OfType<FinancialActivity>())
                    {
                        monthlyRepayment += activity.Incomes.Sum(i => i.Amount);
                    }
                    model.data.Add(new MortgageInvestmentProfileItem
                    {
                        propertyName = mortgageAndHome.Property.PropertyType,
                        address = mortgageAndHome.Property.FullAddress,
                        currency = mortgageAndHome.CurrencyType.ToString(),
                        marketValue = mortgageAndHome.Property.GetTotalMarketValue(),
                        outstandingLoan = mortgageAndHome.CurrentBalance,
                        currentPropertyGearingRatio = mortgageAndHome.CurrentPropertyGearingRatio,
                        institution = mortgageAndHome.LoanProviderInstitution,
                        typeOfRates = mortgageAndHome.TypeOfMortgageRates.ToString(),
                        monthlyRepaymentAmount = monthlyRepayment,
                        loanContractTerm = mortgageAndHome.LoanContractTermInYears,
                        loanExpiryDate = mortgageAndHome.ExpiryDate,
                        RepaymentType = mortgageAndHome.LoanRepaymentType.ToString(),
                        currentLoanBalance = mortgageAndHome.CurrentBalance,
                        currentFinancialYearInterest = mortgageAndHome.CurrentFiancialYearInterest
                    });
                }
                return model;
            }
        }


    }
}
