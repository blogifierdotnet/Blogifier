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

});