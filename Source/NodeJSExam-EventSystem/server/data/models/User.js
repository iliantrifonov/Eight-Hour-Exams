var mongoose = require('mongoose'),
    encryption = require('../../utilities/encryption');

module.exports.init = function() {
    var userSchema = mongoose.Schema({
        username: { type: String, require: '{PATH} is required', unique: true },
        salt: { type: String, require: '{PATH} is required' },
        hashPass: { type: String, require: '{PATH} is required' },
        roles: [String],
        eventPoints: Number,
        firstName: { type: String, require: '{PATH} is required' },
        lastName: { type: String, require: '{PATH} is required' },
        phoneNumber: String,
        email: { type: String, require: '{PATH} is required' },
        initiativesSeasons: [{
            initiative: String,
            season: String
        }],
        profileImage: { type: String, require: '{PATH} is required' },
        facebook: String,
        twitter: String,
        linkedIn: String,
        google: String,
        createdEvents: [{
            type: mongoose.Schema.ObjectId,
            ref: 'Event'
        }],
        joinedEvents: [{
            type: mongoose.Schema.ObjectId,
            ref: 'Event'
        }]
    });

    userSchema.method({
        authenticate: function(password) {
            if (encryption.generateHashedPassword(this.salt, password) === this.hashPass) {
                return true;
            }
            else {
                return false;
            }
        }
    });

    var User = mongoose.model('User', userSchema);
    seed(User);
};

function seed(User) {
    User.findOne({username: '1@1.1'}).exec(function (err, data) {
        if (err || !data) {
            var salt;
            var hashedPwd;
            salt = encryption.generateSalt();
            hashedPwd = encryption.generateHashedPassword(salt, '111111');
            User.create({
                username: '1@1.1',
                salt: salt,
                hashPass: hashedPwd,
                roles: ['admin'],
                eventPoints: 10,
                firstName: 'name1',
                lastName: 'name2',
                phoneNumber: '22222',
                email: 'string@string',
                profileImage: 'Ninjaaaa'
            });
        }
    });
};

