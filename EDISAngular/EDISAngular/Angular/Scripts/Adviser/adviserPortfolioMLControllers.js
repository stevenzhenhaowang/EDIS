/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />


(function () {
    var app = angular.module("EDIS");
    app.controller("adviserPortfolioMLViewController", ["$scope", "adviserPortfolioMLAccountsGet", function ($scope, DBContext) {
        DBContext.query(function (data) {
            $scope.providers = data;
        })

    }]);
    app.controller("adviserPortfolioMLCashflowController", ["$scope", "adviserPortfolioMLCashflowDetails", function ($scope, DBContext) {
        DBContext.get(function (data) {
            $scope.data = data;
        })

    }]);
    app.controller("adviserPortfolioMLRatingController", ["$scope", "adviserPortfolioMLRatingInfo", function ($scope, DBContext) {
        DBContext.get(function (data) {
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
    app.controller("adviserPortfolioMLStatisticsController", ["$scope", "adviserPortfolioMLStatistics", function ($scope, DBContext) {
        DBContext.query(function (data) {
            $scope.data = data;
        });
    }]);
    app.controller("adviserPortfolioMLCompanyProfilesController", ["$scope", "adviserPortfolioMLCompanyProfile", function ($scope, DBContext) {
        DBContext.query(function (data) {
            $scope.data = data;
        })
    }])
    app.controller("adviserPortfolioMLLoanDetailsController", ["$scope", "adviserPortfolioMLLoanDetails", function ($scope, DBContext) {

        $scope.$watch("selectedProvider", function (newValue) {
            DBContext.get({ clientAccountNumber: newValue.edisAccountNumber }, function (data) {
                $scope.data = data;
            })
        })


    }])
    app.controller("adviserPortfolioMLLoanAccountControllers", ["$scope", "adviserPortfolioMLLoanInfo", function ($scope, DBContext) {
        $scope.$watch("selectedProvider", function (newValue) {
            DBContext.get({ clientAccountNumber: newValue.edisAccountNumber }, function (data) {
                $scope.data = data;

            })

        })
    }]);
    app.controller("adviserPortfolioMLSuitabilityController", ["$scope", "adviserPortfolioMLSuitabilityDetails", function ($scope, DBContext) {
        DBContext.get(function (data) {
            $scope.data = data;
        })
    }]);
    app.controller("adviserPortfolioMLImpactofMarginController", ["$scope", "adviserPortfolioMLImpactOfMLtoCashFlow", function ($scope, DBContext) {
        DBContext.get(function (data) {
            $scope.data = data;
        })
    }])
    app.controller("adviserPortfolioMLTypeOfGearingStrategyController", ["$scope", "adivserPortfolioMLTypeOfGearingStrategy", function ($scope, DBContext) {
        DBContext.get(function (data) {
            $scope.data = data;
        })
    }])
    app.controller("adviserPortfolioMLLVRDiversificationController", ["$scope", "adviserPortfolioMLLVRDiversification", function ($scope, DBContext) {
        DBContext.get(function (data) {
            $scope.data = data;
        })
    }])


})();