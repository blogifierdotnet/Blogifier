// get the newsletter form elements
var form = document.getElementById("newsletter");
var form_email = document.getElementById("newsletter_email");
var form_status = document.getElementById("newsletter_status");

// Success, Loading and Error functions
function success() {
  form.reset();
  form_status.classList.add("-show", "-success");
  form_status.innerHTML = form_status.dataset.success;
}
function loading() {
  form_status.classList.add("-show", "-loading");
}
function error(msg) {
  form_status.classList.add("-show", "-error");
  form_status.innerHTML = form_status.dataset.error + " " + msg;
}

// subscribe function
function subscribe(url, data) {
  var options = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(data)
  }
  fetch(url, options)
    .then((response) => {
      if (response.status == 200) {
        return response.json();
      } else {
        throw new Error('Something is wrong!');
      }
    })
    .then(() => {
      success();
    })
    .catch((err) => {
      error(err.message);
    });
}

// handle the form submission event
form.addEventListener("submit", function (e) {
  e.preventDefault();
  loading();
  fetch('https://ipapi.co/json/')
    .then((response) => {
      if (response.status == 200) {
        return response.json();
      } else {
        throw new Error('We can not get your location!');
      }
    })
    .then((loc) => {
      var subscriber_loc = loc.ip + '|' + loc.country_name + '|' + loc.region;
      var subscriber_data = { Email: form_email.value, Ip: subscriber_loc };
      subscribe(form.action, subscriber_data);
    })
    .catch((err) => {
      error(err.message);
    });
});
