/// <reference path="C:\Users\Nikki\Desktop\Telerik\Ilian\SPAApplications\Angular\Angular\lib/angular/angular.min.js" />
app.filter('searchInNameOrId', function () {
    return function (collection, filterForCollection) {
        var result = [];
        if (!filterForCollection) {
            return collection;
        }
        for (var i = 0; i < collection.length; i++) {
            var currentItem = collection[i];

            if (currentItem.name.indexOf(filterForCollection) !== -1 || currentItem.id.toString().indexOf(filterForCollection) !== -1) {
                result.push(currentItem);
            }
        }

        return result;
    };
})