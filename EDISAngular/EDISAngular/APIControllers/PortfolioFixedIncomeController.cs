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
using Domain.Portfolio.AggregateRoots;
using Domain.Portfolio.Services;
using Domain.Portfolio.Values.Cashflow;
using Domain.Portfolio.AggregateRoots.Asset;


namespace EDISAngular.APIControllers
{
    public class PortfolioFixedIncomeController : ApiController
    {
        private PortfolioRepository repo = new PortfolioRepository();
        private EdisRepository edisRepo = new EdisRepository();
        [HttpGet, Route("api/Adviser/FixedIncomePortfolio/General")]
        public SummaryGeneralInfoFCL GetGeneralInfo_Adviser(string clientGroupId = null)
        {
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                double totalCost = 0;
                double totalMarketValue = 0;
                double coupon = 0;
                double faceValue = 0;
                foreach (var account in groupAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList();
                    var fixedIncomes = assets.OfType<FixedIncome>();
                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                        faceValue += asset.LatestPrice;
                    }
                    foreach (var income in fixedIncomes)
                    {
                        coupon += income.CouponRate == null ? 0 : (double)income.CouponRate;
                    }
                }
                foreach (var account in clientAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList();
                    var fixedIncomes = assets.OfType<FixedIncome>();
                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                        faceValue += asset.LatestPrice;
                    }
                    foreach (var income in fixedIncomes)
                    {
                        coupon += income.CouponRate == null ? 0 : (double)income.CouponRate;
                    }
                }
                SummaryGeneralInfoFCL summary = new SummaryGeneralInfoFCL
                {
                    cost = totalCost,
                    marketValue = totalMarketValue,
                    pl = totalMarketValue - totalCost,
                    plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100,
                    aveCoupon = coupon,
                    faceValue = faceValue
                };

