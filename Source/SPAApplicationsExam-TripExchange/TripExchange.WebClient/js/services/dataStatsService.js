app.factory('dataStatsService', [
'$resource', 'baseServiceUrl', '$http', '$q',
function ($resource, baseServiceUrl, $http, $q) {
    "use strict";
    var headers = {};
    headers.useAuth = true;

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


    return {
        getAllStats: getAllStats,
    };
}]);