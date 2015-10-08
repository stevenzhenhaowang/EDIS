app.controller("adviserResearchRebalanceController", ["researchRebalanceServices", "$scope", "$modal", function (service, $scope, $modal) {
    $scope.currentModels = service().getCurrentModels();

    service().getExistingModels().query(function (data) {
        $scope.existingModels = data;
    });




    $scope.selectModel = function (modelId) {

        service().getAndSelectModelProfile(modelId).then(function (data) {
            $scope.selectedModel = data;
            $scope.currentModels = service().getCurrentModels();
        })



    };
    function getBarOptions() {
        if ($scope.selectedModel === null || $scope.selectedModel === undefined) {
            return {};
        }
        return {
            title: {
                text: "Diversification Bar Chart"
            },
            legend: {
                visible: false
            },
            seriesDefaults: {
                type: "column"
            },
            series: [{ field: "portfolioWeighting", name: "Portfolio", categoryField: "group" },
            { field: "modelWeighting", name: "Model", categoryField: "group" }],
            dataSource: {
                data: $scope.selectedModel.diversificationData
            },
            categoryAxis: {
                field: "group",
                name: "Type",
                labels: {
                    rotation: -45
                }
            },
            valueAxis: {
                labels: {
                    format: "c2",
                }
            },
            tooltip: {
                visible: true,
                format: "{0}",
                template: " #= kendo.format('{0:N2}',value) #%"
            }
        };
    }
    $scope.barOptions = getBarOptions();
    $scope.$watch("selectedModel", function () {
        $scope.barOptions = getBarOptions();
    });
    $scope.add = function () {
        var modalInstance = $modal.open({
            templateUrl: "addNewModel",
            controller: "addNewModelController",
            resolve: {
                rebalanceService: service
            },
            backdrop: true
        });

        modalInstance.result.then(function (result) {
            $scope.selectModel(result.reason);
        });
    };
    $scope.remove = function (model) {
        if (angular.isDefined($scope.selectedModel) && $scope.selectedModel.modelId === model.modelId) {
            $scope.selectedModel = null;
        }
        service().removeModel(model);
    }
}]);
app.controller("addNewModelController", function ($scope, $modalInstance, rebalanceService, adviserGetClientGroups) {
    rebalanceService.getExistingModels().query(function (data) {
        $scope.existingModels = data;
    })
    adviserGetClientGroups().then(function (data) {
        $scope.groups = data;
    })

    var loading = false;
    $scope.newModel = {};
    $scope.newModel.selectedParameters = [];
    rebalanceService.getParameterFilters().query(function (data) {
        $scope.parameterTypes = data;
    })
    $scope.createModeOn = false;
    $scope.close = function () {
        $modalInstance.dismiss("cancel");
    }
    $scope.save = function () {
        var modelId = "";
        if ($scope.createModeOn) {
            var model = {
                profileId: $scope.newModel.profileId,
                name: $scope.newModel.modelName,
                clientGroupId:$scope.newModel.clientGroupId,
                parameters: $scope.newModel.selectedParameters
            };
            loading = true;
            //Ajax call will be performed here to post new model and retrieve modelId.
            rebalanceService.addNewModel().save(model, function (data) {

                loading = false;
                $modalInstance.close({ reason: data.modelId });

            });

        } else {
            $modalInstance.close({ reason: $scope.selectedModelId });
        }
    }
    rebalanceService.getAllProfiles().query(function (data) {
        $scope.profiles = data;
    })
    $scope.changeTemplateSelection = function () {
        if (angular.isDefined($scope.newModel.existingModelId)
            && $scope.newModel.existingModelId !== null
            && $scope.newModel.existingModelId !== "") {


            rebalanceService.getModelProfile().get({ modelId: $scope.newModel.existingModelId }, function (data) {
                var existingModel = data;
                $scope.newModel = {};
                $scope.newModel.selectedParameters = [];
                $scope.newModel.existingModelId = data.modelId;
                $scope.newModel.profileId = existingModel.profile.profileId;
                $scope.newModel.modelName = existingModel.modelName;

                for (var i = 0; i < existingModel.templateDetails.itemParameters.length; i++) {
                    var record = existingModel.templateDetails.itemParameters[i];
                    var item = {
                        parameterName: record.itemName,
                        parameterId: record.id,
                        weighting: record.currentWeighting,
                        identityMetaKey: record.identityMetaKey
                    };
                    $scope.newModel.selectedParameters.push(item)
                }

            })
        } else if (angular.isDefined($scope.newModel.existingModelId)
            || $scope.newModel.existingModelId !== null
            || $scope.newModel.existingModelId === "") {
            $scope.newModel.profileId = "";
            $scope.newModel.modelName = "";
        }
    }
    $scope.actionButtonsActive = function () {
        if ($scope.newModel.selectedParameters.length > 0 && !loading) {
            return true;
        }
        return false;
    }
    $scope.parameterGroupChanged = function () {
        rebalanceService.getParameters().query({ groupId: $scope.newModel.parameterGroup.groupId }, function (data) {
            $scope.newModel.parameters = data;
        });
    }
    $scope.addParameter = function () {
        if (!angular.isDefined($scope.newModel.parameter) || $scope.newModel.parameter === null || $scope.newModel.parameter === "") {

            var item = {
                parameterName: $scope.newModel.parameterGroup.groupName,
                parameterId: $scope.newModel.parameterGroup.groupId,
                weighting: $scope.newModel.parameterGroup.currentWeighting,
                identityMetaKey: $scope.newModel.parameterGroup.identityMetaKey
            };
            $scope.newModel.selectedParameters.push(item);
        } else {
            var item = {
                parameterName: $scope.newModel.parameter.itemName,
                parameterId: $scope.newModel.parameter.id,
                weighting: $scope.newModel.parameter.currentWeighting,
                identityMetaKey: $scope.newModel.parameter.identityMetaKey,
            }
            $scope.newModel.selectedParameters.push(item);
        }
    }
    $scope.removeSelectedParameter = function (item) {
        $scope.newModel.selectedParameters.splice($scope.newModel.selectedParameters.indexOf(item), 1);
    }
});