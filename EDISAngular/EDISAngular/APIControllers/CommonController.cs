using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EDISAngular;
using EDISAngular.Models.ViewModels;
using EDIS_DOMAIN;
using EDISAngular.Infrastructure.DatabaseAccess;
using Shared;
using EDISAngular.Models;
using System.Reflection;
using System.ComponentModel;
using Domain.Portfolio.TransactionModels;

namespace EDISAngular.APIControllers
{
    public class CommonController : ApiController
    {
        public CommonController()
        {
            //comRepo = new CommonReferenceDataRepository();
        }




        [HttpGet, Route("api/common/assetClasses")]
        public List<AssetTypeView> getAssetTypes()
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

            return result.Select(a => new AssetTypeView()
            {
                id = a.AssetTypeID,
                name = a.AssetTypeName
            }).ToList();
        }
        [HttpGet, Route("api/common/productClasses")]
        public List<ProductTypeView> getProductTypes()
        {
            var productTypes = Enum.GetValues(typeof(ProductTypes)).Cast<ProductTypes>();
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


        [HttpGet, Route("api/Adviser/Transaction/policyTypes")]
        public List<ClientView> GetAllPolicyTypes()
        {
            List<ClientView> views = new List<ClientView>();

            foreach (var type in Enum.GetValues(typeof(PolicyType)))
            {
                views.Add(new ClientView
                {
                    id = ((int)type).ToString(),
                    name = type.ToString()
                });
            }
            return views;
        }

        [HttpGet, Route("api/Adviser/Transaction/insuranceTypes")]
        public List<ClientView> GetAllInsuranceTypes()
        {
            List<ClientView> views = new List<ClientView>();

            foreach (var type in Enum.GetValues(typeof(InsuranceType)))
            {
                views.Add(new ClientView
                {
                    id = ((int)type).ToString(),
                    name = type.ToString()
                });
            }
            return views;
        }

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


    }
}
