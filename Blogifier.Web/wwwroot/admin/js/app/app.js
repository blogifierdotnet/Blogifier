
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

// setup page
$(".bf-setup-form #AuthorName").keyup(function() {
  var authorUrl = $(this).val();
  authorUrl = authorUrl.replace(/\s+/g, '-').toLowerCase();
  $(".bf-setup-url").text(window.location.host + '/blog/' + authorUrl);
  if ($(this).val() == '') {
    $(".bf-setup-url").text('');
  }
});

// tooltips

var options = {
  placement: function(context, source) {
    if ($(window).width() > 991) {
      return "right";
    } else {
      return "bottom";
    }
  }
};
$(".bf-taskbar .taskbar-item-link").tooltip(options);


$(function() {
  $("#postsMultiactions button").tooltip({
    placement: 'bottom',
    container: 'body'
  });
});

// tooltips
$(function() {
  $(".bf-sidebar-logo").tooltip({
    placement: 'top',
    container: 'body'
  });
});

// tooltips
$(function() {
  $(".bf-editor-footer .btn-icon, .bf-editor-footer .btn-group-icon .btn").tooltip({
    placement: 'top',
    container: 'body'
  });
});

// tooltips
$(function() {
  $(".bf-posts-list .item-status").tooltip({
    placement: 'top',
    container: 'body'
    //position: '-10px'
  });
});

// fixed elements on modal

// Create the measurement node for scrollbar
var scrollDiv = document.createElement("div");
scrollDiv.className = "scrollbar-measure";
document.body.appendChild(scrollDiv);
var scrollbarWidth = scrollDiv.offsetWidth - scrollDiv.clientWidth;
document.body.removeChild(scrollDiv);

$('.modal').on('show.bs.modal', function() {
  $(".mce-toolbar-grp").css({
    "right": scrollbarWidth
  });
});

$('.modal').on('hidden.bs.modal', function() {
  $(".mce-toolbar-grp").attr("style", "");
});

// sidebar toggle mobile
$(".bf-header").on("click", function() {
  $(this).toggleClass("active");
  $(".bf-sidebar").stop(true, true).slideToggle();
});

//
var settingPageTitle = $(".bf-settings").data("page-title");
$(".bf-header span").text(settingPageTitle);

// Tooltips
$("[data-tooltip]").tooltip({
  container: 'body'
});

$('.modal').on('shown.bs.modal', function() {
  $(this).find('[autofocus]').trigger('focus')
})


// dropdown
$('.dropdown-custom .dropdown-item').on('click', function() {
  var thisValue = $(this).text();
  $(this).parent().parent().find(".dropdown-toggle .dropdown-value").text(thisValue);
});
