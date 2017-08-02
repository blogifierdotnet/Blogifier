toastr.options.positionClass = 'toast-bottom-right';
toastr.options.backgroundpositionClass = 'toast-bottom-right';

$('.dropdown-menu').on({"click": function (e) {
        e.stopPropagation();
    }
});

$(function () {
    $('.post-list [data-toggle="tooltip"]').tooltip({
        offset: '-15px 0',
        animation: false
    });
    $('[data-toggle="tooltip"], .btn-icon').tooltip({
        animation: false
    });
});

$('.admin-settings-sidebar li').removeClass('active');
var navSidebarActivated = false;
$(".admin-settings-sidebar li").each(function (idx, li) {
    if (window.location.pathname.indexOf("/" + $(li).attr("class")) > 0) {
        $(li).addClass('active');
        navSidebarActivated = true;
    }
});
if (navSidebarActivated === false) {
    $('.admin-settings-sidebar .basic').addClass('active');
}

$('.admin-nav li').removeClass('active');
var navAdminActivated = false;
$(".admin-nav li").each(function (idx, li) {
    if (window.location.pathname.indexOf("/" + $(li).attr("class")) > 0) {
        $(li).children().addClass('active');
        navAdminActivated = true;
    }
});
if (navAdminActivated === false) {
    $('.admin-nav .admin').children().addClass('active');
}

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
