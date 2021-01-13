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
			$('[data-bs-toggle="tooltip"]').tooltip();
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

// Get the placeholder with _localizer from the hidden textarea in the PostEditorComponent.razor
var easymadePlaceholder = $("#mdEditor").attr('placeholder');



function getEditor() {
    function emdeicons(icon) {
        return $(".bf-emdet-icons " + icon).prop("outerHTML")
    }
    var easymde = new EasyMDE({
        autoDownloadFontAwesome: false,
        toolbar: [
            {
                name: "emdet-bold",
                action: EasyMDE.toggleBold,
                icon: emdeicons(".bi-type-bold"),
                title: "Bold",
            },
            {
                name: "emdet-italic",
                action: EasyMDE.toggleItalic,
                icon: emdeicons(".bi-type-italic"),
                title: "Italic",
            },
            {
                name: "emdet-strikethrough",
                action: EasyMDE.toggleStrikethrough,
                icon: emdeicons(".bi-type-strikethrough"),
                title: "strikethrough",
            },
            "|",
            {
                name: "emdet-H1",
                action: EasyMDE.toggleHeading1,
                icon: emdeicons(".bi-type-h1"),
                title: "H1",
            },
            {
                name: "emdet-H2",
                action: EasyMDE.toggleHeading2,
                icon: emdeicons(".bi-type-h2"),
                title: "H2",
            },
            {
                name: "emdet-H3",
                action: EasyMDE.toggleHeading3,
                icon: emdeicons(".bi-type-h3"),
                title: "H3",
            },
            "|",
            {
                name: "emdet-unordered-list",
                action: EasyMDE.toggleUnorderedList,
                icon: emdeicons(".bi-list-ul"),
                title: "Generic List",
            },
            {
                name: "emdet-ordered-list",
                action: EasyMDE.toggleOrderedList,
                icon: emdeicons(".bi-list-ol"),
                title: "Numbered List",
            },
            {
                name: "emdet-quote",
                action: EasyMDE.toggleBlockquote,
                icon: emdeicons(".bi-chat-left-quote"),
                title: "Quote",
            },
            {
                name: "emdet-horizontal-rule",
                action: EasyMDE.drawHorizontalRule,
                icon: emdeicons(".bi-hr"),
                title: "Insert Horizontal Line",
            },
            {
                name: "emdet-clean-block",
                action: EasyMDE.cleanBlock,
                icon: emdeicons(".bi-eraser"),
                title: "Clean block",
            },
            "|",
            {
                name: "emdet-link",
                action: EasyMDE.drawLink,
                icon: emdeicons(".bi-link-45deg"),
                title: "Create Link",
            },
            {
                name: "emdet-insertImg",
                action: insertImage,
                icon: emdeicons(".bi-card-image"),
                title: "Insert Image",
            },
            {
                name: "emdet-insertYoutube",
                action: insertYoutube,
                icon: emdeicons(".bi-play-btn"),
                title: "Insert Youtube Video"
            },
            {
                name: "emdet-code",
                action: EasyMDE.toggleCodeBlock,
                icon: emdeicons(".bi-code"),
                title: "Insert Code",
            },
            {
                name: "emdet-table",
                action: EasyMDE.drawTable,
                icon: emdeicons(".bi-table"),
                title: "Insert Table",
            },
            "|",
            {
                name: "emdet-side-by-side",
                action: EasyMDE.toggleSideBySide,
                icon: emdeicons(".bi-layout-split"),
                title: "Toggle Side by Side",
                noDisable: true,
                className: "ms-auto", // push to the right
            },
            {
                name: "emdet-fullscreen",
                action: EasyMDE.toggleFullScreen,
                icon: emdeicons(".bi-arrows-fullscreen"),
                title: "Toggle Fullscreen",
                noDisable: true,
            },
            {
                name: "preview",
                action: EasyMDE.togglePreview,
                icon: emdeicons(".bi-eye"),
                title: "Toggle Preview",
                noDisable: true,
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
        placeholder: easymadePlaceholder,
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

