var newsletter = function (dataService) {
    function unsubscribe(email) {
        $.ajax({
            url: '/widgets/newsletter/unsubscribe',
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(email),
            success: function (data) {
                load(1);
                toastr.success('Removed');
            }
        });
        return false;
    }
    function load(page) {
        dataService.get('widgets/newsletter/load?page=' + page, loadCallback, fail);
        return false;
    }
    function loadCallback(data) {
        $('#lstEmails').empty();
        var emails = data.emails;
        $.each(emails, function (index) {
            var email = emails[index].email;
            var tag = '<li class="list-group-item d-flex justify-content-between align-items-center">' + email +
                '<a class="btn btn-sm btn-light btn-danger" href="" onclick="return newsletter.unsubscribe(\'' + email + '\')">' + 
                'Remove</a></li>';
            $("#lstEmails").append(tag);
        });
        loadPager(data.pager);
    }
    function loadPager(pg) {
        $('#email-pagination').empty();

        var last = pg.currentPage * pg.itemsPerPage;
        var first = pg.currentPage === 1 ? 1 : ((pg.currentPage - 1) * pg.itemsPerPage) + 1;
        if (last > pg.total) { last = pg.total; }

        var pager = "";

        if (pg.showOlder === true) {
            pager += '<button type="button" class="btn btn-sm btn-link" onclick="return newsletter.load(' + pg.older + ')"><i class="fa fa-chevron-left"></i></button>';
        }
        pager += '<span class="bf-filemanager-pagination">' + first + '-' + last + ' out of ' + pg.total + '</span>';
        if (pg.showNewer === true) {
            pager += '<button type="button" class="btn btn-sm btn-link" onclick="return newsletter.load(' + pg.newer + ')"><i class="fa fa-chevron-right"></i></button>';
        }
        $('#email-pagination').append(pager);
    }
    function fail(data) { }
    return {
        unsubscribe: unsubscribe,
        load: load
    };
}(DataService);