/// <reference path="C:\Users\ahksysuser03\Documents\Visual Studio 2013\Projects\EDISAngular\EDISAngular\Scripts/angular.js" />

angular.module("EDIS")
.factory("researchRebalanceServices", function ($http, $resource, $filter, $q, AppStrings) {
    var currentModels = [];//as in memory, displayed in tabs, initially empty.
    var existingModels = [];//as in db, displayed in existing models dropdown list
    ///if from tab, should have already been retrieved, otherwise, retrieve from db.
    //May return null/undefined result if modelId is not supplied properly. 
    function getAndSelectModelProfile(modelId) {
        var deferred = $q.defer();
        var found = false;
        for (var i = 0; i < currentModels.length&&!found; i++) {
            if (currentModels[i].modelId === modelId) {
                deferred.resolve(currentModels[i]);
                found = true;
            }
        }
        for (var i = 0; i < existingModels.length&& !found; i++) {
            if (existingModels[i].modelId === modelId) {
                currentModels.push(existingModels[i]);
                deferred.resolve(existingModels[i]);
            }
        }
        if (!found) {
            $resource(AppStrings.EDIS_IP + "api/adviser/model").get({ modelId: modelId }, function (data) {
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
            getExistingModels: function () { return $resource(AppStrings.EDIS_IP + "api/adviser/models"); },//resource
            getAndSelectModelProfile: getAndSelectModelProfile,//promise
            getAllProfiles: function () {
                return $resource(AppStrings.EDIS_IP + "api/adviser/model/profiles");
            },//resource
            getModelProfile: function () { return $resource(AppStrings.EDIS_IP + "api/adviser/model"); },//resource, needs id
            getParameterFilters: function () { return $resource(AppStrings.EDIS_IP + "api/adviser/model/filters"); },//resource
            getParameters: function () { return $resource(AppStrings.EDIS_IP + "api/adviser/model/parameters"); },//resource, needs id
            addNewModel: function () { return $resource(AppStrings.EDIS_IP + "api/adviser/model/create"); },//resource, needs object
            removeModel: removeModel//resource, needs id
        };
    };
});