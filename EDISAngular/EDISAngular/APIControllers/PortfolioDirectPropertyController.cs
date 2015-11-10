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
using Domain.Portfolio.AggregateRoots.Accounts;
using SqlRepository;
using Domain.Portfolio.AggregateRoots.Asset;
using Domain.Portfolio.Services;
using Domain.Portfolio.AggregateRoots;
using Shared;

namespace EDISAngular.APIControllers
{
    public class PortfolioDirectPropertyController : ApiController
    {

        private PortfolioRepository repo = new PortfolioRepository();
        private EdisRepository edisRepo = new EdisRepository();

        [HttpGet, Route("api/Adviser/DirectPropertyPortfolio/General")]
        public SummaryGeneralInfo GetGeneralInfo_Adviser(string clientGroupId = null)
        {
            return GenerateGeneralInforModel(getPropertyAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/DirectPropertyPortfolio/General")]
        public SummaryGeneralInfo GetGeneralInfo_Client()
        {
            return GenerateGeneralInforModel(getPropertyAssetForClient());
        }
        
        [HttpGet, Route("api/Adviser/DirectPropertyPortfolio/GeoInfo")]
        public DirectPropertyGeoModel GetGeoInfo_Adviser(string clientGroupId = null)
        {
            return GeneratePropertyGeoModel(getPropertyAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/DirectPropertyPortfolio/GeoInfo")]
        public DirectPropertyGeoModel GetGeoInfo_Client()
        {
            return GeneratePropertyGeoModel(getPropertyAssetForClient());
        }
        
        [HttpGet, Route("api/Adviser/DirectPropertyPortfolio/QuickStats")]
        public IEnumerable<PortfolioQuickStatsModel> GetQuickStats_Adviser(string clientGroupId = null)
        {
            return repo.DirectProperty_GetQuickStats_Adviser(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/DirectPropertyPortfolio/QuickStats")]
        public IEnumerable<PortfolioQuickStatsModel> GetQuickStats_Client()
        {
            return repo.DirectProperty_GetQuickStats_Client(User.Identity.GetUserId());
        }
        
        [HttpGet, Route("api/Adviser/DirectPropertyPortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Adviser(string clientGroupId = null)
        {
            return GenerateRatingsModel(getPropertyAssetForAdviser(clientGroupId));
        }
        [HttpGet, Route("api/Client/DirectPropertyPortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Client()
        {
            return GenerateRatingsModel(getPropertyAssetForClient());
        }
        
        [HttpGet, Route("api/Adviser/DirectPropertyPortfolio/CashflowDetail")]
        public CashflowBriefModel GetCashflowDetailed_Adviser(string clientGroupId = null)
        {
            return repo.DirectProperty_GetSummaryCashflowDetailed_Adviser(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/DirectPropertyPortfolio/CashflowDetail")]
        public DirectPropertyCashflowDetailedModel GetCashflowDetailed_Client()
        {
            return repo.DirectProperty_GetSummaryCashflowDetailed_Client(User.Identity.GetUserId());
        }


        public List<AssetBase> getPropertyAssetForAdviser(string clientGroupId) {
            List<AssetBase> assets = new List<AssetBase>();

            if (string.IsNullOrEmpty(clientGroupId)) {

                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                groupAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<DirectProperty>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<DirectProperty>().Cast<AssetBase>().ToList()));

                return assets;
            } else {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = clientGroup.GetAccountsSync(DateTime.Now);
                List<ClientAccount> clientAccounts = new List<ClientAccount>();
                clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

                accounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<DirectProperty>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<DirectProperty>().Cast<AssetBase>().ToList()));

                return assets;
            }
        }

        public List<AssetBase> getPropertyAssetForClient() {
            List<AssetBase> assets = new List<AssetBase>();

            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id) {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                groupAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<DirectProperty>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<DirectProperty>().Cast<AssetBase>().ToList()));
            } else {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                accounts.ForEach(a => assets.AddRange(a.GetAssetsSync().OfType<DirectProperty>().Cast<AssetBase>().ToList()));
            }

            return assets;
        }

        public SummaryGeneralInfo GenerateGeneralInforModel(List<AssetBase> assets) {
            double totalCost = 0;
            double totalMarketValue = 0;
            double capitalGain = 0;

            foreach (var asset in assets) {
                totalCost += asset.GetCost().Total;
                totalMarketValue += asset.GetTotalMarketValue();
                capitalGain += asset.GetCost().CapitalGain;
            }
            SummaryGeneralInfo summary = new SummaryGeneralInfo {
                cost = totalCost,
                marketValue = totalMarketValue,
                pl = totalMarketValue - totalCost,
                plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100,
                capitalGain = capitalGain
            };
            return summary;
        }

        public DirectPropertyGeoModel GeneratePropertyGeoModel(List<AssetBase> assets) {
            List<DirectPropertyGeoItem> dataList = new List<DirectPropertyGeoItem>();

            foreach (var property in assets.OfType<DirectProperty>().ToList()) {
                dataList.Add(new DirectPropertyGeoItem {
                    id = property.Id,
                    address = property.FullAddress,
                    latitude = property.Latitude == null ? 0 : (double)property.Latitude,
                    longitude = property.Longitude == null ? 0 : (double)property.Longitude,
                    country = property.Country,
                    state = property.State,
                    type = property.PropertyType,
                    value = property.GetTotalMarketValue()
                });
            }

            DirectPropertyGeoModel model = new DirectPropertyGeoModel {
                data = dataList
            };

            return model;
        }

        public PortfolioRatingModel GenerateRatingsModel(List<AssetBase> assets) {
            List<DirectProperty> equityWithResearchValues = assets.OfType<DirectProperty>().ToList();
            var weightings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();
            double assetsSuitability = weightings.Sum(w => w.Percentage * ((DirectProperty)w.Weightable).GetRating().TotalScore);
            double percentage = equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());

            PortfolioRatingModel model = new PortfolioRatingModel {
                suitability = assetsSuitability,
                notSuited = percentage,
                data = new List<PortfolioRatingItemModel>()
            };

            foreach (var weighting in weightings) {
                Equity equity = (Equity)weighting.Weightable;

                if (model.data.Any(m => m.name == equity.Ticker) == false) {
                    model.data.Add(new PortfolioRatingItemModel {
                        name = equity.Ticker,
                        weighting = weighting.Percentage,
                        score = equity.GetRating().TotalScore
                    });
                }
            }
            return model;
        }
    }
}
