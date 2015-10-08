app.controller("adviserOverview", ["$scope", "$http", "AppStrings", "AdviserBusinessDetailsDBService", function ($scope, $http, AppStrings, DBContext) {
    $scope.edisIP = AppStrings.EDIS_IP;


    DBContext.GetBusinessRevenueData().get(function (data) {
        $scope.businessRev = data;
        $scope.businessRevDataKendo = new kendo.data.DataSource({
            data: $scope.businessRev.data
        });
    })

    // Data for Investment Portfolio Stats
    DBContext.GetInvestmentPorfolioData().get(function (data) {
        $scope.investmentPortfolio = data;
        $scope.investmentPortfolioDataKendo = new kendo.data.DataSource({
            data: $scope.investmentPortfolio.data
        });
    })



    // Data for Debt Instrument Stats
    DBContext.GetDebtInstrumentsData().get(function (data) {
        $scope.debtInstruments = data;
        $scope.debtInstrumentsDataKendo = new kendo.data.DataSource({
            data: $scope.debtInstruments.data
        });
    })
    DBContext.GetInsuranceStatistics().get(function (data) {
        $scope.insuranceStatistics = data;
        $scope.insuranceDataKendo = new kendo.data.DataSource({
            data: $scope.insuranceStatistics.data
        });



    })

    DBContext.GetHistoricalRevenueData().get(function (data) {
        $scope.historicalRevenue = data;
    })

    DBContext.GetWorldMarkets().query(function (data) {
        $scope.worldMarkets = data;
    })
    DBContext.GetCurrencies().query(function (data) {
        $scope.currencies = data;
    })





}]);

