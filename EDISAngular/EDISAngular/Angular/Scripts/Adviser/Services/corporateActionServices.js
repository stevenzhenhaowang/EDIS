angular.module("EDIS")
.factory("corporateActionServices", function ($http, $resource, $filter, $q, AppStrings) {
    
    return {
        allClients: function () { return $resource(AppStrings.EDIS_IP + "api/adviser/clientaccounts"); },//clients
        allCompanies: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorporateAction/Company"); },



        existingOtherCorporateActions: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorporateAction/Other"); },
        existingIPOActions: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorporateAction/IPO"); },

        existingReturnOfCapitals: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/ReturnOfCapital"); },
        exsistingReinvestment: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorporateAction/Reinvestment"); },
        existingStockSplit: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorporateAction/StockSplit"); },
        existingBonusIssues: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorporateAction/BonusIssues"); },
        existingBuyBackProgram: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorporateAction/BuyBackProgram"); },
        existingRightsIssues: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorporateAction/RightsIssues"); },

        //function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorporateAction/IPO"); },
        getClientsBasedOnCompany: function () { return $resource(AppStrings.EDIS_IP + "api/adviser/clientaccounts"); },
        allTickers: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/Ticker"); },
        allocateIPOAction: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorporateAction/IPO/Allocation"); },



        addOtherAction: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/Other"); },
        addIpoAction: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/IPO"); },

      
        //newReturnOfCapital: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/newReturnCapital"); },
        newReturnOfCapital: function (data) {      
            $http.post(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/newReturnCapital", data).success(function () {              
                alert("success");

                }).error(function (data) {
                    alert("failed:" + data);
                })


        },

      addnewReinvestmentAction: function (data) {
            $http.post(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/newReinvestment", data).success(function () {
                alert("success");
            }).error(function (data) {
                alert("failed:" + data);
            })
        },

       
       newStockSplitAction: function (data) {
            $http.post(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/newStockSplit", data).success(function () {
                alert("success");
            }).error(function (data) {
                alert("failed:" + data);
            })
        
        },

        newBonusIssueAction: function (data) {
            $http.post(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/newBonusIssue", data).success(function () {
                alert("success");
            }).error(function (data) {
                alert("failed:" + data);
            })

        },

        newBuyBackProgramAction: function (data) {
            $http.post(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/newBuyBackProgram", data).success(function () {
                alert("success");
            }).error(function (data) {
                alert("failed:" + data);
            })

        },

        newRightsIssueAction: function (data) {
            $http.post(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/newRightsIssue", data).success(function () {
                alert("success");
            }).error(function (data) {
                alert("failed:" + data);
            })

        },

        

      


        getAccountByEquity: function (data) {
            $http.post(AppStrings.EDIS_IP + "api/adviser/corporateAction/getAccountByEquity", data).success(function () {
                //alert("success");
            }).error(function (data) {
                alert("failed:" + data);
            })
        },

       

    }






})