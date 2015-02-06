FavesApp.main = (function () {

    var application;

    function getApplication() {
        return application;
    }

    function initializeApp() {

        //intialize app
        application = new kendo.mobile.Application($(document.body), {
            initial: "faves-login-view",
            skin: "flat"
        });

        //Display loading image on every ajax call
        $(document).ajaxStart(function () {

            if (application.pane) {
                application.showLoading();
            }
        });

        //Hide ajax loading image on after ajax call
        $(document).ajaxStop(function () {

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