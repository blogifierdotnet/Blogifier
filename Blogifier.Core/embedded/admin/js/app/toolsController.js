var toolsController = function (dataService) {

    function importRss() {
        var data = {
            FeedUrl: $('#txtFeedUrl').val(),
            Domain: $('#txtDomain').val(),
            ImportImages: $('#chkImportImages').is(':checked'),
            ImportAttachements: $('#chkImportAttachements').is(':checked')
        };
        $('.loading').fadeIn();
        $('#btnImport .btn').attr('disabled', 'disabled');
        dataService.put('blogifier/api/tools/rssimport', data, importRssCallback, fail);
    }

    function importRssCallback(data) {
        var msg = JSON.parse(data);
        $('.loading').fadeOut();
        $('#btnImport .btn').removeAttr('disabled');
        if (msg.isSuccessStatusCode) {
            toastr.success(msg.reasonPhrase);
        }
        else {
            toastr.error(msg.reasonPhrase);
        }
    }

    function deleteBlog(id) {
        $('.loading').fadeIn();
        dataService.remove("blogifier/api/tools/deleteblog/" + id, callbackDeleteBlog, fail);
    }

    function callbackDeleteBlog(data) {
        toastr.success('Blog removed');
        setTimeout(function() {
            location.reload();
        }, 2000);
    }

    function fail(data) {
        toastr.error('Failed');
        $('.loading').fadeOut();
        $('#btnImport .btn').removeAttr('disabled');
    }

    return {
        importRss: importRss,
        deleteBlog: deleteBlog
    }
}(DataService);
