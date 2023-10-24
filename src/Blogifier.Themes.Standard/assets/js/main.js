// get the newsletter form elements
const form = document.getElementById("newsletter");
const form_email = document.getElementById("newsletter_email");
const form_status = document.getElementById("newsletter_status");

// Success, Loading and Error functions
function successNewsletter(message) {
  form_status.innerHTML = `<div class="newsletter-msg bg-success"><div class="m-auto">${message}</div></div>`;
  setTimeout(() => {
    resetNewsletter();
  }, 2000);
}

function loadingNewsletter() {
  form_status.innerHTML = '<div class="newsletter-msg"><div class="m-auto spinner-border" role="status"></div></div>';
}

function errorNewsletter(message) {
  form_status.innerHTML = `<div class="newsletter-msg bg-danger"><div class="m-auto">${message}</div></div>`;
}

function resetNewsletter() {
  form.reset();
  form_status.innerHTML = "";
}

function subscribeNewsletter(url, data) {
  var options = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data)
  };

  fetch(url, options)
    .then((response) => {
      if (response.status === 200) {
        successNewsletter('Đăng ký nhận thông báo mới cho email này thành công!');
      } else if (response.status === 400) {
        errorNewsletter('Email này đã tồn tại trong danh sách đăng ký.');
      } else {
        errorNewsletter('Lỗi không xác định.');
      }
    })
    .catch((err) => {
      errorNewsletter(err.message);
    });
}

form.addEventListener("submit", function (e) {
  e.preventDefault();
  loadingNewsletter();
  var subscriber_data = {
    Email: form_email.value,
    Ip: "unknown",
    Country: "unknown",
    Region: "unknown"
  };

  subscribeNewsletter(form.action, subscriber_data);
});

// search modal auto focus
var myModal = document.getElementById('searchModal');
if (myModal) {
  myModal.addEventListener('shown.bs.modal', function () {
    document.getElementById('searchFormInput').focus()
  })
}