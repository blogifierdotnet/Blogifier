var newsletter = function (dataService) {
    var obj = {};

    function remove() {
        $('.loading').fadeIn();

        $('input:checkbox.item-checkbox:checked').each(function () {
            var url = 'blogifier/widgets/newsletter/remove/' + $(this).val();
            dataService.put(url, obj, done, fail);
        });
    }

    function done(data) {
        setTimeout(function () {
            window.location.href = root + 'blogifier/widgets/newsletter/settings';
        }, 2000);
    }

    function fail() {
        $('.loading').fadeOut();
        toastr.error('Failed');
    }

    return {
        remove: remove
    }
}(DataService);

$(":checkbox").on("click", function () {
    if (this.name == "selectAll") {
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