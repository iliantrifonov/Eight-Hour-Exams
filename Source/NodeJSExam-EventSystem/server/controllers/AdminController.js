var cached = require('./../data/cachedUsers');

module.exports = {
    name: 'admin',
    data: {
        getUsers: function (req, res, err) {

            cached(req, res).paged(function (err, data) {
                if (err) {
                    console.log('error');
                    console.dir(err);
                }

                res.render('admin/users', { data: data });
            })
        }
    }
}
