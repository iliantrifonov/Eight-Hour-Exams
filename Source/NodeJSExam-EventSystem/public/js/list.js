$(document).ready(function() {

    var urlVariables = getUrlVars();
    $('#order-by').val(urlVariables['orderBy'] || 'date');
    $('#sort').val(urlVariables['sortType'] || 'asc');
    $('#search').val(urlVariables['search']);
    $('#category').val(urlVariables['category'] || '1');

    $('#order-by').on('change', changeButtons);
    $('#sort').on('change', changeButtons);
    $('#search').on('change', changeButtons);
    $('#category').on('change', changeButtons);

    function changeButtons() {
        var queryStr = getCurrentQueryString();

        $('#next').attr("href", queryStr);
        $('#prev').attr("href", queryStr);
        $('#filter').attr("href", queryStr);
    }

    function getUrlVars() {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for(var i = 0; i < hashes.length; i++)
        {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    }

    function getCurrentQueryString() {
        var page = parseInt($('#page').html()) || 2,
            orderBy = $('#order-by').val(),
            sortType = $('#sort').val(),
            search = $('#search').val() || '',
            category = $('#category').val() || '';

        var queryString = "/events/active?page=" + page + "&orderBy=" + orderBy + "&sortType=" + sortType + "&search=" + search + "&category=" + category;
        return queryString;
    }
});