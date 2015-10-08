using EDIS_DOMAIN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDISAngular.Models.ServiceModels.CorporateActions;
using EDISAngular.Models.ServiceModels;
using EDISAngular.Infrastructure.DbFirst;



namespace EDISAngular.Infrastructure.DatabaseAccess
{
    public class CorporateActionRepository
    {
        private edisDbEntities db;
        private CommonReferenceDataRepository comRepo;
        private Random rdm = new Random();
        public CorporateActionRepository()
        {
            db = new edisDbEntities();
            comRepo = new CommonReferenceDataRepository(db);

        }
        public CorporateActionRepository(edisDbEntities database)
        {
            db = database;
            comRepo = new CommonReferenceDataRepository(db);
        }
        public List<IPOActionData> GetAllIpoActionsForAdviser(string adviserUserId)
        {
            return new List<IPOActionData>
            {
                new IPOActionData{
                    actionId= "001",
                    nameOfRaising= "rasing 001",
                    IPOCode= "code 01",
                    listed= true,
                    exchange= "Exchange 01",
                    raisingOpened=DateTime.Now,
                    raisingClosed=DateTime.Now,
                    raisingTradingDate=DateTime.Now,
                    dispatchDocDate=DateTime.Now,
                    issuedPrice=randomMoney(),
                    minimumAmount=randomMoney(),
                    dividendPerShare=randomMoney(),
                    dividendYield=randomPercentage(),
                    marketCapitalisation=randomPercentage(),
                    raisingAmount=randomMoney(),
                    numberOfSharesOnIssue=(int)randomPercentage(),
                    numberOfSharesRaising=(int)randomPercentage(),
                    allocationFinalised= false,
                    participants=new List<IPOActionParticipant>{
                        new IPOActionParticipant{
                            edisAccountNumber="0005",
                            brokerAccountNumber="0005",
                            brokerHinSrn="X11115",
                            type="Autolink",
                            name="XYZ 001",
                            investedAmount=randomMoney(),
                            numberOfUnits=(int)randomMoney(),
                            unitPrice=randomMoney(),
                            tickerNumber="001"
                        },new IPOActionParticipant{
                            edisAccountNumber="0006",
                            brokerAccountNumber="0006",
                            brokerHinSrn="X11116",
                            type="Autolink",
                            name="XYZ 002",
                            investedAmount=randomMoney(),
                            numberOfUnits=(int)randomMoney(),
                            unitPrice=randomMoney(),
                            tickerNumber="002"
                        },new IPOActionParticipant{
                            edisAccountNumber="0007",
                            brokerAccountNumber="0007",
                            brokerHinSrn="X11117",
                            type="Autolink",
                            name="XYZ 003",
                            investedAmount=randomMoney(),
                            numberOfUnits=(int)randomMoney(),
                            unitPrice=randomMoney(),
                            tickerNumber="003"
                        },
                    }
                },new IPOActionData{
                    actionId= "002",
                    nameOfRaising= "rasing 002",
                    IPOCode= "code 02",
                    listed= true,
                    exchange= "Exchange 02",
                    raisingOpened=DateTime.Now,
                    raisingClosed=DateTime.Now,
                    raisingTradingDate=DateTime.Now,
                    dispatchDocDate=DateTime.Now,
                    issuedPrice=randomMoney(),
                    minimumAmount=randomMoney(),
                    dividendPerShare=randomMoney(),
                    dividendYield=randomPercentage(),
                    marketCapitalisation=randomPercentage(),
                    raisingAmount=randomMoney(),
                    numberOfSharesOnIssue=(int)randomPercentage(),
                    numberOfSharesRaising=(int)randomPercentage(),
                    allocationFinalised= false,
                    participants=new List<IPOActionParticipant>{
                        new IPOActionParticipant{
                            edisAccountNumber="0005",
                            brokerAccountNumber="0005",
                            brokerHinSrn="X11115",
                            type="Autolink",
                            name="XYZ 001",
                            investedAmount=randomMoney(),
                            numberOfUnits=(int)randomMoney(),
                            unitPrice=randomMoney(),
                            tickerNumber="001"
                        },new IPOActionParticipant{
                            edisAccountNumber="0006",
                            brokerAccountNumber="0006",
                            brokerHinSrn="X11116",
                            type="Autolink",
                            name="XYZ 002",
                            investedAmount=randomMoney(),
                            numberOfUnits=(int)randomMoney(),
                            unitPrice=randomMoney(),
                            tickerNumber="002"
                        },new IPOActionParticipant{
                            edisAccountNumber="0007",
                            brokerAccountNumber="0007",
                            brokerHinSrn="X11117",
                            type="Autolink",
                            name="XYZ 003",
                            investedAmount=randomMoney(),
                            numberOfUnits=(int)randomMoney(),
                            unitPrice=randomMoney(),
                            tickerNumber="003"
                        },
                    }
                },


            };
        }
        public List<OtherCorporateActionData> GetAllOtherActionsForAdviser(string adviserUserId)
        {

            return new List<OtherCorporateActionData>
            {
                new OtherCorporateActionData{
                    actionId= "0001",
                    corporateActionName="Action 001",
                    corporateActionCode= "CODE01",
                    underlyingCompany=new OtherCorporateActionCompany{companyTicker="0001",name="Company 001"},
                    purposeForCorporateAction="Some lengthy purpose here",
                    recordDateEntitlement=DateTime.Now,
                    exEntitlement=DateTime.Now,
                    corporateActionClosingDate=DateTime.Now,
                    corporateActionStartDate=DateTime.Now,
                    dispatchOfHolding="Dispatch of holding",
                    deferredSettlementTradingDate=DateTime.Now,
                    normalTradingDate=DateTime.Now,
                    participants=new List<OtherActionParticipant>{
                        new OtherActionParticipant{
                            edisAccountNumber="0005",
                            brokerAccountNumber="0005",
                            brokerHinSrn="x11115",
                            type="Autolink",
                            name="Peter Truong 001"
                        },new OtherActionParticipant{
                            edisAccountNumber="0006",
                            brokerAccountNumber="0006",
                            brokerHinSrn="x11116",
                            type="Autolink",
                            name="Peter Truong 002"
                        },new OtherActionParticipant{
                            edisAccountNumber="0007",
                            brokerAccountNumber="0007",
                            brokerHinSrn="x11117",
                            type="Autolink",
                            name="Peter Truong 003"
                        }
                    }
                }, new OtherCorporateActionData{
                    actionId= "0002",
                    corporateActionName="Action 002",
                    corporateActionCode= "CODE02",
                    underlyingCompany=new OtherCorporateActionCompany{companyTicker="0002",name="Company 002"},
                    purposeForCorporateAction="Some lengthy purpose here",
                    recordDateEntitlement=DateTime.Now,
                    exEntitlement=DateTime.Now,
                    corporateActionClosingDate=DateTime.Now,
                    corporateActionStartDate=DateTime.Now,
                    dispatchOfHolding="Dispatch of holding",
                    deferredSettlementTradingDate=DateTime.Now,
                    normalTradingDate=DateTime.Now,
                    participants=new List<OtherActionParticipant>{
                        new OtherActionParticipant{
                            edisAccountNumber="0005",
                            brokerAccountNumber="0005",
                            brokerHinSrn="x11115",
                            type="Autolink",
                            name="Peter Truong 001"
                        },new OtherActionParticipant{
                            edisAccountNumber="0006",
                            brokerAccountNumber="0006",
                            brokerHinSrn="x11116",
                            type="Autolink",
                            name="Peter Truong 002"
                        },new OtherActionParticipant{
                            edisAccountNumber="0007",
                            brokerAccountNumber="0007",
                            brokerHinSrn="x11117",
                            type="Autolink",
                            name="Peter Truong 003"
                        }
                    }
                },
            };
        }
        public void CreateNewIpoAction(IpoActionCreationModel model)
        {
            throw new NotImplementedException();
        }
        public void CreateNewOtherAction(OtherActionCreationModel model)
        {
            throw new NotImplementedException();
        }
        public void AllocateIpoAction(IPOActionData model)
        {
            throw new NotImplementedException();
        }
        #region mock helpers
        private double randomMoney()
        {
            return Math.Round(rdm.NextDouble() * 100000, 2);
        }
        private double randomPercentage()
        {
            return Math.Round(rdm.NextDouble() * 100, 2);
        }
        #endregion
    }
}