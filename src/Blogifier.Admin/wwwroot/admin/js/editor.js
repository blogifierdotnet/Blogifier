const
  editorIcon_heading = `<svg fill="currentColor" width="16" height="16" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg"><path d="M3 2H5V7.25L11 7.25V2H13V14H11V8.75L5 8.75V14H3V2Z" /></svg>`
  , editorIcon_bold = `<svg fill="currentColor" width="16" height="16" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" clip-rule="evenodd" d="M4 2H8.5C10.433 2 12 3.567 12 5.5C12 6.25657 11.7599 6.95707 11.3519 7.52949C12.3416 8.14781 13 9.24701 13 10.5C13 12.433 11.433 14 9.5 14H4V2ZM6 4H8.5C9.32843 4 10 4.67157 10 5.5C10 6.32843 9.32843 7 8.5 7H6V4ZM6 9V12H9.5C10.3284 12 11 11.3284 11 10.5C11 9.67157 10.3284 9 9.5 9H6Z" /></svg>`
  , editorIcon_italic = `<svg fill="currentColor" width="16" height="16" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg"><path d="M12 1V3H9.71429L8.28571 13H10V15H4V13H6.28571L7.71429 3H6V1H12Z" /></svg>`
  , editorIcon_strikethrough = `<svg fill="currentColor" width="16" height="16" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg"><path d="M10 5C10 5.12199 9.98324 5.24814 9.95017 5.375H11.9805C11.9934 5.25154 12 5.12646 12 5C12 2.79086 9.98528 1 7.5 1C5.01472 1 3 2.79086 3 5C3 5.43653 3.07867 5.85673 3.22409 6.25H5.56031C5.1926 5.87477 5 5.41353 5 5C5 4.11038 5.89133 3 7.5 3C9.10867 3 10 4.11038 10 5Z" /><path d="M7.375 9H0V7H16V9H11.398C11.7809 9.58835 12 10.2714 12 11C12 13.2091 9.98528 15 7.5 15C5.01472 15 3 13.2091 3 11C3 10.8735 3.0066 10.7485 3.01952 10.625H5.04983C5.01676 10.7519 5 10.878 5 11C5 11.8896 5.89133 13 7.5 13C9.10867 13 10 11.8896 10 11C10 10.1104 9.10867 9 7.5 9C7.45784 9 7.41617 9.00076 7.375 9.00226V9Z" /></svg>`
  , editorIcon_ol = `<svg fill="currentColor" width="16" height="16" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg"><path d="M2 7H3.5V0L0 1V2.5L2 2V7Z" /><path d="M0.141188 16H5V14.895H1.95229L3.56378 13.2398C4.00519 12.7687 4.31678 12.3561 4.49854 12.002C4.6803 11.6479 4.77118 11.3001 4.77118 10.9587C4.77118 10.3358 4.56832 9.85366 4.16261 9.5122C3.76014 9.17073 3.19377 9 2.46349 9C1.98637 9 1.55956 9.09959 1.18306 9.29878C0.806556 9.49481 0.514443 9.76671 0.306719 10.1145C0.10224 10.4623 0 10.8464 0 11.2669H1.41188C1.41188 10.9192 1.50276 10.6393 1.68452 10.4275C1.86952 10.2125 2.12106 10.105 2.43914 10.105C2.7345 10.105 2.9617 10.1935 3.12074 10.3706C3.27978 10.5445 3.3593 10.7848 3.3593 11.0915C3.3593 11.3159 3.28303 11.5531 3.13048 11.8028C2.98117 12.0526 2.74911 12.3451 2.43427 12.6802L0.141188 15.061V16Z" /><path d="M6 13C6 12.4477 6.44772 12 7 12H15C15.5523 12 16 12.4477 16 13C16 13.5523 15.5523 14 15 14H7C6.44772 14 6 13.5523 6 13Z" /><path d="M7 3C6.44772 3 6 3.44772 6 4C6 4.55228 6.44772 5 7 5H15C15.5523 5 16 4.55228 16 4C16 3.44772 15.5523 3 15 3H7Z" /></svg>`
  , editorIcon_ul = `<svg fill="currentColor" width="16" height="16" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" clip-rule="evenodd" d="M2 6C3.10458 6 4 5.10457 4 4C4 2.89543 3.10458 2 2 2C0.895416 2 0 2.89543 0 4C0 5.10457 0.895416 6 2 6ZM6 4C6 3.44772 6.44772 3 7 3H15C15.5523 3 16 3.44772 16 4C16 4.55228 15.5523 5 15 5H7C6.44772 5 6 4.55228 6 4ZM7 12C6.44772 12 6 12.4477 6 13C6 13.5523 6.44772 14 7 14H15C15.5523 14 16 13.5523 16 13C16 12.4477 15.5523 12 15 12H7ZM4 13C4 14.1046 3.10458 15 2 15C0.895416 15 0 14.1046 0 13C0 11.8954 0.895416 11 2 11C3.10458 11 4 11.8954 4 13Z" /></svg>`
  , editorIcon_blockquote = `<svg fill="currentColor" width="16" height="16" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" clip-rule="evenodd" d="M14.8656 9.84425C14.7761 9.56716 14.6212 9.31325 14.4054 9.08359C14.1796 8.84359 13.8825 8.65786 13.5144 8.52358C13.1458 8.39036 12.6725 8.32286 12.0894 8.32286H11.298C11.3925 7.34214 11.7394 6.5075 12.3396 5.8175C12.9386 5.12964 13.7694 4.52429 14.8315 4.005L14.1873 3C12.789 3.59964 11.5925 4.47679 10.5985 5.63536C9.60489 6.795 9.11006 7.94393 9.11006 9.08359C9.11006 10.3078 9.39702 11.2682 9.97559 11.9614C10.5542 12.6546 11.3829 13 12.4602 13C13.1437 13 13.7375 12.7704 14.2427 12.3114C14.7471 11.8532 15 11.3164 15 10.7042C15 10.4089 14.9556 10.1214 14.8656 9.84425ZM6.7555 9.84425C6.66539 9.56716 6.51073 9.31325 6.29469 9.08359C6.06944 8.84359 5.77217 8.65786 5.40362 8.52358C5.03575 8.39036 4.56255 8.32286 3.97902 8.32286H3.18727C3.28198 7.34214 3.62856 6.5075 4.2284 5.8175C4.82789 5.12964 5.65939 4.52429 6.72073 4.005L6.07689 3C4.6782 3.59964 3.48241 4.47679 2.4881 5.63536C1.4945 6.795 1 7.94393 1 9.08359C1 10.3078 1.28627 11.2682 1.86519 11.9614C2.44304 12.6546 3.2717 13 4.34972 13C5.03293 13 5.62744 12.7704 6.13258 12.3114C6.637 11.8532 6.88957 11.3164 6.88957 10.7042C6.88957 10.4089 6.84454 10.1214 6.7555 9.84425Z" /></svg>`
  , editorIcon_link = `<svg fill="currentColor" width="16" height="16" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg"><path d="M7.85724 2.17097L6 4L7 5L8.93201 3.24577C9.89935 2.27845 11.4719 2.27845 12.4393 3.24577C13.4066 4.21308 13.4066 5.78571 12.4393 6.75303L10.5141 8.75674L11.5141 9.75674L13.5141 7.82783C15.0754 6.26652 15.0754 3.73225 13.5141 2.17094C11.9528 0.609657 9.41852 0.609688 7.85724 2.17097Z" /><path d="M3.24575 12.4392C2.2784 11.4719 2.2784 9.89935 3.24575 8.93201L5 7L4 6L2.17098 7.85721C0.609703 9.41849 0.609642 11.9528 2.17098 13.514C3.73226 15.0753 6.26656 15.0753 7.82784 13.514L9.5141 11.7567L8.5141 10.7567L6.75301 12.4392C5.78573 13.4066 4.2131 13.4066 3.24575 12.4392Z" /><path d="M4.99941 9.55426L9.52486 5.02878L10.6563 6.16016L6.13076 10.6856L4.99941 9.55426Z" /></svg>`
  , editorIcon_image = `<svg fill="currentColor" width="16" height="16" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg"><path d="M5.5 7C6.32843 7 7 6.32843 7 5.5C7 4.67157 6.32843 4 5.5 4C4.67157 4 4 4.67157 4 5.5C4 6.32843 4.67157 7 5.5 7Z" /><path fill-rule="evenodd" clip-rule="evenodd" d="M0.43432 14.5556L0.43813 14.5619C0.96507 15.4243 1.91531 16 3 16H13C14.3941 16 15.566 15.0491 15.9027 13.7606C15.9662 13.5177 16 13.2628 16 13V3C16 1.34315 14.6569 0 13 0H3C1.34315 0 0 1.34315 0 3V13C0 13.5695 0.158712 14.102 0.43432 14.5556ZM13 1H3C1.89543 1 1 1.89543 1 3V12L4.07107 8.92892L6.64213 11.5L11.1421 7L15 10.8579V3C15 1.89543 14.1046 1 13 1ZM15 13V12.8579L11.1421 9L6.64213 13.5L4.07107 10.9289L1.17675 13.8232C1.49057 14.5172 2.1889 15 3 15H13C14.1046 15 15 14.1046 15 13Z" /></svg>`
  , editorIcon_video = `<svg fill="currentColor" width="16" height="16" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg"><path d="M7.5547 10.9635L10.7519 8.83205C11.3457 8.43623 11.3457 7.56377 10.7519 7.16795L7.5547 5.03647C6.89015 4.59343 6 5.06982 6 5.86852V10.1315C6 10.9302 6.89014 11.4066 7.5547 10.9635Z" /><path fill-rule="evenodd" clip-rule="evenodd" d="M16 8C16 12.4183 12.4183 16 8 16C3.58172 16 0 12.4183 0 8C0 3.58172 3.58172 0 8 0C12.4183 0 16 3.58172 16 8ZM15 8C15 11.866 11.866 15 8 15C4.13401 15 1 11.866 1 8C1 4.13401 4.13401 1 8 1C11.866 1 15 4.13401 15 8Z" /></svg>`
  , editorIcon_table = `<svg fill="currentColor" width="16" height="16" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" clip-rule="evenodd" d="M3 0C1.34315 0 0 1.34315 0 3V13C0 14.6569 1.34315 16 3 16H13C14.6569 16 16 14.6569 16 13V3C16 1.34315 14.6569 0 13 0H3ZM10 1H6V5L10 5V1ZM10 6L6 6V10L10 10V6ZM5 5V1H3C1.89543 1 1 1.89543 1 3L1 5H5ZM1 6H5V10H1L1 6ZM11 6H15V10H11V6ZM15 5V3C15 1.89543 14.1046 1 13 1H11V5H15ZM1 13V11H5V15H3C1.89543 15 1 14.1046 1 13ZM10 11L6 11V15H10V11ZM13 15H11V11H15V13C15 14.1046 14.1046 15 13 15Z" /></svg>`
  , editorIcon_code = `<svg fill="currentColor" width="16" height="16" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg"><path d="M5.88778 11.5543C6.07208 11.7081 6.17937 11.9411 6.17937 12.1874C6.17937 12.8802 5.40079 13.2539 4.89681 12.8031L0.30744 8.69762C-0.102481 8.33093 -0.10248 7.66907 0.307441 7.30238L4.89681 3.19691C5.40079 2.74607 6.17937 3.11981 6.17937 3.81257C6.17937 4.05889 6.07208 4.29186 5.88778 4.44574L2.49013 7.28252C2.04958 7.65035 2.04958 8.34965 2.49013 8.71748L5.88778 11.5543Z" /><path d="M9.82053 12.1876C9.82053 11.9413 9.92782 11.7083 10.1121 11.5544L13.5099 8.71748C13.9505 8.34965 13.9505 7.65035 13.5099 7.28252L10.1121 4.44559C9.92782 4.29171 9.82053 4.05874 9.82053 3.81242C9.82053 3.11966 10.5991 2.74593 11.1031 3.19678L15.6926 7.30238C16.1025 7.66908 16.1025 8.33092 15.6926 8.69762L11.1031 12.8032C10.5991 13.2541 9.82053 12.8803 9.82053 12.1876Z" /></svg>`
  , editorIcon_hr = `<svg fill="currentColor" width="16" height="16" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg"><g clip-path="url(#clip0)"><rect x="16" y="7" width="2" height="16" rx="1" transform="rotate(90 16 7)" /></g></svg>`
  , editorIcon_clear = `<svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" fill="currentColor" class="bi bi-eraser" viewBox="0 0 16 16"><path d="M8.086 2.207a2 2 0 0 1 2.828 0l3.879 3.879a2 2 0 0 1 0 2.828l-5.5 5.5A2 2 0 0 1 7.879 15H5.12a2 2 0 0 1-1.414-.586l-2.5-2.5a2 2 0 0 1 0-2.828l6.879-6.879zm2.121.707a1 1 0 0 0-1.414 0L4.16 7.547l5.293 5.293 4.633-4.633a1 1 0 0 0 0-1.414l-3.879-3.879zM8.746 13.547 3.453 8.254 1.914 9.793a1 1 0 0 0 0 1.414l2.5 2.5a1 1 0 0 0 .707.293H7.88a1 1 0 0 0 .707-.293l.16-.16z"/></svg>`
  , editorIcon_preview = `<svg fill="currentColor" width="16" height="16" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg"><path d="M12 11H4V12H12V11Z" /><path d="M4 8H12V9H4V8Z" /><path d="M9 5H4V6H9V5Z" /><path fill-rule="evenodd" clip-rule="evenodd" d="M0 3C0 1.34314 1.34314 0 3 0H13C14.6569 0 16 1.34314 16 3V13C16 14.6569 14.6569 16 13 16H3C1.34314 16 0 14.6569 0 13V3ZM3 1H13C14.1046 1 15 1.89542 15 3V13C15 14.1046 14.1046 15 13 15H3C1.89545 15 1 14.1046 1 13V3C1 1.89542 1.89545 1 3 1Z" /></svg>`
  , editorIcon_sidebyside = `<svg fill="currentColor" width="16" height="16" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" clip-rule="evenodd" d="M0 3C0 1.34315 1.34315 0 3 0H13C14.6569 0 16 1.34315 16 3V13C16 14.6569 14.6569 16 13 16H3C1.34315 16 0 14.6569 0 13V3ZM8.5 1H13C14.1046 1 15 1.89543 15 3V13C15 14.1046 14.1046 15 13 15H8.5V1ZM7.5 1H3C1.89543 1 1 1.89543 1 3V13C1 14.1046 1.89543 15 3 15H7.5V1Z" /></svg>`
  , editorIcon_fullscreen = `<svg fill="currentColor" width="16" height="16" viewBox="0 0 16 16" xmlns="http://www.w3.org/2000/svg"><path d="M11 1H13C14.1046 1 15 1.89543 15 3V5H16V3C16 1.34315 14.6569 0 13 0H11V1Z" /><path d="M5 1V0H3C1.34315 0 0 1.34315 0 3V5H1V3C1 1.89543 1.89543 1 3 1H5Z" /><path d="M1 11H0V13C0 14.6569 1.34315 16 3 16H5V15H3C1.89543 15 1 14.1046 1 13V11Z" /><path d="M11 15V16H13C14.6569 16 16 14.6569 16 13V11H15V13C15 14.1046 14.1046 15 13 15H11Z" /></svg>`
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
    className: "ms-auto"
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

