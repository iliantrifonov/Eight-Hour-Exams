app.controller('CreateTripCtrl',
    function additionalInfoCtrl($scope, $location, dataService, notifier) {


        $scope.redirect = function () {
            $location.path('/');
        };

        $scope.create = function () {
            dataService.createTrip($scope.trip).then(function (data) {
                $scope.driver = data;

                notifier.success('Trip created!');
                $location.path('/');
            });
        };

        dataService.getAllCities().then(function (data) {
            $scope.towns = data;
        });
    });