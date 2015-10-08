

app.controller("adviserClientSummary", ["$scope", "$http", function ($scope, $http) {
    //$scope.edisIP = AppStrings.EDIS_IP;

    //DEBUG- Assign Api Gets when available
//    $http.get($scope.edisIP + "api/adviserClientSummary")
//.success(function (response) { $scope.clientData = response; });

    // Dummy Data for View By Client ***************************************
    $scope.clientSummary = {};
    $scope.clientSummary.data = [
        { EDIS_Account: '20130001', EDIS_Account_Name: 'Alfa Romeo Pty Ltd<Alfa Superannuation A/C>', Net_Cost_Value_$: 250000, Market_Value_$: 285000, Profit_Loss_$: 35000, Profile_Loss_percent: 14, Compliant_Status: 'Compliant', Reminders: 'No Reminders', Client_Feedback: 'Excellent', Ongoing_Revenue: 2850 },
        { EDIS_Account: '20130002', EDIS_Account_Name: 'Alfa Ferrari Pty Ltd<Alfa Superannuation A/C>', Net_Cost_Value_$: 250000, Market_Value_$: 285000, Profit_Loss_$: 35000, Profile_Loss_percent: 14, Compliant_Status: 'Compliant', Reminders: 'No Reminders', Client_Feedback: 'Excellent', Ongoing_Revenue: 2850 },
        { EDIS_Account: '20130003', EDIS_Account_Name: 'Alfa Porche Pty Ltd<Alfa Superannuation A/C>', Net_Cost_Value_$: 250000, Market_Value_$: 285000, Profit_Loss_$: 35000, Profile_Loss_percent: 14, Compliant_Status: 'Compliant', Reminders: 'No Reminders', Client_Feedback: 'Excellent', Ongoing_Revenue: 2850 }
    ];

    $scope.clientSummary.data.detailsByAssetClass = [
        { EDIS_Account: '20130001', AssetClass: 'Australian Equity', Ticker: 'ASX', CompanyName: 'Australian Stock Exchange', Units: '23295', MarketValue: '55000', NetCostValue: '250000', ProfitLossDollar: '5000', ProfitLossPC: '10', Complaint: 'Compliant', Reminders: 'No Reminders', Feedback: 'Excellent', OngoingRev: '500' },        
        { EDIS_Account: '20130002', AssetClass: 'International Equity', Ticker: 'ASX', CompanyName: 'Australian Stock Exchange', Units: '23295', MarketValue: '55000', NetCostValue: '250000', ProfitLossDollar: '5000', ProfitLossPC: '10', Complaint: 'Compliant', Reminders: 'No Reminders', Feedback: 'Excellent', OngoingRev: '500' },
        { EDIS_Account: '20130003', AssetClass: 'Managed Investment', Ticker: 'ASX', CompanyName: 'Australian Stock Exchange', Units: '23295', MarketValue: '55000', NetCostValue: '250000', ProfitLossDollar: '5000', ProfitLossPC: '10', Complaint: 'Compliant', Reminders: 'No Reminders', Feedback: 'Excellent', OngoingRev: '500' }
    ];

   
     
    //View By Client **********************************************************************************
    $scope.viewByClientGridOptions = {
        dataSource: {
            data: $scope.clientSummary.data,
            pageSize: 5,
            serverPaging: true,
            serverSorting: true
        },
        sortable: true,
        pageable: true,
        detailTemplate: kendo.template($("#templateByClient").html()),
        columns: [
            { field: "EDIS_Account", title: "EDIS Account", width: "120px" },
            { field: "EDIS_Account_Name", title: "EDIS Account Name", width: "180px" },
            { field: "Net_Cost_Value_$", title: "Net Cost Value_$", width: "120px" },
            { field: "Market_Value_$", title: "Market_Value_$", width: "120px" },
            { field: "Profit_Loss_$", title: "Profit_Loss_$", width: "120px" },
            { field: "Profile_Loss_percent", title: "Profile_Loss_percent", width: "120px" },
            { field: "Compliant_Status", title: "Compliant_Status", width: "120px" },
            { field: "Reminders", title: "Reminders", width: "120px" },
            { field: "Client_Feedback", title: "Client_Feedback", width: "120px" },
            { field: "Ongoing_Revenue", title: "Ongoing_Revenue", width: "120px" }
        ]
    }
    $scope.detailByClientGridOptions = function (dataItem) {
        return {
            dataSource: {
                data: $scope.clientSummary.data.detailsByAssetClass,
                pageSize: 5,
                serverPaging: true,
                serverSorting: true,
                filter: { field: "EDIS_Account", operator: "eq", value: dataItem.EDIS_Account }
            },
            scrollable: false,
            sortable: true,
            pageable: true,
            columns: [
                { field: "AssetClass", title: "AssetClass", width: "120px" },
                { field: "Ticker", title: "Ticker", width: "120px" },
                { field: "CompanyName", title: "CompanyName", width: "120px" },
                { field: "Units", title: "Units", width: "120px" },
                { field: "MarketValue", title: "MarketValue", width: "120px" },
                { field: "NetCostValue", title: "NetCostValue", width: "120px" },
                { field: "ProfitLossDollar", title: "ProfitLossDollar", width: "120px" }
            ]
            
        };
    };

    //View By Asset **********************************************************************************
    $scope.viewByAssetGridOptions = {
        dataSource: {
            data: $scope.clientSummary.data.detailsByAssetClass,
            pageSize: 5,
            serverPaging: true,
            serverSorting: true
        },
        sortable: true,
        pageable: true,
        detailTemplate: kendo.template($("#templateByClient").html()),
        columns: [
            { field: "AssetClass", title: "EAssetClass", width: "120px" },
            //{ field: "EDIS_Account", title: "EDIS_Account", width: "180px" },
            { field: "Ticker", title: "Ticker", width: "120px" },
            { field: "CompanyName", title: "CompanyName", width: "120px" },
            { field: "Units", title: "Units", width: "120px" },
            { field: "MarketValue", title: "MarketValue", width: "120px" },
            { field: "NetCostValue", title: "NetCostValue", width: "120px" },
            { field: "ProfitLossDollar", title: "ProfitLossDollar", width: "120px" },
            { field: "ProfitLossPC", title: "ProfitLossPC", width: "120px" },
            { field: "Complaint", title: "Complaint", width: "120px" },
            { field: "Reminders", title: "Reminders", width: "120px" },
            { field: "Feedback", title: "Feedback", width: "120px" },
            { field: "OngoingRev", title: "OngoingRev", width: "120px" }
        ]
    }
    $scope.detailbyAssetGridOptions = function (dataItem) {
        return {
            dataSource: {
                data: $scope.clientSummary.data.detailsByAssetClass,
                pageSize: 5,
                serverPaging: true,
                serverSorting: true,
                filter: { field: "AssetClass", operator: "eq", value: dataItem.AssetClass }
            },
            scrollable: false,
            sortable: true,
            pageable: true,
            columns: [
                { field: "EDIS_Account", title: "EDIS_Account", width: "120px" },
                { field: "Ticker", title: "Ticker", width: "120px" },
                { field: "CompanyName", title: "CompanyName", width: "120px" },
                { field: "Units", title: "Units", width: "120px" },
                { field: "MarketValue", title: "MarketValue", width: "120px" },
                { field: "NetCostValue", title: "NetCostValue", width: "120px" },
                { field: "ProfitLossDollar", title: "ProfitLossDollar", width: "120px" }
            ]

        };
    };

}]);
app.controller("adviserClientSummaryController", ["$scope", "AdviserClientAccountsDBService", function ($scope, DBContext) {
    $scope.clientGroupsList = [];

    angular.forEach(DBContext.ClientGroups, function (clientGroup, index) {

        $scope.clientGroupsList.push({
            'id': index,
            'ClientGroupNum': clientGroup.ClientGroupNum,
            'ClientGroupName': clientGroup.ClientGroupName,
            'checked': false
        });

    });

    $scope.adviserClientListGrid = {
        dataSource: {
            data: $scope.clientGroupsList,
            pageSize: 5,
            serverPaging: true,
            serverSorting: true,
        },
        scrollable: false,
        sortable: true,
        pageable: true,
        columns: [
            {
                width: "30px", template: "<input type='checkbox' class='' ng-click='onClick($event, dataItem.id)' />"
                    //"<input type='checkbox' class='' ng-click='onClick(" + "{{dataItem.ClientGroupNum}}" + ")' />"
            },
            { field: "ClientGroupNum", title: "Client Group Number", width: "120px" },
            { field: "ClientGroupName", title: "Client Group Name", width: "200px" }
        ]


    };

    $scope.onClick = function (event, index) {
        if ($scope.clientGroupsList[index].checked == false) {
            $scope.clientGroupsList[index].checked = true;
            alert($scope.clientGroupsList[index].ClientGroupName + 'has been selected');
        } else $scope.clientGroupsList[index].checked = false;
        //var element = $(e.currentTarget);

        //var checked = element.is(':checked')
        //row = element.closest("tr"),
        //grid = element.closest('kendo-grid').getKendoGrid(),
        //dataItem = grid.dataItem(row);

        //$scope.checkedIds[dataItem.EmployeeID] = checked;
        //if (checked) {
        //    row.addClass("k-state-selected");
        //} else {
        //    row.removeClass("k-state-selected");
        //}
    }
}]);
app.controller("AdviserLinkClientController", ["$scope", "AdviserClientAccountsDBService","$modal", function ($scope, DBContext, $modal) {
    $scope.clientGroupsList = [];

    angular.forEach(DBContext.ClientGroups, function (clientGroup, index) {

        $scope.clientGroupsList.push({
            'id': index,
            'ClientGroupNum': clientGroup.ClientGroupNum,
            'ClientGroupName': clientGroup.ClientGroupName,
            'checked': false
        });

    });

    $scope.adviserClientListGrid = {
        dataSource: {
            data: $scope.clientGroupsList,
            pageSize: 5,
            serverPaging: true,
            serverSorting: true,
        },
        scrollable: false,
        sortable: true,
        pageable: true,
        columns: [
            {
                width: "30px", template: "<input type='checkbox' class='' ng-click='onClick($event, dataItem.id)' />"
                //"<input type='checkbox' class='' ng-click='onClick(" + "{{dataItem.ClientGroupNum}}" + ")' />"
            },
            { field: "ClientGroupNum", title: "Client Group Number", width: "120px" },
            { field: "ClientGroupName", title: "Client Group Name", width: "200px" }
        ]


    };

    $scope.onClick = function (event, index) {
        if ($scope.clientGroupsList[index].checked == false) {
            $scope.clientGroupsList[index].checked = true;
            alert($scope.clientGroupsList[index].ClientGroupName + 'has been selected');
        } else $scope.clientGroupsList[index].checked = false;
        //var element = $(e.currentTarget);

        //var checked = element.is(':checked')
        //row = element.closest("tr"),
        //grid = element.closest('kendo-grid').getKendoGrid(),
        //dataItem = grid.dataItem(row);

        //$scope.checkedIds[dataItem.EmployeeID] = checked;
        //if (checked) {
        //    row.addClass("k-state-selected");
        //} else {
        //    row.removeClass("k-state-selected");
        //}
    }
   
    $scope.linkClient = function () {
        var modalInstance = $modal.open({
            templateUrl: "newLinkClientMessage",
            controller: "AdviserLinkClientMessageController",
            backdrop: true
        });

        modalInstance.result.then(function (result) {
            console.log(result.reason);
        });
    };
}]);
app.controller("AdviserLinkClientMessageController", ["$scope", "AdviserClientAccountsDBService", "$modalInstance", function ($scope, DBContext, $modalInstance) {
    $scope.close = function () {
        $modalInstance.dismiss("cancel");
    }
    $scope.save = function () {
        $modalInstance.close({reason: true});
    }
}]);

