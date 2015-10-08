/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />
(function () {
    var app = angular.module("EDIS");
    app.controller("adviserPortfolioFIViewController", function ($scope) {

    });
    app.controller("adviserPortfolioFIBriefInfoController", ["$scope", "adviserPortfolioFIGeneralInfo", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })

    }]);


    app.controller("adviserPortfolioFICashflowBarchartController", ["$scope", "adviserPortfolioFICashflowDetails", function ($scope, DBContext) {
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
    app.controller("adviserPortfolioFICashflowIncomeSpecificController", ["$scope", "adviserPortfolioFICashflowInfoIncomeSpecific", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("adviserPortfolioFIRatingController", ["$scope", "adviserPortfolioFIRatingInfo", function ($scope, DBContext) {
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
    app.controller("adviserPortfolioFIQuickStatsController", ["$scope", "adviserPortfolioFIQuickStats", function ($scope, DBContext) {
        DBContext().query(function (data) {
            $scope.data = data;
        })
    }])

    app.controller("adviserPortfolioFIDiversificationController", ["$scope", "adviserPortfolioFIDiversificationInfo", function ($scope, DBContext) {
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
    app.controller("adviserPortfolioFIStatisticsController", ["$scope", "adviserPortfolioFIStatistics", function ($scope, DBContext) {
        DBContext().query(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("adviserPortfolioFIPriceChartController", ["$scope", "adviserPortfolioFIPriceChartData", function ($scope, DBContext) {
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
    app.controller("adviserPortfolioFIIncomeProfileController", ["$scope", "adviserPortfolioFIIncomeProfiles", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })

    }])
})();