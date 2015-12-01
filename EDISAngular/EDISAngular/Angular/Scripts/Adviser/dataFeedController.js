
angular.module("EDIS")

.controller("dataFeedController", function ($http, $resource, $filter, $q, $scope, AppStrings, dateParser) {
    $scope.loadEquityData = function () {
        var sectors = [];
        var equityTypes = [];

        $http.get(AppStrings.EDIS_IP + "api/Admin/DataFeed/GetSetctorDetails")
            .success(function (data) {
                $scope.sectors = data;
            }).error(function (data) {
                console.log("Error.....");
            });

        $http.get(AppStrings.EDIS_IP + "api/Admin/DataFeed/GetEquityTypesDetails")
            .success(function (data) {
                $scope.equityTypes = data;
            }).error(function (data) {
                console.log("Error.....");
            });
    }

    $scope.loadBondData = function () {
        $http.get(AppStrings.EDIS_IP + "api/Admin/DataFeed/GetFrequenciesDetails")
            .success(function (data) {
                $scope.frequencies = data;
            }).error(function (data) {
                console.log("Error.....");
            });

        $http.get(AppStrings.EDIS_IP + "api/Admin/DataFeed/GetBondTypes")
            .success(function (data) {
                $scope.bondTypes = data;
            }).error(function (data) {
                console.log("Error.....");
            });
    }

    $scope.loadAssetPriceData = function () {
        $http.get(AppStrings.EDIS_IP + "api/Admin/DataFeed/GetPropertyAddresses")
            .success(function (data) {
                $scope.addresses = data;
            }).error(function (data) {
                console.log("Error.....");
            });

        $http.get(AppStrings.EDIS_IP + "api/Admin/DataFeed/GetAssetPriceTypes")
            .success(function (data) {
                $scope.assetTypes = data;
            }).error(function (data) {
                console.log("Error.....");
            });
    }

    $scope.loadResearchValueData = function(){
        $http.get(AppStrings.EDIS_IP + "api/Admin/DataFeed/GetResearchValueKeys")
            .success(function (data) {
                $scope.keys = data;
            }).error(function (data) {
                console.log("Error.....");
            });

        $http.get(AppStrings.EDIS_IP + "api/Admin/DataFeed/GetAssetPriceTypes")
            .success(function (data) {
                $scope.assetTypes = data;
            }).error(function (data) {
                console.log("Error.....");
            });

        $http.get(AppStrings.EDIS_IP + "api/Admin/DataFeed/GetPropertyAddresses")
            .success(function (data) {
                $scope.addresses = data;
            }).error(function (data) {
                console.log("Error.....");
            });
    }

    $scope.loadTickerByAssetType = function () {
        var assetType = $scope.collection.assetPrice.assetType;

        $http.get(AppStrings.EDIS_IP + "api/Admin/DataFeed/GetEquityTickersByType?assetType=" + assetType)
        .success(function (data) {
            $scope.tickers = data;
        }).error(function (data) {
            console.log("Error.....");
        });
    }

    $scope.loadUploadDataTypes = function(){
        $http.get(AppStrings.EDIS_IP + "api/Admin/DataFeed/GetUploadDataTypes")
            .success(function (data) {
                $scope.dataTypes = data;
            }).error(function (data) {
                console.log("Error.....");
            });
    }

    $scope.uploadFile = function () {

        $scope.options = {
            async: {
                saveUrl: AppStrings.EDIS_IP + "api/Admin/DataFeed/UploadDataFile?dataType=" + $scope.collection.uploadData.dataType,
                autoUpload: true
            },
            multiple: false
        };
        $scope.options.async.saveUrl = AppStrings.EDIS_IP + "api/Admin/DataFeed/UploadDataFile?dataType=" + $scope.collection.uploadData.dataType;
    }


    $scope.loadTickerByAssetTypeForRV = function () {
        var assetType = $scope.collection.researchValue.assetType;

        $http.get(AppStrings.EDIS_IP + "api/Admin/DataFeed/GetEquityTickersByType?assetType=" + assetType)
        .success(function (data) {
            $scope.tickers = data;
        }).error(function (data) {
            console.log("Error.....");
        });
    }
    

    $scope.save = function () {
        var data = {};
        if ($scope.collection.transactionType === "Equity") {
            data = {
                Ticker: $scope.collection.equity.ticker,
                CompanyName: $scope.collection.equity.companyName,
                Sector: $scope.collection.equity.sector,
                EquityType: $scope.collection.equity.equityType
            };
            $http.post(AppStrings.EDIS_IP + "api/Admin/DataFeed/InsertEquityBasicDetails", data).success(function () {
                alert("Successs");
            }).error(function (data) {
                alert("failed:" + data);
            })
        }else if ($scope.collection.transactionType === "Bond") {
            data = {
                Ticker: $scope.collection.bond.ticker,
                CompanyName: $scope.collection.bond.companyName,
                Frequency: $scope.collection.bond.frequency,
                BondType: $scope.collection.bond.bondType,
                Issuer: $scope.collection.bond.issuer
            };
            $http.post(AppStrings.EDIS_IP + "api/Admin/DataFeed/InsertBondBasicDetails", data).success(function () {
                alert("Successs");
            }).error(function (data) {
                alert("failed:" + data);
            })
        } else if ($scope.collection.transactionType === "AssetPrice") {
            data = {
                Ticker: $scope.collection.assetPrice.ticker.id,
                Address: $scope.collection.assetPrice.address,
                AssetPrice: $scope.collection.assetPrice.price,
                TransactionDate: dateParser($scope.collection.assetPrice.transactionDate),
                AssetType: $scope.collection.assetPrice.assetType
            };
            $http.post(AppStrings.EDIS_IP + "api/Admin/DataFeed/InsertAssetPriceDetails", data).success(function () {
                alert("Successs");
            }).error(function (data) {
                alert("failed:" + data);
            })
        } else if ($scope.collection.transactionType === "ResearchValue") {
            data = {
                Ticker: $scope.collection.researchValue.ticker.id,
                Address: $scope.collection.researchValue.address,
                Key: $scope.collection.researchValue.key,
                ValueType: $scope.collection.researchValue.key,
                Value: $scope.collection.researchValue.value,
                StringValue: $scope.collection.researchValue.stringValue,
                Issuer: $scope.collection.researchValue.issuer,
                CreateDate: dateParser($scope.collection.researchValue.createDate),
                AssetType: $scope.collection.researchValue.assetType
            };
            $http.post(AppStrings.EDIS_IP + "api/Admin/DataFeed/InsertResearchValue", data).success(function () {
                alert("Successs");
            }).error(function (data) {
                alert("failed:" + data);
            })
        } else if ($scope.collection.transactionType === "BondType") {
            data = {
                Value: $scope.collection.bondType.typeName
            }
            $http.post(AppStrings.EDIS_IP + "api/Admin/DataFeed/InsertBondType", data).success(function () {
                alert("Successs");
            }).error(function (data) {
                alert("failed:" + data);
            })
        } else if ($scope.collection.transactionType === "Sector") {
            data = {
                Value: $scope.collection.sector.sectorName
            }
            $http.post(AppStrings.EDIS_IP + "api/Admin/DataFeed/InsertSector", data).success(function () {
                alert("Successs");
            }).error(function (data) {
                alert("failed:" + data);
            })
        } else if($scope.collection.transactionType === "UploadData"){
            data = {
                id: $scope.collection.uploadData.dataType,
                name: $scope.options.async.saveUrl
            }


            $http.post(AppStrings.EDIS_IP + "api/Admin/DataFeed/UploadDataFile", data).success(function () {
                alert("Successs");
            }).error(function (data) {
                alert("failed:" + data);
            })
        }
    }


});