                return summary;
            }
            else
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                double totalCost = 0;
                double totalMarketValue = 0;
                double coupon = 0;
                double faceValue = 0;
                foreach (var account in accounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList();
                    var fixedIncomes = assets.OfType<FixedIncome>();
                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                        faceValue += asset.LatestPrice;
                    }
                    foreach (var income in fixedIncomes)
                    {
                        coupon += income.CouponRate == null ? 0 : (double)income.CouponRate;
                    }
                }
                SummaryGeneralInfoFCL summary = new SummaryGeneralInfoFCL
                {
                    cost = totalCost,
                    marketValue = totalMarketValue,
                    pl = totalMarketValue - totalCost,
                    plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100,
                    faceValue = faceValue,
                    aveCoupon = coupon
                };

                return summary;
            }

        }
        [HttpGet, Route("api/Client/FixedIncomePortfolio/General")]
        public SummaryGeneralInfoFCL GetGeneralInfo_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                double totalCost = 0;
                double totalMarketValue = 0;
                double coupon = 0;
                double faceValue = 0;
                foreach (var account in groupAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList();
                    var fixedIncomes = assets.OfType<FixedIncome>();
                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                        faceValue += asset.LatestPrice;
                    }
                    foreach (var income in fixedIncomes)
                    {
                        coupon += income.CouponRate == null ? 0 : (double)income.CouponRate;
                    }
                }
                foreach (var account in clientAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList();
                    var fixedIncomes = assets.OfType<FixedIncome>();
                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                        faceValue += asset.LatestPrice;
                    }
                    foreach (var income in fixedIncomes)
                    {
                        coupon += income.CouponRate == null ? 0 : (double)income.CouponRate;
                    }
                }
                SummaryGeneralInfoFCL summary = new SummaryGeneralInfoFCL
                {
                    cost = totalCost,
                    marketValue = totalMarketValue,
                    pl = totalMarketValue - totalCost,
                    plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100,
                    aveCoupon = coupon,
                    faceValue = faceValue
                };

                return summary;
            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                double totalCost = 0;
                double totalMarketValue = 0;
                double coupon = 0;
                double faceValue = 0;
                foreach (var account in accounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList();
                    var fixedIncomes = assets.OfType<FixedIncome>();
                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                        faceValue += asset.LatestPrice;
                    }
                    foreach (var income in fixedIncomes)
                    {
                        coupon += income.CouponRate == null ? 0 : (double)income.CouponRate;
                    }
                }
                SummaryGeneralInfoFCL summary = new SummaryGeneralInfoFCL
                {
                    cost = totalCost,
                    marketValue = totalMarketValue,
                    pl = totalMarketValue - totalCost,
                    plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100,
                    faceValue = faceValue,
                    aveCoupon = coupon
                };

                return summary;
            }
        }

        [HttpGet, Route("api/Adviser/FixedIncomePortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.FixedIncome_GetPortfolioRating_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.FixedIncome_GetPortfolioRating_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/FixedIncomePortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Client()
        {
            return repo.FixedIncome_GetPortfolioRating_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/FixedIncomePortfolio/Price")]
        public CashPriceChartModel GetPrice_Adviser(string clientGroupId = null)
        {
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);




                return repo.FixedIncome_GetPriceData_Adviser(User.Identity.GetUserId());
            }
            else
            {

                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);

                foreach (var account in accounts)
                {
                    account.GetAssetsSync();
                }



                return repo.FixedIncome_GetPriceData_Client(clientGroupId);
            }

        }
        //[HttpGet, Route("api/Client/FixedIncomePortfolio/Price")]
        //public CashPriceChartModel GetPrice_Client()
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

        [HttpGet, Route("api/Adviser/FixedIncomePortfolio/Cashflow")]
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
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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
        [HttpGet, Route("api/Client/FixedIncomePortfolio/Cashflow")]
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
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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
                    List<Cashflow> cashFlows = account.GetAssetsSync().OfType<FixedIncome>().Cast<AssetBase>().ToList().GetMonthlyCashflows();

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

        [HttpGet, Route("api/Adviser/FixedIncomePortfolio/Profiles")]
        public FixedIncomeProfileModel GetProfiles_Adviser(string clientGroupId = null)
        {
            if (string.IsNullOrEmpty(clientGroupId))
            {

                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                FixedIncomeProfileModel model = new FixedIncomeProfileModel { data = new List<FixedIncomeProfileItem>() };

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in groupAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }
                foreach (var account in clientAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }
                var incomes = assets.OfType<FixedIncome>();
                foreach (var income in incomes)
                {
                    FixedIncomeProfileItem item = new FixedIncomeProfileItem
                    {
                        ticker = income.Ticker,
                        fixedIncomeName = income.FixedIncomeName,
                        faceValue = income.LatestPrice,
                        coupon = income.CouponRate == null ? 0 : (double)income.CouponRate,
                        couponFrequency = income.CouponFrequency.ToString(),
                        issuer = income.Issuer,
                        costValue = income.GetCost().AssetCost,
                        totalCostValue = income.GetCost().Total,
                        marketValue = income.GetTotalMarketValue(),
                        priority = (int)income.BoundDetails.Priority,
                        redemptionFeatures = income.BoundDetails.RedemptionFeatures.ToString(),
                    };
                    model.data.Add(item);
                }

                return model;


                //return repo.FixedIncome_GetProfiles_Adviser(User.Identity.GetUserId());
            }
            else
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                FixedIncomeProfileModel model = new FixedIncomeProfileModel { data = new List<FixedIncomeProfileItem>() };

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in accounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }
                var incomes = assets.OfType<FixedIncome>();
                foreach (var income in incomes)
                {
                    FixedIncomeProfileItem item = new FixedIncomeProfileItem
                    {
                        ticker = income.Ticker,
                        fixedIncomeName = income.FixedIncomeName,
                        faceValue = income.LatestPrice,
                        coupon = income.CouponRate == null ? 0 : (double)income.CouponRate,
                        couponFrequency = income.CouponFrequency.ToString(),
                        issuer = income.Issuer,
                        costValue = income.GetCost().AssetCost,
                        totalCostValue = income.GetCost().Total,
                        marketValue = income.GetTotalMarketValue(),
                        priority = (int)income.BoundDetails.Priority,
                        redemptionFeatures = income.BoundDetails.RedemptionFeatures.ToString(),
                    };
                    model.data.Add(item);
                }

                return model;

                //return repo.FixedIncome_GetProfiles_Client(clientGroupId);
            }

        }
        [HttpGet, Route("api/Client/FixedIncomePortfolio/Profiles")]
        public FixedIncomeProfileModel GetProfiles_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                FixedIncomeProfileModel model = new FixedIncomeProfileModel { data = new List<FixedIncomeProfileItem>() };

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in groupAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }
                foreach (var account in clientAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }
                var incomes = assets.OfType<FixedIncome>();
                foreach (var income in incomes)
                {
                    FixedIncomeProfileItem item = new FixedIncomeProfileItem
                    {
                        ticker = income.Ticker,
                        fixedIncomeName = income.FixedIncomeName,
                        faceValue = income.LatestPrice,
                        coupon = income.CouponRate == null ? 0 : (double)income.CouponRate,
                        couponFrequency = income.CouponFrequency.ToString(),
                        issuer = income.Issuer,
                        costValue = income.GetCost().AssetCost,
                        totalCostValue = income.GetCost().Total,
                        marketValue = income.GetTotalMarketValue(),
                        priority = (int)income.BoundDetails.Priority,
                        redemptionFeatures = income.BoundDetails.RedemptionFeatures.ToString(),
                    };
                    model.data.Add(item);
                }

                return model;

            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                FixedIncomeProfileModel model = new FixedIncomeProfileModel { data = new List<FixedIncomeProfileItem>() };

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in accounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }
                var incomes = assets.OfType<FixedIncome>();
                foreach (var income in incomes)
                {
                    FixedIncomeProfileItem item = new FixedIncomeProfileItem
                    {
                        ticker = income.Ticker,
                        fixedIncomeName = income.FixedIncomeName,
                        faceValue = income.LatestPrice,
                        coupon = income.CouponRate == null ? 0 : (double)income.CouponRate,
                        couponFrequency = income.CouponFrequency.ToString(),
                        issuer = income.Issuer,
                        costValue = income.GetCost().AssetCost,
                        totalCostValue = income.GetCost().Total,
                        marketValue = income.GetTotalMarketValue(),
                        priority = (int)income.BoundDetails.Priority,
                        redemptionFeatures = income.BoundDetails.RedemptionFeatures.ToString(),
                    };
                    model.data.Add(item);
                }

                return model;

            }
        }

        [HttpGet, Route("api/Adviser/FixedIncomePortfolio/Stats")]
        public IEnumerable<IncomeStatisticsModel> GetStats_Adviser(string clientGroupId = null)
        {
            if (string.IsNullOrEmpty(clientGroupId))
            {

                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);



                List<IncomeStatisticsModel> incomeModelList = new List<IncomeStatisticsModel>();

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in groupAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }
                foreach (var account in clientAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }

                var fixedIncomesGroup = assets.OfType<FixedIncome>().GroupBy(i => i.BondType);

                foreach (var incomeGroup in fixedIncomesGroup)
                {
                    var income = incomeGroup.FirstOrDefault();
                    IncomeStatisticsModel model = new IncomeStatisticsModel
                    {
                        value = incomeGroup.Sum(i => i.GetTotalMarketValue()),
                        type = income.BondType,
                    };
                    incomeModelList.Add(model);
                }

                foreach (var model in incomeModelList)
                {
                    model.percentage = incomeModelList.Sum(i => i.value) == 0 ? 0 : model.value / incomeModelList.Sum(i => i.value) * 100;
                }

                return incomeModelList;


                //return repo.FixedIncome_GetStats_Adviser(User.Identity.GetUserId());
            }
            else
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);

                List<IncomeStatisticsModel> incomeModelList = new List<IncomeStatisticsModel>();

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in accounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }

                var fixedIncomesGroup = assets.OfType<FixedIncome>().GroupBy(i => i.BondType);

                foreach (var incomeGroup in fixedIncomesGroup)
                {
                    var income = incomeGroup.FirstOrDefault();
                    IncomeStatisticsModel model = new IncomeStatisticsModel
                    {
                        value = incomeGroup.Sum(i => i.GetTotalMarketValue()),
                        type = income.BondType,
                    };
                    incomeModelList.Add(model);
                }

                foreach (var model in incomeModelList)
                {
                    model.percentage = incomeModelList.Sum(i => i.value) == 0 ? 0 : model.value / incomeModelList.Sum(i => i.value) * 100;
                }

                return incomeModelList;
                //return repo.FixedIncome_GetStats_Client(clientGroupId);
            }

        }
        [HttpGet, Route("api/Client/FixedIncomePortfolio/Stats")]
        public IEnumerable<IncomeStatisticsModel> GetStats_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                List<IncomeStatisticsModel> incomeModelList = new List<IncomeStatisticsModel>();

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in groupAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }
                foreach (var account in clientAccounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }

                var fixedIncomesGroup = assets.OfType<FixedIncome>().GroupBy(i => i.BondType);

                foreach (var incomeGroup in fixedIncomesGroup)
                {
                    var income = incomeGroup.FirstOrDefault();
                    IncomeStatisticsModel model = new IncomeStatisticsModel
                    {
                        value = incomeGroup.Sum(i => i.GetTotalMarketValue()),
                        type = income.BondType,
                    };
                    incomeModelList.Add(model);
                }

                foreach (var model in incomeModelList)
                {
                    model.percentage = incomeModelList.Sum(i => i.value) == 0 ? 0 : model.value / incomeModelList.Sum(i => i.value) * 100;
                }

                return incomeModelList;


            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                List<IncomeStatisticsModel> incomeModelList = new List<IncomeStatisticsModel>();

                List<AssetBase> assets = new List<AssetBase>();
                foreach (var account in accounts)
                {
                    assets.AddRange(account.GetAssetsSync());
                }

                var fixedIncomesGroup = assets.OfType<FixedIncome>().GroupBy(i => i.BondType);

                foreach (var incomeGroup in fixedIncomesGroup)
                {
                    var income = incomeGroup.FirstOrDefault();
                    IncomeStatisticsModel model = new IncomeStatisticsModel
                    {
                        value = incomeGroup.Sum(i => i.GetTotalMarketValue()),
                        type = income.BondType,
                    };
                    incomeModelList.Add(model);
                }

                foreach (var model in incomeModelList)
                {
                    model.percentage = incomeModelList.Sum(i => i.value) == 0 ? 0 : model.value / incomeModelList.Sum(i => i.value) * 100;
                }

                return incomeModelList;
            }
        }

        [HttpGet, Route("api/Adviser/FixedIncomePortfolio/Diversifications")]
        public IncomeDiversificationModel GetDiversification_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.FixedIncome_GetDiversification_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.FixedIncome_GetDiversification_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/FixedIncomePortfolio/Diversifications")]
        public IncomeDiversificationModel GetDiversification_Client()
        {
            return repo.FixedIncome_GetDiversification_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/FixedIncomePortfolio/CashflowDetailed")]
        public FixedIncomeCashflowDetailedModel GetDetailedCashflow_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.FixedIncome_GetCashflowDetails_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.FixedIncome_GetCashflowDetails_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/FixedIncomePortfolio/CashflowDetailed")]
        public FixedIncomeCashflowDetailedModel GetDetailedCashflow_Client()
        {
            return repo.FixedIncome_GetCashflowDetails_Client(User.Identity.GetUserId());
        }


    }
}
