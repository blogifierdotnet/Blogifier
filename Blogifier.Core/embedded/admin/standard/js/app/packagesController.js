var packagesController = function (dataService) {
    var obj = {};

    function enable() {
        $('input:checkbox.item-checkbox:checked').each(function () {
            dataService.put('blogifier/api/packages/enable/' + $(this).val(), obj, done, fail);
        });
    }

    function disable() {
        $('input:checkbox.item-checkbox:checked').each(function () {
            dataService.put('blogifier/api/packages/disable/' + $(this).val(), obj, done, fail);
        });
    }

    function done(data) {
        toastr.success('Completed');
        setTimeout(function () {
            window.location.href = getUrl('admin/packages/widgets');
        }, 2000);
    }

    function fail() {
        toastr.error('Failed');
    }

    return {
        enable: enable,
        disable: disable
    }
}(DataService);

$('#btnEnable').prop('disabled', true);
$('#btnArrow').prop('disabled', true);

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
        $('#btnEnable').prop('disabled', false);
        $('#btnArrow').prop('disabled', false);
    }
    else {
        $('#btnEnable').prop('disabled', true);
        $('#btnArrow').prop('disabled', true);
    }
}
