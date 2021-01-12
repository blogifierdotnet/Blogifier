var easymde = {};

window.commonJsFunctions = {

	showPrompt: function (message) {
		return prompt(message, 'Type anything here');
	},
	hideLoader: function (id) {
		var el = document.getElementById(id);
		el.style.display = 'none';
	},
	getFieldValue: function (field) {
		if (field.type === 'checkbox') {
			return document.getElementById(field.id).checked ;
		}
		else {
			return document.getElementById(field.id).value;
		}
	},
	focusElement: function (id) {
		setTimeout(function () {
			const element = document.getElementById(id);
			element.focus();
		}, 500);
   },
   replaceElement: function (id, success) {
      var el = document.getElementById(id);
      el.style.display = 'none';

      if (success) {
         var el2 = document.getElementById('s-' + id);
         el2.style.display = 'block';
      }
      else {
         var el3 = document.getElementById('f-' + id);
         el3.style.display = 'block';
      }
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
		$(document).ready(function () {
			$('[data-toggle="tooltip"]').tooltip();
		});
	},
	loadEditor: function () {
		easymde = getEditor();
	},
	setEditorValue: function (txt) {
		easymde.value(txt
			.replace(/&#xA;/g, '\r\n')
			.replace(/&#xD;/g, '')
			.replace(/&lt;/g, '<')
			.replace(/&gt;/g, '>')
			.replace(/&quot;/g, '"'));
	},
	getEditorValue: function () {
		return easymde.value();
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

var easymdeHeight = "450px";
if (window.screen.height < 769) {
    easymdeHeight = "300px";
}

function getEditor() {
    var easymde = new EasyMDE({
        autoDownloadFontAwesome: false,
        toolbar: [
            {
                name: "bold",
                action: EasyMDE.toggleBold,
                icon: easymde_icon_bold,
                title: "Bold",
            },
            {
                name: "italic",
                action: EasyMDE.toggleItalic,
                icon: easymde_icon_italic,
                title: "Italic",
            },
            {
                name: "strikethrough",
                action: EasyMDE.toggleStrikethrough,
                icon: easymde_icon_strikethrough,
                title: "strikethrough",
            },
            "|",
            {
                name: "H1",
                action: EasyMDE.toggleHeading1,
                icon: easymde_icon_h1,
                title: "H1",
            },
            {
                name: "H2",
                action: EasyMDE.toggleHeading2,
                icon: easymde_icon_h2,
                title: "H2",
            },
            {
                name: "H3",
                action: EasyMDE.toggleHeading3,
                icon: easymde_icon_h3,
                title: "H3",
            },

            "|",

            {
                name: "code",
                action: EasyMDE.toggleCodeBlock,
                icon: easymde_icon_code,
                title: "Insert Code",
            },
            {
                name: "quote",
                action: EasyMDE.toggleBlockquote,
                icon: easymde_icon_quote,
                title: "Quote",
            },
            {
                name: "unordered-list",
                action: EasyMDE.toggleUnorderedList,
                icon: easymde_icon_UnorderedList,
                title: "Generic List",
            },
            {
                name: "ordered-list",
                action: EasyMDE.toggleOrderedList,
                icon: easymde_icon_OrderedList,
                title: "Numbered List",
            },
            {
                name: "clean-block",
                action: EasyMDE.cleanBlock,
                icon: easymde_icon_cleanBlock,
                title: "Clean block",
            },
            {
                name: "link",
                action: EasyMDE.drawLink,
                icon: easymde_icon_drawLink,
                title: "Create Link",
            },
            {
                name: "table",
                action: EasyMDE.drawTable,
                icon: easymde_icon_drawTable,
                title: "Insert Table",
            },
            {
                name: "horizontal-rule",
                action: EasyMDE.drawHorizontalRule,
                icon: easymde_icon_drawHorizontalRule,
                title: "Insert Horizontal Line",
            },

            {
                name: "preview",
                action: EasyMDE.togglePreview,
                icon: easymde_icon_togglePreview,
                title: "Toggle Preview",
            },

            {
                name: "side-by-side",
                action: EasyMDE.toggleSideBySide,
                icon: easymde_icon_toggleSideBySide,
                title: "Toggle Side by Side",
            },

            {
                name: "fullscreen",
                action: EasyMDE.toggleFullScreen,
                icon: easymde_icon_toggleFullScreen,
                title: "Toggle Fullscreen",
            },

            "|",
            {
                name: "insertImg",
                action: insertImage,
                icon: easymde_icon_insertImage,
                title: "Insert Image",
            },
            {
                name: "insertYoutube",
                action: insertYoutube,
                icon: easymde_icon_insertYoutube,
                title: "Insert Youtube Video"
            }
        ],
        element: document.getElementById("mdEditor"),
        indentWithTabs: false,
        minHeight: easymdeHeight,
		parsingConfig: {
			allowAtxHeaderWithoutSpace: true,
			strikethrough: false,
			underscoresBreakWords: true
		},
		placeholder: "Type here...",
		promptURLs: true,
		renderingConfig: {
			singleLineBreaks: false,
			codeSyntaxHighlighting: true
		},
		shortcuts: {
			drawTable: "Cmd-Alt-T"
		},
		spellChecker: true,
		status: ["lines", "words"],
		styleSelectedText: false,
		syncSideBySidePreviewScroll: false
	});
	return easymde;
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
		var tag = '<iframe width="640" height="480" src="http://www.youtube.com/embed/' + id + '" frameborder="0" allowfullscreen></iframe>';
		var cm = _editor.codemirror;
		cm.replaceSelection(tag);
	}
}

// Select bootstrap tab anywhere.
function jumpTab(d) {
    var selectJumpTab = new bootstrap.Tab(document.querySelector(d))
    selectJumpTab.show() 
}

