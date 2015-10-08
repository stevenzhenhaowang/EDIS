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
using Shared;
using Domain.Portfolio.Services;
using Domain.Portfolio.Values.Cashflow;


namespace EDISAngular.APIControllers
{
    public class PortfolioInsuranceController : ApiController
    {
        private PortfolioRepository repo = new PortfolioRepository();
        private EdisRepository edisRepo = new EdisRepository();

        [HttpGet, Route("api/Adviser/InsurancePortfolio/CashflowDetail")]
        public CashflowBriefModel GetCashflow_Adviser(string clientGroupId = null)
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

                    List<Cashflow> cashFlows = account.GetLiabilitiesSync().OfType<Insurance>().Cast<LiabilityBase>().ToList().GetMonthlyCashflows();

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
                    List<Cashflow> cashFlows = account.GetLiabilitiesSync().OfType<Insurance>().Cast<LiabilityBase>().ToList().GetMonthlyCashflows();

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

                    List<Cashflow> cashFlows = account.GetLiabilitiesSync().OfType<Insurance>().Cast<LiabilityBase>().ToList().GetMonthlyCashflows();

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
        [HttpGet, Route("api/Client/InsurancePortfolio/CashflowDetail")]
        public CashflowBriefModel GetCashflow_Client()
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

                    List<Cashflow> cashFlows = account.GetLiabilitiesSync().OfType<Insurance>().Cast<LiabilityBase>().ToList().GetMonthlyCashflows();

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
                    List<Cashflow> cashFlows = account.GetLiabilitiesSync().OfType<Insurance>().Cast<LiabilityBase>().ToList().GetMonthlyCashflows();

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

                    List<Cashflow> cashFlows = account.GetLiabilitiesSync().OfType<Insurance>().Cast<LiabilityBase>().ToList().GetMonthlyCashflows();

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

