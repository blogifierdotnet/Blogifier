
var postsController = function(dataService) {
  function publish() {
    loading();
    var items = $('.bf-posts-list input:checked');
    if (items.length == 0) {
      items = $('.bf-posts-grid input:checked');
    }
    for (i = 0; i < items.length; i++) {
      if (i + 1 < items.length) {
        dataService.put("blogifier/api/posts/publish/" + items[i].id, null, emptyCallback, fail);
      } else {
        dataService.put("blogifier/api/posts/publish/" + items[i].id, null, updateCallback, fail);
      }
    }
  }

  function unpublish() {
    loading();
    var items = $('.bf-posts-list input:checked');
    if (items.length == 0) {
      items = $('.bf-posts-grid input:checked');
    }
    for (i = 0; i < items.length; i++) {
      if (i + 1 < items.length) {
        dataService.put("blogifier/api/posts/unpublish/" + items[i].id, null, emptyCallback, fail);
      } else {
        dataService.put("blogifier/api/posts/unpublish/" + items[i].id, null, updateCallback, fail);
      }
    }
  }

  function featured(id, obj) {
    var i = $(obj.firstElementChild);

    if (i.hasClass("fa-star")) {
      dataService.put("blogifier/api/posts/featured/" + id + "?act=remove", null, removeFeatured(i), fail);
    } else {
      dataService.put("blogifier/api/posts/featured/" + id + "?act=add", null, addFeatured(i), fail);
    }
  }

  function addFeatured(i) {
    i.removeClass('fa-star-o').addClass('fa-star');
    toastr.success("Updated");
  }

  function removeFeatured(i) {
    i.removeClass('fa-star').addClass('fa-star-o');
    toastr.success("Updated");
  }

  function removePost() {
    loading();
    var items = $('.bf-posts-list input:checked');
    if (items.length == 0) {
      items = $('.bf-posts-grid input:checked');
    }
    for (i = 0; i < items.length; i++) {
      if (i + 1 < items.length) {
        dataService.remove('blogifier/api/posts/' + items[i].id, emptyCallback, fail);
      } else {
        dataService.remove('blogifier/api/posts/' + items[i].id, updateCallback, fail);
      }
    }
  }

  function emptyCallback() {}

  function updateCallback() {
    toastr.success('Updated');
    reload();
  }

  function loading() {
    $(actionBtns).attr('disabled', 'disabled');
    $('.loading').fadeIn();
  }

  function reload() {
    setTimeout(function() {
      window.location.href = webRoot + 'admin/content';
    }, 1000);
  }

  function filter() {
    var user = $('input[name=user-filter]:checked').val();
    if (!user) {
      user = "0";
    }
    var status = $('input[name=status-filter]:checked').val();
    var cats = $('input:checkbox:checked').map(function() {
      return this.value;
    }).get();

    var url = webRoot + "admin/content?user=" + user + "&status=" + status;
    if (cats.length > 0) {
      url = url + "&cats=" + cats;
    }
    window.location.href = url;
  }

  function fail(jqXHR, exception) {
    var msg = '';
    if (jqXHR.status === 0) {
      msg = 'Not connect.\n Verify Network.';
    } else if (jqXHR.status == 404) {
      msg = 'Requested page not found. [404]';
    } else if (jqXHR.status == 500) {
      msg = 'Internal Server Error [500].';
    } else if (exception === 'parsererror') {
      msg = 'Requested JSON parse failed.';
    } else if (exception === 'timeout') {
      msg = 'Time out error.';
    } else if (exception === 'abort') {
      msg = 'Ajax request aborted.';
    } else {
      msg = 'Uncaught Error.\n' + jqXHR.responseText;
    }
    toastr.error(msg);
  }

  function togglePostView(active) {
    var obj = {
      CustomKey: 'PostListStyle',
      CustomValue: 'grid'
    }
    if (active == "list") {
      $('#post-list-btn').addClass('active');
      $('#post-grid-btn').removeClass('active');

      $('.bf-posts-grid').removeClass('d-flex');
      $('.bf-posts-list').addClass('d-block');
      obj.CustomValue = 'list';
    } else {
      $('#post-grid-btn').addClass('active');
      $('#post-list-btn').removeClass('active');

      $('.bf-posts-list').removeClass('d-block');
      $('.bf-posts-grid').addClass('d-flex');
    }
    dataService.put("blogifier/api/profile/setcustomfield", obj, emptyCallback, fail);
  }

  function selectListSize(size) {
    var obj = {
      CustomKey: 'PostListSize',
      CustomValue: size
    }
    dataService.put("blogifier/api/profile/setcustomfield", obj, reload, fail);
  }

  return {
    publish: publish,
    unpublish: unpublish,
    removePost: removePost,
    filter: filter,
    featured: featured,
    togglePostView: togglePostView,
    selectListSize: selectListSize
  }
}(DataService);

// filters collapsable
$('.filter-group-title').on('click', function() {
  if ($(window).width() < 992) {
    $(this).toggleClass('active');
    $(this).parent().toggleClass('active');
  }
});
