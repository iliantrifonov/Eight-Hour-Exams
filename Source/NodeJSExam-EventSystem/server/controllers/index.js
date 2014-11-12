//var UsersController = require('./UsersController');
//var ErrorController = require('./ErrorController');
//
//module.exports = {
//    users: UsersController,
//    error: ErrorController
//};

module.exports = {};
var fs = require('fs');
var controllers = fs.readdirSync('./server/controllers');
for (var i = 0; i < controllers.length; i++) {
    if(controllers[i] === 'index.js'){
        continue;
    }

    var current = require('./' + controllers[i]);
    module.exports[current.name] = current.data;
}