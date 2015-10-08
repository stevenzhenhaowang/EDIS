/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />


(function () {
    var app = angular.module("EDIS");
    app.controller("clientPortfolioINTViewController", function ($scope) {

    });
    app.controller("clientPortfolioINTBriefInfoController", ["$scope", "clientPortfolioINTGeneralInfo", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })

    }]);
    app.controller("clientPortfolioINTCashflowController", ["$scope", "clientPortfolioINTCashflowMonthly", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("clientPortfolioINTEvaluationController", ["$scope", "clientPortfolioINTEvaluationAgainstModel", function ($scope, DBContext) {
        DBContext().query(function (data) {
            $scope.data = data;
            $scope.chartOptions = {
                legend: {
                    position: "bottom"
                },
                seriesDefaults: {
                    type: "radarLine"
                },
                series: [{ name: "Expected", field: "expected" }, { name: "Actual", field: "actual" }],
                categoryAxis: { field: "title" },
                dataSource: {
                    data: $scope.data
                }
            };
        })
    }]);
    app.controller("clientPortfolioINTCashflowBarchartController", ["$scope", "clientPortfolioINTCashflowDetails", function ($scope, DBContext) {
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
    app.controller("clientPortfolioINTStatisticsController", ["$scope", "clientPortfolioINTCompanyProfiles", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("clientPortfolioINTDiversificationController", ["$scope", "clientPortfolioINTDiversificationInfo", function ($scope, DBContext) {
        DBContext().get(function (data) {
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
    }])
    app.controller("clientPortfolioINTCashflowCompanySpecificController", ["$scope", "clientPortfolioINTCashflowInfoCompanySpecific", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("clientPortfolioINTRatingController", ["$scope", "clientPortfolioINTRatingInfo", function ($scope, DBContext) {
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
    app.controller("clientPortfolioINTQuickStatsController", ["$scope", "clientPortfolioINTQuickStats", function ($scope, DBContext) {
        DBContext().query(function (data) {
            $scope.data = data;
        })
    }])

})();