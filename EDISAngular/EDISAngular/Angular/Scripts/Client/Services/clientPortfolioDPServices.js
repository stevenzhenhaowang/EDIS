//START OF DP SERVICES
var app = angular.module("EDIS");
app.factory("clientPortfolioDPGeneralInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/DirectPropertyPortfolio/General");
    }
});

app.factory("clientPortfolioDPCashflowInfoPropertySpecific", function (AppStrings, $resource) {
    var DBContext = function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/DirectPropertyPortfolio/CashflowDetail");
    };
    return DBContext;
});

app.factory("clientPortfolioDPCashflowMonthly", function ($http, $resource, AppStrings) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/DirectPropertyPortfolio/Cashflow");
    }
})

app.factory("clientPortfolioDPRatingInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/DirectPropertyPortfolio/Rating");
    }
})

app.factory("clientPortfolioDPQuickStats", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/DirectPropertyPortfolio/QuickStats");
    }
})

app.factory("clientPortfolioDPgeoInfo", function (AppStrings, $resource) {
    return function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/DirectPropertyPortfolio/GeoInfo");
    }
});
//END OF DP SERVICES