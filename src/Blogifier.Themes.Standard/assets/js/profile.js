
// copy input
function test(elm) {
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
