define([
  'views/view',
  'text!views/settings/settings.html'
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
	var view = new View('settings', html, model);

	$.subscribe('/newCategory/add', function (e, text) {
    categories.add({ name: text });
  });

});

var originalLogOutBtnX = 0, originalLogOutBtnY = 0;


//Execute after the DOM finishes loading
$(
	function () {
	}
);


/*
	Description:
	Apply event handler for all settings view buttons
*/
function enableSettingsButtonEventHandlers(){
	addTouchSMEvents("account-logout-btn", "inner-shadow");

	document.getElementById("account-logout-btn").addEventListener("touchend", 
        function (evt) {
			console.log("You are about to be logged out");
			//TODO Remove Local storage keys
			//TODO Invoke webservice call
			webService("logout","");
			removeLocalcredentials();
			returnHome();
		},
		false
	);	
}

/*
	Description:
	Fires the first time the view renders.
*/
function settingsViewInit(e) {
	//TODO Leverage Local Storage keys
	console.log("settings-view init");
	var temp_data = '{"entity":{"firstName":"First Name","lastName":"Last Name","profilePic":"images/image_placeholder.png"}}';
    var data = JSON.parse(temp_data);
    //View-Model declaration
    var viewModel = kendo.observable({
    	name: data.entity.firstName+" "+data.entity.lastName,
	    profilePic: data.entity.profilePic
	});
	//Bind  viewModel to desired element
	kendo.bind($("#myprofile-list-item img"), viewModel);
	kendo.bind($("#myprofile-list-item p"), viewModel);

	enableSettingsButtonEventHandlers();
}

/**/
function afterSettingsViewShow(e) {
	//setWishlistIcon();
	setWishlistTabBadge();
}
