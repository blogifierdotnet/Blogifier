toastr.options.positionClass = 'toast-bottom-right';
toastr.options.backgroundpositionClass = 'toast-bottom-right';

function profileLogOut() {
    $("#frmLogOut").submit();
}

function getDate(date) {
    var d = new Date(Date.parse(date));
    var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    return monthNames[d.getMonth()] + " " + d.getDate() + ", " + d.getFullYear();
}

function fromQueryString(name) {
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(window.location.href);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

function bytesToSize(bytes) {
    var sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB'];
    if (bytes == 0) return '0 Byte';
    var i = parseInt(Math.floor(Math.log(bytes) / Math.log(1024)));
    return Math.round(bytes / Math.pow(1024, i), 2) + ' ' + sizes[i];
};

//function loading() {
//    $('#app-spinner').show();
//}

//function loaded() {
//    $('#app-spinner').hide();
//}

// setup page
$(".bf-setup-form #AuthorName").keyup(function () {
    var authorUrl = $(this).val();
    authorUrl = authorUrl.replace(/\s+/g, '-').toLowerCase();
    $(".bf-setup-url").text(window.location.host + '/blog/' + authorUrl);
    if ($(this).val() == '') {
        $(".bf-setup-url").text('');
    }
});

// tooltips
$(function () {
    $("#postsMultiactions button").tooltip({
        placement: 'bottom',
        container: 'body'
    });
});

// tooltips
$(function () {
    $(".bf-sidebar-logo").tooltip({
        placement: 'top',
        container: 'body'
    });
});

// fixed elements on modal

// Create the measurement node for scrollbar
var scrollDiv = document.createElement("div");
scrollDiv.className = "scrollbar-measure";
document.body.appendChild(scrollDiv);
var scrollbarWidth = scrollDiv.offsetWidth - scrollDiv.clientWidth;
document.body.removeChild(scrollDiv);


$('.modal').on('show.bs.modal', function () {
    $(".bf-nav").css("padding-right", scrollbarWidth);
    $(".mce-toolbar-grp").css({
        "left": -scrollbarWidth,
        "padding-left": scrollbarWidth
    });
});

$('.modal').on('hidden.bs.modal', function () {
    $(".mce-toolbar-grp").attr("style", "");
});

// sidebar settings active style
$('.bf-sidebar-settings li a[href*="' + location.pathname + '"]').addClass('active');