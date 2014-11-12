app.filter('searchInNameOrInformation', function () {
    return function (collection, filterForCollection) {
        var result = [];

        if (!filterForCollection) {
            return collection;
        }

        filterForCollection = filterForCollection.toLowerCase();

        for (var i = 0; i < collection.length; i++) {
            var currentItem = collection[i];

            if (currentItem.name.toLowerCase().indexOf(filterForCollection) !== -1 || currentItem.details.Information.toLowerCase().indexOf(filterForCollection) !== -1) {
                result.push(currentItem);
            }
        }

        return result;
    };
})