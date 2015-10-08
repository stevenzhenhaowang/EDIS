/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />


(function () {
    var app = angular.module("EDIS");
    app.controller("adviserPortfolioCTDViewController", function ($scope) {

    });
    app.controller("adviserPortfolioCTDBriefInfoController", ["$scope", "adviserPortfolioCTDGeneralInfo", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })

    }]);
    app.controller("adviserPortfolioCTDCashflowController", ["$scope", "adviserPortfolioCTDCashflowDetails", function ($scope, DBContext) {
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
    app.controller("adviserPortfolioCTDRatingController", ["$scope", "adviserPortfolioCTDRatingInfo", function ($scope, DBContext) {
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
    app.controller("adviserPortfolioCTDCashflowDepositSpecificController", ["$scope", "adviserPortfolioCTDCashflowInfoDepositSpecific", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("adviserPortfolioCTDDiversificationController", ["$scope", "adviserPortfolioCTDDiversificationInfo", function ($scope, DBContext) {
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
    app.controller("adviserPortfolioCTDStatisticsController", ["$scope", "adviserPortfolioCTDStatistics", function ($scope, DBContext) {
        DBContext().query(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("adviserPortfolioCTDPriceChartController", ["$scope", "adviserPortfolioCTDPriceChartData", function ($scope, DBContext) {
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
    app.controller("adviserPortfolioCTDDepositProfilesController", ["$scope", "adviserPortfolioCTDDepositProfiles", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })

    }])
})();