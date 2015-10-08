/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />


(function () {
    var app = angular.module("EDIS");
    app.controller("AdviserActivityViewController", ["$scope", "AdviserActivityDBService", "$timeout", function ($scope, DBContext, $timeout) {

        $scope.Administration = DBContext.Administration;


        $scope.Account = DBContext.Account;
        $scope.Portfolio = DBContext.Portfolio;
        $scope.GeneralStats = DBContext.GeneralStats;
        $scope.RecordOptions = function (data) {

            return {
                dataSource: {
                    data: data,
                    pageSize: 5,
                    serverPaging: false,
                    serverSorting: false
                },
                columns: [{
                    field: "AccountNumber",
                    title: "Account Number",
                    width: "40%"
                }, {
                    field: "AccountName",
                    title: "Account Name",
                    width: "30%"
                }, {
                    field: "NumberOfNotes",
                    title: "No. of Notes",
                    width: "30%"
                }]
            }
        };
        $scope.StatOptions = function (data) {
            return {
                dataSource: {
                    data: data,
                    pageSize: 5,
                    serverPaging: false,
                    serverSorting: false
                },
                columns: [{
                    field: "Group",
                    title: "Type",
                    width: "50%"
                }, {
                    field: "Number",
                    title: "Number",
                    width: "50%"
                }]
            };
        };
    }]);
})();