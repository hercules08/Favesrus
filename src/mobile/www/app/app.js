/*The order of the views matters*/
define([
	'views/home/home',
	'views/thisorthat/thisorthat',
	'views/login/login',
	'views/wishlist/wishlist',
	'views/shares/shares',
	'views/settings/settings'
], function () {

	// create a global container object
	var APP = window.APP = window.APP || {};

	var init = function () {
    
    // intialize the application
    // APP.instance = new kendo.mobile.Application(document.body, { skin: 'material' });
    APP.instance = new kendo.mobile.Application(document.body, { skin: 'ios7' }, {init: initialize()}, {initial: "#home-view"});
	};

	return {
	init: init
	};

});



/*
	Description: Set all necessary components at application initialization 
*/
function initialize() {
	// setModalSize("thisorthat-recommendations-modal", window.innerWidth*0.95, window.innerHeight*0.90);
	//setTimeout(function() {
	//APP.instance.showLoading();
	//TODO Webservice call to server
    //APP.instance.changeLoadingMessage("Please wait...");
	    //setTimeout(function() {
	        //APP.instance.hideLoading();
		    // loadTortItems();
	    //}, 1000);
    //}, 500);
}

