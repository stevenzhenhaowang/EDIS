//START OF CTD SERVICES
var app = angular.module("EDIS");
app.factory("clientPortfolioCTDGeneralInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/CashTermDepositPortfolio/General");
    }
});
app.factory("clientPortfolioCTDCashflowDetails", function ($http, $resource, AppStrings) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/CashTermDepositPortfolio/Cashflow");
    }
});
app.factory("clientPortfolioCTDCashflowInfoDepositSpecific", function (AppStrings, $resource) {
    var DBContext = function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/CashTermDepositPortfolio/CashflowDetailed");
    }
    return DBContext;
});
app.factory("clientPortfolioCTDDepositProfiles", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/CashTermDepositPortfolio/Profiles");
    }
})

app.factory("clientPortfolioCTDDiversificationInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/CashTermDepositPortfolio/Diversifications");
    }
});
app.factory("clientPortfolioCTDPriceChartData", function ($http, $resource, AppStrings) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/CashTermDepositPortfolio/Price");
    }
});
app.factory("clientPortfolioCTDStatistics", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/CashTermDepositPortfolio/Stats");
    }
});
app.factory("clientPortfolioCTDRatingInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/CashTermDepositPortfolio/Rating");
    }
});
//END OF CTD SERVICES