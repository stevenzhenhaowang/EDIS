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
using Domain.Portfolio.Values.Weighting;
using Domain.Portfolio.AggregateRoots.Liability;
using Domain.Portfolio.Values.Cashflow;

namespace EDISAngular.APIControllers
{
    public class RebalanceController : ApiController
    {
        private EdisRepository edisRepo;
        //private RebalanceRepository rebRepo;

        public RebalanceController()
        {
            //rebRepo = new RebalanceRepository();
            edisRepo = new EdisRepository();
        }

        [HttpGet, Route("api/adviser/models")]
        public List<RebalanceModel> GetAllModelsForAdviser()
        {
            return GenerateReblanaceModel(edisRepo.GetRebalanceModelById(User.Identity.GetUserId()));
        }

        public List<RebalanceModel> GenerateReblanaceModel(List<Domain.Portfolio.Rebalance.RebalanceModel> savedModels) {
            List<RebalanceModel> models = new List<RebalanceModel>();

            foreach (var savedModel in savedModels) {
                List<TemplateDetailsItemParameter> parameters = new List<TemplateDetailsItemParameter>();
                foreach (var parameter in savedModel.TemplateDetailsItemParameters) {
                    parameters.Add(new TemplateDetailsItemParameter {
                        id = parameter.EquityId,
                        currentWeighting = parameter.CurrentWeighting == null ? 0 : (double)parameter.CurrentWeighting,
                        itemName = parameter.ItemName,
                    });
                };
                models.Add(new RebalanceModel {
                    modelId = savedModel.ModelId,
                    profile = new ModelProfile { profileId = savedModel.ProfileId.ToString(), profileName = edisRepo.GetEnumDescription((RebalanceModelProfile)savedModel.ProfileId) },
                    modelName = savedModel.ModelName,
                    itemParameters = parameters,
                });
            }

            return models;
        }

        [HttpGet, Route("api/client/models")]
        public List<RebalanceModel> GetAllModelsForClient() {
            return GenerateReblanaceModel(edisRepo.GetRebalanceModelById(User.Identity.GetUserId()));
        }

        [HttpGet, Route("api/adviser/model")]
        public RebalanceModel GetModelForId(string modelId)
        {
            var savedModel = edisRepo.GetRebalanceModelByModelId(modelId);

            List<TemplateDetailsItemParameter> parameters = new List<TemplateDetailsItemParameter>();
            foreach (var parameter in savedModel.TemplateDetailsItemParameters)
            {
                parameters.Add(new TemplateDetailsItemParameter
                {
                    id = parameter.EquityId,
                    itemName = parameter.ItemName,
                    identityMetaKey = parameter.identityMetaKey,
                    currentWeighting = parameter.CurrentWeighting == null ? 0 : (double)parameter.CurrentWeighting,
                });
            }


            return new RebalanceModel
            {
                modelId = savedModel.ModelId,
                profile = new ModelProfile { profileId = savedModel.ProfileId.ToString(), profileName = edisRepo.GetEnumDescription((RebalanceModelProfile)savedModel.ProfileId) },
                modelName = savedModel.ModelName,
                clientGroupId = savedModel.ClientGroupId,
                itemParameters = parameters,
            };
            //return rebRepo.GetModelById(modelId);
        }


