var app = angular.module("EDIS");
app.factory("clientPortfolioSummaryDBService", function ($http, $resource, $filter, AppStrings) {
    var DBContext = {
        assetSummary: function () {
            return $resource(AppStrings.EDIS_IP + "api/Client/PortfolioOverview/Summary");
        },
        cashflowSummary: function () {
            return $resource(AppStrings.EDIS_IP + "api/Client/PortfolioOverview/Cashflow");
        },
        bestPerformingSummary: function () {
            return $resource(AppStrings.EDIS_IP + "api/Client/PortfolioOverview/BestPerforming");
        },
        worstPerformingSummary: function () {
            return $resource(AppStrings.EDIS_IP + "api/Client/PortfolioOverview/WorstPerforming");
        },
        general: function () {
            return $resource(AppStrings.EDIS_IP + "api/Client/PortfolioOverview/General");
        },
        portfolioStat: function () {
            return $resource(AppStrings.EDIS_IP + "api/Client/PortfolioOverview/Stastics");
        },
        recentStockData: function () {
            return $resource(AppStrings.EDIS_IP + "api/PortfolioOverview/RecentStock");
        },
        portfolioRating: function () {
            return $resource(AppStrings.EDIS_IP + "api/Client/PortfolioOverview/PortfolioRating");
        }
    };
    return DBContext;
});
app.factory("clientPortfolioGetRegionSummary", function ($http, $resource, AppStrings) {
    var DBContext = {
        getRegionalExposure: function () {
            return $resource(AppStrings.EDIS_IP + "api/Client/PortfolioOverview/RegionalExposure");
        }
    };


    return DBContext;

});
app.factory("clientPortfolioGetSectorInfo", function ($http, $resource, AppStrings) {
    var DBContext = {
        getData: function () {
            return $resource(AppStrings.EDIS_IP + "api/Client/PortfolioOverview/SectorialExposure");
        }
    };

    return DBContext;
});
app.factory("clientPortfolioSummaryGetCashflowInfo", function ($http, $resource, AppStrings) {
    var DBContext = function () {
        return $resource(AppStrings.EDIS_IP + "api/Client/PortfolioOverview/CashflowDetail");
    };
    return DBContext;
});
app.factory("clientBusinessDetailsDBService", function (AppStrings, $resource) {

    var GetTotalAmountFromDataCollection = function (collection) {
        var i = 0;
        angular.forEach(collection, function (value, key) {
            i = i + value.amount;
        });
        return i;
    }

    var DBContext = {
        GetBusinessRevenueData: function () {
            //ws call to retrieve business revenue data
            return $resource(AppStrings.EDIS_IP + "api/client/businessRevenueBrief");
        },
        GetBusinessRevenueData_Details: function () {
            return $resource(AppStrings.EDIS_IP + "api/client/BusinessReenueDetails");
        },
        GetInvestmentPorfolioData: function () {
            //ws call to retrieve investment portfolio data
            return $resource(AppStrings.EDIS_IP + "api/client/PortfolioOverview/InvestmentPortfolio");
        },
        GetDebtInstrumentsData: function () {
            return $resource(AppStrings.EDIS_IP + "api/client/debtInstruments");
        },
        GetWorldMarkets: function () {
            return $resource(AppStrings.EDIS_IP + "api/client/worldMarkets");
        },
        GetCurrencies: function () {
            return $resource(AppStrings.EDIS_IP + "api/client/worldMarkets");
        },
        GetHistoricalRevenueData: function () {
            return $resource(AppStrings.EDIS_IP + "api/client/historicalrevenue");
        },
        GetInsuranceStatistics: function () {
            return $resource(AppStrings.EDIS_IP + "api/client/insuranceStatistics");
        },
        AssetStatistics_GetInvestmentStat: function () {
            return $resource(AppStrings.EDIS_IP + "api/client/investmentstat");
        },
        AssetStatistics_GetLendingStat: function () {
            return $resource(AppStrings.EDIS_IP + "api/client/lendingstat");
        },
        AssetStatistics_GetInsuranceStat: function () {
            return $resource(AppStrings.EDIS_IP + "api/client/InsuranceStat");
        },
        ClientPositionMonitor_GetDetails: function () {
            return $resource(AppStrings.EDIS_IP + "api/client/clientPositionsMonitor");
        },
        ClientDemographics_GetGeneralInfo: function () {
            return $resource(AppStrings.EDIS_IP + "api/client/GeoLocations");
        },
        ClientDemographics_GetDetailsForLocations: function () {
            return $resource(AppStrings.EDIS_IP + "api/client/GeoDetails");
        },
        CompliantFiles_GetGeneralInfo: function () {
            return $resource(AppStrings.EDIS_IP + "api/client/ComplianceDetails");
        }
    };

    return DBContext;
});