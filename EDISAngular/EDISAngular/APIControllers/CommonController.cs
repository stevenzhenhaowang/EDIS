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

namespace EDISAngular.APIControllers
{
    public class CommonController : ApiController
    {


        private CommonReferenceDataRepository comRepo;
        public CommonController()
        {
            comRepo = new CommonReferenceDataRepository();
        }

        [HttpGet, Route("api/common/assetClasses")]
        public List<AssetTypeView> getAssetTypes()
        {
            return comRepo.GetAssetTypes().Select(a => new AssetTypeView()
            {
                id = a.AssetTypeID,
                name = a.AssetTypeName
            }).ToList();

        }
        [HttpGet, Route("api/common/productClasses")]
        public List<ProductTypeView> getProductTypes()
        {
            return comRepo.GetAllProductTypes();
        }




    }
}
