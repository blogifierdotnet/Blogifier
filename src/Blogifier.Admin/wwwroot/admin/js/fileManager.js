let fileManager = function (dataService) {
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
    uploadSubmit: uploadSubmit
  };
}(DataService);