app.controller("AdviserReferralListController", ["$scope", "AdviserClientAccountsDBService", "$modal", function ($scope, DBContext, $modal) {
    $scope.clientGroupsList = [];

    angular.forEach(DBContext.ClientGroups, function (clientGroup, index) {

        $scope.clientGroupsList.push({
            'id': index,
            'ClientGroupNum': clientGroup.ClientGroupNum,
            'ClientGroupName': clientGroup.ClientGroupName,
            'checked': false
        });

    });

    $scope.adviserReferralList = {
        dataSource: {
            data: $scope.clientGroupsList,
            pageSize: 5,
            serverPaging: true,
            serverSorting: true,
        },
        scrollable: false,
        sortable: true,
        pageable: true,
        columns: [
            {
                width: "30px", template: "<input type='checkbox' class='' ng-click='onClick($event, dataItem.id)' />"
                //"<input type='checkbox' class='' ng-click='onClick(" + "{{dataItem.ClientGroupNum}}" + ")' />"
            },
            { field: "ClientGroupNum", title: "Client Group Number", width: "120px" },
            { field: "ClientGroupName", title: "Client Group Name", width: "200px" }
        ]


    };

    $scope.onClick = function (event, index) {
        if ($scope.clientGroupsList[index].checked == false) {
            $scope.clientGroupsList[index].checked = true;
            alert($scope.clientGroupsList[index].ClientGroupName + 'has been selected');
        } else $scope.clientGroupsList[index].checked = false;
        //var element = $(e.currentTarget);

        //var checked = element.is(':checked')
        //row = element.closest("tr"),
        //grid = element.closest('kendo-grid').getKendoGrid(),
        //dataItem = grid.dataItem(row);

        //$scope.checkedIds[dataItem.EmployeeID] = checked;
        //if (checked) {
        //    row.addClass("k-state-selected");
        //} else {
        //    row.removeClass("k-state-selected");
        //}
    }

    $scope.openReferral = function () {
        var modalInstance = $modal.open({
            templateUrl: "newLinkClientMessage",
            controller: "AdviserReferralDetailController",
            backdrop: true
        });

        modalInstance.result.then(function (result) {
            console.log(result.reason);
        });
    };
}]);
app.controller("AdviserReferralDetailController", ["$scope", "AdviserClientAccountsDBService", "$modalInstance", function ($scope, DBContext, $modalInstance) {
    $scope.close = function () {
        $modalInstance.dismiss("cancel");
    }
    $scope.save = function () {
        $modalInstance.close({ reason: true });
    }
}]);