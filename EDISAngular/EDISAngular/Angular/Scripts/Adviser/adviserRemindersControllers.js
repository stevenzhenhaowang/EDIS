/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />


(function () {
    var app = angular.module("EDIS");
    app.controller("AdviserRemindersViewController", ["$scope", "AdviserRemindersDBService", function ($scope, DBContext) {
        DBContext.get(function (data) {
            $scope.weekly = data.Weekly;
            $scope.monthly = data.Monthly;
            $scope.yearly = data.Yearly;
            $scope.SecondLayerOptions = function (data) {
                return {
                    dataSource: {
                        data: data,
                        pageSize: 5,
                        serverPaging: false,
                        serverSorting: false
                    },
                    columns: [{
                        field: "accountNumber",
                        title: "Account Number",
                        width: "50%"
                    }, {
                        field: "accountName",
                        title: "Account Name",
                        width: "50%"
                    }]
                }
            };
            $scope.TopLayerOptions = function (data) {
                return {
                    dataSource: {
                        data: data,
                        pageSize: 5,
                        serverPaging: false,
                        serverSorting: false
                    },
                    columns: [{
                        field: "type",
                        title: "Type",
                        width: "50%"
                    }, {
                        field: "number",
                        title: "Number",
                        width: "50%"
                    }]
                };
            };
        })

    }]);
})();