using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EDISAngular.Infrastructure.DatabaseAccess;
using EDISAngular.Models.ServiceModels.RebalanceModels;
using Microsoft.AspNet.Identity;
using Shared;
using SqlRepository;
using Domain.Portfolio.AggregateRoots;
using Domain.Portfolio.AggregateRoots.Accounts;
using Domain.Portfolio.Services;
using Domain.Portfolio.AggregateRoots.Asset;

namespace EDISAngular.APIControllers
{
    public class RebalanceController : ApiController
    {
        private EdisRepository edisRepo;
        private RebalanceRepository rebRepo;

        public RebalanceController()
        {
            rebRepo = new RebalanceRepository();
            edisRepo = new EdisRepository();
        }

        [HttpGet, Route("api/adviser/models")]
        public List<RebalanceModel> GetAllModelsForAdviser()
        {
            return rebRepo.GetAllModelsForAdviser(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/client/models")]
        public List<RebalanceModel> GetAllModelsForClient()
        {
            return rebRepo.GetAllModelsForClient(User.Identity.GetUserId());
        }



        [HttpGet, Route("api/adviser/model")]
        public RebalanceModel GetModelForId(string modelId)
        {
            return rebRepo.GetModelById(modelId);
        }


        [HttpPost,Route("api/adviser/model/create")]
        public IHttpActionResult CreateNewModel(RebalanceCreationModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                //rebRepo.CreateNewModel(model, User.Identity.GetUserId());
                return Ok(new { modelId= "modelIdValue"});
            }
            return BadRequest();
        }
        [HttpPost,Route("api/adviser/model/remove")]
        public IHttpActionResult RemoveModel([FromBody]string modelId)
        {
            //rebRepo.RemoveModel(modelId);
            return Ok();
        }
        [HttpGet, Route("api/adviser/model/parameters")]
        public List<TemplateDetailsItemParameter> GetAllParamtersForGroup(string groupId, string clientGroupId = null)
        {
            List<TemplateDetailsItemParameter> parameters = new List<TemplateDetailsItemParameter>();

            ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(clientGroupId);
            List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
            List<ClientAccount> clientAccounts = new List<ClientAccount>();
            clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

            List<AssetBase> allAssets = new List<AssetBase>();

            accounts.ForEach(a => allAssets.AddRange(a.GetAssetsSync()));
            clientAccounts.ForEach(a => allAssets.AddRange(a.GetAssetsSync()));

            var weightings = allAssets.OfType<Equity>().Cast<AssetBase>().ToList().GetAssetWeightings();

            List<Equity> ownedEquities = allAssets.OfType<Equity>().ToList();
            
            if (edisRepo.GetAllSectorsSync().Contains(groupId)) {
                foreach (var equity in edisRepo.GetAllEquitiesBySectorName(groupId))
                {
                    var weightingPercentage = weightings.Count == 0 ? 0 : weightings.SingleOrDefault(w => ((Equity)w.Weightable).Id == equity.Id).Percentage;

                    parameters.Add(new TemplateDetailsItemParameter() { 
                        id = equity.Id,
                        currentWeighting = weightingPercentage,
                        itemName = equity.Name,
                        //groupId = groupId
                    });
                }
            }
            else if (edisRepo.GetAllResearchStringValueByKey("Country").Contains(groupId)) {
                foreach (var equity in edisRepo.GetAllEquitiesByResearchStringValue(groupId))
                {
                    var weightingPercentage = weightings.Count == 0 ? 0 : weightings.SingleOrDefault(w => ((Equity)w.Weightable).Id == equity.Id).Percentage;

                    parameters.Add(new TemplateDetailsItemParameter()
                    {
                        id = equity.Id,
                        currentWeighting = weightingPercentage,
                        itemName = equity.Name,
                        //groupId = groupId
                    });
                }
            }else{}

            return parameters;

            //return rebRepo.GetAllParametersForGroup(groupId);
        }
        [HttpGet, Route("api/adviser/model/filters")]
        public List<FilterGroupModel> GetFilterGroups()
        {
            List<FilterGroupModel> model = new List<FilterGroupModel>();
            FilterGroupModel sectorGroup = new FilterGroupModel 
            { 
                classificationType = edisRepo.GetEnumDescription(RebalanceClassificationType.Sectors),
                filters = new List<FilterGroupFilter>()
            };

            FilterGroupModel countryGroup = new FilterGroupModel 
            {
                classificationType = edisRepo.GetEnumDescription(RebalanceClassificationType.Country),
                filters = new List<FilterGroupFilter>()
            };

            foreach(var sector in edisRepo.GetAllSectorsSync()){
                sectorGroup.filters.Add(new FilterGroupFilter()
                {
                    groupId = sector,
                    groupName = sector,
                    //identityMetaKey = edisRepo.GetAllEquitiesNameBySectorName(sector)
                });
            }

            foreach (var country in edisRepo.GetAllResearchStringValueByKey("Country")) {
                countryGroup.filters.Add(new FilterGroupFilter()
                {
                    groupId = country,
                    groupName = country,
                    //identityMetaKey = edisRepo.GetAllEquitiesNameByResearchStringValue(country)
                });
            }

            model.Add(sectorGroup);
            model.Add(countryGroup);
            return model;

            //return rebRepo.GetFilterGroups(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/adviser/model/profiles")]
        public List<ModelProfile> GetAllModelProfiles()
        {
            return new List<ModelProfile>(){
                new ModelProfile { 
                    profileId = RebalanceModelProfile.Aggressive.ToString(),
                    profileName = edisRepo.GetEnumDescription(RebalanceModelProfile.Aggressive)
                },
                new ModelProfile {
                    profileId = RebalanceModelProfile.Defensive.ToString(),
                    profileName = edisRepo.GetEnumDescription(RebalanceModelProfile.Defensive)
                },
                new ModelProfile{
                    profileId = RebalanceModelProfile.Neutral.ToString(),
                    profileName = edisRepo.GetEnumDescription(RebalanceModelProfile.Neutral)
                }
            };

            //return rebRepo.GetAllModelProfiles();
        }
    }
}
