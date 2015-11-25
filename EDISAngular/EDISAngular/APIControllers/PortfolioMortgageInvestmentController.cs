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
            return GenerateGeneralInfo(getMortgageLiabilitiesForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/MortgageInvestmentPortfolio/General")]
        public MortgageInvestmentGeneralInfo GetGeneralInfo_Client()
        {
            return GenerateGeneralInfo(getMortgageLiabilitiesForClient());
        }

        [HttpGet, Route("api/Adviser/MortgageInvestmentPortfolio/Cashflow")]
        public CashflowBriefModel GetCashflowSummary_Adviser(string clientGroupId = null)
        {
            return GenerateCashflowSummaryModel(getMortgageLiabilitiesForAdviser(clientGroupId).GetMonthlyCashflows());
        }
        [HttpGet, Route("api/Client/MortgageInvestmentPortfolio/Cashflow")]
        public CashflowBriefModel GetCashflowSummary_Client()
        {
            return GenerateCashflowSummaryModel(getMortgageLiabilitiesForClient().GetMonthlyCashflows());
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
            return GenerateStatsModel(getMortgageLiabilitiesForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/MortgageInvestmentPortfolio/Stats")]
        public MortgageInvestmentStatModel GetStats_Client()
        {
            return GenerateStatsModel(getMortgageLiabilitiesForClient());
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
            return GenerateProfiles(getMortgageLiabilitiesForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/MortgageInvestmentPortfolio/Profiles")]
        public MortgageInvestmentProfileModel GetProfiles_Client()
        {
            return GenerateProfiles(getMortgageLiabilitiesForClient());
        }

        public List<LiabilityBase> getMortgageLiabilitiesForAdviser(string clientGroupId) {
            List<LiabilityBase> liabilities = new List<LiabilityBase>();

            if (string.IsNullOrEmpty(clientGroupId)) {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                groupAccounts.ForEach(a => liabilities.AddRange(a.GetLiabilitiesSync().OfType<MortgageAndHomeLiability>().Cast<LiabilityBase>().ToList()));
                clientAccounts.ForEach(a => liabilities.AddRange(a.GetLiabilitiesSync().OfType<MortgageAndHomeLiability>().Cast<LiabilityBase>().ToList()));
            } else {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = clientGroup.GetAccountsSync(DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

                accounts.ForEach(a => liabilities.AddRange(a.GetLiabilitiesSync().OfType<MortgageAndHomeLiability>().Cast<LiabilityBase>().ToList()));
                clientAccounts.ForEach(a => liabilities.AddRange(a.GetLiabilitiesSync().OfType<MortgageAndHomeLiability>().Cast<LiabilityBase>().ToList()));
            }
            return liabilities;
        }

        public List<LiabilityBase> getMortgageLiabilitiesForClient() {
            List<LiabilityBase> liabilities = new List<LiabilityBase>();

            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id) {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

                groupAccounts.ForEach(a => liabilities.AddRange(a.GetLiabilitiesSync().OfType<MortgageAndHomeLiability>().Cast<LiabilityBase>().ToList()));
                clientAccounts.ForEach(a => liabilities.AddRange(a.GetLiabilitiesSync().OfType<MortgageAndHomeLiability>().Cast<LiabilityBase>().ToList()));
            } else {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                accounts.ForEach(a => liabilities.AddRange(a.GetLiabilitiesSync().OfType<MortgageAndHomeLiability>().Cast<LiabilityBase>().ToList()));
            }
            return liabilities;
        }

        public MortgageInvestmentGeneralInfo GenerateGeneralInfo(List<LiabilityBase> liabilities) {
            double marketValue = 0;
            double outstandingLoans = 0;
            double propertyGearingRatio = 0;
            double monthlyRepayment = 0;

            foreach (var mah in liabilities.OfType<MortgageAndHomeLiability>()) {
                mah.GetActivitiesSync().OfType<FinancialActivity>().ToList().ForEach(a => monthlyRepayment += a.Expenses.Sum(e => e.Amount));
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

        public CashflowBriefModel GenerateCashflowSummaryModel(List<Cashflow> cashFlows) {

            List<CashFlowBriefItem> items = new List<CashFlowBriefItem>();

            foreach (Months month in Enum.GetValues(typeof(Months))) {
                string currentMonth = edisRepo.GetEnumDescription(month);

                var currentFlows = cashFlows.Where(c => c.Month == currentMonth);

                CashFlowBriefItem newItem = new CashFlowBriefItem {
                    month = currentMonth,
                    expense = currentFlows.Sum(c => c.Expenses),
                    income = currentFlows.Sum(c => c.Income)
                };

                items.Add(newItem);
            }

            CashflowBriefModel model = new CashflowBriefModel {
                totalExpense = items.Sum(i => i.expense),
                totalIncome = items.Sum(i => i.income),
                data = items
            };
            return model;
        }

        public MortgageInvestmentStatModel GenerateStatsModel(List<LiabilityBase> liabilities) {
            MortgageInvestmentStatModel model = new MortgageInvestmentStatModel {
                Combination = 0,
                Fixed = 0,
                NotSpecified = 0,
                Variable = 0
            };

            var mortgageGroups = liabilities.OfType<MortgageAndHomeLiability>().GroupBy(l => l.TypeOfMortgageRates);
            foreach (var mortgageGroup in mortgageGroups) {
                var mortgage = mortgageGroup.FirstOrDefault();
                switch (mortgageGroup.Key) {
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

        public MortgageInvestmentProfileModel GenerateProfiles(List<LiabilityBase> liabilities) {
            MortgageInvestmentProfileModel model = new MortgageInvestmentProfileModel { data = new List<MortgageInvestmentProfileItem>() };
            var mortgageAndHomes = liabilities.OfType<MortgageAndHomeLiability>();
            foreach (var mortgageAndHome in mortgageAndHomes) {
                

                double monthlyRepayment = 0;
                foreach (var activity in mortgageAndHome.GetActivitiesSync().OfType<FinancialActivity>()) {
                    monthlyRepayment += activity.Expenses.Sum(e => e.Amount);
                }
                model.data.Add(new MortgageInvestmentProfileItem {
                    propertyName = ((PropertyType)Int32.Parse(mortgageAndHome.Property.PropertyType)).ToString(),
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
                    currentFinancialYearInterest = mortgageAndHome.CurrentFiancialYearInterest,
                    interestRates = mortgageAndHome.CurrentFiancialYearInterest,
                    startDate = mortgageAndHome.GrantedOn,
                    NumberOfYearsToExpiry = (mortgageAndHome.ExpiryDate - mortgageAndHome.GrantedOn).TotalDays / 365,
                    numberOfYearsToDate = (DateTime.Now - mortgageAndHome.GrantedOn).TotalDays / 365,
                    suitability = mortgageAndHome.Property.GetRating().TotalScore
                });
            }
            return model;
        }
    }
}
