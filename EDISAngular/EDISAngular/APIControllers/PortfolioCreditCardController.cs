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


namespace EDISAngular.APIControllers
{
    public class PortfolioCreditCardController : ApiController
    {
        private PortfolioRepository repo = new PortfolioRepository();
        [HttpGet, Route("api/Adviser/CreditCardPortfolio/Diversification")]
        public IEnumerable<DataNameAmountPair> GetDiversification_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.CreditCard_GetDiversification_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.CreditCard_GetDiversification_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/CreditCardPortfolio/Diversification")]
        public IEnumerable<DataNameAmountPair> GetDiversification_Client()
        {
            return repo.CreditCard_GetDiversification_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/CreditCardPortfolio/CardDetails")]
        public IEnumerable<CreditCardDetailsModel> GetCardDetails_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.CreditCard_GetCardDetails_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.CreditCard_GetCardDetails_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/CreditCardPortfolio/CardDetails")]
        public IEnumerable<CreditCardDetailsModel> GetCardDetails_Client()
        {
            return repo.CreditCard_GetCardDetails_Client(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/CreditCardPortfolio/CardStatistics")]
        public IEnumerable<CreditCardStatModel> GetCardStats_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.CreditCard_GetCardStats_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.CreditCard_GetCardStats_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/CreditCardPortfolio/CardStatistics")]
        public IEnumerable<CreditCardStatModel> GetCardStats_Client()
        {
            return repo.CreditCard_GetCardStats_Adviser(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/Adviser/CreditCardPortfolio/QuickStats")]
        public CreditCardQuickStatsModel GetQuickStats_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.CreditCard_GetQuickStats_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.CreditCard_GetQuickStats_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/CreditCardPortfolio/QuickStats")]
        public CreditCardQuickStatsModel GetQuickStats_Client()
        {
            return repo.CreditCard_GetQuickStats_Client(User.Identity.GetUserId());
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               

        [HttpGet, Route("api/Adviser/CreditCardPortfolio/Cashflow")]
        public CashflowBriefModel GetCashflow_Adviser(string clientUserId = null)
        {
            if (string.IsNullOrEmpty(clientUserId))
            {
                return repo.CreditCard_GetCashflowSummary_Adviser(User.Identity.GetUserId());
            }
            else
            {
                return repo.CreditCard_GetCashflowSummary_Client(clientUserId);
            }

        }
        [HttpGet, Route("api/Client/CreditCardPortfolio/Cashflow")]
        public CashflowBriefModel GetCashflow_Client()
        {
            return repo.CreditCard_GetCashflowSummary_Client(User.Identity.GetUserId());
        }
    }
}
