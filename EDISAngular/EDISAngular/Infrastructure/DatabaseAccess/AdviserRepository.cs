using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EDISAngular.Abstract;
using EDISAngular;
using System.Threading.Tasks;
using EDISAngular.Models.BindingModels;
using EDISAngular.Services;
using EDISAngular.Models.ServiceModels;
using EDISAngular.Models.ServiceModels.AdviserProfile;
using EDISAngular.Models.ViewModels;
using EDISAngular.Models.ServiceModels.CorporateActions;
using EDISAngular.Infrastructure.DbFirst;

namespace EDISAngular.Infrastructure.DatabaseAccess
{
    public class AdviserRepository : BaseRepository
    {

        private CommonReferenceDataRepository comRepo;
        private Random rdm = new Random();

        #region completed implementation
        public AdviserRepository(edisDbEntities database)
            :base(database)
        {
        }
        public AdviserRepository():base()
        {

        }
        //public void InsertOrUpdateAdviserProfile_Complete(AdviserRegistrationBindingModel model)
        //{
        //    if (model == null || string.IsNullOrEmpty(model.adviserUserId) || !db.AspNetUsers.Where(u => u.Id == model.adviserUserId).Any())
        //    {
        //        throw new Exception("Model is invalid");
        //    }
        //    if (AdviserExist(model.adviserUserId))
        //    {
        //        UpdateAdviserProfile_Complete(model);
        //    }
        //    else
        //    {
        //        AddAdviserProfile_Complete(model);
        //    }
        //}
        //public AdviserRegistrationBindingModel GetAdviserProfile_Complete(string adviserUserId)
        //{
        //    AdviserRegistrationBindingModel model = new AdviserRegistrationBindingModel();
        //    model.adviserUserId = adviserUserId;
        //    CommonReferenceDataRepository comRepo = new CommonReferenceDataRepository(db);

        //    #region model details
        //    var details = db.Advisers.FirstOrDefault(ad => ad.AdviserId == adviserUserId);
        //    if (details != null)
        //    {
        //        model.ABN = details.ABNACN;
        //        model.addressLine1 = details.AddressLn1;
        //        model.addressLine2 = details.AddressLn2;
        //        model.addressLine3 = details.AddressLn3;
        //        model.adviserUserId = adviserUserId;
        //        model.businessFax = details.Fax;
        //        model.businessMobile = details.Mobile;
        //        model.businessPhone = details.Phone;
        //        model.companyName = details.CompanyName;
        //        model.country = details.Country;
        //        model.currentPositionTitle = details.CurrentTitle;
        //        model.firstName = details.FirstName;
        //        model.gender = details.Gender;
        //        model.industryExperienceStartDate = details.ExperienceStartDate;
        //        model.lastName = details.LastName;
        //        model.middleName = details.MiddleName;
        //        model.postCode = details.PostCode;
        //        model.state = details.State;
        //        model.suburb = details.Suburb;
        //        model.title = details.Title;
        //    }

        //    #endregion
        //    #region model dealer group details
        //    var dealerGroup = db.Adviser_DealerGroupDetails.FirstOrDefault(d => d.UserId == adviserUserId);
        //    if (dealerGroup != null)
        //    {
        //        model.dealerGroup_addressLine1 = dealerGroup.AddressLn1;
        //        model.dealerGroup_addressLine2 = dealerGroup.AddressLn2;
        //        model.dealerGroup_addressLine3 = dealerGroup.AddressLn3;
        //        model.dealerGroup_country = dealerGroup.Country;
        //        model.dealerGroup_postCode = dealerGroup.PostCode;
        //        model.dealerGroup_state = dealerGroup.State;
        //        model.dealerGroup_suburb = dealerGroup.Suburb;
        //        model.dealerGroupHasDerivativesLicense = dealerGroup.HasDerivativesLicense.HasValue && dealerGroup.HasDerivativesLicense.Value == 1;
        //        model.dealerGroupName = dealerGroup.DealerGroupName;
        //        model.asfl = dealerGroup.AFSL;
        //        model.authorizedRepresentativeNumber = dealerGroup.AuthorisedRepNumber;
        //        model.isAuthorizedRepresentative = dealerGroup.IsAuthorisedRep.HasValue && dealerGroup.IsAuthorisedRep.Value == 1;
        //    }


