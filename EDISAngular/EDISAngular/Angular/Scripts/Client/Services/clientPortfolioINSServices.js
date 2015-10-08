//START OF Insurance SERVICES
var app = angular.module("EDIS");
app.factory("clientPortfolioINSCashflowDetails", function ($http, $resource, AppStrings) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/InsurancePortfolio/CashflowDetail");
    };
});
app.factory("clientPortfolioINSStatistics", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/InsurancePortfolio/Statistics");
    }
})
app.factory("clientPortfolioINSList", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/InsurancePortfolio/InsuranceList");

    }
})
app.factory("clientPortfolioINSConditions", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/InsurancePortfolio/Conditions");
    }
})
//END OF Insurance SERVICES