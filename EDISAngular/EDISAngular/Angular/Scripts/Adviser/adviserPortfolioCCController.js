/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />


(function () {
    var app = angular.module("EDIS");
    app.controller("adviserPortfolioCCPieChartController", ["$scope", "adviserPortfolioCCDiversification", function ($scope, DBContext) {
        DBContext().query(function (data) {
            $scope.data = data;
            $scope.datasource = new kendo.data.DataSource({
                data: $scope.data
            })
        })
    }])
    app.controller("adviserPortfolioCCCashflowController", ["$scope", "adviserPortfolioCCCashflowDetails", function ($scope, DBContext) {
         DBContext().get(function (data) {
             $scope.data = data;
        })
    }])
    app.controller("adviserPortfolioCCStatistics", ["$scope", "adviserPortfolioCCStatistics", function ($scope, DBContext) {
        DBContext().get(function (data) {
            $scope.data = data;
        })
    }])
    app.controller("adviserPortfolioCCCardDetails", ["$scope", "adviserPortfolioCCCardDetails", function ($scope, DBContext) {
        DBContext().query(function (data) {
            $scope.data = data;
        })
    }])
    app.controller("adviserPortfolioCCCCStatic", ["$scope", "adviserPortfolioCCLCCStat", function ($scope, DBContext) {
        DBContext().query(function (data) {
            $scope.data = data;
        })
    }])



})();