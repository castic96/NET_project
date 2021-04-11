$(function () {
    var current = this.location.pathname;
    $('.nav-tabs li a').each(function () {
        var $this = $(this);
        if (current.toLowerCase().valueOf() == $this.attr('href').toLowerCase().valueOf()) {
            $this.addClass('active');
        }
    })
})