        //    #endregion
        //    #region model management details
        //    var management = db.Adviser_ManagementDetails.FirstOrDefault(m => m.UserId == adviserUserId);
        //    if (management != null)
        //    {
        //        model.totalInvestmentUndermanagement = management.TotalManagedInvestments.ToString();
        //        model.totalDirectAustralianEquitiesUnderManagement = management.TotalDirectAustralianEquities.ToString();
        //        model.totalDirectInterantionalEquitiesUnderManagement = management.TotalDirectInternational.ToString();
        //        model.totalDirectFixedInterestUnderManagement = management.TotalFixedInterest.ToString();
        //        model.totalDirectLendingBookInterestUnderManagement = management.TotalLendingBook.ToString();
        //        model.totalAssetUnderManagement = management.TotalAssets.ToString();
        //        model.numberOfClientsId = management.ApproxClientNumbersId.Value;
        //    }

        //    #endregion
        //    #region model education
        //    model.educations = new List<EducationCreationModel>();
        //    foreach (var item in db.Adviser_Education.Where(ed => ed.UserId == adviserUserId))
        //    {
        //        var educationLevel = comRepo.GetAllEducationLevels().FirstOrDefault(ed => ed.EducationLevels == item.EducationLevels);
        //        model.educations.Add(new EducationCreationModel
        //        {
        //            courseStatus = item.CurrentlyStudying.HasValue && item.CurrentlyStudying.Value == 1,
        //            courseTitle = item.CourseName,
        //            educationLevelId = educationLevel != null ? educationLevel.EducationLevelsId : 0,
        //            institution = item.Institution

        //        });
        //    }

        //    #endregion
        //    #region model professsional type
        //    var professionalType = db.Adviser_ProfessionalType.FirstOrDefault(p => p.UserId == adviserUserId);
        //    if (professionalType != null)
        //    {
        //        model.professiontypeId = professionalType.ProfessionTypeId.ToString();
        //    }

        //    #endregion
        //    #region model target clients
        //    var targetClient = db.Adviser_TargetClients.FirstOrDefault(t => t.UserId == adviserUserId);
        //    if (targetClient != null)
        //    {
        //        model.annualIncomeLevelId = targetClient.AnnualIncomeLevelsId.Value;
        //        model.investibleAssetLevel = targetClient.InvestibleAssetsLevelId.Value;
        //        model.totalAssetLevelId = targetClient.TotalAssetsLevelId.Value;
        //    }

        //    #endregion
        //    #region newsletter details
        //    model.newsLetterServices = new List<NewsletterServiceModel>();

        //    comRepo.GetNewsletterService().ToList().ForEach(service =>
        //     {
        //         NewsletterServiceModel item = new NewsletterServiceModel();
        //         item.newsLetterServiceId = service.NewsletterServicesId;
        //         item.selected = db.User_NewsletterServices.Where(ns => ns.NewsletterServicesId == service.NewsletterServicesId
        //             && ns.UserId == adviserUserId && ns.IsSubscribed == "True").Any();
        //         item.serviceName = service.NewsletterServices;
        //         model.newsLetterServices.Add(item);
        //     });
        //    #endregion
        //    #region commissions and fees
        //    model.commissionsAndFees = new List<CommisionsAndFeesModel>();
        //    comRepo.GetCommissionAndFees_Filtered(n => n.CommissionsAndFeesId != BusinessLayerParameters.feesAndCommissions_RemunerationType).ForEach(item =>
        //    {
        //        model.commissionsAndFees.Add(new CommisionsAndFeesModel()
        //        {
        //            description = item.CommissionsAndFees,
        //            id = item.CommissionsAndFeesId,
        //            selected = db.Adviser_CommissionsAndFees
        //            .Where(cm => cm.CommissionsAndFeesId == item.CommissionsAndFeesId && cm.UserId == adviserUserId && cm.YesNoNA == 1).Any()
        //        });
        //    });

