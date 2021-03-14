// enable highlight
hljs.initHighlightingOnLoad();

// search modal auto focus
var myModal = document.getElementById('searchModal')
var myInput = document.getElementById('searchFormInput')
myModal.addEventListener('shown.bs.modal', function () {
  myInput.focus()
})

// copy input
function copyInput(elm) {
  var copyText = document.getElementById(elm);
  var copyTextStore = copyText.dataset.link;
  copyText.select();
  copyText.setSelectionRange(0, 99999);
  document.execCommand("copy");
  copyText.value = "Copied!";
  copyText.classList.add("copied");
  setTimeout(function () {
    copyText.value = copyTextStore;
    copyText.classList.remove("copied");
  }, 500);
}
