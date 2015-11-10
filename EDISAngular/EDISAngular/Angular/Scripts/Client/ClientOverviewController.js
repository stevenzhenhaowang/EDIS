app.controller("clientOverview", ["$scope", "$http", "AppStrings", "ClientDashboardDetailsDBService", function ($scope, $http, AppStrings, DBContext) {
    $scope.edisIP = AppStrings.EDIS_IP;
    
    // Data for Investment Portfolio Stats
    DBContext.GetInvestmentPorfolioData().get(function (data) {
        $scope.investmentPortfolio = data;
        $scope.investmentPortfolioDataKendo = new kendo.data.DataSource({
            data: $scope.investmentPortfolio.data
        });
    })



}]);

