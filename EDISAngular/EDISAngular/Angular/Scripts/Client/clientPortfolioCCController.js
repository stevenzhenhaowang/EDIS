/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />


(function () {
    var app = angular.module("EDIS");
    app.controller("clientPortfolioCCPieChartController", ["$scope", "clientPortfolioCCDiversification", function ($scope, DBContext) {
        DBContext().query(function (data) {
            $scope.data = data;
            $scope.datasource = new kendo.data.DataSource({
                data: $scope.data
            })
        })
    }])
    app.controller("clientPortfolioCCCashflowController", ["$scope", "clientPortfolioCCCashflowDetails", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }])
    app.controller("clientPortfolioCCStatistics", ["$scope", "clientPortfolioCCStatistics", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }])
    app.controller("clientPortfolioCCCardDetails", ["$scope", "clientPortfolioCCCardDetails", function ($scope, DBContext) {
        DBContext().query(function (data) {
            $scope.data = data;
        })
    }])
    app.controller("clientPortfolioCCCCStatic", ["$scope", "clientPortfolioCCLCCStat", function ($scope, DBContext) {
        DBContext().query(function (data) {
            $scope.data = data;
        })
    }])
})();