$(".app-side-toggle").click(function() {
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
