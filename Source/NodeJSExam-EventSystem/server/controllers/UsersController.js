var encryption = require('../utilities/encryption');
var Users = require('../data/users');

var CONTROLLER_NAME = 'users';
var ALLOWED_CHARS = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_ .';
function validateUsername(name) {
    if(name.length > 20 || name.length < 6) {
        return false;
    }

    for (var i = 0; i < name.length; i++) {
        if (ALLOWED_CHARS.indexOf(name[i]) === -1) {
            return false;
        }
    }

    return true;
}

module.exports = {
    name: 'users',
    data: {
        getRegister: function (req, res, next) {
            res.render(CONTROLLER_NAME + '/register')
        },
        postRegister: function (req, res, next) {
            var newUserData = req.body;

            if (!validateUsername(newUserData.username)) {
                req.session.error = 'The username should be between 6 and 20 characters long and can contain Latin letters, digits and the symbols (underscore), (space) and (dot)';
                res.redirect('/register');
            }
            else if (newUserData.password.length < 1) {
                req.session.error = 'Password must be at least 1 symbols';
                res.redirect('/register');
            }
            else if (newUserData.password != newUserData.confirmPassword) {
                req.session.error = 'Passwords do not match!';
                res.redirect('/register');
            }
            else {
                newUserData.salt = encryption.generateSalt();
                newUserData.hashPass = encryption.generateHashedPassword(newUserData.salt, newUserData.password);
                newUserData.initiativesSeasons = [];
                var initiative = {
                    initiative: newUserData.init,
                    season: newUserData.season
                }

                newUserData.initiativesSeasons.push(initiative);
                newUserData.profileImage = newUserData.profileImage || 'http://desktopbackground.in/wp-content/uploads/2014/09/Ninja-Cool-140815.jpg';

                Users.create(newUserData, function (err, user) {
                    if (err) {
                        req.session.error = 'Username is already taken, or has invalid characters.';
                        res.redirect('/register');
                        return;
                    }

                    req.logIn(user, function (err) {
                        if (err) {
                            req.session.error = 'Incorrect user/password combination!';
                            res.redirect('/register');
                        }
                        else {
                            res.redirect('/');
                        }
                    })
                });
            }
        },
        getLogin: function (req, res, next) {
            res.render(CONTROLLER_NAME + '/login');
        },
        postLogin: function (req, res, next) {
            if (req.user) {
                res.redirect('/');
            }
            else {
                res.redirect('/login');
            }
        },
        logout: function (req, res, next) {
            res.redirect('/');
        },
        getProfile: function (req, res, next) {
            res.render(CONTROLLER_NAME + '/profile')
        },
        getUpdateUser: function (req, res, next) {
            res.render(CONTROLLER_NAME + '/update')
        },
        updateUser: function (req, res, next) {

            if (req.body.username.length < 1) {
                req.session.error = 'Username must be at least 1 symbol long.';
                res.redirect('/profile/update');
                return;
            }

            if (!validateUsername(req.body.username)) {
                req.session.error = 'The username should be between 6 and 20 characters long and can contain Latin letters, digits and the symbols (underscore), (space) and (dot)';
                res.redirect('/profile/update');
                return;
            }

            if (!req.body.oldPassword) {
                req.session.error = 'Enter your old password.';
                res.redirect('/profile/update');
                return;
            }

            if (req.body.password.length < 1) {
                req.session.error = 'Please enter a valid new password.';
                res.redirect('/profile/update');
                return;
            }

            var hashedPass = encryption.generateHashedPassword(req.user.salt, req.body.oldPassword);

            if (hashedPass !== req.user.hashPass) {
                req.session.error = 'Invalid password.';
                res.redirect('/profile/update');
                return;
            }

            if (req.body.password !== req.body.confirmPassword) {
                req.session.error = 'Passwords do not match.';
                res.redirect('/profile/update');
                return;
            }

            // needed for update
            var conditions = { _id: req.user._id },
                update = {
                    username: req.body.username || '',
                    phoneNumber: req.body.phoneNumber || '',
                    profileImage: req.body.profileImage || '',
                    facebook: req.body.facebook || '',
                    twitter: req.body.twitter || '',
                    linkedIn: req.body.linkedIn || '',
                    google: req.body.google || '',
                    hashPass: encryption.generateHashedPassword(req.user.salt, req.body.password)
                },
                options = { multi: false };

            var callback = function (err, data) {
                if (err) {
                    req.session.error = 'That user name is already taken, or there are invalid characters in your user name or password.';
                    res.redirect('/profile/update');
                }
                else {
                    res.redirect('/profile/update');
                }
            }

            Users.update(conditions, update, options, callback);
        },
        getAddInitiative: function (req, res, next) {
            res.render(CONTROLLER_NAME + '/add-initiative')
        },
        addInitiative: function (req, res, next) {
            // needed for update
            var initiative = {
                initiative: req.body.init,
                season: req.body.season
            }

            for(var i = 0; i < req.user.initiativesSeasons.length; i++){
                if((req.user.initiativesSeasons[i].initiative === initiative.initiative) && (req.user.initiativesSeasons[i].season === initiative.season)){
                    req.session.error = 'You are already a part of that initiative in that season.';
                    res.redirect('/profile/addinitiative');
                    return;
                }
            }

            req.user.initiativesSeasons.push(initiative);
            var conditions = { _id: req.user._id },
                update = {
                    initiativesSeasons: req.user.initiativesSeasons
                },
                options = { multi: false };

            var callback = function (err, data) {
                if (err) {
                    req.session.error = 'You are already a part of that initiative in that season.';
                    res.redirect('/profile/addinitiative');
                }
                else {
                    res.redirect('/profile');
                }
            }

            Users.update(conditions, update, options, callback);
        }
    }
};