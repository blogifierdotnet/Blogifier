var postsController = function (dataService) {
    var curPost = 0;
    var curSlug = '';

    function publish() {
        /*
        var items = $('.post-list input:checked');
        for (i = 0; i < items.length; i++) {
            if (i + 1 < items.length) {
                dataService.put("blogifier/api/posts/publish/" + items[i].id, null, emptyCallback, fail);
            }
            else {
                dataService.put("blogifier/api/posts/publish/" + items[i].id, null, updateCallback, fail);
            }
        }
        */
        dataService.put("blogifier/api/posts/publish/" + curPost, null, updateCallback, fail);
    }

    function unpublish() {
        //var items = $('.post-list input:checked');
        //for (i = 0; i < items.length; i++) {
        //    if (i + 1 < items.length) {
        //        dataService.put("blogifier/api/posts/unpublish/" + items[i].id, null, emptyCallback, fail);
        //    }
        //    else {
        //        dataService.put("blogifier/api/posts/unpublish/" + items[i].id, null, updateCallback, fail);
        //    }
        //}
        dataService.put("blogifier/api/posts/unpublish/" + curPost, null, updateCallback, fail);
    }

    function publishCallback() { }

    function removePost() {
        /*
        var items = $('.post-list input:checked');
        for (i = 0; i < items.length; i++) {
            if (i + 1 < items.length) {
                dataService.remove('blogifier/api/posts/' + items[i].id, emptyCallback, fail);
            }
            else {
                dataService.remove('blogifier/api/posts/' + items[i].id, updateCallback, fail);
            }
        }
        */
        dataService.remove("blogifier/api/posts/" + curPost, updateCallback, fail);
    }

    function emptyCallback() { }
    function updateCallback() {
        toastr.success('Updated');
        reload();
    }

    function showPost(id) {
        if (id) {
            curPost = id;
            dataService.get("blogifier/api/posts/post/" + curPost, showPostCallback, fail);
        }
        else {
            window.open('blog/' + curSlug);
        }

        return false;
    }
    function showPostCallback(data) {
        curSlug = data.slug;
        var cats = '';
        if (data.categories.length > 0) {
            for (i = 0; i < data.categories.length; i++) {
                cats = cats + data.categories[i].text + ', ';
            }
            cats = cats.substring(0, cats.length - 2);
        }
        $('.item-preview-meta').html(getDate(data.published) + " / " + cats + " / " + data.postViews + " views");
        $('.item-preview-title').html(data.title);
        $('.item-preview-body').html(data.content);
        if (data.isPublished) {
            $('#btnUnpublish').show();
            $('#btnPublish').hide();
        }
        else {
            $('#btnUnpublish').hide();
            $('#btnPublish').show();
        }
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

$('.bf-posts-sidebar-list .item-list-link-desktop').click(function () {
    $('.bf-posts-sidebar-list .item-list-link-desktop').removeClass('active');
    $(this).addClass('active');
});

var itemCheck = $('.item-sidebar-checkbox');
var firstItemCheck = itemCheck.first();
var btnAction = '#postsMultiactions';
var sidebarTools = '#sidebarTools';

// check all
$(firstItemCheck).on('change', function () {
    $(itemCheck).prop('checked', this.checked);
});

// uncheck "check all" when one item is unchecked
$(itemCheck).not(firstItemCheck).on('change', function () {
    if ($(this).not(':checked')) {
        $(firstItemCheck).prop('checked', false);
    }
});

// show multi action buttons when any item checked
$(document).on('change', itemCheck, function () {
    if ($(itemCheck).is(':checked')) {
        $(btnAction).stop(true, true).slideDown();
        // hide filters
        $(sidebarTools).collapse('hide');
    } else {
        $(btnAction).stop(true, true).slideUp();
    }
});

// uncheck all when filters is active
$(sidebarTools).on('show.bs.collapse', function () {
    $(itemCheck).prop('checked', false);
    $(btnAction).stop(true, true).slideUp();
})