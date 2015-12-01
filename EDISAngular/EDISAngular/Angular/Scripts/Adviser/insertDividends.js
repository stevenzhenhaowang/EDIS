(function () {
    var app = angular.module("EDIS");
    app.controller("insertDividendsViewController", ["$http", "$scope", "dividendServices", "adviserGetClientGroups", "AppStrings", "dateParser", function ($http, $scope, service, adviserGetClientGroups, AppStrings, dateParser) {


    adviserGetClientGroups().then(function (data) {
        $scope.groups = data;
    });

        service.allTickers().query(function (data) {
            $scope.allTickers = data;
        })

        service.bondTickers().query(function (data) {
            $scope.bondTickers = data;
        })

        service.allProperties().query(function (data) {
            $scope.properties = data;
        })

        $scope.loadAccounts = function () {
            var data = [];
            data = {
                clientGroup: $scope.clientGroup
            }
            $http.post(AppStrings.EDIS_IP + "api/adviser/getAllAccountForGroup", data)
            .success(function (data) {
                $scope.accounts = data;
            }).error(function (data) {
                console.log("Error.............");
            });
        }



        $scope.save = function () {
            var data = {};
            if ($scope.collection.incomeType === "Coupon") {
                data = {
                    Ticker: $scope.collection.coupon.Ticker.tickerNumber,
                    Amount: $scope.collection.coupon.Amount,
                    PaymentOn:dateParser($scope.collection.coupon.Paymenton),
                    Account: $scope.collection.selectedAccount,
                   // DividendType : "coupon",
                };
                $http.post(AppStrings.EDIS_IP + "api/adviser/CouponDividend", data).success(function () {
                    alert("Successs");
                }).error(function (data) {
                    alert("failed:" + data);
                })
            }


            if ($scope.collection.incomeType === "Dividend") {
                data = {
                    Ticker: $scope.collection.dividend.Ticker.tickerNumber,
                    Amount: $scope.collection.dividend.Amount,
                    PaymentOn: dateParser($scope.collection.dividend.Paymenton),
                    AddtionalInfo: $scope.collection.dividend.Franking,
                    Account: $scope.collection.selectedAccount,
                    // DividendType : "coupon",
                };
                $http.post(AppStrings.EDIS_IP + "api/adviser/JustDividend", data).success(function () {
                    alert("Successs");
                }).error(function (data) {
                    alert("failed:" + data);
                })
            
            }


            if ($scope.collection.incomeType === "Interest") {

                data = {
                    
                    Amount: $scope.collection.interest.Amount,
                    PaymentOn: dateParser($scope.collection.interest.Paymenton),
                    Account: $scope.collection.selectedAccount,
                    // DividendType : "coupon",
                };
                $http.post(AppStrings.EDIS_IP + "api/adviser/InterestDividend", data).success(function () {
                    alert("Successs");
                }).error(function (data) {
                    alert("failed:" + data);
                })
            
            }

            if ($scope.collection.incomeType === "Rental") {
                //item.id as item.FullAddress for item in properties
                data = {
                    AddtionalInfo: $scope.collection.rental.property,
                    Amount: $scope.collection.rental.Amount,
                    PaymentOn: dateParser($scope.collection.rental.Paymenton),
                    Account: $scope.collection.selectedAccount,
                    // DividendType : "coupon",
                };
                $http.post(AppStrings.EDIS_IP + "api/adviser/RentalDividend", data).success(function () {
                    alert("Successs");
                }).error(function (data) {
                    alert("failed:" + data);
                })
            }


        }



    }]);


    //api/adviser/allProperties
    app.factory("dividendServices", function ($http, $resource, $filter, $q, AppStrings) {
        return {
            allTickers: function () { return $resource(AppStrings.EDIS_IP + "api/Adviser/CorprateAction/Ticker"); },
            bondTickers: function () { return $resource(AppStrings.EDIS_IP + "api/adviser/allBondTickers");},
            allProperties: function () { return $resource(AppStrings.EDIS_IP + "api/adviser/allProperties"); },





        }
    })
})();