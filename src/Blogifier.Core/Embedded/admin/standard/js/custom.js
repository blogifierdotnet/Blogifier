toastr.options.positionClass = 'toast-bottom-right';
toastr.options.backgroundpositionClass = 'toast-bottom-right';

function profileLogOut() {
    $("#frmLogOut").submit();
}

$(".app-side-toggle").click(function () {
    $(".app").toggleClass("toggled")
});
//
$(document).ready(function(){
  $('input').iCheck({
    checkboxClass: 'icheckbox_square',
    radioClass: 'iradio_square',
    increaseArea: '10%' // optional
  });
});
