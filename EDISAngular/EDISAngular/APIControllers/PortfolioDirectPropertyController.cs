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
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                double totalCost = 0;
                double totalMarketValue = 0;
                foreach (var account in groupAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<DirectProperty>().Cast<AssetBase>().ToList();

                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                    }
                }
                foreach (var account in clientAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<DirectProperty>().Cast<AssetBase>().ToList();
                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                    }
                }
                SummaryGeneralInfo summary = new SummaryGeneralInfo
                {
                    cost = totalCost,
                    marketValue = totalMarketValue,
                    pl = totalMarketValue - totalCost,
                    plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100
                };

                return summary;
            }
            else
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                double totalCost = 0;
                double totalMarketValue = 0;
                foreach (var account in accounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<DirectProperty>().Cast<AssetBase>().ToList();
                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                    }
                }
                SummaryGeneralInfo summary = new SummaryGeneralInfo
                {
                    cost = totalCost,
                    marketValue = totalMarketValue,
                    pl = totalMarketValue - totalCost,
                    plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100
                };

                return summary;
            }

            //return repo.DirectProperty_GetGeneral_Adviser(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/DirectPropertyPortfolio/General")]
        public SummaryGeneralInfo GetGeneralInfo_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                double totalCost = 0;
                double totalMarketValue = 0;
                foreach (var account in groupAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<DirectProperty>().Cast<AssetBase>().ToList();

                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                    }
                }
                foreach (var account in clientAccounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<DirectProperty>().Cast<AssetBase>().ToList();
                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                    }
                }
                SummaryGeneralInfo summary = new SummaryGeneralInfo
                {
                    cost = totalCost,
                    marketValue = totalMarketValue,
                    pl = totalMarketValue - totalCost,
                    plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100
                };

                return summary;
            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);

                double totalCost = 0;
                double totalMarketValue = 0;
                foreach (var account in accounts)
                {
                    List<AssetBase> assets = account.GetAssetsSync().OfType<DirectProperty>().Cast<AssetBase>().ToList();
                    foreach (var asset in assets)
                    {
                        totalCost += asset.GetCost().Total;
                        totalMarketValue += asset.GetTotalMarketValue();
                    }
                }
                SummaryGeneralInfo summary = new SummaryGeneralInfo
                {
                    cost = totalCost,
                    marketValue = totalMarketValue,
                    pl = totalMarketValue - totalCost,
                    plp = totalCost == 0 ? 0 : (totalMarketValue - totalCost) / totalCost * 100
                };

                return summary;
            }
        }
        [HttpGet, Route("api/Adviser/DirectPropertyPortfolio/GeoInfo")]
        public DirectPropertyGeoModel GetGeoInfo_Adviser(string clientGroupId = null)
        {
            if (string.IsNullOrEmpty(clientGroupId))
            {

                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

                List<DirectPropertyGeoItem> dataList = new List<DirectPropertyGeoItem>();

                List<DirectProperty> properties = new List<DirectProperty>();
                foreach (var account in groupAccounts)
                {
                    properties.AddRange(account.GetAssetsSync().OfType<DirectProperty>().ToList());
                }
                foreach (var account in clientAccounts)
                {
                    properties.AddRange(account.GetAssetsSync().OfType<DirectProperty>().ToList());
                }

                foreach (var property in properties)
                {
                    dataList.Add(new DirectPropertyGeoItem
                    {
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


                DirectPropertyGeoModel model = new DirectPropertyGeoModel
                {
                    data = dataList
                };

                return model;
            }
            else {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);


                List<DirectPropertyGeoItem> dataList = new List<DirectPropertyGeoItem>();

                List<DirectProperty> properties = new List<DirectProperty>();
                foreach (var account in accounts)
                {
                    properties.AddRange(account.GetAssetsSync().OfType<DirectProperty>().ToList());
                }

                foreach (var property in properties)
                {
                    dataList.Add(new DirectPropertyGeoItem
                    {
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


                DirectPropertyGeoModel model = new DirectPropertyGeoModel
                {
                    data = dataList
                };

                return model;
            }

            //return repo.DirectProperty_GetGeoInfo_Adviser(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/DirectPropertyPortfolio/GeoInfo")]
        public DirectPropertyGeoModel GetGeoInfo_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);


                List<DirectPropertyGeoItem> dataList = new List<DirectPropertyGeoItem>();

                List<DirectProperty> properties = new List<DirectProperty>();
                foreach (var account in groupAccounts)
                {
                    properties.AddRange(account.GetAssetsSync().OfType<DirectProperty>().ToList());
                }
                foreach (var account in clientAccounts)
                {
                    properties.AddRange(account.GetAssetsSync().OfType<DirectProperty>().ToList());
                }

                foreach (var property in properties)
                {
                    dataList.Add(new DirectPropertyGeoItem
                    {
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


                DirectPropertyGeoModel model = new DirectPropertyGeoModel
                {
                    data = dataList
                };

                return model;
            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);


                List<DirectPropertyGeoItem> dataList = new List<DirectPropertyGeoItem>();

                List<DirectProperty> properties = new List<DirectProperty>();
                foreach (var account in accounts)
                {
                    properties.AddRange(account.GetAssetsSync().OfType<DirectProperty>().ToList());
                }

                foreach (var property in properties)
                {
                    dataList.Add(new DirectPropertyGeoItem
                    {
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


                DirectPropertyGeoModel model = new DirectPropertyGeoModel
                {
                    data = dataList
                };

                return model;
            }
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
            if (string.IsNullOrEmpty(clientGroupId))
            {
                List<GroupAccount> groupAccounts = edisRepo.getAllClientGroupAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.getAllClientAccountsForAdviser(User.Identity.GetUserId(), DateTime.Now);

                double assetsSuitability = 0;
                double percentage = 0;

                foreach (var account in groupAccounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<DirectProperty>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((DirectProperty)w.Weightable).GetRating().TotalScore);

                    //var weigthings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();

                    //% of not suitable assets
                    percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                        / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());
                }

                foreach (var account in clientAccounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<DirectProperty>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((DirectProperty)w.Weightable).GetRating().TotalScore);

                    //var weigthings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();

                    //% of not suitable assets
                    percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                        / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());
                }

                PortfolioRatingModel model = new PortfolioRatingModel
                {
                    suitability = assetsSuitability,
                    notSuited = percentage
                };

                return model;
            }
            else
            {
                ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
                List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);

                double assetsSuitability = 0;
                double percentage = 0;
                foreach (var account in accounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<DirectProperty>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((DirectProperty)w.Weightable).GetRating().TotalScore);

                    //var weigthings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();

                    //% of not suitable assets
                    percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                        / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());
                }

                PortfolioRatingModel model = new PortfolioRatingModel
                {
                    suitability = assetsSuitability,
                    notSuited = percentage
                };

                return model;
            }

            //return repo.DirectProperty_GetPortfolioRating_Adviser(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/Client/DirectPropertyPortfolio/Rating")]
        public PortfolioRatingModel GetRatings_Client()
        {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id)
            {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);


                double assetsSuitability = 0;
                double percentage = 0;

                foreach (var account in groupAccounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<DirectProperty>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((DirectProperty)w.Weightable).GetRating().TotalScore);

                    //var weigthings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();

                    //% of not suitable assets
                    percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                        / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());
                }

                foreach (var account in clientAccounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<DirectProperty>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((DirectProperty)w.Weightable).GetRating().TotalScore);

                    //var weigthings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();

                    //% of not suitable assets
                    percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                        / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());
                }

                PortfolioRatingModel model = new PortfolioRatingModel
                {
                    suitability = assetsSuitability,
                    notSuited = percentage
                };

                return model;
            }
            else
            {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);


                double assetsSuitability = 0;
                double percentage = 0;
                foreach (var account in accounts)
                {
                    var equityWithResearchValues = account.GetAssetsSync().OfType<DirectProperty>().ToList();

                    assetsSuitability += equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings().Sum(w => w.Percentage * ((DirectProperty)w.Weightable).GetRating().TotalScore);

                    //var weigthings = equityWithResearchValues.Cast<AssetBase>().ToList().GetAssetWeightings();

                    //% of not suitable assets
                    percentage += equityWithResearchValues.Where(a => a.GetRating().SuitabilityRating == SuitabilityRating.Danger).Sum(a => a.GetTotalMarketValue())
                        / equityWithResearchValues.Sum(a => a.GetTotalMarketValue());
                }

                PortfolioRatingModel model = new PortfolioRatingModel
                {
                    suitability = assetsSuitability,
                    notSuited = percentage
                };

                return model;
            }
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
    }
}
