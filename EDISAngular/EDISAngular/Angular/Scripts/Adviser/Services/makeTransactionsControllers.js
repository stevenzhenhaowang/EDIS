
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

    $scope.bondTickers = [];
    $http.get(AppStrings.EDIS_IP + "api/adviser/allBondTickers")
     .success(function (data) {
         $scope.bondTickers = data;
     }).error(function (data) {
         console.log("Error.............");
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


    $scope.policyTypes = [];
    $scope.insuranceTypes = [];

    $http.get(AppStrings.EDIS_IP + "api/Adviser/Transaction/policyTypes")
       .success(function (data) {
           $scope.policyTypes = data;
       }).error(function (data) {
           console.log("Error.............");
       });


    $http.get(AppStrings.EDIS_IP + "api/Adviser/Transaction/insuranceTypes")
       .success(function (data) {
           $scope.insuranceTypes = data;
       }).error(function (data) {
           console.log("Error.............");
       });




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
                Ticker: $scope.collection.bondTrans.Ticker.tickerNumber,
                Price: $scope.collection.bondTrans.Price,
                NumberOfUnits: $scope.collection.bondTrans.NumberOfUnits,
                //LoanAmount: $scope.collection.equityTrans.LoanAmount,
                TransactionDate: dateParser($scope.collection.bondTrans.TransactionDate),
                Account: $scope.collection.selectedAccount,
            };
            $http.post(AppStrings.EDIS_IP + "api/adviser/makeBondTransactions", data).success(function () {
                alert("Successs");
            }).error(function (data) {
                alert("failed:" + data);
            })

            console.log("yes~");
        }
        else if ($scope.collection.transactionType == "Insurance") {
            var aaa = true;
            if ($scope.collection.insureTrans.isAquired === "false") {
                aaa = false;
            }
            data = {
                 insuranceType : $scope.collection.insureTrans.insuranceType,
                 insuranceAmount : $scope.collection.insureTrans.insureAmount,
                 isAquired : aaa,
                 policyType : $scope.collection.insureTrans.policyType,
                 policyNumber : $scope.collection.insureTrans.policyNumber,
                 policyAddress : $scope.collection.insureTrans.policyAddress,
                 premium : $scope.collection.insureTrans.Premium,
                 issuer : $scope.collection.insureTrans.issuer,
                 insuredEntity : $scope.collection.insureTrans.insuredEntity,
                 grantedDate : dateParser( $scope.collection.insureTrans.grantedDate),
                 expiryDate : dateParser( $scope.collection.insureTrans.expiryDate),


                account: $scope.collection.selectedAccount

            };
            $http.post(AppStrings.EDIS_IP + "api/adviser/makeInsuranceTransactions", data).success(function () {
                alert("Successs");
            }).error(function (data) {
                alert("failed:" + data);
            })
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
                GrantedDate: dateParser($scope.collection.propertyTrans.grantedDate),
                ExpiryDate: dateParser($scope.collection.propertyTrans.expiryDate),

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
;