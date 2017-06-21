var toolsController = function (dataService) {

    function importRss() {
        var data = {
            ProfileId: 1,
            FeedUrl: $('#txtFeedUrl').val(),
            Domain: 'bbb',
            SubDomain: 'ccc',
            ImportImages: $('#chkImportImages').is(':checked'),
            ImportAttachements: $('#chkImportAttachements').is(':checked')
        };
        dataService.put('blogifier/api/tools/rssimport', data, importRssCallback, fail);
    }

    function importRssCallback(data) {
        var msg = JSON.parse(data);
        if (msg.isSuccessStatusCode) {
            toastr.success(msg.reasonPhrase);
        }
        else {
            toastr.error(msg.reasonPhrase);
        }
    }

    function fail(data) {
        var x = data;
        toastr.error('Failed');
    }

    return {
        importRss: importRss
    }
}(DataService);
