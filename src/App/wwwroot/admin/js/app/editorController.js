function getEditor(post) {
    var simplemde = new SimpleMDE({
        toolbar: [
            "bold", "italic", "heading-2", "|",
            "quote", "unordered-list", "ordered-list", "|",
            "link", "code",
            {
                name: "insertImg",
                action: openFileMgr,
                className: "fa fa-folder-open",
                title: "File Manager"
            },
            {
                name: "insertYoutube",
                action: insertYoutube,
                className: "fa fa-youtube",
                title: "Insert Youtube Video"
            },
            "|", "preview", "|", "guide"
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

    var txt = post.content ? post.content : '';

    simplemde.value(txt
        .replace(/&#xA;/g, '\r\n')
        .replace(/&#xD;/g, '')
        .replace(/&lt;/g, '<')
        .replace(/&gt;/g, '>')
        .replace(/&quot;/g, '"'));

    return simplemde;
}

var _editor = {};

function openFileMgr(editor) {
    _editor = editor;
    fileManagerController.open(insertImageCallback);
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

// Create the measurement node for scrollbar
var scrollDiv = document.createElement("div");
scrollDiv.className = "scrollbar-measure";
document.body.appendChild(scrollDiv);
var scrollbarWidth = scrollDiv.offsetWidth - scrollDiv.clientWidth;
document.body.removeChild(scrollDiv);