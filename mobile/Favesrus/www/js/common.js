FavesApp.common = (function () {

    function navigateToView(view) {
        //Navigate to local/remote or external view
        FavesApp.main.getKendoApplication().navigate(view);
    }

    return {
        navigateToView: navigateToView,
    }

})();