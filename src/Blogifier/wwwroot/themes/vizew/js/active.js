(function ($) {
    'use strict';

    var browserWindow = $(window);

    // :: 1.0 Preloader Active Code
    browserWindow.on('load', function () {
        $('.preloader').fadeOut('slow', function () {
            $(this).remove();
        });
    });

    // :: 2.0 Nav Active Code
    if ($.fn.classyNav) {
        $('#vizewNav').classyNav();
    }

    // :: 3.0 Newsticker Active Code
    if ($.fn.simpleTicker) {
        $.simpleTicker($("#breakingNewsTicker"), {
            speed: 1000,
            delay: 3000,
            easing: 'swing',
            effectType: 'roll'
        });
    }

    // :: 4.0 Sticky Active Code
    if ($.fn.sticky) {
        $("#sticker").sticky({
            topSpacing: 0
        });
    }

    // :: 5.0 Sliders Active Code
    if ($.fn.owlCarousel) {
        $('.twitter-slides').owlCarousel({
            items: 1,
            margin: 0,
            loop: true,
            dots: false,
            autoplay: true,
            autoplayTimeout: 4000,
            smartSpeed: 1000
        });

        $('.featured-post-slides').owlCarousel({
            items: 1,
            margin: 0,
            loop: true,
            nav: true,
            navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
            dots: false,
            autoplay: true,
            autoplayTimeout: 4000,
            smartSpeed: 1000
        });

        $('.sport-video-slides').owlCarousel({
            items: 1,
            margin: 0,
            loop: true,
            nav: true,
            navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
            dots: false,
            autoplay: true,
            autoplayTimeout: 4000,
            smartSpeed: 1000
        });

        $('.business-video-slides').owlCarousel({
            items: 1,
            margin: 0,
            loop: true,
            nav: true,
            navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
            dots: false,
            autoplay: true,
            autoplayTimeout: 4000,
            smartSpeed: 1000
        });
    }

    // :: 6.0 Nicescroll Active Code
    if ($.fn.niceScroll) {
        $(".vizew-nav-tab").niceScroll({
            cursorcolor: "#838586",
            cursorwidth: "6px",
            cursorborder: 'none'
        });
    }

    // :: 7.0 ScrollUp Active Code
    if ($.fn.scrollUp) {
        browserWindow.scrollUp({
            scrollSpeed: 1500,
            scrollText: '<i class="ti-angle-up"></i>'
        });
    }

    // :: 8.0 Tooltip Active Code
    if ($.fn.tooltip) {
        $('[data-toggle="tooltip"]').tooltip();
    }

    // :: 9.0 CounterUp Active Code
    if ($.fn.counterUp) {
        $('.counter').counterUp({
            delay: 10,
            time: 3000
        });
    }

    // :: 10.0 Prevent Default a Click
    $('a[href="#"]').on('click', function (e) {
        e.preventDefault();
    });

    // :: 11.0 Wow Active Code
    if (browserWindow.width() > 767) {
        new WOW().init();
    }

})(jQuery);

var subscribeClick = function () {
    var email = $('#nlEmail').val();
    if (email) {
        subscribe("newsletter/subscribe", email);
    }
    return false;
}

var subscribe = function (url, email) {

    $.getJSON('http://gd.geobytes.com/GetCityDetails?callback=?', function (data) {
        var ip = data.geobytesremoteip || 'n/a';
        var country = data.geobytescountry || 'n/a';
        var region = data.geobytesregion || 'n/a';

        var all = ip + '|' + country + '|' + region;

        var obj = { Email: email, Ip: all };
        var options = {
            url: "/" + url,
            type: "POST",
            data: JSON.stringify(obj),
            contentType: "application/json",
            dataType: "html",
            success: done,
            error: done
        };
        $.ajax(options);
    });
}

var done = function (data) {
    $('#frmNewsletter').slideUp();
    $('#ttlNewsletter').slideDown();
}