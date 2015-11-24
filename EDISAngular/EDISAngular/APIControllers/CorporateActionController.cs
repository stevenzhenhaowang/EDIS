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
using Domain.Portfolio.CorporateActions;
using System.Reflection;
using System.ComponentModel;
using Shared;
using EDISAngular.Models.ViewModels;
using Shared;

namespace EDISAngular.APIControllers
{
    public class CorporateActionController : ApiController
    {
        private EdisRepository repo = new EdisRepository();
        private CommonReferenceDataRepository comRepo = new CommonReferenceDataRepository();
        private string userid;

        public CorporateActionController()
        {
            userid = User.Identity.GetUserId();
        }


        [HttpPost, Route("api/adviser/corporateAction/getAccountByEquity")]
        public List<CorporateActionClientAccountModel> GetAllClientAccountsByEquity(GetAccountByEquityModel model)
        {
           
            var repoRetrieve = repo.GetAllAdviserAccountAccordingToEquity(model.Ticker, userid);
            var result = new List<CorporateActionClientAccountModel>();
            foreach (var reassign in repoRetrieve) {
                var newRecord = new CorporateActionClientAccountModel()
                {
                    edisAccountNumber = reassign.AccountNumber,
                    shareAmount = reassign.ShareAmount,
                    accountName = reassign.AccountName
                };
                result.Add(newRecord);
            }
            return result;
        }



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


        //to do get all return of capital 
        [HttpGet, Route("api/Adviser/CorprateAction/ReturnOfCapital")]
        public List<ReturnOfCapitalData> GetAllReturnOfCapitalActionForAdviser() {
            var result = new List<ReturnOfCapitalData>();
            var repoRetrival = repo.GetReturnOfCapitalHistoryByAdviser(userid);
            foreach (var re in repoRetrival) {
                var oneRecord = new ReturnOfCapitalData() {
                    actionName = re.CorperateActionName,
                    returnDate = re.CorperateActionDate,
                    returnAmount = re.CashAdjustmentAmount,
                    accountNumber = re.AssociatedAccountNumber,
                    ticker = re.Ticker,
                    //?  do need to may be for display purpose its ok  accountName = re.AssociatedAccountNumber
                };
                result.Add(oneRecord);
            }
            return result;
        }
        [HttpGet, Route("api/Adviser/CorporateAction/Reinvestment")]
        public List<ReinvestmentData> GetAllReinvestmentsActionForAdviser() {
            Console.WriteLine("get all reinvestments action check");
            var result = new List<ReinvestmentData>();
            var repoRetrival = repo.GetReinvestmentPlanHistoryByAdviser(userid);
            foreach (var re in repoRetrival)
            {
                var oneRecord = new ReinvestmentData()
                {
                    actionName = re.CorperateActionName,
                    ticker = re.Ticker,
                    reinvestmentShareAmount = re.StockAdjustmentShareAmount,
                    accountNumber = re.AssociatedAccountNumber,
                    reinvestmentDate = re.CorperateActionDate,
                    status = GetEnumDescription(re.Status),

            };
                result.Add(oneRecord);
        }
            return result;
        }


        [HttpGet, Route("api/Adviser/CorporateAction/StockSplit")]
        public List<StockSplitData> GetAllStockSplitActionDataForAdvise() {
            var result = new List<StockSplitData>();
            var repoRetrival = repo.GetStockSplitHistoryByAdviser(userid);
            foreach (var re in repoRetrival) {
                var oneRecord = new StockSplitData
                {
                    actionName = re.CorperateActionName,
                    edisAccountNumber = re.AssociatedAccountNumber,
                    splitDate = re.CorperateActionDate,
                    splitTo = re.StockAdjustmentShareAmount,
                    status = GetEnumDescription(re.Status),
                    ticker = re.Ticker,
            };
                result.Add(oneRecord);
            }
            return result;
        }

