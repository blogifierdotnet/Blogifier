$('.owl-carousel').owlCarousel({
    items: 1,
    loop: true,
    nav: true,
    navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>']
});
$(function () {
    var current = location.pathname;
    $('.navbar-nav .nav-link').each(function () {
        var $this = $(this);
        if ($this.attr('href') == current) {
            $this.addClass('active');
        }
    })
})