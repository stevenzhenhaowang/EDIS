/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />


(function () {
    var app = angular.module("EDIS");
    app.factory("adviserPortfolioGetRegionSummary", function ($http, $resource, AppStrings) {
        var DBContext = {
            getRegionalExposure: function () {
                return $resource(AppStrings.EDIS_IP + "api/Adviser/PortfolioOverview/RegionalExposure");
            }
        };
        return DBContext;
    });
    app.factory("adviserPortfolioGetSectorInfo", function ($http, $resource, AppStrings) {
        var DBContext = {
            getData: function () {
                return $resource(AppStrings.EDIS_IP + "api/Adviser/PortfolioOverview/SectorialExposure");
            }
        };
        return DBContext;
    });
    app.factory("adviserPortfolioSummaryGetCashflowInfo", function ($http, $resource, AppStrings) {
        var DBContext = function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/PortfolioOverview/CashflowDetail");
        };
        return DBContext;
    });

    //START OF AE SERVICES
    //app.factory("adviserPortfolioAEGeneralInfo",function ($http, $resource, AppStrings) {
    //    return function () {
    //        return $resource(AppStrings.EDIS_IP + "api/Adviser/AustralianEquityPortfolio/General");
    //    }
    //});
    //app.factory("adviserPortfolioAEGeneralInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (cselector, $http, $resource, AppStrings) {


    //    var getData = function () {
    //        var clientGroupId = cselector.getCurrentClientUserId();
    //        return $http({
    //            url: AppStrings.EDIS_IP + "api/Adviser/AustralianEquityPortfolio/General",
    //            method: "GET",
    //            params: { clientGroupId: clientGroupId }
    //        });
    //    }
    //    return {
    //        getData:getData
    //    }
    //}]);
    app.factory("adviserPortfolioAEGeneralInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/AustralianEquityPortfolio/General" + clientSelector.getClientIdQueryString());
        }
    }]);

    app.factory("adviserPortfolioAECashflowDetails", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/AustralianEquityPortfolio/Cashflow" + clientSelector.getClientIdQueryString());
        };
    }]);
    app.factory("adviserPortfolioAECashflowInfoCompanySpecific", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        var DBContext = function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/AustralianEquityPortfolio/CashflowDetail" + clientSelector.getClientIdQueryString());
        };
        return DBContext;
    }]);
    app.factory("adviserPortfolioAECompanyProfiles", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/AustralianEquityPortfolio/CompanyProfiles" + clientSelector.getClientIdQueryString());
        }
    }])
    app.factory("adviserPortfolioAEEvaluationAgainstModel", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/AustralianEquityPortfolio/EvaluationModel" + clientSelector.getClientIdQueryString());
        };
    }])
    app.factory("adviserPortfolioAEDiversificationInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/AustralianEquityPortfolio/Diversification" + clientSelector.getClientIdQueryString());
        };
    }]);
    app.factory("adviserPortfolioAERatingInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/AustralianEquityPortfolio/Rating" + clientSelector.getClientIdQueryString());
        }
    }])
    app.factory("adviserPortfolioAEQuickStats", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/AustralianEquityPortfolio/QuickStats" + clientSelector.getClientIdQueryString());
        }
    }])
    //END OF AE SERVICES

    //START OF INT SERVICES
    app.factory("adviserPortfolioINTGeneralInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/InternationalEquityPortfolio/General" + clientSelector.getClientIdQueryString());
        }
    }]);
    app.factory("adviserPortfolioINTCashflowDetails",["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/InternationalEquityPortfolio/Cashflow" + clientSelector.getClientIdQueryString());
        };
    }]);
    app.factory("adviserPortfolioINTCashflowInfoCompanySpecific", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        var DBContext = function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/InternationalEquityPortfolio/CashflowDetail" + clientSelector.getClientIdQueryString());
        }
        return DBContext;
    }]);
    app.factory("adviserPortfolioINTCompanyProfiles", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/InternationalEquityPortfolio/CompanyProfiles" + clientSelector.getClientIdQueryString());
        }
    }])
    app.factory("adviserPortfolioINTCashflowMonthly", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/InternationalEquityPortfolio/Cashflow" + clientSelector.getClientIdQueryString());
        }
    }])
    app.factory("adviserPortfolioINTEvaluationAgainstModel", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/InternationalEquityPortfolio/EvaluationModel" + clientSelector.getClientIdQueryString());
        };
    }])
    app.factory("adviserPortfolioINTDiversificationInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/InternationalEquityPortfolio/Diversification" + clientSelector.getClientIdQueryString());
        }
    }]);
    app.factory("adviserPortfolioINTRatingInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/InternationalEquityPortfolio/Rating" + clientSelector.getClientIdQueryString());
        }
    }])
    app.factory("adviserPortfolioINTQuickStats", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/AustralianEquityPortfolio/QuickStats" + clientSelector.getClientIdQueryString());
        }
    }])
    //END OF INT SERVICES



    //START OF MI SERVICES 
    app.factory("adviserPortfolioMIGeneralInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/ManagedInvestmentPortfolio/General" + clientSelector.getClientIdQueryString());
        }
    }]);
    app.factory("adviserPortfolioMICashflowDetails", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/ManagedInvestmentPortfolio/Cashflow" + clientSelector.getClientIdQueryString());
        };
    }]);
    app.factory("advisorPortfolioMIAssetAllocation", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/ManagedInvestmentPortfolio/AssetAllocation" + clientSelector.getClientIdQueryString());
        };
    }]);
    app.factory("adviserPortfolioMICashflowInfoCompanySpecific", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        var DBContext = function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/ManagedInvestmentPortfolio/CashflowDetail" + clientSelector.getClientIdQueryString());
        }
        return DBContext;
    }]);
    app.factory("adviserPortfolioMICompanyProfiles", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/ManagedInvestmentPortfolio/CompanyProfiles" + clientSelector.getClientIdQueryString());
        }
    }])
    app.factory("adviserPortfolioMICashflowMonthly", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/ManagedInvestmentPortfolio/Cashflow" + clientSelector.getClientIdQueryString());
        }
    }])
    app.factory("adviserPortfolioMIDiversificationInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/ManagedInvestmentPortfolio/Diversification" + clientSelector.getClientIdQueryString());
        }
    }]);
    app.factory("adviserPortfolioMIRatingInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/ManagedInvestmentPortfolio/Rating" + clientSelector.getClientIdQueryString());
        }
    }]);
    app.factory("adviserPortfolioMIQuickStats", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/ManagedInvestmentPortfolio/QuickStats" + clientSelector.getClientIdQueryString());
        }
    }])
    app.factory("adviserPortfolioMIDiversificationGroupSummary", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/ManagedInvestmentPortfolio/DiversificationGroup" + clientSelector.getClientIdQueryString());
        }
    }]);
    //END OF MI SERVICES 


    //START OF DP SERVICES
    app.factory("adviserPortfolioDPGeneralInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/DirectPropertyPortfolio/General" + clientSelector.getClientIdQueryString());
        }
    }]);
    app.factory("adviserPortfolioDPCashflowDetails", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/InternationalEquityPortfolio/Cashflow" + clientSelector.getClientIdQueryString());
        }
    }]);
    app.factory("adviserPortfolioDPCashflowInfoPropertySpecific", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        var DBContext = function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/DirectPropertyPortfolio/CashflowDetail" + clientSelector.getClientIdQueryString());
        };
        return DBContext;
    }]);
    app.factory("adviserPortfolioDPCashflowMonthly", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/DirectPropertyPortfolio/CashflowDetail" + clientSelector.getClientIdQueryString());
        }
    }])
    app.factory("adviserPortfolioDPRatingInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/DirectPropertyPortfolio/Rating" + clientSelector.getClientIdQueryString());
        }
    }]);


    app.factory("adviserPortfolioDPQuickStats", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/DirectPropertyPortfolio/QuickStats" + clientSelector.getClientIdQueryString());
        }
    }])

    //testing out
    //app.factory("adviserPortfolioDPRatingInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
    //    return function () {
    //        return function () {
    //            return $resource(AppStrings.EDIS_IP + "api/Adviser/DirectPropertyPortfolio/Rating" + clientSelector.getClientIdQueryString());
    //        }
    //    }
    //}])

    //not implemented Services
    app.factory("adviserPortfolioDPCompanyProfiles", function ($http, $resource) {
        return function () {
            return {
                data: [
                    {
                        asx: "0221",
                        company: "company 1",
                        costValue: Math.random() * 10000,
                        brokerage: Math.random() * 10000,
                        totalCostValue: Math.random() * 10000,
                        totalCostValueWeighting: Math.random() * 100,
                        marketPrice: Math.random() * 10000,
                        marketValue: Math.random() * 10000,
                        marketValueWeighting: Math.random() * 100,
                        marketValueAssetAllocationWeighting: Math.random() * 100,
                        companySuitabilityToInvestor: Math.random() * 100,
                        earningsPerShare: Math.random() * 10000,
                        earningsPerShareWeighting: Math.random() * 100,
                        earningsPerShareGrowth: Math.random() * 100,
                        earningsPerShareGrowthWeighting: Math.random() * 100,
                        dividend: Math.random() * 10000,
                        dividendWeighting: Math.random() * 100,
                        franking: Math.random() * 10000,
                        frankingWeighting: Math.random() * 100,
                        dividendYield: Math.random() * 10000,
                        dividendYieldWeighting: Math.random() * 100,
                        priceEarningsRatio: Math.random() * 10000,
                        priceEarningsRatioWeighting: Math.random() * 100,
                        returnOnAsset: Math.random() * 10000,
                        returnOnAssetWeighting: Math.random() * 100,
                        returnOnEquity: Math.random() * 10000,
                        returnOnEquityWeighting: Math.random() * 10000,
                        oneYearReturn: Math.random() * 100,
                        oneYearReturnWeighting: Math.random() * 100,
                        threeYearReturn: Math.random() * 100,
                        threeYearReturnWeighting: Math.random() * 100,
                        fiveYearReturn: Math.random() * 100,
                        fiveYearReturnWeighting: Math.random() * 100,
                        beta: Math.random() * 100,
                        betaWeighting: Math.random() * 100,
                        currentRatio: Math.random() * 100,
                        currentRatioWeighting: Math.random() * 100,
                        quickRatio: Math.random() * 100,
                        quickRatioWeighting: Math.random() * 100,
                        debtEquityRatio: Math.random() * 100,
                        debtEquityRatioWeighting: Math.random() * 100,
                        interestCover: Math.random() * 100,
                        interestCoverWeighting: Math.random() * 100,
                        payoutRatio: Math.random() * 100,
                        payoutRatioWeighting: Math.random() * 100,
                        earningsStability: Math.random() * 100,
                        earningsStabilityWeighting: Math.random() * 100,
                    }, {
                        asx: "0222",
                        company: "company 2",
                        costValue: Math.random() * 10000,
                        brokerage: Math.random() * 10000,
                        totalCostValue: Math.random() * 10000,
                        totalCostValueWeighting: Math.random() * 100,
                        marketPrice: Math.random() * 10000,
                        marketValue: Math.random() * 10000,
                        marketValueWeighting: Math.random() * 100,
                        marketValueAssetAllocationWeighting: Math.random() * 100,
                        companySuitabilityToInvestor: Math.random() * 100,
                        earningsPerShare: Math.random() * 10000,
                        earningsPerShareWeighting: Math.random() * 100,
                        earningsPerShareGrowth: Math.random() * 100,
                        earningsPerShareGrowthWeighting: Math.random() * 100,
                        dividend: Math.random() * 10000,
                        dividendWeighting: Math.random() * 100,
                        franking: Math.random() * 10000,
                        frankingWeighting: Math.random() * 100,
                        dividendYield: Math.random() * 10000,
                        dividendYieldWeighting: Math.random() * 100,
                        priceEarningsRatio: Math.random() * 10000,
                        priceEarningsRatioWeighting: Math.random() * 100,
                        returnOnAsset: Math.random() * 10000,
                        returnOnAssetWeighting: Math.random() * 100,
                        returnOnEquity: Math.random() * 10000,
                        returnOnEquityWeighting: Math.random() * 10000,
                        oneYearReturn: Math.random() * 100,
                        oneYearReturnWeighting: Math.random() * 100,
                        threeYearReturn: Math.random() * 100,
                        threeYearReturnWeighting: Math.random() * 100,
                        fiveYearReturn: Math.random() * 100,
                        fiveYearReturnWeighting: Math.random() * 100,
                        beta: Math.random() * 100,
                        betaWeighting: Math.random() * 100,
                        currentRatio: Math.random() * 100,
                        currentRatioWeighting: Math.random() * 100,
                        quickRatio: Math.random() * 100,
                        quickRatioWeighting: Math.random() * 100,
                        debtEquityRatio: Math.random() * 100,
                        debtEquityRatioWeighting: Math.random() * 100,
                        interestCover: Math.random() * 100,
                        interestCoverWeighting: Math.random() * 100,
                        payoutRatio: Math.random() * 100,
                        payoutRatioWeighting: Math.random() * 100,
                        earningsStability: Math.random() * 100,
                        earningsStabilityWeighting: Math.random() * 100,
                    }, {
                        asx: "0223",
                        company: "company 3",
                        costValue: Math.random() * 10000,
                        brokerage: Math.random() * 10000,
                        totalCostValue: Math.random() * 10000,
                        totalCostValueWeighting: Math.random() * 100,
                        marketPrice: Math.random() * 10000,
                        marketValue: Math.random() * 10000,
                        marketValueWeighting: Math.random() * 100,
                        marketValueAssetAllocationWeighting: Math.random() * 100,
                        companySuitabilityToInvestor: Math.random() * 100,
                        earningsPerShare: Math.random() * 10000,
                        earningsPerShareWeighting: Math.random() * 100,
                        earningsPerShareGrowth: Math.random() * 100,
                        earningsPerShareGrowthWeighting: Math.random() * 100,
                        dividend: Math.random() * 10000,
                        dividendWeighting: Math.random() * 100,
                        franking: Math.random() * 10000,
                        frankingWeighting: Math.random() * 100,
                        dividendYield: Math.random() * 10000,
                        dividendYieldWeighting: Math.random() * 100,
                        priceEarningsRatio: Math.random() * 10000,
                        priceEarningsRatioWeighting: Math.random() * 100,
                        returnOnAsset: Math.random() * 10000,
                        returnOnAssetWeighting: Math.random() * 100,
                        returnOnEquity: Math.random() * 10000,
                        returnOnEquityWeighting: Math.random() * 10000,
                        oneYearReturn: Math.random() * 100,
                        oneYearReturnWeighting: Math.random() * 100,
                        threeYearReturn: Math.random() * 100,
                        threeYearReturnWeighting: Math.random() * 100,
                        fiveYearReturn: Math.random() * 100,
                        fiveYearReturnWeighting: Math.random() * 100,
                        beta: Math.random() * 100,
                        betaWeighting: Math.random() * 100,
                        currentRatio: Math.random() * 100,
                        currentRatioWeighting: Math.random() * 100,
                        quickRatio: Math.random() * 100,
                        quickRatioWeighting: Math.random() * 100,
                        debtEquityRatio: Math.random() * 100,
                        debtEquityRatioWeighting: Math.random() * 100,
                        interestCover: Math.random() * 100,
                        interestCoverWeighting: Math.random() * 100,
                        payoutRatio: Math.random() * 100,
                        payoutRatioWeighting: Math.random() * 100,
                        earningsStability: Math.random() * 100,
                        earningsStabilityWeighting: Math.random() * 100,
                    },
                ],
                totalCostInvestment: 505550,
                totalCostWeighting: 100,
                totalMarketValue: 600000,
                totalMarketValueWeighting: 100,
                totalpl: 500000
            }
        };
    })  
    app.factory("adviserPortfolioDPEvaluationAgainstModel", function ($http, $resource) {
        return function () {
            return [
                {
                    title: "Ave 5 Year Return",
                    expected: 5000,
                    actual: Math.random() * 5000
                }, {
                    title: "Ave 3 Year Return",
                    expected: 5000,
                    actual: Math.random() * 5000
                }, {
                    title: "Ave 1 Year Return",
                    expected: 5000,
                    actual: Math.random() * 5000
                }, {
                    title: "Ave Payout Ratio",
                    expected: 5000,
                    actual: Math.random() * 5000
                }, {
                    title: "Ave Franking",
                    expected: 5000,
                    actual: Math.random() * 5000
                }, {
                    title: "Ave Beta",
                    expected: 5000,
                    actual: Math.random() * 5000
                }, {
                    title: "Ave Quick Ratio",
                    expected: 5000,
                    actual: Math.random() * 5000
                }, {
                    title: "Ave Return on Asset",
                    expected: 5000,
                    actual: Math.random() * 5000
                }, {
                    title: "Ave Dividend Yield",
                    expected: 5000,
                    actual: Math.random() * 5000
                },
            ]
        };
    })
    app.factory("adviserPortfolioDPDiversificationInfo", function ($http, $resource) {
        return function () {
            return {
                total: {
                    sectorialExposureValue: Math.random() * 100000,
                    sectorialExposurePercentage: Math.random() * 100,
                    asxSectorialExposurePercentage: Math.random() * 100,
                    asxDifferencesPercentage: Math.random() * 100 - 100,
                    profileAllocationPercentage: Math.random() * 100,
                    actualPercentage: Math.random() * 100,
                    profileDifferencesPercentage: Math.random() * 100 - 100,
                    indicator: "E/Unsuitable",
                    suitabilityIndicator: Math.random() * 100
                },
                data: [
                    {
                        sector: "Consumer Goods",
                        sectorialExposureValue: Math.random() * 100000,
                        sectorialExposurePercentage: Math.random() * 100,
                        asxSectorialExposurePercentage: Math.random() * 100,
                        differencesPercentage: Math.random() * 100 - 100,
                        profileAllocationPercentage: Math.random() * 100,
                        actualPercentage: Math.random() * 100,
                        differencesPercentage: Math.random() * 100 - 100,
                        indicator: "E/Unsuitable",
                        suitabilityIndicator: Math.random() * 100
                    }, {
                        sector: "Consumer Services",
                        sectorialExposureValue: Math.random() * 100000,
                        sectorialExposurePercentage: Math.random() * 100,
                        asxSectorialExposurePercentage: Math.random() * 100,
                        differencesPercentage: Math.random() * 100 - 100,
                        profileAllocationPercentage: Math.random() * 100,
                        actualPercentage: Math.random() * 100,
                        differencesPercentage: Math.random() * 100 - 100,
                        indicator: "E/Unsuitable",
                        suitabilityIndicator: Math.random() * 100
                    }, {
                        sector: "Energy",
                        sectorialExposureValue: Math.random() * 100000,
                        sectorialExposurePercentage: Math.random() * 100,
                        asxSectorialExposurePercentage: Math.random() * 100,
                        differencesPercentage: Math.random() * 100 - 100,
                        profileAllocationPercentage: Math.random() * 100,
                        actualPercentage: Math.random() * 100,
                        differencesPercentage: Math.random() * 100 - 100,
                        indicator: "E/Unsuitable",
                        suitabilityIndicator: Math.random() * 100
                    }, {
                        sector: "Financial Services",
                        sectorialExposureValue: Math.random() * 100000,
                        sectorialExposurePercentage: Math.random() * 100,
                        asxSectorialExposurePercentage: Math.random() * 100,
                        differencesPercentage: Math.random() * 100 - 100,
                        profileAllocationPercentage: Math.random() * 100,
                        actualPercentage: Math.random() * 100,
                        differencesPercentage: Math.random() * 100 - 100,
                        indicator: "E/Unsuitable",
                        suitabilityIndicator: Math.random() * 100
                    }, {
                        sector: "Hardware",
                        sectorialExposureValue: Math.random() * 100000,
                        sectorialExposurePercentage: Math.random() * 100,
                        asxSectorialExposurePercentage: Math.random() * 100,
                        differencesPercentage: Math.random() * 100 - 100,
                        profileAllocationPercentage: Math.random() * 100,
                        actualPercentage: Math.random() * 100,
                        differencesPercentage: Math.random() * 100 - 100,
                        indicator: "E/Unsuitable",
                        suitabilityIndicator: Math.random() * 100
                    }, {
                        sector: "Healthcare",
                        sectorialExposureValue: Math.random() * 100000,
                        sectorialExposurePercentage: Math.random() * 100,
                        asxSectorialExposurePercentage: Math.random() * 100,
                        differencesPercentage: Math.random() * 100 - 100,
                        profileAllocationPercentage: Math.random() * 100,
                        actualPercentage: Math.random() * 100,
                        differencesPercentage: Math.random() * 100 - 100,
                        indicator: "E/Unsuitable",
                        suitabilityIndicator: Math.random() * 100
                    },
                ]
            }
        };
    });
  
    app.factory("adviserPortfolioDPDiversificationGroupSummary", function ($http, $resource) {
        return function () {
            return {
                total: 100,
                data: [
                    {
                        group: "Allocation",
                        data: [
                            {
                                title: "Aggressive Allocation",
                                value: Math.random() * 100
                            }, {
                                title: "Allocation",
                                value: Math.random() * 100
                            }, {
                                title: "Conservative Allocation",
                                value: Math.random() * 100
                            }, {
                                title: "Moderate Allocation",
                                value: Math.random() * 100
                            },
                        ]
                    }, {
                        group: "Alternative",
                        data: [
                            {
                                title: "Hedge Fund",
                                value: Math.random() * 100
                            }, {
                                title: "Other Alternative",
                                value: Math.random() * 100
                            },
                        ]
                    }, {
                        group: "Equity",
                        data: [
                            {
                                title: "Australia Equity",
                                value: Math.random() * 100
                            }, {
                                title: "Asia",
                                value: Math.random() * 100
                            }, {
                                title: "Emerging Markets Equity",
                                value: Math.random() * 100
                            }, {
                                title: "Europe Equity",
                                value: Math.random() * 100
                            },
                        ]
                    },
                ]
            };
        }
    });
    app.factory("adviserPortfolioDPgeoInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/DirectPropertyPortfolio/GeoInfo" + clientSelector.getClientIdQueryString());
        }
    }]);
    //END OF DP SERVICES

    //START OF FI SERVICES
    app.factory("adviserPortfolioFIGeneralInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/FixedIncomePortfolio/General" + clientSelector.getClientIdQueryString());

        }
    }]);
    app.factory("adviserPortfolioFICashflowDetails", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/FixedIncomePortfolio/Cashflow" + clientSelector.getClientIdQueryString());
        };
    }]);
    app.factory("adviserPortfolioFICashflowInfoIncomeSpecific", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        var DBContext = function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/FixedIncomePortfolio/CashflowDetailed" + clientSelector.getClientIdQueryString());

        }


        return DBContext;
    }]);
    app.factory("adviserPortfolioFIIncomeProfiles", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/FixedIncomePortfolio/Profiles" + clientSelector.getClientIdQueryString());
        }
    }])
    app.factory("adviserPortfolioFIDiversificationInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/FixedIncomePortfolio/Diversifications" + clientSelector.getClientIdQueryString());
        };
    }]);
    app.factory("adviserPortfolioFIStatistics", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/FixedIncomePortfolio/Stats" + clientSelector.getClientIdQueryString());
        }
    }]);
    app.factory("adviserPortfolioFIPriceChartData", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/FixedIncomePortfolio/Price" + clientSelector.getClientIdQueryString());
        };
    }]);
       
    //not implemented services
    app.factory("adviserPortfolioFIEvaluationAgainstModel", function ($http, $resource) {
        return function () {
            return [
                {
                    title: "Ave 5 Year Return",
                    expected: 5000,
                    actual: Math.random() * 5000
                }, {
                    title: "Ave 3 Year Return",
                    expected: 5000,
                    actual: Math.random() * 5000
                }, {
                    title: "Ave 1 Year Return",
                    expected: 5000,
                    actual: Math.random() * 5000
                }, {
                    title: "Ave Payout Ratio",
                    expected: 5000,
                    actual: Math.random() * 5000
                }, {
                    title: "Ave Franking",
                    expected: 5000,
                    actual: Math.random() * 5000
                }, {
                    title: "Ave Beta",
                    expected: 5000,
                    actual: Math.random() * 5000
                }, {
                    title: "Ave Quick Ratio",
                    expected: 5000,
                    actual: Math.random() * 5000
                }, {
                    title: "Ave Return on Asset",
                    expected: 5000,
                    actual: Math.random() * 5000
                }, {
                    title: "Ave Dividend Yield",
                    expected: 5000,
                    actual: Math.random() * 5000
                },
            ]
        };
    })    
    app.factory("adviserPortfolioFIRatingInfo", function ($http, $resource) {
        return function () {
            return {
                risk: 80,
                suitability: 88,
                notSuited: 20,
                data: [
                    {
                        name: "Market Capitalisation",
                        score: Math.random() * 100,
                        weighting: Math.random() * 100
                    }, {
                        name: "Dividend Yield",
                        score: Math.random() * 100,
                        weighting: Math.random() * 100
                    }, {
                        name: "Price Earnings Ratio",
                        score: Math.random() * 100,
                        weighting: Math.random() * 100
                    }, {
                        name: "Return on Asset",
                        score: Math.random() * 100,
                        weighting: Math.random() * 100
                    }, {
                        name: "Return on Equity",
                        score: Math.random() * 100,
                        weighting: Math.random() * 100
                    }, {
                        name: "Interest Cover",
                        score: Math.random() * 100,
                        weighting: Math.random() * 100
                    },
                ]
            }
        }
    });
    app.factory("adviserPortfolioFIQuickStats", function ($http, $resource) {
        return function () {
            return [
                {
                    name: "Ave Earnings Per Share",
                    value: "$2.88"
                }, {
                    name: "Ave Earnings Per Share Growth",
                    value: "5%"
                }, {
                    name: "Ave Dividend",
                    value: "$6.78"
                }, {
                    name: "Ave Franking",
                    value: "$7.00"
                }, {
                    name: "Ave Beta",
                    value: "1.65"
                }, {
                    name: "Ave Current Ratio",
                    value: "6"
                }
            ]
        }
    })
    app.factory("adviserPortfolioFIDiversificationGroupSummary", function ($http, $resource) {
        return function () {
            return {
                total: 100,
                data: [
                    {
                        group: "Allocation",
                        data: [
                            {
                                title: "Aggressive Allocation",
                                value: Math.random() * 100
                            }, {
                                title: "Allocation",
                                value: Math.random() * 100
                            }, {
                                title: "Conservative Allocation",
                                value: Math.random() * 100
                            }, {
                                title: "Moderate Allocation",
                                value: Math.random() * 100
                            },
                        ]
                    }, {
                        group: "Alternative",
                        data: [
                            {
                                title: "Hedge Fund",
                                value: Math.random() * 100
                            }, {
                                title: "Other Alternative",
                                value: Math.random() * 100
                            },
                        ]
                    }, {
                        group: "Equity",
                        data: [
                            {
                                title: "Australia Equity",
                                value: Math.random() * 100
                            }, {
                                title: "Asia",
                                value: Math.random() * 100
                            }, {
                                title: "Emerging Markets Equity",
                                value: Math.random() * 100
                            }, {
                                title: "Europe Equity",
                                value: Math.random() * 100
                            },
                        ]
                    },
                ]
            };
        }
    });
    app.factory("adviserPortfolioFIgeoInfo", function ($http, $resource) {
        return function () {
            return {
                data: [
                    {
                        id: 1,
                        address: "DieSachbearbeiter Schönhauser Allee 167c",
                        latitude: -26.274985 + (Math.random() * 10 - 5),
                        longitude: 134.775464 + (Math.random() * 10 - 5),
                        country: "Australia",
                        state: "VIC",
                        type: "Office",
                        value: Math.random() * 100000
                    }, {
                        id: 2,
                        address: "quis enim. Donec pede justo, fringilla ve 447c",
                        latitude: -26.274985 + (Math.random() * 10 - 5),
                        longitude: 134.775464 + (Math.random() * 10 - 5),
                        country: "Australia",
                        state: "NSW",
                        type: "Retail",
                        value: Math.random() * 100000
                    }, {
                        id: 3,
                        address: "Quisque rutrum. Aenean imperdiet.  667c",
                        latitude: -26.274985 + (Math.random() * 10 - 5),
                        longitude: 134.775464 + (Math.random() * 10 - 5),
                        country: "Australia",
                        state: "QLD",
                        type: "Industrial",
                        value: Math.random() * 100000
                    }, {
                        id: 4,
                        address: "porttitor eu, consequat vitae, 57c",
                        latitude: -26.274985 + (Math.random() * 10 - 5),
                        longitude: 134.775464 + (Math.random() * 10 - 5),
                        country: "Australia",
                        state: "VIC",
                        type: "Office",
                        value: Math.random() * 100000
                    }, {
                        id: 5,
                        address: "quam semper libero, sit amet  66",
                        latitude: -26.274985 + (Math.random() * 10 - 5),
                        longitude: 134.775464 + (Math.random() * 10 - 5),
                        country: "Australia",
                        state: "VIC",
                        type: "Residential",
                        value: Math.random() * 100000
                    }, {
                        id: 6,
                        address: "nec, vulputate eget, arcu. In 25",
                        latitude: -26.274985 + (Math.random() * 10 - 5),
                        longitude: 134.775464 + (Math.random() * 10 - 5),
                        country: "Australia",
                        state: "NSW",
                        type: "Office",
                        value: Math.random() * 100000
                    },
                ]
            }
        }
    });


   
    //END OF FI SERVICES 

    //START OF CTD SERVICES
    app.factory("adviserPortfolioCTDGeneralInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/CashTermDepositPortfolio/General" + clientSelector.getClientIdQueryString());
        }
    }]);
    app.factory("adviserPortfolioCTDCashflowDetails", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/CashTermDepositPortfolio/Cashflow" + clientSelector.getClientIdQueryString());
        }
    }]);
    app.factory("adviserPortfolioCTDCashflowInfoDepositSpecific", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        var DBContext = function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/CashTermDepositPortfolio/CashflowDetailed" + clientSelector.getClientIdQueryString());
        }
        return DBContext;
    }]);
    app.factory("adviserPortfolioCTDDepositProfiles", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/CashTermDepositPortfolio/Profiles" + clientSelector.getClientIdQueryString());
        }
    }])
    app.factory("adviserPortfolioCTDDiversificationInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/CashTermDepositPortfolio/Diversifications" + clientSelector.getClientIdQueryString());
        }
    }]);
    app.factory("adviserPortfolioCTDPriceChartData", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/CashTermDepositPortfolio/Price" + clientSelector.getClientIdQueryString());
        }
    }]);
    app.factory("adviserPortfolioCTDStatistics", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/CashTermDepositPortfolio/Stats" + clientSelector.getClientIdQueryString());
        }
    }]);
    app.factory("adviserPortfolioCTDRatingInfo", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/CashTermDepositPortfolio/Rating" + clientSelector.getClientIdQueryString());
        }
    }]);
    //END OF CTD SERVICES


    //START OF MHL SERVICES
    app.factory("adviserPortfolioMHLGeneralInfo",  ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/MortgageInvestmentPortfolio/General" + clientSelector.getClientIdQueryString());
        }
    }]);
    app.factory("adviserPortfolioMHLCashflowDetails",  ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/MortgageInvestmentPortfolio/Cashflow" + clientSelector.getClientIdQueryString());
        };
    }]);
    app.factory("adviserPortfolioMHLStatistics",  ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/MortgageInvestmentPortfolio/Stats" + clientSelector.getClientIdQueryString());
        }
    }]);
    app.factory("adviserPortfolioMHLRatingInfo",  ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/MortgageInvestmentPortfolio/Rating" + clientSelector.getClientIdQueryString());
        }
    }]);
    app.factory("adviserPortfolioMHLInvestmentProfiles",  ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/MortgageInvestmentPortfolio/Profiles" + clientSelector.getClientIdQueryString());
        }
    }]);
    app.factory("adviserPortfolioMHLCashflowInfoPropertySpecific",  ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        var DBContext = function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/MortgageInvestmentPortfolio/CashflowDetailed" + clientSelector.getClientIdQueryString());
        }


        return DBContext;
    }]);
    //END OF MHL SERVICES

    //START OF ML SERVICES
    //not implemented
    app.factory("adviserPortfolioMLRatingInfo", function ($http, $resource) {
        return function () {
            return {
                risk: 80,
                suitability: 88,
                notSuited: 20,
            }
        }
    });
    app.factory("adviserPortfolioMLCashflowDetails", function ($http, $resource, $filter) {
        return function () {
            var result = { data: [] };

            for (var i = 0; i < 12; i++) {
                var date = new Date();
                date.setMonth(date.getMonth() + i);
                result.data.push({
                    date: date,
                    income: Math.random() * 100000,
                    expense: Math.random() * 100000,
                    month: $filter('date')(date, "MMMM")
                })

            }
            result.totalIncome = 99999999;
            result.totalExpense = 88888888;
            return result;
        };
    });
    app.factory("adviserPortfolioMLStatistics", function ($http, $resource, $filter) {
        return function () {
            return [
                {
                    name: "Market Value of Portfolio",
                    value: "$888888"
                },
                {
                    name: "Value of Margin Lending Facilities",
                    value: "$66666666"
                },
                {
                    name: "Net Investment Position",
                    value: "Position"
                }, {
                    name: "AVG LVR",
                    value: "AVG LVR"
                },
                {
                    name: "MAX LVR",
                    value: "MAX LVR"
                },
                {
                    name: "Difference of LVR in Dollar Terms",
                    value: "Difference"
                },
                {
                    name: "Suitability Status",
                    value: "Status"
                }, {
                    name: "No. of People in Danger",
                    value: "55"
                }, {
                    name: "No. of People in Neutral",
                    value: "45"
                }, {
                    name: "No. of People in Comfortable",
                    value: "35"
                }, {
                    name: "Gearing Status",
                    value: "status"
                }, {
                    name: "No. of People in Negative",
                    value: "85"
                }, {
                    name: "No. of People in Neutral",
                    value: "75"
                }, {
                    name: "No. of People in Positive",
                    value: "65"
                }
            ]
        }
    })
    app.factory("adviserPortfolioMLLoanInfo", function ($http, $resource) {
        return function () {
            return {
                accountName: "name",
                loanNumber: "123456",
                tradingAccount: "account",
                dateOpened: new Date(),
                addressOne: "address one",
                addressTwo: "address two",
                addressThree: "address three",
                city: "city",
                state: "state",
                postCode: "3000",
                phone: "0888888",
                mobile: "0555555",
                work: "07444444",
                email: "email@com",
                loanProvider: "provider",
                pid: "89989898",
                lpAddressOne: "address one",
                lpAddressTwo: "address two",
                lpAddressThree: "address three",
                lpCity: "city",
                lpState: "state",
                lpPostcode: "3100",
                lpContactPerson: "Person",
                lpPhoneNumber: "05666666",
                lpFax: "08555555",
                variableLoan: "$10000",
                fixedRateLoan: "$20000",
                unsettledTransaction: "$30000",
                totalOutstandingLoan: "$2500",
                maxLoanLimits: "$2000",
                availableSpending: "$800",
                availableCash: "$500",
                baseLVR: "lvr",
                buffer: "buffer",
                marinCallLVR: "lvr",
                currentLVR: "LVR",
                netAnnualIncome: "$8000",
                negativePositiveGearing: "gearing",
                totalAnnualInterestExpense: "$400",
                totalAnnualIncomeExpense: "$500"
            }
        }
    })
    app.factory("adviserPortfolioMLCompanyProfile", function ($http, $resource) {
        return function () {
            return [
                {
                    ticker: "ticker",
                    company: "company",
                    units: Math.floor(Math.random() * 1000),
                    costPrice: Math.random() * 10000,
                    brokerage: Math.random() * 100000,
                    netCost: Math.random() * 100000,
                    marketPrice: Math.random() * 10000,
                    marketValue: Math.random() * 10000,
                    plPercentage: Math.random() * 100,
                    plValue: Math.random() * 10000,
                    annualIncome: Math.random() * 10000,
                    annualInterestExpenses: Math.random() * 10000,
                    cashflowPosition: "Position",
                    gearing: "Positive",
                    suitability: Math.floor(Math.random() * 100)
                }, {
                    ticker: "ticker",
                    company: "company",
                    units: Math.floor(Math.random() * 1000),
                    costPrice: Math.random() * 10000,
                    brokerage: Math.random() * 100000,
                    netCost: Math.random() * 100000,
                    marketPrice: Math.random() * 10000,
                    marketValue: Math.random() * 10000,
                    plPercentage: Math.random() * 100,
                    plValue: Math.random() * 10000,
                    annualIncome: Math.random() * 10000,
                    annualInterestExpenses: Math.random() * 10000,
                    cashflowPosition: "Position",
                    gearing: "Positive",
                    suitability: Math.floor(Math.random() * 100)
                }, {
                    ticker: "ticker",
                    company: "company",
                    units: Math.floor(Math.random() * 1000),
                    costPrice: Math.random() * 10000,
                    brokerage: Math.random() * 100000,
                    netCost: Math.random() * 100000,
                    marketPrice: Math.random() * 10000,
                    marketValue: Math.random() * 10000,
                    plPercentage: Math.random() * 100,
                    plValue: Math.random() * 10000,
                    annualIncome: Math.random() * 10000,
                    annualInterestExpenses: Math.random() * 10000,
                    cashflowPosition: "Position",
                    gearing: "Positive",
                    suitability: Math.floor(Math.random() * 100)
                }, {
                    ticker: "ticker",
                    company: "company",
                    units: Math.floor(Math.random() * 1000),
                    costPrice: Math.random() * 10000,
                    brokerage: Math.random() * 100000,
                    netCost: Math.random() * 100000,
                    marketPrice: Math.random() * 10000,
                    marketValue: Math.random() * 10000,
                    plPercentage: Math.random() * 100,
                    plValue: Math.random() * 10000,
                    annualIncome: Math.random() * 10000,
                    annualInterestExpenses: Math.random() * 10000,
                    cashflowPosition: "Position",
                    gearing: "Positive",
                    suitability: Math.floor(Math.random() * 100)
                }, {
                    ticker: "ticker",
                    company: "company",
                    units: Math.floor(Math.random() * 1000),
                    costPrice: Math.random() * 10000,
                    brokerage: Math.random() * 100000,
                    netCost: Math.random() * 100000,
                    marketPrice: Math.random() * 10000,
                    marketValue: Math.random() * 10000,
                    plPercentage: Math.random() * 100,
                    plValue: Math.random() * 10000,
                    annualIncome: Math.random() * 10000,
                    annualInterestExpenses: Math.random() * 10000,
                    cashflowPosition: "Position",
                    gearing: "Positive",
                    suitability: Math.floor(Math.random() * 100)
                }, {
                    ticker: "ticker",
                    company: "company",
                    units: Math.floor(Math.random() * 1000),
                    costPrice: Math.random() * 10000,
                    brokerage: Math.random() * 100000,
                    netCost: Math.random() * 100000,
                    marketPrice: Math.random() * 10000,
                    marketValue: Math.random() * 10000,
                    plPercentage: Math.random() * 100,
                    plValue: Math.random() * 10000,
                    annualIncome: Math.random() * 10000,
                    annualInterestExpenses: Math.random() * 10000,
                    cashflowPosition: "Position",
                    gearing: "Positive",
                    suitability: Math.floor(Math.random() * 100)
                }, {
                    ticker: "ticker",
                    company: "company",
                    units: Math.floor(Math.random() * 1000),
                    costPrice: Math.random() * 10000,
                    brokerage: Math.random() * 100000,
                    netCost: Math.random() * 100000,
                    marketPrice: Math.random() * 10000,
                    marketValue: Math.random() * 10000,
                    plPercentage: Math.random() * 100,
                    plValue: Math.random() * 10000,
                    annualIncome: Math.random() * 10000,
                    annualInterestExpenses: Math.random() * 10000,
                    cashflowPosition: "Position",
                    gearing: "Positive",
                    suitability: Math.floor(Math.random() * 100)
                },



            ]
        }
    });
    app.factory("adviserPortfolioMLLoanDetails", function ($http, $resource) {
        return function () {
            return {
                provider: "Colonial Geared Investments",
                data: [
                    {
                        ticker: "ticker",
                        company: "company",
                        netCostValue: Math.random() * 10000,
                        marketValue: Math.random() * 10000,
                        loanValueRatio: Math.random() * 100,
                        loanAmount: Math.random() * 100000,
                        equityInAsset: "asset"
                    }, {
                        ticker: "ticker",
                        company: "company",
                        netCostValue: Math.random() * 10000,
                        marketValue: Math.random() * 10000,
                        loanValueRatio: Math.random() * 100,
                        loanAmount: Math.random() * 100000,
                        equityInAsset: "asset"
                    }, {
                        ticker: "ticker",
                        company: "company",
                        netCostValue: Math.random() * 10000,
                        marketValue: Math.random() * 10000,
                        loanValueRatio: Math.random() * 100,
                        loanAmount: Math.random() * 100000,
                        equityInAsset: "asset"
                    }, {
                        ticker: "ticker",
                        company: "company",
                        netCostValue: Math.random() * 10000,
                        marketValue: Math.random() * 10000,
                        loanValueRatio: Math.random() * 100,
                        loanAmount: Math.random() * 100000,
                        equityInAsset: "asset"
                    }, {
                        ticker: "ticker",
                        company: "company",
                        netCostValue: Math.random() * 10000,
                        marketValue: Math.random() * 10000,
                        loanValueRatio: Math.random() * 100,
                        loanAmount: Math.random() * 100000,
                        equityInAsset: "asset"
                    }, {
                        ticker: "ticker",
                        company: "company",
                        netCostValue: Math.random() * 10000,
                        marketValue: Math.random() * 10000,
                        loanValueRatio: Math.random() * 100,
                        loanAmount: Math.random() * 100000,
                        equityInAsset: "asset"
                    },


                ]

            }
        }
    })
    app.factory("adviserPortfolioMLSuitabilityDetails", function ($http, $resource) {
        return function () {
            return {
                total: 100,
                groups: [
                    {
                        type: "Resources",
                        percentage: 40,
                        items: [
                            {
                                name: "Basic Material",
                                percentage: 20
                            },
                            {
                                name: "Energy",
                                percentage: 20
                            }
                        ]
                    },
                    {
                        type: "Industrials",
                        percentage: 10,
                        items: [
                            {
                                name: "Industrial Companies",
                                percentage: 5
                            }, {
                                name: "Technology",
                                percentage: 5
                            }
                        ]
                    }, {
                        type: "Consumer & Healthcare",
                        percentage: 20,
                        items: [
                            {
                                name: "Consumer Cyclical",
                                percentage: 10
                            }, {
                                name: "Consumer Defensive",
                                percentage: 5
                            }, {
                                name: "Healthcare",
                                percentage: 5
                            }
                        ]
                    }, {
                        type: "Financial Services",
                        percentage: 25,
                        items: [
                            {
                                name: "Financial Services",
                                percentage: 25
                            }, {
                                name: "Realestate Services",
                                percentage: 0
                            }
                        ]
                    }, {
                        type: "Communications & Utilities",
                        percentage: 5,
                        items: [
                            {
                                name: "Communication Services",
                                percentage: 5
                            }, {
                                name: "Utilities & Infrastructure",
                                percentage: 0
                            }
                        ]
                    }
                ]
            }
        }
    })
    app.factory("adviserPortfolioMLImpactOfMLtoCashFlow", function ($http, $resource) {
        return function () {
            return {
                totalMonthlyCashflow: Math.random() * 100000,
                monthlyMarginLoanExpenses: Math.random() * 100000,
                netMonthlyCashflow: Math.random() * 10000,
                situationStatus: "Good"
            }
        }
    })
    app.factory("adivserPortfolioMLTypeOfGearingStrategy", function ($http, $resource) {
        return function () {
            return {
                totalInvestmentIncome: Math.random() * 100000,
                totalInvestmentExpenses: Math.random() * 100000,
                netInvestmentPosition: "Position",
                gearingStatus: "Good"
            }
        }
    })
    app.factory("adviserPortfolioMLLVRDiversification", function ($http, $resource) {
        return function () {
            return {
                totalNumberOfSecurities: Math.floor(Math.random() * 1000),
                numberOfSecurities90lvr: Math.floor(Math.random() * 1000),
                numberOfSecurities80lvr: Math.floor(Math.random() * 1000),
                numberOfSecurities70lvr: Math.floor(Math.random() * 1000),
                numberOfSecurities60lvr: Math.floor(Math.random() * 1000),
                numberOfSecurities50lvr: Math.floor(Math.random() * 1000),
                numberOfSecurities40less: Math.floor(Math.random() * 1000)
            }
        }
    })
    app.factory("adviserPortfolioMLProviderGet", function ($http, $resource) {
        return function () {
            return [
                { name: "Colonial Geared Investments" }
            ]
        }
    })
    app.factory("adviserPortfolioMLAccountsGet", function ($resource, AppStrings) {

        return function () {

        }
    })




    //END OF ML SERVICES


    //START OF CC SERVICES
    app.factory("adviserPortfolioCCDiversification", function ($http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/CreditCardPortfolio/Diversification");
        }
    })
    app.factory("adviserPortfolioCCCashflowDetails", function ($http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/CreditCardPortfolio/Cashflow");
        };
    });
    app.factory("adviserPortfolioCCStatistics", function ($http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/CreditCardPortfolio/QuickStats");
        }
    })
    app.factory("adviserPortfolioCCCardDetails", function ($http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/CreditCardPortfolio/CardDetails");
        }
    });
    app.factory("adviserPortfolioCCLCCStat", function ($http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/CreditCardPortfolio/CardStatistics");
        }
    })
    //END OF CC SERVICES

    //START OF Insurance SERVICES
    app.factory("adviserPortfolioINSCashflowDetails",  ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/InsurancePortfolio/CashflowDetail" + clientSelector.getClientIdQueryString());
        };
    }]);
    app.factory("adviserPortfolioINSStatistics",  ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/InsurancePortfolio/Statistics" + clientSelector.getClientIdQueryString());
        }
    }])
    app.factory("adviserPortfolioINSList", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/InsurancePortfolio/InsuranceList" + clientSelector.getClientIdQueryString());

        }
    }])
    app.factory("adviserPortfolioINSConditions", ["clientSelectionService", "$http", "$resource", "AppStrings", function (clientSelector, $http, $resource, AppStrings) {
        return function () {
            return $resource(AppStrings.EDIS_IP + "api/Adviser/InsurancePortfolio/Conditions" + clientSelector.getClientIdQueryString());
        }
    }])
    //END OF Insurance SERVICES
    



    //// GEt all existing group function...............added on 07/09/2015

    //app.factory("getAllExistingGroup2", function ($http, $resource, $filter, AppStrings) {
    //    return function () {
    //        //var existingGroups = function () { $resource(AppStrings.EDIS_IP + "api/Personclient/GetAllGlientGroup"); }
    //        //return {
    //        //    existingGroups: existingGroups
    //        //}

    //        return $resource(AppStrings.EDIS_IP + "api/Personclient/GetAllGlientGroup");
    //    };


    //});













    //START OF research analysis services
    app.factory("adviserResearchAnalysisDBService", function ($http, $resource, $filter, AppStrings) {

        var banks = {
            metaData: {
                compareDetailsInvestmentAnalysisProperties: [
                    {
                        propertyName: "generalInformation",
                        displayName: "General Information",
                        properties: [
                            {
                                propertyName: "currentPrice",
                                displayName: "Current Price"
                            },
                            {
                                propertyName: "priceChangeAmount",
                                displayName: "Price Change $"
                            }
                        ]
                    },
                    {
                        propertyName: "recommendation",
                        displayName: "Recommendation",
                        properties: [
                            {
                                propertyName: "currentShortTermRecommendation",
                                displayName: "Current Short Term Recommendation"
                            },
                            {
                                propertyName: "currentLongTermRecommendation",
                                displayName: "Current Long Term Recommendation"
                            }
                        ]
                    }
                ]
            },
            data: []
        };

        var storedCompany = [];


        var allCompany = $resource(AppStrings.EDIS_IP + "api/adviser/citylist");

        function GetStockData() {



        }

        var stockData = [];

        for (var i = 0; i < banks.data.length; i++) {

            var result = [];

            for (var j = 0; j < 12; j++) {
                var date = new Date();
                date.setMonth(date.getMonth() + j);
                var item = {
                    date: date,
                    company: Math.random() * 100000,
                    portfolio: Math.random() * 100000,
                    asx200: Math.random() * 100000,
                    month: $filter('date')(date, "MMMM")
                };
                result.push(item);


                var found = false;
                for (var k = 0; k < stockData.length; k++) {
                    if (stockData[k].date === date) {
                        found = true;
                        stockData[k][banks.data[i].id] = item.company;
                    }
                }
                if (!found) {
                    var stockItem = {
                        month: item.month,
                        date: item.date
                    };
                    stockItem[banks.data[i].id] = item.company;
                    stockData.push(stockItem);
                }
            }

            //banks.data[i].indexData = result;
        }




        return function () {

            var companyList = function () {
                return $resource(AppStrings.EDIS_IP + "api/adviser/companyList");
            };
            var getCompanyProfile = function () {
                return $resource(AppStrings.EDIS_IP + "api/adviser/research/companyProfile");
            }
            var getUserCompanyList = function () {
                return storedCompany;
            }
            var addCompanyToUserList = function (id) {

                var found = false;
                for (var i = 0; i < storedCompany.length; i++) {
                    if (storedCompany[i].id === id) {
                        found = true;
                    }
                }
                if (!found) {
                    getCompanyProfile().get({ companyId: id }, function (data) {
                        if (angular.isDefined(data)) {
                            storedCompany.push(data);
                        }
                    })



                }

            }


            var remove = function (item) {
                for (var i = 0; i < storedCompany.length; i++) {
                    if (storedCompany[i].id === item.id) {
                        storedCompany.splice(i, 1);
                    }
                }
            }

            var getCompareMetaData = function () {
                return banks.metaData;
            }
            var getCompareInvestmentData = function () {
                return stockData;
            }




            return {
                getUserCompanyList: getUserCompanyList,
                addCompanyToUserList: addCompanyToUserList,
                getCompanyProfile: getCompanyProfile,
                getAllCompanyList: companyList,
                remove: remove,
                getCompareMetaData: getCompareMetaData,
                getCompareInvestmentData: getCompareInvestmentData
            }
        }
    });
    //End of research analysis service

})();