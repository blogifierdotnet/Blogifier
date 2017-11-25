var usersController = function (dataService) {
    var removeUser = function () {
        $('.loading').fadeIn();
        var items = $('.bf-users-list input:checked');
        for (i = 0; i < items.length; i++) {
            if (i + 1 < items.length) {
                dataService.remove('admin/settings/users/' + items[i].id, emptyCallback, fail);
            }
            else {
                dataService.remove('admin/settings/users/' + items[i].id, updateCallback, fail);
            }
        }
        return false;
    }
    function emptyCallback() { }
    function updateCallback() {
        toastr.success('Updated');
        setTimeout(function () {
            location.reload();
        }, 2000);
    }
    function fail(data) {
        toastr.error('Failed');
        $('.loading').fadeOut();
    }
    return {
        removeUser: removeUser
    }
}(DataService);

var itemCheck = $('.item-checkbox');
var firstItemCheck = itemCheck.first();

// check all
$(firstItemCheck).on('change', function () {
    $(itemCheck).prop('checked', this.checked);
    toggleActionBtns();
});

// uncheck "check all" when one item is unchecked
$(itemCheck).not(firstItemCheck).on('change', function () {
    if ($(this).not(':checked')) {
        $(firstItemCheck).prop('checked', false);
    }
});

// show multi action buttons when any item checked
var usersActionButtons = '#userActionButtons';

$('.bf-users-list').on('change', itemCheck, function () {
    toggleActionBtns();
});

function toggleActionBtns() {
    if ($(itemCheck).is(':checked')) {
          $(usersActionButtons).removeAttr('disabled');
    } else {
          $(usersActionButtons).attr('disabled','disabled');
    }
}
