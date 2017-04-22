var postController = function (dataService) {
    function newPost() {
        open();
        var btns = '<button class="btn btn-white btn-xs" title="Cancel" onclick="return postController.close()">Cancel</button>';
        btns += '<button class="btn btn-white btn-xs" title="Save" onclick="return postController.save(0)">Save</button> &nbsp;&nbsp;';

        $('#edit-actions').empty();
        $('#edit-actions').append(btns);

        $('#txtPostTitle').val('');
        tinyMCE.activeEditor.setContent('');
        return false;
    }

    function edit(id) {
        dataService.get('blogifier/api/posts/post/' + id, editArticle, fail);
        return false;
    };

    function view(id) {
        dataService.get('blogifier/api/posts/post/' + id, viewArticle, fail);
        return false;
    };

    function open() {
        $("#post-edit").fadeIn();
        $("#post-view").hide();
        return false;
    }

    function close() {
        $("#post-edit").hide();
        $("#post-view").fadeIn();
        return false;
    };

    function load(page) {
        dataService.get('blogifier/api/posts/' + page, reload, fail);
        return false;
    };

    function save(id) {
        toastr.success('save ' + id);
    };

    function remove(postId) {
        toastr.success('remove ' + postId);
        return false;
    };

    function addCategory() {
        $("#modalAddCategory").modal();
    }

    function removeCategory(postId, catId) {
        toastr.success('removing ' + postId + ', ' + catId);
        return false;
    }

    function reload(data) {
        $('#admin-posts').empty();
        var posts = data.blogPosts;
        if (posts.length < 1) {
            return false;
        }
        $.each(posts, function (index) {
            var post = posts[index];
            var draft = post.published.startsWith('0001') ? 'draft' : '';
            var info = draft == 'draft' ? 'Draft' : '<i class="fa fa-clock-o"></i> ' + getDate(post.published);
            var active = '';

            if (index === 0) {
                active = 'active';
                article(post.blogPostId);
            }
            var anc = '<a href="" onclick="postController.article(' + post.blogPostId + '); return false;">' +
                '	<strong>' + post.title + '</strong>' +
                '	<div class="small m-t-xs">' +
                '		<p class="m-b-none">' +
                '			' + info + '<span class="pull-right">' + post.postViews + ' views</span>';
                '		</p>' +
                '	</div>' +
                '</a>';

                var li = '<li id="post-' + post.blogPostId + '" class="list-group-item ' + active + '">' + anc + '</li>';
            $("#admin-posts").append(li);
        });
        pager(data.pager);
    }

    function pager(pg) {
        var lastPost = pg.currentPage * pg.itemsPerPage;
        var firstPost = pg.currentPage == 1 ? 1 : ((pg.currentPage - 1) * pg.itemsPerPage) + 1;
        if (lastPost > pg.total) { lastPost = pg.total; }

        var older = '';
        var newer = '';
        if (pg.showOlder === true) {
            older = '<a href="" onclick="return postController.load(' + pg.older + ')"><i class="fa fa-angle-right"></i></a>';
        }
        if (pg.showNewer === true) {
            newer = '<a href="" onclick="return postController.load(' + pg.newer + ')"><i class="fa fa-angle-left"></i></a>';
        }
        $('#pager-label').text('Posts ' + firstPost + '-' + lastPost + ' out of ' + pg.total);

        $('#pager-older').empty();
        $('#pager-older').append(older);

        $('#pager-newer').empty();
        $('#pager-newer').append(newer);
    }

    function article(id) {
        dataService.get('blogifier/api/posts/post/' + id, viewArticle, fail);

        $('#admin-posts li').removeClass('active');
        var li = $('#post-' + id);
        if (li) {
            li.addClass('active');
        }
    }

    function editArticle(data) {
        open();
        var btns = '<button class="btn btn-white btn-xs" title="Cancel" onclick="return postController.close()">Cancel</button>';
        btns += '<button class="btn btn-white btn-xs" title="Save" onclick="return postController.save(' + data.id + ')">Save</button> &nbsp;&nbsp;';
        btns += '<button class="btn btn-white btn-xs" title="Delete" onclick="return postController.remove(' + data.id + ')"><i class="fa fa-trash-o"></i></button>';

        $('#edit-actions').empty();
        $('#edit-categories').empty();

        var catStr = '';
        var cats = data.categories;
        $.each(cats, function (index) {
            var cat = cats[index];
            catStr += '<span class="btn btn-white btn-xs" onclick="postController.removeCategory(' + data.id + ',' + cat.value +  ')">' + cat.text + ' <i class="fa fa-remove"></i></span>';
        });
        catStr += '<span class="btn btn-white btn-xs" title="Add category" onclick="postController.addCategory()"><i class="fa fa-plus"></i></span>';

        $('#edit-categories').append(catStr);
        $('#edit-actions').append(btns);

        $('#txtPostTitle').val(data.title);
        tinyMCE.activeEditor.setContent(data.content);
    }

    function viewArticle(data) {
        close();
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

        var btns = '<button class="btn btn-white btn-xs" title="Edit post" onclick="return postController.edit(' + data.id + ')"><i class="fa fa-pencil"></i> Edit</button>';
        btns += '<a href="/blog/' + data.slug + '" class="btn btn-white btn-xs" target="_blank" title="View in the blog"><i class="fa fa-eye"></i></a> &nbsp;&nbsp;';
        btns += '<button class="btn btn-white btn-xs" title="Delete" onclick="return postController.remove(' + data.id + ')"><i class="fa fa-trash-o"></i></button>';
        $('#article-actions').empty();
        $('#article-actions').append(btns);
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
        new: newPost,
        edit: edit,
        view: view,
        close: close,
        load: load,
        article: article,
        save: save,
        remove: remove,
        addCategory: addCategory,
        removeCategory: removeCategory
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