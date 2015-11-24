
angular.module("EDIS")

//app.factory("adviserPortfolioAEGeneralInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
//    return function () {
//        return $resource(AppStrings.EDIS_IP + "api/Adviser/AustralianEquityPortfolio/General" + clientSelector.getClientIdQueryString());
//    }["clientSelectionService", "$http", "$resource", "AppStrings","adviserGetClientGroups",], clientSelector
//}]);
.controller("adviserMakeTransactions", function ($http, $resource, $filter, $q, $scope, adviserGetClientGroups, AppStrings, dateParser, clientSelectionService) {
    adviserGetClientGroups().then(function (data) {
        $scope.groups = data;
    });


  
    //$scope.loadClients = function () {
    //    var data = [];
    //    data = {
    //        clientGroup: $scope.clientGroup
    //    }
    //    $http.post(AppStrings.EDIS_IP + "api/adviser/getAllClientGroups", data)
    //            .success(function (data) {
    //                $scope.clients = data;
    //            }).error(function (data) {
    //                console.log("Error.............");
    //            });

    //}
    $scope.loadAccounts = function () {
        var data = [];
        data = {
            clientGroup: $scope.clientGroup
        }
        $http.post(AppStrings.EDIS_IP + "api/adviser/getAllAccountForGroup", data)
        .success(function (data) {
            $scope.accounts = data;
        }).error(function (data) {
            console.log("Error.............");
        });
    }

    $scope.loadAllTickers = function () {
        $http.get(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/Ticker")
            .success(function (data) {
                $scope.allTickers = data;
            }).error(function (data) {
                console.log("Error.............");
            });
    }

    $scope.propertyTypes = [];
    $scope.typeOfRates = [];

    $http.get(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/PropertyTypes")
        .success(function (data) {
            $scope.propertyTypes = data;
        }).error(function (data) {
            console.log("Error.............");
        });
    
    $http.get(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/TypeOfMortgageRates")
        .success(function (data) {
            $scope.typeOfRates = data;
        }).error(function (data) {
            console.log("Error.............");
        });

    $scope.reset = function () {
        $scope.collection = {
            equityTrans : {},
            bondTrans: {},
            propertyTrans: {},
            riskProfile: {
                //questions: service.getRiskQuestions,
                //levels: service.getRiskLevels
            },
        };
        //$scope.entityTypes = service.getEntityTypes;
        //$scope.existingGroupsss = service.getExistingGroups;
    }

    $scope.save = function () {
        var data = {};
        if ($scope.collection.transactionType === "Equity") {
            data = {
                Ticker: $scope.collection.equityTrans.Ticker.tickerNumber,
                Sector: $scope.collection.equityTrans.Sector,
                Price: $scope.collection.equityTrans.Price,
                NumberOfUnits: $scope.collection.equityTrans.NumberOfUnits,
                LoanAmount: $scope.collection.equityTrans.LoanAmount,
                TransactionDate: dateParser($scope.collection.equityTrans.TransactionDate),
                Name: $scope.collection.equityTrans.Ticker.tickerName,
             
                Account: $scope.collection.selectedAccount
            };
            $http.post(AppStrings.EDIS_IP + "api/adviser/makeEquityTransactions", data).success(function () {
                alert("Successs");
            }).error(function (data) {
                alert("failed:" + data);
            })
        }
        else if ($scope.collection.transactionType === "Bond") {
            data = {

            };
        }
        else if ($scope.collection.transactionType == "Insurance") {
            data = {

            };
        }
        else if ($scope.collection.transactionType == "Property") {
            data = {
                PropertyAddress: $scope.collection.propertyTrans.propertyAddress,
                PropertyType: $scope.collection.propertyTrans.propertyType,
                PropertyPrice: $scope.collection.propertyTrans.propertyPrice,
                LoanAmount: $scope.collection.propertyTrans.loanAmount,
                LoanRate: $scope.collection.propertyTrans.loanRate,
                TypeOfRate: $scope.collection.propertyTrans.typeOfRate,
                TransactionFee: $scope.collection.propertyTrans.transactionFee,
                TransactionDate: dateParser($scope.collection.propertyTrans.transactionDate),
                Institution: $scope.collection.propertyTrans.institution,

                Account: $scope.collection.selectedAccount
            };
            $http.post(AppStrings.EDIS_IP + "api/adviser/makePropertyTransactions", data).success(function () {
                alert("Successs");
            }).error(function (data) {
                alert("failed:" + data);
            })
        }


    }


});



//.factory("getAllExistingClientGroup", function ($http, $resource, $filter, $q, $scope, AppStrings) {
//    return function () {
//        $resource(AppStrings.EDIS_IP + "api/Personclient/GetAllGlientGroup");
//    }

//})

/* [
        {
            id: "00001",
            name: "Mr. X and Mrs. Y",
            accountNumber: "00001",
            numberOfLinks: 5,
            dateCreated: new Date(),
        }, {
            id: "00002",
            name: "Mr. A and Mrs. B",
            accountNumber: "00002",
            numberOfLinks: 8,
            dateCreated: new Date(),
        }, {
            id: "00003",
            name: "Mr. C and Mrs. D",
            accountNumber: "00003",
            numberOfLinks: 3,
            dateCreated: new Date(),
        }, {
            id: "00004",
            name: "Mr. E and Mrs. F",
            accountNumber: "00004",
            numberOfLinks: 9,
            dateCreated: new Date(),
        },
    ];*/
;