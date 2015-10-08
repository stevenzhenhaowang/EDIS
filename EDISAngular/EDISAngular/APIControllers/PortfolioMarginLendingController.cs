using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EDISAngular.Infrastructure.DatabaseAccess;
using EDISAngular.Models.ServiceModels.CorporateActions;
using EDISAngular.Models.ServiceModels;
using Microsoft.AspNet.Identity;
using EDISAngular.Models.ServiceModels.PortfolioModels;





namespace EDISAngular.APIControllers
{
    public class PortfolioMarginLendingController : ApiController
    {
        private CommonReferenceDataRepository comRepo = new CommonReferenceDataRepository();
        private PortfolioRepository PortRepo = new PortfolioRepository();



        [HttpGet, Route("api/Adviser/MarginLendingPortfolio/RatingInfo")]
        public PortfolioRatingModel GetRatingInfo_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return PortRepo.MarginLending_GetPortfolioRating_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return PortRepo.MarginLending_GetPortfolioRating_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/MarginLendingPortfolio/RatingInfo")]
        public PortfolioRatingModel GetRatingInfo_Client()
        {
            return PortRepo.MarginLending_GetPortfolioRating_Client(User.Identity.GetUserId());
        }




        [HttpGet, Route("api/adviser/MarginLendingPortfolio/CashflowDetails")]
        public object GetCashflow_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return PortRepo.MarginLending_GetCashflowSummary_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return PortRepo.MarginLending_GetCashflowSummary_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/MarginLendingPortfolio/CashflowDetails")]
        public object GetCashflow_Client()
        {
            return PortRepo.MarginLending_GetCashflowSummary_Client(User.Identity.GetUserId());
        }



        [HttpGet, Route("api/Adviser/MarginLendingPortfolio/Stats")]
        public object GetStats_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return PortRepo.MarginLending_GetQuickStats_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return PortRepo.MarginLending_GetQuickStats_Client(clientUserId);
            }
        }
        [HttpGet, Route("api/Client/MarginLendingPortfolio/Stats")]
        public object GetStats_Client()
        {
            return PortRepo.MarginLending_GetQuickStats_Client(User.Identity.GetUserId());
        }




        [HttpGet, Route("api/Adviser/MarginLendingPortfolio/AccountLoanInfo")]
        public object GetAccountLoanInfo(string clientAccountNumber)
        {
            return PortRepo.MarginLending_GetLoanDetailsForClientAccount(clientAccountNumber);
        }



        [HttpGet, Route("api/Adviser/MarginLendingPortfolio/allCompanies")]
        public object GetAllCompanyProfiles_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return PortRepo.MarginLending_GetAllCompanyProfiles_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return PortRepo.MarginLending_GetAllCompanyProfiles_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/MarginLendingPortfolio/allCompanies")]
        public object GetAllCompanyProfiles_Client()
        {
            return PortRepo.MarginLending_GetAllCompanyProfiles_Client(User.Identity.GetUserId());

        }



        [HttpGet, Route("api/Adviser/MarginLendingPortfolio/AccountLoanCompanyDetails")]
        public object GetAccountCompanyDetails(string clientAccountNumber)
        {
            return PortRepo.MarginLending_GetAccountMarginLoanDetails(User.Identity.GetUserId());
        }


        [HttpGet, Route("api/Adviser/MarginLendingPortfolio/SuitabilityDetails")]
        public object GetSuitabilityDetails_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return PortRepo.MarginLending_GetPortfolioSuitability_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return PortRepo.MarginLending_GetPortfolioSuitability_Client(clientUserId);
            }

        }

        [HttpGet, Route("api/Client/MarginLendingPortfolio/SuitabilityDetails")]
        public object GetSuitabilityDetails_Client()
        {
            return PortRepo.MarginLending_GetPortfolioSuitability_Client(User.Identity.GetUserId());
        }




        [HttpGet, Route("api/Adviser/MarginLendingPortfolio/ImpactToCashflow")]
        public object GetImpact_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return PortRepo.MarginLending_GetImpactToCashflow_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return PortRepo.MarginLending_GetImpactToCashflow_Client(clientUserId);
            }

        }

        [HttpGet, Route("api/Client/MarginLendingPortfolio/ImpactToCashflow")]
        public object GetImpact_Client()
        {
            return PortRepo.MarginLending_GetImpactToCashflow_Client(User.Identity.GetUserId());
        }


        [HttpGet, Route("api/Adviser/MarginLendingPortfolio/TypeOfGearing")]
        public object GetGearing_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return PortRepo.MarginLending_GetGearingStrategy_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return PortRepo.MarginLending_GetGearingStrategy_Client(clientUserId);
            }

        }

        [HttpGet, Route("api/Client/MarginLendingPortfolio/TypeOfGearing")]
        public object GetGearing_Client()
        {
            return PortRepo.MarginLending_GetGearingStrategy_Client(User.Identity.GetUserId());
        }


        [HttpGet, Route("api/Adviser/MarginLendingPortfolio/Diversification")]
        public object GetDiversification_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return PortRepo.MarginLending_GetDiversification_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return PortRepo.MarginLending_GetDiversification_Client(clientUserId);
            }

        }

        [HttpGet, Route("api/Client/MarginLendingPortfolio/Diversification")]
        public object GetDiversification()
        {
            return PortRepo.MarginLending_GetDiversification_Client(User.Identity.GetUserId());
        }



    }
}
