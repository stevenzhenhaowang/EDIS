app.controller("adviserProfile", ["$scope", "$http", "AppStrings", "AdivserProfileDBService", function ($scope, $http, AppStrings, DBContext) {
    //$http.get($scope.edisIP + "api/adviseroverview?id=0")
    //.success(function (response) { $scope.adviserDescription = response; });
    //.fail(function () { $window.alert('webservice 0 unavailable'); });

//    $http.get($scope.edisIP + "api/adviseroverview?id=1")
//.success(function (response) { $scope.dealerGroup = response; });

    //$scope.adviser = {};

    //$scope.adviser = DBContext.GetAdviserProfile();
    DBContext.GetAdviserProfile().success(function (data) {
        $scope.adviser = data;
        var adviser = $scope.adviser;
        //$scope.adviseraddress = $scope.adviser.AddressLn1;
        $scope.adviseraddress = (adviser.Addressln1 ? adviser.Addressln1 + '<br />' : "") +
            (adviser.Addressln2 ? adviser.Addressln2 + '<br />' : "") +
            (adviser.Addressln3 ? adviser.Addressln3 + '<br />' : "") +
           (adviser.Suburb + ', ' + adviser.State + ', ' + adviser.Postcode + '<br />') + adviser.Country;
    });


    $scope.adviserAdditional = DBContext.GetAdviserAdditionalInfo();
    $scope.adviserDescription = $scope.adviserAdditional.adviserDescription;
        
    $scope.adviserimage = DBContext.GetAdviserImage();

    $scope.additionalInfo = $scope.adviserAdditional.additionalInfo;
        
    $scope.dealerGroup = DBContext.GetAdviserDealerGroup().Adviser_DealerGroupDetails;
    $scope.dealerGroupAddress = ($scope.dealerGroup.AddressLn1 ? ($scope.dealerGroup.AddressLn1 + '<br />') : "") +
        ($scope.dealerGroup.AddressLn2 ? $scope.dealerGroup.AddressLn2 + '<br />' : "") +
        ($scope.dealerGroup.AddressLn3 ? $scope.dealerGroup.AddressLn3 + '<br />' : "") +
        ($scope.dealerGroup.Suburb + ', ' + $scope.dealerGroup.State + ', ' + $scope.dealerGroup.PostCode + '<br />') + $scope.dealerGroup.Country;

    $scope.adviserManagementDetails = DBContext.GetAdviserManagementDetails();


    $scope.rating = $scope.adviserAdditional.adviserRating;
    //TODO
    $scope.ratings = function () {
        return new Array(parseInt($scope.rating));
    }
    

}]);
app.controller("adviserBookmarks", ["$scope", "$window", "AdivserProfileDBService", function ($scope, $window, DBContext) {
    $scope.bookmarkList = DBContext.GetAdviserBookmarks();
    $scope.openLink = function (url) {
        $window.open(url);
    }
}]);

