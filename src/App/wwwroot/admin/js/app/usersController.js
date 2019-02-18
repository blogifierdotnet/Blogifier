var usersController = function (dataService) {

    function load(page) {
        dataService.get("api/authors?page=" + page, loadCallback, fail);
    }

    function loadCallback(data) {
        $('#usersList').empty();
        $.each(data, function (index) {
            var user = data[index];
            var img = webRoot + 'lib/img/avatar.jpg';
            if (user.avatar) {
                img = webRoot + user.avatar;
            }
            var profileLink = webRoot + 'admin/settings/profile?name=' + user.appUserName;

            tag = '<div class="post-grid-col">' +
                '	<div class="post-grid-item">' +
                '		<a class="item-link" style="background-image:url(' + img + ');"><div class="item-title mt-auto">&nbsp;</div></a>' +
                '		<div class="item-info d-flex align-items-center">' +
                '			<span class="item-date mr-auto">' + user.displayName + '</span>' +
                '			<a href="' + profileLink + '" class="btn-unstyled item-favorite ml-3" data-tooltip="" title="" data-original-title="profile">' +
                '				<i class="fas fa-external-link-square-alt"></i>' +
                '			</a>' +
                '		</div>' +
                '	</div>' +
                '</div>';

            $("#usersList").append(tag);
        });
    }

    function add() {
        var frm = $('#frmRegister');
        frm.validate();
        if (frm.valid() === false) {
            return false;
        }
        var isAdmin = $('input[name=cbxSetAdmin]:checked').val() === 'on' ? true : false;
        var obj = {
            userName: $('#txtUserName').val(),
            email: $('#txtEmail').val(),
            password: $('#txtPassword').val(),
            confirmPassword: $('#txtConfirmPassword').val(),
            setAsAdmin: isAdmin
        };
        dataService.post("api/authors", obj, addCallback, fail);
    }

    function addCallback(data) {
        toastr.success('Completed');
        $('#dlgRegister').modal('hide');
        load(1);
    }

    return {
        load: load,
        add: add
    };
}(DataService);
