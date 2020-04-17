var fileManager = function (dataService) {
    var callBack;
    var uplType;
    var postId;

    function uploadClick(uploadType, id) {
        uplType = uploadType;

        if (uploadType === 'AppLogo') { callBack = appLogoCallback; }
        if (uploadType === 'AppCover') { callBack = appCoverCallback; }
        if (uploadType === 'PostImage') { callBack = insertImgCallback; }
        if (uploadType === 'PostCover') { callBack = postCoverCallback; postId = id; }
        if (uploadType === 'Avatar') { callBack = userAvatarCallback; }

        $('#file').trigger('click');
        return false;
    }
    function uploadSubmit() {
        var data = new FormData($('#frmUpload')[0]);
        var url = postId > 0 ? 'upload/' + uplType + '?postId=' + postId : 'upload/' + uplType;
        dataService.upload(url, data, callBack, fail);
    }

    function appLogoCallback (data) {
        $('#logo').val(data.url);
        location.reload();
    }

    function appCoverCallback (data) {
        $('#cover').val(data.url);
        location.reload();
    }

    function postCoverCallback(data) {
        //$('.editor-header').css('background-image', 'url(' + getUrl(data.url) + ')');
        //$('.post-cover').css('background-image', 'url(' + getUrl(data.url) + ')');
        $('#cover').val(data.url);
    }

    function userAvatarCallback(data) {
        $("#profile-avatar").attr("src", getUrl(data.url));
        $('#avatar').val(data.url);
    }

    function insertImgCallback(data) {
        var cm = _editor.codemirror;
        var url = data.url;
        var output = url + '](' + webRoot + url + ')';

        if (url.toLowerCase().match(/.(mp4|ogv|webm)$/i)) {
            var extv = 'mp4';
            if (url.toLowerCase().match(/.(ogv)$/i)) { extv = 'ogg'; }
            if (url.toLowerCase().match(/.(webm)$/i)) { extv = 'webm'; }
            output = '<video width="320" height="240" controls>\r\n  <source src="' + webRoot + url;
            output += '" type="video/' + extv + '">Your browser does not support the video tag.\r\n</video>';
        }
        else if (url.toLowerCase().match(/.(mp3|ogg|wav)$/i)) {
            var exta = 'mp3';
            if (url.toLowerCase().match(/.(ogg)$/i)) { exta = 'ogg'; }
            if (url.toLowerCase().match(/.(wav)$/i)) { exta = 'wav'; }
            output = '<audio controls>\r\n  <source src="' + webRoot + url;
            output += '" type="audio/' + exta + '">Your browser does not support the audio tag.\r\n</audio>';
        }
        else if (url.toLowerCase().match(/.(jpg|jpeg|png|gif)$/i)) {
            output = '\r\n![' + output;
        }
        else {
            output = '\r\n[' + output;
        }
        var selectedText = cm.getSelection();
        cm.replaceSelection(output);
    }

    function fail(data) {
        console.log(data.url); 
    }

    return {
        uploadClick: uploadClick,
        uploadSubmit: uploadSubmit
    };
}(DataService);

$('.bf-posts-list .item-link-desktop').click(function () {
    $('.bf-posts-list .item-link-desktop').removeClass('active');
    $(this).addClass('active');
});