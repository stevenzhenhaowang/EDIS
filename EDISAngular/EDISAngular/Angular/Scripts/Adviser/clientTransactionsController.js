(function () {        

    //BEGIN AUSTRALIAN EQUITY TRANSACTIONS ==================================================================================
    app.controller("clientTransactionsAEController", ["$scope", "clientTransactionsDBService", function ($scope, DBContext) {

        $scope.product = DBContext.GetCompanyData();
        $scope.account = DBContext.GetClientTransactionAccount();
        $scope.assetTransactionInfo = DBContext.GetAssetTransactionInfo();
        $scope.buyAnalysis = DBContext.GetBuyAnalysis();
        $scope.sellAnalysis = DBContext.GetSellAnalysis();
        $scope.distInfo = DBContext.GetDistributionInfo();
        $scope.transactionHistory = DBContext.GetTransactionHistory();
    }]);
    //END AUSTRALIAN EQUITY TRANSACTIONS ==================================================================================

    //BEGIN INTERNATIONAL EQUITY TRANSACTIONS ==================================================================================
    app.controller("clientTransactionsINTController", ["$scope", "clientTransactionsDBService", function ($scope, DBContext) {
        $scope.product = DBContext.GetCompanyData();
        $scope.account = DBContext.GetClientTransactionAccount();
        $scope.assetTransactionInfo = DBContext.GetAssetTransactionInfo();
        $scope.buyAnalysis = DBContext.GetBuyAnalysis();
        $scope.sellAnalysis = DBContext.GetSellAnalysis();
        $scope.distInfo = DBContext.GetDistributionInfo();
        $scope.transactionHistory = DBContext.GetTransactionHistory();
    }]);
    //END INTERNATIONAL EQUITY TRANSACTIONS ==================================================================================

    //BEGIN MANAGED INVESTMENTS TRANSACTIONS ==================================================================================
    app.controller("clientTransactionsMIController", ["$scope", "clientTransactionsDBService", function ($scope, DBContext) {

        $scope.product = DBContext.GetCompanyData();
        $scope.account = DBContext.GetClientTransactionAccount();
        $scope.assetTransactionInfo = DBContext.GetAssetTransactionInfo();
        $scope.buyAnalysis = DBContext.GetBuyAnalysis();
        $scope.sellAnalysis = DBContext.GetSellAnalysis();
        $scope.distInfo = DBContext.GetDistributionInfo();
        $scope.transactionHistory = DBContext.GetTransactionHistory();
    }]);
    //END MANAGED INVESTMENTS TRANSACTIONS ==================================================================================

    //BEGIN DIRECT PROPERTY TRANSACTIONS ==================================================================================
    app.controller("clientTransactionsDPController", ["$scope", "clientTransactionsDBService", function ($scope, DBContext) {

        $scope.product = DBContext.GetCompanyData();
        $scope.account = DBContext.GetClientTransactionAccount();
        $scope.assetTransactionInfo = DBContext.GetAssetTransactionInfo();
        $scope.buyAnalysis = DBContext.GetBuyAnalysis();
        $scope.sellAnalysis = DBContext.GetSellAnalysis();
        $scope.distInfo = DBContext.GetDistributionInfo();
        $scope.transactionHistory = DBContext.GetTransactionHistory();
    }]);
    //END DIRECT PROPERTY TRANSACTIONS ==================================================================================

    //BEGIN FIXED INCOME TRANSACTIONS ==================================================================================
    app.controller("clientTransactionsFIController", ["$scope", "clientTransactionsDBService", function ($scope, DBContext) {

        $scope.product = DBContext.GetCompanyData();
        $scope.account = DBContext.GetClientTransactionAccount();
        $scope.assetTransactionInfo = DBContext.GetAssetTransactionInfo();
        $scope.buyAnalysis = DBContext.GetBuyAnalysis();
        $scope.sellAnalysis = DBContext.GetSellAnalysis();
        $scope.distInfo = DBContext.GetDistributionInfo();
        $scope.transactionHistory = DBContext.GetTransactionHistory();
    }]);
    //END FIXED INCOME TRANSACTIONS ==================================================================================

    //BEGIN CASH & TERM DEPOSIT TRANSACTIONS ==================================================================================
    app.controller("clientTransactionsCTDController", ["$scope", "clientTransactionsDBService", function ($scope, DBContext) {

        $scope.product = DBContext.GetCompanyData();
        $scope.account = DBContext.GetClientTransactionAccount();
        $scope.assetTransactionInfo = DBContext.GetAssetTransactionInfo();
        $scope.buyAnalysis = DBContext.GetBuyAnalysis();
        $scope.sellAnalysis = DBContext.GetSellAnalysis();
        $scope.distInfo = DBContext.GetDistributionInfo();
        $scope.transactionHistory = DBContext.GetTransactionHistory();
    }]);
    //END CASH & TERM DEPOSIT TRANSACTIONS ==================================================================================

    //BEGIN MORTGAGE & HOME LOAN TRANSACTIONS ==================================================================================
    app.controller("clientTransactionsMHLController", ["$scope", "clientTransactionsDBService", function ($scope, DBContext) {

        $scope.product = DBContext.GetCompanyData();
        $scope.account = DBContext.GetClientTransactionAccount();
        $scope.assetTransactionInfo = DBContext.GetAssetTransactionInfo();
        $scope.buyAnalysis = DBContext.GetBuyAnalysis();
        $scope.sellAnalysis = DBContext.GetSellAnalysis();
        $scope.distInfo = DBContext.GetDistributionInfo();
        $scope.transactionHistory = DBContext.GetTransactionHistory();
    }]);
    //END MORTGAGE & HOME LOAN TRANSACTIONS ==================================================================================

    //BEGIN MARGIN LENDING TRANSACTIONS ==================================================================================
    app.controller("clientTransactionsMLController", ["$scope", "clientTransactionsDBService", function ($scope, DBContext) {

        $scope.product = DBContext.GetCompanyData();
        $scope.account = DBContext.GetClientTransactionAccount();
        $scope.assetTransactionInfo = DBContext.GetAssetTransactionInfo();
        $scope.buyAnalysis = DBContext.GetBuyAnalysis();
        $scope.sellAnalysis = DBContext.GetSellAnalysis();
        $scope.distInfo = DBContext.GetDistributionInfo();
        $scope.transactionHistory = DBContext.GetTransactionHistory();
    }]);
    //END MARGIN LENDING TRANSACTIONS ==================================================================================

    //BEGIN PERSONAL & CREDIT CARD LOANS TRANSACTIONS ==================================================================================
    app.controller("clientTransactionsCCController", ["$scope", "clientTransactionsDBService", function ($scope, DBContext) {

        $scope.product = DBContext.GetCompanyData();
        $scope.account = DBContext.GetClientTransactionAccount();
        $scope.assetTransactionInfo = DBContext.GetAssetTransactionInfo();
        $scope.buyAnalysis = DBContext.GetBuyAnalysis();
        $scope.sellAnalysis = DBContext.GetSellAnalysis();
        $scope.distInfo = DBContext.GetDistributionInfo();
        $scope.transactionHistory = DBContext.GetTransactionHistory();
    }]);
    //END PERSONAL & CREDIT CARD LOANS TRANSACTIONS ==================================================================================

    //BEGIN INSURANCE TRANSACTIONS ==================================================================================
    app.controller("clientTransactionsINSController", ["$scope", "clientTransactionsDBService", function ($scope, DBContext) {

        $scope.product = DBContext.GetCompanyData();
        $scope.account = DBContext.GetClientTransactionAccount();
        $scope.assetTransactionInfo = DBContext.GetAssetTransactionInfo();
        $scope.buyAnalysis = DBContext.GetBuyAnalysis();
        $scope.sellAnalysis = DBContext.GetSellAnalysis();
        $scope.distInfo = DBContext.GetDistributionInfo();
        $scope.transactionHistory = DBContext.GetTransactionHistory();
    }]);
    //END INSURANCE TRANSACTIONS ==================================================================================
})();