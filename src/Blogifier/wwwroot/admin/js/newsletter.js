$('#blog-subscribe').on('click', function () {
    var email = $('#txtEmail').val();
    if (email) {
        subscribe("newsletter/subscribe", email);
    }
    return false;
});

var subscribe = function (url, email) {
    var obj = { Email: email };
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
}

var done = function (data) {   
    $('#frmNewsletter').fadeOut();
    $('#ttlNewsletter').fadeIn();
}