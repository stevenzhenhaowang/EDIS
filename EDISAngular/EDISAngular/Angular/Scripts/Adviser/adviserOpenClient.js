app.controller("adviserOpenClient", ["$scope", "$http", "AppStrings", "AdviserClientAccountsDBService", function ($scope, $http, AppStrings, DBContext) {
    $scope.clientMembers = DBContext.clientMembers;
    $scope.clientMember = $scope.clientMembers[0];
    $scope.noImage = "/Images/no_image.jpg";        


    $scope.openTrTab = function (num) {
        $scope.trTab = num;
        $scope.tab = 4;
    }
    // handle porfolio dropdown menu =============================================================================
    $scope.togglePFDropdown = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.status.pfIsOpen = !$scope.status.pfIsOpen;
        $scope.tab = 2;
        $scope.pfTab = 1;
    };

    $scope.togglePFHover = function ($event) {
        //$event.preventDefault();
        //$event.stopPropagation();

        $scope.status.pfIsOpen = !$scope.status.pfIsOpen;

    };
    // end  porfolio dropdown menu =============================================================================
    // handle transactions dropdown menu =============================================================================
    $scope.toggleTrDropdown = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.status.trIsOpen = !$scope.status.trIsOpen;
        $scope.tab = 4;
        $scope.pfTab = 1;
    };

    $scope.toggleTrHover = function ($event) {
        //$event.preventDefault();
        //$event.stopPropagation();

        $scope.status.trIsOpen = !$scope.status.trIsOpen;

    };
    // end  transactions dropdown menu =============================================================================

    //client searchBox ===========================================================================================
    $scope.selectedClientGroup = {
        ClientGroupNum: '1234',
        ClientGroupName: 'Client Group Not Selected',
        ClientMembers: [{
            Image: "/Images/no_image.jpg",
            FirstName: 'No Client Selected',
            LastName: ''
        }]
    }

    $scope.clientID = $scope.selectedClientGroup.ClientGroupNum;
    //$scope.loadClient = function () {
    //    $scope.clientID = 
    //}

    $scope.searchBoxPlaceholder = "please enter keyword here...";
    $scope.searchingClient = false;
    $scope.openSearchResults = function () {
        $scope.searchingClient = true;
    }

    $scope.closeSearchResults = function (item) {
        if (item != null) {
            var clientDescription = item.FirstName + ' ' + item.LastName + ' in ' + item.ClientGroupName + ' ';
            $scope.searchBoxPlaceholder = item.ClientGroupName;

            var clientGroupNum = item.ClientGroupNum;
            var selectedClients = [];
            angular.forEach($scope.clientMembers, function (client) {
                if (client.ClientGroupNum === clientGroupNum) {
                    selectedClients.push(client);
                }
            });

            $scope.selectedClientGroup = {
                ClientGroupNum: item.ClientGroupNum,
                ClientGroupName: item.ClientGroupName,
                ClientMembers: selectedClients
            }
            //$scope.$apply(function () { });
            //alert(JSON.stringify($scope.selectedClientGroup));
            $scope.clientID = $scope.selectedClientGroup.ClientGroupNum;
            $scope.searchingClient = false;
            $scope.tab = 1;
        } else {
            $scope.searchBoxPlaceholder = "please enter keyword here...";
            $scope.searchingClient = false;
            $scope.tab = 0;
        }
    }
    
    $scope.goToResults = function (keyEvent) {
        if (keyEvent.which === 13)
            alert('Im a lert');
    }
    //client searchBox END =======================================================================================

    $scope.ratings = function (num) {
        var maximum = 5;
        var stars = [];

        for (var i = 1; i <= maximum; i++) {
            if(i<=num){
                stars.push(1);
            } else {
                stars.push(0);
            }
        }
        return stars;
    }
    
}]);

app.controller("adviserClientSearch", ["$scope", "$http", "AppStrings", "AdviserClientAccountsDBService", function ($scope, $http, AppStrings, DBContext) {
    $scope.clientGroups = DBContext.ClientGroups;
}]);