// Create the measurement node
var scrollDiv = document.createElement("div");
scrollDiv.className = "scrollbar-measure";
document.body.appendChild(scrollDiv);
var scrollbarWidth = scrollDiv.offsetWidth - scrollDiv.clientWidth;
document.body.removeChild(scrollDiv);

$(".admin-setup-form #AuthorName").keyup(function () {
    var authorUrl = $(this).val();
    authorUrl = authorUrl.replace(/\s+/g, '-').toLowerCase();
    $(".admin-setup-url").text(window.location.host + '/blog/' + authorUrl);

    if ($(this).val() == '') {
        $(".admin-setup-url").text('');
    }
});

$(function () {
    $(".bf-nav-list a").tooltip({
        placement: 'bottom'
    });

    $(".bf-content-post-cover-body .btn").tooltip({
        placement: 'bottom',
        container: 'body'
    });

    $(".btn-group-actions button").tooltip({
        placement: 'bottom',
        container: 'body'
    });
});

// fixed elements on modal
$('.modal').on('show.bs.modal', function () {
    $(".bf-nav").css("padding-right", scrollbarWidth);
    $(".mce-toolbar-grp").css({
        "left": -scrollbarWidth,
        "padding-left": scrollbarWidth
    });
});

$('.modal').on('hidden.bs.modal', function () {
    $(".bf-nav, .mce-toolbar-grp").attr("style", "");
});