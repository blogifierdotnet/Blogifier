var toolsController = function (dataService) {

    function importRss() {
        var data = {
            ProfileId: $('#hdnProfileId').val(),
            FeedUrl: $('#txtFeedUrl').val(),
            Domain: $('#txtDomain').val(),
            SubDomain: $('#txtSubDomain').val(),
            ImportImages: $('#chkImportImages').is(':checked'),
            ImportAttachements: $('#chkImportAttachements').is(':checked')
        };
        $('.spin-icon').fadeIn();
        $('#btnImport .btn').attr('disabled', 'disabled');
        dataService.put('blogifier/api/tools/rssimport', data, importRssCallback, fail);
    }

    function importRssCallback(data) {
        var msg = JSON.parse(data);
        $('.spin-icon').fadeOut();
        $('#btnImport .btn').removeAttr('disabled');
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
