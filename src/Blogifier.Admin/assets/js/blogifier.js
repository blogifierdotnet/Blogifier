
import "bootstrap/dist/js/bootstrap.bundle.min.js";
import "autosize/dist/autosize.min.js";
import "../lib/highlight.js"
import "../lib/Chart.min.js"

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

window.DataService = function () {
  let upload = function (url, obj, success, fail) {
    fetch(url, {
      method: 'POST',
      body: obj
    })
      .then(response => response.json())
      .then(data => success(data))
      .catch(error => fail(error));
  };
  return {
    upload: upload
  };
}();

window.fileManager = function (dataService) {
  let inputFile = document.getElementById('frmUploadFile');
  let frmUpload = document.getElementById('frmUpload');
  let callBack;
  let uplType;
  let postId;

  function uploadClick(uploadType, id) {
    uplType = uploadType;

    if (uploadType === 'AppCover') { callBack = appCoverCallback; }
    if (uploadType === 'PostImage') { callBack = insertImgCallback; }
    if (uploadType === 'PostCover') { callBack = postCoverCallback; postId = id; }
    if (uploadType === 'Avatar') { callBack = userAvatarCallback; }

    inputFile.click();
    return false;
  }

  function uploadSubmit() {
    let data = new FormData(frmUpload);
    let url = postId > 0 ? `api/storage/upload/${uplType}?postId=${postId}` : `api/storage/upload/${uplType}`;
    dataService.upload(url, data, callBack, fail);
  }

  function clipBoardUpload(ClipboardEvent) {
    let file = getPasteFile(ClipboardEvent);

    if (file === null)
      return

    uplType = "PostImage";
    let url = `api/storage/upload/${uplType}`;
    let data = new FormData();
    data.append('file', file, `${Date.now()}.png`);
    dataService.upload(url, data, (arg) => { insertImgCallback(arg) }, fail);
  }

  function getPasteFile(ClipboardEvent) {
    const data = ClipboardEvent.clipboardData;
    if (data == null) {
      return null;
    }

    if (data.items.length === 0) {
      return null;
    }

    const item = data.items[0];

    if (item.kind === 'string') {
      return null;
    }

    const file = item.getAsFile();

    if (file.type.indexOf("image") === -1)
      return null

    return file;
  }

  function appCoverCallback(data) {
    let defaultCover = document.getElementById('defaultCover');
    defaultCover.value = data;
  }

  function postCoverCallback(data) {
    let postCover = document.getElementById("postCover");
    postCover.src = data;
  }

  function userAvatarCallback(data) {
    let profilePicture = document.querySelectorAll('.profilePicture');
    for (i = 0; i < profilePicture.length; i++) {
      profilePicture[i].src = data;
    }
  }

  function insertImgCallback(data) {
    let cm = _editor.codemirror;
    let url = data;
    let output = url + '](' + url + ')';

    if (url.toLowerCase().match(/.(mp4|ogv|webm)$/i)) {
      let extv = 'mp4';
      if (url.toLowerCase().match(/.(ogv)$/i)) { extv = 'ogg'; }
      if (url.toLowerCase().match(/.(webm)$/i)) { extv = 'webm'; }
      output = `<video width="700" height="350" controls>\r\n\t\t<source src="${url}" type="video/${extv}">\r\n\t\tYour browser does not support the video tag.\r\n</video>`;
    }
    else if (url.toLowerCase().match(/.(mp3|ogg|wav)$/i)) {
      let exta = 'mp3';
      if (url.toLowerCase().match(/.(ogg)$/i)) { exta = 'ogg'; }
      if (url.toLowerCase().match(/.(wav)$/i)) { exta = 'wav'; }
      output = `<audio controls>\r\n\t\t<source src="${url}" type="audio/${exta}">\r\n\t\tYour browser does not support the audio tag.\r\n</audio>`;
    }
    else if (url.toLowerCase().match(/.(jpg|jpeg|png|gif)$/i)) {
      output = '\r\n![' + output;
    }
    else {
      output = '\r\n[' + output;
    }
    let selectedText = cm.getSelection();
    cm.replaceSelection(output);
  }

  function fail(data) {
    console.log(data.url);
  }

  return {
    uploadClick: uploadClick,
    uploadSubmit: uploadSubmit,
    clipBoardUpload: clipBoardUpload,
  };
}(DataService);
