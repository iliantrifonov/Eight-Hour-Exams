app.factory('dataUserService', [
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

    return {
        getUserInfo: getUserInfo,
    };
}]);