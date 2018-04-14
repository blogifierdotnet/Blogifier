var authorController = function (dataService) {

    var create = function () {
        $('#frmCreate').validate().resetForm();
        $('#modCreate').modal();
    }

    var save = function () {
        $('#frmCreate').validate();

        if ($('#frmCreate').valid()) {
            var author = {
                UserName: $('#txtUserName').val(),
                Email: $('#txtEmail').val(),
                Password: $('#txtPwd').val()
            }
            dataService.post("api/author", author, onSave, fail);
        }      
    }

    var onSave = function (data) {
        $('#modCreate').modal('hide');
        toastr.success('saved');
        setTimeout(function () {
            window.location.href = getUrl('settings/users');
        }, 1000);
    }

    var open = function (id) {
        dataService.get("api/author/" + id, onOpen, fail);
        return false;
    }

    var onOpen = function (data) {
        $('#authorTitle').html(data.displayName);
        $('#userName').html(data.userName);
        $('#userEmail').html(data.email);
        $('#userId').html(data.id);
        $('#userCreated').html(getDate(data.created));
        if (data.isAdmin == true) {
            $('#footerBtns').hide();
        }
        else {
            $('#footerBtns').show();
        }
        $('#modAuthor').modal();
    }

    var remove = function () {
        var id = $('#userId').html();
        var result = confirm("Delete this user?");
        if (result) {
            dataService.remove("api/author/" + id, onRemove, fail);           
        }
    }

    var onRemove = function (data) {
        toastr.success('Removed');
        $('#authorModal').modal('hide');
        setTimeout(function () {
            window.location.href = getUrl('settings/users');
        }, 1000);
    }

    $("#frmCreate").validate({
        rules: {
            txtEmail: { required: true, email: true },
            txtConfirmPwd: { equalTo: "#txtPwd" }
        }
    });

    return {
        create: create,
        open: open,
        save: save,
        remove: remove
  };
}(DataService);