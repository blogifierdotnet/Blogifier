$('#blog-subscribe').on('click', function () {
    var email = $('#txtEmail').val();
    if (email) {
        subscribe("newsletter/subscribe", email);
    }
    return false;
});

var subscribe = function (url, email) {

    $.getJSON('http://gd.geobytes.com/GetCityDetails?callback=?', function (data) {
        var ip = data.geobytesremoteip || 'n/a';
        var country = data.geobytescountry || 'n/a';
        var region = data.geobytesregion || 'n/a';

        var all = ip + '|' + country + '|' + region;

        var obj = { Email: email, Ip: all };
        var options = {
            url: "/" + url,
            type: "POST",
            data: JSON.stringify(obj),
            contentType: "application/json",
            dataType: "html",
            success: done,
            error: done
        };
        $.ajax(options);
    }); 
}

var done = function (data) {
    $('#frmNewsletter').slideUp();
    $('#ttlNewsletter').slideDown();
}