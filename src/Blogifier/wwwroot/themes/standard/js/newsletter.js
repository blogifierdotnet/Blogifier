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
  form_status.innerHTML = form_status.dataset.error;
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
        throw new Error('The Newsletter is not working!');
      }
    })
    .then(() => {
      form.reset()
      success();
    })
    .catch((err) => {
      error();
      console.error(err);
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
        throw new Error('Not sure where you are!');
      }
    })
    .then((loc) => {
      var subscriber_loc = loc.ip + '|' + loc.country_name + '|' + loc.region;
      var subscriber_data = { Email: form_email.value, Ip: subscriber_loc };
      subscribe(form.action, subscriber_data);
    })
    .catch((err) => {
      error();
      console.error(err);
    });
});
