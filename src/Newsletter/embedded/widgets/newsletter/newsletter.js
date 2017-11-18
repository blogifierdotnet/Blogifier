var newsletter = function () {

    function subscribe() {
        var email = document.getElementById("txtSubscriberEmail");
        alert(email.value);

        var json = {
            "CustomKey": "NEWSLETTER",
            "CustomValue": email
        }

        $.ajax({
            url: '/blogifier/api/newsletter/subscribe',
            type: 'PUT',
            contentType: 'application/json',
            data: json,
            success: function (data) {
                alert('Thank you!');
            }
        });
    }

    return {
        subscribe: subscribe
    }
}();
