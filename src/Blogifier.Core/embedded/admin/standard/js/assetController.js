var assetController = function (dataService) {
    var currentPage = 1;
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

    function loadFileManager(page) {
        if (page) {
            currentPage = page;
        }
        dataService.get('blogifier/api/assets/' + currentPage + '?type=editor', loadAssetList, fail);
        return false;
    }

    function loadAssetList(data) {
        $('#assetEdit').empty();
        $('#assetList').empty();
        var assets = data.assets;
        $.each(assets, function (index) {
            var asset = assets[index];
            var tag = "";
            if (asset.assetType === 0) {
                // image
                tag = '<a href="#" onclick="assetController.getAsset(\'' +
                    asset.id + '\'); return false;"><img src="' +
                    asset.url + '" alt="' + asset.title + '" title="' + asset.title + '" /></a>';
            }
            else {
                // attachement
                tag = '<a href="#" onclick="assetController.getAsset(\'' +
                    asset.id + '\',\'' + asset.title + '\',' + asset.length + '); return false;"><img src="' +
                    webRoot + asset.image + '" alt="' + asset.title + '" title="' + asset.title + '" /></a>';
            }
            $("#assetList").append(tag);
        });
        var btn = '<button class="btn btn-primary pull-right" title="Add" onclick="return assetController.clickUpload()"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span> New</button>';
        $('#asset-edit-actions').empty();
        $('#asset-edit-actions').append(btn);
        pager(data.pager);
    }

    function pager(pg) {
        var lastPost = pg.currentPage * pg.itemsPerPage;
        var firstPost = pg.currentPage == 1 ? 1 : ((pg.currentPage - 1) * pg.itemsPerPage) + 1;
        if (lastPost > pg.total) { lastPost = pg.total; }

        var older = '<li class="previous disabled"><a href="#">← Older</a></li>';
        var newer = '<li class="next disabled"><a href="#">Newer →</a></li>';
        if (pg.showOlder === true) {
            older = '<li class="previous" onclick="return assetController.loadFileManager(' + pg.older + ')"><a href="">← Older</a></li>';
        }
        if (pg.showNewer === true) {
            newer = '<li class="next" onclick="return assetController.loadFileManager(' + pg.newer + ')"><a href="#">Newer →</a></li>';
        }
        $('.pager').empty();
        if (pg.showNewer === true || pg.showOlder === true) {
            $('.pager').append(older);
            $('.pager').append('<li class="counter">' + firstPost + '-' + lastPost + ' out of ' + pg.total + '</li>');
            $('.pager').append(newer);
        }
    }

    function getAsset(id) {
        dataService.get('blogifier/api/assets/asset/' + id, loadAsset, fail);
    }

    function loadAsset(data) {
        $('#assetEdit').empty();
        $('#assetList').empty();
        $('.pager').empty();
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

        var btn = '<button class="btn btn-default" title="Close" onclick="return assetController.loadFileManager()">Close</button>';
        btn += '<button class="btn btn-danger" title="Delete" onclick="return assetController.remove(' + data.id + ')">Delete</button>';
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