        //    var remunerationTypeFC = db.Adviser_CommissionsAndFees
        //        .FirstOrDefault(f => f.CommissionsAndFeesId == BusinessLayerParameters
        //            .feesAndCommissions_RemunerationType && f.UserId == adviserUserId);
        //    if (remunerationTypeFC != null)
        //    {
        //        model.remunerationMethodSpecified = remunerationTypeFC.YesNoNA.HasValue && remunerationTypeFC.YesNoNA.Value == 1;
        //        model.remunerationMethod = remunerationTypeFC.CommissionDescription;
        //    }


        //    #endregion
        //    #region services provided
        //    List<ServiceGroupModel> groups = new List<ServiceGroupModel>();
        //    foreach (var group in db.Services)
        //    {
        //        ServiceGroupModel item = new ServiceGroupModel();
        //        item.groupName = group.ServiceName;
        //        item.services = new List<ServiceList>();
        //        foreach (var service in db.SubServices.Where(s => s.ServiceId == group.ServiceId))
        //        {
        //            var sitem = new ServiceList();
        //            sitem.serviceId = service.SubServiceId;
        //            sitem.serviceName = service.SubServiceName;
        //            sitem.providing = db.Adviser_ServicesProviding.Where(s => s.SubServiceId == service.SubServiceId
        //                && s.UserId == adviserUserId && s.SubServiceProvideYesNo == "Yes").Any();
        //            item.services.Add(sitem);
        //        }
        //        groups.Add(item);
        //    }
        //    model.services = groups;
        //    #endregion


        //    return model;
        //}

        //public List<ClientView> GetAdvisorClients(string advisorUserId)
        //{
        //    var adviser = db.Adviser_Details.SingleOrDefault(a => a.AdvisorUserId == advisorUserId);
        //    var groups = db.ClientGroups.Where(g => g.AdviserNumber == adviser.AccountNumber);
        //    List<ClientView> clients = new List<ClientView>();
        //    foreach (var group in groups)
        //    {

        //        clients.AddRange(db.Client_Details.Where(g => g.ClientGroupId == group.ClientGroupID)
        //            .Select(c => new ClientView()
        //            {
        //                id = db.Clients.FirstOrDefault(s => s.ClientUserID == c.UserId).ClientUserID,
        //                name = c.ClientType == BusinessLayerParameters.clientType_entity ? c.EntityName : c.FirstName + " " + c.LastName
        //            }).ToList());
        //    }
        //    return clients;
        //}
        //public List<ClientView> GetClientGroupsByAdviserId(string adviserUserId)
        //{
        //    var adviser = db.Adviser_Details.FirstOrDefault(a => a.AdvisorUserId == adviserUserId);
        //    var groups = db.ClientGroups.Where(g => g.AdviserNumber == adviser.AccountNumber);
        //    List<ClientView> clients = new List<ClientView>();
        //    foreach (var group in groups)
        //    {

        //        clients.Add(new ClientView
        //        {
        //            id = group.ClientGroupID,
        //            name = group.AccountName
        //        });
        //    }
        //    return clients;
        //}
        //public Adviser_Details GetAdviserDetailsByNumber(string adviserNumber)
        //{
        //    return db.Adviser_Details.FirstOrDefault(ad => ad.AccountNumber.ToString() == adviserNumber);
        //}
        //#endregion