        public RebalanceModel GetRebalanceModel(List<AssetBase> allAssets, Domain.Portfolio.Rebalance.RebalanceModel savedModel, ClientGroup clientGroup = null, Client client = null) {
            var weightings = allAssets.OfType<Equity>().Cast<AssetBase>().ToList().GetAssetWeightings();
            var totalMarketValue = allAssets.GetTotalMarketValue();

            List<TemplateDetailsItemParameter> parameters = new List<TemplateDetailsItemParameter>();
            List<DiversificationDatas> diversificationDatas = new List<DiversificationDatas>();
            List<RebalanceDataAnalysisModel> rebalanceDataAnalysisModel = new List<RebalanceDataAnalysisModel>();
            BalanceSheetAgainstModel balanceSeetAgainsModel = new BalanceSheetAgainstModel() { data = new List<BalanceSheetAgainstModelData>() };
            RegionalComparisonModel regionalComparisonModel = new RegionalComparisonModel() { data = new List<RegionalComparisonData>() };
            SectorialComparisonModel sectorialComparisonModel = new SectorialComparisonModel() { data = new List<SectorialComparisonData>() };
            MonthlyCashflowComparison monthlyCashflowComparison = new MonthlyCashflowComparison() { data = new List<MonthlyCashflowComparisonData>() };
            List<TransactionCostData> transactionCostData = new List<TransactionCostData>();

            foreach (var parameter in savedModel.TemplateDetailsItemParameters) {
                //DiversificationDatas
                string[] metaKeys = parameter.identityMetaKey.Split('#');

                List<Equity> equitiesForGroup = new List<Equity>();
                if (metaKeys[0] == edisRepo.GetEnumDescription(RebalanceClassificationType.Sectors)) {
                    equitiesForGroup = edisRepo.GetAllEquitiesBySectorName(metaKeys[1]);
                } else if (metaKeys[0] == edisRepo.GetEnumDescription(RebalanceClassificationType.Countries)) {
                    equitiesForGroup = edisRepo.GetAllEquitiesByResearchStringValue(metaKeys[1]);
                }

                double currentWeighting = 0;
                if (metaKeys.Length == 2) {
                    currentWeighting = getCurrentWeightingForEquityGroup(equitiesForGroup, weightings);

                    //multiple equities 


                } else {
                    currentWeighting = getCurrentWeightingForEquity(parameter.EquityId, weightings);

                    //RebalanceDataAnalysisModel
                    List<Equity> currentEquities = null;
                    if (clientGroup != null) {
                        currentEquities = edisRepo.GetEquityForGroupAccountSync(parameter.EquityId, clientGroup);
                    } else{
                        currentEquities = edisRepo.GetEquityForClientAccountSync(parameter.EquityId, client);
                    }

                    int numberOfUnit = 0;
                    double value = 0;
                    double totalCost = 0;
                    double rating = 0;

                    foreach (var currentEquity in currentEquities) {
                        numberOfUnit += (int)currentEquity.TotalNumberOfUnits;
                        value += currentEquity.GetTotalMarketValue();
                        totalCost += currentEquity.GetCost().Total;

                        switch (currentEquity.EquityType) {
                            case EquityTypes.AustralianEquity:
                                rating += ((AustralianEquity)currentEquity).GetRating().TotalScore;
                                break;
                            case EquityTypes.InternationalEquity:
                                rating += ((InternationalEquity)currentEquity).GetRating().TotalScore;
                                break;
                        }
                    }

                    Equity selectedEquity = edisRepo.getEquityById(parameter.EquityId);

                    double reValue = currentWeighting * parameter.CurrentWeighting == 0 ? (double)(totalMarketValue * parameter.CurrentWeighting) : (double)(value / currentWeighting * parameter.CurrentWeighting);//(totalMarketValue * parameter.CurrentWeighting) == null ? 0 : (double)(totalMarketValue * parameter.CurrentWeighting);
                    int reUnit = reValue == 0 ? 0 : (int)(reValue / selectedEquity.LatestPrice);
                    double reProfitAndLoss = reValue - totalCost;
                    double differences = (reValue - value) / value;

                    var analysisGroup = rebalanceDataAnalysisModel.SingleOrDefault(a => a.groupName == metaKeys[1]);
                    if (analysisGroup == null) {
                        analysisGroup = new RebalanceDataAnalysisModel {
                            groupName = metaKeys[1],
                            data = new List<RebalanceDataAnalysisData>(),
                        };
                        rebalanceDataAnalysisModel.Add(analysisGroup);
                    }
                    var index = rebalanceDataAnalysisModel.IndexOf(analysisGroup);
                    rebalanceDataAnalysisModel[index].data.Add(new RebalanceDataAnalysisData {
                        ticker = selectedEquity.Ticker,
                        name = selectedEquity.Name,
                        currentPrice = selectedEquity.LatestPrice,
                        currentExposure = new RebalanceDataAnalysisDataItem {
                            units = numberOfUnit,
                            value = value,
                            profitAndLoss = value - totalCost
                        },
                        rebalance = new RebalanceDataAnalysisDataItem {
                            value = reValue,
                            units = reUnit,
                            profitAndLoss = reProfitAndLoss
                        },
                        advantageousAndDisadvantageous = new RebalanceDataAnalysisAdvantageDisadvantage {
                            suitability = currentWeighting * rating,
                            differences = differences
                            //differenceToTarget = ?，
                            //target = ?
                        }

                    });


                    //BalanceSheetAgainstModel

                    var balanceSheet = balanceSeetAgainsModel.data.SingleOrDefault(a => a.groupName == metaKeys[1]);
                    if (balanceSheet == null) {
                        balanceSheet = new BalanceSheetAgainstModelData {
                            groupName = metaKeys[1],
                            items = new List<BalanceSheetAgainstModelItem>()
                        };
                        balanceSeetAgainsModel.data.Add(balanceSheet);
                    }
                    var balanSheetIndex = balanceSeetAgainsModel.data.IndexOf(balanceSheet);
                    balanceSeetAgainsModel.data[balanSheetIndex].items.Add(new BalanceSheetAgainstModelItem {
                        current = value,
                        currentWeighting = currentWeighting,
                        proposed = reValue,
                        proposedWeighting = parameter.CurrentWeighting == null ? 0 : (double)parameter.CurrentWeighting,
                        difference = differences,
                        itemName = selectedEquity.Name
                    });


                    if (metaKeys[0] == edisRepo.GetEnumDescription(RebalanceClassificationType.Countries)) {
                        //RegionalComparisonModel
                        var regionModel = regionalComparisonModel.data.SingleOrDefault(a => a.groupName == metaKeys[1]);
                        if (regionModel == null) {
                            regionModel = new RegionalComparisonData {
                                groupName = metaKeys[1],
                                items = new List<RegionalComparisonItem>()
                            };
                            regionalComparisonModel.data.Add(regionModel);
                        }

                        var regionModelIndex = regionalComparisonModel.data.IndexOf(regionModel);
                        regionalComparisonModel.data[regionModelIndex].items.Add(new RegionalComparisonItem {
                            current = value,
                            currentWeighting = currentWeighting,
                            proposed = reValue,
                            proposedWeighting = parameter.CurrentWeighting == null ? 0 : (double)parameter.CurrentWeighting,
                            difference = differences,
                            itemName = selectedEquity.Name
                        });
                    } else if (metaKeys[0] == edisRepo.GetEnumDescription(RebalanceClassificationType.Sectors)) {

                        //SectoralComparisonModel
                        var sectorModel = sectorialComparisonModel.data.SingleOrDefault(a => a.groupName == metaKeys[1]);
                        if (sectorModel == null) {
                            sectorModel = new SectorialComparisonData {
                                groupName = metaKeys[1],
                                items = new List<SectorialComparisonItem>()
                            };
                            sectorialComparisonModel.data.Add(sectorModel);
                        }

                        var sectorModelIndex = sectorialComparisonModel.data.IndexOf(sectorModel);
                        sectorialComparisonModel.data[sectorModelIndex].items.Add(new SectorialComparisonItem {
                            current = value,
                            currentWeighting = currentWeighting,
                            proposed = reValue,
                            proposedWeighting = parameter.CurrentWeighting == null ? 0 : (double)parameter.CurrentWeighting,
                            difference = differences,
                            itemName = selectedEquity.Name
                        });
                    }


                    //TransactionCostData
                    var transactionData = transactionCostData.SingleOrDefault(t => t.assetClass == metaKeys[1]);
                    if (transactionData == null) {
                        transactionData = new TransactionCostData {
                            assetClass = metaKeys[1],
                            items = new List<TransactionCostDataItem>()
                        };
                        transactionCostData.Add(transactionData);
                    }
                    var transactionIndex = transactionCostData.IndexOf(transactionData);
                    transactionCostData[transactionIndex].items.Add(new TransactionCostDataItem {
                        buySell = reValue - value,
                        name = selectedEquity.Name,
                        profitLoss = reProfitAndLoss,
                        //transactionCost = ?,
                        netValue = reValue - value //- transactionCost
                    });

                }

                var sameGroup = diversificationDatas.FirstOrDefault(d => d.group == metaKeys[1]);
                if (sameGroup != null) {
                    sameGroup.modelWeighting += parameter.CurrentWeighting == null ? 0 : (double)parameter.CurrentWeighting;
                    sameGroup.portfolioWeighting += currentWeighting;


                } else {
                    diversificationDatas.Add(new DiversificationDatas {
                        group = metaKeys[1],
                        modelWeighting = parameter.CurrentWeighting == null ? 0 : (double)parameter.CurrentWeighting,
                        portfolioWeighting = currentWeighting
                    });
                }


                //TemplateDetailsItemParameter
                parameters.Add(new TemplateDetailsItemParameter {
                    id = parameter.EquityId,
                    itemName = parameter.ItemName,
                    identityMetaKey = parameter.identityMetaKey,
                    currentWeighting = parameter.CurrentWeighting == null ? 0 : (double)parameter.CurrentWeighting,
                });
            }


            for (int i = 0; i < balanceSeetAgainsModel.data.Count; i++) {
                for (int j = 0; j < balanceSeetAgainsModel.data[i].items.Count; j++) {
                    balanceSeetAgainsModel.data[i].current += balanceSeetAgainsModel.data[i].items[j].current;
                    balanceSeetAgainsModel.data[i].currentWeighting += balanceSeetAgainsModel.data[i].items[j].currentWeighting;
                    balanceSeetAgainsModel.data[i].proposed += balanceSeetAgainsModel.data[i].items[j].proposed;
                    balanceSeetAgainsModel.data[i].proposedWeighting += balanceSeetAgainsModel.data[i].items[j].proposedWeighting;
                }
                balanceSeetAgainsModel.data[i].difference = (balanceSeetAgainsModel.data[i].proposed - balanceSeetAgainsModel.data[i].current) / balanceSeetAgainsModel.data[i].current;

                balanceSeetAgainsModel.current += balanceSeetAgainsModel.data[i].current;
                balanceSeetAgainsModel.currentWeighting += balanceSeetAgainsModel.data[i].currentWeighting;
                balanceSeetAgainsModel.proposed += balanceSeetAgainsModel.data[i].proposed;
                balanceSeetAgainsModel.proposedWeighting += balanceSeetAgainsModel.data[i].proposedWeighting;
            }
            balanceSeetAgainsModel.difference = (balanceSeetAgainsModel.proposed - balanceSeetAgainsModel.current) / balanceSeetAgainsModel.current;


            for (int i = 0; i < regionalComparisonModel.data.Count; i++) {
                for (int j = 0; j < regionalComparisonModel.data[i].items.Count; j++) {
                    regionalComparisonModel.data[i].current += regionalComparisonModel.data[i].items[j].current;
                    regionalComparisonModel.data[i].currentWeighting += regionalComparisonModel.data[i].items[j].currentWeighting;
                    regionalComparisonModel.data[i].proposed += regionalComparisonModel.data[i].items[j].proposed;
                    regionalComparisonModel.data[i].proposedWeighting += regionalComparisonModel.data[i].items[j].proposedWeighting;
                }

                regionalComparisonModel.data[i].difference = (regionalComparisonModel.data[i].proposed - regionalComparisonModel.data[i].current) / regionalComparisonModel.data[i].current;

                regionalComparisonModel.current += regionalComparisonModel.data[i].current;
                regionalComparisonModel.currentWeighting += regionalComparisonModel.data[i].currentWeighting;
                regionalComparisonModel.proposed += regionalComparisonModel.data[i].proposed;
                regionalComparisonModel.proposedWeighting += regionalComparisonModel.data[i].proposedWeighting;
            }


            for (int i = 0; i < sectorialComparisonModel.data.Count; i++) {
                for (int j = 0; j < sectorialComparisonModel.data[i].items.Count; j++) {
                    sectorialComparisonModel.data[i].current += sectorialComparisonModel.data[i].items[j].current;
                    sectorialComparisonModel.data[i].currentWeighting += sectorialComparisonModel.data[i].items[j].currentWeighting;
                    sectorialComparisonModel.data[i].proposed += sectorialComparisonModel.data[i].items[j].proposed;
                    sectorialComparisonModel.data[i].proposedWeighting += sectorialComparisonModel.data[i].items[j].proposedWeighting;
                }

                sectorialComparisonModel.data[i].difference = (sectorialComparisonModel.data[i].proposed - sectorialComparisonModel.data[i].current) / sectorialComparisonModel.data[i].current;

                sectorialComparisonModel.current += sectorialComparisonModel.data[i].current;
                sectorialComparisonModel.currentWeighting += sectorialComparisonModel.data[i].currentWeighting;
                sectorialComparisonModel.proposed += sectorialComparisonModel.data[i].proposed;
                sectorialComparisonModel.proposedWeighting += sectorialComparisonModel.data[i].proposedWeighting;
            }

            for (int i = 0; i < transactionCostData.Count; i++) {
                for (int j = 0; j < transactionCostData[i].items.Count; j++) {

                    transactionCostData[i].buySell += transactionCostData[i].items[j].buySell;
                    transactionCostData[i].profitLoss += transactionCostData[i].items[j].profitLoss;
                    transactionCostData[i].transactionCost += transactionCostData[i].items[j].transactionCost;
                    transactionCostData[i].netValue += transactionCostData[i].items[j].netValue;
                    transactionCostData[i].extraDividend += transactionCostData[i].items[j].extraDividend;
                    transactionCostData[i].extraMER += transactionCostData[i].items[j].extraMER;
                }
                //transactionCostData[i].netValue += (transactionCostData[i].buySell + transactionCostData[i].transactionCost);
            }


            return new RebalanceModel {
                modelId = savedModel.ModelId,
                profile = new ModelProfile { profileId = savedModel.ProfileId.ToString(), profileName = edisRepo.GetEnumDescription((RebalanceModelProfile)savedModel.ProfileId) },
                modelName = savedModel.ModelName,
                itemParameters = parameters,
                diversificationData = diversificationDatas,
                rebalancedDataAnalysis = rebalanceDataAnalysisModel,
                balanceSheet = balanceSeetAgainsModel,
                sectorialData = sectorialComparisonModel,
                regionalData = regionalComparisonModel,
                transactionCost = transactionCostData
            };

        }


