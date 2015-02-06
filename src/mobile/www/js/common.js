FavesRUS.common = (function () {

	function navigateToView(view) {
		// Navigate to local/remote or external view
		FavesRUS.main.getKendoApplication().navigate(view);
	}
	
	return {
		navigateToView: navigateToView,
	}

})();