        [HttpGet, Route("api/Adviser/InsurancePortfolio/Statistics")]
        public InsuranceStatisticsModel GetStatistics_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.Insurance_GetStatistics_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.Insurance_GetStatistics_Client(clientUserId);
            }
        }
        [HttpGet, Route("api/Client/InsurancePortfolio/Statistics")]
        public InsuranceStatisticsModel GetStatistics_Client()
        {
            return repo.Insurance_GetStatistics_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/InsurancePortfolio/InsuranceList")]
        public List<InsuranceListItemModel> GetInsuranceList_Adviser(string clientGroupId = null)
        {

            if (string.IsNullOrEmpty(clientGroupId))
            {

                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);



                List<InsuranceListItemDetailModel> assetInsurance = new List<InsuranceListItemDetailModel>();
                List<InsuranceListItemDetailModel> persoanlInsurance = new List<InsuranceListItemDetailModel>();
                List<InsuranceListItemDetailModel> liabilityInsurance = new List<InsuranceListItemDetailModel>();
                List<InsuranceListItemDetailModel> miscellaneousInsurance = new List<InsuranceListItemDetailModel>();

                List<InsuranceListItemModel> model = new List<InsuranceListItemModel>();

                model.Add(new InsuranceListItemModel
                {
                    insuranceMetaType = InsuranceType.AssetInsurance.ToString(),
                    data = assetInsurance,
                });
                model.Add(new InsuranceListItemModel
                {
                    insuranceMetaType = InsuranceType.LiabilityInsurance.ToString(),
                    data = liabilityInsurance,
                });
                model.Add(new InsuranceListItemModel
                {
                    insuranceMetaType = InsuranceType.MiscellaneousInsurance.ToString(),
                    data = miscellaneousInsurance,
                });
                model.Add(new InsuranceListItemModel
                {
                    insuranceMetaType = InsuranceType.PersoanlInsurance.ToString(),
                    data = persoanlInsurance,
                });

                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in groupAccounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                foreach (var account in clientAccounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                var insurancesGroups = liabilities.OfType<Insurance>().GroupBy(i => i.InsuranceType);
                foreach (var insuranceMetaGroup in insurancesGroups)
                {

                    var insurance = insuranceMetaGroup.FirstOrDefault();

                    switch (insuranceMetaGroup.Key)
                    {
                        case InsuranceType.None:
                            model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.None.ToString()).data.Add(SetModelDetailsFromInsurance(new InsuranceListItemDetailModel(), insurance));
                            break;
                        case InsuranceType.AssetInsurance:
                            model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.AssetInsurance.ToString()).data.Add(SetModelDetailsFromInsurance(new InsuranceListItemDetailModel(), insurance));
                            break;
                        case InsuranceType.MiscellaneousInsurance:
                            model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.MiscellaneousInsurance.ToString()).data.Add(SetModelDetailsFromInsurance(new InsuranceListItemDetailModel(), insurance));
                            break;
                        case InsuranceType.PersoanlInsurance:
                            model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.PersoanlInsurance.ToString()).data.Add(SetModelDetailsFromInsurance(new InsuranceListItemDetailModel(), insurance));
                            break;
                    }

                    //switch (insuranceMetaGroup.Key)
                    //{
                    //    case InsuranceType.None:
                    //        SetModelDetailsFromInsurance(model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.None.ToString()).data.SingleOrDefault(d => d.typeOfPolicy == PolicyType.None.ToString()), insurance);
                    //        break;
                    //    case InsuranceType.AssetInsurance:
                    //        switch (insurance.PolicyType)
                    //        {
                    //            case PolicyType.Boat:
                    //                SetModelDetailsFromInsurance(model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.AssetInsurance.ToString()).data.SingleOrDefault(d => d.typeOfPolicy == PolicyType.Boat.ToString()), insurance);
                    //                break;
                    //            case PolicyType.Building:
                    //                SetModelDetailsFromInsurance(model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.AssetInsurance.ToString()).data.SingleOrDefault(d => d.typeOfPolicy == PolicyType.Building.ToString()), insurance);
                    //                break;
                    //            case PolicyType.Car:
                    //                SetModelDetailsFromInsurance(model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.AssetInsurance.ToString()).data.SingleOrDefault(d => d.typeOfPolicy == PolicyType.Car.ToString()), insurance);
                    //                break;
                    //            case PolicyType.MotorBike:
                    //                SetModelDetailsFromInsurance(model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.AssetInsurance.ToString()).data.SingleOrDefault(d => d.typeOfPolicy == PolicyType.MotorBike.ToString()), insurance);
                    //                break;
                    //            case PolicyType.Content:
                    //                SetModelDetailsFromInsurance(model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.AssetInsurance.ToString()).data.SingleOrDefault(d => d.typeOfPolicy == PolicyType.Content.ToString()), insurance);
                    //                break;
                    //        }
                    //        break;
                    //    case InsuranceType.MiscellaneousInsurance:
                    //        switch (insurance.PolicyType)
                    //        {
                    //            case PolicyType.IncomeProtection:
                    //                SetModelDetailsFromInsurance(model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.MiscellaneousInsurance.ToString()).data.SingleOrDefault(d => d.typeOfPolicy == PolicyType.IncomeProtection.ToString()), insurance);
                    //                break;
                    //            case PolicyType.RentalIncome:
                    //                SetModelDetailsFromInsurance(model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.MiscellaneousInsurance.ToString()).data.SingleOrDefault(d => d.typeOfPolicy == PolicyType.RentalIncome.ToString()), insurance);
                    //                break;
                    //        }
                    //        break;
                    //    case InsuranceType.PersoanlInsurance:
                    //        SetModelDetailsFromInsurance(model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.PersoanlInsurance.ToString()).data.SingleOrDefault(d => d.typeOfPolicy == PolicyType.Accident.ToString()), insurance);
                    //        break;
                    //}
                }

                return model;
                //return repo.Insurance_GetInsuranceList_Adviser(User.Identity.GetUserId());
            }
            else
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);

                List<InsuranceListItemDetailModel> assetInsurance = new List<InsuranceListItemDetailModel>();
                List<InsuranceListItemDetailModel> persoanlInsurance = new List<InsuranceListItemDetailModel>();
                List<InsuranceListItemDetailModel> liabilityInsurance = new List<InsuranceListItemDetailModel>();
                List<InsuranceListItemDetailModel> miscellaneousInsurance = new List<InsuranceListItemDetailModel>();

                List<InsuranceListItemModel> model = new List<InsuranceListItemModel>();

                //model.Add(new InsuranceListItemModel
                //{
                //    insuranceMetaType = InsuranceType.None.ToString(),
                //    data = noneInsurance,
                //});
                model.Add(new InsuranceListItemModel
                {
                    insuranceMetaType = InsuranceType.AssetInsurance.ToString(),
                    data = assetInsurance,
                });
                model.Add(new InsuranceListItemModel
                {
                    insuranceMetaType = InsuranceType.LiabilityInsurance.ToString(),
                    data = liabilityInsurance,
                });
                model.Add(new InsuranceListItemModel
                {
                    insuranceMetaType = InsuranceType.MiscellaneousInsurance.ToString(),
                    data = miscellaneousInsurance,
                });
                model.Add(new InsuranceListItemModel
                {
                    insuranceMetaType = InsuranceType.PersoanlInsurance.ToString(),
                    data = persoanlInsurance,
                });

                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in accounts) {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                var insurancesGroups = liabilities.OfType<Insurance>().GroupBy(i => i.InsuranceType);
                foreach (var insuranceMetaGroup in insurancesGroups) 
                {
                    
                    var insurance = insuranceMetaGroup.FirstOrDefault();

                    switch (insuranceMetaGroup.Key) { 
                        case InsuranceType.None :
                            model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.None.ToString()).data.Add(SetModelDetailsFromInsurance(new InsuranceListItemDetailModel(), insurance));
                            break;
                        case InsuranceType.AssetInsurance :
                            model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.AssetInsurance.ToString()).data.Add(SetModelDetailsFromInsurance(new InsuranceListItemDetailModel(), insurance));
                            break;
                        case InsuranceType.MiscellaneousInsurance:
                            model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.MiscellaneousInsurance.ToString()).data.Add(SetModelDetailsFromInsurance(new InsuranceListItemDetailModel(), insurance));
                            break;
                        case InsuranceType.PersoanlInsurance:
                            model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.PersoanlInsurance.ToString()).data.Add(SetModelDetailsFromInsurance(new InsuranceListItemDetailModel(), insurance));
                            break;
                    }

                }

                return model;
                //return repo.Insurance_GetInsuranceList_Client(clientGroupId);
            }

        }

        public InsuranceListItemDetailModel SetModelDetailsFromInsurance(InsuranceListItemDetailModel detailModel, Insurance insurance)
        {
            detailModel.amountInsured = insurance.AmountInsured;
            detailModel.assetInsured = insurance.AssetInsurerd;
            detailModel.nameOfPolicy = insurance.NameOfPolicy;
            detailModel.policyAddress = insurance.PolicyAddress;
            detailModel.policyNumber = insurance.PolicyNumber;
            detailModel.premium = insurance.Premium;
            detailModel.type = insurance.InsuranceType.ToString();
            detailModel.typeOfPolicy = insurance.PolicyType.ToString();

            return detailModel;
        }

        [HttpGet, Route("api/Client/InsurancePortfolio/InsuranceList")]
        public List<InsuranceListItemModel> GetInsuranceList_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);


                List<InsuranceListItemDetailModel> assetInsurance = new List<InsuranceListItemDetailModel>();
                List<InsuranceListItemDetailModel> persoanlInsurance = new List<InsuranceListItemDetailModel>();
                List<InsuranceListItemDetailModel> liabilityInsurance = new List<InsuranceListItemDetailModel>();
                List<InsuranceListItemDetailModel> miscellaneousInsurance = new List<InsuranceListItemDetailModel>();

                List<InsuranceListItemModel> model = new List<InsuranceListItemModel>();

                model.Add(new InsuranceListItemModel
                {
                    insuranceMetaType = InsuranceType.AssetInsurance.ToString(),
                    data = assetInsurance,
                });
                model.Add(new InsuranceListItemModel
                {
                    insuranceMetaType = InsuranceType.LiabilityInsurance.ToString(),
                    data = liabilityInsurance,
                });
                model.Add(new InsuranceListItemModel
                {
                    insuranceMetaType = InsuranceType.MiscellaneousInsurance.ToString(),
                    data = miscellaneousInsurance,
                });
                model.Add(new InsuranceListItemModel
                {
                    insuranceMetaType = InsuranceType.PersoanlInsurance.ToString(),
                    data = persoanlInsurance,
                });

                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in groupAccounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                foreach (var account in clientAccounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                var insurancesGroups = liabilities.OfType<Insurance>().GroupBy(i => i.InsuranceType);
                foreach (var insuranceMetaGroup in insurancesGroups)
                {

                    var insurance = insuranceMetaGroup.FirstOrDefault();

                    switch (insuranceMetaGroup.Key)
                    {
                        case InsuranceType.None:
                            model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.None.ToString()).data.Add(SetModelDetailsFromInsurance(new InsuranceListItemDetailModel(), insurance));
                            break;
                        case InsuranceType.AssetInsurance:
                            model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.AssetInsurance.ToString()).data.Add(SetModelDetailsFromInsurance(new InsuranceListItemDetailModel(), insurance));
                            break;
                        case InsuranceType.MiscellaneousInsurance:
                            model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.MiscellaneousInsurance.ToString()).data.Add(SetModelDetailsFromInsurance(new InsuranceListItemDetailModel(), insurance));
                            break;
                        case InsuranceType.PersoanlInsurance:
                            model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.PersoanlInsurance.ToString()).data.Add(SetModelDetailsFromInsurance(new InsuranceListItemDetailModel(), insurance));
                            break;
                    }

                }

                return model;
            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                List<InsuranceListItemDetailModel> assetInsurance = new List<InsuranceListItemDetailModel>();
                List<InsuranceListItemDetailModel> persoanlInsurance = new List<InsuranceListItemDetailModel>();
                List<InsuranceListItemDetailModel> liabilityInsurance = new List<InsuranceListItemDetailModel>();
                List<InsuranceListItemDetailModel> miscellaneousInsurance = new List<InsuranceListItemDetailModel>();


                List<InsuranceListItemModel> model = new List<InsuranceListItemModel>();

                model.Add(new InsuranceListItemModel
                {
                    insuranceMetaType = InsuranceType.AssetInsurance.ToString(),
                    data = assetInsurance,
                });
                model.Add(new InsuranceListItemModel
                {
                    insuranceMetaType = InsuranceType.LiabilityInsurance.ToString(),
                    data = liabilityInsurance,
                });
                model.Add(new InsuranceListItemModel
                {
                    insuranceMetaType = InsuranceType.MiscellaneousInsurance.ToString(),
                    data = miscellaneousInsurance,
                });
                model.Add(new InsuranceListItemModel
                {
                    insuranceMetaType = InsuranceType.PersoanlInsurance.ToString(),
                    data = persoanlInsurance,
                });

                List<LiabilityBase> liabilities = new List<LiabilityBase>();
                foreach (var account in accounts)
                {
                    liabilities.AddRange(account.GetLiabilitiesSync());
                }
                var insurancesGroups = liabilities.OfType<Insurance>().GroupBy(i => i.InsuranceType);
                foreach (var insuranceMetaGroup in insurancesGroups)
                {

                    var insurance = insuranceMetaGroup.FirstOrDefault();

                    switch (insuranceMetaGroup.Key)
                    {
                        case InsuranceType.None:
                            model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.None.ToString()).data.Add(SetModelDetailsFromInsurance(new InsuranceListItemDetailModel(), insurance));
                            break;
                        case InsuranceType.AssetInsurance:
                            model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.AssetInsurance.ToString()).data.Add(SetModelDetailsFromInsurance(new InsuranceListItemDetailModel(), insurance));
                            break;
                        case InsuranceType.MiscellaneousInsurance:
                            model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.MiscellaneousInsurance.ToString()).data.Add(SetModelDetailsFromInsurance(new InsuranceListItemDetailModel(), insurance));
                            break;
                        case InsuranceType.PersoanlInsurance:
                            model.SingleOrDefault(i => i.insuranceMetaType == InsuranceType.PersoanlInsurance.ToString()).data.Add(SetModelDetailsFromInsurance(new InsuranceListItemDetailModel(), insurance));
                            break;
                    }

                }

                return model;
            }
        }

        //[HttpGet, Route("api/Adviser/InsurancePortfolio/Conditions")]
        //public List<InsuranceConditionModel> GetConditions_Adviser(string clientUserId = null)
        //{
        //    if (string.IsNullOrEmpty(clientUserId))
        //    {
        //        return repo.Insurance_GetConditions_Adviser(User.Identity.GetUserId());
        //    }
        //    else
        //    {
        //        return repo.Insurance_GetConditions_Client(clientUserId);
        //    }
        //}
        //[HttpGet, Route("api/Client/InsurancePortfolio/Conditions")]
        //public List<InsuranceConditionModel> GetConditions_Client()
        //{
        //    return repo.Insurance_GetConditions_Client(User.Identity.GetUserId());
        //}

    }
}
