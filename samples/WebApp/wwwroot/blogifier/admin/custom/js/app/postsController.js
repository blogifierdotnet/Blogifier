var postsController = function (dataService) {
    function publish(id) {
        dataService.put("blogifier/api/posts/publish/" + id, null, publishCallback, fail);
    }

    function unpublish(id) {
        dataService.put("blogifier/api/posts/unpublish/" + id, null, publishCallback, fail);
    }

    function publishCallback() {
        toastr.success('Updated');
        reload();
    }

    function removePost(id) {
        dataService.remove('blogifier/api/posts/' + id, removeCallback, fail);
    }

    function removeCallback() {
        toastr.success('Removed');
        reload();
    }

    function reload() {
        setTimeout(function () {
            window.location.href = webRoot + 'admin';
        }, 1000);
    }

    function fail(jqXHR, exception) {
        var msg = '';
        if (jqXHR.status === 0) { msg = 'Not connect.\n Verify Network.'; }
        else if (jqXHR.status == 404) { msg = 'Requested page not found. [404]'; }
        else if (jqXHR.status == 500) { msg = 'Internal Server Error [500].'; }
        else if (exception === 'parsererror') { msg = 'Requested JSON parse failed.'; }
        else if (exception === 'timeout') { msg = 'Time out error.'; }
        else if (exception === 'abort') { msg = 'Ajax request aborted.'; }
        else { msg = 'Uncaught Error.\n' + jqXHR.responseText; }
        toastr.error(msg);
    }

    return {
        publish: publish,
        unpublish: unpublish,
        removePost: removePost
    }
}(DataService);
