var postsController = function (dataService) {
    var curPost = 0;

    function publish() {
        var items = $('.post-list input:checked');
        for (i = 0; i < items.length; i++) {
            if (i + 1 < items.length) {
                dataService.put("blogifier/api/posts/publish/" + items[i].id, null, emptyCallback, fail);
            }
            else {
                dataService.put("blogifier/api/posts/publish/" + items[i].id, null, updateCallback, fail);
            }
        }
    }

    function unpublish() {
        var items = $('.post-list input:checked');
        for (i = 0; i < items.length; i++) {
            if (i + 1 < items.length) {
                dataService.put("blogifier/api/posts/unpublish/" + items[i].id, null, emptyCallback, fail);
            }
            else {
                dataService.put("blogifier/api/posts/unpublish/" + items[i].id, null, updateCallback, fail);
            }
        }
    }

    function publishCallback() { }

    function removePost() {
        var items = $('.post-list input:checked');
        for (i = 0; i < items.length; i++) {
            if (i + 1 < items.length) {
                dataService.remove('blogifier/api/posts/' + items[i].id, emptyCallback, fail);
            }
            else {
                dataService.remove('blogifier/api/posts/' + items[i].id, updateCallback, fail);
            }
        }
    }

    function emptyCallback() { }
    function updateCallback() {
        toastr.success('Updated');
        reload();
    }

    function showPost(id) {
        if (id) {
            curPost = id;
        }
        dataService.get("blogifier/api/posts/post/" + curPost, showPostCallback, fail);
        return false;
    }
    function showPostCallback(data) {
        var cats = '';
        if (data.categories.length > 0) {
            for (i = 0; i < data.categories.length; i++) {
                cats = cats + data.categories[i].text + ', ';
            }
            cats = cats.substring(0, cats.length - 2);
        }
        $('#post-tagline').html(getDate(data.published) + " / " + cats + " / " + data.postViews + " views");
        $('#post-title').html(data.title);
        $('.bf-content-post-text').html(data.content);
        
    }

    function editPost(id) {
        if (id) {
            curPost = id;
        }
        location.href = getUrl("admin/editor/" + curPost);
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
        removePost: removePost,
        showPost: showPost,
        editPost: editPost
    }
}(DataService);
