var easymde = {};

window.commonJsFunctions = {
  setTitle: function (title) {
    document.title = title + " - Blogifier";
  },
  setPageTitle: function () {

  },
  showPrompt: function (message) {
    return prompt(message, 'Type anything here');
  },
  hideLoader: function (id) {
    var el = document.getElementById(id);
    el.style.display = 'none';
  },
  getFieldValue: function (field) {
    if (field.type === 'checkbox') {
      return document.getElementById(field.id).checked;
    }
    else {
      return document.getElementById(field.id).value;
    }
  },
  getTxtValue: function (txt) {
    return document.getElementById(txt).value;
  },
  getSrcValue: function (src) {
    return document.getElementById(src).src;
  },
  focusElement: function (id) {
    setTimeout(function () {
      const element = document.getElementById(id);
      element.focus();
    }, 500);
  },
  writeCookie: function (name, value, days) {

    var expires;
    if (days) {
      var date = new Date();
      date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
      expires = "; expires=" + date.toGMTString();
    }
    else {
      expires = "";
    }
    document.cookie = name + "=" + value + expires + "; path=/";
  },
  setTooltip: function (args) {
    setTimeout(function () {
      let options = { "trigger": "hover", fallbackPlacements: ['bottom'] };
      let tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
      let tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl, options)
      });
    }, 1000);
  },
  showModal: function (id) {
    document.getElementById(id).showModal();
  },
  startClock: function () {
    var clock = document.getElementById('clock');
    var clockDay = document.getElementById('clock-day');
    var clockMonth = document.getElementById('clock-month');

    function time() {
      var date = new Date();
      var hours = date.getHours();
      var minutes = date.getMinutes();
      hours = hours % 12;
      hours = hours ? hours : 12;
      minutes = minutes < 10 ? '0' + minutes : minutes;
      clock.textContent = hours + ':' + minutes;

      var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
      var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

      clockDay.textContent = days[date.getDay()];
      clockMonth.textContent = months[date.getMonth()] + ' ' + date.getDate();
    }
    time();
    setInterval(time, 60 * 1000);
  }
};
