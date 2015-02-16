define([
  'views/view',
  'text!views/first/first.html'
], function (View, html) {

  var categories = new kendo.data.DataSource({
    data: [
      { name: 'Work' },
      { name: 'Personal' },
      { name: 'Other' }
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