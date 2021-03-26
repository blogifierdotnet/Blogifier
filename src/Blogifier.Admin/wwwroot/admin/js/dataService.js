var DataService = function () {
    var upload = function (url, obj, done, fail) {
        $.ajax({
            type: "POST",
            url: url,
            enctype: 'multipart/form-data',
            contentType: false,
            processData: false,
            cache: false,
            data: obj,
            success: done,
            error: fail
        });
    };
    return {
        upload: upload
    };
}();