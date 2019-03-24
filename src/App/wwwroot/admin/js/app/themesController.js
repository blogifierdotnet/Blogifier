var themesController = function (dataService) {

    function load(page) {
        dataService.get("api/themes?page=" + page, loadCallback, fail);
    }

    function loadCallback(data) {
        $('#themesList').empty();
        $.each(data, function (index) {
            var theme = data[index];
            var tag = '<div class="post-grid-col">' +
                '	<div class="post-grid-item">' +
                '		<a class="item-link" style="background-image:url(' + webRoot + theme.cover + ');"><div class="item-title mt-auto">&nbsp;</div></a>' +
                '		<div class="item-info d-flex align-items-center">' +
                '			<span class="item-date mr-auto">' + theme.title + '</span>';

            if (theme.isCurrent) {
                tag += '	' +
                    '		<i class="fas fa-star" style="color: #ffbe00; font-size: 1.3em"></i>' +
                    '	';
            }
            else {
                tag += '<button class="btn-unstyled item-favorite ml-3" onclick="themesController.select(\'' + theme.title + '\')" data-tooltip="" title="" data-original-title="select">' +
                    '	<i class="far fa-star"></i>' +
                    '</button>' +
                    '<a class="item-show ml-4" href="" onclick="themesController.remove(\'' + theme.title + '\'); return false" data-tooltip="" title="" data-original-title="delete">' +
                    '	<i class="fas fa-trash" style="color: #ff6666"></i>' +
                    '</a>';
            }

            tag += '</div></div></div>';
            $("#themesList").append(tag);
        });
    }

    function select(id) {
        dataService.put("api/themes/select/" + id, null, selectCallback, fail);
    }

    function selectCallback(data) {
        toastr.success(data);
        load(1);
    }

    function remove(id) {
        dataService.remove("api/themes/remove/" + id, removeCallback, fail);
    }

    function removeCallback(data) {
        toastr.success(data);
        load(1);
    }

    return {
        load: load,
        select: select,
        remove: remove
    };
}(DataService);
