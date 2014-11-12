var auth = require('./auth'),
    controllers = require('../controllers');

module.exports = function(app) {
    app.get('/register', controllers.users.getRegister);
    app.post('/register', controllers.users.postRegister);

    app.get('/profile', auth.isAuthenticated, controllers.users.getProfile);
    app.get('/profile/update', auth.isAuthenticated, controllers.users.getUpdateUser);
    app.post('/profile/update', auth.isAuthenticated, controllers.users.updateUser);

    app.get('/profile/addinitiative', auth.isAuthenticated, controllers.users.getAddInitiative);
    app.post('/profile/addinitiative', auth.isAuthenticated, controllers.users.addInitiative);
    //app.post('/profile/update', auth.isAuthenticated, auth.isInRole('random'), controllers.users.updateUser);

    app.get('/events/create', auth.isAuthenticated, controllers.events.getChoose);
    app.get('/events/details/:id', auth.isAuthenticated, controllers.events.getDetails);
    app.post('/events/details/:id', auth.isAuthenticated, controllers.events.join);

    app.get('/events/create/public', auth.isAuthenticated, controllers.events.getCreatePublic);
    app.post('/events/create/public', auth.isAuthenticated, controllers.events.postCreate);

    app.get('/events/create/initiative', auth.isAuthenticated, controllers.events.getCreateInit);
    app.post('/events/create/initiative', auth.isAuthenticated, controllers.events.postCreate);

    app.get('/events/create/initiativeseason', auth.isAuthenticated, controllers.events.getCreateSeason);
    app.post('/events/create/initiativeseason', auth.isAuthenticated, controllers.events.postCreate);

    app.get('/events/active', auth.isAuthenticated, controllers.events.getActive);
    app.get('/events/past', auth.isAuthenticated, controllers.events.getPast);

    app.post('/comment/:id', auth.isAuthenticated, controllers.events.postComment);

    app.get('/login', controllers.users.getLogin);
    app.post('/login', auth.login);
    app.get('/logout', auth.logout);

    //app.get('/admin/users', auth.isAuthenticated, auth.isInRole('admin'), controllers.admin.getUsers);

    app.get('/', controllers.statistics.all);

    app.get('/error', controllers.error.getErrorPage);

    app.get('*', function(req, res) {
        res.render('index');
    });
};