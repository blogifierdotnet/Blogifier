toastr.options.positionClass = 'toast-bottom-right';
toastr.options.backgroundpositionClass = 'toast-bottom-right';

$('li').removeClass('active');
var activated = false;
$("#side-menu li").each(function (idx, li) {
    if (window.location.pathname.indexOf("/" + $(li).attr("class")) > 0) {
        $(li).addClass('active');
        activated = true;
    }
});
if (activated === false) { $('.posts').addClass('active'); }

function profileLogOut() {
    $("#frmLogOut").submit();
}

function getDate(date) {
    var d = new Date(Date.parse(date));
    var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    return monthNames[d.getMonth()] + " " + d.getDate() + ", " + d.getFullYear();
}

function loading() {
    $('#app-spinner').show();
}

function loaded() {
    $('#app-spinner').hide();
}