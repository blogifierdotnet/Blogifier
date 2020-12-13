$('#blog-subscribe').on('click', function () {
	var email = $('#txtEmail').val();
	if (email) {
		subscribe("api/newsletter/subscribe", email);
	}
	return false;
});

var subscribe = function (url, email) {

	$.getJSON('http://gd.geobytes.com/GetCityDetails?callback=?', function (data) {
		var ip = data.geobytesremoteip || 'n/a';
		var country = data.geobytescountry || 'n/a';
		var region = data.geobytesregion || 'n/a';

		var obj = { Email: email, Ip: ip, Country: country, Region: region };
		var options = {
			url: "/" + url,
			type: "PUT",
			data: JSON.stringify(obj),
			contentType: "application/json",
			dataType: "html",
			success: done,
			error: done
		};
		$.ajax(options);
	});
}

$('#blog-unsubscribe').on('click', function () {
	var email = $('#txtEmail').val();
	if (email) {
		unsubscribe(email);
	}
	return false;
});

var unsubscribe = function (email) {

	var obj = { Email: email };
	var options = {
		url: "/api/newsletters/unsubscribe?email=" + email,
		type: "PUT",
		data: JSON.stringify(obj),
		contentType: "application/json",
		dataType: "html",
		success: done,
		error: done
	};
	$.ajax(options);
}

var done = function (data) {
	$('#frmNewsletter').slideUp();
	$('#ttlNewsletter').slideDown();
}