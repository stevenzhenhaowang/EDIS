using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDISAngular.Models.BindingModels;
using EDISAngular.Models.ViewModels;
using EDISAngular.Services;
using System.Threading.Tasks;
using EDIS_DOMAIN;
using EDISAngular.Models.ServiceModels.AdviserProfile;
using EDISAngular.Models.ServiceModels;
using EDISAngular.Models.ServiceModels.CorporateActions;
using EDISAngular.Infrastructure.DbFirst;

using SqlRepository;

namespace EDISAngular.Infrastructure.DatabaseAccess
{
    public class ClientRepository : IDisposable
    {
        private edisDbEntities db;
        private Random rdm = new Random();
        private EdisRepository repo;
        public ClientRepository()
        {
            repo = new EdisRepository();
            db = new edisDbEntities();
        }
        public ClientRepository(edisDbEntities database)
        {
            db = database;
        }
        //public PreClientViewModel PopulatePreclientProfileAndReturnViewModel(string clientUserId)
        //{

        //    var profile = db.Client_Details.Where(p => p.UserId == clientUserId).SingleOrDefault();
        //    if (profile == null)
        //    {
        //        profile = new Client_Details
        //        {
        //            UserId = clientUserId
        //        };
        //        db.Client_Details.Add(profile);
        //        db.SaveChanges();
        //    }
        //    PreClientViewModel model = new PreClientViewModel
        //    {
        //        ClientType = profile.ClientType
        //    };
        //    return model;

        //}
        //public ClientPersonCompleteProfileBinding GetPersonClientProfileBindingModel(string clientUserId)
        //{
        //    var profile = db.Client_Details.SingleOrDefault(p => p.UserId == clientUserId);
        //    if (profile == null)
        //    {
        //        return (new ClientPersonCompleteProfileBinding());
        //    }
        //    ClientPersonCompleteProfileBinding model = new ClientPersonCompleteProfileBinding
        //    {
        //        Country = profile.Country,
        //        DOB = profile.DateOfBirth,
        //        Fax = profile.Fax,
        //        FirstName = profile.FirstName,
        //        Gender = profile.Gender,
        //        LastName = profile.LastName,
        //        line1 = profile.AddressLn1,
        //        line2 = profile.AddressLn2,
        //        line3 = profile.AddressLn3,
        //        MiddleName = profile.MiddleName,
        //        Mobile = profile.Mobile,
        //        Phone = profile.Phone,
        //        PostCode = profile.PostCode,
        //        State = profile.State,
        //        Suburb = profile.Suburb,
        //        Title = profile.Title,
        //        UserId = clientUserId
        //    };
        //    return model;
        //}
        //public ClientEntityCompleteProfileBinding GetEntityClientProfileBindingModel(string clientUserId)
        //{
        //    var profile = db.Client_Details.SingleOrDefault(p => p.UserId == clientUserId);
        //    if (profile == null)
        //    {
        //        return (new ClientEntityCompleteProfileBinding());
        //    }

        //    ClientEntityCompleteProfileBinding model = new ClientEntityCompleteProfileBinding
        //    {
        //        ABN = profile.abn,
        //        ACN = profile.acn,
        //        Country = profile.Country,
        //        EntityName = profile.EntityName,
        //        EntityType = profile.EntityType,
        //        Fax = profile.Fax,
        //        line1 = profile.AddressLn1,
        //        line2 = profile.AddressLn2,
        //        line3 = profile.AddressLn3,
        //        Mobile = profile.Mobile,
        //        Phone = profile.Phone,
        //        PostCode = profile.PostCode,
        //        State = profile.State,
        //        Suburb = profile.Suburb,
        //        UserID = clientUserId
        //    };
        //    return model;
        //}
        //public RiskProfileBindingModel GetRiskProfileBindingModel(string clientUserId)
        //{
        //    var client = db.Clients.FirstOrDefault(c => c.ClientUserID == clientUserId);
        //    if (client.RiskProfiles.Count > 0)
        //    {
        //        var profile = client.RiskProfiles.OrderByDescending(p => p.DateModified).FirstOrDefault();
        //        return (new RiskProfileBindingModel
        //        {
        //            capitalLossAttitude = profile.CapitalLossAttitude,
        //            shortTermTrading = profile.ShortTermTrading,
        //            shortTermGoal3 = profile.ShortTermGoal3,
        //            shortTermGoal2 = profile.ShortTermGoal2,
        //            shortTermGoal1 = profile.ShortTermGoal1,
        //            shortTermEquityPercent = profile.ShortTermEquityPercent,
        //            shortTermAssetPercent = profile.ShortTermAssetPercent,
        //            riskAttitude = profile.RiskAttitude,
        //            retirementIncome = profile.RetirementIncome,
        //            retirementAge = profile.RetirementAge.ToString(),
        //            investmentProfile = profile.InvestmentProfile,
        //            clientId = profile.ClientID,
        //            comments = profile.Comments,
        //            incomeSource = profile.IncomeSource,
        //            investmentKnowledge = profile.InvestmentKnowledge,
        //            investmentObjective1 = profile.InvestmentObjective1,
        //            investmentObjective2 = profile.InvestmentObjective2,
        //            investmentObjective3 = profile.InvestmentObjective3,
        //            investmentTimeHorizon = profile.InvestmentTimeHorizon,
        //            longTermGoal1 = profile.LongTermGoal1,
        //            longTermGoal2 = profile.LongTermGoal2,
        //            longTermGoal3 = profile.LongTermGoal3,
        //            medTermGoal1 = profile.MedTermGoal1,
        //            medTermGoal2 = profile.MedTermGoal2,
        //            medTermGoal3 = profile.MedTermGoal3,
        //            profileId = profile.RiskProfileID
        //        });
        //    }


        //    return (new RiskProfileBindingModel { clientId = client.ClientUserID });
        //}
        //public IQueryable<Client> GetAllClients()
        //{
        //    return db.Clients;
        //}
        //public void AddOrUpdatePersonProfile(ClientPersonCompleteProfileBinding model)
        //{
        //    var profile = db.Client_Details.SingleOrDefault(c => c.UserId == model.UserId);
        //    profile.AddressLn1 = model.line1;
        //    profile.AddressLn2 = model.line2;
        //    profile.AddressLn3 = model.line3;
        //    profile.ClientType = BusinessLayerParameters.clientType_person;
        //    profile.Country = model.Country;
        //    profile.DateOfBirth = model.DOB;
        //    profile.Fax = model.Fax;
        //    profile.FirstName = model.FirstName;
        //    profile.Gender = model.Gender;
        //    profile.LastName = model.LastName;
        //    profile.LastUpdated = DateTime.Now;
        //    profile.MiddleName = model.MiddleName;
        //    profile.Mobile = model.Mobile;
        //    profile.Phone = model.Phone;
        //    profile.PostCode = model.PostCode;
        //    profile.State = model.State;
        //    profile.Suburb = model.Suburb;
        //    profile.Title = model.Title;
        //    profile.Lat = GoogleGeoService.GetCoordinatesLat(model.line1 + " "
        //        + model.line2 + " " + model.line3 + " " + model.State + " " + model.Country);
        //    profile.Lng = GoogleGeoService.GetCoordinatesLng(model.line1 + " "
        //        + model.line2 + " " + model.line3 + " " + model.State + " " + model.Country);
        //}
        //public void AddOrUpdateClientProfileImage(string clientUserId, HttpPostedFileBase image)
        //{
        //    var client = db.Clients.FirstOrDefault(c => c.ClientUserID == clientUserId);
        //    if (client != null)
        //    {

