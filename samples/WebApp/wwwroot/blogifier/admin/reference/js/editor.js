tinymce.init({
    selector: '#txtContent',
    plugins: [
        "advlist autolink autoresize lists link image charmap print preview anchor",
        "searchreplace visualblocks code textcolor imagetools",
        "insertdatetime media table contextmenu paste"
    ],
    toolbar: "styleselect | alignleft aligncenter alignright | bullist numlist | forecolor backcolor | link media | code",
    autosave_ask_before_unload: false,
    autoresize_min_height: 320,
    menubar: false,
    relative_urls: false,
    browser_spellcheck: true,
    paste_data_images: true
});