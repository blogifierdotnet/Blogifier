toastr.options.positionClass = 'toast-bottom-right';
toastr.options.backgroundpositionClass = 'toast-bottom-right';

// scrollbar width
var scrollDiv = document.createElement("div");
scrollDiv.className = "scrollbar-measure";
document.body.appendChild(scrollDiv);
var scrollbarWidth = scrollDiv.offsetWidth - scrollDiv.clientWidth;
document.body.removeChild(scrollDiv);

$(function () {
    $(document).on('click', '.bf-sidebar-posts-header .list-filter', function () {
        $(".bf-sidebar-posts-tools").toggle();
    });
});

$(function () {
    var sidebarHeader = $(".bf-sidebar-posts-header");
    var checkMulti = $(".bf-sidebar .item-control-multi");
    $(document).on('change', checkMulti, function () {
        if (checkMulti.is(':checked')) {
            $(sidebarHeader).addClass("active");
        }
        else {
            $(sidebarHeader).removeClass("active");
        }
    });
});

$(".admin-setup-form #AuthorName").keyup(function () {
    var authorUrl = $(this).val();
    authorUrl = authorUrl.replace(/\s+/g, '-').toLowerCase();
    $(".admin-setup-url").text(window.location.host + '/blog/' + authorUrl);

    if ($(this).val() == '') {
        $(".admin-setup-url").text('');
    }
});

$(function () {
    $(document).on('click', '[data-modal-target]', function () {
        $("body").addClass("modal-open");
        $($(this).data("modal-target")).show();
        $("body").css("padding-right", scrollbarWidth);
        $(".bf-nav").css("margin-right", scrollbarWidth);
    });
    $(document).on('click', '.modal-close', function () {
        $("body").removeClass("modal-open");
        $(this).parents().find(".modal").hide();
        $("body").css("padding-right", "");
        $(".bf-nav").css("margin-right", "0");
    });
});


$(function () {
    $(document).on('click', '.dropdown .dropdown-toggle', function () {
        $(this).parent(".dropdown").toggleClass("active");
    });
    $(document).click(function (e) {
        var target = e.target;
        if (!$(target).is('.dropdown .dropdown-toggle') && !$(target).parents().is('.dropdown .dropdown-toggle')) {
            $('.dropdown').removeClass("active");
        }
    });
    //$(document).on('click', '.dropdown-menu', function (e) {
    //    e.stopPropagation();
    //});
});

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

function loading() {
    $('#app-spinner').show();
}

function loaded() {
    $('#app-spinner').hide();
}


$('.tooltip').on({
    mouseenter: function () {

        var tooltip = $(this);
        var tooltipAttr = tooltip.attr('title');

        var tooltipOffset = tooltip.offset();
        var tooltipHeight = tooltip.outerHeight();
        var tooltipWidth = tooltip.outerWidth() / 2;
        $(tooltip).attr("data-tooltip-title", tooltipAttr);
        $(tooltip).removeAttr("title");

        var tooltipDataAttr = tooltip.data("tooltip-title");

        $("body").append('<div class="tooltip-box">' + tooltipDataAttr + '</div>').find(".tooltip-box").css({
            top: tooltipOffset.top + tooltipHeight,
            left: tooltipOffset.left + tooltipWidth
        });

    },
    mouseleave: function () {
        var tooltip = $(this);
        var tooltipDataAttr = tooltip.data("tooltip-title");
        $(".tooltip-box").remove();
        $(tooltip).attr("title", tooltipDataAttr);
        $(tooltip).removeAttr("data-tooltip-title");

    }

});