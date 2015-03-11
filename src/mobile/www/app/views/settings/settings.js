define([
  'views/view',
  'text!views/settings/settings.html'
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

    //var view = new View('categories', html, model);
	var view = new View('settings', html, model);

	$.subscribe('/newCategory/add', function (e, text) {
    categories.add({ name: text });
  });

});

//Execute after the DOM finishes loading
$(
	function () {
	}
);

