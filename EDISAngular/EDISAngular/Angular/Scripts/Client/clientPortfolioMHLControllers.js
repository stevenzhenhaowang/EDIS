/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />


(function () {
    var app = angular.module("EDIS");
    app.controller("clientPortfolioMHLViewController", function ($scope) {

    });
    app.controller("clientPortfolioMHLBriefInfoController", ["$scope", "clientPortfolioMHLGeneralInfo", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })

    }]);
    app.controller("clientPortfolioMHLCashflowController", ["$scope", "clientPortfolioMHLCashflowDetails", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
            $scope.dataSource = new kendo.data.DataSource({
                data: $scope.data.data,
                sort: {
                    field: "date",
                    dir: "asc"
                }
            });
        })

    }]);
    app.controller("clientPortfolioMHLRatingController", ["$scope", "clientPortfolioMHLRatingInfo", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
            $scope.mainOptions = {
                title: {
                    text: "Portfolio Rating"
                },
                legend: {
                    position: "top"
                },
                seriesDefaults: {
                    type: "column"
                },
                series: [{
                    name: "Risk",
                    data: [$scope.data.risk]
                }, {
                    name: "Suitability",
                    data: [$scope.data.suitability]
                }, {
                    name: "% of Asset Not Suited",
                    data: [$scope.data.notSuited]
                }],

                valueAxis: {
                    labels: {
                        format: "{0}"
                    },
                    line: {
                        visible: false
                    },
                    axisCrossingValue: 0
                },
                tooltip: {
                    visible: true,
                    format: "{0}",
                    template: "#= series.name # rating is #= value #"
                }
            };
        })

    }])
    app.controller("clientPortfolioMHLStatisticsController", ["$scope", "clientPortfolioMHLStatistics", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("clientPortfolioMHLInvestmentProfilesController", ["$scope", "clientPortfolioMHLInvestmentProfiles", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }])
    app.controller("clientPortfolioMHLDiversificationController", ["$scope", "clientPortfolioMHLInvestmentProfiles", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data.data;
            $scope.countries = (function () {
                var result = [];
                var container = [];
                for (var i = 0; i < $scope.data.length; i++) {
                    if (result.indexOf($scope.data[i].country) === -1) {
                        result.push($scope.data[i].country);
                    }
                }
                for (var i = 0; i < result.length; i++) {
                    container.push({ name: result[i], checked: false });
                }
                return container;
            })();
            $scope.states = (function () {
                var result = [];
                var container = [];
                for (var i = 0; i < $scope.data.length; i++) {
                    if (result.indexOf($scope.data[i].state) === -1) {
                        result.push($scope.data[i].state);
                    }
                }
                for (var i = 0; i < result.length; i++) {
                    container.push({ name: result[i], checked: false });
                }
                return container;
            })();
            $scope.types = (function () {
                var result = [];
                var container = [];
                for (var i = 0; i < $scope.data.length; i++) {
                    if (result.indexOf($scope.data[i].type) === -1) {
                        result.push($scope.data[i].type);
                    }
                }
                for (var i = 0; i < result.length; i++) {
                    container.push({ name: result[i], checked: false });
                }
                return container;
            })();
            $scope.properties = function () {
                var checked = false;
                var result = [];
                for (var i = 0; i < $scope.countries.length; i++) {
                    if ($scope.countries[i].checked) {
                        checked = true;
                        for (var j = 0; j < $scope.data.length; j++) {
                            if ($scope.data[j].country === $scope.countries[i].name && $scope.countries[i].checked) {
                                result.push($scope.data[j]);
                            }
                        }
                    }
                }
                for (var i = 0; i < $scope.states.length; i++) {
                    if ($scope.states[i].checked) {
                        checked = true;
                        for (var j = 0; j < $scope.data.length; j++) {
                            if ($scope.data[j].state === $scope.states[i].name && $scope.states[i].checked && result.indexOf($scope.data[j]) === -1) {
                                result.push($scope.data[j]);
                            }
                        }
                    }
                }
                for (var i = 0; i < $scope.types.length; i++) {
                    if ($scope.types[i].checked) {
                        checked = true;
                        for (var j = 0; j < $scope.data.length; j++) {
                            if ($scope.data[j].type === $scope.types[i].name && $scope.types[i].checked
                                && result.indexOf($scope.data[j]) === -1) {
                                result.push($scope.data[j]);
                            }
                        }
                    }
                }

                if (checked) {
                    return result;
                } else {
                    return $scope.data;
                }
            };

        })
    }])
    app.controller("clientPortfolioMHLCashflowPropertySpecificController", ["$scope", "clientPortfolioMHLCashflowInfoPropertySpecific", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }]);

})();