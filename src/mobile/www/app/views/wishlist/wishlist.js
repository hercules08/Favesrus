define([
  'views/view',
  'text!views/wishlist/wishlist.html'
], function (View, html) {

	var categories = new kendo.data.DataSource({
	data: [
		/*{ name: 'Work' },
		{ name: 'Personal' },
		{ name: 'Other' }*/
	]
	});

	var model = {
		categories: categories,
		title: 'Title'
	};

	var view = new View('categories', html, model);

	$.subscribe('/newCategory/add', function (e, text) {
		categories.add({ name: text });
	});

});

//Execute after the DOM finishes loading
$(
	function () {
	}
);

/*
	Description: 
	Invoked when the 'data-after-show' event is triggered associated to the wishlist view
*/
function afterWishlistViewShow(e) {
	//Open Login Modal View if user hasn't sign in
	// if () {
		$("#login-modal").data("kendoMobileModalView").open();
		enableButtonTouchEventListeners("login");
	// }
}

/*
	Description:
	Add the appropriate eventhandlers based on the selected view
*/
function enableButtonTouchEventListeners(view) {
	if (view === "login"){
		//Add touchstart event to the create account button
        document.getElementById("facebook-login-btn").addEventListener("touchstart", 
            function (e) {
            	alert("Sign in With Facebook");
            	closeLoginModal();
            	e.preventDefault();
            }, 
        false);

        document.getElementById("create-account-btn").addEventListener("touchstart", 
            function (e) {
            	$("#login-fields-container ul li:last-child").removeClass("hidden");
            	$("#forgot-password-link").addClass("hidden");
            	$("#account-login-btn").text("Create Account");
            	e.preventDefault();
            }, 
        false);

        document.getElementById("sign-in-btn").addEventListener("touchstart", 
            function (e) {
            	$("#login-fields-container ul li:last-child").addClass("hidden");
            	$("#forgot-password-link").removeClass("hidden");
            	$("#account-login-btn").text("Sign In");
            	e.preventDefault();
            }, 
        false);

        document.getElementById("account-login-btn").addEventListener("touchstart", 
            function (e) {
            	alert($("#account-login-btn").text());
            	closeLoginModal();
            	e.preventDefault();
            }, 
        false);
	}
}

/*
  Description: Open Login Modal View if user hasn't sign in
*/
function closeLoginModal() {
 	$("#login-modal").kendoMobileModalView("close");
}