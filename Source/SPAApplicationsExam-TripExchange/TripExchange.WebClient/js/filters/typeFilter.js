app.filter('typeFilter', function () {
    return function (item) {
        switch (item){
            case true: return "Yes it is :)";
            case false: return "No its not :(";
        }
    }
})