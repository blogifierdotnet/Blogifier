var uploadController = function (dataService) {

    function remove(type) {
        dataService.remove('blogifier/api/assets/' + type, removeCallback, fail);
    }

    function removeCallback(data) {
        toastr.success('Removed');
        location.reload();
    }

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
        clickMultiple: clickMultiple,
        uploadMultiple: uploadMultiple,
        remove: remove
    }
}(DataService);
