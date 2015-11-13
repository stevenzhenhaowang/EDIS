
//START OF research analysis services
app.factory("clientResearchAnalysisDBService", function ($http, $resource, AppStrings) {
    var stockData = [];
    var banks = {
        metaData: {
            compareDetailsInvestmentAnalysisProperties: [
                {
                    propertyName: "generalInformation",
                    displayName: "General Information",
                    properties: [
                        {
                            propertyName: "currentPrice",
                            displayName: "Current Price"
                        },
                        {
                            propertyName: "priceChangeAmount",
                            displayName: "Price Change $"
                        }
                    ]
                },
                {
                    propertyName: "recommendation",
                    displayName: "Recommendation",
                    properties: [
                        {
                            propertyName: "currentShortTermRecommendation",
                            displayName: "Current Short Term Recommendation"
                        },
                        {
                            propertyName: "currentLongTermRecommendation",
                            displayName: "Current Long Term Recommendation"
                        }
                    ]
                }
            ]
        },
        data: []
    };
    var storedCompany = [];
    return function () {

        var companyList = function () {
            return $resource(AppStrings.EDIS_IP + "api/adviser/companyList");
        };
        var getCompanyProfile = function () {
            return $resource(AppStrings.EDIS_IP + "api/adviser/research/companyProfile");
        }
        var getUserCompanyList = function () {
            return storedCompany;
        }
        var addCompanyToUserList = function (id) {

            var found = false;
            for (var i = 0; i < storedCompany.length; i++) {
                if (storedCompany[i].id === id) {
                    found = true;
                }
            }
            if (!found) {
                getCompanyProfile().get({ companyId: id }, function (data) {
                    if (angular.isDefined(data)) {
                        storedCompany.push(data);
                    }
                })



            }

        }


        var remove = function (item) {
            for (var i = 0; i < storedCompany.length; i++) {
                if (storedCompany[i].id === item.id) {
                    storedCompany.splice(i, 1);
                }
            }
        }

        var getCompareMetaData = function () {
            return banks.metaData;
        }
        var getCompareInvestmentData = function () {
            return stockData;
        }




        return {
            getUserCompanyList: getUserCompanyList,
            addCompanyToUserList: addCompanyToUserList,
            getCompanyProfile: getCompanyProfile,
            getAllCompanyList: companyList,
            remove: remove,
            getCompareMetaData: getCompareMetaData,
            getCompareInvestmentData: getCompareInvestmentData
        }
    }

});
//End of research analysis service

angular.module("EDIS")
.factory("clientResearchRebalanceServices", function (AppStrings, $resource, $filter, $q) {
    var currentModels = [];//as in memory, displayed in tabs, initially empty.
    var existingModels = [];//as in db, displayed in existing models dropdown list
    ///if from tab, should have already been retrieved, otherwise, retrieve from db.
    //May return null/undefined result if modelId is not supplied properly. 
    function getAndSelectModelProfile(modelId) {
        var deferred = $q.defer();
        var found = false;
        for (var i = 0; i < currentModels.length && !found; i++) {
            if (currentModels[i].modelId === modelId) {
                deferred.resolve(currentModels[i]);
                found = true;
            }
        }
        for (var i = 0; i < existingModels.length && !found; i++) {
            if (existingModels[i].modelId === modelId) {
                currentModels.push(existingModels[i]);
                deferred.resolve(existingModels[i]);
            }
        }
        if (!found) {
            $resource(AppStrings.EDIS_IP + "api/client/model/details").get({ modelId: modelId }, function (data) {
                currentModels.push(data)
                deferred.resolve(data);
            })
        }
        return deferred.promise;
    }
    function getCurrentModels() {
        return currentModels;
    }
    function removeModel(model) {
        currentModels.splice(currentModels.indexOf(model), 1);
        existingModels.splice(existingModels.indexOf(model), 1);
        $http.post(AppStrings.EDIS_IP + "api/adviser/model/remove", { modelId: model.modelId });
    }
    return function () {
        return {
            getCurrentModels: getCurrentModels,
            getExistingModels: function () { return $resource(AppStrings.EDIS_IP + "api/client/models"); },//resource   .....
            getAndSelectModelProfile: getAndSelectModelProfile,//promise
            getAllProfiles: function () {
                return $resource(AppStrings.EDIS_IP + "api/adviser/model/profiles");
            },//resource
            getModelProfile: function () { return $resource(AppStrings.EDIS_IP + "api/adviser/model"); },//resource, needs id   ......
            getParameterFilters: function () { return $resource(AppStrings.EDIS_IP + "api/adviser/model/filters"); },//resource
            getParameters: function () { return $resource(AppStrings.EDIS_IP + "api/adviser/model/parameters"); },//resource, needs id        //, clientGroupId : clientGroupId 
            addNewModel: function () { return $resource(AppStrings.EDIS_IP + "api/client/model/create"); },//resource, needs object ....
            removeModel: removeModel//resource, needs id
        };
    };
});