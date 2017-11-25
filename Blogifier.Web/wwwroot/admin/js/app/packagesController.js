var packagesController = function (dataService) {
    var obj = {};

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
        disable: disable
    }
}(DataService);

$(":checkbox").on("click", function () {
    if (this.id == "selectAll") {
        var selected = this.checked;
        $("input:checkbox.item-checkbox").each(function () {
            $(this).prop('checked', selected);
        });
    }
    toggleActionBtns();
});

function toggleActionBtns() {
    if ($("input:checkbox.item-checkbox:checked").length > 0) {
        $('#postActionButtons > button').prop('disabled', false);
    }
    else {
        $('#postActionButtons > button').prop('disabled', true);
    }
}
