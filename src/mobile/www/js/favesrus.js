/*This should replace most of the code in main.js or this should be renamed to main.js and follow a the pattern shown*/\
FavesRUS.main = (function () {
	var application;

	function getApplication() {
		return application;
	}

	function initializeApp() {

		// Initialize app
		application = new kendo.mobile.Application(document.body,
		{
			/*{skin:"material"},*/ //Set skin to Android Material Light
			/*{platform: "ios7"},*/
			{initial: "fi-login-view"},
			transition: 'slide',
			loading: "<h3>Loading...</h3>"
		});

		// Display loading image on every ajax call
		$(document).ajaxStart(function() {
			// application.showLoading calls the showLoading() method 
			// of pane object in the app for time consuming task
			if (application.pane) {
				application.showLoading();
			}
		});

		// Hide ajax loading image on after ajax call
		$(document).ajaxStop(function() {

			if (application.pane) {
				application.hideLoading();
			}
		});
	}

	return {
		initializeApp: initializeApp,
		getKendoApplication: getApplication
	}
})();