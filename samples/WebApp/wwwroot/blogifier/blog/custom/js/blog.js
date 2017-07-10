// BLOG-SEARCH
$(".blog-search button").click(function () {
    $(".blog-search").addClass("active");
    $(".blog-search input").focus();
});
$(".blog-search input").blur(function () {
    $(".blog-search").removeClass("active");
});

//
$(function () {
    $('.post-item-text').dotdotdot();
});

//
$('[data-toggle="tooltip"]').tooltip();