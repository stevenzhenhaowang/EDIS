/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />

(function () {
    var app = angular.module("EDIS");


    app.config(function ($routeProvider) {
        $routeProvider.when("/home", { templateUrl: "/Angular/Templates/Client/home.html" });

        $routeProvider.when("/clientcorrespondence", { templateUrl: "/Angular/Templates/client/correspondence.html" })
        $routeProvider.when("/clientportfoliosummary", { templateUrl: "/Angular/Templates/Client/clientPortfolioSummary.html" });
        $routeProvider.when("/clientportfolioae", { templateUrl: "/Angular/Templates/Client/clientPortfolioAE.html" })
        $routeProvider.when("/clientportfolioint", { templateUrl: "/Angular/Templates/Client/clientPortfolioINT.html" })
        $routeProvider.when("/clientportfoliomi", { templateUrl: "/Angular/Templates/Client/clientPortfolioMI.html" })
        $routeProvider.when("/clientportfoliodp", { templateUrl: "/Angular/Templates/Client/clientPortfolioDP.html" })
        $routeProvider.when("/clientportfoliofi", { templateUrl: "/Angular/Templates/Client/clientPortfolioFI.html" })
        $routeProvider.when("/clientportfolioctd", { templateUrl: "/Angular/Templates/Client/clientPortfolioCTD.html" })
        $routeProvider.when("/clientportfoliomhl", { templateUrl: "/Angular/Templates/Client/clientPortfolioMHL.html" })
        $routeProvider.when("/clientportfolioml", { templateUrl: "/Angular/Templates/Client/clientPortfolioML.html" })
        $routeProvider.when("/clientportfoliocc", { templateUrl: "/Angular/Templates/Client/clientPortfolioCC.html" })
        $routeProvider.when("/clientportfolioins", { templateUrl: "/Angular/Templates/Client/clientPortfolioINS.html" })
        $routeProvider.when("/clientresearchAnalysis", { templateUrl: "/Angular/Templates/Client/clientResearchAnalysis.html" })
        $routeProvider.when("/clientresearchRebalance", { templateUrl: "/Angular/Templates/Client/clientRebalance.html" })
        $routeProvider.otherwise({ templateUrl: "/Angular/Templates/Client/home.html" });
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
                var sep = value.indexOf(formats.DECIMAL_SEP);
                console.log(amount, value);
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

        // All filters must return a function. The first parameter
        // is the data that is to be filtered, and the second is an
        // argument that may be passed with a colon (searchFor:searchString)

        return function (arr, searchString) {

            if (!searchString) {
                return arr;
            }

            var result = [];

            searchString = searchString.toLowerCase();

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
    app.directive("treeView", function () {
        function link(scope, element, attrs) {
            element.kendoTreeView();
        }

        return link;

    });

    app.factory("clientRemindersDBService", function ($http, $resource) {
        var DBContext = {
            data: {
                Weekly: [

                    {
                        group: "All Reminders",
                        data: [
                            {
                                type: "No. of Clients having Birthdays today:",
                                number: 5,
                                contents: [
                                    {
                                        accountNumber: 20130001,
                                        accountName: "Mr. X and Mrs X"
                                    }
                                ]
                            },
                            {
                                type: "No. of Clients turning 55yrs within next month",
                                number: 10,
                                contents: [
                                    {
                                        accountNumber: 20130002,
                                        accountName: "Mr. Y and Mrs. Y"
                                    }
                                ]
                            }
                        ]
                    }, {
                        group: "Personal Reminders",
                        data: [
                            {
                                type: "No. of Clients having Birthdays today:",
                                number: 8,
                                contents: [
                                    {
                                        accountNumber: 20130001,
                                        accountName: "Mr. X and Mrs X"
                                    }
                                ]
                            },
                            {
                                type: "No. of Clients turning 55yrs within next month",
                                number: 12,
                                contents: [
                                    {
                                        accountNumber: 20130002,
                                        accountName: "Mr. Y and Mrs. Y"
                                    }
                                ]
                            }
                        ]
                    }, {
                        group: "Administration Reminders",
                        data: [
                            {
                                type: "No. of Portfolio Reviews today:",
                                number: 8,
                                contents: [
                                    {
                                        accountNumber: 20130001,
                                        accountName: "Mr. X and Mrs X"
                                    }
                                ]
                            },
                            {
                                type: "No. of Insurance Premium due Today",
                                number: 12,
                                contents: [
                                    {
                                        accountNumber: 20130002,
                                        accountName: "Mr. Y and Mrs. Y"
                                    }
                                ]
                            }
                        ]
                    }, {
                        group: "Portfolio Reminders",
                        data: [
                            {
                                type: "No. of Term Deposits Maturity due",
                                number: 8,
                                contents: [
                                    {
                                        accountNumber: 20130001,
                                        accountName: "Mr. X and Mrs X"
                                    }
                                ]
                            },
                            {
                                type: "No. of Clients turning 55yrs within next month",
                                number: 12,
                                contents: [
                                    {
                                        accountNumber: 20130002,
                                        accountName: "Mr. Y and Mrs. Y"
                                    }
                                ]
                            }
                        ]
                    }
                ],
                Monthly: [

                    {
                        group: "All Reminders",
                        data: [
                            {
                                type: "No. of Clients having Birthdays today:",
                                number: 5,
                                contents: [
                                    {
                                        accountNumber: 20130001,
                                        accountName: "Mr. X and Mrs X"
                                    }
                                ]
                            },
                            {
                                type: "No. of Clients turning 55yrs within next month",
                                number: 10,
                                contents: [
                                    {
                                        accountNumber: 20130002,
                                        accountName: "Mr. Y and Mrs. Y"
                                    }
                                ]
                            }
                        ]
                    }, {
                        group: "Personal Reminders",
                        data: [
                            {
                                type: "No. of Clients having Birthdays today:",
                                number: 8,
                                contents: [
                                    {
                                        accountNumber: 20130001,
                                        accountName: "Mr. X and Mrs X"
                                    }
                                ]
                            },
                            {
                                type: "No. of Clients turning 55yrs within next month",
                                number: 12,
                                contents: [
                                    {
                                        accountNumber: 20130002,
                                        accountName: "Mr. Y and Mrs. Y"
                                    }
                                ]
                            }
                        ]
                    }, {
                        group: "Administration Reminders",
                        data: [
                            {
                                type: "No. of Portfolio Reviews today:",
                                number: 8,
                                contents: [
                                    {
                                        accountNumber: 20130001,
                                        accountName: "Mr. X and Mrs X"
                                    }
                                ]
                            },
                            {
                                type: "No. of Insurance Premium due Today",
                                number: 12,
                                contents: [
                                    {
                                        accountNumber: 20130002,
                                        accountName: "Mr. Y and Mrs. Y"
                                    }
                                ]
                            }
                        ]
                    }, {
                        group: "Portfolio Reminders",
                        data: [
                            {
                                type: "No. of Term Deposits Maturity due",
                                number: 8,
                                contents: [
                                    {
                                        accountNumber: 20130001,
                                        accountName: "Mr. X and Mrs X"
                                    }
                                ]
                            },
                            {
                                type: "No. of Clients turning 55yrs within next month",
                                number: 12,
                                contents: [
                                    {
                                        accountNumber: 20130002,
                                        accountName: "Mr. Y and Mrs. Y"
                                    }
                                ]
                            }
                        ]
                    }
                ],
                Yearly: [

                    {
                        group: "All Reminders",
                        data: [
                            {
                                type: "No. of Clients having Birthdays today:",
                                number: 5,
                                contents: [
                                    {
                                        accountNumber: 20130001,
                                        accountName: "Mr. X and Mrs X"
                                    }
                                ]
                            },
                            {
                                type: "No. of Clients turning 55yrs within next month",
                                number: 10,
                                contents: [
                                    {
                                        accountNumber: 20130002,
                                        accountName: "Mr. Y and Mrs. Y"
                                    }
                                ]
                            }
                        ]
                    }, {
                        group: "Personal Reminders",
                        data: [
                            {
                                type: "No. of Clients having Birthdays today:",
                                number: 8,
                                contents: [
                                    {
                                        accountNumber: 20130001,
                                        accountName: "Mr. X and Mrs X"
                                    }
                                ]
                            },
                            {
                                type: "No. of Clients turning 55yrs within next month",
                                number: 12,
                                contents: [
                                    {
                                        accountNumber: 20130002,
                                        accountName: "Mr. Y and Mrs. Y"
                                    }
                                ]
                            }
                        ]
                    }, {
                        group: "Administration Reminders",
                        data: [
                            {
                                type: "No. of Portfolio Reviews today:",
                                number: 8,
                                contents: [
                                    {
                                        accountNumber: 20130001,
                                        accountName: "Mr. X and Mrs X"
                                    }
                                ]
                            },
                            {
                                type: "No. of Insurance Premium due Today",
                                number: 12,
                                contents: [
                                    {
                                        accountNumber: 20130002,
                                        accountName: "Mr. Y and Mrs. Y"
                                    }
                                ]
                            }
                        ]
                    }, {
                        group: "Portfolio Reminders",
                        data: [
                            {
                                type: "No. of Term Deposits Maturity due",
                                number: 8,
                                contents: [
                                    {
                                        accountNumber: 20130001,
                                        accountName: "Mr. X and Mrs X"
                                    }
                                ]
                            },
                            {
                                type: "No. of Clients turning 55yrs within next month",
                                number: 12,
                                contents: [
                                    {
                                        accountNumber: 20130002,
                                        accountName: "Mr. Y and Mrs. Y"
                                    }
                                ]
                            }
                        ]
                    }
                ]
            }
        }
        return DBContext;
    });
    app.factory("clientCorrespondenceDBService", function ($http, $resource) {
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
    app.factory("clientGetId", function ($http, $q, AppStrings) {

        function getId() {
            var deferred = $q.defer();
            $http.get(AppStrings.EDIS_IP + "api/client/id")
            .success(function (data) {
                deferred.resolve(data);
            }).error(function (data) {
                deferred.reject("Bad request");
            });
            return deferred.promise;
        }
        return getId

    });
    app.factory("clientGetAdviser", function ($http, $q, AppStrings) {
        function getClients() {
            var deferred = $q.defer();
            $http.get(AppStrings.EDIS_IP + "api/client/adviser")
            .success(function (data) {
                deferred.resolve(data);
            }).error(function (data) {
                deferred.reject("Bad request");
            });
            return deferred.promise;
        }
        return getClients
    })
    app.factory("clientGetNoteTypes", function ($http, $q, AppStrings) {
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
    })





})();