        //#region service methods added 12/05/2015
        //public List<CorporateActionClientAccountModel> GetAllClientAccounts(string adviserUserId)
        //{
        //    return new List<CorporateActionClientAccountModel>
        //    {
        //        new CorporateActionClientAccountModel{
        //            edisAccountNumber= "0008",
        //            brokerAccountNumber= "0008",
        //            brokerHinSrn= "X1111118",
        //            type= "Autolink",
        //            name="Peter Truong 008",
        //        },new CorporateActionClientAccountModel{
        //            edisAccountNumber= "0007",
        //            brokerAccountNumber= "0007",
        //            brokerHinSrn= "X1111117",
        //            type= "Autolink",
        //            name="Peter Truong 007",
        //        },new CorporateActionClientAccountModel{
        //            edisAccountNumber= "0006",
        //            brokerAccountNumber= "0006",
        //            brokerHinSrn= "X1111116",
        //            type= "Autolink",
        //            name="Peter Truong 006",
        //        },new CorporateActionClientAccountModel{
        //            edisAccountNumber= "0005",
        //            brokerAccountNumber= "0005",
        //            brokerHinSrn= "X1111115",
        //            type= "Autolink",
        //            name="Peter Truong 005",
        //        },
        //    };
        //}
        //public List<CorporateActionClientAccountModel> GetClientAccountsForCompany(string adviserUserId, string companyTicker)
        //{
        //    return new List<CorporateActionClientAccountModel>
        //    {
        //        new CorporateActionClientAccountModel{
        //            edisAccountNumber= "0008",
        //            brokerAccountNumber= "0008",
        //            brokerHinSrn= "X1111118",
        //            type= "Autolink",
        //            name="Peter Truong 008",
        //        },new CorporateActionClientAccountModel{
        //            edisAccountNumber= "0007",
        //            brokerAccountNumber= "0007",
        //            brokerHinSrn= "X1111117",
        //            type= "Autolink",
        //            name="Peter Truong 007",
        //        },new CorporateActionClientAccountModel{
        //            edisAccountNumber= "0006",
        //            brokerAccountNumber= "0006",
        //            brokerHinSrn= "X1111116",
        //            type= "Autolink",
        //            name="Peter Truong 006",
        //        },new CorporateActionClientAccountModel{
        //            edisAccountNumber= "0005",
        //            brokerAccountNumber= "0005",
        //            brokerHinSrn= "X1111115",
        //            type= "Autolink",
        //            name="Peter Truong 005",
        //        },
        //    };
        //}
        //#endregion


        #region adviser overview services
        public BusinessPortfolioOverviewBriefModel GetBusinessRevenueData(string adviserUserId)
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
        public BusinessPortfolioOverviewBriefModel GetDebtInstrumentsData(string adviserUserId)
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
        public ProfileInsuranceStatisticsModel GetInsuranceStatisticsData(string adviserUserId)
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
        public List<WordMarketItemModel> GetWorldMarketData(string adviserUserId)
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
        public List<WordMarketItemModel> GetCurrencies(string adviserUserId)
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
        public HistoricalRevenueModel GetHistoricalRevenueData(string adviseruserId)
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
        public BusinessStatDetailModel GetInvestmentStat(string adviserUserId)
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
        public BusinessStatDetailModel GetLendingStat(string adviserUserId)
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
        public InsuranceStatModel GetInsuranceStatDetailed(string adviserUserId)
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
        public List<ClientPositionMonitorModel> GetClientPositionMonitor(string adviserUserId)
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
        public List<GeoGraphicalLocations> GetGeoLocations(string adviserUserId)
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
        public GeoStatsModel GetGeoStats(string adviserUserId, string[] geoIds)
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
        public BuisnessRevenueDetailsDataModel GetBusinessRevenueDetails(string adviserUserId)
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
        public CompliantModel GetComplianceDetails(string adviserUserId)
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
        public ReminderModel GetReminders(string adviserUserId)
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

        public List<AnalysisCityBrief> GetAnalysisCompaniesList(string adviserUserId)
        {
            return new List<AnalysisCityBrief>
            {
                new AnalysisCityBrief{ id="RS01", name="ANZ"},
                new AnalysisCityBrief{id="RS02",name="Commonwealth Bank"},
            };
        }
        public CompanyProfileDataItem GetCompanyProfile(string adviserUserId, string profileId)
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


        //public async Task SaveAsync()
        //{
        //    await db.SaveChangesAsync();
        //}

