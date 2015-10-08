/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />


(function () {
    var app = angular.module("EDIS");
    app.controller("adviserPortfolioMIViewController", function ($scope) {

    });
    app.controller("adviserPortfolioMIBriefInfoController", ["$scope", "adviserPortfolioMIGeneralInfo", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })

    }]);
    app.controller("adviserPortfolioMICashflowController", ["$scope", "adviserPortfolioMICashflowMonthly", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }]);


    app.controller("adviserPortfolioMICashflowBarchartController", ["$scope", "adviserPortfolioMICashflowDetails", function ($scope, DBContext) {
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
    

    app.controller("adviserPortfolioMIPiechartController", ["$scope", "advisorPortfolioMIAssetAllocation", function ($scope, DBContext) {

        DBContext().get(function (data) {
            $scope.investmentPortfolio = data;
            $scope.investmentPortfolioDataKendo = new kendo.data.DataSource({
                data: $scope.investmentPortfolio.data
            });


        })


    }]);

    app.controller("adviserPortfolioMIStatisticsController", ["$scope", "adviserPortfolioMICompanyProfiles", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        });
    }]);
    app.controller("adviserPortfolioMIDiversificationController", ["$scope", "adviserPortfolioMIDiversificationInfo", "adviserPortfolioMIDiversificationGroupSummary", function ($scope, sectorDB, summaryDB) {
        sectorDB().get(function (data) {
            $scope.data = data;
            $scope.pieOptions = {
                dataSource: {
                    data: $scope.data.data

                },
                title: {
                    text: "Diversification Pie Chart"
                },
                legend: {
                    position: "bottom"
                },
                seriesDefaults: {
                    type: "pie"
                },
                series: [{
                    field: "sectorialExposureValue",
                    categoryField: "sector",
                    padding: 0
                }],
                tooltip: {
                    visible: true,
                    format: "N0",
                    template: "#= category # - #= kendo.format('{0:P}', percentage)#"
                }
            }

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
                series: [{ field: "sectorialExposureValue", name: "Value", spacing: 8 }],
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
                    template: " $#= kendo.format('{0:c2}',value) #"
                }
            };

            $scope.suitabilityOptions = {
                title: {
                    text: "Suitability Bar Chart"
                },
                legend: {
                    visible: false
                },
                seriesDefaults: {
                    type: "column"
                },
                series: [{ field: "suitabilityIndicator", name: "Value", spacing: 8 }],
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
                        format: "n0",

                    }
                },
                tooltip: {
                    visible: true,
                    format: "{0}",
                    template: " $#= kendo.format('{0:c2}',value) #"
                }
            };




        })
        summaryDB().get(function (data) {
            $scope.summary = data;
        });
    }])
    app.controller("adviserPortfolioMICashflowCompanySpecificController", ["$scope", "adviserPortfolioMICashflowInfoCompanySpecific", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("adviserPortfolioMIRatingController", ["$scope", "adviserPortfolioMIRatingInfo", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
            $scope.suitability = data.suitability;
            $scope.suitabilityDesc = data.SuitabilityDesc;

            $scope.detailOptions = {
                title: {
                    text: "Detailed Rating"
                },
                dataSource: {
                    data: $scope.data.data
                },
                legend: {
                    position: "top"
                },
                seriesDefaults: {
                    type: "bar"
                }, categoryAxis: {
                    field: "name"
                },
                series: [{
                    name: "Score",
                    field: "score"
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
                    template: "#= category # rating is #= kendo.format('{0:N2}', value) #"
                }
            };
        })


    }])
    app.controller("adviserPortfolioMIQuickStatsController", ["$scope", "adviserPortfolioMIQuickStats", function ($scope, DBContext) {
        DBContext().query(function (data) {
            $scope.data = data;
        })
    }])
})();