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
                tag = '<div class="col-sm-4"><a href="#" onclick="filePickerController.pick(\'' +
                    asset.id + '\'); return false;"><img src="' +
                    asset.url + '" alt="' + asset.title + '" title="' + asset.title + '" /></a></div>';
            }
            else {
                // attachement
                tag = '<div class="col-sm-4"><a href="#" onclick="filePickerController.pick(\'' +
                    asset.id + '\'); return false;"><img src="' +
                    webRoot + asset.image + '" alt="' + asset.title + '" title="' + asset.title + '" /></a></div>';
            }
            $("#filePickerList").append(tag);
        });
        if (assets && assets.length > 0) {
            $('.modal-list-empty').hide();
        }
        else {
            $('.modal-list-empty').show();
        }
        if ($('#modalFilePicker').is(':visible') === false) {
            $('#modalFilePicker').modal();
        }
        pager(data.pager);
    }

    function pick(assetId) {
        if (uploadType === "postImage") {
            if ($('#hdnPostId').val() === '0') {
                dataService.get('blogifier/api/assets/asset/' + assetId , pickPostImgCallback, fail);
            }
            else {
                dataService.get('blogifier/api/assets/postimage/' + assetId + '/' + $('#hdnPostId').val(), pickPostImgCallback, fail);
            }
        }
        else {
            dataService.get('blogifier/api/assets/' + uploadType + '/' + assetId, pickCallback, fail);
        }
    }

    function pickPostImgCallback(data) {
        $('#hdnPostImg').val(data.url);
        editorController.loadPostImage();
        toastr.success('Updated');
        close();
    }

    function pickCallback(data) {
        if (uploadType === "profilelogo") {
            $('#Logo').val(data.url);
        }
        if (uploadType === "profileavatar") {
            $('#Avatar').val(data.url);

            // TODO: can not access page element from modal
            setTimeout(function () {
                var zzz = $('img.profile-img');
                $('img.profile-img').attr('src', data.url);
            }, 3000);
        }
        if (uploadType === "profileimage") {
            $('#Image').val(data.url);
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

        var older = '';
        var newer = '';
        if (pg.showOlder === true) {
            older = '<li onclick="return filePickerController.load(' + pg.older + ')"><a href="#" role="button" aria-label="Older posts"><i class="fa fa-long-arrow-left"></i></a></li>';
        }
        if (pg.showNewer === true) {
            newer = '<li onclick="return filePickerController.load(' + pg.newer + ')"><a href="#" role="button" aria-label="Newer posts"><i class="fa fa-long-arrow-right"></i></a></li>';
        }
        $('.pagination-custom').empty();
        if (pg.showNewer === true || pg.showOlder === true) {
            $('.pagination-custom').append(older);
            $('.pagination-custom').append('<li><a class="item-count">' + firstPost + '-' + lastPost + ' out of ' + pg.total + '</a></li>');
            $('.pagination-custom').append(newer);
        }
        else {
            $('.modal-footer').hide();
        }
    }

    function uploadAssets() {
        var data = new FormData($('#frmUploadAssets')[0]);
        dataService.upload('blogifier/api/assets/multiple', data, uploadCallback, fail);
    }

    function uploadCallback() {
        toastr.success('Files uploaded');
        loadPage(1);
    }

    function humanFileSize(size) {
        var i = Math.floor(Math.log(size) / Math.log(1024));
        return (size / Math.pow(1024, i)).toFixed(2) * 1 + ' ' + ['B', 'kB', 'MB', 'GB', 'TB'][i];
    }

    function fail() {
        toastr.error('Failed');
    }

    function close() {
        $("body").removeClass("modal-open");
        $("#modalFilePicker").hide();
        $("body").css("padding-right", ""); 
    }

    return {
        open: open,
        load: loadPage,
        pick: pick,
        uploadAssets: uploadAssets
    }
}(DataService);
