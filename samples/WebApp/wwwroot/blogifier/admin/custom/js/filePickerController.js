var filePickerController = function (dataService) {
    var uploadType = '';
    var postId = 0;
    var page = 1;

    function open(type, id) {
        uploadType = type;
        postId = id;
        loadPage(1);
    }

    function loadPage(page) {
        dataService.get('blogifier/api/assets/' + page + '?type=' + uploadType, loadFilePicker, fail);
        return false;
    }

    function loadFilePicker(data) {
        $('#filePickerList').empty();
        var assets = data.assets;
        $.each(assets, function (index) {
            var asset = assets[index];
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
        if (assets && assets.length > 0) {
            $('.modal-list-empty').hide();
        }
        else {
            $('.modal-list-empty').show();
        }
        $('#modalFilePicker').modal();
        pager(data.pager);
    }

    function pick(assetId) {
        if (uploadType === "postImage") {
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
            setTimeout(function () {
                window.location.href = webRoot + 'admin/editor/' + postId;
            }, 1000); 
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

    function pager(pg) {
        var lastPost = pg.currentPage * pg.itemsPerPage;
        var firstPost = pg.currentPage == 1 ? 1 : ((pg.currentPage - 1) * pg.itemsPerPage) + 1;
        if (lastPost > pg.total) { lastPost = pg.total; }

        var older = '<li class="previous disabled"><a href="#">← Older</a></li>';
        var newer = '<li class="next disabled"><a href="#">Newer →</a></li>';
        if (pg.showOlder === true) {
            older = '<li class="previous" onclick="return filePickerController.load(' + pg.older + ')"><a href="">← Older</a></li>';
        }
        if (pg.showNewer === true) {
            newer = '<li class="next" onclick="return filePickerController.load(' + pg.newer + ')"><a href="#">Newer →</a></li>';
        }
        $('.pager').empty();
        if (pg.showNewer === true || pg.showOlder === true) {
            $('.pager').append(older);
            $('.pager').append('<li class="counter">' + firstPost + '-' + lastPost + ' out of ' + pg.total + '</li>');
            $('.pager').append(newer);
        }
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
        load: loadPage,
        pick: pick
    }
}(DataService);
