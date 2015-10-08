/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />

(function () {
    var app = angular.module("EDIS");


    app.config(function ($routeProvider) {
        $routeProvider.when("/home", { templateUrl: "/Angular/Templates/Adviser/home.html" });
        $routeProvider.when("/clientSummary", { templateUrl: "/Angular/Templates/Adviser/clientSummary.html" });
        $routeProvider.when("/clientOpenAccount", { templateUrl: "/Angular/Templates/Adviser/clientOpenAccount.html" });
        $routeProvider.when("/adviserProfile", { templateUrl: "/Angular/Templates/Adviser/adviserProfile.html" });
        $routeProvider.when("/businessDetails", { templateUrl: "/Angular/Templates/Adviser/BusinessDetails.html" });
        $routeProvider.when("/adviserActivities", { templateUrl: "/Angular/Templates/Adviser/adviserActivity.html" })
        $routeProvider.when("/reminders", { templateUrl: "/Angular/Templates/Adviser/reminders.html" })
        $routeProvider.when("/correspondence", { templateUrl: "/Angular/Templates/Adviser/correspondence.html" })
        $routeProvider.when("/portfoliosummary", { templateUrl: "/Angular/Templates/Adviser/adviserPortfolioSummary.html" });
        $routeProvider.when("/portfolioae", { templateUrl: "/Angular/Templates/Adviser/adviserPortfolioAE.html" })
        $routeProvider.when("/portfolioint", { templateUrl: "/Angular/Templates/Adviser/adviserPortfolioINT.html" })
        $routeProvider.when("/portfoliomi", { templateUrl: "/Angular/Templates/Adviser/adviserPortfolioMI.html" })
        $routeProvider.when("/portfoliodp", { templateUrl: "/Angular/Templates/Adviser/adviserPortfolioDP.html" })
        $routeProvider.when("/portfoliofi", { templateUrl: "/Angular/Templates/Adviser/adviserPortfolioFI.html" })
        $routeProvider.when("/portfolioctd", { templateUrl: "/Angular/Templates/Adviser/adviserPortfolioCTD.html" })
        $routeProvider.when("/portfoliomhl", { templateUrl: "/Angular/Templates/Adviser/adviserPortfolioMHL.html" })
        $routeProvider.when("/portfolioml", { templateUrl: "/Angular/Templates/Adviser/adviserPortfolioML.html" })
        $routeProvider.when("/portfoliocc", { templateUrl: "/Angular/Templates/Adviser/adviserPortfolioCC.html" })
        $routeProvider.when("/portfolioins", { templateUrl: "/Angular/Templates/Adviser/adviserPortfolioINS.html" })
        $routeProvider.when("/researchAnalysis", { templateUrl: "/Angular/Templates/Adviser/adviserResearchAnalysis.html" })
        $routeProvider.when("/researchRebalance", { templateUrl: "/Angular/Templates/Adviser/adviserRebalance.html" })
        $routeProvider.when("/adviserSendBulkMessages", { templateUrl: "/Angular/Templates/Adviser/adviserSendBulkMessages.html" })
        $routeProvider.when("/adviserLinkClient", { templateUrl: "/Angular/Templates/Adviser/adviserLinkClient.html" })
        $routeProvider.when("/adviserReferrals", { templateUrl: "/Angular/Templates/Adviser/adviserReferrals.html" })
        $routeProvider.when("/adviserCorporateActions", { templateUrl: "/Angular/Templates/Adviser/adviserCorporateActions.html" })
        $routeProvider.when("/adviserCreateClient", { templateUrl: "/Angular/Templates/Adviser/adviserCreateClient.html" })
        $routeProvider.when("/adviserCreateClientAccount", { templateUrl: "/Angular/Templates/Adviser/adviserCreateClientAccount.html" })
        $routeProvider.when("/adviserCreateClientGroupAccount", { templateUrl: "/Angular/Templates/Adviser/adviserCreateClientGroupAccount.html" })

        $routeProvider.when("/insertAssetsData", { templateUrl: "/Angular/Templates/Adviser/InsertAssetsData.html" })
        

        $routeProvider.otherwise({ templateUrl: "/Angular/Templates/Adviser/home.html" });
    });
    app.config(function (uiGmapGoogleMapApiProvider) {
        uiGmapGoogleMapApiProvider.configure({
            //    key: 'your api key',
            v: '3.17',
            libraries: 'weather,geometry,visualization'
        });
    })
    app.filter('summaryNum',
        ['$filter', '$locale', function (filter, locale) {
            var currencyFilter = filter('currency');
            var formats = locale.NUMBER_FORMATS;
            return function (amount, currencySymbol) {
                var value = currencyFilter(amount, currencySymbol);
                if (!angular.isNumber(amount)) {
                    return value;
                }
                var sep = value.indexOf(formats.DECIMAL_SEP);

                if (amount >= 0) {
                    value = value.substring(0, sep);
                    if (value.length > 14) {
                        value = value.slice(0, -12) + 'B';
                    } else if (value.length > 10) {
                        value = value.slice(0, -8) + 'M';
                    } else if (value.length > 7) {
                        value = value.slice(0, -4) + 'K';
                    }

                    return value;
                }
                return value.substring(0, sep) + ')';
            };
        }]);
    app.filter('adviserClientSearch', function () {
        return function (arr, searchString) {

            if (!searchString) {
                return arr;
            }

            var result = [];

            searchString = searchString.toLowerCase();
            if (!angular.isArray(arr)) {
                return arr;
            }
            // Using the forEach helper method to loop through the array
            angular.forEach(arr, function (item) {

                if (item.ClientGroupName.toLowerCase().indexOf(searchString) !== -1) {
                    result.push(item);
                }

            });

            return result;
        };

    });
    app.constant('AppStrings', {
        EDIS_IP: 'http://localhost:63381/'
    });
    app.directive("easyPieChart", function () {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                element.addClass("easy-pie-chart");
                element.append("<span class='percent'></span>");

                var cOptions = {
                    animate: 3000,
                    trackColor: "#ccc",
                    scaleColor: "#ddd",
                    lineCap: "square",
                    lineWidth: 5,
                    barColor: "#ef1e25",
                    onStep: function (from, to, percent) {
                        element.find('.percent').text(Math.round(percent));
                    }
                }
                switch (attrs.color) {
                    case "green":
                        cOptions.barColor = "#3E9C1A";
                        break;
                    case "yellow":
                        cOptions.barColor = "#FFB800";
                        break;
                    case "red":
                        cOptions.barColor = "#E60404";
                        break;
                    case "blue":
                        cOptions.barColor = "#157DEC";
                        break;
                    default:
                        break;
                }

                element.easyPieChart(cOptions);
                element.append("<p>" + attrs.title + "</p>");

            }

        }
    });
    app.directive("clientSelector", ["$route", "clientSelectionService", function ($route, clientSelectionService) {
        return {
            restrict: 'E',
            template: '<select class="form-control" style="max-width:300px" ng-model="selectedClient" ng-change="selectClient(item)" ng-options="item as item.name for item in clients"></select>',
            controller: ["$scope", "$resource", "AppStrings", function ($scope, $resource, AppStrings) {
                $resource(AppStrings.EDIS_IP + "api/adviser/clientgroups").query(function (data) {
                    var found = false;
                    $scope.clients = data;
                    for (var i = 0; i < $scope.clients.length; i++) {
                        if ($scope.clients[i].id === clientSelectionService.getCurrentClientUserId()) {
                            $scope.selectedClient = $scope.clients[i];
                            found = true;
                        }
                    }
                    var allClients = { id: -1, name: "All Clients" };
                    $scope.clients.unshift(allClients);
                    if (!found) {
                        $scope.selectedClient = allClients;
                    }
                })
                $scope.selectClient = function () {
                    if ($scope.selectedClient.id === -1) {
                        clientSelectionService.resetSelection();
                    } else {
                        clientSelectionService.selectClient($scope.selectedClient.id, $scope.selectedClient.name);
                    }

                    $route.reload();
                }

            }]
        }

    }]);
    app.directive("clientName", ["$route", "clientSelectionService", function ($route, clientSelectionService) {
        return {
            restrict: 'E',
            template: '{{clientName}}',
            controller: ["$scope",  function ($scope) {
                $scope.clientName = (clientSelectionService.getClientName() === "" ? "" : clientSelectionService.getClientName()+"'s ")

            }]
        }

    }])
    app.factory("clientSelectionService", function ($http, $resource, AppStrings) {
        var currentClientUserId = "";
        var currentClientFullName = "";
        function selectClient(clientUserId, clientUserName) {
            currentClientUserId = clientUserId;
            currentClientFullName = clientUserName;
        }
        function getCurrentClientUserId() {
            return currentClientUserId;
        }
        function hasClientSelected() {
            return currentClientUserId !== "";
        }
        function resetSelection() {
            currentClientUserId = "";
            currentClientFullName = "";
        }
        function getClientIdQueryString(){
            if (currentClientUserId !== "") {
                return "?clientGroupId=" + currentClientUserId;
            } else {
                return "";
            }
        }
        function getClientName(){
            return currentClientFullName;
        }


        return {
            selectClient: selectClient,
            getCurrentClientUserId: getCurrentClientUserId,
            hasClientSelected: hasClientSelected,
            resetSelection: resetSelection,
            getClientIdQueryString:getClientIdQueryString,
            getClientName:getClientName
        }
    })
    app.factory("AdviserBusinessDetailsDBService", function (AppStrings, $resource) {
        var GetTotalAmountFromDataCollection = function (collection) {
            var i = 0;
            angular.forEach(collection, function (value, key) {
                i = i + value.amount;
            });
            return i;
        }
        var DBContext = {
            GetBusinessRevenueData: function () {
                //ws call to retrieve business revenue data
                return $resource(AppStrings.EDIS_IP + "api/adviser/businessRevenueBrief");
            },
            GetBusinessRevenueData_Details: function () {
                return $resource(AppStrings.EDIS_IP + "api/adviser/BusinessReenueDetails");
            },
            GetInvestmentPorfolioData: function () {
                //ws call to retrieve investment portfolio data
                return $resource(AppStrings.EDIS_IP + "api/Adviser/PortfolioOverview/InvestmentPortfolio");
            },
            GetDebtInstrumentsData: function () {
                return $resource(AppStrings.EDIS_IP + "api/adviser/debtInstruments");
            },
            GetWorldMarkets: function () {
                return $resource(AppStrings.EDIS_IP + "api/adviser/worldMarkets");
            },
            GetCurrencies: function () {
                return $resource(AppStrings.EDIS_IP + "api/adviser/worldMarkets");
            },
            GetHistoricalRevenueData: function () {
                return $resource(AppStrings.EDIS_IP + "api/adviser/historicalrevenue");
            },
            GetInsuranceStatistics: function () {
                return $resource(AppStrings.EDIS_IP + "api/adviser/insuranceStatistics");
            },
            AssetStatistics_GetInvestmentStat: function () {
                return $resource(AppStrings.EDIS_IP + "api/adviser/investmentstat");
            },
            AssetStatistics_GetLendingStat: function () {
                return $resource(AppStrings.EDIS_IP + "api/adviser/lendingstat");
            },
            AssetStatistics_GetInsuranceStat: function () {
                return $resource(AppStrings.EDIS_IP + "api/adviser/InsuranceStat");
            },
            ClientPositionMonitor_GetDetails: function () {
                return $resource(AppStrings.EDIS_IP + "api/adviser/clientPositionsMonitor");
            },
            ClientDemographics_GetGeneralInfo: function () {
                return $resource(AppStrings.EDIS_IP + "api/adviser/GeoLocations");
            },
            ClientDemographics_GetDetailsForLocations: function () {
                return $resource(AppStrings.EDIS_IP + "api/adviser/GeoDetails");
            },
            CompliantFiles_GetGeneralInfo: function () {
                return $resource(AppStrings.EDIS_IP + "api/adviser/ComplianceDetails");
            }
        };

        return DBContext;
    });
    app.factory("dateParser", function () {
        return function (dateString) {
            var dates = dateString.split("/");
            return new Date(parseInt(dates[2]), parseInt(dates[1]), parseInt(dates[0]));
        };
    })
    app.factory("AdivserProfileDBService", function ($http, $resource, AppStrings, $q) {
        var DBContext = {
            GetAdviserProfile: function () {
                //ws call to retrieve adviser profile
                //var temp = {};
                //var defer = $q.defer();
                return $http.get(AppStrings.EDIS_IP + "api/adviseroverview");
                //var p = defer.promise;
                //return defer.promise;
                //return $http.get(AppStrings.EDIS_IP + "api/adviseroverview");
            },
            GetAdviserImage: function () {
                return "/Images/adviser2_sample.jpg";
            },
            GetAdviserAdditionalInfo: function () {

                var result = {
                    additionalInfo: { BirthDate: '21st Nov, 1988', Website: 'www.edis.com.au', LastLogin: '22nd Feb, 2012', DateJoined: '24th Mar, 2009', Email: 'arthur.goldsmith@edis.com.au' },
                    adviserDescription: 'We provide you with full financial planning advice on retirement planning, government benefits, taxation, and Personal Insurance. ',
                    adviserProfessionalType: 'Investment Adviser',
                    adviserIndustryExp: '5 Years',
                    adviserProfessionalAssociations: [{ ProfessionalAssociations: 'Financial Planning Association of Australia (FPA)' }, { ProfessionalAssociations: 'CPA Australia (CPA)' }, { ProfessionalAssociations: 'Stockbrokers Association of Australia (SAA)' }],
                    adviserServicesProvided: [{ ServiceName: 'Financial Planning' }, { ServiceName: 'Insurances' }, { ServiceName: 'Investment Services' }, { ServiceName: 'Business Development Manager' }],
                    adviserCapability: {
                        GeneralInvestDebt: [{ Capability: 'Managed Investments' }, { Capability: 'Australian Equity' }, { Capability: 'Direct Property' }, { Capability: 'Trauma Insurance' }],
                        adviserPlatforms: [{ Platform: 'EDS Services Software' }, { Platform: 'Iress X-Plann' }, { Platform: 'Coin' }, { Platform: 'Mid Winter' }, { Platform: 'Adviser Logic' }, { Platform: 'Iress Market Systems' }, { Platform: 'CommSec Adviser Services' }, { Platform: 'Colonial First State First Wrap Account' }],
                        approvedResearch: [{ Research: 'Morningstar' }, { Research: 'Van Eyke' }, { Research: 'Lonsec' }, { Research: 'Mercer' }, { Research: 'Alter Vista' }, { Research: 'CBA Research' }, { Research: 'UBS Research' }, { Research: 'Citigroup Research' }],
                        adviserSpecialisedProducts: [{ SpecialProduct: 'Structured Products' }, { SpecialProduct: 'Exchange Traded Options' }, { SpecialProduct: 'Warrants' }, { SpecialProduct: 'Contract for Difference (CFD)' }, { SpecialProduct: 'Initial Public Offers' }, { SpecialProduct: 'Managed Discretionary Accounts' }, { SpecialProduct: 'Self Managed Superannuation Funds (SMSF)' }, { SpecialProduct: 'Taxation Advice' }]
                    },
                    adviserTargetClient: {
                        AnnualIncomeLevels: 'Between $100,000 to $200,000', InvestibleAssetsLevel: 'Between $400,000 to $600,000', TotalAssetsLevel: 'Greater than $600,000'

                    },
                    adviserEducation: [
                        { EducationLevels: 'Masters', Institution: 'Monash', CourseName: 'MIT', CurrentlyStudying: '1' },
                        { EducationLevels: 'Professor', Institution: 'University of Melbourne', CourseName: 'MIT', CurrentlyStudying: '1' },
                        { EducationLevels: 'Diploma', Institution: 'Deakin University', CourseName: 'MIT', CurrentlyStudying: '1' }
                    ],
                    adviserRating: 5,
                    adviserSubscription: {
                        PlanType: 'Full Subscription 12 months', SubscriptionStatus: 'Active', VerifiedStatus: 'Verified'
                    }
                };

                return result;
            },
            GetAdviserManagementDetails: function () {
                var result = {
                    TotalAssets: '10000000', TotalManagedInvestments: '5000000', TotalDirectAustralianEquities: '5000000', TotalDirectInternational: '30000000', TotalFixedInterest: '5000000', TotalLendingBook: '5000000', ApproxClientNumbersId: '1001 to 1500 clients'
                };
                return result;
            },
            GetAdviserDealerGroup: function () {
                var result = {
                    Adviser_DealerGroupDetails: { UserId: '5eab3e28-1a84-445f-8fb3-e079360f1dc4', DealerGroupName: 'EDIS DealerGroup', AFSL: '123 445 67', HasDerivativesLicense: '1', IsAuthorisedRep: '1', AuthorisedRepNumber: '11255687', AddressLn1: 'Level 10', AddressLn2: '201 Sussex Street', AddressLn3: 'Level 10', Suburb: 'Sydney', State: 'NSW', PostCode: '2000', Country: 'Australia', LastUpdated: '14/01/15 11:40:59 AM', }

                };

                return result;

            },
            GetAdviserBookmarks: function () {

                var result = [{ Href: "http://www.cba.com.au", Description: "Commonwealth Bank of Australia", Logo: "" },
                        { Href: "http://www.anz.com.au", Description: "Australia and NZ Bank", Logo: "" },
                        { Href: "http://www.nab.com.au", Description: "National Australia Bank", Logo: "" }
                ]
                return result;
            }

        };
        return DBContext;
    });
    app.factory("AdviserActivityDBService", function ($http, $resource) {
        var DBContext = {
            Administration: {
                GetWeeklyActivityReport_Notes: function () {
                    var result = {
                        data: [
                            {
                                AccountNumber: 20130001,
                                AccountName: "XXXXX",
                                NumberOfNotes: 2
                            }, {
                                AccountNumber: 20130002,
                                AccountName: "YYYYY",
                                NumberOfNotes: 5
                            }, {
                                AccountNumber: 20130003,
                                AccountName: "ZZZZZ",
                                NumberOfNotes: 8
                            },
                        ],
                        total: 50
                    };

                    return result;
                },
                GetWeeklyActivityReport_Recording: function () {
                    var result = {
                        data: [
                            {
                                AccountNumber: 20130001,
                                AccountName: "XXXXXV",
                                NumberOfNotes: 2
                            }, {
                                AccountNumber: 20130002,
                                AccountName: "YYYYYV",
                                NumberOfNotes: 5
                            }, {
                                AccountNumber: 20130003,
                                AccountName: "ZZZZZV",
                                NumberOfNotes: 8
                            },
                        ],
                        total: 51
                    };

                    return result;

                },
                GetWeeklyActivityReport_Email: function () {
                    var result = {
                        data: [
                            {
                                AccountNumber: 20130001,
                                AccountName: "XXXXXE",
                                NumberOfNotes: 2
                            }, {
                                AccountNumber: 20130002,
                                AccountName: "YYYYYE",
                                NumberOfNotes: 5
                            }, {
                                AccountNumber: 20130003,
                                AccountName: "ZZZZZE",
                                NumberOfNotes: 8
                            },
                        ],
                        total: 52
                    };

                    return result;
                }
            },
            Account: {
                GetWeeklyActivityReport_Notes: function () {
                    var result = {
                        data: [
                            {
                                AccountNumber: 20130001,
                                AccountName: "XXXXXA",
                                NumberOfNotes: 2
                            }, {
                                AccountNumber: 20130002,
                                AccountName: "YYYYYA",
                                NumberOfNotes: 5
                            }, {
                                AccountNumber: 20130003,
                                AccountName: "ZZZZZA",
                                NumberOfNotes: 8
                            },
                        ],
                        total: 53
                    };

                    return result;
                },
                GetWeeklyActivityReport_Recording: function () {
                    var result = {
                        data: [
                            {
                                AccountNumber: 20130001,
                                AccountName: "XXXXX",
                                NumberOfNotes: 2
                            }, {
                                AccountNumber: 20130002,
                                AccountName: "YYYYY",
                                NumberOfNotes: 5
                            }, {
                                AccountNumber: 20130003,
                                AccountName: "ZZZZZ",
                                NumberOfNotes: 8
                            },
                        ],
                        total: 54
                    };

                    return result;
                },
                GetWeeklyActivityReport_Email: function () {
                    var result = {
                        data: [
                            {
                                AccountNumber: 20130001,
                                AccountName: "XXXXX",
                                NumberOfNotes: 2
                            }, {
                                AccountNumber: 20130002,
                                AccountName: "YYYYY",
                                NumberOfNotes: 5
                            }, {
                                AccountNumber: 20130003,
                                AccountName: "ZZZZZ",
                                NumberOfNotes: 8
                            },
                        ],
                        total: 55
                    };

                    return result;
                }
            },
            Portfolio: {
                GetWeeklyActivityReport_Notes: function () {
                    var result = {
                        data: [
                            {
                                AccountNumber: 20130001,
                                AccountName: "XXXXX",
                                NumberOfNotes: 2
                            }, {
                                AccountNumber: 20130002,
                                AccountName: "YYYYY",
                                NumberOfNotes: 5
                            }, {
                                AccountNumber: 20130003,
                                AccountName: "ZZZZZ",
                                NumberOfNotes: 8
                            },
                        ],
                        total: 56
                    };

                    return result;
                },
                GetWeeklyActivityReport_Recording: function () {
                    var result = {
                        data: [
                            {
                                AccountNumber: 20130001,
                                AccountName: "XXXXX",
                                NumberOfNotes: 2
                            }, {
                                AccountNumber: 20130002,
                                AccountName: "YYYYY",
                                NumberOfNotes: 5
                            }, {
                                AccountNumber: 20130003,
                                AccountName: "ZZZZZ",
                                NumberOfNotes: 8
                            },
                        ],
                        total: 58
                    };

                    return result;
                },
                GetWeeklyActivityReport_Email: function () {
                    var result = {
                        data: [
                            {
                                AccountNumber: 20130001,
                                AccountName: "XXXXX",
                                NumberOfNotes: 2
                            }, {
                                AccountNumber: 20130002,
                                AccountName: "YYYYY",
                                NumberOfNotes: 5
                            }, {
                                AccountNumber: 20130003,
                                AccountName: "ZZZZZ",
                                NumberOfNotes: 8
                            },
                        ],
                        total: 59
                    };

                    return result;
                }
            },
            GeneralStats: {
                Weekly: function () {
                    var result = [{
                        Group: "No. of Notes Entry",
                        Number: 5
                    }, {
                        Group: "No. of Voice Recording",
                        Number: 5
                    }, {
                        Group: "No. of Messages sent to clients",
                        Number: 5
                    }, {
                        Group: "No. of New Referrals",
                        Number: 5
                    }, {
                        Group: "No. of New Clients",
                        Number: 5
                    }, {
                        Group: "No. of SOA Written & Sent",
                        Number: 5
                    },
                    ];

                    return result;
                },
                Monthly: function () {
                    var result = [{
                        Group: "No. of Notes Entry",
                        Number: 10
                    }, {
                        Group: "No. of Voice Recording",
                        Number: 10
                    }, {
                        Group: "No. of Email sent to clients",
                        Number: 10
                    }, {
                        Group: "No. of New Referrals",
                        Number: 10
                    }, {
                        Group: "No. of New Clients",
                        Number: 10
                    }, {
                        Group: "No. of SOA Written & Sent",
                        Number: 10
                    },
                    ];

                    return result;
                },
                Yearly: function () {
                    var result = [{
                        Group: "No. of Notes Entry",
                        Number: 102
                    }, {
                        Group: "No. of Voice Recording",
                        Number: 100
                    }, {
                        Group: "No. of Email sent to clients",
                        Number: 100
                    }, {
                        Group: "No. of New Referrals",
                        Number: 100
                    }, {
                        Group: "No. of New Clients",
                        Number: 100
                    }, {
                        Group: "No. of SOA Written & Sent",
                        Number: 100
                    },
                    ];

                    return result;
                }
            }

        };

        return DBContext;
    });
    app.factory("AdviserRemindersDBService", function ($http, $resource, AppStrings) {
        return $resource(AppStrings.EDIS_IP + "api/adviser/reminders");
    });
    app.factory("AdviserCorrespondenceDBService", function ($http, $resource) {
        var DBContext = {
            notes: [
                {
                    date: new Date(),
                    individual: "Mr. A",
                    commentray: "Something important"
                }, {
                    date: new Date(),
                    individual: "Mr. B",
                    commentray: "Something important"
                }, {
                    date: new Date(),
                    individual: "Mr. C",
                    commentray: "Something important"
                }, {
                    date: new Date(),
                    individual: "Mr. D",
                    commentray: "Something important"
                }
            ],
            messages: [
                {
                    date: new Date(),
                    email: "N234412",
                    accountNumber: 20130001,
                    accountName: "Mrs. A",
                    message: "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,"
                }, {
                    date: new Date(),
                    email: "N234413",
                    accountNumber: 20130001,
                    accountName: "Mrs. A",
                    message: "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,"
                }, {
                    date: new Date(),
                    email: "N234414",
                    accountNumber: 20130001,
                    accountName: "Mrs. A",
                    message: "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,"
                }, {
                    date: new Date(),
                    email: "N234415",
                    accountNumber: 20130001,
                    accountName: "Mrs. A",
                    message: "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,"
                }, {
                    date: new Date(),
                    email: "N234416",
                    accountNumber: 20130001,
                    accountName: "Mrs. A",
                    message: "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus. Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. Nam quam nunc, blandit vel, luctus pulvinar, hendrerit id, lorem. Maecenas nec odio et ante tincidunt tempus. Donec vitae sapien ut libero venenatis faucibus. Nullam quis ante. Etiam sit amet orci eget eros faucibus tincidunt. Duis leo. Sed fringilla mauris sit amet nibh. Donec sodales sagittis magna. Sed consequat, leo eget bibendum sodales, augue velit cursus nunc,"
                },
            ],
            voices: [
                {
                    date: new Date(),
                    title: "Recording title 001",
                    length: 50,
                    size: 100,
                    type: "audio/ogg",
                    src: "dummy.mp3"
                }, {
                    date: new Date(),
                    title: "Recording title 002",
                    length: 50,
                    size: 100,
                    type: "audio/ogg",
                    src: "dummy.mp3"
                }, {
                    date: new Date(),
                    title: "Recording title 003",
                    length: 50,
                    size: 100,
                    type: "audio/ogg",
                    src: "dummy.mp3"
                },
            ],
            documents: [
                {
                    title: "Personal Profile",
                    type: "folder",
                    items: [
                        {
                            title: "document one",
                            type: "document",
                            url: "",
                            items: []
                        }
                    ]
                }, {
                    title: "Australian Equity",
                    type: "folder",
                    items: [
                        {
                            title: "Sub Folder One",
                            type: "folder",
                            items: [{
                                title: "document two",
                                type: "document",
                                url: "google.com",
                                children: []
                            }]
                        }
                    ]
                }
            ]
        };


        return DBContext;
    });
    app.directive("treeView", function () {
        function link(scope, element, attrs) {
            element.kendoTreeView();
        }

        return link;

    });
    app.factory("AdviserPortfolioSummaryDBService", function ($http,
        $resource, $filter, AppStrings, clientSelectionService) {
        var DBContext = {
            assetSummary: function () {
                return $resource(AppStrings.EDIS_IP + "api/Adviser/PortfolioOverview/Summary" + clientSelectionService.getClientIdQueryString());
            },
            cashflowSummary: function () {
                return $resource(AppStrings.EDIS_IP + "api/Adviser/PortfolioOverview/Cashflow" + clientSelectionService.getClientIdQueryString());
            },
            bestPerformingSummary: function () {
                return $resource(AppStrings.EDIS_IP + "api/Adviser/PortfolioOverview/BestPerforming" + clientSelectionService.getClientIdQueryString());
            },
            worstPerformingSummary: function () {
                return $resource(AppStrings.EDIS_IP + "api/Adviser/PortfolioOverview/WorstPerforming" + clientSelectionService.getClientIdQueryString());
            },
            general: function () {
                return $resource(AppStrings.EDIS_IP + "api/Adviser/PortfolioOverview/General" + clientSelectionService.getClientIdQueryString());
            },
            portfolioStat: function () {
                return $resource(AppStrings.EDIS_IP + "api/Adviser/PortfolioOverview/Stastics" + clientSelectionService.getClientIdQueryString());
            },
            recentStockData: function () {
                return $resource(AppStrings.EDIS_IP + "api/PortfolioOverview/RecentStock" + clientSelectionService.getClientIdQueryString());
            },
            portfolioRating: function () {
                return $resource(AppStrings.EDIS_IP + "api/Adviser/PortfolioOverview/PortfolioRating" + clientSelectionService.getClientIdQueryString());
            }
        };
        return DBContext;
    });
    app.factory("AdviserClientAccountsDBService", function ($http, $resource) {
        var DBContext = {
            ClientGroups: [
                { ClientGroupNum: "12345", ClientGroupName: "Mr & Mrs Smith Trust Account", NumberOfMembers: 3 },
                { ClientGroupNum: "45678", ClientGroupName: "Mr & Mrs Citizen Trust Account", NumberOfMembers: 2 },
                { ClientGroupNum: "53536", ClientGroupName: "Mr & Mrs Jennings Trust Account", NumberOfMembers: 1 }
            ],
            clientMembers: [
                { Image: "/Images/clients/client-1.png", FirstName: "John", LastName: "Smith", ClientGroupNum: "12345", ClientGroupName: "Mr & Mrs Smith Trust Account", NumberOfMembers: 3, Compliance: 3 },
                { Image: "/Images/clients/client-2.png", FirstName: "Margaret", LastName: "Smith", ClientGroupNum: "12345", ClientGroupName: "Mr & Mrs Smith Trust Account", NumberOfMembers: 3, Compliance: 4 },
                { Image: "/Images/clients/client-1.png", FirstName: "John", LastName: "Smith", ClientGroupNum: "12345", ClientGroupName: "Mr & Mrs Smith Trust Account", NumberOfMembers: 3, Compliance: 5 },
                { Image: "/Images/clients/client-2.png", FirstName: "Margaret", LastName: "Smith", ClientGroupNum: "45678", ClientGroupName: "Mr & Mrs Smith Trust Account", NumberOfMembers: 3, Compliance: 5 },
                { Image: "/Images/clients/client-1.png", FirstName: "John", LastName: "Smith", ClientGroupNum: "45678", ClientGroupName: "Mr & Mrs Smith Trust Account", NumberOfMembers: 3, Compliance: 4 },
                { Image: "/Images/clients/client-2.png", FirstName: "Margaret", LastName: "Smith", ClientGroupNum: "45678", ClientGroupName: "Mr & Mrs Smith Trust Account", NumberOfMembers: 3, Compliance: 3 },
                { Image: "/Images/clients/client-1.png", FirstName: "John", LastName: "Smith", ClientGroupNum: "53536", ClientGroupName: "Mr & Mrs Smith Trust Account", NumberOfMembers: 3, Compliance: 4 },
                { Image: "/Images/clients/client-2.png", FirstName: "Margaret", LastName: "Smith", ClientGroupNum: "53536", ClientGroupName: "Mr & Mrs Smith Trust Account", NumberOfMembers: 3, Compliance: 4 },
                { Image: "/Images/clients/client-3.png", FirstName: "Alexander", LastName: "Smith", ClientGroupNum: "53536", ClientGroupName: "Mr & Mrs Smith Trust Account", NumberOfMembers: 3, Compliance: 4 }
            ]
            //clientMember: [{ Image: "/Images/clients/client-1.png" }, { client: "/Images/clients/client-2.png" }, { client: "/Images/clients/client-3.png" }]
        }
        return DBContext;
    });
    app.factory("CorrespondenceTokenGetter", function ($http, $q, AppStrings) {

        function getTokenPromise() {
            var deferred = $q.defer();
            $http.get(AppStrings.EDIS_IP + "api/correspondence/file/token")
            .success(function (data) {
                deferred.resolve(data);
            }).error(function (data) {
                deferred.reject("Bad request");
            });
            return deferred.promise;
        }
        return {
            getToken: getTokenPromise
        };


    });
    app.factory("assetClassesGetter", function ($http, $q, AppStrings) {

        function getClasses() {
            var deferred = $q.defer();
            $http.get(AppStrings.EDIS_IP + "api/common/assetClasses")
            .success(function (data) {
                deferred.resolve(data);
            }).error(function (data) {
                deferred.reject("Bad request");
            });
            return deferred.promise;
        }
        return getClasses


    });
    app.factory("productClassGetter", function ($http, $q, AppStrings) {

        function getClasses() {
            var deferred = $q.defer();
            $http.get(AppStrings.EDIS_IP + "api/common/productClasses")
            .success(function (data) {
                deferred.resolve(data);
            }).error(function (data) {
                deferred.reject("Bad request");
            });
            return deferred.promise;
        }
        return getClasses

    });
    app.factory("adviserGetId", function ($http, $q, AppStrings) {

        function getId() {
            var deferred = $q.defer();
            $http.get(AppStrings.EDIS_IP + "api/adviser/accountNumber")
            .success(function (data) {
                deferred.resolve(data);
            }).error(function (data) {
                deferred.reject("Bad request");
            });
            return deferred.promise;
        }
        return getId

    });
    app.factory("adviserGetClients", function ($http, $q, AppStrings) {
        function getClients() {
            var deferred = $q.defer();
            $http.get(AppStrings.EDIS_IP + "api/adviser/clients")
            .success(function (data) {
                deferred.resolve(data);
            }).error(function (data) {
                deferred.reject("Bad request");
            });
            return deferred.promise;
        }
        return getClients
    });
    app.factory("adviserGetNoteTypes", function ($http, $q, AppStrings) {
        function getNoteTypes() {
            var deferred = $q.defer();
            $http.get(AppStrings.EDIS_IP + "api/correspondence/noteType")
            .success(function (data) {
                deferred.resolve(data);
            }).error(function (data) {
                deferred.reject("Bad request");
            });
            return deferred.promise;
        }
        return getNoteTypes
    });
    app.factory("adviserGetClientGroups", function ($http, $q, AppStrings) {
        function getClients() {
            var deferred = $q.defer();
            $http.get(AppStrings.EDIS_IP + "api/adviser/clientgroups")
            .success(function (data) {
                deferred.resolve(data);
            }).error(function (data) {
                deferred.reject("Bad request");
            });
            return deferred.promise;
        }
        return getClients
    });


    app.factory("adviserGetClientAccountTypes", function ($http, $q, AppStrings) {
        function getClientAccountTypes() {
            var deferred = $q.defer();
            $http.get(AppStrings.EDIS_IP + "api/adviser/accountType")
            .success(function (data) {
                deferred.resolve(data);
            }).error(function (data) {
                deferred.reject("Bad request");
            });
            return deferred.promise;
        }
        return getClientAccountTypes
    });

    //app.factory("adviserGetClientsForGroup", function ($http, $q, AppStrings) {
    //    function getClients() {
    //        var deferred = $q.defer();
    //        $http.post(AppStrings.EDIS_IP + "api/adviser/clientsForGroup")
    //        .success(function (data) {
    //            deferred.resolve(data);
    //        }).error(function (data) {
    //            deferred.reject("Bad request");
    //        });
    //        return deferred.promise;
    //    }
    //    return getClients
    //});


    app.factory("adviserCreateClientAccount", function ($resource) {
    })
})();


