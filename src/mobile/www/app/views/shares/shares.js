define([
  'views/view',
  'text!views/shares/shares.html'
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

    //var view = new View('categories', html, model);
	var view = new View('shares', html, model);

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
*/
function loadSharesWishlists(){
	var template = kendo.template($("#sharesWishlistTemplate").html()); //Get the external template definition
    var temp_data = '{"Status":"search", "Model":{"items":[{"id":"345435","name":"Damola Omotosho","image":"https://avatars0.githubusercontent.com/u/1912337?v=3&s=460", "description":"Likes: Microsoft Products, Software Technical books...."},{"id":"3545354","name":"Jovoni Ashtian","image":"https://media.licdn.com/mpr/mpr/shrink_200_200/p/7/005/09a/2ac/295b34a.jpg", "description":"Likes: Video Games (Sony), Video Games (Nintendo), Japanese Animation, DIY Kits"},{"id":"3454398","name":"Devrelle Dumas","image":"https://engineering.purdue.edu/MEP/Spotlights/copy2_of_DEANSINTERNATIONALEXPERIENCEAWARDpaysofinParisFranc/Devrelle%20before%20trip.jpg", "description":"Likes: Fantasy books, Perfumes"}]}}';
    var data = JSON.parse(temp_data);
    
    var result = template(data); //Execute the template
    console.log(data);
    //APP.instance.view().element.find("#products-search-listview").html(result); //Append the result
    $("#shares-wishlist-listview").html(result); //Append the result
}

/*
	Description:
	Callback function invoked when the Prompt confirmation dialog's buttons are pressed
	//TODO Send the inputted password to the FavesRUs web service to reset password
*/
function onRequestUserWishlistPrompt(results) {
    // results.buttonIndex = 1 for the first button, 2 for the second button, etc.
    // results.input1 = inputed text
    /*console.log(results);
    console.log("Inputted Email "+results.input1);*/
    webService("requestuserwishlist","email="+results.input1);
}

/**/
function initSharesView(e) {
	document.getElementById("send-wishlist-request-btn").removeEventListener("touchstart", function(){});
    document.getElementById("send-wishlist-request-btn").removeEventListener("touchmove", function(){});
	addTouchSMEvents("send-wishlist-request-btn", "inner-shadow");
	document.getElementById("send-wishlist-request-btn").removeEventListener("touchend", function(){});
    document.getElementById("send-wishlist-request-btn").addEventListener("touchend", function (evt) {
    	$("#send-wishlist-request-btn").removeClass("inner-shadow");
        	if (navigator.notification) {
	            navigator.notification.prompt(
	                "Please enter the email for the user that you would like to request access for their wishlist.",  // message
	                onRequestUserWishlistPrompt,         	// callback
	                "Request Wishlist",   // title
	                ["Send","Cancel"],  // buttonName
	            	""					//Default test
	            );
	        }
        	//e.preventDefault();
    });

}

/*
*/
function afterSharesViewShow(e) {
	//setWishlistIcon();
	setWishlistTabBadge();
	loadSharesWishlists();
}
