var mongoose = require('mongoose');

module.exports.init = function() {
    var eventSchema = mongoose.Schema({
        title: { type: String, require: '{PATH} is required' },
        description: { type: String, require: '{PATH} is required' },
        location: {
            lat: Number,
            lon: Number
        },
        category: { type: String, require: '{PATH} is required' },
        date: { type: Date, require: '{PATH} is required' },
        type: {
            initiative: String,
            season: String
        },
        creator: {
            username: { type: String, require: '{PATH} is required' },
            phone: { type: String, require: '{PATH} is required' },
            id: { type: String, require: '{PATH} is required' }
        },
        joined: [{
            type: mongoose.Schema.ObjectId,
            ref: 'User'
        }],
        comments: [{
            content: { type: String, require: '{PATH} is required' },
            username: { type: String, require: '{PATH} is required' },
            published: {
                type: Date,
                default: Date.now
            }
        }],
        evaluation: Number
    });

    var Event = mongoose.model('Event', eventSchema);
    seed(Event);
};

function seed(Event) {

    Event.find({}).exec(function (err, collection) {
        if (err) {
            console.log('Cannot find events: ' + err);
            return;
        }

        if (collection.length === 0) {
            //TODO: SEED some events
        }
    });
};

