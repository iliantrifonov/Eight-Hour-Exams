var Events = require('../data/events');
var Users = require('../data/users');
var CONTROLLER_NAME = 'events';

function canCreateEvent(user, initiative){
    for (var i = 0; i < user.initiativesSeasons.length; i++) {

        if(user.initiativesSeasons[i].initiative === initiative.initiative){
            if (initiative.season){
                if(user.initiativesSeasons[i].season === initiative.season){
                    return true;
                }
            } else {
                return true;
            }
        }
    }

    return false;
}

module.exports = {
    name: 'events',
    data: {
        join: function (req, res, next) {
            var eventId = req.params.id;

            if (req.user.joinedEvents.indexOf(eventId) > -1){
                req.session.error = 'You are already a part of this event';
                res.redirect('/events/details/' + eventId);
                return;
            }

            Events.byId(eventId, function (err, data) {
                if (err){
                    req.session.error = 'That event does not exist';
                    res.redirect('/');
                    return;
                }

                var joined = data.joined || [];
                joined.push(req.user);

                var conditions = { _id: data._id },
                    update = {
                        joined: joined
                    },
                    options = { multi: false };
                var prevData = data;
                var callback = function (err, data) {
                    if (err) {
                        req.session.error = '';
                        res.redirect('/');
                    }
                    else {
                        var userEvents = req.user.joinedEvents || [];
                        userEvents.push(prevData._id);
                        Users.update(
                            { _id: req.user._id },
                            { joinedEvents: userEvents },
                            { multi: false }, function () {

                            }
                        );

                        res.redirect('/events/details/' + eventId);
                    }
                }

                Events.update(conditions, update, options, callback);
            })
        },
        postComment: function (req, res, next) {
            var eventId = req.params.id;

            if (req.user.joinedEvents.indexOf(eventId) < 0){
                req.session.error = 'Cannot comment if you are not part of the event';
                res.redirect('/events/details/' + eventId);
                return;
            }

            Events.byId(eventId, function (err, data) {
                if (err){
                    req.session.error = 'That event does not exist';
                    res.redirect('/');
                    return;
                }

                var comments = data.comments || [];
                comments.push({
                    content: req.body.content,
                    username: req.user.username
                })

                var conditions = { _id: data._id },
                    update = {
                        comments: comments
                    },
                    options = { multi: false };

                var callback = function (err, data) {
                    if (err) {
                        req.session.error = '';
                        res.redirect('/');
                    }
                    else {
                        res.redirect('/events/details/' + eventId);
                    }
                }

                Events.update(conditions, update, options, callback);
            })
        },
        getDetails: function (req, res, next) {
            var eventId = req.params.id;

            Events.byId(eventId, function (err, data) {
                if (err){
                    req.session.error = 'That event does not exist';
                    res.redirect('/');
                    return;
                }

                res.render(CONTROLLER_NAME + '/details', data);
            })
        },
        getActive: function (req, res, next) {
            Events.active(req, res, function (err, data) {

                var arr = [];
                for (var i = 0; i < data.length; i++){
                    if(data[i].type.initiative) {
                        if (canCreateEvent(req.user, data[i].type)) {
                            arr.push(data[i]);
                        }
                    } else {
                        arr.push(data[i]);
                    }
                }

                res.render(CONTROLLER_NAME + '/list-active', { data: arr });
            })
        },
        getPast: function (req, res, next) {
            Events.past(req, res, function (err, data) {

                var arr = [];
                for (var i = 0; i < data.length; i++){
                    if(data[i].type.initiative) {
                        if (canCreateEvent(req.user, data[i].type)) {
                            arr.push(data[i]);
                        }
                    } else {
                        arr.push(data[i]);
                    }
                }

                res.render(CONTROLLER_NAME + '/list-active', { data: arr });
            })
        },
        getChoose: function (req, res, next) {
            res.render(CONTROLLER_NAME + '/choose');
        },
        getCreatePublic: function (req, res, next) {
            res.render(CONTROLLER_NAME + '/create-public');
        },
        getCreateInit: function (req, res, next) {
            res.render(CONTROLLER_NAME + '/create-init');
        },
        getCreateSeason: function (req, res, next) {
            res.render(CONTROLLER_NAME + '/create-season');
        },
        postCreate: function (req, res, next) {
            var eventData = req.body;
            var creator = {
                username: req.user.username,
                phone: req.user.phoneNumber,
                id: req.user._id
            }

            eventData.creator = creator;

             //= eventData.type || eventData.init + ' ' + eventData.season;
            if(!(eventData.type === 'Public')){
                eventData.type = {
                    initiative: eventData.init,
                    season: eventData.season
                }
            } else {
                eventData.type = undefined;
            }

            eventData.location = {
                lat: eventData.lat,
                lon: eventData.lon
            }

            eventData.joined = [];
            eventData.joined.push(req.user);

            eventData.evaluation = 1;

            if (eventData.type) {
                if(!canCreateEvent(req.user, eventData.type)){
                    req.session.error = 'You cannot create this type of event as you are not part of its initiative or season.';
                    res.redirect('/events/create');
                    return;
                }
            }

            if (!req.user) {
                req.session.error = 'You are not logged in.';
                res.redirect('/login');
            }
            else if(req.user.phoneNumber.length < 1){
                req.session.error = 'You have to add a phone number to make events.';
                res.redirect('/events/create');
            }
            else {

                Events.create(eventData, function (err, event) {
                    if (err) {
                        req.session.error = 'Event not saved, please enter all fields';
                        res.redirect('/events/create');
                        return;
                    }

                    var createdEvents = req.user.createdEvents || [];
                    createdEvents.push(event);
                    var joinedEvents = req.user.joinedEvents || [];
                    joinedEvents.push(event);
                    // join as creator
                    var conditions = { _id: req.user._id },
                        update = {
                            createdEvents: createdEvents,
                            joinedEvents: joinedEvents
                        },
                        options = { multi: false };

                    var callback = function (err, data) {
                        if (err) {
                            req.session.error = '';
                            res.redirect('/');
                        }
                        else {
                            res.redirect('/');
                        }
                    }

                    Users.update(conditions, update, options, callback);
                });
            }
        }
    }
};