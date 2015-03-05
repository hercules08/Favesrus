define([
  'views/view',
  'text!views/login/login.html'
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

*/
function clearTextFields(){

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
	Invoke the Facebook or Email login process
*/
function login(option){
	if(option === "facebook"){
		openFB.login(
            function(response) {
                if(response.status === 'connected') {
                    alert('Facebook login succeeded, got access token: ' + response.authResponse.token);
                    getFacebookProfilePic();
                    getFacebookInfo(response.authResponse.token);
                } else {
                    alert('Facebook login failed: ' + response.error);
                }
            }, 
            {scope: 'email,user_birthday'}
        );
	}
	else if(option === "register") {
        webService("registerEmail","email=" + $("#email-input").val() + "&password=" + $("#confirm-password-input").val());
    }
    else if(option === "email") {
        webService("loginEmail","email=" + $("#email-input").val() + "&password=" + $("#confirm-password-input").val());
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
            	if($("#account-login-btn").text() === "Create Account") {
                    login("register");
                }
                else if($("#account-login-btn").text() === "Sign In"){
                    login("email");
                }
            	//e.preventDefault();
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

        //Client side validation
        /*$("#email-input").bind("blur", function() {
            alert("loss focus");
            /*if((newRegExp("@").test($("#email-input").text())) === false){
                alert("this is not a vaild email address.");
            }
        });*/
        /*$("#login-fields-container").on('focusout',function() { //Not working
           console.log($(this).find( "li" ).length);
        });*/
         
        function myFunction() {
            alert("Input field lost focus.");
        }
          
        document.getElementById("email-input").addEventListener("focusout", myFunction);

        console.log($("#email-input"));
	}
}

/*
  Description: Return to two previous pages back
*/
function returnToPrevious() {
    APP.instance.navigate("app/views/home/home.html");
}
