var selectAllCheckbox = $('#selectAll');
var actionBtns = $('#multiActionBtns .btn');
var itemCheckbox = '.item-checkbox';

$(selectAllCheckbox).change(function() { //"select all" change
  $(".item-checkbox").not(":hidden").prop('checked', $(this).prop("checked")); //change all ".item-checkbox" checked status
  toggleActionBtns();

});

$(itemCheckbox).on('change', function() { //".item-checkbox" change
  //uncheck "select all", if one of the listed checkbox item is unchecked
  if (false === $(this).prop("checked")) { //if this item is unchecked
    $(selectAllCheckbox).prop('checked', false); //change "select all" checked status to false
  }

  if ($(itemCheckbox + ':checked').not(":hidden").length == $(itemCheckbox).not(":hidden").length) { //check "select all" if all checkbox items are checked
    $(selectAllCheckbox).prop('checked', true);
  }
  toggleActionBtns();
});

function toggleActionBtns() {
  var bxs = $(itemCheckbox + ':checked').length;
  if (bxs && bxs > 0) {
    $(actionBtns).removeAttr('disabled');
  } else {
    $(actionBtns).attr('disabled', 'disabled');
  }
}

// Toastr Options --------
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
  if (bytes === 0) return '0 Byte';
  var i = parseInt(Math.floor(Math.log(bytes) / Math.log(1024)));
  return Math.round(bytes / Math.pow(1024, i), 2) + ' ' + sizes[i];
}

function getMonthName(i) {
    var m = new Array();
    m[0] = "January";
    m[1] = "February";
    m[2] = "March";
    m[3] = "April";
    m[4] = "May";
    m[5] = "June";
    m[6] = "July";
    m[7] = "August";
    m[8] = "September";
    m[9] = "October";
    m[10] = "November";
    m[11] = "December";
    return m[i];
}

// setup page
$(".bf-setup-form #AuthorName").keyup(function() {
  var authorUrl = $(this).val();
  authorUrl = authorUrl.replace(/\s+/g, '-').toLowerCase();
  $(".bf-setup-url").text(window.location.host + '/blog/' + authorUrl);
  if ($(this).val() === '') {
    $(".bf-setup-url").text('');
  }
});

// Tooltips
$("[data-tooltip]").tooltip({
  container: 'body'
});

// Taskbar Tooltip - this option will toggle the placement of the Tooltip on Desktop and Mobile
var taskbarTooltipOptions = {
  placement: function(context, source) {
    if ($(window).width() > 991) {
      return "right";
    } else {
      return "bottom";
    }
  }
};
$(".taskbar-item-link").tooltip(taskbarTooltipOptions);

// Sidebar Toggle mobile
$(".bf-header").on("click", function() {
  $(this).toggleClass("active");
  $(".bf-sidebar").stop(true, true).slideToggle();
});

// autofocus for modals that has "autofocus" attribute
// http://getbootstrap.com/docs/4.0/components/modal/#how-it-works
$('.modal').on('shown.bs.modal', function () {
    $(this).find('[autofocus]').trigger('focus');
});

// Dropdown
$('.dropdown-custom .dropdown-item').on('click', function() {
  var thisValue = $(this).text();
  $(this).parent().parent().find(".dropdown-toggle .dropdown-value").text(thisValue);
});

function fail(jqXHR, exception) {
    var msg = '';
    if (jqXHR.status === 0) {
        msg = 'Not connected.\n Verify Network.';
    } else if (jqXHR.status === 404) {
        msg = 'Requested page not found. [404]';
    } else if (jqXHR.status === 500) {
        msg = 'Internal Server Error [500].';
    } else if (exception === 'parsererror') {
        msg = 'Requested JSON parse failed.';
    } else if (exception === 'timeout') {
        msg = 'Time out error.';
    } else if (exception === 'abort') {
        msg = 'Ajax request aborted.';
    } else {
        msg = 'Uncaught Error.\n' + jqXHR.responseText;
    }
    $('.loading').fadeOut('fast');
    toastr.error(msg);
}

function emptyCallback() { }