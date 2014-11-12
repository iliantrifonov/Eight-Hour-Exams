var a = require('./models/Event');
a.init();
var Event = require('mongoose').model('Event');

module.exports = {
    create: function(user, callback) {
        Event.create(user, callback);
    },
    update: function(conditions, update, options, callback){
        Event.update(conditions, update, options, callback);
    },
    active: function (req, res, callback) {
        var options = queryHandler(req);

        res.locals.options = options;

        var mongooseOptions = getMongooseOptions(options);

        var findObj = {};

        if (options.search){
            findObj.title = new RegExp(options.search, "i");
        }

        if(mongooseOptions.category === "1"){
            findObj.category = new RegExp('Academy initiative', 'i');
        }
        else if (mongooseOptions.category === "2"){
            findObj.category = new RegExp('Free time', 'i');
        }
        else if (mongooseOptions.category === "3"){
            findObj.category = new RegExp('Bar', 'i');
        }
        else if (mongooseOptions.category === "4"){
            findObj.category = new RegExp('Rooftop caffee', 'i');
        }

        var currentDate = new Date();
        Event.find(findObj)
            .where('date')
            .gte(currentDate)
            .order(mongooseOptions)
            .page(mongooseOptions, function (err, data) {
                callback(err, data.results);
            });
    },
    past: function (req, res, callback) {
        var options = queryHandler(req);

        res.locals.options = options;

        var mongooseOptions = getMongooseOptions(options);

        var findObj = {};

        if (options.search){
            findObj.title = new RegExp(options.search, "i");
        }

        if(mongooseOptions.category === "1"){
            findObj.category = new RegExp('Academy initiative', 'i');
        }
        else if (mongooseOptions.category === "2"){
            findObj.category = new RegExp('Free time', 'i');
        }
        else if (mongooseOptions.category === "3"){
            findObj.category = new RegExp('Bar', 'i');
        }
        else if (mongooseOptions.category === "4"){
            findObj.category = new RegExp('Rooftop caffee', 'i');
        }

        var currentDate = new Date();
        Event.find(findObj)
            .where('date')
            .lte(currentDate)
            .order(mongooseOptions)
            .page(mongooseOptions, function (err, data) {
                callback(err, data.results);
            });
    },
    byId: function (id, callback) {
        Event.findOne({_id: id }).populate('joined').exec(callback);
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
    mongooseOptions.category = options.category;

    return mongooseOptions;
}

function queryHandler(req) {
    var page = parseInt(req.query.page || 1),
        perPage = req.query.perPage || 10,
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

    var orderBy = req.query.orderBy || 'date'; //IMPORTANT

    var sortType = req.query.sortType || 'asc';
    var search = req.query.search || '';

    options.orderBy = orderBy;
    options.sortType = sortType;
    options.search = search;
    options.category = req.query.category || '1';

    return options;
}