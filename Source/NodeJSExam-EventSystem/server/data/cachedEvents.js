var Event = require('mongoose').model('Event');
var mongoose = require('mongoose');
require('mongoose-middleware').initialize(mongoose);

var NodeCache = require( "node-cache" );
var myCache = new NodeCache( { stdTTL: 600, checkperiod: 603 } );

Event.find({}).exec(function (err, data) {
    myCache.set('data', data);
});

myCache.on( "expired", function( key, value ){
    if (key === 'data'){
        var currentDate = new Date();
        Event.find({})
            .order({
                sort: {
                    desc: 'date'
                }
            })
            .where('date')
            .lte(currentDate)
            .exec(function (err, data) {
            myCache.set('data', data);
        });
    }
});

module.exports = {
    all: function (callback) {
        myCache.get('data', function (err, data) {
            callback(err, data.data);
        });
    }
}
