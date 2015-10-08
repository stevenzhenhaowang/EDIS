var app = angular.module("EDIS");
//START OF MHL SERVICES
app.factory("clientPortfolioMHLGeneralInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/MortgageInvestmentPortfolio/General");
    }
});
app.factory("clientPortfolioMHLCashflowDetails", function ($http, $resource, AppStrings) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/MortgageInvestmentPortfolio/Cashflow");
    };
});

app.factory("clientPortfolioMHLStatistics", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/MortgageInvestmentPortfolio/Stats");
    }
});
app.factory("clientPortfolioMHLRatingInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/MortgageInvestmentPortfolio/Rating");
    }
});
app.factory("clientPortfolioMHLInvestmentProfiles", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/MortgageInvestmentPortfolio/Profiles");
    }
});
app.factory("clientPortfolioMHLCashflowInfoPropertySpecific", function (AppStrings, $resource) {
    var DBContext = function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/MortgageInvestmentPortfolio/CashflowDetailed");
    }
    return DBContext;
});
//END OF MHL SERVICES