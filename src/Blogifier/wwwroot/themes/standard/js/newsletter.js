// get the newsletter form elements
const form = document.getElementById("newsletter");
const form_email = document.getElementById("newsletter_email");
const form_status = document.getElementById("newsletter_status");
const newsletterSucessMsg = form_status.dataset.success, newsletterErrorMsg = form_status.dataset.error;

// Success, Loading and Error functions
function successNewsletter() {
  form_status.innerHTML = `<div class="newsletter-msg bg-success"><div class="m-auto"> ${newsletterSucessMsg} </div></div>`;
  setTimeout(resetNewsletter, 2000);
}
function loadingNewsletter() {
  form_status.innerHTML = '<div class="newsletter-msg"><div class="m-auto spinner-border" role="status"></div></div>'
}
function errorNewsletter(msg) {
  form_status.innerHTML = `<div class="newsletter-msg bg-danger"><div class="m-auto">${newsletterErrorMsg} <br> ${msg}</div></div>`;
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
      successNewsletter();
    })
    .catch((err) => {
      errorNewsletter(err);
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
  fetch('https://ipapi.co/json/')
    .then((response) => {
      if (response.status == 200) {
        return response.json();
      } else {
        throw new Error('Not sure where you are!');
      }
    })
    .then((loc) => {
      subscriber_data.Ip = loc.ip;
      subscriber_data.Country = loc.country_name;
      subscriber_data.Region = loc.region;
      subscribeNewsletter(form.action, subscriber_data);
    })
    .catch((err) => {
      subscribeNewsletter(form.action, subscriber_data);
    });
});
