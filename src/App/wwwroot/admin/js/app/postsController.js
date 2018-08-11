var postsController = function (dataService) {

  function publish(id) {
    dataService.put("admin/publishpost/" + id + "?flag=P", null, callback, fail);
  }

  function unpublish(id) {
    dataService.put("admin/publishpost/" + id + "?flag=U", null, callback, fail);
  }

  function feature(id) {
      dataService.put("admin/featurepost/" + id + "?flag=F", null, callback, fail);
  }

  function unfeature(id) {
      dataService.put("admin/featurepost/" + id + "?flag=U", null, callback, fail);
  }

  function remove(id) {
      dataService.remove("admin/removepost/" + id, callback, fail);
  }

  function callback() {
    toastr.success('Updated');
    setTimeout(function () {
        window.location.href = webRoot + 'admin';
    }, 1000);
  }

  function filter() {
    var status = $('input[name=status-filter]:checked').val();
    var url = webRoot + "admin/posts?status=" + status;
    window.location.href = url;
  }

  return {
    publish: publish,
    unpublish: unpublish,
    feature: feature,
    unfeature: unfeature,
    remove: remove,
    filter: filter
  };
}(DataService);

// filters collapsable
$('.filter-group-title').on('click', function() {
  if ($(window).width() < 992) {
    $(this).toggleClass('active');
    $(this).parent().toggleClass('active');
  }
});