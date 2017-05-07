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

    function fail() {
        toastr.error('Failed');
    }

    return {
        clickSingle: clickSingle,
        uploadSingle: uploadSingle
    }
}(DataService);
