var packagesController = function (dataService) {
    var obj = {};
    var packages = [];

    function enable() {
        $('.loading').fadeIn();

        $('input:checkbox.item-checkbox:checked').each(function () {
            dataService.put('blogifier/api/packages/enable/' + $(this).val(), obj, done, fail);
        });
    }

    function disable() {
        $('.loading').fadeIn();

        $('input:checkbox.item-checkbox:checked').each(function () {
            dataService.put('blogifier/api/packages/disable/' + $(this).val(), obj, done, fail);
        });
    }

    function disableSingle(id) {
        dataService.put('blogifier/api/packages/disable/' + id, obj, doneSingle, fail);
    }
    function enableSingle(id) {
        dataService.put('blogifier/api/packages/enable/' + id, obj, doneSingle, fail);
    }

    function doneSingle(data) {
        toastr.success('Updated');
    }

    function done(data) {
        setTimeout(function () {
            window.location.href = getUrl('admin/packages/widgets');
        }, 2000);
    }

    function fail() {
        $('.loading').fadeOut();
        toastr.error('Failed');
    }

    return {
        enable: enable,
        disable: disable,
        disableSingle: disableSingle,
        enableSingle: enableSingle,
        packages: packages
    }
}(DataService);

$('#packageInfo').on('show.bs.modal', function (event) {

    var button = $(event.relatedTarget)
    var modalPackage_Title = button.data('title')

    var items = packagesController.packages;

    for (i = 0; i < items.length; i++) {
        var item = items[i];
        var date = new Date(item.LastUpdated);

        if (item && item.Title == modalPackage_Title) {
            var modal = $(this);
            modal.find('.bf-package-info .bf-package-info-title').text(item.Title);
            modal.find('.bf-package-info .bf-package-info-desc').text(item.Description);
            modal.find('.bf-package-info .bf-package-info-logo').attr("src", item.Icon);
            modal.find('.bf-package-info .bf-package-info-cover img').attr("src", item.Cover);
            modal.find('.bf-package-info .bf-package-info-version').text(item.Version);
            modal.find('.bf-package-info .bf-package-info-date').text(getMonthName(date.getMonth()) + " " + date.getDate() + ", " + date.getFullYear());
            //modal.find('.bf-package-info .bf-package-info-installs').text(modalPackage_Installs)
            modal.find('.bf-package-info .bf-package-info-developer').text(item.Author);
        }
    }
});
