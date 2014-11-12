app.controller('TripsCtrl',
    function mainItemsPageCtrl($scope, $location, dataService, $routeParams, identity) {
        'use strict';

        $scope.pageNumber = 1;
        $scope.identity = identity;
        $scope.orderBy = 'date'
        $scope.orderType = 'asc';
        $scope.noData = false;

        dataService.getTopTripsPublic().then(function (data) {
            $scope.tripsPublic = data;
            $scope.trips = data;
            if (data.length < 1) {
                $scope.noData = true;
            } else {
                $scope.noData = false;
            }
        });

        $scope.redirectCreate = function () {
            $location.path('/create');
        };

        dataService.getAllCities().then(function (data) {
            $scope.towns = data;
        });

        $scope.prev = function () {
            if($scope.pageNumber > 1){
                $scope.pageNumber--;
                $scope.getPrivateTripsAllParams();
            }
        };

        $scope.next = function () {
            $scope.pageNumber++;
            $scope.getPrivateTripsAllParams();
        };

        $scope.getPrivateTripsAllParams = function getPrivateTripsAllParams() {
            if (!$scope.orderBy) {
                $scope.orderBy = 'date';
            }

            if (!$scope.orderType) {
                $scope.orderType = 'asc';
            }

            if (!$scope.finished) {
                $scope.finished = false;
            }

            if (!$scope.onlyMine) {
                $scope.onlyMine = false;
            }
            
            dataService.getTrips($scope.pageNumber,
                                $scope.orderBy,
                                $scope.orderType,
                                $scope.from,
                                $scope.to,
                                $scope.finished,
                                $scope.onlyMine
                                ).then(function (data) {
                                    $scope.trips = data;
                                    if (data.length < 1) {
                                        $scope.noData = true;
                                    } else {
                                        $scope.noData = false;
                                    }
                                });
        };
    });