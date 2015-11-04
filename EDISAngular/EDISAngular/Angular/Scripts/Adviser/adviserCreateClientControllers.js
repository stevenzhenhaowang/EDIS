angular.module("EDIS")
.controller("adviserCreateClientController", ["adviserCreateClientService", "$scope", "$http", "AppStrings", "adviserGetId",
    "adviserGetClientGroups", function (service, $scope, $http, AppStrings, adviserId, clientGroups) {
        adviserId().then(function (value) {
            $scope.adviserId = value;
        });
        clientGroups().then(function (value) {
            $scope.existingGroups = value;
        })
        $scope.reset = function () {
            $scope.collection = {
                personProfile: {
                    isGroupLeader: "false",
                },
                entityProfile: {},
                newGroup: {},
                riskProfile: {
                    questions: service.getRiskQuestions,
                    levels: service.getRiskLevels
                },
            };
            $scope.entityTypes = service.getEntityTypes;
            //$scope.existingGroupsss = service.getExistingGroups;
        }


        $scope.save = function () {
            var data = {};
            if ($scope.collection.accountType === "Person") {
                
                data = {
                    firstName: $scope.collection.personProfile.firstName,
                    lastName: $scope.collection.personProfile.lastName,
                    middleName: $scope.collection.personProfile.middleName,
                    email: $scope.collection.personProfile.email,
                    contactPhone: $scope.collection.personProfile.phone,
                    isGroupLeader: $scope.collection.personProfile.isGroupLeader === "true",
                    newGroupAccountName: $scope.collection.personProfile.groupName,
                    newGroupAlias: $scope.collection.personProfile.groupAlias,
                    newGroupAdviserNumber: $scope.adviserId,
                    existingGroupId: $scope.collection.personProfile.isGroupLeader === "true" ? "" : $scope.collection.personProfile.group.id,
                    riskProfile: {
                        shortTermGoal1: $scope.collection.riskProfile.questions[0].answer,
                        medTermGoal1: $scope.collection.riskProfile.questions[1].answer,
                        longTermGoal1: $scope.collection.riskProfile.questions[2].answer,
                        comments: $scope.collection.riskProfile.questions[3].answer,
                        retirementAge: $scope.collection.riskProfile.questions[4].answer,
                        retirementIncome: $scope.collection.riskProfile.questions[5].answer,
                        incomeSource: $scope.collection.riskProfile.questions[6].answer,
                        investmentProfile: $scope.collection.riskProfile.questions[7].answer,
                        investmentObjective1: $scope.collection.riskProfile.questions[8].answer,
                        investmentObjective2: $scope.collection.riskProfile.questions[9].answer,
                        investmentObjective3: $scope.collection.riskProfile.questions[10].answer,
                        investmentTimeHorizon: $scope.collection.riskProfile.questions[11].answer,
                        investmentKnowledge: $scope.collection.riskProfile.questions[12].answer,
                        riskAttitude: $scope.collection.riskProfile.questions[13].answer,
                        capitalLossAttitude: $scope.collection.riskProfile.questions[14].answer,
                        shortTermTrading: $scope.collection.riskProfile.questions[15].answer,
                        shortTermAssetPercent: $scope.collection.riskProfile.questions[16].answer,
                        shortTermEquityPercent: $scope.collection.riskProfile.questions[17].answer,
                        riskLevel: $scope.collection.riskProfile.level.id

                    }
                };

                $http.post(AppStrings.EDIS_IP + "api/Personclient/Create", data).success(function () {
                    alert("success");
                }).error(function (data) {
                    alert("failed:" + data);
                })
            } else {
                data = {
                    nameOfEntity: $scope.collection.entityProfile.nameOfEntity,
                    typeOfEntity: $scope.collection.entityProfile.typeOfEntity,
                    abn: $scope.collection.entityProfile.abn,
                    acn: $scope.collection.entityProfile.acn,
                    contactPhone: $scope.collection.entityProfile.phone,
                    email: $scope.collection.entityProfile.email,
                    existingGroupId: $scope.collection.entityProfile.group.id,
                    riskProfile: {
                        shortTermGoal1: $scope.collection.riskProfile.questions[0].answer,
                        medTermGoal1: $scope.collection.riskProfile.questions[1].answer,
                        longTermGoal1: $scope.collection.riskProfile.questions[2].answer,
                        comments: $scope.collection.riskProfile.questions[3].answer,
                        retirementAge: $scope.collection.riskProfile.questions[4].answer,
                        retirementIncome: $scope.collection.riskProfile.questions[5].answer,
                        incomeSource: $scope.collection.riskProfile.questions[6].answer,
                        investmentProfile: $scope.collection.riskProfile.questions[7].answer,
                        investmentObjective1: $scope.collection.riskProfile.questions[8].answer,
                        investmentObjective2: $scope.collection.riskProfile.questions[9].answer,
                        investmentObjective3: $scope.collection.riskProfile.questions[10].answer,
                        investmentTimeHorizon: $scope.collection.riskProfile.questions[11].answer,
                        investmentKnowledge: $scope.collection.riskProfile.questions[12].answer,
                        riskAttitude: $scope.collection.riskProfile.questions[13].answer,
                        capitalLossAttitude: $scope.collection.riskProfile.questions[14].answer,
                        shortTermTrading: $scope.collection.riskProfile.questions[15].answer,
                        shortTermAssetPercent: $scope.collection.riskProfile.questions[16].answer,
                        shortTermEquityPercent: $scope.collection.riskProfile.questions[17].answer,
                        riskLevel: $scope.collection.riskProfile.level.id
                    }
                };
                $http.post(AppStrings.EDIS_IP + "api/Entityclient/Create", data).success(function () {
                    alert("success");
                }).error(function (data) {
                    alert("failed:" + data);
                })
            }
        }
        $scope.reset();
    }])

