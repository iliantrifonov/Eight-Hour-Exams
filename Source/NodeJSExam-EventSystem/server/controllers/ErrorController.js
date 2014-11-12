module.exports = {
    name: 'error',
    data: {
        getErrorPage: function (req, res, err) {
            res.render('shared/error');
        }
    }
}
