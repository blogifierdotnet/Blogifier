var editorController = function (dataService, filePickerController) {

    function loadPostEdit(postId) {
        dataService.get('blogifier/api/posts/post/' + postId, loadPostEditCallback, fail);
    }

    function loadPostEditCallback(data) {
        //$('#edit-categories').empty();
        ////var catStr = '<span class="btn btn-success clickable" title="Add category" onclick="return postController.addCategory()"><span class="fa fa-plus" aria-hidden="true"></span></span>';
        //var catStr = '';
        //var cats = data.categories;
        //if (cats === null) { cats = []; }
        //$.each(cats, function (index) {
        //    var cat = cats[index];
        //    catStr += '<li>'+ cat.text +'<button type="button" class="clickable float-right" onclick="postController.removeCategory(' + data.id + ',' + cat.value + ')"><i class="fa fa-remove"></i></button></li>';
        //});
        //$('#edit-categories').append(catStr);
        $('#txtPostTitle').val(data.title);
        $('#txtPostImage').val(data.image);
        tinyMCE.activeEditor.setContent(data.content);
    }

    function savePost() {
        var obj = {
            Id: $('#hdnPostId').val(),
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
        var url = JSON.parse(data);
        toastr.success('Saved');
        setTimeout(function () {
            window.location.href = webRoot + url;
        }, 2000); 
    }

    // categories

    //function addCategory() {
    //    dataService.get('blogifier/api/categories/blogcategories', loadCategories, fail);
    //    return false;
    //}

    //function loadCategories(data) {
    //    var options = { data: data };
    //    $("#modalAddCategory").modal();
    //    $("#txtCategory").easyAutocomplete(options);
    //    $('div.easy-autocomplete').removeAttr('style');
    //}

    //function saveCategory() {
    //    var obj = { Title: $("#txtCategory").val(), PostId: current.post }
    //    dataService.put("blogifier/api/categories/addcategorytopost", obj, saveCategoryCallback, fail);
    //}

    //function saveCategoryCallback() {
    //    var cat = $("#txtCategory").val();
    //    toastr.success('Saved');
    //    $("#modalAddCategory").modal('hide');
    //    $("#txtCategory").val('');
    //    loadPostEdit(current.post);
    //}

    //function removeCategory(postId, catId) {
    //    var obj = { CategoryId: catId, PostId: postId }
    //    dataService.put("blogifier/api/categories/removecategoryfrompost", obj, removeCategoryCallback, fail);
    //}

    //function removeCategoryCallback(data) {
    //    toastr.success('Saved');
    //    loadPostEdit(current.post);
    //}

    // miscellaneous

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

    //function pager(pg) {
    //    var lastPost = pg.currentPage * pg.itemsPerPage;
    //    var firstPost = pg.currentPage == 1 ? 1 : ((pg.currentPage - 1) * pg.itemsPerPage) + 1;
    //    if (lastPost > pg.total) { lastPost = pg.total; }

    //    var older = '<li class="disabled"><a href="#"><i class="fa fa-arrow-left"></i></a></li>';
    //    var newer = '<li class="disabled"><a href="#"><i class="fa fa-arrow-right"></i></a></li>';
    //    if (pg.showOlder === true) {
    //        older = '<li onclick="return postController.loadPage(' + pg.older + ')"><a href="#"><i class="fa fa-arrow-left"></i></a></li>';
    //    }
    //    if (pg.showNewer === true) {
    //        newer = '<li onclick="return postController.loadPage(' + pg.newer + ')"><a href="#"><i class="fa fa-arrow-right"></i></a></li>';
    //    }
    //    $('.pagination-custom').empty();
    //    if (pg.showNewer === true || pg.showOlder === true) {
    //        $('.pagination-custom').append(older);
    //        $('.pagination-custom').append('<li><a class="item-count">' + firstPost + '-' + lastPost + ' out of ' + pg.total + '</a></li>');
    //        $('.pagination-custom').append(newer);
    //    }
    //}

    //function getPubDate(date) {
    //    return date.startsWith('0001') ? 'DRAFT' : '<span class="glyphicon glyphicon-time" aria-hidden="true"></span> ' + getDate(date);
    //}

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
        //addCategory: addCategory,
        //saveCategory: saveCategory,
        //removeCategory: removeCategory,
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
        height: "515",
        menubar: false,
        relative_urls: false,
        browser_spellcheck: true,
        paste_data_images: true
    });
});