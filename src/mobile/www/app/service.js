/*service.js*/

/*
	Description:
	Get the user's facebook profile picture
*/
function getFacebookProfilePic() {
	openFB.api({
            path: '/me',
            success: function(data) {
                //console.log(JSON.stringify(data));
                //Set the facebook profile pic
                $("#profile-pic span").removeClass("km-profile-pic-holder");
                $("#profile-pic span").append("<img src=''/>");
                $("#profile-pic img").attr('src', 'http://graph.facebook.com/' + data.id + '/picture?type=small');
            },
            error: function errorHandler(error) {
        		//alert(error.message);
    		}
    	});
}

/*
	Description:
	Get the user's facebook profile information
*/
function getFacebookInfo(accessToken) {
	openFB.api({
            path: '/me',
            success: function(data) {
                //console.log(JSON.stringify(data));
                //Get the facebook profile email, access token, birthday, first name, last name IMPLEMENT LATER!!!!
                //alert("Email: "+ data.email);
                //Send User's facebook info to Faves R Us
                webService("loginFacebook","email=" + data.email + "&providerkey="+accessToken+"&firstname="+data.first_name+"&lastname="+data.last_name+"&gender="+data.gender+"&birthday="+data.user_birthday+"&profilepic=http://graph.facebook.com/" + data.id + "/picture?type=small");
            },
            error: function errorHandler(error) {
        		//alert(error.message);
    		}
    	});
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
		requestURL = "";
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
	    alert( "success " + response);
	    if((requestString === "registerEmail") || (requestString === "loginEmail") || (requestString === "loginFacebook")){
	    	closeLoginModal();
	    }
	})
	.fail(function(data, status, xhr) {		//Replaces the error() method
	    //alert( "error " + response.message);
	    if (navigator.notification) {
		    navigator.notification.alert(
			    JSON.parse(data.responseText).Message,  	// message
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