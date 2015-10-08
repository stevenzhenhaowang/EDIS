/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />


(function () {
    var app = angular.module("EDIS");
    app.controller("adviserPortfolioAEViewController", function ($scope) {
        $scope.changeClient = function () {
            $http.get(AppStrings.EDIS_IP + "api/adviser/insertAssetsData").success(function () {
                alert("success");
            }).error(function (data) {
                alert("failed:" + data);
            })
        }












        
    });

    app.controller("adviserPortfolioAEBriefInfoController", ["$scope", "adviserPortfolioAEGeneralInfo", function ($scope, DBContext) {
        DBContext().get(function (data) {

            $scope.data = data;
        })

    }]);


    //app.controller("adviserPortfolioAEBriefInfoController", ["$scope", "adviserPortfolioAEGeneralInfo", function ($scope, DBContext) {
    //    DBContext.getData().then(function (data) {
    //        $scope.data = data.data;
    //    })
    //}]);

    //app.controller("adviserPortfolioAEBriefInfoController", ["$scope", "clientSelectionService", "$http", "$resource", "AppStrings", function ($scope, cselector, $http, $resource, AppStrings) {

    //    var clientGroupId = cselector.getCurrentClientUserId();

    //    $scope.data = $resource(AppStrings.EDIS_IP + "api/Adviser/AustralianEquityPortfolio/General/:clientGroupId", { clientGroupId: '@clientGroupId' });

    //    //DBContext().get(function (data) {
    //    //    $scope.data = data;
    //    //})

    //}]);


    app.controller("adviserPortfolioAECashflowController", ["$scope", "adviserPortfolioAECashflowDetails", function ($scope, DBContext) {
        DBContext().get(function (data) {

            $scope.data = data;
        })

    }]);
    app.controller("adviserPortfolioAEEvaluationController", ["$scope", "adviserPortfolioAEEvaluationAgainstModel", function ($scope, DBContext) {
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
    app.controller("adviserPortfolioAECashflowBarchartController", ["$scope", "adviserPortfolioAECashflowDetails", function ($scope, DBContext) {
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
    app.controller("adviserPortfolioAEStatisticsController", ["$scope", "adviserPortfolioAECompanyProfiles", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("adviserPortfolioAEDiversificationController", ["$scope", "adviserPortfolioAEDiversificationInfo", function ($scope, DBContext) {
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
    app.controller("adviserPortfolioAECashflowCompanySpecificController", ["$scope", "adviserPortfolioAECashflowInfoCompanySpecific", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        });
    }]);
    app.controller("adviserPortfolioAERatingController", ["$scope", "adviserPortfolioAERatingInfo", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;



            //$scope.options = {
            //    title: {
            //        text: "Portfolio Rating"
            //    },
            //    legend: {
            //        position: "top"
            //    },
            //    seriesDefaults: {
            //        type: "column"
            //    },
            //    series: [{
            //        name: "Risk",
            //        data: [$scope.data.risk]
            //    }, {
            //        name: "Suitability",
            //        data: [$scope.data.suitability]
            //    }, {
            //        name: "% of Asset Not Suited",
            //        data: [$scope.data.notSuited]
            //    }],
            //    valueAxis: {
            //        labels: {
            //            format: "{0}"
            //        },
            //        line: {
            //            visible: false
            //        },
            //        axisCrossingValue: 0
            //    },
            //    tooltip: {
            //        visible: true,
            //        format: "{0}",
            //        template: "#= series.name # rating is #= value #"
            //    }
            //};





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



    //inject here
 app.controller("adviserPortfolioSectorDiversificationController", ["$scope", "adviserPortfolioGetSectorInfo", function ($scope, DBContext) {
     DBContext.getData().get(function (data) {
         $scope.data = data;
         console.log(data.data);
        /* $scope.barOptions = {
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
         };*/

         console.log("??AAA weishenme");

         $scope.pieSource = {
             data: $scope.data.data
         }


     })



 }]);
    
    

    app.controller("adviserPortfolioAEQuickStatsController", ["$scope", "adviserPortfolioAEQuickStats","adviserPortfolioGetSectorInfo", function ($scope, DBContext) {
        DBContext().query(function (data) {
            $scope.data = data;
            $scope.barOptions = {
                title: {
                    text: "Sectorial Bar Chart555"
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
            console.log("checkThis");
            $scope.pieSource = {
                data: $scope.data.data

            }
        })
    }])
})();