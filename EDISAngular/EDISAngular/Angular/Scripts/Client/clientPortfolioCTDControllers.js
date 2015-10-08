/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />


(function () {
    var app = angular.module("EDIS");
    app.controller("clientPortfolioCTDViewController", function ($scope) {

    });
    app.controller("clientPortfolioCTDBriefInfoController", ["$scope", "clientPortfolioCTDGeneralInfo", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("clientPortfolioCTDCashflowController", ["$scope", "clientPortfolioCTDCashflowDetails", function ($scope, DBContext) {
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
    app.controller("clientPortfolioCTDRatingController", ["$scope", "clientPortfolioCTDRatingInfo", function ($scope, DBContext) {
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
    app.controller("clientPortfolioCTDCashflowDepositSpecificController", ["$scope", "clientPortfolioCTDCashflowInfoDepositSpecific", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("clientPortfolioCTDDiversificationController", ["$scope", "clientPortfolioCTDDiversificationInfo", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
            $scope.barOptions = {
                title: {
                    text: "Diversification Bar Chart"
                },
                legend: {
                    visible: false
                },
                seriesDefaults: {
                    type: "column"
                },
                series: [{ field: "value", name: "Value", spacing: 8 }],
                dataSource: {
                    data: $scope.data.data
                },
                categoryAxis: {
                    field: "name",
                    name: "Name",
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
                    template: " $#= value #"
                }
            };

            $scope.pieSource = {
                data: $scope.data.data
            }
        })
    }])

    app.controller("clientPortfolioCTDStatisticsController", ["$scope", "clientPortfolioCTDStatistics", function ($scope, DBContext) {
        DBContext().query(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("clientPortfolioCTDPriceChartController", ["$scope", "clientPortfolioCTDPriceChartData", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
            $scope.datasource = new kendo.data.DataSource({
                data: $scope.data.data,
                sort: {
                    field: "date",
                    dir: "asc"
                }
            })

        })

    }]);
    app.controller("clientPortfolioCTDDepositProfilesController", ["$scope", "clientPortfolioCTDDepositProfiles", function ($scope, DBContext) {
         DBContext().get(function (data) {
             $scope.data = data;
        })

    }])



})();