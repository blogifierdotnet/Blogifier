var fileManagerController = function (dataService) {
    var callBack;

    function open(openCallback) {
        $('#fileManager').modal();
        load(1);
        callBack = openCallback;
    }

    function close() {
        $('#fileManager').modal('hide')
    }

    function pick(id) {
        callBack(id);
        close();
    }

    function uploadClick() {
        $('#files').trigger('click');
        return false;
    }

    function uploadSubmit() {
        var data = new FormData($('#frmUpload')[0]);

        dataService.upload('blogifier/api/assets/multiple', data, submitCallback, fail);
    }
    function submitCallback() {
        load(1);
    }

    function remove() {
        var items = $('#fileManagerList input:checked');
        for (i = 0; i < items.length; i++) {
            if (i + 1 < items.length) {
                dataService.remove('blogifier/api/assets/' + items[i].id, emptyCallback, fail);
            }
            else {
                dataService.remove('blogifier/api/assets/' + items[i].id, removeCallback, fail);
            }
        }
    }

    function removeCallback(data) {
        toastr.success('Deleted');
        load(1);
    }

    function load(page) {
        uploadType = 'A';
        dataService.get('blogifier/api/assets/' + page + '?type=' + uploadType, loadCallback, fail);
        return false;
    }

    function loadCallback(data) {

        $('#fileManagerList').empty();
        var assets = data.assets;
        $.each(assets, function (index) {
            var asset = assets[index];
            var tag = "";
            if (asset.assetType === 0) {
                // image
                tag = '<div class="col-sm-6 col-md-4 col-lg-3">' +
                    '	<div class="item">' +
                    '	    <a href="" onclick="fileManagerController.pick(' + asset.id + '); return false">' +
                    '		    <div class="item-img"><img src="' + asset.url + '" alt="' + asset.title + '" /></div>' +
                    '	    </a>' +
                    '		<label class="item-name custom-control custom-checkbox">' +
                    '			<input type="checkbox" id="' + asset.id + '" class="custom-control-input item-check">' +
                    '			<span class="custom-control-indicator"></span>' +
                    '			<span class="custom-control-description">' + asset.title + '</span>' +
                    '		</label>' +
                    '	</div>' +
                    '</div>';
            }
            else {
                // attachement
                tag = '<div class="col-sm-6 col-md-4 col-lg-3">' +
                    '	<div class="item">' +
                    '	    <a href="" onclick="fileManagerController.pick(' + asset.id + '); return false">' +
                    '		    <div class="item-img"><img src="' + webRoot + asset.image + '" alt="' + asset.title + '" /></div>' +
                    '	    </a>' +
                    '		<label class="item-name custom-control custom-checkbox">' +
                    '			<input type="checkbox" id="' + asset.id + '" class="custom-control-input item-check">' +
                    '			<span class="custom-control-indicator"></span>' +
                    '			<span class="custom-control-description">' + asset.title + '</span>' +
                    '		</label>' +
                    '	</div>' +
                    '</div>';
            }
            $("#fileManagerList").append(tag);
        });

    }

    function humanFileSize(size) {
        var i = Math.floor(Math.log(size) / Math.log(1024));
        return (size / Math.pow(1024, i)).toFixed(2) * 1 + ' ' + ['B', 'kB', 'MB', 'GB', 'TB'][i];
    }

    function emptyCallback(data) { }
    function fail() {
        toastr.error('Failed');
    }

    return {
        open: open,
        close: close,
        pick: pick,
        uploadClick: uploadClick,
        uploadSubmit: uploadSubmit,
        remove: remove
    }
}(DataService);
