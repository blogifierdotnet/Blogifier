var postsController = function (dataService) {
    function publish() {
        $('.post-list input:checked').each(function () {
            dataService.put("blogifier/api/posts/publish/" + $(this).attr('id'), null, publishCallback, fail);
        });
        toastr.success('Updated');
        reload();
    }

    function unpublish() {
        $('.post-list input:checked').each(function () {
            dataService.put("blogifier/api/posts/unpublish/" + $(this).attr('id'), null, publishCallback, fail);
        });
        toastr.success('Updated');
        reload();
    }

    function publishCallback() { }

    function removePost() {
        $('.post-list input:checked').each(function () {
            dataService.remove('blogifier/api/posts/' + $(this).attr('id'), removeCallback, fail);
        });
        toastr.success('Updated');
        reload();
    }

    function removeCallback() { }

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
