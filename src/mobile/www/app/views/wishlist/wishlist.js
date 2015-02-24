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

var wishlistShowCounter = 0;

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
		if (wishlistShowCounter == 0){
			enableButtonTouchEventListeners("login");
			//Initiate openFB
			// Defaults to sessionStorage for storing the Facebook token
     		openFB.init({appId: '576057902525208'});
		}
		wishlistShowCounter = 1;
	// }
}

/*
	Description:
	Callback function invoked when the Prompt confirmation dialog's buttons are pressed
	//TODO Send the inputted password to the FavesRUs web service to reset password
*/
function onForgetPasswordPrompt(results) {
    // results.buttonIndex = 1 for the first button, 2 for the second button, etc.
    // results.input1 = inputed text
    
}

/*
	Description:
	Get the user's facebook profile
*/
function getFacebookProfilePic() {
	openFB.api({
            path: '/me',
            success: function(data) {
                //console.log(JSON.stringify(data));
                //Set the facebook profile pic
                //document.getElementById("userPic").src = 'http://graph.facebook.com/' + data.id + '/picture?type=small';
                //$("#profile-pic").attr("data-icon","");
                $("#profile-pic span").removeClass("km-profile-pic-holder");
                $("#profile-pic span").append("<img src=''/>");
                $("#profile-pic img").attr('src', 'http://graph.facebook.com/' + data.id + '/picture?type=small');
            },
            error: function errorHandler(error) {
        		alert(error.message);
    		}
    	});
}


/*
	Description:
	Invoke the Facebook or Email login process
*/
function login(option){
	if(option === "facebook"){
		openFB.login(
            function(response) {
                if(response.status === 'connected') {
                    alert('Facebook login succeeded, got access token: ' + response.authResponse.token);
                    closeLoginModal();
                    getFacebookProfilePic();
                } else {
                    alert('Facebook login failed: ' + response.error);
                }
            }, 
            {scope: 'email,user_birthday'}
        );
	}
	else if(option === "email") {

	}
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
            	//alert("Sign in With Facebook");
            	login("facebook");
            	//e.preventDefault(); //What is the Experience?
            }, 
        false);

        document.getElementById("create-account-btn").addEventListener("touchstart", 
            function (e) {
            	$("#login-fields-container ul li:last-child").removeClass("hidden");
            	$("#forgot-password-link").addClass("hidden");
            	$("#account-login-btn").text("Create Account");
            }, 
        false);

        document.getElementById("sign-in-btn").addEventListener("touchstart", 
            function (e) {
            	$("#login-fields-container ul li:last-child").addClass("hidden");
            	$("#forgot-password-link").removeClass("hidden");
            	$("#account-login-btn").text("Sign In");
            }, 
        false);

        document.getElementById("account-login-btn").addEventListener("touchstart", 
            function (e) {
            	alert($("#account-login-btn").text());
            	closeLoginModal();
            	e.preventDefault();
            }, 
        false);

        document.getElementById("forgot-password-link").addEventListener("touchstart", 
            function (e) {
            	if (navigator.notification) {
		            navigator.notification.prompt(
		                "Please enter your account's email address to receive your temporary password!",  // message
		                onForgetPasswordPrompt,         	// callback
		                'Password Reset',   // title
		                ["Send","Cancel"],  // buttonName
		            	""					//Default test
		            );
		        }
            	//e.preventDefault();
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