const fullToolbar = [
  editorToolbar_heading,
  editorToolbar_bold,
  editorToolbar_strike,
  editorToolbar_italic,
  "|",
  editorToolbar_ol,
  editorToolbar_ul,
  "|",
  editorToolbar_quote,
  editorToolbar_code,
  editorToolbar_table,
  editorToolbar_hr,
  "|",
  editorToolbar_link,
  editorToolbar_image,
  editorToolbar_video,
  "|",
  editorToolbar_preview,
  editorToolbar_sidebyside,
  editorToolbar_fullscreen
];

const miniToolbar = [
  editorToolbar_heading,
  editorToolbar_bold,
  editorToolbar_strike,
  editorToolbar_italic,
  "|",
  editorToolbar_ol,
  editorToolbar_ul,
  "|",
  editorToolbar_hr,
  editorToolbar_link,
  editorToolbar_image,
  editorToolbar_video,
  "|",
  editorToolbar_preview,
];

function getEditor(_toolbar) {
  let selectedToolbar = fullToolbar;
  if (_toolbar == "miniToolbar") {
    selectedToolbar = miniToolbar;
  }

  let bf_editor = document.getElementById('bf_editor');
  let easyMDE = new EasyMDE({
    element: bf_editor,
    autoDownloadFontAwesome: false,
    indentWithTabs: false,
    status: false,
    height: "200px",
    minHeight: "200px",
    parsingConfig: {
      allowAtxHeaderWithoutSpace: true,
      underscoresBreakWords: true
    },
    renderingConfig: {
      singleLineBreaks: false,
      codeSyntaxHighlighting: true
    },
    toolbar: selectedToolbar,
    insertTexts: {
      horizontalRule: ["", "\n---\n"]
    }
  });
  return easyMDE;
}

