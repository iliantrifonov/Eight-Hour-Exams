var express = require('express'),
    bodyParser = require('body-parser'),
    cookieParser = require('cookie-parser'),
    session = require('express-session'),
    busboy = require('connect-busboy'),
    passport = require('passport'),
    favicon = require('serve-favicon');

module.exports = function(app, config) {
    app.set('view engine', 'jade');
    app.set('views', config.rootPath + '/server/views');
    app.use(cookieParser());
    app.use(bodyParser.json());
    app.use(bodyParser.urlencoded({extended: true}));
    app.use(busboy({immediate: false}));
    app.use(session({secret: 'magic unicorns', resave: true, saveUninitialized: true}));
    app.use(passport.initialize());
    app.use(passport.session());
    app.use(favicon(__dirname + '/../../public/img/favicon.ico'));
    app.use(express.static(config.rootPath + '/public'));
    app.use(express.static(config.rootPath + '/../public'));
    app.use(function (req, res, next){
        if (req.user) {
            app.locals.currentUser = req.user;
            res.locals.currentUser = req.user;
        }
        else {
            app.locals.currentUser = undefined;
            res.locals.currentUser = undefined;
        }

        next();
    });
    app.use(function(req, res, next) {
        if (req.session.error) {
            var msg = req.session.error;
            req.session.error = undefined;
            app.locals.errorMessage = msg;
        }
        else {
            app.locals.errorMessage = undefined;
        }

        next();
    });
};