app.filter('tripFilter', function ($routeParams) {
    return function (collection, isMine, isDriver) {
        var result = [];

        if (!isMine && !isDriver) {
            return collection;
        }

        for (var i = 0; i < collection.length; i++) {
            var currentItem = collection[i];
            var inside = false;
            if (isMine) {
                if (currentItem.isMine === true) {
                    result.push(currentItem);
                    inside = true;
                }
            }

            if (inside) {
                continue;
            }

            if (isDriver) {
                if (currentItem.driverId === $routeParams.id) {
                    result.push(currentItem);
                }
            }
        }

        return result;
    };
})