        [HttpGet, Route("api/Adviser/CorporateAction/BonusIssues")]
        public List<BonusIssueData> GetAllBonusIssuesActionDataForAdviser()
        {
            var result = new List<BonusIssueData>();
            var repoRetrival = repo.GetBonusIssueHistoryByAdviser(userid);
            foreach (var re in repoRetrival)
            {
                var oneRecord = new BonusIssueData()
                {
                    actionName = re.CorperateActionName,
                    bonusDate = re.CorperateActionDate,
                    bonusIssueShareAmount = re.StockAdjustmentShareAmount,
                    edisAccountNumber = re.AssociatedAccountNumber,
                    ticker = re.Ticker,
                    status = GetEnumDescription(re.Status),
                    //?  do need to may be for display purpose its ok  accountName = re.AssociatedAccountNumber
                };
                result.Add(oneRecord);
            }
            return result;
        }

        [HttpGet, Route("api/Adviser/CorporateAction/BuyBackProgram")]
        public List<BuyBackProgramData> GetAllBuyBackProgramActionDataForAdviser()
        {
            var result = new List<BuyBackProgramData>();
            var repoRetrival = repo.GetBuyBackProgramHistoryByAdviser(userid);
            foreach (var re in repoRetrival)
            {
                var oneRecord = new BuyBackProgramData()
                {
                    actionName = re.CorperateActionName,
                    buyBackDate = re.CorperateActionDate,
                    cashAdjusment = re.CashAdjustmentAmount,
                    shareAmountAdjustment = re.StockAdjustmentShareAmount,
                    ticker = re.Ticker,
                    edisAccountNumber = re.AssociatedAccountNumber,
                    status = GetEnumDescription(re.Status),
                    
                };
                result.Add(oneRecord);
            }
            return result;
        }
        [HttpGet, Route("api/Adviser/CorporateAction/RightsIssues")]
        public List<RightsIssueData> GetAllRightsIssuesActionDataForAdviser()
        {
            var result = new List<RightsIssueData>();
            var repoRetrival = repo.GetRightsIssueHistoryByAdviser(userid);
            foreach (var re in repoRetrival)
            {
                var oneRecord = new RightsIssueData()
                {
                    actionName = re.CorperateActionName,
                    RightsIssueDate = re.CorperateActionDate,
                    cashAdjustment = re.CashAdjustmentAmount,
                    shareAdjustment = re.StockAdjustmentShareAmount,
                    ticker = re.Ticker,
                    edisAccountNumber = re.AssociatedAccountNumber,
                    status = GetEnumDescription(re.Status),
                };
                result.Add(oneRecord);
            }
            return result;
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
        [HttpPost, Route("api/Adviser/CorprateAction/newReturnCapital")]
        public IHttpActionResult CreateNewReturnCapital(ReturnOfCapitalActionCreationModel model)
        {
            var userid = User.Identity.GetUserId();
            //Console.WriteLine("create new return of captial Check");
            if (model != null && ModelState.IsValid)
            {
                ReturnOfCapitalCreationModel repoModel = new ReturnOfCapitalCreationModel();
                repoModel.AccountsInfo = new List<ReturnOfCapitalParticipantAccounts>();
                repoModel.AdjustmentDate = model.returnDate;
                repoModel.AdviserId = userid;
                repoModel.ActionName = model.actionName;
                repoModel.Ticker = model.equityId;
                var partiInfo = model.ParticipantsInfo;
                foreach (var acc in partiInfo) {
                  var newAccount =  new ReturnOfCapitalParticipantAccounts()
                    {
                        AccountNumber = acc.accountNumber,
                        ReturnAmount = acc.returnAmount
                    };
                    repoModel.AccountsInfo.Add(newAccount);
                }
                repo.CreateNewReturnOfCapitalAction(repoModel);


                return Ok();
            }
            return BadRequest();
        }

        [HttpPost, Route("api/Adviser/CorprateAction/newReinvestment")]
        public IHttpActionResult CreateNewReinvestment(ReinvestmentPlanCreationModel model) {
            //repo
            model.AdviserId = userid;
            repo.AdviserCreateNewReinvestmentAdviserInital(model);
            return Ok();
        }
        [HttpPost, Route("api/Adviser/CorprateAction/newStockSplit")]
        public IHttpActionResult CreateNewStockSplit(StockSplitCreationModel model)
        {
            model.AdviserId = userid;
            repo.CreateNewStockSplitAction(model);
            return Ok();
        }


        [HttpPost, Route("api/Adviser/CorprateAction/newBonusIssue")]
        public IHttpActionResult CreateNewBonusIssue(BonusIssueCreationModel model)
        {
            model.AdviserId = userid;
            repo.CreateNewBonusIssueAction(model);
            return Ok();
        }

        [HttpPost, Route("api/Adviser/CorprateAction/newRightsIssue")]
        public IHttpActionResult CreateRightsIssue(RightsIssueCreationModel model)
        {
            model.AdviserId = userid;
            repo.CreateNewRightsIssueActionAdviseInital(model);
            return Ok();
        }

        [HttpPost, Route("api/Adviser/CorprateAction/newBuyBackProgram")]
        public IHttpActionResult CreateBuyBackProgram(BuyBackProgramCreationModel model)
        { 
            model.AdviserId = userid;
            repo.CreateNewBuyBackProgramActionAdviseInital(model);
            return Ok();
        }


        [HttpGet, Route("api/Client/CorperateAction/AllPendingActions")]
        public List<PendingActionViewModel> GetAllPendingCorperateAction() {  
            return repo.GetAllPendingCorporateActionsForClient(userid, ActionRetrieveType.PendingRetrieve);
        }

        [HttpGet, Route("api/Client/CorperateAction/AllActions")]
        public List<PendingActionViewModel> GetAllActionsForClient()
        {
            return repo.GetAllPendingCorporateActionsForClient(userid, ActionRetrieveType.AllActionRetrieve);
        }
        //[HttpPost, Route("api/adviser/corporateAction/getAccounts")]
        //public List<string> getAllAccounts(string EquityId) {
        //    return new List<string>();
        //}


        [HttpPost, Route("api/client/corporateAction/rejectactions")]
        public IHttpActionResult clientRejectActions([FromBody]ClientActionModel model)
        {
            var actionIdIntValue = Convert.ToInt32(model.ActionId);
            repo.ClientRejectCorporateAction(actionIdIntValue);
            return Ok();
        }

        [HttpPost, Route("api/client/corporateAction/acceptactions")]
        public IHttpActionResult clientAcceptActions([FromBody]ClientActionModel model)
        {
            var actionIdIntValue = Convert.ToInt32(model.ActionId);
            repo.ClientAcceptCorporateAction(actionIdIntValue);
            return Ok();
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
            return comRepo.GetAllTIckers().OrderBy(t => t.tickerNumber).ToList();
        }

        [HttpGet, Route("api/Adviser/CorprateAction/PropertyTypes")]
        public List<ClientView> GetAllPropertyTypes() {
            List<ClientView> views = new List<ClientView>();

            foreach(var type in Enum.GetValues(typeof(PropertyType))){
                views.Add(new ClientView { 
                    id = ((int)type).ToString(),
                    name = type.ToString()
                });
            }
            return views;
        }


        [HttpGet, Route("api/Adviser/CorprateAction/TypeOfMortgageRates")]
        public List<ClientView> GetAllTypeOfMortgageRates() {
            List<ClientView> views = new List<ClientView>();

            foreach (var type in Enum.GetValues(typeof(TypeOfMortgageRates))) {
                views.Add(new ClientView {
                    id = ((int)type).ToString(),
                    name = type.ToString()
                });
            }
            return views;
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





        private string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }


    }
}