        //#region helpers
        //private void AddAdviserProfile_Complete(AdviserRegistrationBindingModel model)
        //{
        //    CommonReferenceDataRepository comRepo = new CommonReferenceDataRepository(db);
        //    #region adviser_details
        //    Adviser adviserDetails = new Adviser()
        //    {
        //        AdviserId = model.adviserUserId,
        //        ABNACN = model.ABN,
        //        CompanyName = model.companyName,
        //        Country = model.country,
        //        AddressLn1 = model.addressLine1,
        //        AddressLn2 = model.addressLine2,
        //        AddressLn3 = model.addressLine3,
        //        CreatedOn = DateTime.Now,
        //        CurrentTitle = model.currentPositionTitle,
        //        ExperienceStartDate = model.industryExperienceStartDate,
        //        Fax = model.businessFax,
        //        FirstName = model.firstName,
        //        Gender = model.gender,
        //        LastName = model.lastName,
        //        LastUpdate = DateTime.Now,
        //        Lat = GoogleGeoService.GetCoordinatesLat(model.addressLine1 + " "
        //        + model.addressLine2 + " " + model.addressLine3 + " "
        //        + model.state + " " + model.country),
        //        Lng = GoogleGeoService.GetCoordinatesLng(model.addressLine1 + " "
        //        + model.addressLine2 + " " + model.addressLine3 + " "
        //        + model.state + " " + model.country),
        //        MiddleName = model.middleName,
        //        Mobile = model.businessMobile,
        //        Phone = model.businessPhone,
        //        PostCode = model.postCode,
        //        State = model.state,
        //        Suburb = model.suburb,
        //        Title = model.title,
        //        VerifiedId = BusinessLayerParameters.verificationStatus_NotVerified,
        //    };

        //    db.Advisers.Add(adviserDetails);
        //    #endregion
        //    #region adviser_dealerGroupDetails
        //    Adviser_DealerGroupDetails adviserDealerGroupDetails = new Adviser_DealerGroupDetails()
        //    {
        //        AddressLn1 = model.dealerGroup_addressLine1,
        //        AddressLn2 = model.dealerGroup_addressLine2,
        //        AddressLn3 = model.dealerGroup_addressLine3,
        //        AFSL = model.asfl,
        //        AuthorisedRepNumber = model.authorizedRepresentativeNumber,
        //        Country = model.dealerGroup_country,
        //        DealerGroupName = model.dealerGroupName,
        //        HasDerivativesLicense = model.dealerGroupHasDerivativesLicense ? 1 : 0,
        //        IsAuthorisedRep = model.isAuthorizedRepresentative ? 1 : 0,
        //        LastUpdated = DateTime.Now,
        //        PostCode = model.dealerGroup_postCode,
        //        State = model.dealerGroup_state,
        //        Suburb = model.dealerGroup_suburb,
        //        UserId = model.adviserUserId
        //    };
        //    db.Adviser_DealerGroupDetails.Add(adviserDealerGroupDetails);
        //    #endregion
        //    #region adviser_managementDetails
        //    Adviser_ManagementDetails adviserManagementDetails = new Adviser_ManagementDetails()
        //    {
        //        LastUpdated = DateTime.Now,
        //        TotalManagedInvestments = getDoubleFromProperty(model.totalInvestmentUndermanagement),
        //        TotalDirectAustralianEquities = getDoubleFromProperty(model.totalDirectAustralianEquitiesUnderManagement),
        //        TotalDirectInternational = getDoubleFromProperty(model.totalDirectInterantionalEquitiesUnderManagement),
        //        TotalFixedInterest = getDoubleFromProperty(model.totalDirectFixedInterestUnderManagement),
        //        TotalLendingBook = getDoubleFromProperty(model.totalDirectLendingBookInterestUnderManagement),
        //        UserId = model.adviserUserId,
        //        TotalAssets = getDoubleFromProperty(model.totalAssetUnderManagement),
        //        ApproxClientNumbersId = model.numberOfClientsId
        //    };
        //    db.Adviser_ManagementDetails.Add(adviserManagementDetails);
        //    #endregion
        //    #region adviser_educations
        //    if (model.educations != null)
        //    {
        //        foreach (var item in model.educations)
        //        {

        //            var level = comRepo.GetAllEducationLevels().SingleOrDefault(ed => ed.EducationLevelsId == item.educationLevelId);
        //            Adviser_Education education = new Adviser_Education()
        //            {
        //                CourseName = item.courseTitle,
        //                CurrentlyStudying = item.courseStatus ? 1 : 0,
        //                EducationLevels = level != null ? level.EducationLevels : "",
        //                Institution = item.institution,
        //                LastUpdated = DateTime.Now,
        //                UserId = model.adviserUserId,
        //            };
        //            db.Adviser_Education.Add(education);