        //        client.ImageMimeType = image.ContentType;
        //        client.Image = new byte[image.ContentLength];
        //        image.InputStream.Read(client.Image, 0, image.ContentLength);
        //    }
        //    else
        //    {
        //        throw new Exception("Cannot find client profile, invalid client user id supplied");
        //    }
        //}
        //public void AddOrUpdateEntityProfile(ClientEntityCompleteProfileBinding model)
        //{
        //    var profile = db.Client_Details.SingleOrDefault(c => c.UserId == model.UserID);
        //    profile.AddressLn1 = model.line1;
        //    profile.AddressLn2 = model.line2;
        //    profile.AddressLn3 = model.line3;
        //    profile.ClientType = BusinessLayerParameters.clientType_person;
        //    profile.Country = model.Country;
        //    profile.Fax = model.Fax;
        //    profile.LastUpdated = DateTime.Now;
        //    profile.Mobile = model.Mobile;
        //    profile.Phone = model.Phone;
        //    profile.PostCode = model.PostCode;
        //    profile.State = model.State;
        //    profile.Suburb = model.Suburb;
        //    profile.abn = model.ABN;
        //    profile.acn = model.ACN;
        //    profile.EntityName = model.EntityName;
        //    profile.EntityType = model.EntityType;
        //}
        //public void AddOrUpdateRiskProfile(RiskProfileBindingModel model)
        //{
        //    var newProfile = new RiskProfile
        //    {
        //        CapitalLossAttitude = model.capitalLossAttitude,
        //        ClientID = model.clientId,
        //        Comments = model.comments,
        //        DateCreated = DateTime.Now,
        //        DateModified = DateTime.Now,
        //        IncomeSource = model.incomeSource,
        //        InvestmentKnowledge = model.investmentKnowledge,
        //        InvestmentObjective1 = model.investmentObjective1,
        //        InvestmentObjective2 = model.investmentObjective2,
        //        InvestmentObjective3 = model.investmentObjective3,
        //        InvestmentProfile = model.investmentProfile,
        //        InvestmentTimeHorizon = model.investmentTimeHorizon,
        //        LongTermGoal1 = model.longTermGoal1,
        //        LongTermGoal2 = model.longTermGoal2,
        //        LongTermGoal3 = model.longTermGoal3,
        //        MedTermGoal1 = model.medTermGoal1,
        //        MedTermGoal2 = model.medTermGoal2,
        //        MedTermGoal3 = model.medTermGoal3,
        //        RetirementAge = string.IsNullOrEmpty(model.retirementAge) ? (int?)null : Convert.ToInt32(model.retirementAge),
        //        RetirementIncome = model.retirementIncome,
        //        RiskProfileID = Guid.NewGuid().ToString(),
        //        RiskAttitude = model.riskAttitude,
        //        ShortTermAssetPercent = model.shortTermAssetPercent,
        //        ShortTermEquityPercent = model.shortTermEquityPercent,
        //        ShortTermGoal1 = model.shortTermGoal1,
        //        ShortTermGoal2 = model.shortTermGoal2,
        //        ShortTermGoal3 = model.shortTermGoal3,
        //        ShortTermTrading = model.shortTermTrading
        //    };

        //    if (string.IsNullOrEmpty(model.profileId))
        //    {
        //        db.RiskProfiles.Add(newProfile);
        //    }
        //    else
        //    {
        //        var profile = db.RiskProfiles.FirstOrDefault(p => p.RiskProfileID == model.profileId);

        //        if (profile == null)
        //        {
        //            throw new Exception("Cannot find risk profile for client");
        //        }
        //        profile.CapitalLossAttitude = newProfile.CapitalLossAttitude;
        //        profile.Comments = newProfile.Comments;
        //        profile.DateModified = DateTime.Now;
        //        profile.IncomeSource = newProfile.IncomeSource;
        //        profile.InvestmentKnowledge = newProfile.InvestmentKnowledge;
        //        profile.InvestmentObjective1 = newProfile.InvestmentObjective1;
        //        profile.InvestmentObjective2 = newProfile.InvestmentObjective2;
        //        profile.InvestmentObjective3 = newProfile.InvestmentObjective3;
        //        profile.InvestmentProfile = newProfile.InvestmentProfile;
        //        profile.InvestmentTimeHorizon = newProfile.InvestmentTimeHorizon;
        //        profile.LongTermGoal1 = newProfile.LongTermGoal1;
        //        profile.LongTermGoal2 = newProfile.LongTermGoal2;
        //        profile.LongTermGoal3 = newProfile.LongTermGoal3;
        //        profile.MedTermGoal1 = newProfile.MedTermGoal1;
        //        profile.MedTermGoal2 = newProfile.MedTermGoal2;
        //        profile.MedTermGoal3 = newProfile.MedTermGoal3;
        //        profile.RetirementAge = newProfile.RetirementAge;
        //        profile.RetirementIncome = newProfile.RetirementIncome;
        //        profile.RiskAttitude = newProfile.RiskAttitude;
        //        profile.ShortTermAssetPercent = newProfile.ShortTermAssetPercent;
        //        profile.ShortTermEquityPercent = newProfile.ShortTermEquityPercent;
        //        profile.ShortTermGoal1 = newProfile.ShortTermGoal1;
        //        profile.ShortTermGoal2 = newProfile.ShortTermGoal2;
        //        profile.ShortTermGoal3 = newProfile.ShortTermGoal3;
        //        profile.ShortTermTrading = newProfile.ShortTermTrading;
        //    }

        //}
        //private Client RetrieveProfileAndEnsureClientProfileExist(string clientUserId)
        //{
        //    if (string.IsNullOrEmpty(clientUserId))
        //    {
        //        throw new Exception("Invalid client user id supplied");
        //    }
        //    var profile = db.Clients.FirstOrDefault(c => c.ClientUserID == clientUserId);
        //    if (profile != null)
        //    {
        //        return profile;
        //    }
        //    else
        //    {
        //        profile = new Client
        //        {
        //            ClientUserID = clientUserId
        //        };
        //        db.Clients.Add(profile);
        //        return profile;
        //    }
        //}

