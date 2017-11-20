var newsletter = function (dataService) {
    var obj = {};

    function subscribe() {
        var email = document.getElementById("txtSubscriberEmail");

        if (!isValidEmail(email.value)) {
            $('#msg-alert').fadeIn();
            return false;
        }

        var json = {
            "CustomKey": "NEWSLETTER",
            "CustomValue": email.value
        }

        $.ajax({
            url: '/blogifier/api/newsletter/subscribe',
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(json),
            success: function (data) {
                $('#msg-alert').hide();
                $('#frm-newsletter').fadeOut();
                $('#msg-success').fadeIn();
            }
        });

        function isValidEmail(emailAddress) {
            var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
            return pattern.test(emailAddress);
        };
    }

    function remove() {
        $('input:checkbox.item-checkbox:checked').each(function () {
            dataService.put('blogifier/api/newsletter/remove/' + $(this).val(), obj, done, fail);
        });
    }

    function done(data) {
        toastr.success('Completed');
        setTimeout(function () {
            window.location.href = getUrl('admin/packages/widgets/newsletter');
        }, 2000);
    }

    function fail() {
        toastr.error('Failed');
    }

    return {
        subscribe: subscribe,
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
        $('.btn-primary').prop('disabled', false);
    }
    else {
        $('.btn-primary').prop('disabled', true);
    }
}