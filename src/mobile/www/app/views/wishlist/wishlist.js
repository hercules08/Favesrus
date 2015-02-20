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

//Execute after the DOM finishes loading
$(
  function () {
    /*Event Listeners*/
  }
);

/*
  Description: 
  Invoked when the 'data-after-show' event is triggered associated to the wishlist view
  Open Login Modal View if user hasn't sign in
*/
function afterWishlistViewShow(e) {
  $("#login-modal").data("kendoMobileModalView").open();
}

/*
  Description: Open Login Modal View if user hasn't sign in
*/
function closeLoginModal() {
  $("#login-modal").kendoMobileModalView("close");
}