        //        }
        //    }

        //    #endregion
        //    #region adviser_professionalType
        //    Adviser_ProfessionalType professionalType = new Adviser_ProfessionalType()
        //    {
        //        ProfessionTypeId = string.IsNullOrEmpty(model.professiontypeId) ? (int?)null : Convert.ToInt32(model.professiontypeId),
        //        UserId = model.adviserUserId
        //    };
        //    db.Adviser_ProfessionalType.Add(professionalType);
        //    #endregion
        //    #region adviser_serviceProviding
        //    if (model.services != null)
        //    {
        //        foreach (var group in model.services)
        //        {

        //            foreach (var item in group.services)
        //            {
        //                Adviser_ServicesProviding service = new Adviser_ServicesProviding()
        //                {
        //                    DateAdded = DateTime.Now,
        //                    SubServiceId = item.serviceId,
        //                    SubServiceProvideYesNo = item.providing ? "Yes" : "No",
        //                    UserId = model.adviserUserId
        //                };
        //                db.Adviser_ServicesProviding.Add(service);
        //            }
        //        }
        //    }
        //    #endregion
        //    #region adviser Newsletter
        //    foreach (var item in model.newsLetterServices)
        //    {
        //        User_NewsletterServices newsletter = new User_NewsletterServices()
        //        {
        //            IsSubscribed = item.selected ? "True" : "False",
        //            LastUpdated = DateTime.Now,
        //            NewsletterServicesId = item.newsLetterServiceId,
        //            UserId = model.adviserUserId
        //        };
        //        db.User_NewsletterServices.Add(newsletter);
        //    }
        //    #endregion
        //    #region adviser fees and commissions
        //    foreach (var item in model.commissionsAndFees)
        //    {

        //        Adviser_CommissionsAndFees fc = new Adviser_CommissionsAndFees()
        //        {
        //            CommissionsAndFeesId = item.id,
        //            LastUpdated = DateTime.Now,
        //            UserId = model.adviserUserId,
        //            YesNoNA = item.selected ? 1 : 0
        //        };
        //        db.Adviser_CommissionsAndFees.Add(fc);
        //    }

        //    Adviser_CommissionsAndFees rfc = new Adviser_CommissionsAndFees()
        //    {
        //        CommissionsAndFeesId = BusinessLayerParameters.feesAndCommissions_RemunerationType,
        //        YesNoNA = model.remunerationMethodSpecified ? 1 : 0,
        //        UserId = model.adviserUserId,
        //        LastUpdated = DateTime.Now,
        //        CommissionDescription = model.remunerationMethod
        //    };
        //    db.Adviser_CommissionsAndFees.Add(rfc);
        //    #endregion
        //    #region adviser target clients
        //    Adviser_TargetClients target = new Adviser_TargetClients()
        //    {
        //        AnnualIncomeLevelsId = model.annualIncomeLevelId,
        //        InvestibleAssetsLevelId = model.investibleAssetLevel,
        //        LastUpdated = DateTime.Now,
        //        TotalAssetsLevelId = model.totalAssetLevelId,
        //        UserId = model.adviserUserId
        //    };
        //    db.Adviser_TargetClients.Add(target);


