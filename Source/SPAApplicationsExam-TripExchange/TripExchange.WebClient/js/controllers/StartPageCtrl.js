app.controller('StartPageCtrl', function mainItemsPageCtrl($scope, dataService, dataStatsService) {
    'use strict';
    $scope.noDataTrip = false;
    $scope.noDataDriver = false;
    dataStatsService.getAllStats().then(function (data) {
        $scope.stats = data;
    });

    dataService.getTopTripsPublic().then(function (data) {
        $scope.trips = data;
        if (data.length < 1) {
            $scope.noDataTrip = true;
        } else {
            $scope.noDataTrip = false;
        }
    });

    dataService.getTopDriversPublic().then(function (data) {
        $scope.users = data;
        if (data.length < 1) {
            $scope.noDataDriver = true;
        } else {
            $scope.noDataDriver = false;
        }
    });
});