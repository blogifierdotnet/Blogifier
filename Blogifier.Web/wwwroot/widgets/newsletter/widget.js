var newsletter = function () {
    var obj = {};

    function subscribe(ip) {
        var email = document.getElementById("txtSubscriberEmail");

        if (!isValidEmail(email.value)) {
            $('#msg-alert').fadeIn();
            return false;
        }

        // user stats
        var parser = new UAParser();
        var result = parser.getResult();

        var browser = result.browser.name + ' ' + result.browser.version;
        var device = 'Undefined';
        if (result.device.model) {
            device = result.device.model + ' ' + result.device.type + ' ' + result.device.vendor;
        }
        var os = result.os.name + ' ' + result.os.version;
        
        var json = {
            "CustomKey": "NEWSLETTER",
            "CustomValue": email.value + '|' + browser + '|' + device + '|' + os + '|' + ip
        }

        $.ajax({
            url: root + 'blogifier/widgets/newsletter/subscribe',
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

    return {
        subscribe: subscribe
    }
}();