        [HttpGet, Route("api/adviser/model/details")]
        public RebalanceModel GetModelDetailForId(string modelId)
        {
            List<AssetBase> allAssets = new List<AssetBase>();
            var savedModel = edisRepo.GetRebalanceModelByModelId(modelId);

            ClientGroup clientGroup = edisRepo.getClientGroupByGroupId(savedModel.ClientGroupId);
            List<GroupAccount> accounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
            List<ClientAccount> clientAccounts = new List<ClientAccount>();
            clientGroup.GetClientsSync().ForEach(c => clientAccounts.AddRange(c.GetAccountsSync()));

            accounts.ForEach(a => allAssets.AddRange(a.GetAssetsSync()));
            clientAccounts.ForEach(a => allAssets.AddRange(a.GetAssetsSync()));

            return GetRebalanceModel(allAssets, savedModel, clientGroup);
        }


        [HttpGet, Route("api/client/model/details")]
        public RebalanceModel GetModelDetailForId_Client(string modelId) {
            List<AssetBase> allAssets = new List<AssetBase>();
            var savedModel = edisRepo.GetRebalanceModelByModelId(modelId);

            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);
            if (clientGroup.MainClientId == client.Id) {
                List<GroupAccount> groupAccounts = edisRepo.GetAccountsForClientGroupSync(clientGroup.ClientGroupNumber, DateTime.Now);
                List<ClientAccount> clientAccounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                groupAccounts.ForEach(a => allAssets.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList()));
                clientAccounts.ForEach(a => allAssets.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList()));

