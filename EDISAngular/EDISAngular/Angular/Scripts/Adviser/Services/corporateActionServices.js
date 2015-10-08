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


        //function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorporateAction/IPO"); },
        getClientsBasedOnCompany: function () { return $resource(AppStrings.EDIS_IP + "api/adviser/clientaccounts"); },
        allTickers: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/Ticker"); },
        allocateIPOAction: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorporateAction/IPO/Allocation"); },



        addOtherAction: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/Other"); },
        addIpoAction: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/IPO"); },

      





    }






})