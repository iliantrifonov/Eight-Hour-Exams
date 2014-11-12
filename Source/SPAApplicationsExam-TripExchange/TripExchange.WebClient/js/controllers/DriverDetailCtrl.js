app.controller('DriverDetailCtrl',
    function mainItemsPageCtrl($scope, dataService, $routeParams) {
    'use strict';

    dataService.getDriverDetail($routeParams.id).then(function (data) {
        $scope.driver = data;
    });
});