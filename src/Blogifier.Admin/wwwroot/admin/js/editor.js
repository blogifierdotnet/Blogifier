const
  editorIcon_heading = `<svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" fill="currentColor" class="bi bi-h" viewBox="0 0 16 16" ><path d="M12 13V3H10.59V7.23443H5.40996V3H4V13H5.40996V8.42125H10.59V13H12Z"/></svg>`
  , editorIcon_bold = `<svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" fill="currentColor" class="bi bi-type-bold" viewBox="0 0 16 16"><path d="M8.21 13c2.106 0 3.412-1.087 3.412-2.823 0-1.306-.984-2.283-2.324-2.386v-.055a2.176 2.176 0 0 0 1.852-2.14c0-1.51-1.162-2.46-3.014-2.46H3.843V13H8.21zM5.908 4.674h1.696c.963 0 1.517.451 1.517 1.244 0 .834-.629 1.32-1.73 1.32H5.908V4.673zm0 6.788V8.598h1.73c1.217 0 1.88.492 1.88 1.415 0 .943-.643 1.449-1.832 1.449H5.907z"/></svg>`
  , editorIcon_italic = `<svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" fill="currentColor" class="bi bi-type-italic" viewBox="0 0 16 16"><path d="M7.991 11.674 9.53 4.455c.123-.595.246-.71 1.347-.807l.11-.52H7.211l-.11.52c1.06.096 1.128.212 1.005.807L6.57 11.674c-.123.595-.246.71-1.346.806l-.11.52h3.774l.11-.52c-1.06-.095-1.129-.211-1.006-.806z"/></svg>`
  , editorIcon_strikethrough = `<svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" fill="currentColor" class="bi bi-type-strikethrough" viewBox="0 0 16 16"><path d="M6.333 5.686c0 .31.083.581.27.814H5.166a2.776 2.776 0 0 1-.099-.76c0-1.627 1.436-2.768 3.48-2.768 1.969 0 3.39 1.175 3.445 2.85h-1.23c-.11-1.08-.964-1.743-2.25-1.743-1.23 0-2.18.602-2.18 1.607zm2.194 7.478c-2.153 0-3.589-1.107-3.705-2.81h1.23c.144 1.06 1.129 1.703 2.544 1.703 1.34 0 2.31-.705 2.31-1.675 0-.827-.547-1.374-1.914-1.675L8.046 8.5H1v-1h14v1h-3.504c.468.437.675.994.675 1.697 0 1.826-1.436 2.967-3.644 2.967z"/></svg>`
  , editorIcon_ol = `<svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" fill="currentColor" class="bi bi-list-ol" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M5 11.5a.5.5 0 0 1 .5-.5h9a.5.5 0 0 1 0 1h-9a.5.5 0 0 1-.5-.5zm0-4a.5.5 0 0 1 .5-.5h9a.5.5 0 0 1 0 1h-9a.5.5 0 0 1-.5-.5zm0-4a.5.5 0 0 1 .5-.5h9a.5.5 0 0 1 0 1h-9a.5.5 0 0 1-.5-.5z"/><path d="M1.713 11.865v-.474H2c.217 0 .363-.137.363-.317 0-.185-.158-.31-.361-.31-.223 0-.367.152-.373.31h-.59c.016-.467.373-.787.986-.787.588-.002.954.291.957.703a.595.595 0 0 1-.492.594v.033a.615.615 0 0 1 .569.631c.003.533-.502.8-1.051.8-.656 0-1-.37-1.008-.794h.582c.008.178.186.306.422.309.254 0 .424-.145.422-.35-.002-.195-.155-.348-.414-.348h-.3zm-.004-4.699h-.604v-.035c0-.408.295-.844.958-.844.583 0 .96.326.96.756 0 .389-.257.617-.476.848l-.537.572v.03h1.054V9H1.143v-.395l.957-.99c.138-.142.293-.304.293-.508 0-.18-.147-.32-.342-.32a.33.33 0 0 0-.342.338v.041zM2.564 5h-.635V2.924h-.031l-.598.42v-.567l.629-.443h.635V5z"/></svg>`
  , editorIcon_ul = `<svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" fill="currentColor" class="bi bi-list-ul" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M5 11.5a.5.5 0 0 1 .5-.5h9a.5.5 0 0 1 0 1h-9a.5.5 0 0 1-.5-.5zm0-4a.5.5 0 0 1 .5-.5h9a.5.5 0 0 1 0 1h-9a.5.5 0 0 1-.5-.5zm0-4a.5.5 0 0 1 .5-.5h9a.5.5 0 0 1 0 1h-9a.5.5 0 0 1-.5-.5zm-3 1a1 1 0 1 0 0-2 1 1 0 0 0 0 2zm0 4a1 1 0 1 0 0-2 1 1 0 0 0 0 2zm0 4a1 1 0 1 0 0-2 1 1 0 0 0 0 2z"/></svg>`
  , editorIcon_blockquote = `<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-blockquote-left" viewBox="0 0 16 16"><path fill-rule="evenodd" clip-rule="evenodd" d="M12.8976 9.4754C12.8294 9.25373 12.7114 9.0506 12.547 8.86687C12.3749 8.67487 12.1486 8.52629 11.8681 8.41886C11.5873 8.31229 11.2267 8.25829 10.7824 8.25829H10.1794C10.2514 7.47371 10.5157 6.806 10.973 6.254C11.4294 5.70371 12.0624 5.21943 12.8716 4.804L12.3808 4C11.3154 4.47971 10.4038 5.18143 9.6465 6.10829C8.88944 7.036 8.51243 7.95514 8.51243 8.86687C8.51243 9.84627 8.73106 10.6146 9.17188 11.1691C9.61269 11.7237 10.2441 12 11.0649 12C11.5857 12 12.0381 11.8163 12.423 11.4491C12.8073 11.0826 13 10.6531 13 10.1634C13 9.92713 12.9662 9.69713 12.8976 9.4754ZM7.38514 9.4754C7.31649 9.25373 7.19865 9.0506 7.03405 8.86687C6.86243 8.67487 6.63594 8.52629 6.35514 8.41886C6.07486 8.31229 5.71432 8.25829 5.26973 8.25829H4.66649C4.73865 7.47371 5.00271 6.806 5.45973 6.254C5.91649 5.70371 6.55001 5.21943 7.35865 4.804L6.86811 4C5.80244 4.47971 4.89136 5.18143 4.13379 6.10829C3.37676 7.036 3 7.95514 3 8.86687C3 9.84627 3.21811 10.6146 3.65919 11.1691C4.09946 11.7237 4.73082 12 5.55217 12C6.07271 12 6.52567 11.8163 6.91054 11.4491C7.29486 11.0826 7.48729 10.6531 7.48729 10.1634C7.48729 9.92713 7.45298 9.69713 7.38514 9.4754Z" /></svg>`
  , editorIcon_link = `<svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" fill="currentColor" class="bi bi-link-45deg" viewBox="0 0 16 16"><path d="M4.715 6.542 3.343 7.914a3 3 0 1 0 4.243 4.243l1.828-1.829A3 3 0 0 0 8.586 5.5L8 6.086a1.002 1.002 0 0 0-.154.199 2 2 0 0 1 .861 3.337L6.88 11.45a2 2 0 1 1-2.83-2.83l.793-.792a4.018 4.018 0 0 1-.128-1.287z"/><path d="M6.586 4.672A3 3 0 0 0 7.414 9.5l.775-.776a2 2 0 0 1-.896-3.346L9.12 3.55a2 2 0 1 1 2.83 2.83l-.793.792c.112.42.155.855.128 1.287l1.372-1.372a3 3 0 1 0-4.243-4.243L6.586 4.672z"/></svg>`
  , editorIcon_image = `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-image" viewBox="0 0 16 16"><path d="M6.002 5.5a1.5 1.5 0 1 1-3 0 1.5 1.5 0 0 1 3 0z"/><path d="M2.002 1a2 2 0 0 0-2 2v10a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V3a2 2 0 0 0-2-2h-12zm12 1a1 1 0 0 1 1 1v6.5l-3.777-1.947a.5.5 0 0 0-.577.093l-3.71 3.71-2.66-1.772a.5.5 0 0 0-.63.062L1.002 12V3a1 1 0 0 1 1-1h12z"/></svg>`
  , editorIcon_video = `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-play-btn" viewBox="0 0 16 16"><path d="M6.79 5.093C6.71524 5.03977 6.62726 5.00814 6.53572 5.00158C6.44418 4.99503 6.35259 5.01379 6.27101 5.05582C6.18942 5.09786 6.12098 5.16154 6.07317 5.23988C6.02537 5.31823 6.00006 5.40822 6 5.5V10.5C6.00006 10.5918 6.02537 10.6818 6.07317 10.7601C6.12098 10.8385 6.18942 10.9021 6.27101 10.9442C6.35259 10.9862 6.44418 11.005 6.53572 10.9984C6.62726 10.9919 6.71524 10.9602 6.79 10.907L10.29 8.407C10.3548 8.36074 10.4076 8.29968 10.4441 8.22889C10.4806 8.1581 10.4996 8.07962 10.4996 8C10.4996 7.92037 10.4806 7.8419 10.4441 7.7711C10.4076 7.70031 10.3548 7.63925 10.29 7.593L6.79 5.093V5.093Z" /><path d="M0 2C0 1.46957 0.210714 0.960859 0.585786 0.585786C0.960859 0.210714 1.46957 0 2 0H14C14.5304 0 15.0391 0.210714 15.4142 0.585786C15.7893 0.960859 16 1.46957 16 2V14C16 14.5304 15.7893 15.0391 15.4142 15.4142C15.0391 15.7893 14.5304 16 14 16H2C1.46957 16 0.960859 15.7893 0.585786 15.4142C0.210714 15.0391 0 14.5304 0 14V2ZM15 2C15 1.73478 14.8946 1.48043 14.7071 1.29289C14.5196 1.10536 14.2652 1 14 1H2C1.73478 1 1.48043 1.10536 1.29289 1.29289C1.10536 1.48043 1 1.73478 1 2V14C1 14.2652 1.10536 14.5196 1.29289 14.7071C1.48043 14.8946 1.73478 15 2 15H14C14.2652 15 14.5196 14.8946 14.7071 14.7071C14.8946 14.5196 15 14.2652 15 14V2Z" /></svg>`
  , editorIcon_table = `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-grid-3x3" viewBox="0 0 16 16"><path d="M0 1.5A1.5 1.5 0 0 1 1.5 0h13A1.5 1.5 0 0 1 16 1.5v13a1.5 1.5 0 0 1-1.5 1.5h-13A1.5 1.5 0 0 1 0 14.5v-13zM1.5 1a.5.5 0 0 0-.5.5V5h4V1H1.5zM5 6H1v4h4V6zm1 4h4V6H6v4zm-1 1H1v3.5a.5.5 0 0 0 .5.5H5v-4zm1 0v4h4v-4H6zm5 0v4h3.5a.5.5 0 0 0 .5-.5V11h-4zm0-1h4V6h-4v4zm0-5h4V1.5a.5.5 0 0 0-.5-.5H11v4zm-1 0V1H6v4h4z"/></svg>`
  , editorIcon_code = `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-code-square" viewBox="0 0 16 16"><path d="M14 1a1 1 0 0 1 1 1v12a1 1 0 0 1-1 1H2a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1h12zM2 0a2 2 0 0 0-2 2v12a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V2a2 2 0 0 0-2-2H2z"/><path d="M6.854 4.646a.5.5 0 0 1 0 .708L4.207 8l2.647 2.646a.5.5 0 0 1-.708.708l-3-3a.5.5 0 0 1 0-.708l3-3a.5.5 0 0 1 .708 0zm2.292 0a.5.5 0 0 0 0 .708L11.793 8l-2.647 2.646a.5.5 0 0 0 .708.708l3-3a.5.5 0 0 0 0-.708l-3-3a.5.5 0 0 0-.708 0z"/></svg>`
  , editorIcon_hr = `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-hr" viewBox="0 0 16 16"><path d="M1 8C1 7.44772 1.44772 7 2 7H14C14.5523 7 15 7.44772 15 8V8C15 8.55228 14.5523 9 14 9H2C1.44772 9 1 8.55228 1 8V8Z" /></svg>`
  , editorIcon_clear = `<svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" fill="currentColor" class="bi bi-eraser" viewBox="0 0 16 16"><path d="M8.086 2.207a2 2 0 0 1 2.828 0l3.879 3.879a2 2 0 0 1 0 2.828l-5.5 5.5A2 2 0 0 1 7.879 15H5.12a2 2 0 0 1-1.414-.586l-2.5-2.5a2 2 0 0 1 0-2.828l6.879-6.879zm2.121.707a1 1 0 0 0-1.414 0L4.16 7.547l5.293 5.293 4.633-4.633a1 1 0 0 0 0-1.414l-3.879-3.879zM8.746 13.547 3.453 8.254 1.914 9.793a1 1 0 0 0 0 1.414l2.5 2.5a1 1 0 0 0 .707.293H7.88a1 1 0 0 0 .707-.293l.16-.16z"/></svg>`
  , editorIcon_preview = `<svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" fill="currentColor" class="bi bi-eye" viewBox="0 0 16 16"><path d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8zM1.173 8a13.133 13.133 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13.133 13.133 0 0 1 14.828 8c-.058.087-.122.183-.195.288-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5c-2.12 0-3.879-1.168-5.168-2.457A13.134 13.134 0 0 1 1.172 8z"/><path d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5zM4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0z"/></svg>`
  , editorIcon_sidebyside = `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-layout-split" viewBox="0 0 16 16"><path d="M0 3a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v10a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V3zm8.5-1v12H14a1 1 0 0 0 1-1V3a1 1 0 0 0-1-1H8.5zm-1 0H2a1 1 0 0 0-1 1v10a1 1 0 0 0 1 1h5.5V2z"/></svg>`
  , editorIcon_fullscreen = `<svg xmlns="http://www.w3.org/2000/svg" width="14" height="14" fill="currentColor" class="bi bi-arrows-fullscreen" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M5.828 10.172a.5.5 0 0 0-.707 0l-4.096 4.096V11.5a.5.5 0 0 0-1 0v3.975a.5.5 0 0 0 .5.5H4.5a.5.5 0 0 0 0-1H1.732l4.096-4.096a.5.5 0 0 0 0-.707zm4.344 0a.5.5 0 0 1 .707 0l4.096 4.096V11.5a.5.5 0 1 1 1 0v3.975a.5.5 0 0 1-.5.5H11.5a.5.5 0 0 1 0-1h2.768l-4.096-4.096a.5.5 0 0 1 0-.707zm0-4.344a.5.5 0 0 0 .707 0l4.096-4.096V4.5a.5.5 0 1 0 1 0V.525a.5.5 0 0 0-.5-.5H11.5a.5.5 0 0 0 0 1h2.768l-4.096 4.096a.5.5 0 0 0 0 .707zm-4.344 0a.5.5 0 0 1-.707 0L1.025 1.732V4.5a.5.5 0 0 1-1 0V.525a.5.5 0 0 1 .5-.5H4.5a.5.5 0 0 1 0 1H1.732l4.096 4.096a.5.5 0 0 1 0 .707z"/></svg>`
  ;

