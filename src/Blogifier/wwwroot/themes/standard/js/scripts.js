// search modal auto focus
var myModal = document.getElementById('searchModal')
var myInput = document.getElementById('searchFormInput')

myModal.addEventListener('shown.bs.modal', function () {
  myInput.focus()
})
