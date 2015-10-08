/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />
(function () {
    var app = angular.module('EDIS');
    app.controller("BusinessDetailsController", function ($scope) {
        $scope.tab = 1;
    });

    app.controller("InvestmentStatisticsViewController", ["$scope", "AdviserBusinessDetailsDBService", function ($scope, DBContext) {
        DBContext.AssetStatistics_GetInvestmentStat().get(function (data) {
            $scope.data = data;

            $scope.TopLayerOptions = {
                dataSource: {
                    data: $scope.data.data,
                    pageSize: 5,
                    serverPaging: false,
                    serverSorting: false
                },
                columns: [{
                    field: "Name",
                    title: "Name",
                    width: "40%"
                }, {
                    field: "Value",
                    title: "Amount",
                    width: "30%",
                    template: "<span ng-class='{\"text-danger\": dataItem.Value < 0, \"text-success\": dataItem.Value > 0}'> {{dataItem.Value | currency}}</span>"
                }, {
                    field: "PL",
                    title: "Profit/Loss",
                    width: "30%",
                    template: "<span ng-class='{\"text-danger\": dataItem.PL < 0, \"text-success\": dataItem.PL > 0}'> {{dataItem.PL | currency}}</span>"
                }]
            };

            $scope.SecondLayerOptions = function (dataItem) {
                return {
                    dataSource: {
                        data: dataItem.Stock,
                        serverPaging: false,
                        serverSorting: false,
                        serverFiltering: false,
                        pageSize: 5
                    },
                    scrollable: false,
                    sortable: true,
                    pageable: true,
                    columns: [
                    { field: "Label", title: "Name", width: "20%" },
                    { field: "Name", title: "Company Name", width: "20%" },
                    { field: "Units", title: "Units", width: "20%" },
                    {
                        field: "Value", title: "Value", width: "20%",
                        template: "<span ng-class='{\"text-danger\": dataItem.Value < 0, \"text-success\": dataItem.Value > 0}'> {{dataItem.Value | currency}}</span>"
                    },
                    {
                        field: "PL", title: "Profit/Loss", width: "20%",
                        template: "<span ng-class='{\"text-danger\": dataItem.PL < 0, \"text-success\": dataItem.PL > 0}'> {{dataItem.PL | currency}}</span>"
                    }
                    ]
                };
            };
            $scope.ThirdLayerOptions = function (dataItem) {
                return {
                    dataSource: {
                        data: dataItem.Accounts,
                        serverPaging: false,
                        serverSorting: false,
                        serverFiltering: false,
                        pageSize: 5
                    },
                    scrollable: false,
                    sortable: true,
                    pageable: true,
                    columns: [
                    { field: "AccountNumber", title: "Account Number", width: "20%" },
                    { field: "AccountName", title: "Account Name", width: "20%" },
                    { field: "Units", title: "Units", width: "20%" },
                    {
                        field: "Value", title: "Value", width: "20%",
                        template: "<span ng-class='{\"text-danger\": dataItem.Value < 0, \"text-success\": dataItem.Value > 0}'> {{dataItem.Value | currency}}</span>"
                    },
                    {
                        field: "PL", title: "Profit/Loss", width: "20%",
                        template: "<span ng-class='{\"text-danger\": dataItem.Value < 0, \"text-success\": dataItem.Value > 0}'> {{dataItem.Value | currency}}</span>"
                    }
                    ]
                };
            };
        })
    }]);
    app.controller("LendingDebtStatisticsViewController", ["$scope", "AdviserBusinessDetailsDBService", function ($scope, DBContext) {
        DBContext.AssetStatistics_GetLendingStat().get(function (data) {
            $scope.data = data;
            $scope.TopLayerOptions = {
                dataSource: {
                    data: $scope.data.data,
                    pageSize: 5,
                    serverPaging: false,
                    serverSorting: false
                },
                columns: [{
                    field: "Name",
                    title: "Name",
                    width: "50%"
                }, {
                    field: "PL",
                    title: "Profit/Loss",
                    width: "50%",
                    template: "<span ng-class='{\"text-danger\": dataItem.PL < 0, \"text-success\": dataItem.PL > 0}'> {{dataItem.PL | currency}}</span>"
                }]
            };


            $scope.SecondLayerOptions = function (dataItem) {
                return {
                    dataSource: {
                        data: dataItem.Stock,
                        serverPaging: false,
                        serverSorting: false,
                        serverFiltering: false,
                        pageSize: 5
                    },
                    scrollable: false,
                    sortable: true,
                    pageable: true,
                    columns: [
                    { field: "Label", title: "Name", width: "25%" },
                    { field: "Name", title: "Company Name", width: "25%" },
                    {
                        field: "Value", title: "Value", width: "25%",
                        template: "<span ng-class='{\"text-danger\": dataItem.Value < 0, \"text-success\": dataItem.Value > 0}'> {{dataItem.Value | currency}}</span>"
                    },
                    {
                        field: "PL", title: "Profit/Loss", width: "25%",
                        template: "<span ng-class='{\"text-danger\": dataItem.PL < 0, \"text-success\": dataItem.PL > 0}'> {{dataItem.PL | currency}}</span>"
                    }
                    ]
                };
            };
            $scope.ThirdLayerOptions = function (dataItem) {
                return {
                    dataSource: {
                        data: dataItem.Accounts,
                        serverPaging: false,
                        serverSorting: false,
                        serverFiltering: false,
                        pageSize: 5
                    },
                    scrollable: false,
                    sortable: true,
                    pageable: true,
                    columns: [
                    { field: "AccountNumber", title: "Account Number", width: "25%" },
                    { field: "AccountName", title: "Account Name", width: "25%" },
                    {
                        field: "Value", title: "Value", width: "25%",
                        template: "<span ng-class='{\"text-danger\": dataItem.Value < 0, \"text-success\": dataItem.Value > 0}'> {{dataItem.Value | currency}}</span>"
                    },
                    {
                        field: "PL", title: "Profit/Loss", width: "25%",
                        template: "<span ng-class='{\"text-danger\": dataItem.Value < 0, \"text-success\": dataItem.Value > 0}'> {{dataItem.Value | currency}}</span>"
                    }
                    ]
                };
            };


        })






    }]);
    app.controller("InsuranceStatisticsViewController", ["$scope", "AdviserBusinessDetailsDBService", function ($scope, DBContext) {
        DBContext.AssetStatistics_GetInsuranceStat().get(function (data) {
            $scope.data = data;
            $scope.TopLayerOptions = {
                dataSource: {
                    data: $scope.data.data,
                    pageSize: 5,
                    serverPaging: false,
                    serverSorting: false
                },
                columns: [{
                    field: "Type"
                }]
            };
            $scope.SecondLayerOptions = function (dataItem) {
                return {
                    dataSource: {
                        data: dataItem.Content,
                        pageSize: 5,
                        serverPaging: false,
                        serverSorting: false
                    },
                    columns: [{
                        field: "Name",
                        title: "Name",
                        width: "50%"
                    }, {
                        field: "PL",
                        title: "Profit/Loss",
                        width: "50%",
                        template: "<span ng-class='{\"text-danger\": dataItem.PL < 0, \"text-success\": dataItem.PL > 0}'> {{dataItem.PL | currency}}</span>"
                    }]
                }
            };

            $scope.ThirdLayerOptions = function (dataItem) {
                return {
                    dataSource: {
                        data: dataItem.Stock,
                        serverPaging: false,
                        serverSorting: false,
                        serverFiltering: false,
                        pageSize: 5
                    },
                    scrollable: false,
                    sortable: true,
                    pageable: true,
                    columns: [
                    { field: "Label", title: "Name", width: "25%" },
                    { field: "Name", title: "Company Name", width: "25%" },
                    {
                        field: "Value", title: "Value", width: "25%",
                        template: "<span ng-class='{\"text-danger\": dataItem.Value < 0, \"text-success\": dataItem.Value > 0}'> {{dataItem.Value | currency}}</span>"
                    },
                    {
                        field: "PL", title: "Profit/Loss", width: "25%",
                        template: "<span ng-class='{\"text-danger\": dataItem.PL < 0, \"text-success\": dataItem.PL > 0}'> {{dataItem.PL | currency}}</span>"
                    }
                    ]
                };
            };
            $scope.FourthLayerOptions = function (dataItem) {
                return {
                    dataSource: {
                        data: dataItem.Accounts,
                        serverPaging: false,
                        serverSorting: false,
                        serverFiltering: false,
                        pageSize: 5
                    },
                    scrollable: false,
                    sortable: true,
                    pageable: true,
                    columns: [
                    { field: "AccountNumber", title: "Account Number", width: "25%" },
                    { field: "AccountName", title: "Account Name", width: "25%" },
                    {
                        field: "Value", title: "Value", width: "25%",
                        template: "<span ng-class='{\"text-danger\": dataItem.Value < 0, \"text-success\": dataItem.Value > 0}'> {{dataItem.Value | currency}}</span>"
                    },
                    {
                        field: "PL", title: "Profit/Loss", width: "25%",
                        template: "<span ng-class='{\"text-danger\": dataItem.Value < 0, \"text-success\": dataItem.Value > 0}'> {{dataItem.Value | currency}}</span>"
                    }
                    ]
                };
            };


        })
    }]);
    app.controller("ClientPositionMonitorController", ["$scope", "AdviserBusinessDetailsDBService", function ($scope, DBContext) {
        DBContext.ClientPositionMonitor_GetDetails().query(function (data) {
            $scope.data = data;

            $scope.TopLayerOptions = {
                dataSource: {
                    data: $scope.data,
                    pageSize: 5,
                    serverPaging: true,
                    serverSorting: true
                },
                pageable: true,
                columns: [{
                    field: "AccountNumber",
                    title: "Account Number"
                }, {
                    field: "AccountName",
                    title: "Account Name",
                }, {
                    field: "AssetClass",
                    title: "Asset Class",
                }, {
                    field: "NetCost",
                    title: "NetCost Value($)",
                }, {
                    field: "MarketValue",
                    title: "Market Value($)",
                }, {
                    field: "PL",
                    title: "Profit/Loss ($)",
                }, {
                    field: "PLRate",
                    title: "Profit/Loss (%)",
                }, {
                    field: "CompliantStatus",
                    title: "Compliant Status",
                }, {
                    field: "Reminders",
                    title: "Reminders",
                },

                ]
            };
            $scope.SecondLayerOptions = function (dataItem) {
                return {
                    dataSource: {
                        data: dataItem.Type,
                        pageSize: 5,
                        serverPaging: false,
                        serverSorting: false
                    },
                    columns: [{
                        field: "Name",
                        title: "Name",
                    }]
                }
            };

            $scope.ThirdLayerOptions = function (dataItem) {
                return {
                    dataSource: {
                        data: dataItem.Data,
                        serverPaging: false,
                        serverSorting: false,
                        serverFiltering: false,
                        pageSize: 5
                    },
                    scrollable: false,
                    sortable: true,
                    pageable: true,
                    columns: [
                    { field: "Name", title: "Name", width: "25%" }, {
                        field: "NetCost",
                        title: "NetCost Value($)",
                    }, {
                        field: "MarketValue",
                        title: "Market Value($)",
                    }, {
                        field: "PL",
                        title: "Profit/Loss ($)",
                    }, {
                        field: "PLRate",
                        title: "Profit/Loss (%)",
                    }, {
                        field: "CompliantStatus",
                        title: "Compliant Status",
                    }, {
                        field: "Reminders",
                        title: "Reminders",
                    },
                    ]
                };
            };
            $scope.FourthLayerOptions = function (dataItem) {
                return {
                    dataSource: {
                        data: dataItem.Accounts,
                        serverPaging: false,
                        serverSorting: false,
                        serverFiltering: false,
                        pageSize: 5
                    },
                    scrollable: false,
                    sortable: true,
                    pageable: true,
                    columns: [
                        { field: "Name", title: "Name" },
                    {
                        field: "NetCost",
                        title: "NetCost Value($)",
                    }, {
                        field: "MarketValue",
                        title: "Market Value($)",
                    }, {
                        field: "PL",
                        title: "Profit/Loss ($)",
                    }, {
                        field: "PLRate",
                        title: "Profit/Loss (%)",
                    }, {
                        field: "CompliantStatus",
                        title: "Compliant Status",
                    }, {
                        field: "Reminders",
                        title: "Reminders",
                    }
                    ]
                };
            };

        })

    }]);
    app.controller("ClientDemographicsController", ["$scope", "AdviserBusinessDetailsDBService", function ($scope, DBContext) {
        DBContext.ClientDemographics_GetGeneralInfo().query(function (data) {
            $scope.locations = data;

        })
        DBContext.ClientDemographics_GetDetailsForLocations().get(function (data) {
            $scope.data = data;

        });
        $scope.setAllState = function (state, selected) {
            for (var i = 0; i < state.Data.length; i++) {
                $scope.setAllCity(state.Data[i], selected);
                state.Data[i].checked = selected;
            }
            $scope.refreshLocationSelect();
        };
        $scope.setAllCity = function (city, selected) {

            for (var i = 0; i < city.Data.length; i++) {
                city.Data[i].checked = selected;
            }
            $scope.refreshLocationSelect();

        };
        $scope.refreshLocationSelect = function () {
            for (var i = 0; i < $scope.locations.length; i++) {
                var numberOfSelectedCities = 0;
                for (var j = 0; j < $scope.locations[i].Data.length && angular.isArray($scope.locations[i].Data) ; j++) {
                    var numberOfSelectedSuburbs = 0;
                    for (var k = 0; k < $scope.locations[i].Data[j].Data.length && angular.isArray($scope.locations[i].Data[j].Data) ; k++) {
                        if ($scope.locations[i].Data[j].Data[k].checked) {
                            numberOfSelectedSuburbs++;
                        }
                    }
                    if (numberOfSelectedSuburbs > 0) {
                        numberOfSelectedCities++;
                        //$scope.locations[i].Data[j].checked = true;
                    } else {
                        $scope.locations[i].Data[j].checked = false;
                    }
                }
                if (numberOfSelectedCities > 0) {
                    //$scope.locations[i].checked=true;
                } else {
                    $scope.locations[i].checked = false;
                }
            }
            Filter();
        };

        function Filter() {
            var keys = [];
            for (var i = 0; i < $scope.locations.length; i++) {
                for (var j = 0; j < $scope.locations[i].Data.length && angular.isArray($scope.locations[i].Data) ; j++) {
                    for (var k = 0; k < $scope.locations[i].Data[j].Data.length && angular.isArray($scope.locations[i].Data[j].Data) ; k++) {
                        if ($scope.locations[i].Data[j].Data[k].checked) {
                            keys.push($scope.locations[i].Data[j].Data[k].id);
                        }
                    }
                }
            }

            DBContext.ClientDemographics_GetDetailsForLocations().get({ locations: keys }, function (data) {
                $scope.data = data;

            });

        };


    }]);
    app.controller("BusinessRevenueDetailsViewController", ["$scope", "AdviserBusinessDetailsDBService", function ($scope, DBContext) {
        DBContext.GetBusinessRevenueData_Details().get(function (data) {
            $scope.businessRev = data;
            $scope.businessRevDataKendo = new kendo.data.DataSource({
                data: $scope.businessRev.data
            });
            $scope.TopLayerOptions = {
                dataSource: {
                    data: $scope.businessRev.data,
                    pageSize: 10,
                    serverPaging: false,
                    serverSorting: false
                },
                pageable: false,
                columns: [{
                    field: "name",
                    title: "Name",
                    width: "50%"
                }, {
                    field: "amount",
                    title: "amount",
                    width: "50%",
                    template: "<span ng-class='{\"text-danger\": dataItem.amount < 0, \"text-success\": dataItem.amount > 0}'> {{dataItem.amount| currency}}</span>"
                }]
            };

            $scope.SecondLayerOptions = function (dataItem) {
                return {
                    dataSource: {
                        data: dataItem.Accounts,
                        serverPaging: false,
                        serverSorting: false,
                        serverFiltering: false,
                        pageSize: 5
                    },
                    scrollable: false,
                    sortable: true,
                    pageable: true,
                    columns: [
                    { field: "AccountNumber", title: "Account Number", width: "25%" },
                    { field: "AccountName", title: "Account Name", width: "25%" },
                    { field: "AUM", title: "AUM", width: "25%", template: "{{dataItem.AUM | currency}}" },
                    {
                        field: "Revenue", title: "Revenue", width: "25%",
                        template: " {{dataItem.Revenue | currency}}"
                    }
                    ]
                };
            };


        })

    }]);
    app.controller("CompliantFilesViewController", ["$scope", "AdviserBusinessDetailsDBService", function ($scope, DBContext) {
        DBContext.CompliantFiles_GetGeneralInfo().get(function (data) {
            $scope.data = data;
            $scope.TopLayerOptions_CompliantFiles = {
                dataSource: {
                    data: $scope.data.CompliantFiles.data,
                    pageSize: 5,
                    serverPaging: false,
                    serverSorting: false
                },
                columns: [{
                    field: "Group",
                    title: "Group"
                }, {
                    field: "Number",
                    title: "No. of Clients"
                }]
            };
            $scope.SecondLayerOptions_CompliantFiles = function (dataItem) {
                return {
                    dataSource: {
                        data: dataItem.Clients,
                        pageSize: 5,
                        serverPaging: false,
                        serverSorting: false
                    },
                    columns: [{
                        field: "AccountGroupNumber",
                        title: "Account Group Number",
                        width: "50%"
                    }, {
                        field: "AccountName",
                        title: "Account Name",
                        width: "50%"
                    }]
                }
            };

            $scope.TopLayerOptions_ComplianceOverview = {
                dataSource: {
                    data: $scope.data.ComplianceOverview.data,
                    pageSize: 5,
                    serverPaging: false,
                    serverSorting: false
                },
                columns: [{
                    field: "Group",
                    title: "Group"
                }, {
                    field: "Number",
                    title: "No. of Products"
                }]
            };
            $scope.SecondLayerOptions_ComplianceOverview = function (dataItem) {
                return {
                    dataSource: {
                        data: dataItem.Stocks,
                        pageSize: 5,
                        serverPaging: false,
                        serverSorting: false
                    },
                    columns: [{
                        field: "Label",
                        title: "Label",
                        width: "50%"
                    }, {
                        field: "Name",
                        title: "Name",
                        width: "50%"
                    }]
                }
            };




        })





    }]);



})();