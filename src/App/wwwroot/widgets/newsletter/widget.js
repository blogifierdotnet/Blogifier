var newsletter = function () {
    function subscribe() {
        var email = document.getElementById("txtSubscriberEmail");
        if (email.value.length === 0) {
            return false;
        }
        $.ajax({
            url: '/widgets/newsletter/subscribe',
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(email.value),
            success: function (data) {
                var frm = document.getElementById("newsletter-form");
                var msg = document.getElementById("newsletter-msg");
                frm.style.display = "none";
                if (data.length > 0) {
                    msg.innerHTML = data;
                }
                msg.style.display = "";
                return false;
            }
        });
    }
    return {
        subscribe: subscribe
    };
}();

document.addEventListener("DOMContentLoaded", function (event) {
    document.getElementById('txtSubscriberEmail').onkeypress = function (e) {
        if (!e) e = window.event;
        var keyCode = e.keyCode || e.which;
        if (keyCode === 13) {
            newsletter.subscribe();
            return false;
        }
    };
});