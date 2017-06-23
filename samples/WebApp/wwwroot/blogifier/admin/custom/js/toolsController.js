var toolsController = function (dataService) {

    function importRss() {
        var data = {
            ProfileId: 1,
            FeedUrl: $('#txtFeedUrl').val(),
            Domain: $('#txtDomain').val(),
            SubDomain: $('#txtSubDomain').val(),
            ImportImages: $('#chkImportImages').is(':checked'),
            ImportAttachements: $('#chkImportAttachements').is(':checked')
        };
        $('.spin-icon').fadeIn();
        $('#btnImport').fadeOut();
        dataService.put('blogifier/api/tools/rssimport', data, importRssCallback, fail);
    }

    function importRssCallback(data) {
        var msg = JSON.parse(data);
        $('.spin-icon').fadeOut();
        $('#btnImport').fadeIn();
        if (msg.isSuccessStatusCode) {
            toastr.success(msg.reasonPhrase);
        }
        else {
            toastr.error(msg.reasonPhrase);
        }
    }

    function fail(data) {
        toastr.error('Failed');
    }

    return {
        importRss: importRss
    }
}(DataService);
