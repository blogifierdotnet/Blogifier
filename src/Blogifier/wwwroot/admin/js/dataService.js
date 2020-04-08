var DataService = function () {
    var get = function (url, done, fail, obj) {
        var data = obj ? JSON.stringify(obj) : {};
        var options = {
            url: getUrl(url),
            type: "GET",
            data: data,
            dataType: "json",
            success: done,
            error: fail
        };
        $.ajax(options);
    };
    var post = function (url, obj, done, fail) {
        var options = {
            url: getUrl(url),
            type: "POST",
            data: JSON.stringify(obj),
            contentType: "application/json",
            dataType: "html",
            success: done,
            error: fail
        };
        $.ajax(options);
    };
    var put = function (url, obj, done, fail) {
        var data = obj ? JSON.stringify(obj) : {};
        var options = {
            url: getUrl(url),
            type: "PUT",
            data: data,
            contentType: "application/json",
            dataType: "html",
            success: done,
            error: fail
        };
        $.ajax(options);
    };
    var remove = function (url, done, fail) {
        var options = {
            url: getUrl(url),
            type: "DELETE",
            dataType: "html",
            success: done,
            error: fail
        };
        $.ajax(options);
    };
    var upload = function (url, obj, done, fail) {
        $.ajax({
            type: "POST",
            url: getUrl(url),
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
        get: get,
        post: post,
        remove: remove,
        put: put,
        upload: upload
    };
}();