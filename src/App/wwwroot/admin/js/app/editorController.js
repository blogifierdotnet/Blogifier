var editorController = function (dataService) {

    function save(publish) {
        $('#PostItem_Content').val(simplemde.value());
        $('#PostItem_Status').val(publish === 'P' ? 2 : 1);
        $('#frmEditor').submit();
    }

    function publish() {
        $('#PostItem_Content').val(simplemde.value());
        $('#PostItem_Status').val(2);
        $('#frmEditor').submit();
    }

    function unpublish() {
        $('#PostItem_Content').val(simplemde.value());
        $('#PostItem_Status').val(3);
        $('#frmEditor').submit();
    }

    function remove() {
        $('.loading').fadeIn('fast');
        dataService.remove("admin/removepost/" + $('#PostItem_Id').val(), removeCallback, fail);
    }

    function removeCallback(data) {
        toastr.success('Deleted');
        setTimeout(function () {
            $('.loading').fadeOut('fast');
            window.location.href = webRoot + 'admin';
        }, 1000);
    }

    function loadCover() {
        var postId = $('#hdnPostId').val();
        var postImg = $('#hdnPostImg').val();
        $('#post-image').empty();
        if (!postImg.length > 0) {
            var btn = '<button type="button" title="Add Cover" class="btn btn-secondary btn-block" data-placement="bottom" onclick="return editorController.openFilePicker(' + postId + ');">Upload Post Cover</button >';
        }
        $('#post-image').append(btn);
        if (postImg.length > 0) {
            var dd = '<div class="admin-editor-cover-image"><img src="' + postImg + '" /></div>';
            dd += '<button type="button" class="btn btn-danger btn-block" onclick="return editorController.resetPostImage();">Remove Cover</button>';
        }
        $('#post-image').append(dd);
    }

    function loadButtons(id) {
        if (id > 0) {
            $('.act-upd').css('display', 'inline-block');
            $('.act-new').css('display', 'none');
        }
        else {
            $('.act-new').css('display', 'inline-block');
            $('.act-upd').css('display', 'none');
        }
    }

    function settings() {
        $('#postSettings').modal();
        return false;
    }

    return {
        save: save,
        publish: publish,
        unpublish: unpublish,
        remove: remove,
        loadCover: loadCover,
        loadButtons: loadButtons,
        settings: settings
    };
}(DataService);

function getEditor() {
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

    var txt = $('#PostItem_Content').val();

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
