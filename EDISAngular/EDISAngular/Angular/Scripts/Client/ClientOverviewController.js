﻿app.controller("clientOverview", ["$scope", "$http", "AppStrings", "ClientDashboardDetailsDBService", "uiGmapGoogleMapApi", function ($scope, $http, AppStrings, DBContext, uiGmapGoogleMapApi) {
    $scope.edisIP = AppStrings.EDIS_IP;
    
    // Data for Investment Portfolio Stats
    DBContext.GetInvestmentPorfolioData().get(function (data) {
        $scope.investmentPortfolio = data;
        $scope.investmentPortfolioDataKendo = new kendo.data.DataSource({
            data: $scope.investmentPortfolio.data
        });

        $scope.clickPortfolio = function (x) {
            if (angular.isDefined(x)) {
                switch(x.name){
                    case "Australian Equity": window.location.href = '#/clientportfolioae'; break;
                    case "International Equity": window.location.href = '#/clientportfolioint'; break;
                    case "Managed Investments": window.location.href = '#/clientportfoliomi'; break;
                    case "Direct & Listed Property": window.location.href = '#/clientportfoliodp'; break;
                    case "Fixed Income Investments": window.location.href = '#/clientportfoliofi'; break;
                    case "Cash & Term Deposit": window.location.href = '#/clientportfolioctd'; break;
                }
            }
        };
    })

    DBContext.GetEquityLocationData().get(function(data){
        uiGmapGoogleMapApi.then(function (maps) {

            var map = new google.maps.Map(document.getElementById("layeredMap"), {
                center: new google.maps.LatLng(30, 0),
                zoom: 2,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            });

            var country = data.countryCodes;

            $scope.map = new maps.FusionTablesLayer({
                query: {
                    select: 'geometry',
                    from: '1N2LBk4JHwWpOY4d9fobIn27lfnZ5MDy-NoqqRpk',
                    where: "ISO_2DIGIT IN (" + country + ")",
                },
                styles: [{
                    where: "ISO_2DIGIT IN (" + country + ")",
                    polygonOptions: {
                        fillColor: 'red',
                        fillOpacity: 0.3
                    }
                }],
                map: map,
                suppressInfoWindows: true
            });
        });

        $scope.markers = data.data;
    })
    
    

    
}]);


