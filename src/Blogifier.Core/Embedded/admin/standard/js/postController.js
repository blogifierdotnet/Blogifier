var postController = function (dataService) {

    var currentPage = 1;
    var currentPost = 0;

    function loadPage(page) {
        currentPage = page;
        dataService.get('blogifier/api/posts/' + currentPage, loadList, fail);
        return false;
    }

    function loadList(data) {
        $('#admin-posts').empty();
        var posts = data.blogPosts;
        if (posts.length < 1) {
            $("#admin-posts").append('<li class="list-group-item">Empty</li>');
            return false;
        }
        $.each(posts, function (index) {
            var post = posts[index];
            var anc = '<a class="post-list-title" id="post-title-' + post.blogPostId + '" href="" onclick="postController.loadPostView(' + post.blogPostId + '); return false;">' +
                '	' + post.title + '</a>' +
                '	<div class="small m-t-xs" style="padding: 5px 0">' +
                '	    <span id="post-time-' + post.blogPostId + '" class="pull-left">' + getPubDate(post.published) + '</span>' +
                '       <span class="pull-right">' + post.postViews + ' views</span>' +
                '	</div>';
            var li = '<li id="post-' + post.blogPostId + '" class="list-group-item">' + anc + '</li>';
            $("#admin-posts").append(li);
        });

        pager(data.pager);

        if (posts[0]) {
            loadPostView(posts[0].blogPostId);
        }
    }

    function loadPostView(postId) {
        currentPost = postId;
        if (currentPost > 0) {
            dataService.get('blogifier/api/posts/post/' + currentPost, loadPostViewCallback, fail);
        }
        $('#admin-posts li').removeClass('active');
        var li = $('#post-' + currentPost);
        if (li) {
            li.addClass('active');
        }
    }

    function loadPostViewCallback(data) {
        closeEditor();
        $('#article-content').empty();
        $('#article-header').empty();
        $('#article-categories').empty();

        var catStr = '<i class="fa fa-tags"></i> ';
        var cats = data.categories;
        $.each(cats, function (index) {
            var cat = cats[index];
            catStr += '<span class="btn btn-white btn-xs">' + cat.text + '</span>';
        });
        $('#article-categories').append(catStr);

        var content = data.content;
        $('#article-content').append(content);
        $('#article-header').text(data.title);

        var btns = '<button class="btn btn-white btn-xs" title="Edit post" onclick="return postController.loadPostEdit(' + data.id + ')"><i class="fa fa-pencil"></i> Edit</button>';
        btns += '<a href="/blog/' + data.slug + '" class="btn btn-white btn-xs" target="_blank" title="View in the blog"><i class="fa fa-eye"></i> View</a>';
        $('#post-view-actions').empty();
        $('#post-view-actions').append(btns);

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

    function loadPostEdit(id) {
        dataService.get('blogifier/api/posts/post/' + id, loadPostEditCallback, fail);
    }

    function loadPostEditCallback(data) {
        openEditor();
        var btns = '<button class="btn btn-white btn-xs" title="Cancel" onclick="return postController.closeEditor()">Close</button>';
        btns += '<button class="btn btn-white btn-xs" title="Save" onclick="return postController.savePost(' + data.id + ')">Save</button>';
        if (data.published.startsWith('0001')) {
            btns += '<button class="btn btn-white btn-xs" title="Publish" onclick="return postController.publish(' + data.id + ')"><i class="fa fa-send"></i> Publish</button>';
        }
        else {
            btns += '<button class="btn btn-white btn-xs" title="Unpublish" onclick="return postController.unpublish(' + data.id + ')"><i class="fa fa-undo"></i> Unpublish</button>';
        }
        btns += '&nbsp;&nbsp;<button class="btn btn-white btn-xs" title="Delete" onclick="return postController.removePost(' + data.id + ')"><i class="fa fa-trash-o"></i></button>';

        $('#post-edit-actions').empty();
        $('#edit-categories').empty();

        var catStr = '<i class="fa fa-tags"></i> <span class="btn btn-white btn-xs" title="Add category" onclick="return postController.addCategory()"><i class="fa fa-plus"></i></span>';
        var cats = data.categories;
        $.each(cats, function (index) {
            var cat = cats[index];
            catStr += '<span class="btn btn-white btn-xs" onclick="postController.removeCategory(' + data.id + ',' + cat.value + ')">' + cat.text + ' <i class="fa fa-remove"></i></span>';
        });
        $('#edit-categories').append(catStr);
        $('#post-edit-actions').append(btns);

        $('#txtPostTitle').val(data.title);
        tinyMCE.activeEditor.setContent(data.content);
    }

    function newPost() {
        openEditor();
        currentPost = 0;
        var btns = '<button class="btn btn-white btn-xs" title="Cancel" onclick="return postController.closeEditor()">Cancel</button>';
        btns += '<button class="btn btn-white btn-xs" title="Save" onclick="return postController.savePost()">Save</button> &nbsp;&nbsp;';

        $('#post-edit-actions').empty();
        $('#post-edit-actions').append(btns);

        $('#txtPostTitle').val('');
        tinyMCE.activeEditor.setContent('');
        return false;
    }

    function savePost() {
        var obj = {
            Id: currentPost,
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
        currentPost = JSON.parse(data).id;
        savedCallback();
    }

    function publish(id) {
        dataService.put("blogifier/api/posts/publish/" + id, null, savedCallback, fail);
    }

    function unpublish(id) {
        dataService.put("blogifier/api/posts/unpublish/" + id, null, savedCallback, fail);
    }

    function removePost(postId) {
        dataService.remove('blogifier/api/posts/' + postId, savedCallback, fail);
    }

    function savedCallback() {
        toastr.success('Saved');
        loadPage(currentPage);
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
        var obj = { Title: $("#txtCategory").val(), PostId: currentPost }
        dataService.put("blogifier/api/categories/addcategorytopost", obj, saveCategoryCallback, fail);
    }

    function saveCategoryCallback() {
        var cat = $("#txtCategory").val();
        toastr.success('saving category : ' + cat);
        $("#modalAddCategory").modal('hide');
        $("#txtCategory").val('');
        loadPostEdit(currentPost);
    }

    function removeCategory(postId, catId) {
        var obj = { CategoryId: catId, PostId: postId }
        dataService.put("blogifier/api/categories/removecategoryfrompost", obj, removeCategoryCallback, fail);
    }

    function removeCategoryCallback(data) {
        toastr.success('Saved');
        loadPostEdit(currentPost);
    }

    // miscellaneous

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
        loadPostView: loadPostView,
        loadPostEdit: loadPostEdit,
        newPost: newPost,
        savePost: savePost,
        removePost: removePost,
        publish: publish,
        unpublish: unpublish,      
        closeEditor: closeEditor,
        addCategory: addCategory,
        saveCategory: saveCategory,
        removeCategory: removeCategory
    }
}(DataService);

postController.loadPage(1);

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