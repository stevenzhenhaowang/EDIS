
(function () {
    var app = angular.module("EDIS");
    app.controller("clientCorporateActionViewController", function ($scope, $http, AppStrings) {

        $scope.allPendingActions = [];
            $http.get(AppStrings.EDIS_IP + "api/Client/CorperateAction/AllPendingActions")
              .success(function (data) {
                  $scope.allPendingActions = data;
              }).error(function (data) {
                  alert("Bad request");
              });

        //api/client/corporateAction/acceptactions
        //api/client/corporateAction/rejectactions
        $scope.accept = function (data) {
            console.log(data.ActionId);
            var pass = data.ActionId.toString();
            $http.post(AppStrings.EDIS_IP + "api/client/corporateAction/acceptactions", { actionId: pass })
              .success(function () {
                  alert("success");
                  $scope.allPendingActions = [];
                  $http.get(AppStrings.EDIS_IP + "api/Client/CorperateAction/AllPendingActions")
              .success(function (data) {
                  $scope.allPendingActions = data;
              }).error(function (data) {
                  alert("Bad request");
              });
              }).error(function () {
                  alert("Bad request");
              });
        };
        $scope.reject = function (data) {
            console.log(data.ActionId);
            console.log(data.ActionId.toString());
            $http.post(AppStrings.EDIS_IP + "api/client/corporateAction/rejectactions", { actionId: (data.ActionId) })
              .success(function () {
                  alert("success");
                  $scope.allPendingActions = [];
                  $http.get(AppStrings.EDIS_IP + "api/Client/CorperateAction/AllPendingActions")
              .success(function (data) {
                  $scope.allPendingActions = data;
              }).error(function (data) {
                  alert("Bad request");
              });
              }).error(function () {
                  alert("Bad request");
              });
        };


        $scope.AllActions = [];
        $http.get(AppStrings.EDIS_IP + "api/Client/CorperateAction/AllActions")
              .success(function (data) {
                  $scope.AllActions = data;
              }).error(function (data) {
                  alert("Bad request");
              });



    })

})();