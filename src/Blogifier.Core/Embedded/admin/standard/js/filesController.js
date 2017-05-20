var filesController = function (dataService) {
    function clickUpload() {
        $('#files').trigger('click');
        return false;
    }

    function uploadAssets() {
        var data = new FormData($('#frmUploadAssets')[0]);
        dataService.upload('blogifier/api/assets/multiple', data, uploadCallback, fail);
    }

    function uploadCallback() {
        toastr.success('Files uploaded');
        loadFileManager();
    }

    function loadFileManager() {
        dataService.get('blogifier/api/assets', loadAssetList, fail);
    }

    function loadAssetList(data) {
        $('#assetEdit').empty();
        $('#assetList').empty();
        $.each(data, function (index) {
            var asset = data[index];
            var tag = "";
            if (asset.assetType === 0) {
                // image
                tag = '<a href="#" onclick="filesController.getAsset(\'' +
                    asset.id + '\'); return false;"><img src="' +
                    asset.url + '" alt="' + asset.title + '" title="' + asset.title + '" /></a>';
            }
            else {
                // attachement
                tag = '<a href="#" onclick="filesController.getAsset(\'' +
                    asset.id + '\',\'' + asset.title + '\',' + asset.length + '); return false;"><img src="' +
                    webRoot + asset.image + '" alt="' + asset.title + '" title="' + asset.title + '" /></a>';
            }
            $("#assetList").append(tag);
        });
        var btn = '<button class="btn btn-primary pull-right" title="Add" onclick="return filesController.clickUpload()"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span> New</button>';
        $('#asset-edit-actions').empty();
        $('#asset-edit-actions').append(btn);
    }

    function getAsset(id) {
        dataService.get('blogifier/api/assets/' + id, loadAsset, fail);
    }

    function loadAsset(data) {
        $('#assetEdit').empty();
        $('#assetList').empty();
        var url = data.assetType === 0 ? data.url : webRoot + data.image;
        var tag = '<h4>' + data.title + '</h4>';
        tag += '<p>Url: ' + data.url + '</p>';
        tag += '<p>Size: ' + bytesToSize(data.length) + '</p>';
        if (data.assetType === 1) {
            tag += '<p>Download count: ' + data.downloadCount + '</p>';
        }
        tag += '<p>Last updated: ' + getDate(data.lastUpdated) + '</p>';
        tag += '<img src="' + url + '" style="max-width: 100%" />';
        $('#assetEdit').append(tag);

        var btn = '<button class="btn btn-default" title="Close" onclick="return filesController.loadFileManager()">Close</button>';
        btn += '<button class="btn btn-danger" title="Delete" onclick="return filesController.remove(' + data.id + ')">Delete</button>';
        $('#asset-edit-actions').empty();
        $('#asset-edit-actions').append(btn);
    }

    function remove(id) {
        dataService.remove('blogifier/api/assets/' + id, removeCallback, fail);
    }

    function removeCallback(data) {
        toastr.success('Deleted');
        loadFileManager();
    }

    function fail() {
        toastr.error('Failed');
    }

    return {
        clickUpload: clickUpload,
        uploadAssets: uploadAssets,
        loadFileManager: loadFileManager,
        getAsset: getAsset,
        remove: remove
    }
}(DataService);
