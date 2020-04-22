var simplemde = {};

window.commonJsFunctions = {

    showPrompt: function (message) {
        return prompt(message, 'Type anything here');
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
        simplemde = getEditor();
    },
    setEditorValue: function (txt) {
        simplemde.value(txt
            .replace(/&#xA;/g, '\r\n')
            .replace(/&#xD;/g, '')
            .replace(/&lt;/g, '<')
            .replace(/&gt;/g, '>')
            .replace(/&quot;/g, '"'));
    },
    getEditorValue: function () {
        return simplemde.value();
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

function getEditor() {
    var simplemde = new SimpleMDE({
        toolbar: [
            "bold", "italic", "heading-2",
            "|", "quote", "unordered-list", "ordered-list",
            "|", "link", "code",
            {
                name: "insertImg",
                action: insertImage,
                className: "fa fa-picture-o",
                title: "Insert Image"
            },
            {
                name: "insertYoutube",
                action: insertYoutube,
                className: "fa fa-youtube",
                title: "Insert Youtube Video"
            },
            "|", "side-by-side", "fullscreen", "preview",
            "|", "guide"
        ],
        blockStyles: {
            bold: "__",
            italic: "_"
        },
        element: document.getElementById("mdEditor"),
        indentWithTabs: false,
        insertTexts: {
            horizontalRule: ["", "\n\n-----\n\n"],
            image: ["![](http://", ")"],
            link: ["[", "](#url#)"],
            table: ["", "\n\n| Column 1 | Column 2 | Column 3 |\n| -------- | -------- | -------- |\n| Text     | Text      | Text     |\n\n"]
        },
        lineWrapping: true,
        minHeight: "300px",
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
    return simplemde;
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