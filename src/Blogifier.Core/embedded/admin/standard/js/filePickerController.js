var filePickerController = function (dataService) {
    var uploadType = '';
    var postId = 0;

    function open(type, id) {
        uploadType = type;
        postId = id;
        if (type === 'editor') {
            dataService.get('blogifier/api/assets', loadFilePicker, fail);
        }
        else {
            dataService.get('blogifier/api/assets/' + type, loadFilePicker, fail);
        }
    }

    function loadFilePicker(data) {
        $('#filePickerList').empty();
        $.each(data, function (index) {
            var asset = data[index];
            var tag = "";
            if (asset.assetType === 0) {
                // image
                tag = '<a href="#" onclick="filePickerController.pick(\'' +
                    asset.id + '\'); return false;"><img src="' +
                    asset.url + '" alt="' + asset.title + '" title="' + asset.title + '" /></a>';
            }
            else {
                // attachement
                tag = '<a href="#" onclick="filePickerController.pick(\'' +
                    asset.id + '\'); return false;"><img src="' +
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

    function pick(assetId) {
        if (uploadType === "postImage") {
            toastr.success(assetId + ' ' + postId);
            dataService.get('blogifier/api/assets/postimage/' + assetId + '/' + postId, pickCallback, fail);
        }
        else {
            dataService.get('blogifier/api/assets/' + uploadType + '/' + assetId, pickCallback, fail);
        }
    }

    function pickCallback(data) {
        if (uploadType === "profileLogo") {
            $('#Profile_Logo').val(data.url);
        }
        if (uploadType === "profileAvatar") {
            $('#Profile_Avatar').val(data.url);
            $('#profile-img').attr("src", data.url);
        }
        if (uploadType === "profileImage") {
            $('#Profile_Image').val(data.url);
        }
        if (uploadType === "postImage") {
            $('#txtPostImage').val(data.url);
        }
        if (uploadType === "editor") {
            if (data.assetType === 0) {
                tinymce.activeEditor.execCommand('mceInsertContent', false, '<img src="' + data.url + '" />');
            }
            else {
                var tag = '<a class="download" href="' + data.url + '">' + data.title + ' (' + humanFileSize(data.length) + ')</a>';
                tinymce.activeEditor.execCommand('mceInsertContent', false, tag);
            }
        }
        toastr.success('Updated');
        close();
    }

    function humanFileSize(size) {
        var i = Math.floor(Math.log(size) / Math.log(1024));
        return (size / Math.pow(1024, i)).toFixed(2) * 1 + ' ' + ['B', 'kB', 'MB', 'GB', 'TB'][i];
    }

    function fail() {
        toastr.error('Failed');
    }

    function close() {
        $('#modalFilePicker').modal('hide');
    }

    return {
        open: open,
        pick: pick
    }
}(DataService);