            #endregion
        //}
        private void UpdateAdviserProfile_Complete(AdviserRegistrationBindingModel model)
        {
            var userid = model.adviserUserId;
            #region adviser details
            var adviserDetails = db.Advisers.FirstOrDefault(ad => ad.AdviserId == userid);
            adviserDetails.ABNACN = model.ABN;
            adviserDetails.CompanyName = model.companyName;
            adviserDetails.Country = model.country;
            adviserDetails.AddressLn1 = model.addressLine1;
            adviserDetails.AddressLn2 = model.addressLine2;
            adviserDetails.AddressLn3 = model.addressLine3;
            adviserDetails.CurrentTitle = model.currentPositionTitle;
            adviserDetails.ExperienceStartDate = model.industryExperienceStartDate;
            adviserDetails.Fax = model.businessFax;
            adviserDetails.Gender = model.gender;
            adviserDetails.LastName = model.lastName;
            adviserDetails.FirstName = model.firstName;
            adviserDetails.LastUpdate = DateTime.Now;
            adviserDetails.Lat = GoogleGeoService.GetCoordinatesLat(model.addressLine1 + " "
            + model.addressLine2 + " " + model.addressLine3 + " "
            + model.state + " " + model.country);
            adviserDetails.Lng = GoogleGeoService.GetCoordinatesLng(model.addressLine1 + " "
            + model.addressLine2 + " " + model.addressLine3 + " "
            + model.state + " " + model.country);
            adviserDetails.MiddleName = model.middleName;
            adviserDetails.Mobile = model.businessMobile;
            adviserDetails.Phone = model.businessPhone;
            adviserDetails.PostCode = model.postCode;
            adviserDetails.State = model.state;
            adviserDetails.Suburb = model.suburb;
            adviserDetails.Title = model.title;

            #endregion
            //#region adviser_dealer_group
            //var dealership = db.Adviser_DealerGroupDetails.FirstOrDefault(d => d.UserId == userid);
            //dealership.AddressLn1 = model.dealerGroup_addressLine1;
            //dealership.AddressLn2 = model.dealerGroup_addressLine2;
            //dealership.AddressLn3 = model.dealerGroup_addressLine3;
            //dealership.AFSL = model.asfl;
            //dealership.AuthorisedRepNumber = model.authorizedRepresentativeNumber;
            //dealership.Country = model.dealerGroup_country;
            //dealership.DealerGroupName = model.dealerGroupName;
            //dealership.HasDerivativesLicense = model.dealerGroupHasDerivativesLicense ? 1 : 0;
            //dealership.IsAuthorisedRep = model.isAuthorizedRepresentative ? 1 : 0;
            //dealership.LastUpdated = DateTime.Now;
            //dealership.PostCode = model.dealerGroup_postCode;
            //dealership.State = model.dealerGroup_state;
            //dealership.Suburb = model.dealerGroup_suburb;
            //#endregion
            //#region adviser management details
            //var managementDetail = db.Adviser_ManagementDetails.FirstOrDefault(m => m.UserId == userid);
            //managementDetail.LastUpdated = DateTime.Now;
            //managementDetail.TotalManagedInvestments = getDoubleFromProperty(model.totalInvestmentUndermanagement);
            //managementDetail.TotalDirectAustralianEquities = getDoubleFromProperty(model.totalDirectAustralianEquitiesUnderManagement);
            //managementDetail.TotalDirectInternational = getDoubleFromProperty(model.totalDirectInterantionalEquitiesUnderManagement);
            //managementDetail.TotalFixedInterest = getDoubleFromProperty(model.totalDirectFixedInterestUnderManagement);
            //managementDetail.TotalLendingBook = getDoubleFromProperty(model.totalDirectLendingBookInterestUnderManagement);
            //managementDetail.TotalAssets = getDoubleFromProperty(model.totalAssetUnderManagement);
            //managementDetail.ApproxClientNumbersId = model.numberOfClientsId;
            //#endregion
            //#region educations
            ////remove all educations
            //var existingEducations = db.Adviser_Education.Where(ed => ed.UserId == userid);
            //db.Adviser_Education.RemoveRange(existingEducations);
            ////add new educations
            //if (model.educations != null)
            //{
            //    foreach (var item in model.educations)
            //    {
            //        var level = comRepo.GetAllEducationLevels().SingleOrDefault(ed => ed.EducationLevelsId == item.educationLevelId);
            //        Adviser_Education education = new Adviser_Education()
            //        {
            //            CourseName = item.courseTitle,
            //            CurrentlyStudying = item.courseStatus ? 1 : 0,
            //            EducationLevels = level != null ? level.EducationLevels : "",
            //            Institution = item.institution,
            //            LastUpdated = DateTime.Now,
            //            UserId = userid,
            //        };
            //        db.Adviser_Education.Add(education);

            //    }
            //}
            //#endregion
            //#region professional Type
            //var professionalType = db.Adviser_ProfessionalType.FirstOrDefault(p => p.UserId == userid);
            //professionalType.ProfessionTypeId = string.IsNullOrEmpty(model.professiontypeId) ?
            //    (int?)null : Convert.ToInt32(model.professiontypeId);
            //#endregion
            //#region services provided
            ////remove existing services
            //var existingServices = db.Adviser_ServicesProviding.Where(s => s.UserId == userid);
            //db.Adviser_ServicesProviding.RemoveRange(existingServices);
            //if (model.services != null)
            //{
            //    foreach (var group in model.services)
            //    {

            //        foreach (var item in group.services)
            //        {
            //            Adviser_ServicesProviding service = new Adviser_ServicesProviding()
            //            {
            //                DateAdded = DateTime.Now,
            //                SubServiceId = item.serviceId,
            //                SubServiceProvideYesNo = item.providing ? "Yes" : "No",
            //                UserId = userid
            //            };
            //            db.Adviser_ServicesProviding.Add(service);
            //        }
            //    }
            //}
            //#endregion
            //#region newsletter
            ////remove existing newsletter settings
            //var existingNewsletter = db.User_NewsletterServices.Where(n => n.UserId == userid);
            //db.User_NewsletterServices.RemoveRange(existingNewsletter);
            ////add new ones
            //foreach (var item in model.newsLetterServices)
            //{
            //    User_NewsletterServices newsletter = new User_NewsletterServices()
            //    {
            //        IsSubscribed = item.selected ? "True" : "False",
            //        LastUpdated = DateTime.Now,
            //        NewsletterServicesId = item.newsLetterServiceId,
            //        UserId = userid
            //    };
            //    db.User_NewsletterServices.Add(newsletter);
            //}
            //#endregion
            //#region commission and fees
            ////remove all existing commissions and fees
            //var existingFees = db.Adviser_CommissionsAndFees.Where(c => c.UserId == userid);
            //db.Adviser_CommissionsAndFees.RemoveRange(existingFees);
            ////add new ones
            //foreach (var item in model.commissionsAndFees)
            //{

            //    Adviser_CommissionsAndFees fc = new Adviser_CommissionsAndFees()
            //    {
            //        CommissionsAndFeesId = item.id,
            //        LastUpdated = DateTime.Now,
            //        UserId = userid,
            //        YesNoNA = item.selected ? 1 : 0
            //    };
            //    db.Adviser_CommissionsAndFees.Add(fc);
            //}

            //Adviser_CommissionsAndFees rfc = new Adviser_CommissionsAndFees()
            //{
            //    CommissionsAndFeesId = BusinessLayerParameters.feesAndCommissions_RemunerationType,
            //    YesNoNA = model.remunerationMethodSpecified ? 1 : 0,
            //    UserId = userid,
            //    LastUpdated = DateTime.Now,
            //    CommissionDescription = model.remunerationMethod
            //};
            //db.Adviser_CommissionsAndFees.Add(rfc);
            //#endregion
            //#region target clients
            //var target = db.Adviser_TargetClients.FirstOrDefault(t => t.UserId == userid);
            //target.AnnualIncomeLevelsId = model.annualIncomeLevelId;
            //target.InvestibleAssetsLevelId = model.investibleAssetLevel;
            //target.LastUpdated = DateTime.Now;
            //target.TotalAssetsLevelId = model.totalAssetLevelId;
            //#endregion
        }

        private bool AdviserExist(string adviserUsereId)
        {
            return db.Advisers.Where(a => a.AdviserId == adviserUsereId).Any();
                //&& db.Adviser_DealerGroupDetails.Where(a => a.UserId == adviserUsereId).Any()
                //&& db.Adviser_ManagementDetails.Where(a => a.UserId == adviserUsereId).Any()
                //&& db.Adviser_ProfessionalType.Where(a => a.UserId == adviserUsereId).Any()
                //&& db.Adviser_TargetClients.Where(a => a.UserId == adviserUsereId).Any();
        }





        //#endregion

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