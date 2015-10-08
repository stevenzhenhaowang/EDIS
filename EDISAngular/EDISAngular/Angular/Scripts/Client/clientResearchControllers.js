/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />
app.controller("clientResearchAnalysisController", ["$scope", "clientResearchAnalysisDBService", "$modal", function ($scope, service, $modal) {
    $scope.userCompanyList = service().getUserCompanyList();
    service().getAllCompanyList().query(function (data) {
        $scope.allCompanyList = data;
    })


    $scope.currentCompanyDetails = $scope.userCompanyList.length > 0 ?
        service().getCompanyProfile($scope.userCompanyList[0].id) : null;

    $scope.selectCompany = function (item) {
        service().getCompanyProfile().get({ companyId: item.id }, function (data) {
            $scope.currentCompanyDetails = data;
            $scope.compareOn = false;
        })
    }
    $scope.panelCanShow = function () {
        return angular.isDefined($scope.currentCompanyDetails) && ($scope.currentCompanyDetails !== null);
    }
    $scope.canCompare = function () {
        if ($scope.userCompanyList.length > 0) {
            return true;
        }
        return false;
    }
    $scope.add = function () {
        var modalInstance = $modal.open({
            templateUrl: "addCompanyProfile",
            controller: "addNewCompanyProfileController",
            resolve: {
                companyList: function () {
                    return $scope.allCompanyList;
                }
            },
            backdrop: true
        });
        modalInstance.result.then(function (result) {
            service().addCompanyToUserList(result.reason);
            $scope.$broadcast("profileListChanged");
        });
    };
    $scope.remove = function (item) {
        service().remove(item);
        if (angular.isDefined($scope.currentCompanyDetails)
            && $scope.currentCompanyDetails !== null
            && $scope.currentCompanyDetails.id === item.id) {
            $scope.currentCompanyDetails = null;
            if ($scope.userCompanyList.length == 0) {
                compareOn = false;
            }
        }
        $scope.$broadcast("profileListChanged");
    }
    $scope.enableCompare = function () {
        $scope.compareOn = true;
    };

    $scope.highlight = function (item) {
        if ($scope.currentCompanyDetails.id === item.id && !compareOn) {
            return "active"
        } else {
            return "";
        }
    }
}])
app.controller("clientResearchCompanyProfileDetailsController", function ($scope) {

    $scope.dataSource = new kendo.data.DataSource({
        data: $scope.currentCompanyDetails.indexData,
        field: "date",
        dir: "asc"
    });

    $scope.$watchGroup(["currentCompanyDetails", "compareOn"], function () {
        $scope.dataSource = new kendo.data.DataSource({
            data: $scope.currentCompanyDetails.indexData,
            field: "date",
            dir: "asc"
        });
    })


});
app.controller("addNewCompanyProfileController", function ($scope, $modalInstance, companyList) {

    $scope.companyList = companyList;
    $scope.close = function () {
        $modalInstance.dismiss("cancel");
    }
    $scope.save = function () {
        $modalInstance.close({ reason: $scope.selectedCompanyId });
    }


});
app.controller("clientResearchCompareController", ["$scope", "clientResearchAnalysisDBService", "$timeout", function ($scope, service, $timeout) {
    $scope.userCompanyList = service().getUserCompanyList();
    $scope.details = [];
    for (var i = 0; i < $scope.userCompanyList.length; i++) {
        service().getCompanyProfile().get({ companyId: $scope.userCompanyList[i].id }, function (data) {
            $scope.details.push(data);
        })

    }

    $scope.compareMeta = service().getCompareMetaData();
    $scope.stockCompare = (function () {
        var result = [];
        for (var i = 0; i < $scope.userCompanyList.length; i++) {
            for (var j = 0; j < $scope.userCompanyList[i].indexData.length; j++) {
                var data = $scope.userCompanyList[i].indexData[j];
                var item = {
                    month: data.month,
                    date: data.date,
                };
                item[$scope.userCompanyList[i].id] = data.company;
                result.push(item);
            }

        }
        return result;
    })();
    $scope.dataSeries = (function () {
        var result = [];
        for (var i = 0; i < $scope.userCompanyList.length; i++) {
            result.push({
                field: $scope.userCompanyList[i].id,
                name: $scope.userCompanyList[i].name,
                categoryField: "month"
            })
        }
        return result;
    })();
    $scope.options = {
        legend: {
            position: "bottom"
        },
        seriesDefaults: {
            type: "area",
            area: {
                line: {
                    style: "smooth"
                }
            }
        },
        series: $scope.dataSeries,
        dataSource: {
            data: $scope.stockCompare
        },
        tooltip: {
            visible: true,
            format: '{0}%',
            template: '#= series.name #: #= value # %'
        }
    };


    $scope.$on("profileListChanged", function () {
        $scope.userCompanyList = service().getUserCompanyList();
        //$scope.stockCompare = service().getCompareInvestmentData();
        for (var i = 0; i < $scope.userCompanyList.length; i++) {

        }



        $scope.dataSeries = (function () {
            var result = [];
            for (var i = 0; i < $scope.userCompanyList.length; i++) {
                result.push({
                    field: $scope.userCompanyList[i].id,
                    name: $scope.userCompanyList[i].name,
                    categoryField: "month"
                })
            }
            return result;
        })();
        $scope.options = {
            legend: {
                position: "bottom"
            },
            seriesDefaults: {
                type: "area",
                area: {
                    line: {
                        style: "smooth"
                    }
                }
            },
            series: $scope.dataSeries,
            dataSource: {
                data: $scope.stockCompare
            },
            tooltip: {
                visible: true,
                format: '{0}%',
                template: '#= series.name #: #= value # %'
            }
        };


    })
}]);