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
	//Open Login View if user hasn't sign in

	
        APP.instance.showLoading();
        APP.instance.changeLoadingMessage("Please wait...");
        setTimeout(function() {
            APP.instance.hideLoading();
            if ((localStorage.loginstatus === false) || (localStorage.loginstatus === undefined) ) {
                APP.instance.navigate("#login-view", "overlay:down");
            }
            else {
                console.log("You have already login in!");
            }
        }, 500);

		if (wishlistShowCounter == 0){
            enableButtonTouchEventListeners("login");
			//Initiate openFB
			// Defaults to sessionStorage for storing the Facebook token
     		//openFB.init({appId: '1549506478654715'});
            wishlistShowCounter = 1;
		}
        
}
