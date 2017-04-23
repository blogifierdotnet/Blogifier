var postController = function (dataService) {
    function loadPage(page) {
        if (page === null || page === '') { page = 1; }
        $('#hdnCurrentPage').val(page);
        dataService.get('blogifier/api/posts/' + page, loadPageCallback, fail);
        return false;
    }

    function loadPost(id) {
        $('#hdnSelectedPost').val(id);
        dataService.get('blogifier/api/posts/post/' + id, loadPostCallback, fail);

        $('#admin-posts li').removeClass('active');
        var li = $('#post-' + id);
        if (li) {
            li.addClass('active');
        }
    }

    function addPost() {
        openEditor();
        var btns = '<button class="btn btn-white btn-xs" title="Cancel" onclick="return postController.closeEditor()">Cancel</button>';
        btns += '<button class="btn btn-white btn-xs" title="Save" onclick="return postController.savePost(0)">Save</button> &nbsp;&nbsp;';

        $('#edit-actions').empty();
        $('#edit-actions').append(btns);

        $('#txtPostTitle').val('');
        tinyMCE.activeEditor.setContent('');
        return false;
    }

    function editPost(id) {
        dataService.get('blogifier/api/posts/post/' + id, editPostCallback, fail);
        return false;
    }
    
    function savePost(id) {
        var obj = {
            Id: id,
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
        obj.categories = [];
        //$("#categoryMgrList input[type=checkbox]").each(function () {
        //    if ($(this).is(":checked")) {
        //        if ($(this).attr("id")) {
        //            var catId = $(this).attr("id").replace("cbx-", "");
        //            obj.categories.push(catId);
        //        }
        //    }
        //});
        dataService.post("blogifier/api/posts", obj, savePostCallback, fail);
    }

    function removePost(postId) {
        dataService.remove('blogifier/api/posts/' + postId, postRemoveCallback, fail);
        return false;
    }

    function addCategory() {
        $("#modalAddCategory").modal();
    }

    function removeCategory(postId, catId) {
        toastr.success('removing ' + postId + ', ' + catId);
        return false;
    }

    function publish(id) {
        dataService.put("blogifier/api/posts/publish/" + id, null, savedCallback, fail);
        loadPost(id);
        //return false;
    }

    function unpublish(id) {
        dataService.put("blogifier/api/posts/unpublish/" + id, null, savedCallback, fail);
        loadPost(id);
        //return false;
    }

    // miscellaneous

    function openEditor() {
        $("#post-edit").fadeIn();
        $("#post-view").hide();
        return false;
    }

    function closeEditor() {
        $("#post-edit").hide();
        $("#post-view").fadeIn();
        return false;
    }

    function pager(pg) {
        var lastPost = pg.currentPage * pg.itemsPerPage;
        var firstPost = pg.currentPage == 1 ? 1 : ((pg.currentPage - 1) * pg.itemsPerPage) + 1;
        if (lastPost > pg.total) { lastPost = pg.total; }

        var older = '';
        var newer = '';
        if (pg.showOlder === true) {
            older = '<a href="" onclick="return postController.loadPage(' + pg.older + ')"><i class="fa fa-angle-right"></i></a>';
        }
        if (pg.showNewer === true) {
            newer = '<a href="" onclick="return postController.loadPage(' + pg.newer + ')"><i class="fa fa-angle-left"></i></a>';
        }
        $('#pager-label').text('Posts ' + firstPost + '-' + lastPost + ' out of ' + pg.total);

        $('#pager-older').empty();
        $('#pager-older').append(older);

        $('#pager-newer').empty();
        $('#pager-newer').append(newer);
    }

    // callbacks

    function loadPageCallback(data) {
        $('#admin-posts').empty();
        var posts = data.blogPosts;
        if (posts.length < 1) {
            return false;
        }
        $.each(posts, function (index) {
            var post = posts[index];
            var active = '';
            if (index === 0) {
                active = 'active';
                loadPost(post.blogPostId);
            }          
            var anc = '<a class="post-list-title" id="post-title-' + post.blogPostId + '" href="" onclick="postController.loadPost(' + post.blogPostId + '); return false;">' +
                '	' + post.title + '</a>' +
                '	<div class="small m-t-xs" style="padding: 5px 0">' +
                '	    <span id="post-time-' + post.blogPostId + '" class="pull-left">' + getPubDate(post.published) + '</span>' +
                '       <span class="pull-right">' + post.postViews + ' views</span>' +
                '	</div>';

            var li = '<li id="post-' + post.blogPostId + '" class="list-group-item ' + active + '">' + anc + '</li>';
            $("#admin-posts").append(li);
        });
        pager(data.pager);
    }

    function loadPostCallback(data) {
        closeEditor();
        $('#article-content').empty();
        $('#article-header').empty();
        $('#article-categories').empty();

        var catStr = '';
        var cats = data.categories;
        $.each(cats, function (index) {
            var cat = cats[index];
            catStr += '<span class="btn btn-white btn-xs">' + cat.text + '</span>';
        });
        $('#article-categories').append(catStr);

        var content = data.content;
        $('#article-content').append(content);
        $('#article-header').text(data.title);

        var btns = '<button class="btn btn-white btn-xs" title="Edit post" onclick="return postController.editPost(' + data.id + ')"><i class="fa fa-pencil"></i> Edit</button>';
        if (data.published.startsWith('0001')) {
            btns += '<button class="btn btn-white btn-xs" title="Publish" onclick="return postController.publish(' + data.id + ')"><i class="fa fa-send"></i> Publish</button>';
        }
        else {
            btns += '<button class="btn btn-white btn-xs" title="Unpublish" onclick="return postController.unpublish(' + data.id + ')"><i class="fa fa-undo"></i> Unpublish</button>';
        }
        btns += '<a href="/blog/' + data.slug + '" class="btn btn-white btn-xs" target="_blank" title="View in the blog"><i class="fa fa-eye"></i> View</a> &nbsp;&nbsp;';
        btns += '<button class="btn btn-white btn-xs" title="Delete" onclick="return postController.removePost(' + data.id + ')"><i class="fa fa-trash-o"></i></button>';
        $('#article-actions').empty();
        $('#article-actions').append(btns);

        // if title was updated - sync title in the post list
        var curTitle = $('#post-title-' + data.id);
        if (curTitle.text() !== data.title) {
            curTitle.text(data.title);
        }
        // sync post time when publish/unpublish
        var date = getDate(new Date().toLocaleString());
        var curTime = $('#post-time-' + data.id);
        curTime.empty();
        curTime.append(getPubDate(data.published));
    }

    function editPostCallback(data) {
        openEditor();
        var btns = '<button class="btn btn-white btn-xs" title="Cancel" onclick="return postController.closeEditor()">Close</button>';
        btns += '<button class="btn btn-white btn-xs" title="Save" onclick="return postController.savePost(' + data.id + ')">Save</button>&nbsp;&nbsp;';
        btns += '<button class="btn btn-white btn-xs" title="Delete" onclick="return postController.removePost(' + data.id + ')"><i class="fa fa-trash-o"></i></button>';

        $('#edit-actions').empty();
        $('#edit-categories').empty();

        var catStr = '';
        var cats = data.categories;
        $.each(cats, function (index) {
            var cat = cats[index];
            catStr += '<span class="btn btn-white btn-xs" onclick="postController.removeCategory(' + data.id + ',' + cat.value + ')">' + cat.text + ' <i class="fa fa-remove"></i></span>';
        });
        catStr += '<span class="btn btn-white btn-xs" title="Add category" onclick="postController.addCategory()"><i class="fa fa-plus"></i></span>';

        $('#edit-categories').append(catStr);
        $('#edit-actions').append(btns);

        $('#txtPostTitle').val(data.title);
        tinyMCE.activeEditor.setContent(data.content);
    }

    function savePostCallback() {
        toastr.success('Saved');
        dataService.get('blogifier/api/posts/post/' + $('#hdnSelectedPost').val(), loadPostCallback, fail);
    }

    function postRemoveCallback() {
        loadPage(1);
        toastr.success('Removed');
    }

    function savedCallback() {
        toastr.success('Saved');
    }

    function getPubDate(date) {
        return date.startsWith('0001') ? 'Draft' : '<i class="fa fa-clock-o"></i> ' + getDate(date);
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
        loadPost: loadPost,
        addPost: addPost,
        editPost: editPost,
        savePost: savePost,
        removePost: removePost,
        addCategory: addCategory,
        removeCategory: removeCategory,
        closeEditor: closeEditor,
        publish: publish,
        unpublish: unpublish,
    }
}(DataService);

document.addEventListener("DOMContentLoaded", function (event) { 
    tinymce.init({
        selector: '#txtContent',
        plugins: [
            "advlist autolink autoresize lists link image charmap print preview anchor",
            "searchreplace visualblocks code textcolor imagetools",
            "insertdatetime media table contextmenu paste"
        ],
        toolbar: "styleselect | alignleft aligncenter alignright | bullist numlist | forecolor backcolor | link media | code",
        autosave_ask_before_unload: false,
        autoresize_min_height: 320,
        menubar: false,
        relative_urls: false,
        browser_spellcheck: true,
        paste_data_images: true
    });
});