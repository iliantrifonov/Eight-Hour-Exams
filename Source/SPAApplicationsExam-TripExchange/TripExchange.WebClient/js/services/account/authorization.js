'use strict';

app.factory('authorization', ['identity', function(identity) {
    return {
        getAuthorizationHeader: function () {
            var usr = identity.getCurrentUser();
            if (!usr) {
                return;
            }

            return {
                
                'Authorization': 'Bearer ' + identity.getCurrentUser()['access_token']
            }
        }
    }
}]);