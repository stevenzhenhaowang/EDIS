var app = angular.module("EDIS");
//START OF AE SERVICES
app.factory("clientPortfolioAEGeneralInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/AustralianEquityPortfolio/General");
    }
});
app.factory("clientPortfolioAECashflowDetails", function ($http, $resource, AppStrings) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/AustralianEquityPortfolio/Cashflow");
    };
});
app.factory("clientPortfolioAECashflowInfoCompanySpecific", function (AppStrings, $resource) {
    var DBContext = function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/AustralianEquityPortfolio/CashflowDetail");
    };
    return DBContext;
});

app.factory("clientPortfolioAECompanyProfiles", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/AustralianEquityPortfolio/CompanyProfiles");
    }
})



app.factory("clientPortfolioAEEvaluationAgainstModel", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/AustralianEquityPortfolio/EvaluationModel");
    };
})
app.factory("clientPortfolioAEDiversificationInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/AustralianEquityPortfolio/Diversification");
    };
});
app.factory("clientPortfolioAERatingInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/AustralianEquityPortfolio/Rating");
    }
})
app.factory("clientPortfolioAEQuickStats", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/AustralianEquityPortfolio/QuickStats");
    }
})
//END OF AE SERVICES