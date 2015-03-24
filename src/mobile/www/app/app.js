/*The order of the views matters*/
define([
	'views/thisorthat/thisorthat',
	'views/home/home',
	/*'views/search/search',*/
	'views/wishlist/wishlist',
	'views/login/login'/*,
	'views/shares/shares',
	'views/settings/settings'*/
], function () {

	// create a global container object
	var APP = window.APP = window.APP || {};

	var init = function () {
    
    // intialize the application
    // APP.instance = new kendo.mobile.Application(document.body, { skin: 'material' });
    APP.instance = new kendo.mobile.Application(document.body, { skin: 'ios7' }, {init: initialize()}, {initial: "#thisorthat-view"});
	};

	return {
	init: init
	};

});

/*
	Description: Set all necessary components at application initialization 
*/
function initialize() {
	setModalSize("thisorthat-preferences-modal", window.innerWidth*0.95, window.innerHeight*0.90);
	//setTimeout(function() {
	//APP.instance.showLoading();
    //APP.instance.changeLoadingMessage("Please wait...");
	    //setTimeout(function() {
	        //APP.instance.hideLoading();
		    var template = kendo.template($("#javascriptTemplate").html()); //Get the external template definition
		    var data = ["Set1", "Set2", "Set3", "Set4", "Set5"]; //Create some dummy data
		    var result = template(data); //Execute the template
		    //alert(JSON.stringify(result))
		    $("#thisorthat-scrollview").html(result); //Append the result
	    //}, 1000);
    //}, 500);
}