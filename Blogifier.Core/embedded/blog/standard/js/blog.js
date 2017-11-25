//// cover
//$(window).on('load resize', function () {
//    if ($(window).height() <= 767 && $(window).width() <= 767) {
//        var headerHeight = $(".blog-header").outerHeight();
//        var coverHeight = $(window).height() - headerHeight;
//        $(".cover").height(coverHeight);
//    } else {
//        $(".cover").attr('style', function (i, style) {
//            return style.replace(/height[^;]+;?/g, '');
//        });
//    }
//});

// logout
function profileLogOut() {
    $("#frmLogOut").submit();
}

// blog-header
$(".blog-header-toggle").click(function () {
    $(".blog-header-modal").toggleClass("active");
});


// tooltip
$('[data-toggle="tooltip"]').tooltip({
    animation: false
});