.controller("adviserCreateNewClientAccountController", function ($http, $scope, AppStrings, adviserGetClientGroups, adviserGetClientAccountTypes) {
    adviserGetClientGroups().then(function (data) {
        $scope.groups = data;
        //$scope.clients = data;
    });
    adviserGetClientAccountTypes().then(function (data) {
        $scope.accountTypes = data;
    })
    
    $scope.loadClients = function () {
        var data = [];
        data = {
            clientGroup : $scope.clientGroup
        }
        $http.post(AppStrings.EDIS_IP + "api/adviser/getAllClientGroups", data)
                .success(function (data) {
                    $scope.clients = data;
                }).error(function (data) {
                    console.log("Error.............");
                });

    }


    $scope.submit = function () {
        var data = {};
        data = {
            clientGroup: $scope.clientGroup,
            client: $scope.selectedClient,
            accountType: $scope.selectedAccountType,
            accountName: $scope.accountName
        };

        $http.post(AppStrings.EDIS_IP + "api/adviser/createClientAccount", data).success(function () {
            alert("success");
        }).error(function (data) {
            alert("failed:" + data);
        })
    }
})

.controller("adviserCreateNewClientGroupAccountController", function ($http, $scope, AppStrings, adviserGetClientGroups, adviserGetClientAccountTypes) {
    adviserGetClientGroups().then(function (data) {
        $scope.groups = data;
        //$scope.clients = data;
    });
    adviserGetClientAccountTypes().then(function (data) {
        $scope.accountTypes = data;
    })

    $scope.submit = function () {
        var data = {};
        data = {
            clientGroup: $scope.clientGroup,
            accountType: $scope.selectedAccountType,
            accountName: $scope.accountName
        }

        $http.post(AppStrings.EDIS_IP + "api/adviser/createGroupAccount", data).success(function () {
            alert("success");
        }).error(function (data) {
            alert("failed:" + data);
        })
    }
})


.controller("insertAssetsDataController", function ($http, $scope, AppStrings) {
    

    $scope.submit = function () {
        

        $http.get(AppStrings.EDIS_IP + "api/adviser/insertAssetsData").success(function () {
            alert("success");
        }).error(function (data) {
            alert("failed:" + data);
        })
    }
})


//.controller("getAllExistingGroupController1", ["dbContextServices", "$scope", "$http", "AppStrings", "adviserGetId",
//    "adviserGetClientGroups", function (service, $scope, $http, AppStrings, adviserId, clientGroups) {
//        $scope.reset = function () {
            
//            service.getAllExistingGroup().query(function (data) {
//                $scope.getAllExistingGroup = data;
//            })
//        }
//    }])


//    .controller("getAllExistingGroupController", function (service, $scope, $http, AppStrings, adviserId, clientGroups) {
//        $scope.whatever = [{ name: "check" }, {name:"get"}];
//    })


//.controller("getAllExistingGroupController2", function ($http, $resource, $filter, AppStrings,$scope) {
//    return function () {
//        console.log("++++++++++++++++++++++++++++++++++++++");
//        $resource(AppStrings.EDIS_IP + "api/Personclient/GetAllGlientGroup");
//    }
//})

//(function(){
//    var app = angular.module("EDIS");
//    app.controller("getAllExistingGroupController3", ["$scope", "getAllExistingGroup2", function ($scope, DBContext) {
//        DBContext().get(function (data) {
//            $scope.group = data;
//        })

//    }]);
//})();

