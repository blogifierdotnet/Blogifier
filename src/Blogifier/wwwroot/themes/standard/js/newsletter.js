window.addEventListener("DOMContentLoaded", function () {

  // get the form elements defined in your form HTML above

  var form = document.getElementById("newsletter");
  var form_body = document.getElementById("newsletter");
  var form_email = document.getElementById("newsletter_email");
  var form_status = document.getElementById("newsletter_status");

  // Success and Error functions for after the form is submitted

  function success() {
    form.reset();
    form_status.classList.add("-show", "-success");
    form_status.innerHTML = "Thanks!";
  }

  function loading(){
    form_status.classList.add("-show", "-loading");
    form_status.innerHTML = "Loading...";
  }

  function error() {
    form_status.classList.add("-show", "-error");
    form_status.innerHTML = "Oops! There was a problem.";
  }

  // handle the form submission event

  form.addEventListener("submit", function (ev) {
    ev.preventDefault();
    loading();
    fetch('https://ipapi.co/json/')
      .then(function(response) {
        return response.json();
      })
      .then(function(data) {
        var info = data.ip + '|' + data.country_name + '|' + data.region;
        var obj = { Email: form_email.value, Ip: info };
        var data = JSON.stringify(obj);
        ajax(form.method, form.action, data, success, error);
      });
  });
});

// helper function for sending an AJAX request

function ajax(method, url, data, success, error) {
  var xhr = new XMLHttpRequest();
  xhr.open(method, url);
  xhr.setRequestHeader("Accept", "application/json");
  xhr.onreadystatechange = function () {
    if (xhr.readyState !== XMLHttpRequest.DONE) return;
    if (xhr.status === 200) {
      success(xhr.response, xhr.responseType);
    } else {
      error(xhr.status, xhr.response, xhr.responseType);
    }
  };
  xhr.send(data);
}
