/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />


(function () {
    var app = angular.module("EDIS");
    app.controller("clientPortfolioMIViewController", function ($scope) {

    });
    app.controller("clientPortfolioMIBriefInfoController", ["$scope", "clientPortfolioMIGeneralInfo", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("clientPortfolioMICashflowController", ["$scope", "clientPortfolioMICashflowMonthly", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("clientPortfolioMICashflowBarchartController", ["$scope", "clientPortfolioMICashflowDetails", function ($scope, DBContext) {
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
    app.controller("clientPortfolioMIStatisticsController", ["$scope", "clientPortfolioMICompanyProfiles", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("clientPortfolioMIDiversificationController", ["$scope", "clientPortfolioMIDiversificationInfo", "clientPortfolioMIDiversificationGroupSummary", function ($scope, sectorDB, summaryDB) {
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
        })
    }])
    app.controller("clientPortfolioMICashflowCompanySpecificController", ["$scope", "clientPortfolioMICashflowInfoCompanySpecific", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("clientPortfolioMIRatingController", ["$scope", "clientPortfolioMIRatingInfo", function ($scope, DBContext) {
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
    app.controller("clientPortfolioMIQuickStatsController", ["$scope", "clientPortfolioMIQuickStats", function ($scope, DBContext) {
        DBContext().query(function (data) {
            $scope.data = data;
        })
    }])
})();