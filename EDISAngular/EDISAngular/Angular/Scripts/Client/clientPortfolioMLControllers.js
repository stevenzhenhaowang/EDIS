/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />


//(function () {
//    var app = angular.module("EDIS");
//    app.controller("clientPortfolioMLViewController", ["$scope", "clientPortfolioMLProviderGet", function ($scope, DBContext) {
//        $scope.providers = DBContext();

//    }]);

//    app.controller("clientPortfolioMLCashflowController", ["$scope", "clientPortfolioMLCashflowDetails", function ($scope, DBContext) {
//        $scope.data = DBContext();
//        $scope.dataSource = new kendo.data.DataSource({
//            data: $scope.data.data,
//            sort: {
//                field: "date",
//                dir: "asc"
//            }
//        });
//    }]);
//    app.controller("clientPortfolioMLRatingController", ["$scope", "clientPortfolioMLRatingInfo", function ($scope, DBContext) {
//        $scope.data = DBContext();
//        $scope.mainOptions = {
//            title: {
//                text: "Portfolio Rating"
//            },
//            legend: {
//                position: "top"
//            },
//            seriesDefaults: {
//                type: "column"
//            },
//            series: [{
//                name: "Risk",
//                data: [$scope.data.risk]
//            }, {
//                name: "Suitability",
//                data: [$scope.data.suitability]
//            }, {
//                name: "% of Asset Not Suited",
//                data: [$scope.data.notSuited]
//            }],

//            valueAxis: {
//                labels: {
//                    format: "{0}"
//                },
//                line: {
//                    visible: false
//                },
//                axisCrossingValue: 0
//            },
//            tooltip: {
//                visible: true,
//                format: "{0}",
//                template: "#= series.name # rating is #= value #"
//            }
//        };


//    }])
//    app.controller("clientPortfolioMLStatisticsController", ["$scope", "clientPortfolioMLStatistics", function ($scope, DBContext) {
//        $scope.data = DBContext();
//    }]);
//    app.controller("clientPortfolioMLCompanyProfilesController", ["$scope", "clientPortfolioMLCompanyProfile", function ($scope, DBContext) {
//        $scope.data = DBContext();
//    }])
//    app.controller("clientPortfolioMLLoanDetailsController", ["$scope", "clientPortfolioMLLoanDetails", function ($scope, DBContext) {
//        $scope.data = DBContext();
//    }])
//    app.controller("clientPortfolioMLLoanAccountControllers", ["$scope", "clientPortfolioMLLoanInfo", function ($scope, DBContext) {
//        $scope.data = DBContext();
//    }]);
//    app.controller("clientPortfolioMLSuitabilityController", ["$scope", "clientPortfolioMLSuitabilityDetails", function ($scope, DBContext) {
//        $scope.data = DBContext();
//    }]);
//    app.controller("clientPortfolioMLImpactofMarginController", ["$scope", "clientPortfolioMLImpactOfMLtoCashFlow", function ($scope, DBContext) {
//        $scope.data = DBContext();
//    }])
//    app.controller("clientPortfolioMLTypeOfGearingStrategyController", ["$scope", "clientPortfolioMLTypeOfGearingStrategy", function ($scope, DBContext) {
//        $scope.data = DBContext();
//    }])
//    app.controller("clientPortfolioMLLVRDiversificationController", ["$scope", "clientPortfolioMLLVRDiversification", function ($scope, DBContext) {
//        $scope.data = DBContext();
//    }])


//})();

var app = angular.module("EDIS");
app.controller("clientPortfolioMLViewController", ["$scope", "clientPortfolioMLAccountGet", function ($scope, DBContext) {
    DBContext.query(function (data) {
        $scope.providers = data;

    })

}]);
app.controller("clientPortfolioMLCashflowController", ["$scope", "clientPortfolioMLCashflowDetails", function ($scope, DBContext) {
    DBContext.get(function (data) {
        $scope.data = data;

    })

}]);
app.controller("clientPortfolioMLRatingController", ["$scope", "clientPortfolioMLRatingInfo", function ($scope, DBContext) {
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
app.controller("clientPortfolioMLStatisticsController", ["$scope", "clientPortfolioMLStatistics", function ($scope, DBContext) {
    DBContext.query(function (data) {
        $scope.data = data;
    });
}]);
app.controller("clientPortfolioMLCompanyProfilesController", ["$scope", "clientPortfolioMLCompanyProfile", function ($scope, DBContext) {
    DBContext.query(function (data) {
        $scope.data = data;
    })
}])
app.controller("clientPortfolioMLLoanDetailsController", ["$scope", "clientPortfolioMLLoanDetails", function ($scope, DBContext) {

    $scope.$watch("selectedProvider", function (newValue) {
        DBContext.get({ clientAccountNumber: newValue.edisAccountNumber }, function (data) {
            $scope.data = data;
        })
    })


}])
app.controller("clientPortfolioMLLoanAccountControllers", ["$scope", "clientPortfolioMLLoanInfo", function ($scope, DBContext) {
    $scope.$watch("selectedProvider", function (newValue) {
        DBContext.get({ clientAccountNumber: newValue.edisAccountNumber }, function (data) {
            $scope.data = data;

        })

    })
}]);
app.controller("clientPortfolioMLSuitabilityController", ["$scope", "clientPortfolioMLSuitabilityDetails", function ($scope, DBContext) {
    DBContext.get(function (data) {
        $scope.data = data;
    })
}]);
app.controller("clientPortfolioMLImpactofMarginController", ["$scope", "clientPortfolioMLImpactOfMLtoCashFlow", function ($scope, DBContext) {
    DBContext.get(function (data) {
        $scope.data = data;
    })
}])
app.controller("clientPortfolioMLTypeOfGearingStrategyController", ["$scope", "adivserPortfolioMLTypeOfGearingStrategy", function ($scope, DBContext) {
    DBContext.get(function (data) {
        $scope.data = data;
    })
}])
app.controller("clientPortfolioMLLVRDiversificationController", ["$scope", "clientPortfolioMLLVRDiversification", function ($scope, DBContext) {
    DBContext.get(function (data) {
        $scope.data = data;
    })
}])

