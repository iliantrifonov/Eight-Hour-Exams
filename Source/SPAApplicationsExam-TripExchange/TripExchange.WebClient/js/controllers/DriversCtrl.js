app.controller('DriversCtrl',
    function mainItemsPageCtrl($scope, $location, dataService, $routeParams, identity) {
        'use strict';
        $scope.noData = false;
        $scope.pageNumber = 1;
        $scope.identity = identity;

        dataService.getTopDriversPublic().then(function (data) {
            $scope.driversPublic = data;
            $scope.drivers = data;
            if (data.length < 1) {
                $scope.noData = true;
            } else {
                $scope.noData = false;
            }
        });

        $scope.prev = function () {
            if ($scope.pageNumber > 1) {
                $scope.pageNumber--;
                $scope.getPrivateDriversAllParams();
            }
        };

        $scope.next = function () {
            $scope.pageNumber++;
            $scope.getPrivateDriversAllParams();
        };

        $scope.getPrivateDriversAllParams = function getPrivateDriversAllParams() {
            dataService.getDrivers($scope.pageNumber, $scope.username).then(function (data) {
                $scope.drivers = data;
                if (data.length < 1) {
                    $scope.noData = true;
                } else {
                    $scope.noData = false;
                }
            });
        };
    }
);