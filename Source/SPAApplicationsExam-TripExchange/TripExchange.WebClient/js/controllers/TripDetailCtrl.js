app.controller('TripDetailCtrl',
    function mainItemsPageCtrl($scope, dataService, $routeParams) {
        'use strict';

        dataService.getTripDetails($routeParams.id).then(function (data) {
            $scope.trip = data;
        });

        $scope.join = function () {
            dataService.joinTrip($routeParams.id).then(function (data) {
                $scope.trip = data;
            });
        }
    });