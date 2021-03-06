﻿angular.module("EDIS")
.controller("corporateActionController", ["$scope", "corporateActionServices", "$modal", "$http", function ($scope, service, $modal, $http) {
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

    service.existingRightsIssues().query(function (data) {
        $scope.existingRightsIssues = data;
    })

    service.existingBuyBackProgram().query(function (data) {
        $scope.existingBuyBackProgram = data;
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
   
    //new return of capital action popups 
    $scope.newReturnOfCapital = function () {
        var modalInstance = $modal.open({
            templateUrl: "existingReturnOfCapitals",
            controller: "newReturnOfCapitalActionController",
            backdrop: true
        });
        modalInstance.result.then(function (result) {

            $scope.existingReturnOfCapitals = [];
            service.existingReturnOfCapitals().query(function (data) {
                $scope.existingReturnOfCapitals = data;
            })


            //$scope.selectModel(result.reason);
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
   
    $scope.newReinvestment = function () {
        var modalInstance = $modal.open({
            templateUrl: "exsistingReinvestment",
            controller: "newReinvestmentActionController",
            backdrop:true

        });

        modalInstance.result.then(function (result) {
            $scope.existingReinvestments = [];
            service.exsistingReinvestment().query(function (data) {
                $scope.existingReinvestments = data;
            })
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
            $scope.existingStockSplits = [];
            service.existingStockSplit().query(function (data) {
                $scope.existingStockSplits = data;
            })
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
            $scope.existingBonuses = [];
            service.existingBonusIssues().query(function (data) {
                $scope.existingBonuses = data;
            })
        });

            
    }


    $scope.newBuyBack = function () {
        var modalInstance = $modal.open({
            templateUrl: "exsistingBuyBack",
            controller: "newBuyBackController",
            backdrop: true
        });
        modalInstance.result.then(function (result) {
            $scope.existingBuyBackProgram = [];

            service.existingBuyBackProgram().query(function (data) {
                $scope.existingBuyBackProgram = data;
            })
        });

    }

    $scope.newRightsIssue = function () {
        var modalInstance = $modal.open({
            templateUrl: "exsistingRightsIssues",
            controller: "newRightsIssueController",
            backdrop: true
        });
        modalInstance.result.then(function (result) {
            $scope.existingRightsIssues = [];

            service.existingRightsIssues().query(function (data) {
                $scope.existingRightsIssues = data;
            })

         
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
["$scope", "corporateActionServices", "$modalInstance", "dateParser", "adviserGetId", "AppStrings", "$http",  function ($scope, service, $modalInstance, dateParser, adviserGetId, AppStrings, $http) {
   
    var adviserId = "";
    adviserGetId().then(function (data) {
        adviserId = data;
    })

    service.allTickers().query(function (data) {
        $scope.allTickers = data;
    })



    $scope.loadAccounts = function () {
        var data = [];
        data = {
            Ticker: $scope.tickerNumber
        }
        $http.post(AppStrings.EDIS_IP + "api/adviser/corporateAction/getAccountByEquity", data)
          .success(function (data) {
              $scope.allAccounts = data;
          }).error(function (data) {
              console.log("Error.............");
          });
    }


    $scope.add = function () {
        var data = {
            actionName: $scope.actionName,
            equityId: $scope.tickerNumber,
            returnDate: dateParser($scope.returnDate),
          
            ParticipantsInfo: []
        };
        //this corperate action is mandatory all clients should participate which needs to be implemented

        for (var i = 0; i < $scope.allAccounts.length; i++) {
           
            var client = $scope.allAccounts[i];
            data.ParticipantsInfo.push({
                   accountNumber: client.edisAccountNumber,
                    returnAmount: client.returnAmount
                });
           
        }
 

        //service.newReturnOfCapital(data, function () {
        //    $modalInstance.close({ reason: "success" });
        //    $scope.formUpdated();
        //})

        $http.post(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/newReturnCapital", data).success(function () {
                    alert("success");
                    $modalInstance.close({ reason: "success" });
                    
                }).error(function (data) {
                    alert("failed:" + data);
                })



    }
}])




  .controller("newReinvestmentActionController",
["$scope", "corporateActionServices", "$modalInstance", "dateParser", "adviserGetId", "$http", "AppStrings", function ($scope, service, $modalInstance, dateParser, adviserGetId, $http, AppStrings) {
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


    service.allTickers().query(function (data) {
        $scope.allTickers = data;
    })



    $scope.loadAccounts = function () {
        var data = [];
        data = {
            Ticker: $scope.tickerNumber
        }
        $http.post(AppStrings.EDIS_IP + "api/adviser/corporateAction/getAccountByEquity", data)
            .success(function (data) {
                $scope.allAccounts = data;
            }).error(function (data) {
                console.log("Error.............");
            });
    }


    $scope.add = function () {
        var data = {
            ActionName: $scope.actionName,
            Ticker: $scope.tickerNumber,
            //ShareMount: $scope.reinvestmentShareAmount,
            ReinvestmentDate: dateParser($scope.reinvestmentDate),

            participants: []
        };

        //for (var i = 0; i < $scope.allClients.length; i++) {
        //    if ($scope.allClients[i].selected) {
        //        data.participants.push($scope.allClients[i])
        //    }
        //}
        for (var i = 0; i < $scope.allAccounts.length; i++) {
           // if ($scope.allClients[i].selected) {
            var client = $scope.allAccounts[i];
                data.participants.push({
                    accountNumber: client.edisAccountNumber,
                    shareMount: client.reinvenstmentAmount,
                    //type: client.type,
                    //name: client.name,
                    //investedAmount: client.investedAmount,
                    //numberOfUnits: 0,
                    //unitPrice: 0,
                    //tickerNumber: ""
                });
           // }
        }




        //service.addnewReinvestmentAction(data, function () {
        //    $modalInstance.close({ reason: "success" });
        //})


        $http.post(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/newReinvestment", data).success(function () {
            alert("success");
            $modalInstance.close({ reason: "success" });
           
        }).error(function (data) {
            alert("failed:" + data);
        })

    }
}])


 .controller("newStockSplitActionController", 
["$http", "$scope", "corporateActionServices", "$modalInstance", "dateParser", "adviserGetId", "AppStrings", function ($http, $scope, service, $modalInstance, dateParser, adviserGetId, AppStrings) {
    //service.allCompanies().query(function (data) {
    //    $scope.allCompanies = data;
    //})

    var adviserId = "";
    adviserGetId().then(function (data) {
        adviserId = data;
    })

    service.allTickers().query(function (data) {
        $scope.allTickers = data;
    })


    service.allClients().query(function (data) {
        $scope.allClients = data;
    })

    $scope.loadAccounts = function () {
        var data = [];
        data = {
            Ticker: $scope.tickerNumber
        }
        $http.post(AppStrings.EDIS_IP + "api/adviser/corporateAction/getAccountByEquity", data)
          .success(function (data) {
              $scope.allAccounts = data;
          }).error(function (data) {
              console.log("Error.............");
          });
    }

    $scope.add = function () {
        var data = {
            ActionName: $scope.actionName,
            adviserUserId: adviserId,
            Ticker:$scope.tickerNumber, 
            splitDate: dateParser($scope.splitDate),

            AccountsInfo: []
        };

        for (var i = 0; i < $scope.allAccounts.length; i++) {
            var client = $scope.allAccounts[i];
            data.AccountsInfo.push
           ({
               accountNumber: client.edisAccountNumber,
               splitToUnit: client.splitTo,
           });
        }

        $http.post(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/newStockSplit", data).success(function () {
            alert("success");
            $modalInstance.close({ reason: "success" });
           
        }).error(function (data) {
            alert("failed:" + data);
        })

        /*service.addnewReinvestmentAction(data, function () {
            $modalInstance.close({ reason: "success" });
        })*/

    }





}])


 .controller("newBonusesActionController",
["$scope", "corporateActionServices", "$modalInstance", "dateParser", "adviserGetId", "$http", "AppStrings", function ($scope, service, $modalInstance, dateParser, adviserGetId, $http, AppStrings) {
    service.allCompanies().query(function (data) {
        $scope.allCompanies = data;
    })

    var adviserId = "";
    adviserGetId().then(function (data) {
        adviserId = data;
    })

    service.allTickers().query(function (data) {
        $scope.allTickers = data;
    })


    //service.allClients().query(function (data) {
    //    $scope.allClients = data;
    //})

    $scope.loadAccounts = function () {
        var data = [];
        data = {
            Ticker: $scope.tickerNumber
        }
        $http.post(AppStrings.EDIS_IP + "api/adviser/corporateAction/getAccountByEquity", data)
          .success(function (data) {
              $scope.allAccounts = data;
          }).error(function (data) {
              console.log("Error.............");
          });
    }

    $scope.add = function () {
        var data = {
            ActionName: $scope.actionName,
            Ticker: $scope.tickerNumber,

            AdviserId: adviserId,
        
            BonusIssueDate: dateParser($scope.bonusDate),

            Participants: []
        };

        for (var i = 0; i < $scope.allAccounts.length; i++) {
            var client = $scope.allAccounts[i];
            data.Participants.push
           ({
               AccountNumber: client.edisAccountNumber,
               ShareAmount: client.BonusShareAmount,
           });
        }

        $http.post(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/newBonusIssue", data).success(function () {
            alert("Success!");
            $modalInstance.close({ reason: data });
        }).error(function (data) {
            alert("failed:" + data);
        })



    }
}])
    //newBuyBackController
    .controller("newBuyBackController",
["$scope", "corporateActionServices", "$modalInstance", "dateParser", "adviserGetId", "$http", "AppStrings", function ($scope, service, $modalInstance, dateParser, adviserGetId, $http, AppStrings) {
    //service.allCompanies().query(function (data) {
    //    $scope.allCompanies = data;
    //})

    var adviserId = "";
    adviserGetId().then(function (data) {
        adviserId = data;
    })

    service.allTickers().query(function (data) {
        $scope.allTickers = data;
    })


    //service.allClients().query(function (data) {
    //    $scope.allClients = data;
    //})

    $scope.loadAccounts = function () {
        var data = [];
        data = {
            Ticker: $scope.tickerNumber
        }
        $http.post(AppStrings.EDIS_IP + "api/adviser/corporateAction/getAccountByEquity", data)
          .success(function (data) {
              $scope.allAccounts = data;
          }).error(function (data) {
              console.log("Error.............");
          });
    }

    $scope.add = function () {
        var data = {
            ActionName: $scope.actionName,
            AdviserId: adviserId,
            Ticker: $scope.tickerNumber,
            //rightsIssue: $scope.rightsIssue,
            BuyBackDate: dateParser($scope.buyBackDate),

            Participants: []
        };

        for (var i = 0; i < $scope.allAccounts.length; i++) {
            var client = $scope.allAccounts[i];
            data.Participants.push
           ({
               AccountNumber: client.edisAccountNumber,
               ShareAmount: client.ShareAdjustment,
               CashAmount: client.CashAdjustment
           });
        }

        //service.newBuyBackProgramAction(data, function () {
        //    $modalInstance.close({ reason: "success" });

        //})
        $http.post(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/newBuyBackProgram", data).success(function () {
            alert("success");
            $modalInstance.close({ reason: data });
        }).error(function (data) {
            alert("failed:" + data);
        })


    }
}])


.controller("newRightsIssueController",
["$scope", "corporateActionServices", "$modalInstance", "dateParser", "adviserGetId", "$http", "AppStrings", function ($scope, service, $modalInstance, dateParser, adviserGetId, $http, AppStrings) {
    //service.allCompanies().query(function (data) {
    //    $scope.allCompanies = data;
    //})

    var adviserId = "";
    adviserGetId().then(function (data) {
        adviserId = data;
    })

    service.allTickers().query(function (data) {
        $scope.allTickers = data;
    })


    //service.allClients().query(function (data) {
    //    $scope.allClients = data;
    //})

    $scope.loadAccounts = function () {
        var data = [];
        data = {
            Ticker: $scope.tickerNumber
        }
        $http.post(AppStrings.EDIS_IP + "api/adviser/corporateAction/getAccountByEquity", data)
          .success(function (data) {
              $scope.allAccounts = data;
          }).error(function (data) {
              console.log("Error.............");
          });
    }

    $scope.add = function () {
        var data = {
            ActionName: $scope.actionName,
            AdviserId: adviserId,
            Ticker: $scope.tickerNumber,
            //rightsIssue: $scope.rightsIssue,
            RightsIssueDate: dateParser($scope.issueDate),

            Participants: []
        };

        for (var i = 0; i < $scope.allAccounts.length; i++) {
            var client = $scope.allAccounts[i];
            data.Participants.push
           ({
               AccountNumber: client.edisAccountNumber,
               ShareAmount: client.ShareAdjustment,
               CashAmount: client.CashAdjustment
           });
        }

        //service.newRightsIssueAction(data, function () {
        //    $modalInstance.close({ reason: "success" });

        //})
        $http.post(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/newRightsIssue", data).success(function () {
            alert("success");
            $modalInstance.close({ reason: data });
        }).error(function (data) {
            alert("failed:" + data);
        })


    }
}])


;