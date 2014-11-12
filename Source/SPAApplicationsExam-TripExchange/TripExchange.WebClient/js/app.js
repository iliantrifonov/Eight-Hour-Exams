var app = angular.module('myApp', ['ngRoute', 'ngResource', 'ngCookies', 'angularUtils.directives.dirPagination']).
    config(['$routeProvider', function ($routeProvider) {
        'use strict';

        $routeProvider
            .when('/', {
                templateUrl: 'views/partials/startPage.html',
                controller: 'StartPageCtrl'
            })
            .when('/register', {
                templateUrl: 'views/partials/register.html',
                controller: 'SignUpCtrl'
            })
            .when('/drivers/:id', {
                templateUrl: 'views/partials/driverDetail.html',
                controller: 'DriverDetailCtrl'
            })
            .when('/drivers', {
                templateUrl: 'views/partials/drivers.html',
                controller: 'DriversCtrl'
            })
            .when('/trips/:id', {
                templateUrl: 'views/partials/tripDetail.html',
                controller: 'TripDetailCtrl'
            })
            .when('/trips', {
                templateUrl: 'views/partials/trips.html',
                controller: 'TripsCtrl'
            })
            .when('/create', {
                templateUrl: 'views/partials/createTrip.html',
                controller: 'CreateTripCtrl'
            })
            .when('/unauthorized', {
                templateUrl: 'views/partials/unauthorized.html',
            })
            .otherwise({ redirectTo: '/' });
    }])
    .config(["$httpProvider",
    function ($httpProvider) {
        //$httpProvider.defaults.withCredentials = true;
        //$httpProvider.defaults.headers.common["X-Requested-With"] = "XMLHttpRequest";
        $httpProvider.interceptors.push('authInterceptorService');
}
    ])
    .value('toastr', toastr)
    .constant('baseServiceUrl', 'http://spa2014.bgcoder.com');