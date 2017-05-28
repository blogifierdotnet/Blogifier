var uploadController = function (dataService) {
    var uploadType = '';

    function clickSingle(type) {
        uploadType = type;
        $('#file').trigger('click');
        return false;
    }

    function uploadSingle(callback) {
        var data = new FormData($('#frmUpload')[0]);
        var callback = fail;

        if (uploadType === 'profileLogo') {
            callback = uploadPfofileLogoCallback;
        }
        if (uploadType === 'profileAvatar') {
            callback = uploadProfileAvatarCallback;
        }
        if (uploadType === 'profileImage') {
            callback = uploadProfileImageCallback;
        }
        if (uploadType === 'editor') {
            callback = uploadEditor;
        }
        dataService.upload('blogifier/api/assets/single/' + uploadType, data, callback, fail);
    }

    function uploadPfofileLogoCallback(data) {
        $('#Profile_Logo').val(data.url);
        toastr.success('Uploaded logo');
    }

    function uploadProfileAvatarCallback(data) {
        $('#Profile_Avatar').val(data.url);
        $('#profile-img').attr("src", data.url);
        toastr.success('Uploaded avatar');
    }

    function uploadProfileImageCallback(data) {
        $('#Profile_Image').val(data.url);
        toastr.success('Uploaded image');
    }

    function uploadEditor(data) {
        tinymce.activeEditor.execCommand('mceInsertContent', false, '<img src="' + data.url + '" />');
        toastr.success('added to editor');
    }

    function remove(type) {
        dataService.remove('blogifier/api/assets/' + type, removeCallback, fail);
    }

    function removeCallback(data) {
        toastr.success('Removed');
        location.reload();
    }

    // file picker

    function openFilePicker() {
        dataService.get('blogifier/api/assets', loadFilePicker, fail);
    }

    function loadFilePicker(data) {
        $('#filePickerList').empty();
        $.each(data, function (index) {
            var asset = data[index];
            var tag = "";
            if (asset.assetType === 0) {
                // image
                tag = '<a href="#" onclick="uploadController.insertImage(\'' +
                    asset.url + '\'); return false;"><img src="' +
                    asset.url + '" alt="' + asset.title + '" title="' + asset.title + '" /></a>';
            }
            else {
                // attachement
                tag = '<a href="#" onclick="uploadController.insertFile(\'' +
                    asset.url + '\',\'' + asset.title + '\',' + asset.length + '); return false;"><img src="' +
                    webRoot + asset.image + '" alt="' + asset.title + '" title="' + asset.title + '" /></a>';
            }
            $("#filePickerList").append(tag);
        });
        if (data && data.length > 0) {
            $('.modal-list-empty').hide();
        }
        else {
            $('.modal-list-empty').show();
        }
        $('#modalFilePicker').modal();
    }

    function insertImage(url) {
        tinymce.activeEditor.execCommand('mceInsertContent', false, '<img src="' + url + '" />');
        $('#modalFilePicker').modal('hide');
    }

    function insertFile(url, title, len) {
        var tag = '<a class="download" href="' + url + '">' + title + ' (' + humanFileSize(len) + ')</a>';
        tinymce.activeEditor.execCommand('mceInsertContent', false, tag);
        $('#modalFilePicker').modal('hide');
    }

    function humanFileSize(size) {
        var i = Math.floor(Math.log(size) / Math.log(1024));
        return (size / Math.pow(1024, i)).toFixed(2) * 1 + ' ' + ['B', 'kB', 'MB', 'GB', 'TB'][i];
    }

    // multiple

    function clickMultiple() {
        $('#files').trigger('click');
        return false;
    }

    function uploadMultiple() {
        var data = new FormData($('#frmUploadMultiple')[0]);

        dataService.upload('blogifier/api/assets/multiple', data, uploadMultipleCallback, fail);
    }

    function uploadMultipleCallback(data) {
        toastr.success('Uploaded');
    }

    function firstUpload() {
        $('#modalFilePicker').modal('hide');
        clickMultiple();
        return false;
    }

    function fail() {
        toastr.error('Failed');
    }

    return {
        clickSingle: clickSingle,
        clickMultiple: clickMultiple,
        uploadSingle: uploadSingle,
        uploadMultiple: uploadMultiple,
        openFilePicker: openFilePicker,
        firstUpload: firstUpload,
        insertImage: insertImage,
        insertFile: insertFile,
        remove: remove,
        uploadType: uploadType
    }
}(DataService);