                return GetRebalanceModel(allAssets, savedModel, clientGroup);
            } else {
                List<ClientAccount> accounts = edisRepo.GetAccountsForClientSync(client.ClientNumber, DateTime.Now);
                accounts.ForEach(a => allAssets.AddRange(a.GetAssetsSync().OfType<AustralianEquity>().Cast<AssetBase>().ToList()));

                return GetRebalanceModel(allAssets, savedModel, null, client);
            }
        }

        [HttpPost,Route("api/adviser/model/create")]
        public IHttpActionResult CreateNewModel(RebalanceCreationModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                List<Domain.Portfolio.Rebalance.TemplateDetailsItemParameter> parameters = new List<Domain.Portfolio.Rebalance.TemplateDetailsItemParameter>();
                foreach (var parameter in model.parameters)
                {
                    parameters.Add(new Domain.Portfolio.Rebalance.TemplateDetailsItemParameter
                    {
                        EquityId = parameter.parameterId,
                        ItemName = parameter.parameterName,
                        CurrentWeighting = (double)parameter.weighting,
                        ModelId = model.modelId,
                        identityMetaKey = parameter.identityMetaKey
                    });
                };

                Domain.Portfolio.Rebalance.RebalanceModel newModel = new Domain.Portfolio.Rebalance.RebalanceModel
                {
                    ModelId = model.modelId,
                    AdviserId = User.Identity.GetUserId(),
                    ClientGroupId = model.clientGroupId,
                    ModelName = model.name,
                    ProfileId = (int)Enum.Parse(typeof(RebalanceModelProfile), model.profileId),
                    TemplateDetailsItemParameters = parameters
                };

                if (model.modelId != null)
                {
                    edisRepo.UpdateRebalanceModel(newModel);
                }
                else {
                    edisRepo.CreateRebalanceModel(newModel);    
                }

                return Ok();
                //rebRepo.CreateNewModel(model, User.Identity.GetUserId());
            }
            return BadRequest();
        }


        [HttpPost, Route("api/client/model/create")]
        public IHttpActionResult CreateNewModel_Client(RebalanceCreationModel model) {
            Client client = edisRepo.GetClientSync(User.Identity.GetUserId(), DateTime.Now);
            ClientGroup clientGroup = edisRepo.GetClientGroupSync(client.ClientGroupId, DateTime.Now);

            if (model != null) {
                List<Domain.Portfolio.Rebalance.TemplateDetailsItemParameter> parameters = new List<Domain.Portfolio.Rebalance.TemplateDetailsItemParameter>();
                foreach (var parameter in model.parameters) {
                    parameters.Add(new Domain.Portfolio.Rebalance.TemplateDetailsItemParameter {
                        EquityId = parameter.parameterId,
                        ItemName = parameter.parameterName,
                        CurrentWeighting = (double)parameter.weighting,
                        ModelId = model.modelId,
                        identityMetaKey = parameter.identityMetaKey
                    });
                };

                Domain.Portfolio.Rebalance.RebalanceModel newModel = new Domain.Portfolio.Rebalance.RebalanceModel {
                    ModelId = model.modelId,
                    ClientId = User.Identity.GetUserId(),
                    ClientGroupId = clientGroup.Id,
                    ModelName = model.name,
                    ProfileId = (int)Enum.Parse(typeof(RebalanceModelProfile), model.profileId),
                    TemplateDetailsItemParameters = parameters
                };

                if (model.modelId != null) {
                    edisRepo.UpdateRebalanceModel(newModel);
                } else {
                    edisRepo.CreateRebalanceModel(newModel);
                }

                return Ok();
                //rebRepo.CreateNewModel(model, User.Identity.GetUserId());
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
        public List<TemplateDetailsItemParameter> GetAllParamtersForGroup(string groupId)
        {
            List<TemplateDetailsItemParameter> parameters = new List<TemplateDetailsItemParameter>();

            if (edisRepo.GetAllSectorsSync().Contains(groupId))
            {
                foreach(var equity in edisRepo.GetAllEquitiesBySectorName(groupId)){
                    parameters.Add(new TemplateDetailsItemParameter {
                        id = equity.Id,
                        itemName = equity.Name,
                        identityMetaKey =  edisRepo.GetEnumDescription(RebalanceClassificationType.Sectors) + "#" + groupId + "#" + equity.Name
                    });
                }
            }
            else if (edisRepo.GetAllResearchStringValueByKey("Country").Contains(groupId)) {
                foreach (var equity in edisRepo.GetAllEquitiesByResearchStringValue(groupId))
                {
                    parameters.Add(new TemplateDetailsItemParameter
                    {
                        id = equity.Id,
                        itemName = equity.Name,
                        identityMetaKey =  edisRepo.GetEnumDescription(RebalanceClassificationType.Countries) + "#" + groupId + "#" + equity.Name
                    });
                }
            }else{
            }
            
            return parameters.OrderBy(p => p.itemName).ToList();
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
                classificationType = edisRepo.GetEnumDescription(RebalanceClassificationType.Countries),
                filters = new List<FilterGroupFilter>()
            };

            foreach(var sector in edisRepo.GetAllSectorsSync()){
                sectorGroup.filters.Add(new FilterGroupFilter()
                {
                    groupId = sector,
                    groupName = sector,
                    identityMetaKey = edisRepo.GetEnumDescription(RebalanceClassificationType.Sectors) + "#" + sector
                });
            }

            foreach (var country in edisRepo.GetAllResearchStringValueByKey("Country")) {
                countryGroup.filters.Add(new FilterGroupFilter()
                {
                    groupId = country,
                    groupName = country,
                    identityMetaKey =  edisRepo.GetEnumDescription(RebalanceClassificationType.Countries) + "#" + country
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

        public double getCurrentWeightingForEquity(string equityId, List<Weighting> weightings) {

            double weightingPercentage = 0;
            Weighting weighting = null;

            if (weightings.Count == 0) {
                weightingPercentage = 0;
            } else {
                weighting = weightings.SingleOrDefault(w => ((Equity)w.Weightable).Id == equityId);
                if (weighting == null) {
                    weightingPercentage = 0;
                } else {
                    weightingPercentage = weighting.Percentage;
                }
            }
            return weightingPercentage;
        }

        public double getCurrentWeightingForEquityGroup(List<Equity> equities, List<Weighting> weightings) {

            double weightingPercentage = 0;
            foreach (var equity in equities) {
                Weighting weighting = null;

                if (weightings.Count == 0) {
                    weightingPercentage += 0;
                } else {
                    weighting = weightings.SingleOrDefault(w => ((Equity)w.Weightable).Id == equity.Id);
                    if (weighting == null) {
                        weightingPercentage += 0;
                    } else {
                        weightingPercentage += weighting.Percentage;
                    }
                }
            }
            return weightingPercentage;
        }

        public List<Equity> GetEquitiesByGroup(string groupId) {
            if (edisRepo.GetAllSectorsSync().Contains(groupId)) {
                return edisRepo.GetAllEquitiesBySectorName(groupId);
            } else if (edisRepo.GetAllResearchStringValueByKey("Country").Contains(groupId)) {
                return edisRepo.GetAllEquitiesByResearchStringValue(groupId);
            } else {
                return null;
            }
        }

    }
}
