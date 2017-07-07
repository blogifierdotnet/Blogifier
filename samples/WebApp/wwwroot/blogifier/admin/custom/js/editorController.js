var editorController = function (dataService, filePickerController) {

    function savePost(publish) {
        var obj = {
            Id: $('#hdnPostId').val(),
            Title: $("#txtPostTitle").val(),
            Content: tinyMCE.activeEditor.getContent(),
            IsPublished: publish === true ? publish : false,
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
        toastr.success('Saved');
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
        li += '	  <input type="checkbox" id="cbx-' + obj.id + '" value="' + obj.id + '" class="custom-control-input">';
        li += '   <span class="custom-control-indicator"></span>';
        li += '   <span class="custom-control-description">' + obj.title + '</span>';
        li += '	</label>';
        li += '<button type="button" tabindex="-1" onclick="editorController.removeCategory(\'' + obj.id + '\')"><i class="fa fa-times"></i></button>';
        li += '</li>';
        $("#edit-categories").append(li);
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

    function resetPostImage(postId) {
        dataService.remove('blogifier/api/assets/resetpostimage/' + postId, imgResetCallback, fail);
    }

    function imgResetCallback(data) {
        var url = JSON.parse(data);
        toastr.success('Updated');
        setTimeout(function () {
            window.location.href = webRoot + url;
        }, 1000); 
    }

    function loadPostImage() {
        var postId = $('#hdnPostId').val();
        var postImg = $('#hdnPostImg').val();
        // toastr.success(postId + ' :: ' + postImg);

        var btn = '<button type="button" class="btn btn-secondary" onclick="return editorController.openFilePicker(' + postId + ');">';
        btn += '<i class="fa fa-image" ></i></button >';

        if (postImg.length > 0) {
            btn = '<button type="button" class="btn btn-secondary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">';
            btn += '<i class="fa fa-image"></i></button>';
        }
        $('#post-image').append(btn);

        var dd = '<div id="ddPostImg" class="dropdown-menu dropdown-menu-right" aria-labelledby="btnPostImgToggle">';
        dd += '<div class="post-image"><img src="' + postImg + '" /><div class="post-image-actions btn-group">';
        dd += '<button type="button" class="btn btn-danger" onclick="return editorController.resetPostImage(' + postId + ');">';
        dd += '<i class="fa fa-trash"></i></button></div></div></div>';

        $('#post-image').append(dd);
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
        saveCategory: saveCategory,
        removeCategory: removeCategory,
        categoryKeyPress: categoryKeyPress,
        openFilePicker: openFilePicker,
        loadPostImage: loadPostImage,
        resetPostImage: resetPostImage
    }
}(DataService, filePickerController);

document.addEventListener("DOMContentLoaded", function (event) {
    tinymce.init({
        skin: "blogifier",
        selector: '#txtContent',
        plugins: [
            "advlist autolink lists link image charmap print preview anchor",
            "searchreplace visualblocks code textcolor imagetools",
            "insertdatetime media table contextmenu paste fullscreen fileupload"
        ],
        toolbar: "styleselect | alignleft aligncenter alignright | bullist numlist | forecolor backcolor | link media fileupload | code fullscreen",
        autosave_ask_before_unload: false,
        height: "400",
        menubar: false,
        relative_urls: false,
        browser_spellcheck: true,
        paste_data_images: true
    });
});