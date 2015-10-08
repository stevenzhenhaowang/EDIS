app.controller("clientResearchRebalanceController", ["clientResearchRebalanceServices", "$scope", function (service, $scope) {

    service().getExistingModels().query(function (data) {
        $scope.existingModels = data;
    })
    $scope.selectModel = function (modelId) {
        service().getAndSelectModelProfile(modelId).then(function (data) {
            $scope.selectedModel = data;
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

}]);
