/*service.js*/

var temp_obj = null; //global temporary object
var temp_deviceID = null;

/*
	Description:
	Get the user's facebook profile picture
*/
function getFBInfo(response) {
		// facebookConnectPlugin.api(response.authResponse.userID+"/?fields=id,email,picture", ["email","user_birthday"],
		//alert("Request pic data from facebook");
		try {
			facebookConnectPlugin.api("me/?fields=id,name,picture,birthday,email,gender,first_name,last_name", ["email","user_birthday"],
			    function (result) {
			        console.log("FBInfo Result: " + JSON.stringify(result));
			        //Set the facebook profile pic
	                $("#profile-pic span").removeClass("km-profile-pic-holder");
	                $("#profile-pic span").append("<img src=''/>");
	                $("#profile-pic img").attr('src', result.picture.data.url);

					//temporaraly save the result 
					temp_obj = result;

					if (cordova) {
						temp_deviceID = device.uuid;
		                //Send User's facebook info to Faves R Us
	            		//Complete submission
	            		webService("loginFacebook","email=" + result.email + "&providerkey="+temp_deviceID+"&firstname="+result.first_name+"&lastname="+result.last_name+"&gender="+result.gender+"&birthday="+result.birthday+"&profilepic="+result.picture.data.url);
            		}
			    },
			    function (result) {
			        alert("Failed: " + result);
			    }
			);
		}
		catch (exception) {
			alert(exception);
		}
}

function alertDismissed() {
	
}

/*
	Description:
	Make the service calls to the FavesRUs server API
	requestString - identifier for the appropriate request
	content - JSON object with the relevant information for the request
*/
function webService(requestString, content) {
	var requestURL, requestType;

	if(requestString == "forgetPassword") {
		requestURL = ""; //TODO
		requestType = "POST";
	}

	else if(requestString == "registerEmail") {
		requestURL = "http://dev.favesrus.com/api/account/register";
		requestType = "POST";
	}

	else if(requestString == "loginEmail") {
		requestURL = "http://dev.favesrus.com/api/account/login";
		requestType = "POST";
	}
	else if(requestString == "loginFacebook") {
		requestURL = "http://dev.favesrus.com/api/account/loginfacebook";
		requestType = "POST";
	}

	//Make the AJAX call
	$.ajax({
		type: requestType,	//Specifies the type of request. (GET or POST)
		url: requestURL,	//Specifies the URL to send the request to. Default is the current page
		data: content		//Specifies data to be sent to the server
	})
	.done(function(data, status, xhr) {		//Replaces the success() method
	    alert( "success ");
	    if((requestString === "registerEmail") || (requestString === "loginEmail") || (requestString === "loginFacebook")){
	    	storeLocalcredentials();
	    	if(APP.instance.view().id === "#login-view") {
	    		APP.instance.navigate("app/views/wishlist/wishlist.html");
	    		alert("view the Wishlist View");
	    	}
	    }
	})
	.fail(function(data, status, xhr) {		//Replaces the error() method
	    //alert( "error " + response.message);
	    console.log(JSON.stringify(data)+" Status: "+JSON.stringify(status));
	    if (navigator.notification) {
		    navigator.notification.alert(
			    JSON.parse(data.responseText).Message,  	// message
			    alertDismissed,         					// callback
			    'Error',            						// title
			    'OK'                						// buttonName
			);
		}

		/*if(requestString === "registerFacebook"){
			//Attempt to login
			console.log("Attempt to log into your account with Facebook");
			webService("loginFacebook","email=" + temp_obj.email + "&providerkey="+temp_deviceID);
		}*/

	})
	.always(function(data, status, xhr) {	//Replaces the complete method
	    //alert( "complete " + response);
	});
}

/*TODO Local Storage - check for the user's credentials when the application is started */
function checkLocalCredentials() {
	if ((localStorage.email !== undefined) && (localStorage.password !== undefined) ) {
		//email & password against faves R us server
		alert("User credentials defined!");
	}
	else {
		alert("Email & password undefined");
	}
}


/*Local Storage - //Store username and accessToken or password locally for use later during relaunching of the app */
function storeLocalcredentials() {
	localStorage.email = temp_obj.email;
	localStorage.deviceID= temp_deviceID;
	localStorage.loginstatus = true;

	//Null the  global temp variables
	/*
	temp_obj.email = null;
	temp_deviceID = null;
	*/
}



