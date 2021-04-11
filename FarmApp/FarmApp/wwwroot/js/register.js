$(function () {

	$('#customer-form-link').click(function (e) {
		$("#customer-form").delay(100).fadeIn(100);
		$("#farmer-form").fadeOut(100);
		$('#farmer-form-link').removeClass('active');
		$(this).addClass('active');
		e.preventDefault();
	});
	$('#farmer-form-link').click(function (e) {
		$("#farmer-form").delay(100).fadeIn(100);
		$("#customer-form").fadeOut(100);
		$("#customer-form-link").removeClass('active');
		$(this).addClass('active');
		e.preventDefault();
	});

});
