import { Tooltip } from 'bootstrap';

export function triggerClick(element) {
  element.click();
}

export function setTitle(title) {
  document.title = title + " - Blogifier";
}

export function setPageTitle() {

}

export function getFieldValue() {
  if (field.type === 'checkbox') {
    return document.getElementById(field.id).checked;
  }
  else {
    return document.getElementById(field.id).value;
  }
}

export function getTxtValue(txt) {
  return document.getElementById(txt).value;
}

export function focusElement(id) {
  setTimeout(function () {
    const element = document.getElementById(id);
    element.focus();
  }, 500);
}

export function writeCookie() {
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
}

export function setTooltip() {
  setTimeout(function () {
    let options = { "trigger": "hover", fallbackPlacements: ['bottom'] };
    let tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    let tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
      return new Tooltip(tooltipTriggerEl, options)
    });
  }, 1000);
}

export function startClock() {
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

export function getInputFileBlobInfo(inputElement) {
  const file = inputElement.files[0];
  const fileName = file.name;
  const url = URL.createObjectURL(file);
  console.log(url);
  return { fileName, url };
}
