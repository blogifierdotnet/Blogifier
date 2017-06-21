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
        dataService.put('blogifier/api/tools/rssimport', data, actionCallback, fail);
    }

    function updateDisqus() {
        var data = {
            CustomKey: $('#DisqusModel_CustomKey').val(),
            CustomValue: $('#DisqusModel_CustomValue').val(),
            Id: $('#DisqusModel_Id').val(),
            ParentId: $('#DisqusModel_ParentId').val(),
            Title: $('#DisqusModel_Title').val(),
            CustomType: $('#DisqusModel_CustomType').val()
        };
        dataService.put('blogifier/api/tools/disqus', data, actionCallback, fail);
    }

    function actionCallback(data) {
        var msg = JSON.parse(data);
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
        importRss: importRss,
        updateDisqus: updateDisqus
    }
}(DataService);
