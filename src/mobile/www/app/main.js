//Comment: Prevent Default Scrolling  
var preventDefaultScroll = function(event) {
    //Comment: Prevent scrolling on this element
    event.preventDefault();
    window.scroll(0,0);
    return false;
};
window.document.addEventListener("touchmove", preventDefaultScroll, false);

require.config({
  paths: {
    'text': '../bower_components/requirejs-text/text'
  }
});

define([
  'app'
], function (app) {
  if (kendo.support.mobileOS) {
    document.addEventListener('deviceready', function () {
      app.init();
      //Hide Splashscreen
      navigator.splashscreen.hide();
      //Keyboard
      cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
      //cordova.plugins.Keyboard.disableScroll(true);
    }, false);
  }
  else {
    app.init();
  }

});