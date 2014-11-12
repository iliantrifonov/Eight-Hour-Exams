app.factory('dataService', [
'$resource', 'baseServiceUrl', '$http', '$q',
function ($resource, baseServiceUrl, $http, $q) {
    "use strict";
    var headers = {};
    headers.useAuth = true;

    var getUserInfo = function () {
        var deferred = $q.defer();

        $http({ url: baseServiceUrl + '/api/users/userInfo', headers: headers, method: 'GET', })
            .success(function (data) {
                deferred.resolve(data);
            }, function (response) {
                deferred.reject(response);
            });

        return deferred.promise;
    };

    var getAllStats = function () {
        var deferred = $q.defer();

        $http({ url: baseServiceUrl + '/api/stats', method: 'GET', })
            .success(function (data) {
                deferred.resolve(data);
            }, function (response) {
                deferred.reject(response);
            });

        return deferred.promise;
    };

    var getAllCities = function () {
        var deferred = $q.defer();

        $http({ url: baseServiceUrl + '/api/cities', method: 'GET', })
            .success(function (data) {
                deferred.resolve(data);
            }, function (response) {
                deferred.reject(response);
            });

        return deferred.promise;
    };
    
    var createTrip = function (toSend) {
        var deferred = $q.defer();

        $http({ url: baseServiceUrl + '/api/trips', headers: headers, method: 'POST', data: toSend })
            .success(function (data) {
                deferred.resolve(data);
            }, function (response) {
                deferred.reject(response);
            });

        return deferred.promise;
    };

    var getTripDetails = function (id) { 
        var deferred = $q.defer();

        $http({ url: baseServiceUrl + '/api/trips/' + id, headers: headers, method: 'GET', })
            .success(function (data) {
                deferred.resolve(data);
            }, function (response) {
                deferred.reject(response);
            });

        return deferred.promise;
    };

    var joinTrip = function (id) {
        var deferred = $q.defer();

        $http({ url: baseServiceUrl + '/api/trips/' + id, headers: headers, method: 'PUT', })
            .success(function (data) {
                deferred.resolve(data);
            }, function (response) {
                deferred.reject(response);
            });

        return deferred.promise;
    };

    var getTrips = function (page, orderBy, orderType, from, to, finished,onlyMine) {
        var deferred = $q.defer();

        var createdUrl = baseServiceUrl + '/api/trips?page=' + page
                + '&orderBy=' + orderBy
                + '&orderType=' + orderType
                + '&finished=' + finished
                + '&onlyMine=' + onlyMine;

        if (from) {
            createdUrl += '&from=' + from;
        }

        if (to) {
            createdUrl += '&to=' + to;
        }

        $http({
            url: createdUrl, headers: headers, method: 'GET',
        })
            .success(function (data) {
                deferred.resolve(data);
            }, function (response) {
                deferred.reject(response);
            });

        return deferred.promise;
    };

    var getDrivers = function (page, username) { // check on server for public !
        var deferred = $q.defer();
        if (!page) {
            page = 1;
        }


        var actualUrl = baseServiceUrl + '/api/drivers?page=' + page;
        if (username) {
            actualUrl += '&username=' + username;
        }
        $http({ url: actualUrl, headers: headers, method: 'GET', })
            .success(function (data) {
                deferred.resolve(data);
            }, function (response) {
                deferred.reject(response);
            });

        return deferred.promise;
    };

    var getDriverDetail = function (id) {
        var deferred = $q.defer();

        $http({ url: baseServiceUrl + '/api/drivers/' + id, headers: headers, method: 'GET', })
            .success(function (data) {
                deferred.resolve(data);
            }, function (response) {
                deferred.reject(response);
            });

        return deferred.promise;
    };

    var getTopTripsPublic = function () {
        var deferred = $q.defer();

        $http({ url: baseServiceUrl + '/api/trips', method: 'GET', })
            .success(function (data) {
                deferred.resolve(data);
            }, function (response) {
                deferred.reject(response);
            });

        return deferred.promise;
    };

    var getTopDriversPublic = function () { // check on server for public !
        var deferred = $q.defer();

        $http({ url: baseServiceUrl + '/api/drivers', method: 'GET', })
            .success(function (data) {
                deferred.resolve(data);
            }, function (response) {
                deferred.reject(response);
            });

        return deferred.promise;
    };

    return {
        getUserInfo: getUserInfo,
        getAllStats: getAllStats,
        getAllCities: getAllCities,
        createTrip: createTrip,
        getTripDetails: getTripDetails,
        joinTrip: joinTrip,
        getTrips: getTrips,
        getDrivers: getDrivers,
        getDriverDetail: getDriverDetail,
        getTopTripsPublic: getTopTripsPublic,
        getTopDriversPublic: getTopDriversPublic,
    };
}]);