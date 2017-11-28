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
