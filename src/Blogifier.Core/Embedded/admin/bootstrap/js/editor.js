tinymce.init({
    selector: '#txtContent',
    plugins: [
        "advlist autolink autoresize lists link image charmap print preview anchor",
        "searchreplace visualblocks code fullscreen textcolor imagetools",
        "insertdatetime media table contextmenu paste"
    ],
    toolbar: "styleselect | alignleft aligncenter alignright | bullist numlist | forecolor backcolor | link media | fullscreen code",
    autosave_ask_before_unload: false,
    autoresize_min_height: 400,
    fullscreen_new_window: true,
    menubar: false,
    relative_urls: false,
    browser_spellcheck: true,
    paste_data_images: true
});