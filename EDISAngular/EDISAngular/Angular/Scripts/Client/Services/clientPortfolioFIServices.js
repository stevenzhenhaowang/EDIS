var app = angular.module("EDIS");
//START OF FI SERVICES
app.factory("clientPortfolioFIGeneralInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/FixedIncomePortfolio/General");

    }
});
app.factory("clientPortfolioFICashflowDetails", function (AppStrings, $resource, $filter) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/FixedIncomePortfolio/Cashflow");
    };
});
app.factory("clientPortfolioFICashflowInfoIncomeSpecific", function (AppStrings, $resource) {
    var DBContext = function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/FixedIncomePortfolio/CashflowDetailed");

    }
    return DBContext;
});
app.factory("clientPortfolioFIIncomeProfiles", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/FixedIncomePortfolio/Profiles");
    }
})
app.factory("clientPortfolioFIDiversificationInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/FixedIncomePortfolio/Diversifications");
    };
});
app.factory("clientPortfolioFIRatingInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/FixedIncomePortfolio/Rating");
    };
});
app.factory("clientPortfolioFIQuickStats", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/FixedIncomePortfolio/Stats");
    };
})
app.factory("clientPortfolioFIStatistics", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/FixedIncomePortfolio/Stats");
    }
});
app.factory("clientPortfolioFIPriceChartData", function (AppStrings, $resource, $filter) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/FixedIncomePortfolio/Price");
    };
});
//END OF FI SERVICES 