const
  editorToolbar_heading = {
    name: "heading",
    action: EasyMDE.toggleHeadingSmaller,
    icon: editorIcon_heading,
    title: "Heading",
  },
  editorToolbar_bold = {
    name: "bold",
    action: EasyMDE.toggleBold,
    icon: editorIcon_bold,
    title: "Bold",
  },
  editorToolbar_italic = {
    name: "italic",
    action: EasyMDE.toggleItalic,
    icon: editorIcon_italic,
    title: "Italic",
  },
  editorToolbar_strike = {
    name: "strikethrough",
    action: EasyMDE.toggleStrikethrough,
    icon: editorIcon_strikethrough,
    title: "Strikethrough",
  },
  editorToolbar_ul = {
    name: "unordered-list",
    action: EasyMDE.toggleUnorderedList,
    icon: editorIcon_ul,
    title: "List",
  },
  editorToolbar_ol = {
    name: "ordered-list",
    action: EasyMDE.toggleOrderedList,
    icon: editorIcon_ol,
    title: "Numbered List",
  },
  editorToolbar_quote = {
    name: "quote",
    action: EasyMDE.toggleBlockquote,
    icon: editorIcon_blockquote,
    title: "Quote",
  },
  editorToolbar_link = {
    name: "link",
    action: EasyMDE.drawLink,
    icon: editorIcon_link,
    title: "Create Link",
  },
  editorToolbar_image = {
    name: "image",
    action: insertImage,
    icon: editorIcon_image,
    title: "Insert Image",
  },
  editorToolbar_video = {
    name: "insertYoutube",
    action: insertYoutube,
    icon: editorIcon_video,
    title: "Insert Video",
  },
  editorToolbar_table = {
    name: "table",
    action: EasyMDE.drawTable,
    icon: editorIcon_table,
    title: "Insert Table",
  },
  editorToolbar_code = {
    name: "code",
    action: EasyMDE.toggleCodeBlock,
    icon: editorIcon_code,
    title: "Insert Code",
  },
  editorToolbar_hr = {
    name: "horizontal-rule",
    action: EasyMDE.drawHorizontalRule,
    icon: editorIcon_hr,
    title: "Horizontal Line",
  },
  editorToolbar_clear = {
    name: "clean-block",
    action: EasyMDE.cleanBlock,
    icon: editorIcon_clear,
    title: "Clean block",
  },
  editorToolbar_preview = {
    name: "preview",
    action: EasyMDE.togglePreview,
    icon: editorIcon_preview,
    title: "Toggle Preview",
    noDisable: true,
  },
  editorToolbar_sidebyside = {
    name: "side-by-side",
    action: EasyMDE.toggleSideBySide,
    icon: editorIcon_sidebyside,
    title: "Toggle Side by Side",
    noDisable: true,
  },
  editorToolbar_fullscreen = {
    name: "fullscreen",
    action: EasyMDE.toggleFullScreen,
    icon: editorIcon_fullscreen,
    title: "Toggle Fullscreen",
    noDisable: true,
  };