let _editor = {};

// Image Upload
function insertImage(editor) {
  _editor = editor;
  fileManager.uploadClick('PostImage');
}

// TODO: insert video or embed not only YouTube.
function insertYoutube(editor) {
  _editor = editor;
  let id = prompt("Please enter video ID", "");

  if (id !== null && id !== "") {
    let tag = `<iframe width="700" height="400" src="https://www.youtube.com/embed/${id}" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>`;
    let cm = _editor.codemirror;
    cm.replaceSelection(tag);
  }
}

// Stick the toolbar to the top
function stickyToolbar(tool) {
  if (tool != "miniToolbar") {
    let body = document.querySelector("body");
    let editorWrapper = document.querySelector(".easymde-wrapper");
    let sticky = editorWrapper.offsetTop;
    if (window.pageYOffset > sticky) {
      body.classList.add("toolbar-sticky");
    } else {
      body.classList.remove("toolbar-sticky");
    }
  }
}

function editorToolbarTooltip() {
  console.log('x');
  let buttons = document.querySelectorAll('.editor-toolbar button');
  for (let i = 0; i < buttons.length; i++) {
    buttons[i].setAttribute('data-bs-toggle', 'tooltip');
    buttons[i].setAttribute('data-bs-placement', 'bottom');
  }
  // TODO: remove this later:
  commonJsFunctions.setTooltip();
}
