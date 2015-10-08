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
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

                List<AssetBase> assets = new List<AssetBase>();
                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in groupAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                foreach (var account in clientAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }


                PortfolioSummary summary = new PortfolioSummary
                {
                    investment = new SummaryItem
                    {
                        data = new List<DataNameAmountPair>
                        {
                            new DataNameAmountPair{ amount = assets.OfType<AustralianEquity>().Cast<AssetBase>().ToList().GetTotalMarketValue(), name="Australian Equity"},
                            new DataNameAmountPair { amount =  assets.OfType<InternationalEquity>().Cast<AssetBase>().ToList().GetTotalMarketValue(), name="International Equity"},
                            new DataNameAmountPair { amount =  assets.OfType<ManagedInvestment>().Cast<AssetBase>().ToList().GetTotalMarketValue(), name="Managed Investment"},
                        },
                    },
                    liability = new SummaryItem
                    {
                        data = new List<DataNameAmountPair>
                        {
                            //new DataNameAmountPair{amount =  liabilities.OfType<MortgageAndHomeLiability>().Cast<LiabilityBase>().ToList(),name="Mortgage & Investment Loans"},
                            //new DataNameAmountPair{amount=30000,name="Margin Loans"}
                        },
                        total = 60000
                    },
                    networth = new SummaryItem
                    {
                        data = new List<DataNameAmountPair>
                        {
                            //new DataNameAmountPair{amount=30000, name="Investor Equity"},
                            //new DataNameAmountPair{amount=500000, name="Non-Investment Asset"}
                        }
                    }
                };

                summary.investment.total = summary.investment.data.Sum(d => d.amount);
                summary.liability.total = summary.liability.data.Sum(d => d.amount);
                summary.networth.total = summary.networth.data.Sum(d => d.amount);

                return summary;


                //return repo.Overview_GetSummary_Adviser(User.Identity.GetUserId());
            }
            else
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);

                List<AssetBase> assets = new List<AssetBase>();
                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in accounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }


                PortfolioSummary summary =  new PortfolioSummary
                {
                    investment = new SummaryItem
                    {
                        data = new List<DataNameAmountPair>
                        {
                            new DataNameAmountPair{ amount = assets.OfType<AustralianEquity>().Cast<AssetBase>().ToList().GetTotalMarketValue(), name="Australian Equity"},
                            new DataNameAmountPair { amount =  assets.OfType<InternationalEquity>().Cast<AssetBase>().ToList().GetTotalMarketValue(), name="International Equity"},
                            new DataNameAmountPair { amount =  assets.OfType<ManagedInvestment>().Cast<AssetBase>().ToList().GetTotalMarketValue(), name="Managed Investment"},
                        },
                    },
                    liability = new SummaryItem
                    {
                        data = new List<DataNameAmountPair>
                        {
                            //new DataNameAmountPair{amount =  liabilities.OfType<MortgageAndHomeLiability>().Cast<LiabilityBase>().ToList(),name="Mortgage & Investment Loans"},
                            //new DataNameAmountPair{amount=30000,name="Margin Loans"}
                        },
                        total = 60000
                    },
                    networth = new SummaryItem
                    {
                        data = new List<DataNameAmountPair>
                        {
                            //new DataNameAmountPair{amount=30000, name="Investor Equity"},
                            //new DataNameAmountPair{amount=500000, name="Non-Investment Asset"}
                        }
                    }
                };

                summary.investment.total = summary.investment.data.Sum(d => d.amount);
                summary.liability.total = summary.investment.data.Sum(d => d.amount);
                summary.networth.total = summary.investment.data.Sum(d => d.amount);

                return summary;

                //return repo.Overview_GetSummary_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/PortfolioOverview/Summary")]
        public PortfolioSummary GetPortfolioSummary_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);


                List<AssetBase> assets = new List<AssetBase>();
                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in groupAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                foreach (var account in clientAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }


                PortfolioSummary summary = new PortfolioSummary
                {
                    investment = new SummaryItem
                    {
                        data = new List<DataNameAmountPair>
                        {
                            new DataNameAmountPair{ amount = assets.OfType<AustralianEquity>().Cast<AssetBase>().ToList().GetTotalMarketValue(), name="Australian Equity"},
                            new DataNameAmountPair { amount =  assets.OfType<InternationalEquity>().Cast<AssetBase>().ToList().GetTotalMarketValue(), name="International Equity"},
                            new DataNameAmountPair { amount =  assets.OfType<ManagedInvestment>().Cast<AssetBase>().ToList().GetTotalMarketValue(), name="Managed Investment"},
                        },
                    },
                    liability = new SummaryItem
                    {
                        data = new List<DataNameAmountPair>
                        {
                            //new DataNameAmountPair{amount =  liabilities.OfType<MortgageAndHomeLiability>().Cast<LiabilityBase>().ToList(),name="Mortgage & Investment Loans"},
                            //new DataNameAmountPair{amount=30000,name="Margin Loans"}
                        },
                        total = 60000
                    },
                    networth = new SummaryItem
                    {
                        data = new List<DataNameAmountPair>
                        {
                            //new DataNameAmountPair{amount=30000, name="Investor Equity"},
                            //new DataNameAmountPair{amount=500000, name="Non-Investment Asset"}
                        }
                    }
                };

                summary.investment.total = summary.investment.data.Sum(d => d.amount);
                summary.liability.total = summary.liability.data.Sum(d => d.amount);
                summary.networth.total = summary.networth.data.Sum(d => d.amount);

                return summary;
            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                List<AssetBase> assets = new List<AssetBase>();
                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in accounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }


                PortfolioSummary summary = new PortfolioSummary
                {
                    investment = new SummaryItem
                    {
                        data = new List<DataNameAmountPair>
                        {
                            new DataNameAmountPair{ amount = assets.OfType<AustralianEquity>().Cast<AssetBase>().ToList().GetTotalMarketValue(), name="Australian Equity"},
                            new DataNameAmountPair { amount =  assets.OfType<InternationalEquity>().Cast<AssetBase>().ToList().GetTotalMarketValue(), name="International Equity"},
                            new DataNameAmountPair { amount =  assets.OfType<ManagedInvestment>().Cast<AssetBase>().ToList().GetTotalMarketValue(), name="Managed Investment"},
                        },
                    },
                    liability = new SummaryItem
                    {
                        data = new List<DataNameAmountPair>
                        {
                            //new DataNameAmountPair{amount =  liabilities.OfType<MortgageAndHomeLiability>().Cast<LiabilityBase>().ToList(),name="Mortgage & Investment Loans"},
                            //new DataNameAmountPair{amount=30000,name="Margin Loans"}
                        },
                        total = 60000
                    },
                    networth = new SummaryItem
                    {
                        data = new List<DataNameAmountPair>
                        {
                            //new DataNameAmountPair{amount=30000, name="Investor Equity"},
                            //new DataNameAmountPair{amount=500000, name="Non-Investment Asset"}
                        }
                    }
                };

                summary.investment.total = summary.investment.data.Sum(d => d.amount);
                summary.liability.total = summary.investment.data.Sum(d => d.amount);
                summary.networth.total = summary.investment.data.Sum(d => d.amount);

                return summary;
            }

        }
        [HttpGet, Route("api/Adviser/PortfolioOverview/Cashflow")]
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

                    List<Cashflow> cashFlows = account.GetAssetsSync().GetMonthlyCashflows();

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
                    List<Cashflow> cashFlows = account.GetAssetsSync().GetMonthlyCashflows();

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

                    List<Cashflow> cashFlows = account.GetAssetsSync().GetMonthlyCashflows();

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
        [HttpGet, Route("api/Client/PortfolioOverview/Cashflow")]
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

                    List<Cashflow> cashFlows = account.GetAssetsSync().GetMonthlyCashflows();

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
                    List<Cashflow> cashFlows = account.GetAssetsSync().GetMonthlyCashflows();

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

                    List<Cashflow> cashFlows = account.GetAssetsSync().GetMonthlyCashflows();

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
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                double totalCost = 0;
                double totalMarketValue = 0;
                foreach (var account in groupAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync();

                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                    }
                }
                foreach (var account in clientAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync();
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
                    List<AssetBase> assets = account.GetAssetsSync();
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
        [HttpGet, Route("api/Client/PortfolioOverview/General")]
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
                    List<AssetBase> assets = account.GetAssetsSync();

                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                    }
                }
                foreach (var account in clientAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync();
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
                    List<AssetBase> assets = account.GetAssetsSync();
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
        [HttpGet, Route("api/Adviser/PortfolioOverview/Stastics")]
        public PortfolioStasticsModel GetStastics_Adviser(string clientGroupId = null)
        {

            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

                PortfolioStasticsModel model = new PortfolioStasticsModel { data = new List<PortfolioStasticsItem>() };

                List<AssetBase> assets = new List<AssetBase>();
                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in groupAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                foreach (var account in clientAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }

                List<AssetBase> aeAssets = assets.OfType<AustralianEquity>().Cast<AssetBase>().ToList();
                List<AssetBase> ieAssets = assets.OfType<InternationalEquity>().Cast<AssetBase>().ToList();
                List<AssetBase> miAssets = assets.OfType<ManagedInvestment>().Cast<AssetBase>().ToList();

                model.data.Add(new PortfolioStasticsItem
                {
                    assetClass = "Australian Equity",
                    costInvestment = aeAssets.Sum(a => a.GetCost().Total),
                    marketValue = aeAssets.Sum(a => a.GetTotalMarketValue()),
                    suitability = aeAssets.GetAssetWeightings().Sum(w => w.Percentage * ((AustralianEquity)w.Weightable).GetRating().TotalScore),
                    oneYearReturn = assets.GetAverageRatiosFor<AustralianEquity>().OneYearReturn,
                    threeYearReturn = assets.GetAverageRatiosFor<AustralianEquity>().ThreeYearReturn,
                    fiveYearReturn = assets.GetAverageRatiosFor<AustralianEquity>().FiveYearReturn,
                    earningsPerShare = assets.GetAverageRatiosFor<AustralianEquity>().EarningsStability,
                    dividend = assets.GetAverageRatiosFor<AustralianEquity>().DividendYield,
                    beta = assets.GetAverageRatiosFor<AustralianEquity>().Beta.ToString(),
                    returnOnAsset = assets.GetAverageRatiosFor<AustralianEquity>().ReturnOnAsset,
                    returnOnEquity = assets.GetAverageRatiosFor<AustralianEquity>().ReturnOnEquity,
                    priceEarningsRatio = assets.GetAverageRatiosFor<AustralianEquity>().PriceEarningRatio,
                    avMarketCap = assets.GetAverageRatiosFor<AustralianEquity>().Capitalisation.ToString()
                });

                model.data.Add(new PortfolioStasticsItem
                {
                    assetClass = "International Equity",
                    costInvestment = ieAssets.Sum(a => a.GetCost().Total),
                    marketValue = ieAssets.Sum(a => a.GetTotalMarketValue()),
                    suitability = ieAssets.GetAssetWeightings().Sum(w => w.Percentage * ((InternationalEquity)w.Weightable).GetRating().TotalScore),
                    oneYearReturn = ieAssets.GetAverageRatiosFor<InternationalEquity>().OneYearReturn,
                    threeYearReturn = ieAssets.GetAverageRatiosFor<InternationalEquity>().ThreeYearReturn,
                    fiveYearReturn = ieAssets.GetAverageRatiosFor<InternationalEquity>().FiveYearReturn,
                    earningsPerShare = ieAssets.GetAverageRatiosFor<InternationalEquity>().EarningsStability,
                    dividend = ieAssets.GetAverageRatiosFor<InternationalEquity>().DividendYield,
                    beta = ieAssets.GetAverageRatiosFor<InternationalEquity>().Beta.ToString(),
                    returnOnAsset = ieAssets.GetAverageRatiosFor<InternationalEquity>().ReturnOnAsset,
                    returnOnEquity = ieAssets.GetAverageRatiosFor<InternationalEquity>().ReturnOnEquity,
                    priceEarningsRatio = ieAssets.GetAverageRatiosFor<InternationalEquity>().PriceEarningRatio,
                    avMarketCap = ieAssets.GetAverageRatiosFor<InternationalEquity>().Capitalisation.ToString()
                });

                model.data.Add(new PortfolioStasticsItem
                {
                    assetClass = "Managed Investment",
                    costInvestment = miAssets.Sum(a => a.GetCost().Total),
                    marketValue = miAssets.Sum(a => a.GetTotalMarketValue()),
                    suitability = miAssets.GetAssetWeightings().Sum(w => w.Percentage * ((ManagedInvestment)w.Weightable).GetRating().TotalScore),
                    oneYearReturn = miAssets.GetAverageRatiosFor<ManagedInvestment>().OneYearReturn,
                    threeYearReturn = miAssets.GetAverageRatiosFor<ManagedInvestment>().ThreeYearReturn,
                    fiveYearReturn = miAssets.GetAverageRatiosFor<ManagedInvestment>().FiveYearReturn,
                    earningsPerShare = miAssets.GetAverageRatiosFor<ManagedInvestment>().EarningsStability,
                    dividend = miAssets.GetAverageRatiosFor<ManagedInvestment>().DividendYield,
                    beta = miAssets.GetAverageRatiosFor<ManagedInvestment>().Beta.ToString(),
                    returnOnAsset = miAssets.GetAverageRatiosFor<ManagedInvestment>().ReturnOnAsset,
                    returnOnEquity = miAssets.GetAverageRatiosFor<ManagedInvestment>().ReturnOnEquity,
                    priceEarningsRatio = miAssets.GetAverageRatiosFor<ManagedInvestment>().PriceEarningRatio,
                    avMarketCap = miAssets.GetAverageRatiosFor<ManagedInvestment>().Capitalisation.ToString()
                });


                foreach (var item in model.data)
                {
                    item.pl = item.marketValue - item.costInvestment;
                    item.plp = item.costInvestment == 0 ? 0 : (item.marketValue - item.costInvestment) / item.costInvestment;
                }

                return model;
            }
            else
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);

                PortfolioStasticsModel model = new PortfolioStasticsModel{ data = new List<PortfolioStasticsItem>()};

                List<AssetBase> assets = new List<AssetBase>();
                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in accounts) {
                    assets.AddRange(account.GetAssetsSync());
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }

                List<AssetBase> aeAssets = assets.OfType<AustralianEquity>().Cast<AssetBase>().ToList();
                List<AssetBase> ieAssets = assets.OfType<InternationalEquity>().Cast<AssetBase>().ToList();
                List<AssetBase> miAssets = assets.OfType<ManagedInvestment>().Cast<AssetBase>().ToList();

                model.data.Add(new PortfolioStasticsItem { 
                    assetClass = "Australian Equity",
                    costInvestment = aeAssets.Sum(a => a.GetCost().Total),
                    marketValue = aeAssets.Sum(a => a.GetTotalMarketValue()),
                    suitability = aeAssets.GetAssetWeightings().Sum(w => w.Percentage * ((AustralianEquity)w.Weightable).GetRating().TotalScore),
                    oneYearReturn = assets.GetAverageRatiosFor<AustralianEquity>().OneYearReturn,
                    threeYearReturn = assets.GetAverageRatiosFor<AustralianEquity>().ThreeYearReturn,
                    fiveYearReturn = assets.GetAverageRatiosFor<AustralianEquity>().FiveYearReturn,
                    earningsPerShare = assets.GetAverageRatiosFor<AustralianEquity>().EarningsStability,
                    dividend = assets.GetAverageRatiosFor<AustralianEquity>().DividendYield,
                    beta = assets.GetAverageRatiosFor<AustralianEquity>().Beta.ToString(),
                    returnOnAsset = assets.GetAverageRatiosFor<AustralianEquity>().ReturnOnAsset,
                    returnOnEquity = assets.GetAverageRatiosFor<AustralianEquity>().ReturnOnEquity,
                    priceEarningsRatio = assets.GetAverageRatiosFor<AustralianEquity>().PriceEarningRatio,
                    avMarketCap = assets.GetAverageRatiosFor<AustralianEquity>().Capitalisation.ToString()
                });

                model.data.Add(new PortfolioStasticsItem
                {
                    assetClass = "International Equity",
                    costInvestment = ieAssets.Sum(a => a.GetCost().Total),
                    marketValue = ieAssets.Sum(a => a.GetTotalMarketValue()),
                    suitability = ieAssets.GetAssetWeightings().Sum(w => w.Percentage * ((InternationalEquity)w.Weightable).GetRating().TotalScore),
                    oneYearReturn = ieAssets.GetAverageRatiosFor<InternationalEquity>().OneYearReturn,
                    threeYearReturn = ieAssets.GetAverageRatiosFor<InternationalEquity>().ThreeYearReturn,
                    fiveYearReturn = ieAssets.GetAverageRatiosFor<InternationalEquity>().FiveYearReturn,
                    earningsPerShare = ieAssets.GetAverageRatiosFor<InternationalEquity>().EarningsStability,
                    dividend = ieAssets.GetAverageRatiosFor<InternationalEquity>().DividendYield,
                    beta = ieAssets.GetAverageRatiosFor<InternationalEquity>().Beta.ToString(),
                    returnOnAsset = ieAssets.GetAverageRatiosFor<InternationalEquity>().ReturnOnAsset,
                    returnOnEquity = ieAssets.GetAverageRatiosFor<InternationalEquity>().ReturnOnEquity,
                    priceEarningsRatio = ieAssets.GetAverageRatiosFor<InternationalEquity>().PriceEarningRatio,
                    avMarketCap = ieAssets.GetAverageRatiosFor<InternationalEquity>().Capitalisation.ToString()
                });

                model.data.Add(new PortfolioStasticsItem
                {
                    assetClass = "Managed Investment",
                    costInvestment = miAssets.Sum(a => a.GetCost().Total),
                    marketValue = miAssets.Sum(a => a.GetTotalMarketValue()),
                    suitability = miAssets.GetAssetWeightings().Sum(w => w.Percentage * ((ManagedInvestment)w.Weightable).GetRating().TotalScore),
                    oneYearReturn = miAssets.GetAverageRatiosFor<ManagedInvestment>().OneYearReturn,
                    threeYearReturn = miAssets.GetAverageRatiosFor<ManagedInvestment>().ThreeYearReturn,
                    fiveYearReturn = miAssets.GetAverageRatiosFor<ManagedInvestment>().FiveYearReturn,
                    earningsPerShare = miAssets.GetAverageRatiosFor<ManagedInvestment>().EarningsStability,
                    dividend = miAssets.GetAverageRatiosFor<ManagedInvestment>().DividendYield,
                    beta = miAssets.GetAverageRatiosFor<ManagedInvestment>().Beta.ToString(),
                    returnOnAsset = miAssets.GetAverageRatiosFor<ManagedInvestment>().ReturnOnAsset,
                    returnOnEquity = miAssets.GetAverageRatiosFor<ManagedInvestment>().ReturnOnEquity,
                    priceEarningsRatio = miAssets.GetAverageRatiosFor<ManagedInvestment>().PriceEarningRatio,
                    avMarketCap = miAssets.GetAverageRatiosFor<ManagedInvestment>().Capitalisation.ToString()
                });
                

                foreach (var item in model.data)
                {
                    item.pl = item.marketValue - item.costInvestment;
                    item.plp = item.costInvestment == 0 ? 0 : (item.marketValue - item.costInvestment) / item.costInvestment;
                }
                return model;
            }
            //return repo.Overview_GetPortfolioStat_Adviser(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/PortfolioOverview/Stastics")]
        public PortfolioStasticsModel GetStastics_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);


                PortfolioStasticsModel model = new PortfolioStasticsModel { data = new List<PortfolioStasticsItem>() };

                List<AssetBase> assets = new List<AssetBase>();
                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in groupAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                foreach (var account in clientAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }

                List<AssetBase> aeAssets = assets.OfType<AustralianEquity>().Cast<AssetBase>().ToList();
                List<AssetBase> ieAssets = assets.OfType<InternationalEquity>().Cast<AssetBase>().ToList();
                List<AssetBase> miAssets = assets.OfType<ManagedInvestment>().Cast<AssetBase>().ToList();

                model.data.Add(new PortfolioStasticsItem
                {
                    assetClass = "Australian Equity",
                    costInvestment = aeAssets.Sum(a => a.GetCost().Total),
                    marketValue = aeAssets.Sum(a => a.GetTotalMarketValue()),
                    suitability = aeAssets.GetAssetWeightings().Sum(w => w.Percentage * ((AustralianEquity)w.Weightable).GetRating().TotalScore),
                    oneYearReturn = assets.GetAverageRatiosFor<AustralianEquity>().OneYearReturn,
                    threeYearReturn = assets.GetAverageRatiosFor<AustralianEquity>().ThreeYearReturn,
                    fiveYearReturn = assets.GetAverageRatiosFor<AustralianEquity>().FiveYearReturn,
                    earningsPerShare = assets.GetAverageRatiosFor<AustralianEquity>().EarningsStability,
                    dividend = assets.GetAverageRatiosFor<AustralianEquity>().DividendYield,
                    beta = assets.GetAverageRatiosFor<AustralianEquity>().Beta.ToString(),
                    returnOnAsset = assets.GetAverageRatiosFor<AustralianEquity>().ReturnOnAsset,
                    returnOnEquity = assets.GetAverageRatiosFor<AustralianEquity>().ReturnOnEquity,
                    priceEarningsRatio = assets.GetAverageRatiosFor<AustralianEquity>().PriceEarningRatio,
                    avMarketCap = assets.GetAverageRatiosFor<AustralianEquity>().Capitalisation.ToString()
                });

                model.data.Add(new PortfolioStasticsItem
                {
                    assetClass = "International Equity",
                    costInvestment = ieAssets.Sum(a => a.GetCost().Total),
                    marketValue = ieAssets.Sum(a => a.GetTotalMarketValue()),
                    suitability = ieAssets.GetAssetWeightings().Sum(w => w.Percentage * ((InternationalEquity)w.Weightable).GetRating().TotalScore),
                    oneYearReturn = ieAssets.GetAverageRatiosFor<InternationalEquity>().OneYearReturn,
                    threeYearReturn = ieAssets.GetAverageRatiosFor<InternationalEquity>().ThreeYearReturn,
                    fiveYearReturn = ieAssets.GetAverageRatiosFor<InternationalEquity>().FiveYearReturn,
                    earningsPerShare = ieAssets.GetAverageRatiosFor<InternationalEquity>().EarningsStability,
                    dividend = ieAssets.GetAverageRatiosFor<InternationalEquity>().DividendYield,
                    beta = ieAssets.GetAverageRatiosFor<InternationalEquity>().Beta.ToString(),
                    returnOnAsset = ieAssets.GetAverageRatiosFor<InternationalEquity>().ReturnOnAsset,
                    returnOnEquity = ieAssets.GetAverageRatiosFor<InternationalEquity>().ReturnOnEquity,
                    priceEarningsRatio = ieAssets.GetAverageRatiosFor<InternationalEquity>().PriceEarningRatio,
                    avMarketCap = ieAssets.GetAverageRatiosFor<InternationalEquity>().Capitalisation.ToString()
                });

                model.data.Add(new PortfolioStasticsItem
                {
                    assetClass = "Managed Investment",
                    costInvestment = miAssets.Sum(a => a.GetCost().Total),
                    marketValue = miAssets.Sum(a => a.GetTotalMarketValue()),
                    suitability = miAssets.GetAssetWeightings().Sum(w => w.Percentage * ((ManagedInvestment)w.Weightable).GetRating().TotalScore),
                    oneYearReturn = miAssets.GetAverageRatiosFor<ManagedInvestment>().OneYearReturn,
                    threeYearReturn = miAssets.GetAverageRatiosFor<ManagedInvestment>().ThreeYearReturn,
                    fiveYearReturn = miAssets.GetAverageRatiosFor<ManagedInvestment>().FiveYearReturn,
                    earningsPerShare = miAssets.GetAverageRatiosFor<ManagedInvestment>().EarningsStability,
                    dividend = miAssets.GetAverageRatiosFor<ManagedInvestment>().DividendYield,
                    beta = miAssets.GetAverageRatiosFor<ManagedInvestment>().Beta.ToString(),
                    returnOnAsset = miAssets.GetAverageRatiosFor<ManagedInvestment>().ReturnOnAsset,
                    returnOnEquity = miAssets.GetAverageRatiosFor<ManagedInvestment>().ReturnOnEquity,
                    priceEarningsRatio = miAssets.GetAverageRatiosFor<ManagedInvestment>().PriceEarningRatio,
                    avMarketCap = miAssets.GetAverageRatiosFor<ManagedInvestment>().Capitalisation.ToString()
                });


                foreach (var item in model.data)
                {
                    item.pl = item.marketValue - item.costInvestment;
                    item.plp = item.costInvestment == 0 ? 0 : (item.marketValue - item.costInvestment) / item.costInvestment;
                }

                return model;
            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                PortfolioStasticsModel model = new PortfolioStasticsModel { data = new List<PortfolioStasticsItem>() };

                List<AssetBase> assets = new List<AssetBase>();
                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in accounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }

                List<AssetBase> aeAssets = assets.OfType<AustralianEquity>().Cast<AssetBase>().ToList();
                List<AssetBase> ieAssets = assets.OfType<InternationalEquity>().Cast<AssetBase>().ToList();
                List<AssetBase> miAssets = assets.OfType<ManagedInvestment>().Cast<AssetBase>().ToList();

                model.data.Add(new PortfolioStasticsItem
                {
                    assetClass = "Australian Equity",
                    costInvestment = aeAssets.Sum(a => a.GetCost().Total),
                    marketValue = aeAssets.Sum(a => a.GetTotalMarketValue()),
                    suitability = aeAssets.GetAssetWeightings().Sum(w => w.Percentage * ((AustralianEquity)w.Weightable).GetRating().TotalScore),
                    oneYearReturn = assets.GetAverageRatiosFor<AustralianEquity>().OneYearReturn,
                    threeYearReturn = assets.GetAverageRatiosFor<AustralianEquity>().ThreeYearReturn,
                    fiveYearReturn = assets.GetAverageRatiosFor<AustralianEquity>().FiveYearReturn,
                    earningsPerShare = assets.GetAverageRatiosFor<AustralianEquity>().EarningsStability,
                    dividend = assets.GetAverageRatiosFor<AustralianEquity>().DividendYield,
                    beta = assets.GetAverageRatiosFor<AustralianEquity>().Beta.ToString(),
                    returnOnAsset = assets.GetAverageRatiosFor<AustralianEquity>().ReturnOnAsset,
                    returnOnEquity = assets.GetAverageRatiosFor<AustralianEquity>().ReturnOnEquity,
                    priceEarningsRatio = assets.GetAverageRatiosFor<AustralianEquity>().PriceEarningRatio,
                    avMarketCap = assets.GetAverageRatiosFor<AustralianEquity>().Capitalisation.ToString()
                });

                model.data.Add(new PortfolioStasticsItem
                {
                    assetClass = "International Equity",
                    costInvestment = ieAssets.Sum(a => a.GetCost().Total),
                    marketValue = ieAssets.Sum(a => a.GetTotalMarketValue()),
                    suitability = ieAssets.GetAssetWeightings().Sum(w => w.Percentage * ((InternationalEquity)w.Weightable).GetRating().TotalScore),
                    oneYearReturn = ieAssets.GetAverageRatiosFor<InternationalEquity>().OneYearReturn,
                    threeYearReturn = ieAssets.GetAverageRatiosFor<InternationalEquity>().ThreeYearReturn,
                    fiveYearReturn = ieAssets.GetAverageRatiosFor<InternationalEquity>().FiveYearReturn,
                    earningsPerShare = ieAssets.GetAverageRatiosFor<InternationalEquity>().EarningsStability,
                    dividend = ieAssets.GetAverageRatiosFor<InternationalEquity>().DividendYield,
                    beta = ieAssets.GetAverageRatiosFor<InternationalEquity>().Beta.ToString(),
                    returnOnAsset = ieAssets.GetAverageRatiosFor<InternationalEquity>().ReturnOnAsset,
                    returnOnEquity = ieAssets.GetAverageRatiosFor<InternationalEquity>().ReturnOnEquity,
                    priceEarningsRatio = ieAssets.GetAverageRatiosFor<InternationalEquity>().PriceEarningRatio,
                    avMarketCap = ieAssets.GetAverageRatiosFor<InternationalEquity>().Capitalisation.ToString()
                });

                model.data.Add(new PortfolioStasticsItem
                {
                    assetClass = "Managed Investment",
                    costInvestment = miAssets.Sum(a => a.GetCost().Total),
                    marketValue = miAssets.Sum(a => a.GetTotalMarketValue()),
                    suitability = miAssets.GetAssetWeightings().Sum(w => w.Percentage * ((ManagedInvestment)w.Weightable).GetRating().TotalScore),
                    oneYearReturn = miAssets.GetAverageRatiosFor<ManagedInvestment>().OneYearReturn,
                    threeYearReturn = miAssets.GetAverageRatiosFor<ManagedInvestment>().ThreeYearReturn,
                    fiveYearReturn = miAssets.GetAverageRatiosFor<ManagedInvestment>().FiveYearReturn,
                    earningsPerShare = miAssets.GetAverageRatiosFor<ManagedInvestment>().EarningsStability,
                    dividend = miAssets.GetAverageRatiosFor<ManagedInvestment>().DividendYield,
                    beta = miAssets.GetAverageRatiosFor<ManagedInvestment>().Beta.ToString(),
                    returnOnAsset = miAssets.GetAverageRatiosFor<ManagedInvestment>().ReturnOnAsset,
                    returnOnEquity = miAssets.GetAverageRatiosFor<ManagedInvestment>().ReturnOnEquity,
                    priceEarningsRatio = miAssets.GetAverageRatiosFor<ManagedInvestment>().PriceEarningRatio,
                    avMarketCap = miAssets.GetAverageRatiosFor<ManagedInvestment>().Capitalisation.ToString()
                });


                foreach (var item in model.data)
                {
                    item.pl = item.marketValue - item.costInvestment;
                    item.plp = item.costInvestment == 0 ? 0 : (item.marketValue - item.costInvestment) / item.costInvestment;
                }
                return model;
            }
        }


        [HttpGet, Route("api/Adviser/PortfolioOverview/PortfolioRating")]
        public PortfolioRatingModel GetPortfolioRating_Adviser(string clientGroupId = null)
        {
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

                double assetsSuitability = 0;
                double percentage = 0;

                foreach (var account in groupAccounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<Equity>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((Equity)w.Weightable).GetRating().TotalScore);

                    //var weigthings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();

                    //% of not suitable assets
                    percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                        / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());
                }

                foreach (var account in clientAccounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<Equity>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((Equity)w.Weightable).GetRating().TotalScore);

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
                //return repo.AustralianEquity_GetPortfolioRating_Adviser(User.Identity.GetUserId());
            }
            else
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);

                double assetsSuitability = 0;
                double percentage = 0;
                foreach (var account in accounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<Equity>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((Equity)w.Weightable).GetRating().TotalScore);

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
                //return repo.AustralianEquity_GetPortfolioRating_Client(clientUserId);
            }
        }
        [HttpGet, Route("api/Client/PortfolioOverview/PortfolioRating")]
        public PortfolioRatingModel GetPortfolioRating_Client()
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
                    var equityWithResearchValues = account.GetAssetsSync().OfType<Equity>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((Equity)w.Weightable).GetRating().TotalScore);

                    //var weigthings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();

                    //% of not suitable assets
                    percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                        / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());
                }

                foreach (var account in clientAccounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<Equity>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((Equity)w.Weightable).GetRating().TotalScore);

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
                    var equityWithResearchValues = account.GetAssetsSync().OfType<Equity>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((Equity)w.Weightable).GetRating().TotalScore);

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
        [HttpGet, Route("api/PortfolioOverview/RecentStock")]
        public IEnumerable<StockDataModel> GetRecentStock()
        {
            return repo.Overview_GetRecentStockData();
        }
        [HttpGet, Route("api/Adviser/PortfolioOverview/InvestmentPortfolio")]
        public InvestmentPortfolioModel GetInvestmentPortfolio_Adviser(string clientGroupId = null)
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

                model.total = model.data.Sum(d => d.amount);

                return model;
            }
            else
            {

                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);

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

                model.total = model.data.Sum(d => d.amount);

                return model;
            }

        }
        [HttpGet, Route("api/Client/PortfolioOverview/InvestmentPortfolio")]
        public InvestmentPortfolioModel GetInvestmentPortfolio_Client()
        {
            
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

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
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

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
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

                var allAectors = edisRepo.GetAllSectorsSync();
                List<SectorItem> sectorItems = new List<SectorItem>();

                foreach (var sector in allAectors)
                {
                    sectorItems.Add(new SectorItem
                    {
                        sector = sector
                    });
                }

                double totalValue = 0;


                foreach (var account in groupAccounts)
                {
                    var assets = account.GetAssetsSync();
                    var dictionary = AssetsExtensions.GetAssetSectorialDiversificationSync<AustralianEquity>(assets, edisRepo);

                    foreach (var item in dictionary)
                    {
                        totalValue += item.Value;
                        sectorItems.FirstOrDefault(s => s.sector == item.Key).value += item.Value;
                    }
                }
                foreach (var account in clientAccounts)
                {
                    var assets = account.GetAssetsSync();
                    var dictionary = AssetsExtensions.GetAssetSectorialDiversificationSync<AustralianEquity>(assets, edisRepo);

                    foreach (var item in dictionary)
                    {
                        totalValue += item.Value;
                        sectorItems.FirstOrDefault(s => s.sector == item.Key).value += item.Value;
                    }
                }

                foreach (var sectorItem in sectorItems) {
                    sectorItem.percentage = sectorItem.value / totalValue;
                }

                SectorialPortfolioModel model = new SectorialPortfolioModel{
                    data = sectorItems,
                    total = totalValue,
                    percentage = 1
                };


                return model;
                //return repo.Overview_GetSectorialSummary_Adviser(User.Identity.GetUserId());
            }
            else
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);

                var allAectors = edisRepo.GetAllSectorsSync();
                List<SectorItem> sectorItems = new List<SectorItem>();

                foreach (var sector in allAectors) {
                    sectorItems.Add(new SectorItem { 
                        sector = sector
                    });
                }

                double totalValue = 0;
                foreach (var account in accounts) {
                    var assets = account.GetAssetsSync();
                    var dictionary = AssetsExtensions.GetAssetSectorialDiversificationSync<AustralianEquity>(assets, edisRepo);

                    foreach (var item in dictionary) {
                        totalValue += item.Value;
                        sectorItems.FirstOrDefault(s => s.sector == item.Key).value += item.Value;
                    }
                }

                foreach (var sectorItem in sectorItems) {
                    sectorItem.percentage = sectorItem.value / totalValue;
                }

                SectorialPortfolioModel model = new SectorialPortfolioModel {
                    data = sectorItems,
                    total = totalValue,
                    percentage = 1
                };
                

                return model;
                //return repo.Overview_GetSectorialSummary_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/PortfolioOverview/SectorialExposure")]
        public SectorialPortfolioModel GetSectorialExposureSummary_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);


                var allAectors = edisRepo.GetAllSectorsSync();
                List<SectorItem> sectorItems = new List<SectorItem>();

                foreach (var sector in allAectors)
                {
                    sectorItems.Add(new SectorItem
                    {
                        sector = sector
                    });
                }

                double totalValue = 0;


                foreach (var account in groupAccounts)
                {
                    var assets = account.GetAssetsSync();
                    var dictionary = AssetsExtensions.GetAssetSectorialDiversificationSync<AustralianEquity>(assets, edisRepo);

                    foreach (var item in dictionary)
                    {
                        totalValue += item.Value;
                        sectorItems.FirstOrDefault(s => s.sector == item.Key).value += item.Value;
                    }
                }
                foreach (var account in clientAccounts)
                {
                    var assets = account.GetAssetsSync();
                    var dictionary = AssetsExtensions.GetAssetSectorialDiversificationSync<AustralianEquity>(assets, edisRepo);

                    foreach (var item in dictionary)
                    {
                        totalValue += item.Value;
                        sectorItems.FirstOrDefault(s => s.sector == item.Key).value += item.Value;
                    }
                }

                foreach (var sectorItem in sectorItems)
                {
                    sectorItem.percentage = sectorItem.value / totalValue;
                }

                SectorialPortfolioModel model = new SectorialPortfolioModel
                {
                    data = sectorItems,
                    total = totalValue,
                    percentage = 1
                };


                return model;
            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                var allAectors = edisRepo.GetAllSectorsSync();
                List<SectorItem> sectorItems = new List<SectorItem>();

                foreach (var sector in allAectors)
                {
                    sectorItems.Add(new SectorItem
                    {
                        sector = sector
                    });
                }

                double totalValue = 0;
                foreach (var account in accounts)
                {
                    var assets = account.GetAssetsSync();
                    var dictionary = AssetsExtensions.GetAssetSectorialDiversificationSync<AustralianEquity>(assets, edisRepo);

                    foreach (var item in dictionary)
                    {
                        totalValue += item.Value;
                        sectorItems.FirstOrDefault(s => s.sector == item.Key).value += item.Value;
                    }
                }

                foreach (var sectorItem in sectorItems)
                {
                    sectorItem.percentage = sectorItem.value / totalValue;
                }

                SectorialPortfolioModel model = new SectorialPortfolioModel
                {
                    data = sectorItems,
                    total = totalValue,
                    percentage = 1
                };


                return model;
            }
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
    }
}
