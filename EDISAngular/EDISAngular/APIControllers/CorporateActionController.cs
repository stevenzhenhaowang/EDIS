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
using SqlRepository;



namespace EDISAngular.APIControllers
{
    public class CorporateActionController : ApiController
    {
        private EdisRepository repo = new EdisRepository();
        private CommonReferenceDataRepository comRepo = new CommonReferenceDataRepository();
        [HttpGet, Route("api/Adviser/CorporateAction/IPO")]
        public List<IPOActionData> GetAllIpoActionsForAdviser()
        {
            Console.WriteLine("ipo action check");
            return new List<IPOActionData>
            {
                new IPOActionData {actionId = "1", nameOfRaising = "name of raising", IPOCode= "ipocode 1", listed = true,
                exchange= "exchage", raisingOpened = DateTime.Now, raisingClosed = DateTime.Now, raisingTradingDate = DateTime.Now,
                dispatchDocDate = DateTime.Now, issuedPrice = 100.00, minimumAmount = 200.00, dividendPerShare = 300.00,
                    marketCapitalisation = 150.00, raisingAmount = 200.00, numberOfSharesOnIssue = 100, numberOfSharesRaising = 150,
                allocationFinalised= true, dividendYield = 200.00, participants = new List<IPOActionParticipant> {
                       new IPOActionParticipant { edisAccountNumber = "10000", brokerAccountNumber=  "broker account number",
                       brokerHinSrn = "hinsrn", investedAmount = 1999, name = "eric", numberOfUnits = 100, tickerNumber = "200",
                           type ="type", unitPrice = 188 }

                } },

            };
        }
        [HttpGet,Route("api/Adviser/CorporateAction/Other")]
        public List<OtherCorporateActionData> GetAllOtherCorporateActionsForAdviser()
        {
            Console.WriteLine("get all other coperate action check");
            return new List<OtherCorporateActionData> {
                new OtherCorporateActionData { actionId = "123", corporateActionName = "other corperate action",
                    corporateActionCode = "action code", purposeForCorporateAction = "no purpose",
                    underlyingCompany = new OtherCorporateActionCompany { name = "other company", companyTicker =" company ticker",
                    }, corporateActionClosingDate = DateTime.Now, corporateActionStartDate = DateTime.Now,
                    recordDateEntitlement = DateTime.Now, dispatchOfHolding = "holding", deferredSettlementTradingDate = DateTime.Now,
                    normalTradingDate = DateTime.Now,exEntitlement = DateTime.Now, participants = new List<OtherActionParticipant> {
                        new OtherActionParticipant { name = "stan", brokerAccountNumber= "12312312", brokerHinSrn ="hin srn",
                            edisAccountNumber = "1988", type = "typer"}
                    }
                },                        
            };
        }

        [HttpGet, Route("api/Adviser/CorprateAction/ReturnOfCapital")]
        public List<ReturnOfCapitalData> GetAllReturnOfCapitalActionForAdviser() {
            Console.WriteLine("get all return of capital action check");
            return new List<ReturnOfCapitalData> {
                new ReturnOfCapitalData {
                    actionId = "re1234", actionName = "whateverName", equityId = "equityID", returnDate = DateTime.Now, returnAmount = "888",
                    shareAmount= "whatever", participants = new List<returnOfCapitalParticipant> { new returnOfCapitalParticipant { edisAccountNumber = "cartman"} }
                }
            };
        }
        [HttpGet, Route("api/Adviser/CorporateAction/Reinvestment")]
        public List<ReinvestmentData> GetAllReinvestmentsActionForAdviser() {
            Console.WriteLine("get all reinvestments action check");
            return new List<ReinvestmentData>{
                new ReinvestmentData {
                    actionName = "reinvestment",equtiyId = "equity", reinvestmentDate = DateTime.Now,
                    reinvestmentShareAmount = "reinvestmentAmount", participants= new List<ReinvestmentParticipant> {
                        new ReinvestmentParticipant { edisAccountNumber = "kyle"}
                    }
                }

            };
        }


        [HttpGet, Route("api/Adviser/CorporateAction/StockSplit")]
        public List<StockSplitData> GetAllStockSplitActionDataForAdvise() {
            return new List<StockSplitData> {
                new StockSplitData { actionId = "stockSplitID", actionCode = "splitcode" , splitDate = DateTime.Now,
               stockSplitShares = "how many?", participants = new List<StockSplitParticipant> {
                   new StockSplitParticipant { edisAccountNumber = "edis"}
               } }
            };

        }

        [HttpGet, Route("api/Adviser/CorporateAction/BonusIssues")]
        public List<BonusIssueData> GetAllBonusIssuesActionDataForAdviser()
        {
            return new List<BonusIssueData> { new BonusIssueData {
                actionId = "bonuseIssue", actionCode = "bonusIssueCode", bonusDate = DateTime.Now, bonusIssue = "issues", partcipants
                = new List<BonusParticipant> { new BonusParticipant { edisAccountNumber = "kenny"} }
            } };
        }


       

        [HttpPost, Route("api/Adviser/CorprateAction/IPO")]
        public IHttpActionResult CreateNewIPO(IPOActionData model)
        {
            Console.WriteLine("create new ipo action check");
            if (model != null && ModelState.IsValid)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost, Route("api/Adviser/CorprateAction/Other")]
        public IHttpActionResult CreateNewOther(OtherActionCreationModel model)
        {
            Console.WriteLine("create new other action check");
            if (model != null && ModelState.IsValid)
            {
                return Ok();
            }
            return BadRequest();
        }



        [HttpGet, Route("api/Adviser/CorporateAction/Company")]
        public List<CompanyBriefModel> GetAllCompanies()
        {
            Console.WriteLine("get all companies check");
            return comRepo.GetAllCompanyBriefDetails();
        }
        [HttpGet, Route("api/Adviser/CorprateAction/Ticker")]
        public List<TickerBriefModel> GetAllTickers()
        {
            return comRepo.GetAllTIckers();
        }
        [HttpPost, Route("api/Adviser/CorporateAction/IPO/Allocation")]
        public IHttpActionResult AllocateIPO(IPOActionData model)
        {
            if (model != null && ModelState.IsValid)
            {
                return Ok();
            }
            return BadRequest();
        }


    }
}
