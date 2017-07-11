// logout
function profileLogOut() {
    $("#frmLogOut").submit();
}

// blog-search
$(".blog-search button").click(function () {
    $(".blog-search").addClass("active");
    $(".blog-search input").focus();
});
$(".blog-search input").blur(function () {
    $(".blog-search").removeClass("active");
});

// dotdotdot
$(function () {
    $('.post-item-text').dotdotdot();
});

// tooltip
$('[data-toggle="tooltip"]').tooltip({
    animation: false
});
