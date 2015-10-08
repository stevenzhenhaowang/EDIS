var app = angular.module("EDIS");
//START OF MI SERVICES 
app.factory("clientPortfolioMIGeneralInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/ManagedInvestmentPortfolio/General");
    }
});
app.factory("clientPortfolioMICashflowDetails", function ($http, $resource, AppStrings) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/ManagedInvestmentPortfolio/Cashflow");
    };
});
app.factory("clientPortfolioMICashflowInfoCompanySpecific", function (AppStrings, $resource) {
    var DBContext = function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/ManagedInvestmentPortfolio/CashflowDetail");
    }
    return DBContext;
});
app.factory("clientPortfolioMICompanyProfiles", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/ManagedInvestmentPortfolio/CompanyProfiles");
    }
})
app.factory("clientPortfolioMICashflowMonthly", function ($http, $resource, AppStrings) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/ManagedInvestmentPortfolio/Cashflow");
    }
})

app.factory("clientPortfolioMIDiversificationInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/ManagedInvestmentPortfolio/Diversification");
    }
});
app.factory("clientPortfolioMIRatingInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/ManagedInvestmentPortfolio/Rating");
    }
})

app.factory("clientPortfolioMIQuickStats", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/ManagedInvestmentPortfolio/QuickStats");
    }
})
app.factory("clientPortfolioMIDiversificationGroupSummary", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/ManagedInvestmentPortfolio/DiversificationGroup");
    }
});
//END OF MI SERVICES 