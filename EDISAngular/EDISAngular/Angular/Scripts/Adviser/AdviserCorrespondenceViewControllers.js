/// <reference path="C:\Users\ahksysuser03\Desktop\EDIS\EDIS\Scripts/angular.js" />


(function () {
    var app = angular.module("EDIS");
    app.controller("AdviserCorrespondenceViewController", ["$scope", "AdviserCorrespondenceDBService",
        "$modal", "CorrespondenceTokenGetter", "assetClassesGetter", "productClassGetter", "adviserGetClients", "adviserGetNoteTypes",
        "adviserGetId", "AppStrings", function ($scope, DBContext,
           $modal, tokenService, assetClassesGetter, productClassGetter, adviserGetClients, adviserGetNoteTypes, adviserGetId, AppStrings) {
            $scope.counter = 0;
            $scope.notes = DBContext.notes;
            $scope.notesOptions = {
                dataSource: {
                    transport: {
                        read: AppStrings.EDIS_IP + "api/adviser/correspondence/notes"
                    },
                    pageSize: 5,
                    serverPaging: false,
                    serverSorting: false
                },
                pageable: true,
                columns: [{
                    field: "date",
                    title: "Date",
                    width: "40%",
                    template: "{{dataItem.date | date}}"
                }, {
                    field: "clientName",
                    title: "Client",
                    width: "30%"
                }, {
                    field: "subject",
                    title: "Subject",
                    width: "30%"
                }]
            };
            $scope.messages = DBContext.messages;
            $scope.messageOptions = {
                dataSource: {
                    transport: {
                        read: AppStrings.EDIS_IP + "api/adviser/correspondence/messages"
                    },
                    pageSize: 5,
                    serverPaging: false,
                    serverSorting: false
                },
                pageable: true,
                columns: [{
                    field: "date",
                    title: "Date",
                    width: "40%",
                    template: "{{dataItem.date | date}}"
                }, {
                    field: "clientName",
                    title: "Client",
                    width: "30%"
                }, {
                    field: "subject",
                    title: "Subject",
                    width: "30%"
                }]
            };
            $scope.voices = DBContext.voices;
            $scope.voiceOptions = {
                dataSource: {
                    transport: {
                        read: AppStrings.EDIS_IP + "api/adviser/correspondence/voice"
                    },
                    pageSize: 5,
                    serverPaging: false,
                    serverSorting: false
                },
                pageable: true,
                columns: [{
                    field: "date",
                    title: "Date",
                    width: "40%",
                    template: "{{dataItem.date | date}}"
                }, {
                    field: "clientName",
                    title: "Client",
                    width: "30%"
                }, {
                    field: "subject",
                    title: "Subject",
                    width: "30%"
                }]
            };
            $scope.documents = DBContext.documents;

            $scope.documentOptions = {
                dataSource: $scope.documents,
                dataTextField: "title",
                dataUrlField: "url"
            }

            $scope.userdetails = { name: "name" };
            $scope.newNote = function (folowNote) {
                var modalInstance = $modal.open({
                    templateUrl: "newCorrespondenceMessage",
                    controller: "AdviserCorrespondenceNewNoteController",
                    resolve: {
                        userdetails: function () {
                            return $scope.userdetails;
                        },
                        getToken: tokenService.getToken,
                        getAssetClasses: assetClassesGetter,
                        getProductClasses: productClassGetter,
                        getClients: adviserGetClients,
                        adviserGetNoteTypes: adviserGetNoteTypes,
                        adviserGetId: adviserGetId,
                        followUpNote: function () {
                            if (angular.isDefined(folowNote) && folowNote !== null) {
                                return folowNote
                            } else {
                                return null;
                            }
                        }
                    },
                    backdrop: true
                });

                modalInstance.result.then(function (result) {
                    try {
                        $scope.noteGrid.dataSource.read();
                    } catch (e) {
                    }
                    try {
                        $scope.messageGrid.dataSource.read();
                    } catch (e) {
                    }
                    try {
                        $scope.voiceGrid.dataSource.read();
                    } catch (e) {
                    }
                });
            };

            $scope.newMessage = function () {
                var modalInstance = $modal.open({
                    templateUrl: "newCorrespondenceMessage",
                    controller: "AdviserCorrespondenceNewMessageController",
                    resolve: {
                        userdetails: function () {
                            return $scope.userdetails;
                        }
                    },
                    backdrop: true
                });

                modalInstance.result.then(function (result) {
                    console.log(result.reason);
                });
            };

            $scope.showDetail = function (item) {
                $modal.open({
                    templateUrl: 'correspondenceDetails',
                    controller: "displayCorrespondenceDetailsController",
                    resolve: {
                        detail:function(){return item}
                    }
                });
            }

        }]);
    app.controller("AdviserCorrespondenceNewNoteController", function ($scope, $modalInstance, userdetails, $http,
        AppStrings, getToken, getAssetClasses, getProductClasses, getClients, adviserGetNoteTypes, adviserGetId, followUpNote) {


        $scope.assetClasses = getAssetClasses;
        $scope.productClasses = getProductClasses;
        $scope.noteTypes = adviserGetNoteTypes;
        $scope.adviserId = adviserGetId;
        $scope.clients = getClients;
        $scope.followUpNote = followUpNote;

        $scope.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };
        $scope.cannotUpload = false;



        $scope.options =
            {
                async: {
                    saveUrl: AppStrings.EDIS_IP + "api/correspondence/file/upload?resourceToken=" + getToken,
                    removeUrl: AppStrings.EDIS_IP + "api/correspondence/file/remove?resourceToken=" + getToken,
                    autoUpload: true
                },
                multiple: false
            };

        $scope.close = function () {
            $modalInstance.dismiss("cancel");
        }

        function convertStringToDate(input) {
            if (angular.isUndefined(input) || input === null || input === "") {
                return null;
            } else {
                var dates = input.split("/");
                return new Date(dates[2], dates[1], dates[0]);
            }
        }

        $scope.save = function () {

            if ($scope.followUpNote) {
                var model = {
                    existingnoteId: $scope.followUpNote.noteId,
                    body: $scope.commentary,
                };
                $http.post(AppStrings.EDIS_IP + "api/correspondence/followup", model)
                .success(function () {
                    $modalInstance.close();
                })
                .error(function (data) {
                    $scope.error = data;
                })
            } else {
                var model = {
                    adviserNumber: $scope.adviserId,
                    clientId: $scope.clientId,
                    assetTypeId: $scope.assetClass,
                    productTypeId: $scope.productClass,
                    timespent: $scope.timeSpent,
                    subject: $scope.subject,
                    body: $scope.commentary,
                    followupActions: $scope.actions,
                    dateCompleted: convertStringToDate($scope.completionDate),
                    followupDate: convertStringToDate($scope.followupDate),
                    reminder: $scope.removeFromReminder === "true" ? false : true,
                    noteTypeId: $scope.messageType,
                    resourceToken: getToken,
                    dateDue: convertStringToDate($scope.completionDate),
                };
                $http.post(AppStrings.EDIS_IP + "api/correspondence/create", model)
                .success(function () {
                    $modalInstance.close();
                })
                .error(function (data) {
                    $scope.error = data;
                })
            }


        }


    });
    app.controller("displayCorrespondenceDetailsController", function ($scope, detail) {
        $scope.detail = detail;
    })

})();