﻿/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />


(function () {
    var app = angular.module("EDIS");
    app.controller("adviserPortfolioSummaryViewController", function ($scope) {

    });
    app.controller("adviserPortfolioSummaryFiguresController", ["$scope", "AdviserPortfolioSummaryDBService", function ($scope, DBContext) {
        DBContext.assetSummary().get(function (data) {
            $scope.data = data;
        });

    }]);
    app.controller("adviserPortfolioSummaryCashflowController", ["$scope", "AdviserPortfolioSummaryDBService", function ($scope, DBContext) {
        DBContext.cashflowSummary().get(function (data) {
            $scope.data = data;
        });
    }]);
    app.controller("adviserPortfolioAssetClassPerformanceController", ["$scope", "AdviserPortfolioSummaryDBService", function ($scope, DBContext) {
        DBContext.bestPerformingSummary().query(function (data) {
            $scope.bestPerforming = data;
        })
        DBContext.worstPerformingSummary().query(function (data) {
            $scope.worstPerforming = data;
        })

    }]);
    app.controller("adviserPortfolioBriefInfoController", ["$scope", "AdviserPortfolioSummaryDBService", function ($scope, DBContext) {
        DBContext.general().get(function (data) {
            $scope.data = data;
        })

    }]);
    app.controller("adviserPortfolioStockDataController", ["$scope", "AdviserPortfolioSummaryDBService", "$rootScope", function ($scope, DBContext, $rootScope) {
        $scope.$on('stockChart', function(event, args){
            DBContext.recentStockData().query(function (data) {
                $scope.data = data;
          
                $scope.dataSource = new kendo.data.DataSource({
                    //data: $scope.data,
                    data:data,  
                    sort: {
                        field: "year",
                        dir: "asc"
                    }
                });
            })
        })
    }]);


    //app.controller("adviserPortfolioStockDataController", ["$scope", "$http", "companySelectionService", "periodSelectionService", function ($scope, $http, companySelectionService, periodSelectionService) {

    //    $http.get(AppStrings.EDIS_IP + "api/PortfolioOverview/RecentStock", { companyId: companySelectionService.getCurrentCompanyId(), periodId: periodSelectionService.getCurrentPeriodId() }).success(function (response) {
    //        $scope.data = response;
    //        console.log(data);
    //        $scope.dataSource = new kendo.data.DataSource({
    //            //data: $scope.data,
    //            data: data,
    //            sort: {
    //                field: "year",
    //                dir: "asc"
    //            }
    //        });
    //    })

    //}]);
    app.controller("adviserPortfolioInvestmentPiechartController", ["$scope", "AdviserBusinessDetailsDBService", function ($scope, DBContext) {

        DBContext.GetInvestmentPorfolioData().get(function (data) {
            $scope.investmentPortfolio = data;
            $scope.investmentPortfolioDataKendo = new kendo.data.DataSource({
                data: $scope.investmentPortfolio.data
            });


        })


    }]);
    app.controller("adviserPortfolioCashflowBarchartController", ["$scope", "AdviserPortfolioSummaryDBService", function ($scope, DBContext) {
        DBContext.cashflowSummary().get(function (data) {
            $scope.data = data;
            $scope.dataSource = new kendo.data.DataSource({
                data: $scope.data.data,
                
            });
        })

    }]);
    app.controller("adviserPortfolioStatisticsController", ["$scope", "AdviserPortfolioSummaryDBService", function ($scope, DBContext) {
        DBContext.portfolioStat().get(function (data) {
            $scope.data = data;
        });

    }]);
    app.controller("adviserPortfolioRatingController", ["$scope", "AdviserPortfolioSummaryDBService", function ($scope, DBContext) {
        DBContext.portfolioRating().get(function (data) {
            $scope.data = data;

            $scope.options = {
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
                //    name: "Risk",
                //    data: [$scope.data.risk]
                //}, {
                    name: "Suitability",
                    data: [$scope.data.suitability]
                //}, {
                //    name: "% of Asset Not Suited",
                //    data: [$scope.data.notSuited]
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
        });
    }])
    app.controller("adviserPortfolioRegionController", ["$scope", "adviserPortfolioGetRegionSummary", function ($scope, DBContext) {
        DBContext.getRegionalExposure().get(function (data) {
            $scope.data = data;
            $scope.$watch("data", function () {
                $scope.plots = GetPlotsFromDate($scope.data);
            })
        });

        function GetPlotsFromDate(data) {
            var plots = {};
            var counter = 0;
            for (var i = 0; i < data.data.length; i++) {
                for (var j = 0; angular.isDefined(data.data[i].assets) && j < data.data[i].assets.length; j++) {
                    var value = data.data[i].assets[j];
                    plots[getNumberToStringName(counter)] = {
                        value: value.assetValue,
                        latitude: value.latitude,
                        longitude: value.longitude,
                        href: "#",
                        description: value.country + " with asset value of $"
                                + value.assetValue + " and weighting of " + value.assetWeighting + "%"
                    }
                    counter++;
                }
            }
            return plots;
        }
        function getNumberToStringName(number) {
            return "country" + number;
        }
    }]);
    app.controller("adivserPortfolioRegionTableController", ["$scope", "adviserPortfolioGetRegionSummary", function ($scope, DBContext) {
        DBContext.getRegionalExposure().get(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("adviserPortfolioSectorDiversificationController", ["$scope", "adviserPortfolioGetSectorInfo", function ($scope, DBContext) {
        DBContext.getData().get(function (data) {
            $scope.data = data;
            $scope.barOptions = {
                title: {
                    text: "Sectorial Bar Chart"
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
                    field: "sector",
                    name: "Sector",
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
    app.controller("adviserPortfolioSummaryCashflowDetailController",
        ["$scope", "adviserPortfolioSummaryGetCashflowInfo", function ($scope, DBContext) {
             DBContext().get(function (data) {
                 $scope.data = data;
            })
    }])
})();