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

var loginViewShowCounter = 0;

//Execute after the DOM finishes loading
$(
	function () {
	}
);


/*
    Description:
    Associated to the data-after-show attribute (Recurring execution) which executes afte the data-init attribute
*/
function afterLoginViewShow(e){
    // alert("login");
    if (loginViewShowCounter === 0) {
        $("#login-view .km-leftitem").click(function(){
            //console.log("hello");
            returnHome();
        });
        loginViewShowCounter = 1;
    }
}

/*

*/
function resetTextFields() {
    $("#email-input").val("");
    $("#password-input").val("");
    $("#confirm-password-input").val("");
}

/*
	Description:
	Callback function invoked when the Prompt confirmation dialog's buttons are pressed
	//TODO Send the inputted password to the FavesRUs web service to reset password
*/
function onForgotPasswordPrompt(results) {
    // results.buttonIndex = 1 for the first button, 2 for the second button, etc.
    // results.input1 = inputed text
    /*console.log(results);
    console.log("Inputted Email "+results.input1);*/
    webService("forgotPassword","email="+results.input1);
}

/*
    Description:
    Client side validation
    Validate the contents of the views in the respective input elements
        - Check if entries are blank, (Email) if '@' is present, (Password) if length is equal to or greater than 6
*/
function validateInputs(viewName, loginOption) {
    var status = false;
    if (viewName === "#login-view") {
        var el;
        for(var i = 0; i < $(APP.instance.view().id).find("input:visible").length; i++) {
            el = $(APP.instance.view().id).find($("input:visible")[i]);
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
            else if ($("#password-input").val() === $("#confirm-password-input").val()) {
                status = true;
            }
            else if (((el.attr("type") === "email") && (el.val() !== "")) || ( ( (el.attr("type") === "password") && (el.val() !== "") ) && (loginOption === "register") )) {
                console.log("check "+el.attr("type"));
                console.log((((el.attr("type") === "email") && (el.val() !== "")) || ( ( (el.attr("type") === "password") && (el.val() !== "") ) && (loginOption === "register") )));
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
                console.log("check "+el.attr("type")+"set to true");
                status = true;
            }        
        }
        console.log(loginOption);
    }
    console.log(status);
    return status;
}

function facebookAuthSuccess (response) {
    console.log("Facebook Login Successful");
    //alert("Result: " + JSON.stringify(response));
    //alert('Facebook login succeeded, got access token: ' + response.authResponse.accessToken);
    getFBInfo(response);
}

function facebookAuthFailure (response) {
    alert("Facebook Login failed");
}

/*
	Description:
	Invoke the Facebook or Email login process
*/
function login(option){
	if(option === "facebook"){ 
        /*facebook Connect Cordova plugin*/
        //facebookConnectPlugin.browserInit("1549506478654715")
        try {
            facebookConnectPlugin.login(["email","user_birthday"], facebookAuthSuccess, facebookAuthFailure);
        }
        catch(ex) {
            if (navigator.notification) {
                navigator.notification.alert(
                    "A connection to Facebook is unable to be established.",    // message
                    alertDismissed,                             // callback
                    'Unable to Connect via Facebook',                                  // title
                    'OK'                                        // buttonName
                );
            }
            else {alert("A connection to Facebook is unable to be established.");}
        }
	}
	else if((option === "register") && (validateInputs(APP.instance.view().id, "register"))) {
        webService("registerEmail","email=" + $("#email-input").val() + "&password=" + $("#confirm-password-input").val());
    }
    else if((option === "email") && (validateInputs(APP.instance.view().id, "signin"))) {
        webService("loginEmail","email=" + $("#email-input").val() + "&password=" + $("#password-input").val());
	}
}


/*
	Description:
	Add the appropriate eventhandlers based on the selected view
*/
function enableButtonTouchEventListeners(view) {
	if (view === "login"){

        //kendo.unbind("#facebook-login-btn");
        kendo.destroy("#facebook-login-btn");
        $("#facebook-login-btn").kendoTouch({
            touchstart: function (evt) {
                $("#facebook-login-btn").addClass("inner-shadow");
            },
            dragstart: function (evt) {
                $("#facebook-login-btn").removeClass("inner-shadow");
            },
            tap: function (evt) {
                $("#facebook-login-btn").removeClass("inner-shadow");  
                login("facebook");
            }
        });

        //kendo.unbind("#create-account-btn");
        kendo.destroy("#create-account-btn");
        $("#create-account-btn").kendoTouch({
            touchstart: function (evt) {
                $("#create-account-btn").addClass("inner-shadow");
            },
            dragstart: function (evt) {
                $("#create-account-btn").removeClass("inner-shadow");
            },
            tap: function (evt) {
                $("#create-account-btn").removeClass("inner-shadow");  
                resetTextFields();
                $("#login-fields-container ul li:last-child").removeClass("hidden");
                $("#forgot-password-link").addClass("hidden");
                $("#account-login-btn").text("Create Account");
            }
        });
        
        //kendo.unbind("#sign-in-btn");
        kendo.destroy("#sign-in-btn");
        $("#sign-in-btn").kendoTouch({
            touchstart: function (evt) {
                $("#sign-in-btn").addClass("inner-shadow");
            },
            dragstart: function (evt) {
                $("#sign-in-btn").removeClass("inner-shadow");
            },
            tap: function (evt) {
                $("#sign-in-btn").removeClass("inner-shadow");  
                resetTextFields();
                $("#login-fields-container ul li:last-child").addClass("hidden");
                $("#forgot-password-link").removeClass("hidden");
                $("#account-login-btn").text("Sign In");
            }
        });

        //kendo.unbind("#account-login-btn");
        kendo.destroy("#account-login-btn");
        $("#account-login-btn").kendoTouch({
            touchstart: function (evt) {
                $("#account-login-btn").addClass("inner-shadow");
            },
            dragstart: function (evt) {
                $("#account-login-btn").removeClass("inner-shadow");
            },
            tap: function (evt) {
                $("#account-login-btn").removeClass("inner-shadow");  
                resetTextFields();
                $("#login-fields-container ul li:last-child").addClass("hidden");
                $("#forgot-password-link").removeClass("hidden");
                $("#account-login-btn").text("Sign In");
            }
        });

        //kendo.unbind("#forgot-password-link");
        kendo.destroy("#forgot-password-link");
        $("#forgot-password-link").kendoTouch({
            tap: function (evt) {
                if (navigator.notification) {
                    navigator.notification.prompt(
                        "Please enter your account's email address to receive your temporary password!",  // message
                        onForgotPasswordPrompt,             // callback
                        'Password Reset',   // title
                        ["Send","Cancel"],  // buttonName
                        ""                  //Default test
                    );
                }
            }
        });
	}
}

/*
  Description: Return to two previous pages back
*/
function returnHome() {
    APP.instance.navigate("#home-view");
    APP.instance.view().footer.find(".km-tabstrip").data("kendoMobileTabStrip").switchTo("#home-view");
}

