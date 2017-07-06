var editorController = function (dataService, filePickerController) {

    function loadPostEdit(postId) {
        dataService.get('blogifier/api/posts/post/' + postId, loadPostEditCallback, fail);
    }

    function loadPostEditCallback(data) {
        $('#txtPostTitle').val(data.title);
        $('#txtPostImage').val(data.image);
        tinyMCE.activeEditor.setContent(data.content);
    }

    function savePost() {
        var obj = {
            Id: $('#hdnPostId').val(),
            Title: $("#txtPostTitle").val(),
            Content: tinyMCE.activeEditor.getContent(),
            IsPublished: $('#cbxPublished').is(':checked'),
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

    function saveCategory() {
        var obj = { Title: $("#txtCategory").val() }
        dataService.post("blogifier/api/categories/addcategory", obj, saveCategoryCallback, fail);
    }

    function saveCategoryCallback(data) {
        var obj = JSON.parse(data);
        var li = '<li>';
        li += '	<label class="custom-control custom-checkbox">';
        li += '	  <input type="checkbox" id="cbx-' + obj.id + '" value="' + obj.id + '" class="custom-control-input">';
        li += '   <span class="custom-control-indicator"></span>';
        li += '   <span class="custom-control-description">' + obj.title + '</span>';
        li += '	</label>';
        li += '</li>';
        $("#edit-categories").append(li);
        $("#txtCategory").val('');
        $("#txtCategory").focus();
        toastr.success('Saved');
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
        loadPostEdit: loadPostEdit,
        savePost: savePost,
        saveCategory: saveCategory,
        openFilePicker: openFilePicker,
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