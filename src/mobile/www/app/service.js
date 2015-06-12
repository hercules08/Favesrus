/*service.js*/

var temp_obj = null; //global temporary object
var temp_deviceID = null;

/*Global webServiceResponse*/
var webServiceResponse = "";

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
					temp_deviceID = getDeviceID();
	                //Send User's facebook info to Faves R Us
            		//Complete submission
            		webService("loginFacebook","email=" + result.email + "&providerkey="+temp_deviceID+"&firstname="+result.first_name+"&lastname="+result.last_name+"&gender="+result.gender+"&birthday="+result.birthday+"&profilepic="+result.picture.data.url);
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

/*
	Description:
*/

function getDeviceID () {
	if (cordova) {
		return device.uuid;
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
	var requestURL, requestType,
		domainName = "http://dev.favesrus.com/", contentType = "application/x-www-form-urlencoded; charset=UTF-8";

	if(requestString == "forgotPassword") {
		requestURL = domainName + "api/account/forgotpassword"; 
		requestType = "POST";
	}

	else if(requestString == "registerEmail") {
		requestURL = domainName + "api/account/register";
		requestType = "POST";
	}

	else if(requestString == "loginEmail") {
		requestURL = domainName + "api/account/login";
		requestType = "POST";
	}
	else if(requestString == "loginFacebook") {
		requestURL = domainName + "api/account/loginfacebook";
		requestType = "POST";
	}
	else if(requestString == "logout") {
		requestURL = domainName + "api/account/logout";
		requestType = "POST";
	}
	else if(requestString == "getrecommendations") {
		requestURL = domainName + "api/recommendation/getrecommendations";
		requestType = "GET";
	}
	else if(requestString == "gettotlist") {
		requestURL = domainName + "api/giftitem/gettotlist";
		requestType = "POST";
		contentType = "application/json";
	}
	else if(requestString == "getgiftitemswithterm") {
		requestURL = domainName + "api/giftitem/"+requestString;
		requestType = "GET";
		// contentType = "application/json";
	}
	else if(requestString == "addgiftitemtowishlist") {
		requestURL = domainName + "api/wishlist/"+requestString;
		requestType = "POST";
		contentType = "application/json";
	}
	else if(requestString == "removegiftitemfromwishlist") {
		requestURL = domainName + "api/wishlist/"+requestString;
		requestType = "POST";
		contentType = "application/json";
	}

	//console.log("Data sent "+content);

	//Make the AJAX call
	$.ajax({
		type: requestType,	//Specifies the type of request. (GET or POST)
		url: requestURL,	//Specifies the URL to send the request to. Default is the current page
		data: content,		//Specifies data to be sent to the server
		contentType: contentType	//Specifies the contentType (by default - )
	})
	.done(function(data, status, xhr) {		//Replaces the success() method
	    // alert( JSON.stringify(data)+" Status: "+JSON.stringify(status));
	    if (navigator.notification) {
		    /*navigator.notification.alert(
			    JSON.stringify(data)+" Status: "+JSON.stringify(status),  	// message
			    alertDismissed,         					// callback
			    'Success',            						// title
			    'OK'                						// buttonName
			);*/
		}

		else {
			//alert(JSON.stringify(data)+" Status: "+JSON.stringify(status));
		}


	    if((requestString === "registerEmail") || (requestString === "loginEmail") || (requestString === "loginFacebook")){
	    	storeLocalcredentials(content);
	    	if(APP.instance.view().id === "#login-view") {
	    		APP.instance.navigate("#wishlist-view");
	    		alert("view the Wishlist View");
	    	}
	    }
	    else if (requestString === "gettotlist"){
	    	setTortScrollViewData(data);
	    }
	    else if(requestString === "getgiftitemswithterm"){
	    	setHomeViewProductData(data);
	    }
	})
	.fail(function(xhr, status, error) {		//Replaces the error() method
	    console.log(xhr.responseText);
	    //alert(JSON.stringify(data)+" Status: "+JSON.stringify(status));
	    if (navigator.notification) {
		    navigator.notification.alert(
			    JSON.stringify(xhr.model.entity.reason),  	// message
			    alertDismissed,         					// callback
			    'Error',            						// title
			    'OK'                						// buttonName
			);
		}

	})
	.always(function(data, status, xhr) {	//Replaces the complete method
	    //alert( "complete " + response);
	});
}

/*TODO Local Storage - check for the user's credentials when the application is started */
function checkLocalCredentials() {
	if ((localStorage.email !== undefined) && (localStorage.password !== undefined) ) {
		//email & password against faves R us server
		console.log("User credentials defined!");
	}
	else {
		console.log("Email & password undefined");
	}
}


/*
Description: Local Storage - //Store username and accessToken or password locally for use later during relaunching of the app
*/
function storeLocalcredentials(data) {
	localStorage.email = data.split("&")[0].split("=")[1];
	localStorage.deviceID= getDeviceID();
	localStorage.loginstatus = true;
}

/*
	Description: Remove the stored login credentials
*/
function removeLocalcredentials() {
	localStorage.removeItem("email");
	localStorage.removeItem("deviceID");
	localStorage.removeItem("loginstatus");
	//Reset Image source
	$("#profile-pic span").remove("img");
	$("#profile-pic span").addClass("km-profile-pic-holder");
}


