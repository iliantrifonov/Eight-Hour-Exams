var cached = require('./../data/cachedEvents');

module.exports = {
    name: 'statistics',
    data: {
        all: function (req, res, err) {

            cached.all(function (err, data) {
                if (err) {
                    console.log('error');
                    console.dir(err);
                }

                res.render('statistics/all', { data: data });
            })
        }
    }
}