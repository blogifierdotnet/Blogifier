var notificationsController = function (dataService) {
    function remove(id) {
        dataService.remove("api/settings/removenotification/" + id, removeCallback, fail);
    }
    function removeCallback() {
        toastr.success('Removed');
    }
    return {
        open: open,
        remove: remove
    };
}(DataService);