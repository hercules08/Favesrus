/*service.js*/

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

	else if(requestString == "loginEmail") {
		requestURL = "";
		requestType = "GET";
	}

	else if(requestString == "loginFacebook") {
		requestURL = "";
		requestType = "GET";
	}

	//Make the AJAX call
	$.ajax({
		type: requestType,	//Specifies the type of request. (GET or POST)
		url: requestURL,	//Specifies the URL to send the request to. Default is the current page
		data: content		//Specifies data to be sent to the server
	})
	.done(function(response) {		//Replaces the success() method
	    alert( "success" );
	})
	.fail(function(response) {		//Replaces the error() method
	    alert( "error" );
	})
	.always(function(response) {	//Replaces the complete method
	    alert( "complete" );
	});
}