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
                tag = '<div class="col-sm-4 col-md-3"><a class="asset-list-item" href="#" onclick="assetController.getAsset(\'' +
                    asset.id + '\'); return false;"><img src="' +
                    asset.url + '" alt="' + asset.title + '" title="' + asset.title + '" /></a></div>';
            } else {
                // attachement
                tag = '<div class="col-sm-4 col-md-3"><a class="asset-list-item" href="#" onclick="assetController.getAsset(\'' +
                    asset.id + '\',\'' + asset.title + '\',' + asset.length + '); return false;"><img src="' +
                    webRoot + asset.image + '" alt="' + asset.title + '" title="' + asset.title + '" /></a><div>';
            }
            $("#assetList").append(tag);
        });
        var btn = '<button class="btn btn-black btn-main" onclick="return assetController.clickUpload()">Upload</button>';
        $('#asset-edit-actions').empty();
        $('#asset-edit-actions').append(btn);
        pager(data.pager);
    }

    function pager(pg) {
        var lastPost = pg.currentPage * pg.itemsPerPage;
        var firstPost = pg.currentPage == 1 ? 1 : ((pg.currentPage - 1) * pg.itemsPerPage) + 1;
        if (lastPost > pg.total) {
            lastPost = pg.total;
        }

        var older = '<li class="disabled"><a href="#"><i class="fa fa-long-arrow-left"></i></a></li>';
        var newer = '<li class="disabled"><a href="#"><i class="fa fa-long-arrow-right"></i></a></li>';
        if (pg.showOlder === true) {
            older = '<li onclick="return assetController.loadFileManager(' + pg.older + ')"><a href="#"><i class="fa fa-long-arrow-left"></i></a></li>';
        }
        if (pg.showNewer === true) {
            newer = '<li onclick="return assetController.loadFileManager(' + pg.newer + ')"><a href="#"><i class="fa fa-long-arrow-right"></i></a></li>';
        }
        $('.pagination-custom').empty();
        if (pg.showNewer === true || pg.showOlder === true) {
            $('.pagination-custom').append(older);
            $('.pagination-custom').append('<li><a class="item-count">' + firstPost + '-' + lastPost + ' out of ' + pg.total + '</a></li>');
            $('.pagination-custom').append(newer);
        }
    }

    function getAsset(id) {
        dataService.get('blogifier/api/assets/asset/' + id, loadAsset, fail);
    }

    function loadAsset(data) {
        $('#assetEdit').empty();
        $('#assetList').empty();
        $('.pagination-custom').empty();
        var url = data.assetType === 0 ? data.url : webRoot + data.image;


        var tag = '<div class="asset-edit-item"><img class="asset-edit-item-img" src="' + url + '"/></div>';
        tag += '<div class="asset-edit-item-info">';
        tag += '<div class="row"><div class="col-sm-3 col-md-2">Name:</div><div class="col-sm-9 col-md-10">' + data.title + '</div></div>';
        tag += '<div class="row"><div class="col-sm-3 col-md-2">URL:</div><div class="col-sm-9 col-md-10"><a href="' + data.url + '" target="_blank">' + data.url + '</a></div></div>';
        tag += '<div class="row"><div class="col-sm-3 col-md-2">Size:</div><div class="col-sm-9 col-md-10">' + bytesToSize(data.length) + '</div></div>';
        if (data.assetType === 1) {
            tag += '<div class="row"><div class="col-sm-3 col-md-2">Download count:</div><div class="col-sm-9 col-md-10">' + data.downloadCount + '</div></div>';
        }
        tag += '<div class="row"><div class="col-sm-3 col-md-2">Last updated:</div><div class="col-sm-9 col-md-10">' + getDate(data.lastUpdated) + '</div></div>';
        tag += '</div>';

        $('#assetEdit').append(tag);

        var btn = '<button class="btn btn-danger btn-main mr-2" onclick="return assetController.remove(' + data.id + ')">Delete</button>';
        btn += '<button class="btn btn-secondary" onclick="return assetController.loadFileManager()">Cancel</button>';
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