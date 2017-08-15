toastr.options.positionClass = 'toast-bottom-right';
toastr.options.backgroundpositionClass = 'toast-bottom-right';


$(function () {
    $(".bf-nav-toggle").click(function () {
        $(this).parent().toggleClass("active");
    });
});


$(function () {
    $(".head-tools-right").click(function () {
        $(".bf-sidebar-posts-tools").slideToggle();
    });
});

$(function () {
    var toolbarMutli = $(".admin-toolbar-mutlicheck");
    var checkMulti = $(".admin-list-multicheck input[type='checkbox']");
    $(checkMulti).on('change', function() {
        if (checkMulti.is(':checked')) {
            $(toolbarMutli).addClass("visible");
        }
        else {
            $(toolbarMutli).removeClass("visible");
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

$('.dropdown-menu').on({"click": function (e) {
        e.stopPropagation();
    }
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
