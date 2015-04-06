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


/*
	Description:
	Fires the first time the view renders.
*/
function settingsViewInit(e) {
	//TODO Webservice call
	console.log("settings-view init");
	var template = kendo.template($("#myProfileTemplate").html()); //Get the external template definition
    var temp_data = '{"entity":{"firstName":"John","lastName":"Doe","profilePic":"images/image_placeholder.png"}}';
    //Create some dummy data
    var data = JSON.parse(temp_data);
    var result = template(data); //Execute the template
    e.view.element.find("#myprofile-list-item").html(result); //Append the result*/
}