        #region services added on 18/05/2015
        public BusinessPortfolioOverviewBriefModel GetBusinessRevenueData(string clientUserId)
        {
            return new BusinessPortfolioOverviewBriefModel
            {
                data = new List<DataNameAmountPair>
            {
                new DataNameAmountPair{name="Assets Under Management Fees", amount=randomMoney()},
                new DataNameAmountPair{name="Assets Under Management Fees", amount=randomMoney()},
                new DataNameAmountPair{name="Fee for Service Advice", amount=randomMoney()},
                new DataNameAmountPair{name="Miscellaneous Revenue", amount=randomMoney()},
                new DataNameAmountPair{name="Commission Trail from Investments(Grandfathered)", amount=randomMoney()},
            },
                total = randomMoney()
            };
        }
        public BusinessPortfolioOverviewBriefModel GetDebtInstrumentsData(string clientUserId)
        {
            return new BusinessPortfolioOverviewBriefModel
            {
                data = new List<DataNameAmountPair>
            {
                new DataNameAmountPair{name="Mortgage & Investment Home Loans", amount=randomMoney()},
                new DataNameAmountPair{name="Commercial Loans", amount=randomMoney()},
                new DataNameAmountPair{name="Margin Lending Loans", amount=randomMoney()},
                new DataNameAmountPair{name="Personal & Credit Card Loans", amount=randomMoney()},
                new DataNameAmountPair{name="Lending & Debt Statistics", amount=randomMoney()},
            },
                total = randomMoney()
            };
        }
        public ProfileInsuranceStatisticsModel GetInsuranceStatisticsData(string clientUserId)
        {
            return new ProfileInsuranceStatisticsModel
            {
                total = randomMoney(),
                data = new List<DataNameAmountPair>
                {
                    new DataNameAmountPair{name="Motor Vehicles & Fleet Insurance", amount=randomMoney()},
                    new DataNameAmountPair{name="Business Related Insurance", amount=randomMoney()},
                    new DataNameAmountPair{name="General Insurance", amount=randomMoney()},
                    new DataNameAmountPair{name="Income Protection Insurance", amount=randomMoney()},
                    new DataNameAmountPair{name="Trauma Insurance", amount=randomMoney()},
                    new DataNameAmountPair{name="Term Life Insurance", amount=randomMoney()},
                    new DataNameAmountPair{name="Total & Permanent Disability Insurance", amount=randomMoney()},
                },
                group = new List<ProfileInsuranceStatisticsGroup>
                {
                    new ProfileInsuranceStatisticsGroup{ 
                        name="Asset & Revenue",
                        stat=new List<DataNameAmountPair>{ 
                            new DataNameAmountPair{name="Motor Vehicles & Fleet Insurance", amount=randomMoney()},
                            new DataNameAmountPair{name="Business Related Insurance", amount=randomMoney()},
                            new DataNameAmountPair{name="General Insurance", amount=randomMoney()},
                        }
                    },new ProfileInsuranceStatisticsGroup{ 
                        name="Personal Insurance",
                        stat=new List<DataNameAmountPair>{ 
                            new DataNameAmountPair{name="Income Protection Insurance", amount=randomMoney()},
                            new DataNameAmountPair{name="Trauma Insurance", amount=randomMoney()},
                            new DataNameAmountPair{name="Term Life Insurance", amount=randomMoney()},
                        }
                    },new ProfileInsuranceStatisticsGroup{ 
                        name="Liability Insurance",
                        stat=new List<DataNameAmountPair>{ 
                            new DataNameAmountPair{name="Public Liability", amount=randomMoney()},
                            new DataNameAmountPair{name="Professional Indemnity", amount=randomMoney()},
                            new DataNameAmountPair{name="Product Liability", amount=randomMoney()},
                        }
                    },
                },

            };
        }
        public List<WordMarketItemModel> GetWorldMarketData(string clientUserId)
        {
            return new List<WordMarketItemModel>
            {
                new WordMarketItemModel{name="Shanghai",value=randomMoney(),changedRatePercentage=randomPercentage(),changedValue=randomPercentage()},
                new WordMarketItemModel{name="Hang Seng Index",value=randomMoney(),changedRatePercentage=randomPercentage(),changedValue=randomPercentage()},
                new WordMarketItemModel{name="TSEC",value=randomMoney(),changedRatePercentage=randomPercentage(),changedValue=randomPercentage()},
                new WordMarketItemModel{name="Shanghai",value=randomMoney(),changedRatePercentage=randomPercentage(),changedValue=randomPercentage()},
                new WordMarketItemModel{name="Hang Seng Index",value=randomMoney(),changedRatePercentage=randomPercentage(),changedValue=randomPercentage()},
                new WordMarketItemModel{name="TSEC",value=randomMoney(),changedRatePercentage=randomPercentage(),changedValue=randomPercentage()},
            };
        }
        public List<WordMarketItemModel> GetCurrencies(string clientUserId)
        {
            return new List<WordMarketItemModel>
            {
                new WordMarketItemModel{name="EUR/USD",value=randomMoney(),changedRatePercentage=randomPercentage(),changedValue=randomPercentage()},
                new WordMarketItemModel{name="USD/JPY",value=randomMoney(),changedRatePercentage=randomPercentage(),changedValue=randomPercentage()},
                new WordMarketItemModel{name="EUR/USD",value=randomMoney(),changedRatePercentage=randomPercentage(),changedValue=randomPercentage()},
                new WordMarketItemModel{name="USD/JPY",value=randomMoney(),changedRatePercentage=randomPercentage(),changedValue=randomPercentage()},
                new WordMarketItemModel{name="EUR/USD",value=randomMoney(),changedRatePercentage=randomPercentage(),changedValue=randomPercentage()},
                new WordMarketItemModel{name="USD/JPY",value=randomMoney(),changedRatePercentage=randomPercentage(),changedValue=randomPercentage()},
            };
        }
        public HistoricalRevenueModel GetHistoricalRevenueData(string clientUserId)
        {
            return new HistoricalRevenueModel
            {
                data = new List<HistoricalRevenueItem>
                {
                    new HistoricalRevenueItem{title="Current Business Revenue", amount=randomMoney()},
                    new HistoricalRevenueItem{title="2011-2012 Business Revenue", amount=randomMoney()},
                    new HistoricalRevenueItem{title="2010-2011 Business Revenue", amount=randomMoney()},
                    new HistoricalRevenueItem{title="2009-2010 Business Revenue", amount=randomMoney()},
                },
                total = randomMoney()
            };
        }
        public BusinessStatDetailModel GetInvestmentStat(string clientUserId)
        {
            return new BusinessStatDetailModel
            {
                total = randomMoney(),
                data = new List<BusinessStatDataItem>
                {
                    new BusinessStatDataItem { 
                        Name="AUstralian Equity",
                        Value=randomMoney(),
                        PL=randomMoney(),
                        Stock=new List<BusinessStatStock>{  
                            new BusinessStatStock{ 
                                Label="ASX",
                                Name="Australian Stock Exchange",
                                Units=rdm.Next() * 1000,
                                Value=randomMoney(),
                                PL=randomMoney(),
                                Accounts=new List<BusinessStatStockAccount>{ 
                                    new BusinessStatStockAccount{
                                        AccountName="Mr. X & Mrs. X",
                                        AccountNumber="001",
                                        Units=rdm.Next() * 1000,
                                        Value=randomMoney(),
                                        PL=randomMoney()
                                    },new BusinessStatStockAccount{
                                        AccountName="Mr. Y & Mrs. Y",
                                        AccountNumber="002",
                                        Units=rdm.Next() * 1000,
                                        Value=randomMoney(),
                                        PL=randomMoney()
                                    },new BusinessStatStockAccount{
                                        AccountName="Mr. Z & Mrs. Z",
                                        AccountNumber="003",
                                        Units=rdm.Next() * 1000,
                                        Value=randomMoney(),
                                        PL=randomMoney()
                                    },
                                }
                            },new BusinessStatStock{ 
                                Label="BHP",
                                Name="BHP Billiton Ltd",
                                Units=rdm.Next() * 1000,
                                Value=randomMoney(),
                                PL=randomMoney(),
                                Accounts=new List<BusinessStatStockAccount>{ 
                                    new BusinessStatStockAccount{
                                        AccountName="Mr. X & Mrs. X",
                                        AccountNumber="001",
                                        Units=rdm.Next() * 1000,
                                        Value=randomMoney(),
                                        PL=randomMoney()
                                    },new BusinessStatStockAccount{
                                        AccountName="Mr. Y & Mrs. Y",
                                        AccountNumber="002",
                                        Units=rdm.Next() * 1000,
                                        Value=randomMoney(),
                                        PL=randomMoney()
                                    },new BusinessStatStockAccount{
                                        AccountName="Mr. Z & Mrs. Z",
                                        AccountNumber="003",
                                        Units=rdm.Next() * 1000,
                                        Value=randomMoney(),
                                        PL=randomMoney()
                                    },
                                }
                            },
                        }
                    }
                }
            };
        }
        public BusinessStatDetailModel GetLendingStat(string clientUserId)
        {
            return new BusinessStatDetailModel
            {
                total = randomMoney(),
                data = new List<BusinessStatDataItem>
                {
                    new BusinessStatDataItem { 
                        Name="Mortages & Investment Home Loans",
                        Value=randomMoney(),
                        PL=randomMoney(),
                        Stock=new List<BusinessStatStock>{  
                            new BusinessStatStock{ 
                                Label="Loan1",
                                Name="Australian Stock Exchange",
                                Units=rdm.Next() * 1000,
                                Value=randomMoney(),
                                PL=randomMoney(),
                                Accounts=new List<BusinessStatStockAccount>{ 
                                    new BusinessStatStockAccount{
                                        AccountName="Mr. X & Mrs. X",
                                        AccountNumber="001",
                                        Units=rdm.Next() * 1000,
                                        Value=randomMoney(),
                                        PL=randomMoney()
                                    },new BusinessStatStockAccount{
                                        AccountName="Mr. Y & Mrs. Y",
                                        AccountNumber="002",
                                        Units=rdm.Next() * 1000,
                                        Value=randomMoney(),
                                        PL=randomMoney()
                                    },new BusinessStatStockAccount{
                                        AccountName="Mr. Z & Mrs. Z",
                                        AccountNumber="003",
                                        Units=rdm.Next() * 1000,
                                        Value=randomMoney(),
                                        PL=randomMoney()
                                    },
                                }
                            },new BusinessStatStock{ 
                                Label="Loan2",
                                Name="BHP Billiton Ltd",
                                Units=rdm.Next() * 1000,
                                Value=randomMoney(),
                                PL=randomMoney(),
                                Accounts=new List<BusinessStatStockAccount>{ 
                                    new BusinessStatStockAccount{
                                        AccountName="Mr. X & Mrs. X",
                                        AccountNumber="001",
                                        Units=rdm.Next() * 1000,
                                        Value=randomMoney(),
                                        PL=randomMoney()
                                    },new BusinessStatStockAccount{
                                        AccountName="Mr. Y & Mrs. Y",
                                        AccountNumber="002",
                                        Units=rdm.Next() * 1000,
                                        Value=randomMoney(),
                                        PL=randomMoney()
                                    },new BusinessStatStockAccount{
                                        AccountName="Mr. Z & Mrs. Z",
                                        AccountNumber="003",
                                        Units=rdm.Next() * 1000,
                                        Value=randomMoney(),
                                        PL=randomMoney()
                                    },
                                }
                            },
                        }
                    }
                }
            };
        }
        public InsuranceStatModel GetInsuranceStatDetailed(string clientUserId)
        {
            return new InsuranceStatModel
            {
                total = randomMoney(),
                data = new List<InsuranceStateItem>
                {
                    new InsuranceStateItem{
                         Content= new List<BusinessStatDataItem>{ 
                            new BusinessStatDataItem { 
                                Name="Mortages & Investment Home Loans",
                                Value=randomMoney(),
                                PL=randomMoney(),
                                Stock=new List<BusinessStatStock>{  
                                    new BusinessStatStock{ 
                                        Label="Loan1",
                                        Name="Australian Stock Exchange",
                                        Units=rdm.Next() * 1000,
                                        Value=randomMoney(),
                                        PL=randomMoney(),
                                        Accounts=new List<BusinessStatStockAccount>{ 
                                            new BusinessStatStockAccount{
                                                AccountName="Mr. X & Mrs. X",
                                                AccountNumber="001",
                                                Units=rdm.Next() * 1000,
                                                Value=randomMoney(),
                                                PL=randomMoney()
                                            },new BusinessStatStockAccount{
                                                AccountName="Mr. Y & Mrs. Y",
                                                AccountNumber="002",
                                                Units=rdm.Next() * 1000,
                                                Value=randomMoney(),
                                                PL=randomMoney()
                                            },new BusinessStatStockAccount{
                                                AccountName="Mr. Z & Mrs. Z",
                                                AccountNumber="003",
                                                Units=rdm.Next() * 1000,
                                                Value=randomMoney(),
                                                PL=randomMoney()
                                            },
                                        }
                                    },new BusinessStatStock{ 
                                        Label="Loan2",
                                        Name="BHP Billiton Ltd",
                                        Units=rdm.Next() * 1000,
                                        Value=randomMoney(),
                                        PL=randomMoney(),
                                        Accounts=new List<BusinessStatStockAccount>{ 
                                            new BusinessStatStockAccount{
                                                AccountName="Mr. X & Mrs. X",
                                                AccountNumber="001",
                                                Units=rdm.Next() * 1000,
                                                Value=randomMoney(),
                                                PL=randomMoney()
                                            },new BusinessStatStockAccount{
                                                AccountName="Mr. Y & Mrs. Y",
                                                AccountNumber="002",
                                                Units=rdm.Next() * 1000,
                                                Value=randomMoney(),
                                                PL=randomMoney()
                                            },new BusinessStatStockAccount{
                                                AccountName="Mr. Z & Mrs. Z",
                                                AccountNumber="003",
                                                Units=rdm.Next() * 1000,
                                                Value=randomMoney(),
                                                PL=randomMoney()
                                            },
                                        }
                                    },
                                }
                            }
                         },
                          Type="Assets & Revenue"
                    },
                    
                }
            };
        }
        public List<ClientPositionMonitorModel> GetClientPositionMonitor(string clientUserId)
        {
            return new List<ClientPositionMonitorModel>
            {
                new ClientPositionMonitorModel{
                    AccountNumber= "20130001",
                    AccountName= "Alfa Romeo Pty Ltd",
                    AssetClass= "",
                    NetCost= 250000.00,
                    MarketValue= 285000.00,
                    PL= 35000,
                    PLRate= 14.0,
                    CompliantStatus= "Compliant",
                    Reminders= 0,
                    Type=new List<ClientPositionMonitorType>{ 
                        new ClientPositionMonitorType{ 
                            Name="Equity",
                            Data=new List<ClientPositionMonitorTypeData>{ 
                                new ClientPositionMonitorTypeData{ 
                                    Name= "Australian Equity",
                                    NetCost= 500000,
                                    MarketValue= 55000,
                                    PL= 5000,
                                    PLRate= 10,
                                    CompliantStatus= "Compliant",
                                    Reminders= 0
                                }
                            }
                        }
                    }
                },new ClientPositionMonitorModel{
                    AccountNumber= "20130002",
                    AccountName= "Alfa Romeo Pty Ltd",
                    AssetClass= "",
                    NetCost= 250000.00,
                    MarketValue= 285000.00,
                    PL= 35000,
                    PLRate= 14.0,
                    CompliantStatus= "Compliant",
                    Reminders= 0,
                    Type=new List<ClientPositionMonitorType>{ 
                        new ClientPositionMonitorType{ 
                            Name="Equity",
                            Data=new List<ClientPositionMonitorTypeData>{ 
                                new ClientPositionMonitorTypeData{ 
                                    Name= "Australian Equity",
                                    NetCost= 500000,
                                    MarketValue= 55000,
                                    PL= 5000,
                                    PLRate= 10,
                                    CompliantStatus= "Compliant",
                                    Reminders= 0
                                }
                            }
                        }
                    }
                },
            };
        }
        public List<GeoGraphicalLocations> GetGeoLocations(string clientUserId)
        {
            return new List<GeoGraphicalLocations>
            {
                new GeoGraphicalLocations{
                    Clients=rdm.Next()* 10000,
                    id="1",
                    State="Victoria",
                    Data=new List<GeoCityData>{ 
                        new GeoCityData{ 
                            City="Melbourne",
                            Clients=rdm.Next() * 10000,
                            id="1",
                            Data=new List<GeoSuburbData>{
                                new GeoSuburbData{
                                    id="100",
                                    Suburb="CBD & Inner Suburbs",
                                    Clients=rdm.Next() * 10000
                                },new GeoSuburbData{
                                    id="101",
                                    Suburb="Eastern Suburbs",
                                    Clients=rdm.Next() * 10000
                                },
                            }
                        }
                    }
                }
            };
        }
        public GeoStatsModel GetGeoStats(string clientUserId, string[] geoIds)
        {
            return new GeoStatsModel
            {
                ClientByAge = new GeoGroupItem
                {
                    Total = rdm.Next() * 1000,
                    Data = new List<GeoGroupDataItem>
                    {
                        new GeoGroupDataItem{Group="Client in Pension",Number=rdm.Next() * 10000},
                        new GeoGroupDataItem{Group="Clients in Transition to Retirement",Number=rdm.Next() * 10000},
                        new GeoGroupDataItem{Group="Client in Accumulation Phase",Number=rdm.Next() * 10000}
                    }
                },
                AssetsUnderManagement = new GeoGroupItem
                {
                    Total = rdm.Next() * 1000,
                    Data = new List<GeoGroupDataItem>
                    {
                        new GeoGroupDataItem{Group="Greater than $1,000,000",Number=rdm.Next() * 10000},
                        new GeoGroupDataItem{Group="$500,000 - $1,000,000",Number=rdm.Next() * 10000},
                        new GeoGroupDataItem{Group="$250,000 - $500,000",Number=rdm.Next() * 10000},
                        new GeoGroupDataItem{Group="$Less Than $250,000",Number=rdm.Next() * 10000},
                    }
                },
                ClientProfileClassification = new GeoGroupItem
                {
                    Total = rdm.Next() * 1000,
                    Data = new List<GeoGroupDataItem>
                    {
                        new GeoGroupDataItem{Group="Ultra Defensive",Number=rdm.Next() * 10000},
                        new GeoGroupDataItem{Group="Defensive",Number=rdm.Next() * 10000},
                        new GeoGroupDataItem{Group="Conservative",Number=rdm.Next() * 10000},
                        new GeoGroupDataItem{Group="Balanced",Number=rdm.Next() * 10000},
                        new GeoGroupDataItem{Group="Assertive",Number=rdm.Next() * 10000},
                        new GeoGroupDataItem{Group="Aggressive",Number=rdm.Next() * 10000},
                    }
                },
                ClientTimeFrameClassification = new GeoGroupItem
                {
                    Total = rdm.Next() * 1000,
                    Data = new List<GeoGroupDataItem>
                    {
                        new GeoGroupDataItem{Group="Less than 12 months",Number=rdm.Next() * 10000},
                        new GeoGroupDataItem{Group="1 year to 3 years",Number=rdm.Next() * 10000},
                        new GeoGroupDataItem{Group="Greater than 5 years",Number=rdm.Next() * 10000},
                    }
                },
                TopUsedDebt = new GeoRankingItem
                {
                    Data = new List<GeoRankingDataItem>
                    {
                        new GeoRankingDataItem{Name="1",Value=rdm.Next()*1000},
                        new GeoRankingDataItem{Name="2",Value=rdm.Next()*1000},
                        new GeoRankingDataItem{Name="3",Value=rdm.Next()*1000},
                        new GeoRankingDataItem{Name="4",Value=rdm.Next()*1000},
                        new GeoRankingDataItem{Name="5",Value=rdm.Next()*1000},
                        new GeoRankingDataItem{Name="6",Value=rdm.Next()*1000},
                        new GeoRankingDataItem{Name="7",Value=rdm.Next()*1000},
                        new GeoRankingDataItem{Name="8",Value=rdm.Next()*1000},
                        new GeoRankingDataItem{Name="9",Value=rdm.Next()*1000},
                        new GeoRankingDataItem{Name="10",Value=rdm.Next()*1000},
                    },
                    Total = rdm.Next() * 10000
                },
                TopUsedInvestments = new GeoRankingItem
                {
                    Data = new List<GeoRankingDataItem>
                    {
                        new GeoRankingDataItem{Name="1",Value=rdm.Next()*1000},
                        new GeoRankingDataItem{Name="2",Value=rdm.Next()*1000},
                        new GeoRankingDataItem{Name="3",Value=rdm.Next()*1000},
                        new GeoRankingDataItem{Name="4",Value=rdm.Next()*1000},
                        new GeoRankingDataItem{Name="5",Value=rdm.Next()*1000},
                        new GeoRankingDataItem{Name="6",Value=rdm.Next()*1000},
                        new GeoRankingDataItem{Name="7",Value=rdm.Next()*1000},
                        new GeoRankingDataItem{Name="8",Value=rdm.Next()*1000},
                        new GeoRankingDataItem{Name="9",Value=rdm.Next()*1000},
                        new GeoRankingDataItem{Name="10",Value=rdm.Next()*1000},
                    },
                    Total = rdm.Next() * 10000
                }


            };
        }
        public BuisnessRevenueDetailsDataModel GetBusinessRevenueDetails(string clientUserId)
        {
            return new BuisnessRevenueDetailsDataModel
            {
                total = randomMoney(),
                data = new List<BusinessRevenueItem>
                {
                    new BusinessRevenueItem{ 
                        amount=randomMoney(),
                        name="Assets Under management Fees", 
                        Accounts=new List<BusinessRevenueAccount>{
                            new BusinessRevenueAccount{ AccountName="Mr. Client X", AccountNumber="20130001", AUM=randomMoney().ToString(), Revenue=randomMoney()},
                            new BusinessRevenueAccount{ AccountName="Mr. Client Y", AccountNumber="20130001", AUM=randomMoney().ToString(), Revenue=randomMoney()},
                            new BusinessRevenueAccount{ AccountName="Mr. Client Z", AccountNumber="20130001", AUM=randomMoney().ToString(), Revenue=randomMoney()},
                            new BusinessRevenueAccount{ AccountName="Mr. Client E", AccountNumber="20130001", AUM=randomMoney().ToString(), Revenue=randomMoney()},
                        }
                    },new BusinessRevenueItem{ 
                        amount=randomMoney(),
                        name="Fee for Service Advice", 
                        Accounts=new List<BusinessRevenueAccount>{
                            new BusinessRevenueAccount{ AccountName="Mr. Client X", AccountNumber="20130001", AUM=randomMoney().ToString(), Revenue=randomMoney()},
                            new BusinessRevenueAccount{ AccountName="Mr. Client Y", AccountNumber="20130001", AUM=randomMoney().ToString(), Revenue=randomMoney()},
                            new BusinessRevenueAccount{ AccountName="Mr. Client Z", AccountNumber="20130001", AUM=randomMoney().ToString(), Revenue=randomMoney()},
                            new BusinessRevenueAccount{ AccountName="Mr. Client E", AccountNumber="20130001", AUM=randomMoney().ToString(), Revenue=randomMoney()},
                        }
                    },new BusinessRevenueItem{ 
                        amount=randomMoney(),
                        name="Commission Trail from Investments(Grandfathered)", 
                        Accounts=new List<BusinessRevenueAccount>{
                            new BusinessRevenueAccount{ AccountName="Mr. Client X", AccountNumber="20130001", AUM=randomMoney().ToString(), Revenue=randomMoney()},
                            new BusinessRevenueAccount{ AccountName="Mr. Client Y", AccountNumber="20130001", AUM=randomMoney().ToString(), Revenue=randomMoney()},
                            new BusinessRevenueAccount{ AccountName="Mr. Client Z", AccountNumber="20130001", AUM=randomMoney().ToString(), Revenue=randomMoney()},
                            new BusinessRevenueAccount{ AccountName="Mr. Client E", AccountNumber="20130001", AUM=randomMoney().ToString(), Revenue=randomMoney()},
                        }
                    },
                }
            };
        }
        public CompliantModel GetComplianceDetails(string clientUserId)
        {
            return new CompliantModel
            {
                ComplianceOverview = new ComplianceOverview
                {
                    total = randomMoney(),
                    data = new List<ComplianceOverviewItem>
                    {
                        new ComplianceOverviewItem{
                            Number=1,
                            Group="Australian Equity",
                            Stocks=new List<ComplianceOverviewStock>{ 
                                new ComplianceOverviewStock{ Label="ASX", Name="Australian Stock Exchange"},
                                new ComplianceOverviewStock{ Label="ASX", Name="Australian Stock Exchange"},
                                new ComplianceOverviewStock{ Label="ASX", Name="Australian Stock Exchange"},
                                new ComplianceOverviewStock{ Label="ASX", Name="Australian Stock Exchange"},
                                new ComplianceOverviewStock{ Label="ASX", Name="Australian Stock Exchange"},
                                new ComplianceOverviewStock{ Label="ASX", Name="Australian Stock Exchange"},
                                new ComplianceOverviewStock{ Label="ASX", Name="Australian Stock Exchange"},
                            }
                        },new ComplianceOverviewItem{
                            Number=2,
                            Group="Hybrid Security",
                            Stocks=new List<ComplianceOverviewStock>{ 
                                new ComplianceOverviewStock{ Label="ASX", Name="Australian Stock Exchange"},
                                new ComplianceOverviewStock{ Label="ASX", Name="Australian Stock Exchange"},
                                new ComplianceOverviewStock{ Label="ASX", Name="Australian Stock Exchange"},
                                new ComplianceOverviewStock{ Label="ASX", Name="Australian Stock Exchange"},
                                new ComplianceOverviewStock{ Label="ASX", Name="Australian Stock Exchange"},
                                new ComplianceOverviewStock{ Label="ASX", Name="Australian Stock Exchange"},
                                new ComplianceOverviewStock{ Label="ASX", Name="Australian Stock Exchange"},
                            }
                        },
                    }
                },
                CompliantFiles = new CompliantFiles
                {
                    total = randomMoney(),
                    data = new List<CompliantFilesItem>
                    {
                        new CompliantFilesItem{
                             Group="Compliant Files", Number=400,
                             Clients=new List<CompliantFilesClient>{
                                 new CompliantFilesClient{AccountGroupNumber="200000",AccountName="Mr X and Mrs X"},
                                 new CompliantFilesClient{AccountGroupNumber="200000",AccountName="Mr X and Mrs X"},
                                 new CompliantFilesClient{AccountGroupNumber="200000",AccountName="Mr X and Mrs X"},
                             }
                        },
                        new CompliantFilesItem{
                             Group="Compliant Files", Number=400,
                             Clients=new List<CompliantFilesClient>{
                                 new CompliantFilesClient{AccountGroupNumber="200000",AccountName="Mr X and Mrs X"},
                                 new CompliantFilesClient{AccountGroupNumber="200000",AccountName="Mr X and Mrs X"},
                                 new CompliantFilesClient{AccountGroupNumber="200000",AccountName="Mr X and Mrs X"},
                             }
                        },
                    }
                }

            };
        }
        public ReminderModel GetReminders(string clientUserId)
        {
            return new ReminderModel
            {
                Monthly = new List<ReminderTimeGroupmodel>
                {
                    new ReminderTimeGroupmodel{ 
                        group="All Reminders",
                        data=new List<ReminderTimeGroupDataItem>{
                            new ReminderTimeGroupDataItem{
                                type="No. of Clients having Birthdays today",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            },new ReminderTimeGroupDataItem{
                                type="No. of Clients turning 55yrs within next month",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            }
                        }
                    },
                     new ReminderTimeGroupmodel{ 
                        group="Personal Reminders",
                        data=new List<ReminderTimeGroupDataItem>{
                            new ReminderTimeGroupDataItem{
                                type="No. of Clients having Birthdays today",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            },new ReminderTimeGroupDataItem{
                                type="No. of Clients turning 55yrs within next month",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            }
                        }
                    },
                     new ReminderTimeGroupmodel{ 
                        group="Administration Reminders",
                        data=new List<ReminderTimeGroupDataItem>{
                            new ReminderTimeGroupDataItem{
                                type="No. of Clients having Birthdays today",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            },new ReminderTimeGroupDataItem{
                                type="No. of Clients turning 55yrs within next month",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            }
                        }
                    },
                     new ReminderTimeGroupmodel{ 
                        group="Portfolio Reminders",
                        data=new List<ReminderTimeGroupDataItem>{
                            new ReminderTimeGroupDataItem{
                                type="No. of Clients having Birthdays today",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            },new ReminderTimeGroupDataItem{
                                type="No. of Clients turning 55yrs within next month",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            }
                        }
                    }
                },
                Weekly = new List<ReminderTimeGroupmodel>
                {
                    new ReminderTimeGroupmodel{ 
                        group="All Reminders",
                        data=new List<ReminderTimeGroupDataItem>{
                            new ReminderTimeGroupDataItem{
                                type="No. of Clients having Birthdays today",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            },new ReminderTimeGroupDataItem{
                                type="No. of Clients turning 55yrs within next month",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            }
                        }
                    },
                     new ReminderTimeGroupmodel{ 
                        group="Personal Reminders",
                        data=new List<ReminderTimeGroupDataItem>{
                            new ReminderTimeGroupDataItem{
                                type="No. of Clients having Birthdays today",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            },new ReminderTimeGroupDataItem{
                                type="No. of Clients turning 55yrs within next month",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            }
                        }
                    },
                     new ReminderTimeGroupmodel{ 
                        group="Administration Reminders",
                        data=new List<ReminderTimeGroupDataItem>{
                            new ReminderTimeGroupDataItem{
                                type="No. of Clients having Birthdays today",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            },new ReminderTimeGroupDataItem{
                                type="No. of Clients turning 55yrs within next month",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            }
                        }
                    },
                     new ReminderTimeGroupmodel{ 
                        group="Portfolio Reminders",
                        data=new List<ReminderTimeGroupDataItem>{
                            new ReminderTimeGroupDataItem{
                                type="No. of Clients having Birthdays today",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            },new ReminderTimeGroupDataItem{
                                type="No. of Clients turning 55yrs within next month",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            }
                        }
                    }
                },
                Yearly = new List<ReminderTimeGroupmodel>
                {
                    new ReminderTimeGroupmodel{ 
                        group="All Reminders",
                        data=new List<ReminderTimeGroupDataItem>{
                            new ReminderTimeGroupDataItem{
                                type="No. of Clients having Birthdays today",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            },new ReminderTimeGroupDataItem{
                                type="No. of Clients turning 55yrs within next month",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            }
                        }
                    },
                     new ReminderTimeGroupmodel{ 
                        group="Personal Reminders",
                        data=new List<ReminderTimeGroupDataItem>{
                            new ReminderTimeGroupDataItem{
                                type="No. of Clients having Birthdays today",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            },new ReminderTimeGroupDataItem{
                                type="No. of Clients turning 55yrs within next month",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            }
                        }
                    },
                     new ReminderTimeGroupmodel{ 
                        group="Administration Reminders",
                        data=new List<ReminderTimeGroupDataItem>{
                            new ReminderTimeGroupDataItem{
                                type="No. of Clients having Birthdays today",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            },new ReminderTimeGroupDataItem{
                                type="No. of Clients turning 55yrs within next month",
                                number=5,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            }
                        }
                    },
                     new ReminderTimeGroupmodel{ 
                        group="Portfolio Reminders",
                        data=new List<ReminderTimeGroupDataItem>{
                            new ReminderTimeGroupDataItem{
                                type="No. of Clients having Birthdays today",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            },new ReminderTimeGroupDataItem{
                                type="No. of Clients turning 55yrs within next month",
                                number=rdm.Next() * 100,
                                contents=new List<ReminderTimeGroupDataContent>{
                                    new ReminderTimeGroupDataContent{accountName="Mr. X and Mrs. X", accountNumber="20130001"},
                                    new ReminderTimeGroupDataContent{accountName="Mr. Y and Mrs. Y", accountNumber="20130002"},
                                }
                            }
                        }
                    }
                }
            };
        }
        public List<AnalysisCityBrief> GetAnalysisCompaniesList(string clientUserId)
        {
            return new List<AnalysisCityBrief>
            {
                new AnalysisCityBrief{ id="RS01", name="ANZ"},
                new AnalysisCityBrief{id="RS02",name="Commonwealth Bank"},
            };
        }
        public CompanyProfileDataItem GetCompanyProfile(string clientUserId, string profileId)
        {
            return new CompanyProfileDataItem
            {
                id = profileId,
                fullName = "Australia & New Zealand Banking Limited",
                ticker = "ANZ",
                country = "Australia",
                exchange = "Australian Stock Exchange",
                assetClass = "Australian Equity",
                sector = "Financial Services",
                closingPrice = randomMoney(),
                changeAmount = randomMoney(),
                changeRatePercentage = randomMoney(),
                weeksDifferenceAmount = randomMoney(),
                weeksDifferenceRatePercentage = randomMoney(),
                marketCapitalisation = "Capitalisation",
                priceDate = DateTime.Now,
                currencyType = "AUD",
                suitabilityScore = randomPercentage(),
                suitsTypeOfClients = "Client types that are suitable for this company",
                reasons = "Some reasons",
                companyBriefing = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                "Aliquam euismod dolor massa, ac laoreet leo tristique eu. Vestibulum eu iaculis erat, non consectetur lectus." +
                "Proin placerat odio id ullamcorper malesuada. Nullam nec sapien a metus ornare porttitor. Ut id quam consectetur, " +
                "sollicitudin dui at, pellentesque purus. Quisque interdum vel orci pretium finibus. Mauris viverra condimentum sapien. " +
                "Fusce ac fringilla turpis, sit amet laoreet lectus. Nullam facilisis purus rhoncus, facilisis nulla et, ultrices purus." +
                "Curabitur ornare libero sed tincidunt luctus. Cras faucibus faucibus lacus, vel malesuada enim efficitur non. Nullam sed massa erat." +
                "Aenean eget sollicitudin nunc. Donec tincidunt justo vel ante gravida aliquam. Donec nulla nulla, mattis ac convallis vitae," +
                "vulputate eget ex.Vestibulum sit amet lectus ut velit convallis porttitor. Suspendisse auctor eleifend pretium." +
                "Donec non ultrices nunc. Nunc in feugiat urna, sed interdum sem. Integer fringilla ante est, id maximus velit feugiat et." +
                "Sed est arcu, egestas et cursus lacinia, tempus sit amet ipsum. Cras ullamcorper aliquet tempor. Etiam suscipit, est a rutrum aliquet," +
                "sapien diam dignissim erat, in ultrices nisl ipsum id tortor. Ut quis convallis ligula. Aenean sit amet metus sit amet est sodales aliquet" +
                "ac porttitor turpis. Sed ut euismod arcu. In pharetra vel velit at molestie. Suspendisse molestie luctus consequat. Ut non tellus sed ex efficitur" +
                "suscipit ut eu diam. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas." +
                "Mauris id egestas velit, id malesuada ex. Duis commodo lectus a ante laoreet venenatis. Morbi congue sapien vel vulputate maximus." +
                "Etiam sit amet tortor sit amet ligula tincidunt bibendum eget nec metus. In eget neque eget diam facilisis lacinia. Quisque ut mauris nec" +
                "justo sagittis gravida. Proin dignissim vel lorem at lobortis. Aliquam semper lacus nibh, vel bibendum ex condimentum non. Aliquam ex nulla, sagittis eget enim in, auctor facilisis eros.",
                companyStrategies = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." +
                "Aliquam euismod dolor massa, ac laoreet leo tristique eu. Vestibulum eu iaculis erat, non consectetur lectus." +
                "Proin placerat odio id ullamcorper malesuada. Nullam nec sapien a metus ornare porttitor. Ut id quam consectetur, " +
                "sollicitudin dui at, pellentesque purus. Quisque interdum vel orci pretium finibus. Mauris viverra condimentum sapien. " +
                "Fusce ac fringilla turpis, sit amet laoreet lectus. Nullam facilisis purus rhoncus, facilisis nulla et, ultrices purus." +
                "Curabitur ornare libero sed tincidunt luctus. Cras faucibus faucibus lacus, vel malesuada enim efficitur non. Nullam sed massa erat." +
                "Aenean eget sollicitudin nunc. Donec tincidunt justo vel ante gravida aliquam. Donec nulla nulla, mattis ac convallis vitae," +
                "vulputate eget ex.Vestibulum sit amet lectus ut velit convallis porttitor. Suspendisse auctor eleifend pretium." +
                "Donec non ultrices nunc. Nunc in feugiat urna, sed interdum sem. Integer fringilla ante est, id maximus velit feugiat et." +
                "Sed est arcu, egestas et cursus lacinia, tempus sit amet ipsum. Cras ullamcorper aliquet tempor. Etiam suscipit, est a rutrum aliquet," +
                "sapien diam dignissim erat, in ultrices nisl ipsum id tortor. Ut quis convallis ligula. Aenean sit amet metus sit amet est sodales aliquet" +
                "ac porttitor turpis. Sed ut euismod arcu. In pharetra vel velit at molestie. Suspendisse molestie luctus consequat. Ut non tellus sed ex efficitur" +
                "suscipit ut eu diam. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas." +
                "Mauris id egestas velit, id malesuada ex. Duis commodo lectus a ante laoreet venenatis. Morbi congue sapien vel vulputate maximus." +
                "Etiam sit amet tortor sit amet ligula tincidunt bibendum eget nec metus. In eget neque eget diam facilisis lacinia. Quisque ut mauris nec" +
                "justo sagittis gravida. Proin dignissim vel lorem at lobortis. Aliquam semper lacus nibh, vel bibendum ex condimentum non. Aliquam ex nulla, sagittis eget enim in, auctor facilisis eros.",
                investment = "Investment",
                investmentName = "investment name",
                indexData = new List<CompanyProfileIndexData>{ 
                    new CompanyProfileIndexData{date=DateTime.Now.AddMonths(-12),company=randomMoney(),portfolio=randomMoney(),asx200=randomMoney(),month=DateTime.Now.AddMonths(-12).ToString("MMM") },
                    new CompanyProfileIndexData{date=DateTime.Now.AddMonths(-11),company=randomMoney(),portfolio=randomMoney(),asx200=randomMoney(),month=DateTime.Now.AddMonths(-11).ToString("MMM") },
                    new CompanyProfileIndexData{date=DateTime.Now.AddMonths(-10),company=randomMoney(),portfolio=randomMoney(),asx200=randomMoney(),month=DateTime.Now.AddMonths(-10).ToString("MMM") },
                    new CompanyProfileIndexData{date=DateTime.Now.AddMonths(-9),company=randomMoney(),portfolio=randomMoney(),asx200=randomMoney(),month=DateTime.Now.AddMonths(-9).ToString("MMM") },
                    new CompanyProfileIndexData{date=DateTime.Now.AddMonths(-8),company=randomMoney(),portfolio=randomMoney(),asx200=randomMoney(),month=DateTime.Now.AddMonths(-8).ToString("MMM") },
                    new CompanyProfileIndexData{date=DateTime.Now.AddMonths(-7),company=randomMoney(),portfolio=randomMoney(),asx200=randomMoney(),month=DateTime.Now.AddMonths(-7).ToString("MMM") },
                    new CompanyProfileIndexData{date=DateTime.Now.AddMonths(-6),company=randomMoney(),portfolio=randomMoney(),asx200=randomMoney(),month=DateTime.Now.AddMonths(-6).ToString("MMM") },
                    new CompanyProfileIndexData{date=DateTime.Now.AddMonths(-5),company=randomMoney(),portfolio=randomMoney(),asx200=randomMoney(),month=DateTime.Now.AddMonths(-5).ToString("MMM") },
                    new CompanyProfileIndexData{date=DateTime.Now.AddMonths(-4),company=randomMoney(),portfolio=randomMoney(),asx200=randomMoney(),month=DateTime.Now.AddMonths(-4).ToString("MMM") },
                    new CompanyProfileIndexData{date=DateTime.Now.AddMonths(-3),company=randomMoney(),portfolio=randomMoney(),asx200=randomMoney(),month=DateTime.Now.AddMonths(-3).ToString("MMM") },
                    new CompanyProfileIndexData{date=DateTime.Now.AddMonths(-2),company=randomMoney(),portfolio=randomMoney(),asx200=randomMoney(),month=DateTime.Now.AddMonths(-2).ToString("MMM") },
                    new CompanyProfileIndexData{date=DateTime.Now.AddMonths(-1),company=randomMoney(),portfolio=randomMoney(),asx200=randomMoney(),month=DateTime.Now.AddMonths(-1).ToString("MMM") },
                    new CompanyProfileIndexData{date=DateTime.Now,company=randomMoney(),portfolio=randomMoney(),asx200=randomMoney(),month=DateTime.Now.ToString("MMM") }
                },
                currentAnalysis = new CurrentAnalysisPayload
                {
                    metaProperties = new List<AnalysisPayloadMetaProperty>
                    {
                        new AnalysisPayloadMetaProperty{propertyName="baseInformation",displayName="Base Information"},
                        new AnalysisPayloadMetaProperty{propertyName="morningstar",displayName="Morningstar"},
                        new AnalysisPayloadMetaProperty{propertyName="brokerX",displayName="Broker X"},
                        new AnalysisPayloadMetaProperty{propertyName="ASX200Accumulation",displayName="ASX 200 Accumulation"},
                    },
                    groups = new List<AnalysisPayloadGroupModel>
                    {
                        new AnalysisPayloadGroupModel{
                            name="Recommendation",
                            data=new List<AnalysisPayloadGroupDataItem>{
                                new AnalysisPayloadGroupDataItem{
                                    name= "Current Short Term Recommendation",
                                    baseInformation= "base information",
                                    morningstar= "Morning star information",
                                    brokerX= "brokerX information",
                                    ASX200Accumulation= "Accumulation Details"
                                },new AnalysisPayloadGroupDataItem{
                                    name= "Current Long Term Recommendation",
                                    baseInformation= "base information",
                                    morningstar= "Morning star information",
                                    brokerX= "brokerX information",
                                    ASX200Accumulation= "Accumulation Details"
                                },new AnalysisPayloadGroupDataItem{
                                    name= "Price Target",
                                    baseInformation= "base information",
                                    morningstar= "Morning star information",
                                    brokerX= "brokerX information",
                                    ASX200Accumulation= "Accumulation Details"
                                },

                            }
                        },new AnalysisPayloadGroupModel{
                            name="Income",
                            data=new List<AnalysisPayloadGroupDataItem>{
                                new AnalysisPayloadGroupDataItem{
                                    name= "Current Short Term Recommendation",
                                    baseInformation= "base information",
                                    morningstar= "Morning star information",
                                    brokerX= "brokerX information",
                                    ASX200Accumulation= "Accumulation Details"
                                },new AnalysisPayloadGroupDataItem{
                                    name= "Current Long Term Recommendation",
                                    baseInformation= "base information",
                                    morningstar= "Morning star information",
                                    brokerX= "brokerX information",
                                    ASX200Accumulation= "Accumulation Details"
                                },new AnalysisPayloadGroupDataItem{
                                    name= "Price Target",
                                    baseInformation= "base information",
                                    morningstar= "Morning star information",
                                    brokerX= "brokerX information",
                                    ASX200Accumulation= "Accumulation Details"
                                },
                            }
                        }
                    }
                },
                forecast = new ForecastAnalysisPayload
                {
                    metaProperties = new List<AnalysisPayloadMetaProperty>
                    {
                        new AnalysisPayloadMetaProperty{propertyName="baseInformation",displayName="Base Information"},
                        new AnalysisPayloadMetaProperty{propertyName="morningstar",displayName="Morningstar"},
                        new AnalysisPayloadMetaProperty{propertyName="brokerX",displayName="Broker X"},
                        new AnalysisPayloadMetaProperty{propertyName="ASX200Accumulation",displayName="ASX 200 Accumulation"},
                    },
                    oneYearForecastGroups = new List<AnalysisPayloadGroupModel>
                    {
                        new AnalysisPayloadGroupModel{
                            name="Forecast Year 1 Income",
                            data=new List<AnalysisPayloadGroupDataItem>{
                                new AnalysisPayloadGroupDataItem{
                                    name="Dividend Per Share",
                                    baseInformation="base information",
                                    morningstar="Morning star information",
                                    brokerX="brokerX information",
                                    ASX200Accumulation="Accumulation Details"
                                },new AnalysisPayloadGroupDataItem{
                                    name="Franking",
                                    baseInformation="base information",
                                    morningstar="Morning star information",
                                    brokerX="brokerX information",
                                    ASX200Accumulation="Accumulation Details"
                                },new AnalysisPayloadGroupDataItem{
                                    name="Pay out Ratio",
                                    baseInformation="base information",
                                    morningstar="Morning star information",
                                    brokerX="brokerX information",
                                    ASX200Accumulation="Accumulation Details"
                                },
                            }
                        },
                        new AnalysisPayloadGroupModel{
                            name="Forecast Year 1 Risk",
                            data=new List<AnalysisPayloadGroupDataItem>{
                                new AnalysisPayloadGroupDataItem{
                                    name="Dividend Per Share",
                                    baseInformation="base information",
                                    morningstar="Morning star information",
                                    brokerX="brokerX information",
                                    ASX200Accumulation="Accumulation Details"
                                },new AnalysisPayloadGroupDataItem{
                                    name="Franking",
                                    baseInformation="base information",
                                    morningstar="Morning star information",
                                    brokerX="brokerX information",
                                    ASX200Accumulation="Accumulation Details"
                                },new AnalysisPayloadGroupDataItem{
                                    name="Pay out Ratio",
                                    baseInformation="base information",
                                    morningstar="Morning star information",
                                    brokerX="brokerX information",
                                    ASX200Accumulation="Accumulation Details"
                                },
                            }
                        }
                    },
                    twoYearForecastGroups = new List<AnalysisPayloadGroupModel>
                    {
                        new AnalysisPayloadGroupModel{
                            name="Forecast Year 2 Income",
                            data=new List<AnalysisPayloadGroupDataItem>{
                                new AnalysisPayloadGroupDataItem{
                                    name="Dividend Per Share",
                                    baseInformation="base information",
                                    morningstar="Morning star information",
                                    brokerX="brokerX information",
                                    ASX200Accumulation="Accumulation Details"
                                },new AnalysisPayloadGroupDataItem{
                                    name="Franking",
                                    baseInformation="base information",
                                    morningstar="Morning star information",
                                    brokerX="brokerX information",
                                    ASX200Accumulation="Accumulation Details"
                                },new AnalysisPayloadGroupDataItem{
                                    name="Pay out Ratio",
                                    baseInformation="base information",
                                    morningstar="Morning star information",
                                    brokerX="brokerX information",
                                    ASX200Accumulation="Accumulation Details"
                                },
                            }
                        },
                        new AnalysisPayloadGroupModel{
                            name="Forecast Year 2 Risk",
                            data=new List<AnalysisPayloadGroupDataItem>{
                                new AnalysisPayloadGroupDataItem{
                                    name="Beta",
                                    baseInformation="base information",
                                    morningstar="Morning star information",
                                    brokerX="brokerX information",
                                    ASX200Accumulation="Accumulation Details"
                                },new AnalysisPayloadGroupDataItem{
                                    name="Current Ratio",
                                    baseInformation="base information",
                                    morningstar="Morning star information",
                                    brokerX="brokerX information",
                                    ASX200Accumulation="Accumulation Details"
                                },new AnalysisPayloadGroupDataItem{
                                    name="Quick Ratio",
                                    baseInformation="base information",
                                    morningstar="Morning star information",
                                    brokerX="brokerX information",
                                    ASX200Accumulation="Accumulation Details"
                                },
                            }
                        }
                    }
                },
                comparisonDetails = new ComparisonDetailsModel
                {
                    suitability = new ComparisonDetailsSuitability
                    {
                        annualReturn = "annual return",
                        oneYearBeta = "one year beta",
                        companyGearing = "company gearing",
                        debtEquityRatio = "debt/equity ratio",
                        dividendYield = "dividend Yield",
                        earningsPerShareGrowth = "earnings",
                        interestCover = "cover",
                        marketCapitalisation = "capitalisation",
                        priceEarningsRatio = "earnings ratio",
                        totalStandardRating = randomPercentage()
                    },
                    recommendationSuitability = new ComparisonDetailsRecommendationSuitability
                    {
                        recommendationOnInvestmentAsset = "asset",
                        forecastDividendYield = "dividend",
                        forecastDividendFranking = "franking",
                        forecastEarningsPerShareGrowth = "growth",
                        forecastOneYearBeta = "one year beta",
                        forecastCompanyGearing = "gearing",
                        forecastPriceEarningsRatio = "earnings ratio",
                        forecastPriceEarningsRatioGrowth = "ratio growth",
                        forecastInterestCover = "cover",
                        forecastDebtEquityRatio = "debt/equity ratio",
                        forecastReturnOnEquity = "return on equity",
                        forecastReturnOnAsset = "return on asset",
                        forecastAssetAllocation = "asset allocation",
                        totalRecommendationSuitabilityScore = randomPercentage()
                    },
                    investmentAnalysis = new ComparisonDetailsInvestmentAnalysis
                    {
                        generalInformation = new ComparisonDetailsInvestmentGeneralInfo
                        {
                            currentPrice = new ComparisonDetailsInvestmentAnalysisItem
                            {
                                baseInformation = "Base Information",
                                clientObjective = "Client Objective",
                                clientSuitability = "Client Suitability"
                            },
                            priceChangeAmount = new ComparisonDetailsInvestmentAnalysisItem
                            {
                                baseInformation = "Base Information",
                                clientSuitability = "Client  Objective",
                                clientObjective = "Client Suitability"
                            },
                        },
                        recommendation = new ComparisonDetailsInvestmentRecommendation
                        {
                            currentLongTermRecommendation = new ComparisonDetailsInvestmentAnalysisItem
                            {
                                baseInformation = "Base Information",
                                clientObjective = "Client Objective",
                                clientSuitability = "Client Suitability"
                            },
                            currentShortTermRecommendation = new ComparisonDetailsInvestmentAnalysisItem
                            {
                                baseInformation = "Base Information",
                                clientSuitability = "Client Suitability",
                                clientObjective = "Client Objective"
                            }
                        }
                    }
                }
            };
        }
        #endregion
        #region services added on 26/05/2015
        public List<CorporateActionClientAccountModel> GetAllClientAccounts(string clientUserId)
        {
            return new List<CorporateActionClientAccountModel>
            {
                new CorporateActionClientAccountModel{
                    edisAccountNumber= "0008",
                    brokerAccountNumber= "0008",
                    brokerHinSrn= "X1111118",
                    type= "Autolink",
                    name="Peter Truong 008",
                },new CorporateActionClientAccountModel{
                    edisAccountNumber= "0007",
                    brokerAccountNumber= "0007",
                    brokerHinSrn= "X1111117",
                    type= "Autolink",
                    name="Peter Truong 007",
                },new CorporateActionClientAccountModel{
                    edisAccountNumber= "0006",
                    brokerAccountNumber= "0006",
                    brokerHinSrn= "X1111116",
                    type= "Autolink",
                    name="Peter Truong 006",
                },new CorporateActionClientAccountModel{
                    edisAccountNumber= "0005",
                    brokerAccountNumber= "0005",
                    brokerHinSrn= "X1111115",
                    type= "Autolink",
                    name="Peter Truong 005",
                },
            };
        }
        #endregion

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
        #region added services on 04/05/2015

        public AdviserView GetAdviserAccountNumberForClient(string clientUserId)
        {
            var client = db.Clients.FirstOrDefault(c => c.ClientId == clientUserId);
            var clientGroup = db.ClientGroups.FirstOrDefault(g => g.ClientGroupId == client.ClientGroupId);
            var adviser = db.Advisers.FirstOrDefault(a => a.AdviserNumber == clientGroup.Adviser_AdviserId);
            return new AdviserView
            {
                //accountNumber = clientGroup.Adviser.,
                //name = adviser.FirstName + " " + adviser.LastName
            };
        }

        #endregion

        public void Save()
        {
            db.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}