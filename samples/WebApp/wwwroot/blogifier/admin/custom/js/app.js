toastr.options.positionClass = 'toast-bottom-right';
toastr.options.backgroundpositionClass = 'toast-bottom-right';

//
$('.admin-settings-sidebar ul li').removeClass('active');
var activated = false;
$(".admin-settings-sidebar ul li").each(function (idx, li) {
    if (window.location.pathname.indexOf("/" + $(li).attr("class")) > 0) {
        $(li).addClass('active');
        activated = true;
    }
});
if (activated === false) { $('.admin-settings-sidebar .basic').addClass('active'); }


//
$('.dropdown-menu-app li').removeClass('active');
var activatedAD = false;
$(".dropdown-menu-app li").each(function (idx, li) {
    if (window.location.pathname.indexOf("/" + $(li).attr("class")) > 0) {
        $(li).children().addClass('active');
        $(".nav-title").text($(li).children().text());

        activatedAD = true;
    }
});
if (activatedAD === false) {
    $('.dropdown-menu-app .admin').children().addClass('active');
    $(".nav-title").text($('.dropdown-menu-app .admin').children().text());
}


//
function profileLogOut() {
    $("#frmLogOut").submit();
}

function getDate(date) {
    var d = new Date(Date.parse(date));
    var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    return monthNames[d.getMonth()] + " " + d.getDate() + ", " + d.getFullYear();
}

function bytesToSize(bytes) {
    var sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB'];
    if (bytes == 0) return '0 Byte';
    var i = parseInt(Math.floor(Math.log(bytes) / Math.log(1024)));
    return Math.round(bytes / Math.pow(1024, i), 2) + ' ' + sizes[i];
};

function loading() {
    $('#app-spinner').show();
}

function loaded() {
    $('#app-spinner').hide();
}