function getEditor() {
  let bf_editor = document.getElementById('bf_editor');
  var easyMDE = new EasyMDE({
    element: bf_editor,
    autoDownloadFontAwesome: false,
    indentWithTabs: false,
    status: false,
    height: "500px",
    minHeight: "500px",
    parsingConfig: {
      allowAtxHeaderWithoutSpace: true,
      underscoresBreakWords: true
    },
    renderingConfig: {
      singleLineBreaks: false,
      codeSyntaxHighlighting: true
    },
    toolbar: [
      editorToolbar_heading,
      editorToolbar_bold,
      editorToolbar_italic,
      editorToolbar_strike,
      editorToolbar_ul,
      editorToolbar_ol,
      editorToolbar_hr,
      editorToolbar_link,
      editorToolbar_image,
      editorToolbar_video,
      editorToolbar_table,
      editorToolbar_code,
      editorToolbar_quote,
      editorToolbar_clear,
      editorToolbar_preview,
      editorToolbar_sidebyside,
      editorToolbar_fullscreen,

    ]
  });
  return easyMDE;
}

var _editor = {};

function insertImage(editor) {
  _editor = editor;
  fileManager.uploadClick('PostImage');
}

function insertYoutube(editor) {
  _editor = editor;
  var id = prompt("Please enter video ID", "");

  if (id !== null && id !== "") {
    var tag = '<iframe width="640" height="480" src="https://www.youtube.com/embed/' + id + '" frameborder="0" allowfullscreen></iframe>';
    var cm = _editor.codemirror;
    cm.replaceSelection(tag);
  }
}

// stick the toolbar to the top

function stickyToolbar() {
  var toolbar = document.querySelector(".editor-toolbar");
  var editor = document.querySelector(".editor-editor");


  var sticky = editor.offsetTop;

  if (window.pageYOffset > sticky) {
    toolbar.classList.add("-sticky");
  } else {
    toolbar.classList.remove("-sticky");
  }
}
