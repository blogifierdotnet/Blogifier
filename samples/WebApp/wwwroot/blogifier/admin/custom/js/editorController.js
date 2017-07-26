var editorController = function (dataService, filePickerController) {

    function savePost(publish) {
        $('.spin-icon').fadeIn();
        $('.admin-editor-toolbar').fadeOut();
        var obj = {
            Id: $('#hdnPostId').val(),
            Title: $("#txtPostTitle").val(),
            Image: $('#hdnPostImg').val(),
            Content: tinyMCE.activeEditor.getContent(),
            Description: $('#txtDescription').val(),
            IsPublished: publish ? true : false,
            Categories: []
        }
        if (obj.Title.length === 0) {
            toastr.error("Title is required");
            return false;
        }
        if (obj.Content.length === 0) {
            toastr.error("Content is required");
            return false;
        }
        $("#edit-categories input[type=checkbox]").each(function () {
            if ($(this).is(':checked')) {
                var cat = { Value: $(this).attr('value') };
                obj.Categories.push(cat);
            }
        });
        dataService.post("blogifier/api/posts", obj, savePostCallback, fail);
    }

    function savePostCallback(data) {
        var callback = JSON.parse(data);
        $('#hdnPostId').val(callback.id);
        $('#hdnPostSlug').val(callback.slug);
        $('#hdnPostImg').val(callback.image);
        $('#hdnPublished').val(callback.published);
        toastr.success('Saved');
        loadActionButtons();
        $('.spin-icon').fadeOut();
        $('.admin-editor-toolbar').fadeIn();
    }

    function deletePost() {
        var postId = $('#hdnPostId').val();
        dataService.remove("blogifier/api/posts/" + postId, deletePostCallback, fail);
    }

    function deletePostCallback(data) {
        toastr.success('Deleted');
        setTimeout(function () {
            window.location.href = webRoot + 'admin';
        }, 1000);
    }

    function unpublishPost() {
        var postId = $('#hdnPostId').val();
        var obj = {};
        dataService.put("blogifier/api/posts/unpublish/" + postId, obj, unpublishPostCallback, fail);
    }

    function unpublishPostCallback() {
        toastr.success('Unpublished');
        $('#hdnPublished').val('1/1/0001 12:00:00 AM');
        loadActionButtons();
    }

    function categoryKeyPress(e) {
        if (e.which === 13) {
            e.preventDefault();
            saveCategory();
        }
        return false;
    }

    function saveCategory() {
        var obj = { Title: $("#txtCategory").val() }
        dataService.post("blogifier/api/categories/addcategory", obj, saveCategoryCallback, fail);
    }

    function saveCategoryCallback(data) {
        var obj = JSON.parse(data);
        var li = '<li id="cat-' + obj.id + '">';
        li += '	<label class="custom-control custom-checkbox">';
        li += '	  <input type="checkbox" id="cbx-' + obj.id + '" value="' + obj.id + '" checked="checked" class="custom-control-input">';
        li += '   <span class="custom-control-indicator"></span>';
        li += '   <span class="custom-control-description">' + obj.title + '</span>';
        li += '	</label>';
        li += '<i class="fa fa-times" onclick="editorController.removeCategory(\'' + obj.id + '\')"></i>';
        li += '</li>';
        $("#edit-categories").prepend(li);
        $("#txtCategory").val('');
        $("#txtCategory").focus();
    }

    function removeCategory(id) {
        dataService.remove("blogifier/api/categories/" + id, removeCategoryCallback(id), fail);
    }

    function removeCategoryCallback(id) {
        $("#cat-" + id).remove();
        $("#txtCategory").focus();
    }

    function openFilePicker(postId) {
        filePickerController.open('postImage', postId);
    }

    function resetPostImage() {
        var postId = $('#hdnPostId').val();
        if (postId == "0") {
            imgResetCallback();
        }
        else {
            dataService.remove('blogifier/api/assets/resetpostimage/' + postId, imgResetCallback, fail);
        }
    }

    function imgResetCallback() {
        $('#hdnPostImg').val('');
        loadPostImage();
    }

    function loadPostImage() {
        var postId = $('#hdnPostId').val();
        var postImg = $('#hdnPostImg').val();
        $('#post-image').empty();
        if (!postImg.length > 0) {
            var btn = '<button type="button" title="Add Cover" class="btn btn-secondary btn-block" data-placement="bottom" onclick="return editorController.openFilePicker(' + postId + ');">Upload Post Cover</button >';
        }
        $('#post-image').append(btn);
        if (postImg.length > 0) {
        var dd = '<div class="admin-editor-cover-image"><img src="' + postImg + '" /></div>';
            dd += '<button type="button" class="btn btn-danger btn-block" onclick="return editorController.resetPostImage();">Remove Cover</button>';
        }
        $('#post-image').append(dd);
    }

    function loadActionButtons() {
        var postId = $('#hdnPostId').val();
        var postSlug = $('#hdnPostSlug').val();
        var published = $('#hdnPublished').val();
        $('#action-buttons').empty();
        var btn = '';
        if (postId === '0') {
            // new
            btn += '<div class="btn-group">';
            btn += '<button type="button" onclick="editorController.savePost(true); return false;" class="btn btn-black">Publish</button>';
            btn += '<button type="button" class="btn btn-black dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>';
            btn += '<div class="dropdown-menu">';
            btn += '<a class="dropdown-item" onclick="editorController.savePost(); return false;">Save</a>';
            btn += '</div></div>';
        }
        else {
            if (published.indexOf("0001") >= 0) {
                // draft
                btn += '<div class="btn-group">';
                btn += '<button type="button" onclick="editorController.savePost(true); return false;" class="btn btn-black">Publish</button>';
                btn += '<button type="button" class="btn btn-black dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>';
                btn += '<div class="dropdown-menu">';
                btn += '<a class="dropdown-item" onclick="editorController.savePost(); return false;">Save</a>';
                btn += '<a class="dropdown-item" onclick="editorController.deletePost(); return false;">Delete</a>';
                btn += '</div></div>';
            }
            else {
                // published
                btn += '<div class="btn-group">';
                btn += '<button type="button" onclick="editorController.savePost(); return false;" class="btn btn-black btn-block">Save</button>';
                btn += '<button type="button" class="btn btn-black dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>';
                btn += '<div class="dropdown-menu">';
                btn += '<a class="dropdown-item" onclick="editorController.unpublishPost(); return false;">Unpublish</a>';
                btn += '<a class="dropdown-item" onclick="editorController.deletePost(); return false;">Delete</a>';
                btn += '</div></div> ';
                btn += '<a href="' + webRoot + blogRoute + postSlug + '" target="_blank" class="btn btn-secondary">View</a>';
            }
        }
        $('#action-buttons').append(btn);
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
        savePost: savePost,
        unpublishPost: unpublishPost,
        deletePost: deletePost,
        saveCategory: saveCategory,
        removeCategory: removeCategory,
        categoryKeyPress: categoryKeyPress,
        openFilePicker: openFilePicker,
        loadPostImage: loadPostImage,
        resetPostImage: resetPostImage,
        loadActionButtons: loadActionButtons
    }
}(DataService, filePickerController);

document.addEventListener("DOMContentLoaded", function (event) {
    tinymce.init({
        skin: "blogifier",
        selector: '#txtContent',
        plugins: [
            "advlist autolink lists link image charmap print preview anchor",
            "searchreplace visualblocks code textcolor imagetools",
            "insertdatetime media table contextmenu paste fullscreen fileupload codesample"
        ],
        toolbar: "styleselect | bold italic underline | alignleft aligncenter alignright | bullist numlist | forecolor backcolor | link media fileupload | codesample code fullscreen",
        autosave_ask_before_unload: false,
        height: "400",
        menubar: false,
        relative_urls: false,
        browser_spellcheck: true,
        paste_data_images: true
    });
});