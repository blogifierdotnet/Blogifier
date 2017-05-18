var postController = function (dataService) {
    var current = {
        page: 1,
        post: 0
    }

    function loadPage(page) {
        current.page = page;
        current.post = 0;
        reload();
        return false;
    }

    function reload() {
        dataService.get('blogifier/api/posts/' + current.page, loadList, fail);
    }

    function loadList(data) {
        if (data.blogPosts.length < 1) {
            return false;
        }
        $('#admin-posts').empty();
        var posts = data.blogPosts;
        $.each(posts, function (index) {
            var post = posts[index];
            var anc = '<a class="post-list-title" id="post-title-' + post.blogPostId + '" href="" onclick="postController.loadPostEdit(' + post.blogPostId + '); return false;">' +
                '<h4>' + post.title + '<span class="pull-right clock">' + getPubDate(post.published) + '</span></h4><div class="post-excerpt">' + post.content + '</div></a>';
            var li = '<li id="post-' + post.blogPostId + '" class="list-group-item">' + anc + '</li>';
            $("#admin-posts").append(li);
        });
        pager(data.pager);
        postId = current.post > 0 ? current.post : posts[0].blogPostId;
        loadPostEdit(postId);
    }

    function loadPostEdit(postId) {
        $('#admin-posts li').removeClass('active');
        var li = $('#post-' + postId);
        if (li) {
            li.addClass('active');
        }
        current.post = postId;
        dataService.get('blogifier/api/posts/post/' + postId, loadPostEditCallback, fail);
    }

    function loadPostEditCallback(data) {
        var btns = '<div class="btn-group">';
        btns += '<button class="btn btn-primary" title="Save" onclick="return postController.savePost(' + data.id + ')">Save</button>';
        btns += '<button class="btn btn-default" title="Add" onclick="return postController.newPost()"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span></button>';
        if (data.published.startsWith('0001')) {
            btns += '<button class="btn btn-default" title="Publish" onclick="return postController.publish(' + data.id + ')"><span class="glyphicon glyphicon-send" aria-hidden="true"></span> Publish</button>';
        }
        else {
            btns += '<button class="btn btn-default" title="Unpublish" onclick="return postController.unpublish(' + data.id + ')"><span class="glyphicon glyphicon-share-alt" aria-hidden="true"></span> Unpublish</button>';
        }
        btns += '<button class="btn btn-default" title="Delete" onclick="return postController.removePost(' + data.id + ')"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span></button></div>';

        $('#post-edit-actions').empty();
        $('#edit-categories').empty();

        var catStr = '<span class="badge clickable" title="Add category" onclick="return postController.addCategory()"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span></span>';
        var cats = data.categories;
        $.each(cats, function (index) {
            var cat = cats[index];
            catStr += '<span class="badge clickable" onclick="postController.removeCategory(' + data.id + ',' + cat.value + ')">' + cat.text + ' <span class="glyphicon glyphicon-remove" aria-hidden="true"></span></span>';
        });
        $('#edit-categories').append(catStr);
        $('#post-edit-actions').append(btns);
        $('#txtPostTitle').val(data.title);

        try {
            tinyMCE.activeEditor.setContent(data.content);
        }
        catch (err) {
            $('#txtContent').html(data.content);
        }
    }

    function newPost() {
        current.post = 0;
        var btns = '<div class="btn-group"><button class="btn btn-default" title="Cancel" onclick="return postController.cancel()">Cancel</button>';
        btns += '<button class="btn btn-primary" title="Save" onclick="return postController.savePost()">Save</button></div>';

        $('#edit-categories').empty();
        $('#post-edit-actions').empty();
        $('#post-edit-actions').append(btns);

        $('#txtPostTitle').val('');
        $('#txtPostTitle').focus();
        tinyMCE.activeEditor.setContent('');
        return false;
    }

    function cancel() {
        reload();
    }

    function savePost() {
        var obj = {
            Id: current.post,
            Title: $("#txtPostTitle").val(),
            Content: tinyMCE.activeEditor.getContent()
        }
        if (obj.Title.length === 0) {
            toastr.error("Title is required");
            return false;
        }
        if (obj.Content.length === 0) {
            toastr.error("Content is required");
            return false;
        }
        dataService.post("blogifier/api/posts", obj, savePostCallback, fail);
    }

    function savePostCallback(data) {
        current.post = JSON.parse(data).id;
        savedCallback();
    }

    function publish(id) {
        dataService.put("blogifier/api/posts/publish/" + id, null, savedCallback, fail);
    }

    function unpublish(id) {
        dataService.put("blogifier/api/posts/unpublish/" + id, null, savedCallback, fail);
    }

    function removePost(postId) {
        dataService.remove('blogifier/api/posts/' + postId, removeCallback, fail);
    }

    function savedCallback() {
        toastr.success('Saved');
        reload();
    }

    function removeCallback() {
        toastr.success('Removed');
        current.post = 0;
        reload();
    }

    // categories

    function addCategory() {
        dataService.get('blogifier/api/categories/blogcategories', loadCategories, fail);
        return false;
    }

    function loadCategories(data) {
        var options = { data: data };
        $("#modalAddCategory").modal();
        $("#txtCategory").easyAutocomplete(options);
        $('div.easy-autocomplete').removeAttr('style');
    }

    function saveCategory() {
        var obj = { Title: $("#txtCategory").val(), PostId: current.post }
        dataService.put("blogifier/api/categories/addcategorytopost", obj, saveCategoryCallback, fail);
    }

    function saveCategoryCallback() {
        var cat = $("#txtCategory").val();
        toastr.success('Saved');
        $("#modalAddCategory").modal('hide');
        $("#txtCategory").val('');
        loadPostEdit(current.post);
    }

    function removeCategory(postId, catId) {
        var obj = { CategoryId: catId, PostId: postId }
        dataService.put("blogifier/api/categories/removecategoryfrompost", obj, removeCategoryCallback, fail);
    }

    function removeCategoryCallback(data) {
        toastr.success('Saved');
        loadPostEdit(current.post);
    }

    // miscellaneous

    function pager(pg) {
        var lastPost = pg.currentPage * pg.itemsPerPage;
        var firstPost = pg.currentPage == 1 ? 1 : ((pg.currentPage - 1) * pg.itemsPerPage) + 1;
        if (lastPost > pg.total) { lastPost = pg.total; }

        var older = '<li class="previous disabled"><a href="#">← Older</a></li>';
        var newer = '<li class="next disabled"><a href="#">Newer →</a></li>';
        if (pg.showOlder === true) {
            older = '<li class="previous" onclick="return postController.loadPage(' + pg.older + ')"><a href="">← Older</a></li>';
        }
        if (pg.showNewer === true) {
            newer = '<li class="next" onclick="return postController.loadPage(' + pg.newer + ')"><a href="#">Newer →</a></li>';
        }
        $('.pager').empty();
        if (pg.showNewer === true || pg.showOlder === true) {
            $('.pager').append(older);
            $('.pager').append('<li class="counter">' + firstPost + '-' + lastPost + ' out of ' + pg.total + '</li>');
            $('.pager').append(newer);
        }
    }

    function getPubDate(date) {
        return date.startsWith('0001') ? 'Draft' : '<span class="glyphicon glyphicon-time" aria-hidden="true"></span> ' + getDate(date);
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
        loadPage: loadPage,
        loadPostEdit: loadPostEdit,
        newPost: newPost,
        savePost: savePost,
        removePost: removePost,
        publish: publish,
        unpublish: unpublish,      
        addCategory: addCategory,
        saveCategory: saveCategory,
        removeCategory: removeCategory,
        cancel: cancel
    }
}(DataService);

document.addEventListener("DOMContentLoaded", function (event) { 
    tinymce.init({
        selector: '#txtContent',
        plugins: [
            "advlist autolink lists link image charmap print preview anchor",
            "searchreplace visualblocks code textcolor imagetools",
            "insertdatetime media table contextmenu paste fullscreen fileupload"
        ],
        toolbar: "styleselect | alignleft aligncenter alignright | bullist numlist | forecolor backcolor | link media fileupload | code fullscreen",
        autosave_ask_before_unload: false,
        height: "515",
        menubar: false,
        relative_urls: false,
        browser_spellcheck: true,
        paste_data_images: true
    });

    postController.loadPage(1);
});

$(document).ready(function () {
    //postController.loadPage(1);
});