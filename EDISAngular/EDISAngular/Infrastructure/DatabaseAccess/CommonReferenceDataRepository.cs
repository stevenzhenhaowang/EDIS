using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Reflection;
using System.ComponentModel;
using EDISAngular.Models.ViewModels;


using EDISAngular.Models.ServiceModels;
using EDISAngular.Infrastructure.DbFirst;
using EDIS_DOMAIN.Enum.Enums;
using EDISAngular.Models;

namespace EDISAngular.Infrastructure.DatabaseAccess
{
    public class CommonReferenceDataRepository : BaseRepository
    {
        private edisDbEntities db;
        public CommonReferenceDataRepository()
        {
            db = new edisDbEntities();
        }
        public CommonReferenceDataRepository(edisDbEntities database)
        {
            db = database;
        }

        //public List<SubService> GetSubServices_Filtered(Func<SubService, bool> filter)
        //{
        //    return db.SubServices.Where(filter).ToList();
        //}
        //public List<Service> GetAllServiceGroups()
        //{
        //    return db.Services.ToList();
        //}
        public List<EDISAngular.Models.ViewModels.ProfessionType> GetAllProfessionTypes()
        {
            var professionTypes = Enum.GetValues(typeof(EDIS_DOMAIN.Enum.Enums.ProfessionType)).Cast<EDIS_DOMAIN.Enum.Enums.ProfessionType>();
            List<EDISAngular.Models.ViewModels.ProfessionType> result = new List<EDISAngular.Models.ViewModels.ProfessionType>();

            foreach (var ptype in professionTypes)
            {
                result.Add(new EDISAngular.Models.ViewModels.ProfessionType
                {
                    ProfessionTypeId = (int)ptype,
                    ProfessionType1 = GetEnumDescription(ptype)
                });
            }

            return result;
        }
        public List<NoteTypeView> GetAllNoteTypes()
        {

            var noteTypes = Enum.GetValues(typeof(EDIS_DOMAIN.Enum.Enums.NoteTypes)).Cast<EDIS_DOMAIN.Enum.Enums.NoteTypes>();
            List<EDISAngular.Models.ViewModels.NoteTypeView> result = new List<EDISAngular.Models.ViewModels.NoteTypeView>();

            foreach (var ptype in noteTypes)
            {
                result.Add(new EDISAngular.Models.ViewModels.NoteTypeView
                {
                    id = (int)ptype,
                    name = GetEnumDescription(ptype)
                });
            }

            return result;

        }
        public List<EDISAngular.Models.ViewModels.ProductTypeView> GetAllProductTypes()
        {
            var productTypes = Enum.GetValues(typeof(EDIS_DOMAIN.Enum.Enums.ProductTypes)).Cast<EDIS_DOMAIN.Enum.Enums.ProductTypes>();
            List<EDISAngular.Models.ViewModels.ProductTypeView> result = new List<EDISAngular.Models.ViewModels.ProductTypeView>();

            foreach (var ptype in productTypes)
            {
                result.Add(new EDISAngular.Models.ViewModels.ProductTypeView
                {
                    id = (int)ptype,
                    name = GetEnumDescription(ptype)
                });
            }

            return result;
        }
        public List<EducationLevel> GetAllEducationLevels()
        {
            var educationLevels = Enum.GetValues(typeof(EducationLevels)).Cast<EducationLevels>();
            List<EducationLevel> result = new List<EducationLevel>();

            foreach (var ptype in educationLevels)
            {
                result.Add(new EducationLevel
                {
                    EducationLevelsId = (int)ptype,
                    EducationLevels = GetEnumDescription(ptype)
                });
            }

            return result;
        }
        public List<NewsletterService> GetNewsletterService()
        {
            var newsletteServices = Enum.GetValues(typeof(NewsLetterServices)).Cast<NewsLetterServices>();
            List<NewsletterService> result = new List<NewsletterService>();

            foreach (var ptype in newsletteServices)
            {
                result.Add(new NewsletterService
                {
                    NewsletterServicesId = (int)ptype,
                    NewsletterServices = GetEnumDescription(ptype)
                });
            }

            return result;
        }
        public List<CommissionsAndFee> GetCommissionAndFees_Filtered(Func<CommissionsAndFee, bool> filter)
        {
            var commissionsAndFees = Enum.GetValues(typeof(CommissionsAndFees)).Cast<CommissionsAndFees>();
            List<CommissionsAndFee> result = new List<CommissionsAndFee>();

            foreach (var ptype in commissionsAndFees)
            {
                result.Add(new CommissionsAndFee
                {
                    CommissionsAndFeesId = (int)ptype,
                    CommissionsAndFees = GetEnumDescription(ptype)
                });
            }

            return result.Where(filter).ToList();
        }
        public List<EDISAngular.Models.ViewModels.ApproxClientNumber> GetApproxClientNumbers()
        {
            var approxiClientNumbers = Enum.GetValues(typeof(EDIS_DOMAIN.Enum.Enums.ApproxClientNumber)).Cast<EDIS_DOMAIN.Enum.Enums.ApproxClientNumber>();
            List<EDISAngular.Models.ViewModels.ApproxClientNumber> result = new List<EDISAngular.Models.ViewModels.ApproxClientNumber>();

            foreach (var ptype in approxiClientNumbers)
            {
                result.Add(new EDISAngular.Models.ViewModels.ApproxClientNumber
                {
                    ApproxClientNumbersId = (int)ptype,
                    ApproxClientNumbers = GetEnumDescription(ptype)
                });
            }

            return result;
        }
        public List<EDISAngular.Models.ViewModels.InvestibleAssetsLevel> GetAllInvestibleAssetLevels()
        {
            var investibleAssetsLevels = Enum.GetValues(typeof(EDIS_DOMAIN.Enum.Enums.InvestibleAssetsLevel)).Cast<EDIS_DOMAIN.Enum.Enums.InvestibleAssetsLevel>();
            List<EDISAngular.Models.ViewModels.InvestibleAssetsLevel> result = new List<EDISAngular.Models.ViewModels.InvestibleAssetsLevel>();

            foreach (var ptype in investibleAssetsLevels)
            {
                result.Add(new EDISAngular.Models.ViewModels.InvestibleAssetsLevel
                {
                    InvestibleAssetsLevelId = (int)ptype,
                    InvestibleAssetsLevel1 = GetEnumDescription(ptype)
                });
            }

            return result;
        }
        public List<TotalAssetsLevel> GetAllTotalAssetLevels()
        {
            var totalAssetLevels = Enum.GetValues(typeof(TotalAssetLevels)).Cast<TotalAssetLevels>();
            List<TotalAssetsLevel> result = new List<TotalAssetsLevel>();

            foreach (var ptype in totalAssetLevels)
            {
                result.Add(new TotalAssetsLevel
                {
                    TotalAssetsLevelId = (int)ptype,
                    TotalAssetsLevel1 = GetEnumDescription(ptype)
                });
            }

            return result;
        }
        public List<AssetType> GetAssetTypes()
        {
            var assetTypes = Enum.GetValues(typeof(BalanceGroups)).Cast<BalanceGroups>();
            List<AssetType> result = new List<AssetType>();

            foreach (var ptype in assetTypes)
            {
                result.Add(new AssetType
                {
                    AssetTypeID = (int)ptype,
                    AssetTypeName = GetEnumDescription(ptype)
                });
            }

            return result;
        }
        public List<EDISAngular.Models.ViewModels.AnnualIncomeLevel> GetAllAnnualIncomeLevels()
        {
            var annualIncomeLevels = Enum.GetValues(typeof(EDIS_DOMAIN.Enum.Enums.AnnualIncomeLevel)).Cast<EDIS_DOMAIN.Enum.Enums.AnnualIncomeLevel>();
            List<EDISAngular.Models.ViewModels.AnnualIncomeLevel> result = new List<EDISAngular.Models.ViewModels.AnnualIncomeLevel>();

            foreach (var ptype in annualIncomeLevels)
            {
                result.Add(new EDISAngular.Models.ViewModels.AnnualIncomeLevel
                {
                    AnnualIncomeLevelsId = (int)ptype,
                    AnnualIncomeLevels = GetEnumDescription(ptype)
                });
            }

            return result;
        }
        #region service methods added 12/05/2015

        public List<CompanyBriefModel> GetAllCompanyBriefDetails()
        {
            return new List<CompanyBriefModel>
            {
                new CompanyBriefModel{ companyName="Company 1", companyTicker="00001"},
                new CompanyBriefModel{ companyName="Company 1", companyTicker="00001"},
                new CompanyBriefModel{ companyName="Company 1", companyTicker="00001"},
                new CompanyBriefModel{ companyName="Company 1", companyTicker="00001"},
                new CompanyBriefModel{ companyName="Company 1", companyTicker="00001"},
            };
        }
        public List<TickerBriefModel> GetAllTIckers()
        {
            return new List<TickerBriefModel>
            {
                new TickerBriefModel{ tickerName="Ticker 01", tickerNumber="001"},
                new TickerBriefModel{ tickerName="Ticker 02", tickerNumber="002"},
                new TickerBriefModel{ tickerName="Ticker 03", tickerNumber="003"},
                new TickerBriefModel{ tickerName="Ticker 04", tickerNumber="004"},
            };
        }

        #endregion

        #region helper
        private static string GetEnumDescription(Enum value)
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

        #endregion

    }
}