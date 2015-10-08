var app = angular.module("EDIS");
//START OF INT SERVICES
app.factory("clientPortfolioINTGeneralInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/InternationalEquityPortfolio/General");
    }
});
app.factory("clientPortfolioINTCashflowDetails", function ($http, $resource, AppStrings) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/InternationalEquityPortfolio/Cashflow");
    };
});
app.factory("clientPortfolioINTCashflowInfoCompanySpecific", function (AppStrings, $resource) {
    var DBContext = function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/InternationalEquityPortfolio/CashflowDetail");
    }
    return DBContext;
});
app.factory("clientPortfolioINTCompanyProfiles", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/InternationalEquityPortfolio/CompanyProfiles");
    }
})
app.factory("clientPortfolioINTCashflowMonthly", function ($http, $resource, AppStrings) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/InternationalEquityPortfolio/Cashflow");
    }
})
app.factory("clientPortfolioINTEvaluationAgainstModel", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/InternationalEquityPortfolio/EvaluationModel");
    };
})
app.factory("clientPortfolioINTDiversificationInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/InternationalEquityPortfolio/Diversification");
    }
});
app.factory("clientPortfolioINTRatingInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/InternationalEquityPortfolio/Rating");
    }
})
app.factory("clientPortfolioINTQuickStats", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/AustralianEquityPortfolio/QuickStats");
    }
})
//END OF AE SERVICES