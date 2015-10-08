angular.module("EDIS")
.controller("corporateActionController", ["$scope", "corporateActionServices", "$modal", function ($scope, service, $modal) {
    service.existingIPOActions().query(function (data) {
        $scope.existingIPOActions = data;
    })
    service.existingOtherCorporateActions().query(function (data) {
        $scope.existingOtherCorporateActions = data;
    })
    //here needs to implement existing corperate Action and services needs to be implemented too
    service.existingReturnOfCapitals().query(function (data) {
        $scope.existingReturnOfCapitals = data;
    })
    

    service.exsistingReinvestment().query(function (data) {
        $scope.existingReinvestments = data;
    })

    service.existingStockSplit().query(function (data) {
        $scope.existingStockSplits = data;
    })

    service.existingBonusIssues().query(function (data) {
        $scope.existingBonuses = data;
    })


     












    $scope.selectIPOAction = function (item) {
        $scope.selectedIPOAction = item;
    }
    service.allTickers().query(function (data) {
        $scope.allTickers = data;
    })

    $scope.process = {};
    $scope.finaliseAllocation = function (action) {
        service.allocateIPOAction().save(action, function () {
            action.allocationFinalised = true;


        })
    }
    $scope.selectOtherCorporateAction = function (action) {
        $scope.selectedOtherCorporateAction = action;
    }
   
    //new return of capital action
    $scope.newReturnOfCapital = function () {
        var modalInstance = $modal.open({
            templateUrl: "existingReturnOfCapitals",
            controller: "newReturnOfCapitalActionController",
            backdrop: true
        });
        modalInstance.result.then(function (result) {
            $scope.selectModel(result.reason);
        });
    }

    $scope.newother = function () {
        var modalInstance = $modal.open({
            templateUrl: "ExistingOtherCorporateActions",
            controller: "newOtherCorporateActionController",
            backdrop: true
        });

        modalInstance.result.then(function (result) {
            $scope.selectModel(result.reason);
        });
    }
    $scope.newipo = function () {
        var modalInstance = $modal.open({
            templateUrl: "IPOAction",
            controller: "newIPOActionController",
            backdrop: true,
            size: 'lg'
        });

        modalInstance.result.then(function (result) {
            $scope.selectModel(result.reason);
        });
    }
    //
    $scope.newReinvestment = function () {
        var modalInstance = $modal.open({
            templateUrl: "exsistingReinvestment",
            controller: "newReinvestmentActionController",
            backdrop:true

        });

        modalInstance.result.then(function (result) {
            $scope.selectModel(result.reason);
        });
    }
    //
    $scope.newStockSplit = function () {
        var modalInstance = $modal.open({
            templateUrl: "exsistingStockSplit",
            controller: "newStockSplitActionController",
            backdrop:true
        });
        modalInstance.result.then(function (result) {
            $scope.selectModel(result.reason);
        });


    }
    //
    $scope.newBonusesAction = function () {
        var modalInstance = $modal.open({
            templateUrl: "exsistingBonusIssues",
            controller: "newBonusesActionController",
            backdrop: true
        });
        modalInstance.result.then(function (result) {
            $scope.selectModel(result.reason);
        });

            
    }


    $scope.newBuyBack = function () {
        var modalInstance = $modal.open({
            templateUrl: "exsistingBuyBack",
            controller: "newBuyBackController",
            backdrop: true
        });
        modalInstance.result.then(function (result) {
            $scope.selectModel(result.reason);
        });

    }

    $scope.newRightsIssue = function () {
        var modalInstance = $modal.open({
            templateUrl: "exsistingRightsIssues",
            controller: "newRightsIssueController",
            backdrop: true
        });
        modalInstance.result.then(function (result) {
            $scope.selectModel(result.reason);
        });

    }
      


}])


    .controller("newOtherCorporateActionController",
["$scope", "corporateActionServices", "$modalInstance", "dateParser", "adviserGetId", function ($scope, service, $modalInstance, dateParser, adviserGetId) {
    service.allCompanies().query(function (data) {
        $scope.allCompanies = data;
    })

    var adviserId = "";
    adviserGetId().then(function (data) {
        adviserId = data;
    })
    $scope.companyChanged = function (ticker) {
        service.getClientsBasedOnCompany().query({ companyTicker: ticker }, function (data) {
            $scope.allClients = data;
            for (var i = 0; i < $scope.allClients.length; i++) {
                $scope.allClients[i].selected = false;
            }
        })

    };
    $scope.hasClientsSelected = function () {
        if ($scope.allClients === undefined || $scope.allClients === null || $scope.allClients.length === 0) {
            return false;
        } else {
            var numberOfSelected = 0;
            for (var i = 0; i < $scope.allClients.length; i++) {
                if ($scope.allClients[i].selected) {
                    numberOfSelected++;
                }
            }
            if (numberOfSelected > 0) {
                return true;
            }
            return false;
        }
    }
    $scope.add = function () {
        var data = {
            corporateActionName: $scope.actionName,
            corporateActionCode: $scope.actionCode,
            underlyingCompany: {
                companyTicker: $scope.underlyingCompany.companyTicker,
                name: $scope.underlyingCompany.companyName
            },
            adviserUserId: adviserId,
            purposeForCorporateAction: $scope.actionPurpose,
            recordDateEntitlement: dateParser($scope.recordDateEntitlement),
            exEntitlement: dateParser($scope.exEntitlement),
            corporateActionStartDate: dateParser($scope.corporateActionStartDate),
            corporateActionClosingDate: dateParser($scope.corporateActionClosingDate),
            dispatchOfHolding: $scope.dispatchOfHolding,
            deferredSettlementTradingDate: dateParser($scope.deferredSettlementTradingDate),
            normalTradingDate: dateParser($scope.normalTradingDate),
            participants: []
        };

        for (var i = 0; i < $scope.allClients.length; i++) {
            if ($scope.allClients[i].selected) {
                data.participants.push($scope.allClients[i])
            }
        }

        service.addOtherAction().save(data, function () {
            $modalInstance.close({ reason: "success" });

        })



    }
}])


    .controller("newIPOActionController", ["$scope", "corporateActionServices", "$modalInstance", "adviserGetId", "dateParser",
    function ($scope, service, $modalInstance, adviserGetId, dateParser) {
        var adviserId = "";
        adviserGetId().then(function (data) {
            adviserId = data;
        });
        service.allClients().query(function (data) {
            $scope.allClients = data;
        })
        service.allTickers().query(function (data) {
            $scope.allTickers = data;
        })

        $scope.hasClientsSelected = function () {
            if ($scope.allClients === undefined || $scope.allClients === null || $scope.allClients.length === 0) {
                return false;
            } else {
                var numberOfSelected = 0;
                for (var i = 0; i < $scope.allClients.length; i++) {
                    if ($scope.allClients[i].selected) {
                        numberOfSelected++;
                    }
                }
                if (numberOfSelected > 0) {
                    return true;
                }
                return false;
            }
        }
        $scope.add = function () {
            var data = {
                //actionId: "id",
                tickerNumber:$scope.tickerNumber,
                nameOfRaising: $scope.nameOfRaising,
                IPOCode: $scope.ipoCode,
                listed: $scope.listed,
                exchange: $scope.exchange,
                raisingOpened: dateParser($scope.raisingOpened),
                raisingClosed: dateParser($scope.raisingClosed),
                raisingTradingDate: dateParser($scope.raisingTradingDate),
                dispatchDocDate: dateParser($scope.dispatchDocDate),
                issuedPrice: $scope.issuedPrice,
                minimumAmount: $scope.minimumAmount,
                dividendPerShare: $scope.dividendPerShare,
                dividendYield: $scope.dividendYield,
                marketCapitalisation: $scope.marketCapitalisation,
                raisingAmount: $scope.raisingAmount,
                numberOfSharesOnIssue: $scope.numberOfSharesOnIssue,
                numberOfSharesRaising: $scope.numberOfSharesRaising,
                allocationFinalised: false,
                participants: []
            };

            for (var i = 0; i < $scope.allClients.length; i++) {
                if ($scope.allClients[i].selected) {
                    var client = $scope.allClients[i];
                    data.participants.push({
                        edisAccountNumber: client.edisAccountNumber,
                        brokerAccountNumber: client.brokerAccountNumber,
                        brokerHinSrn: client.brokerHinSrn,
                        type: client.type,
                        name: client.name,
                        investedAmount: client.investedAmount,
                        numberOfUnits: 0,
                        unitPrice: 0,
                        tickerNumber: ""
                    });
                }
            }
            service.addIpoAction().save(data, function () {
                $modalInstance.close({ reason: "success" });
            })
        }
        
    }])



  .controller("newReturnOfCapitalActionController",
["$scope", "corporateActionServices", "$modalInstance", "dateParser", "adviserGetId", function ($scope, service, $modalInstance, dateParser, adviserGetId) {
    //service.allCompanies().query(function (data) {
    //    $scope.allCompanies = data;
    //})

    var adviserId = "";
    adviserGetId().then(function (data) {
        adviserId = data;
    })
  
    $scope.add = function () {
        var data = {
            corporateActionName: $scope.actionName,
            equityId: $scope.equityId,
            shareAmount: $scope.shareAmount,
            adviserUserId: adviserId,
            returnAmount: $scope.returnAmount,
            returnDate: dateParser($scope.returnDate),
          
            participants: []
        };
        //this corperate action is mandatory all clients should participate which needs to be implemented
        var allClients = service.allClients().query(function (data) {
            data.participants = data;
        })


        //for (var i = 0; i < $scope.allClients.length; i++) {
        //    if ($scope.allClients[i].selected) {
        //        data.participants.push($scope.allClients[i])
        //    }
        //}

        service.addReturnOfCapitalAction().save(data, function () {
            $modalInstance.close({ reason: "success" });

        })



    }
}])




  .controller("newReinvestmentActionController",
["$scope", "corporateActionServices", "$modalInstance", "dateParser", "adviserGetId", function ($scope, service, $modalInstance, dateParser, adviserGetId) {
    //service.allCompanies().query(function (data) {
    //    $scope.allCompanies = data;
    //})

    var adviserId = "";
    adviserGetId().then(function (data) {
        adviserId = data;
    })

    service.allClients().query(function (data) {
        $scope.allClients = data;
    })


    $scope.add = function () {
        var data = {
            corporateActionName: $scope.actionName,
            equityId: $scope.equityId,

            adviserUserId: adviserId,
            reinvestmentShareAmount: $scope.reinvestmentShareAmount,
            reinvestmentDate: dateParser($scope.reinvestmentDate),

            participants: []
        };

        //for (var i = 0; i < $scope.allClients.length; i++) {
        //    if ($scope.allClients[i].selected) {
        //        data.participants.push($scope.allClients[i])
        //    }
        //}
        for (var i = 0; i < $scope.allClients.length; i++) {
            if ($scope.allClients[i].selected) {
                var client = $scope.allClients[i];
                data.participants.push({
                    edisAccountNumber: client.edisAccountNumber,
                  
                    type: client.type,
                    name: client.name,
                    //investedAmount: client.investedAmount,
                    //numberOfUnits: 0,
                    //unitPrice: 0,
                    //tickerNumber: ""
                });
            }
        }



        $scope.hasClientsSelected = function () {
            if ($scope.allClients === undefined || $scope.allClients === null || $scope.allClients.length === 0) {
                return false;
            } else {
                var numberOfSelected = 0;
                for (var i = 0; i < $scope.allClients.length; i++) {
                    if ($scope.allClients[i].selected) {
                        numberOfSelected++;
                    }
                }
                if (numberOfSelected > 0) {
                    return true;
                }
                return false;
            }
        }

        service.addnewReinvestmentAction().save(data, function () {
            $modalInstance.close({ reason: "success" });

        })



    }
}])


 .controller("newStockSplitActionController",
["$scope", "corporateActionServices", "$modalInstance", "dateParser", "adviserGetId", function ($scope, service, $modalInstance, dateParser, adviserGetId) {
    //service.allCompanies().query(function (data) {
    //    $scope.allCompanies = data;
    //})

    var adviserId = "";
    adviserGetId().then(function (data) {
        adviserId = data;
    })

    service.allClients().query(function (data) {
        $scope.allClients = data;
    })

    $scope.add = function () {
        var data = {
            corporateActionName: $scope.actionName,
            corporateActionCode: $scope.actionCode,

            adviserUserId: adviserId,
            stockSplitShares: $scope.stockSplitShares,
            splitDate: dateParser($scope.splitDate),

            participants: []
        };

        for (var i = 0; i < $scope.allClients.length; i++) {
            if ($scope.allClients[i].selected) {
                data.participants.push($scope.allClients[i])
            }
        }

        service.addOtherAction().save(data, function () {
            $modalInstance.close({ reason: "success" });

        })



    }
}])


 .controller("newBonusesActionController",
["$scope", "corporateActionServices", "$modalInstance", "dateParser", "adviserGetId", function ($scope, service, $modalInstance, dateParser, adviserGetId) {
    service.allCompanies().query(function (data) {
        $scope.allCompanies = data;
    })

    var adviserId = "";
    adviserGetId().then(function (data) {
        adviserId = data;
    })

    service.allClients().query(function (data) {
        $scope.allClients = data;
    })

    $scope.add = function () {
        var data = {
            corporateActionName: $scope.actionName,
            corporateActionCode: $scope.actionCode,

            adviserUserId: adviserId,
            bonusIssue: $scope.bonusIssue,
            bonusDate: dateParser($scope.bonusDate),

            participants: []
        };

        for (var i = 0; i < $scope.allClients.length; i++) {
            if ($scope.allClients[i].selected) {
                data.participants.push($scope.allClients[i])
            }
        }

        service.addOtherAction().save(data, function () {
            $modalInstance.close({ reason: "success" });

        })



    }
}])
    //newBuyBackController
    .controller("newBuyBackController",
["$scope", "corporateActionServices", "$modalInstance", "dateParser", "adviserGetId", function ($scope, service, $modalInstance, dateParser, adviserGetId) {
    //service.allCompanies().query(function (data) {
    //    $scope.allCompanies = data;
    //})

    var adviserId = "";
    adviserGetId().then(function (data) {
        adviserId = data;
    })

    service.allClients().query(function (data) {
        $scope.allClients = data;
    })

    $scope.add = function () {
        var data = {
            corporateActionName: $scope.actionName,
            corporateActionCode: $scope.actionCode,

            adviserUserId: adviserId,
            rightsIssue: $scope.rightsIssue,
            rightsDate: dateParser($scope.issue),

            participants: []
        };

        for (var i = 0; i < $scope.allClients.length; i++) {
            if ($scope.allClients[i].selected) {
                data.participants.push($scope.allClients[i])
            }
        }

        service.addOtherAction().save(data, function () {
            $modalInstance.close({ reason: "success" });

        })



    }
}])


.controller("newRightsIssueController",
["$scope", "corporateActionServices", "$modalInstance", "dateParser", "adviserGetId", function ($scope, service, $modalInstance, dateParser, adviserGetId) {
    //service.allCompanies().query(function (data) {
    //    $scope.allCompanies = data;
    //})

    var adviserId = "";
    adviserGetId().then(function (data) {
        adviserId = data;
    })

    service.allClients().query(function (data) {
        $scope.allClients = data;
    })

    $scope.add = function () {
        var data = {
            corporateActionName: $scope.actionName,
            corporateActionCode: $scope.actionCode,

            adviserUserId: adviserId,
            rightsIssue: $scope.rightsIssue,
            rightsDate: dateParser($scope.issue),

            participants: []
        };

        for (var i = 0; i < $scope.allClients.length; i++) {
            if ($scope.allClients[i].selected) {
                data.participants.push($scope.allClients[i])
            }
        }

        service.addOtherAction().save(data, function () {
            $modalInstance.close({ reason: "success" });

        })



    }
}])


;