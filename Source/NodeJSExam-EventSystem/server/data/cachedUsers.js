var User = require('mongoose').model('User');
var mongoose = require('mongoose');
require('mongoose-middleware').initialize(mongoose);

var NodeCache = require( "node-cache" );
var myCache = new NodeCache( { stdTTL: 40, checkperiod: 43 } );

User.find({}).exec(function (err, data) {
    myCache.set('data', data);
});

myCache.on( "expired", function( key, value ){
    if (key === 'data'){
        User.find({}).exec(function (err, data) {
            myCache.set('data', data);
        });
    }
});

module.exports = function (req, res) {

    return  {
        all: function(callback){
            myCache.get('data', function (err, data) {
                callback(err, data.data);
            });
        },
        paged: function (callback) {
            var options = queryHandler(req, res);

            res.locals.options = options;

            var mongooseOptions = getMongooseOptions(options);

            var findObj = {};

            if (options.search){
                findObj.username = new RegExp(options.search, "i");
            }

            User.find(findObj)
                .order(mongooseOptions)
                .page(mongooseOptions, function (err, data) {
                    callback(err, data.results);
                });
        }
    }
};

function getMongooseOptions(options) {
    var mongooseOptions = {};

    if (options.orderBy) {
        mongooseOptions.sort = {};

        if (options.sortType === 'desc') {
            mongooseOptions.sort.desc = options.orderBy;
        } else {
            mongooseOptions.sort.asc = options.orderBy;
        }
    }

    var count = options.perPage;
    var start  = (options.page - 1) * count;

    mongooseOptions.start = start;
    mongooseOptions.count = count;

    return mongooseOptions;
}

function queryHandler(req, res) {
    var page = parseInt(req.query.page || 1),
        perPage = req.query.perPage || 3,
        prevPage,
        nextPage;
    if (page <= 1) {
        prevPage = 1;
        page = 1;
    } else {
        prevPage = page - 1;
    }

    nextPage = page + 1;

    var options = {};
    options.page = page;
    options.prevPage = prevPage;
    options.nextPage = nextPage;
    options.perPage = perPage;

    var orderBy = req.query.orderBy || ''; //IMPORTANT

    var sortType = req.query.sortType || 'asc';
    var search = req.query.search || '';

    options.orderBy = orderBy;
    options.sortType = sortType;
    options.search = search;

    return options;
}