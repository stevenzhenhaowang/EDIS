/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />



(function () {
    var app = angular.module("EDIS");
    app.directive("edisDatepicker", function () {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                element.datepicker({
                    format: "dd/mm/yyyy"
                });

                element.change(function () {
                    var value = this.value;
                    scope[attrs.ngModel] = value;
                    scope.$apply();
                });
            }
        }
    })
    app.directive("edisMap", function () {
        return {
            restrict: 'A',
            template: '<div class="map-container"><div class="details"></div><div class="map"></div>' +
                        '</div>',
            scope: { ngMapPlots: "=" },
            link: function (scope, element, attrs) {
                var plots = scope.ngMapPlots;
                var legend = scope[attrs.ngMapLegends];
                scope.$watch("ngMapPlots", function (newval, oldval) {
                    plots = newval;
                    element.find(".map-container").mapael({
                        map: {
                            name: "world_countries",
                            zoom: {
                                enabled: true,
                                maxLevel: 10
                            }
                        }
                       ,
                        plots: angular.isDefined(plots) ? plots : {},
                        legend: angular.isDefined(legend) ? legend : {}
                    });

                    element.find("circle").mouseover(function () {
                        element.find(".details").empty();
                        var text = plots[angular.element(this).data("id")].description;
                        element.find(".details").html("<span class='label label-lg label-success'>" + text + "</span>");
                    }).mouseleave(function () {
                        element.find(".details").empty();
                    });
                })
                angular.element(document).ready(function () {
                    element.find(".map-container").mapael({
                        map: {
                            name: "world_countries",
                            zoom: {
                                enabled: true,
                                maxLevel: 10
                            }
                        }
                        ,
                        plots: angular.isDefined(plots) ? plots : {},
                        legend: angular.isDefined(legend) ? legend : {}
                    });

                    element.find("circle").mouseover(function () {
                        element.find(".details").empty();
                        var text = plots[angular.element(this).data("id")].description;
                        element.find(".details").html("<span class='label label-lg label-success'>" + text + "</span>");
                    }).mouseleave(function () {
                        element.find(".details").empty();
                    });


                });


            }
        }
    })
    app.directive('disableNgAnimate', ['$animate', function ($animate) {
        return {
            restrict: 'A',
            link: function (scope, element) {
                $animate.enabled(false, element);
            }
        };
    }]);




})();