app.controller("clientPortfolioPremiumInsuranceProductsController", ["$scope", "clientPortfolioINSList", function ($scope, DBContext) {
    DBContext().query(function (data) {
        $scope.data = [];
        for (var i = 0; i < data.length; i++) {
            var records = data[i].data;
            $scope.data = $scope.data.concat(records);
        }



        $scope.options = {
            dataSource: {
                data: $scope.data
            },
            title: {
                text: "Premium VS Insurance Product"
            },
            legend: {
                position: "top"
            },
            seriesDefaults: {
                type: "column"
            },
            series: [{
                field: "premium",
                categoryField: "type"
            }],
            tooltip: {
                visible: true,
                format: "{0}",
                template: "#= kendo.format('{0:C}',value) #"
            },
            categoryAxis: {
                field: "type",
                labels: {
                    rotation: -45,
                }
            }
        };

    })


}])
app.controller("clientPortfolioPremiumInsuranceMonthlyCashflowController", ["$scope", "clientPortfolioINSCashflowDetails", function ($scope, DBContext) {

    DBContext().get(function (data) {
        $scope.total = data.totalExpense;
        $scope.data = data.data;
        $scope.options = {
            dataSource: {
                data: $scope.data
            },
            title: {
                text: "Premium Payment on each month"
            },
            legend: {
                position: "top"
            },
            seriesDefaults: {
                type: "column"
            },
            series: [{
                field: "expense",
                categoryField: "month"
            }],
            tooltip: {
                visible: true,
                format: "{0}",
                template: "#= kendo.format('{0:C}',value) #"
            },
            categoryAxis: {
                field: "month",
                labels: {
                    rotation: -45,
                }
            }
        };

    })



}])
app.controller("clientPortfolioPremiumStatsController", ["$scope", "clientPortfolioINSStatistics", function ($scope, DBContext) {
    DBContext().get(function (data) {
        $scope.data = data;
    })
}
]);
app.controller("clientPortfolioPremiumDetailTableController", ["$scope", "clientPortfolioINSList", function ($scope, DBContext) {
    DBContext().query(function (data) {
        $scope.data = data;
    })
}
]);

app.controller("clientPortfolioPremiumConditionListController", ["$scope", "clientPortfolioINSConditions", function ($scope, DBContext) {
    DBContext().query(function (data) {
        $scope.data = data;
    })
}])




