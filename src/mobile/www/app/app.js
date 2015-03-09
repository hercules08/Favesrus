define([
  'views/home/home',
  /*'views/thisorthat/thisorthat',
  'views/search/search',*/
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
    APP.instance = new kendo.mobile.Application(document.body, { skin: 'ios7' }, {initial: "app/views/home/home.html"});
  };

  return {
    init: init
  };

});