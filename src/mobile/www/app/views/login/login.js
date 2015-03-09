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
function resetTextFields(){

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
    Client side validation
    Validate the contents of the views in the respective input elements
        - Check if entries are blank, (Email) if '@' is present, (Password) if length is equal to or greater than 6
*/
function validateInputs(viewName) {
    var status = false;
    if (viewName === "#login-view") {
        var el;
        for(var i = 0; i < $(APP.instance.view().id).find("input:visible").length; i++) {
            el = $(APP.instance.view().id).find($("input")[i]);
            //Blank Entries
            if (el.val() === "") {
                if(el.attr("type") === "email") {
                    el.attr("placeholder", "Enter a valid email address!");
                }
                else if (el.attr("type") === "password") {
                    el.attr("placeholder", "Enter a valid password!");
                }
                el.addClass("error-placeholder");
                status=false;
            }
            else if (((el.attr("type") === "email") && (el.val() !== "")) || ( (el.attr("type") === "password") && (el.val() !== ""))) {
                //Check for '@' sign
                if(el.attr("type") === "email") {
                    if (el.val().indexOf("@") === -1) {
                        el.val("");
                        el.attr("placeholder", "Email: '@' sign required!");
                    }
                    else if ((el.val().indexOf("@") === 0) || (el.val().length < 10)) {
                        el.val("");
                        el.attr("placeholder", "Enter a valid email address!");
                    }
                }
                else if (el.attr("type") === "password") {
                    //Password not match
                    if($("#password-input").val() != $("#confirm-password-input").val()) {
                        $("#password-input").val("");
                        $("#password-input").attr("placeholder", "Password does not match!");
                        $("#confirm-password-input").val("");
                        $("#confirm-password-input").attr("placeholder", "Password does not match!");
                        $("#confirm-password-input").addClass("error-placeholder");
                        i = 10;
                    }
                    //Password less than 6 characters
                    else if (el.val().length < 6) {
                        el.val("");
                        el.attr("placeholder", "Password: > 5 characters required!");
                    }
                }
                el.addClass("error-placeholder");
                status = false;
            }
            else {
                status = true;
            }
        }
    }
    return status;
}


/*
	Description:
	Invoke the Facebook or Email login process
*/
function login(option){
	if(option === "facebook"){
		//Initiate Facebook connection session
        // Defaults to sessionStorage for storing the Facebook token
        // openFB.init({appId: '1549506478654715'});

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
	else if((option === "register") && (validateInputs(APP.instance.view().id))) {
        webService("registerEmail","email=" + $("#email-input").val() + "&password=" + $("#confirm-password-input").val());
    }
    else if((option === "email") && (validateInputs(APP.instance.view().id))) {
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
            	//alert($("#account-login-btn").text());
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
	}
}

/*
  Description: Return to two previous pages back
*/
function returnToPrevious() {
    APP.instance.navigate("app/views/home/home.html");
}
