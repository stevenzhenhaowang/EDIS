var app = angular.module("EDIS");
//START OF CC SERVICES
app.factory("clientPortfolioCCDiversification", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/CreditCardPortfolio/Diversification");
    }
})
app.factory("clientPortfolioCCCashflowDetails", function ($http, $resource, AppStrings) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/CreditCardPortfolio/Cashflow");
    };
});
app.factory("clientPortfolioCCStatistics", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/CreditCardPortfolio/QuickStats");
    }
})
app.factory("clientPortfolioCCCardDetails", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/CreditCardPortfolio/CardDetails");
    }
});
app.factory("clientPortfolioCCLCCStat", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/CreditCardPortfolio/CardStatistics");
    }
})
//END OF CC SERVICES