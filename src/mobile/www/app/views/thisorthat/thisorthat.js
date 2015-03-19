define([
  'views/view',
  'text!views/thisorthat/thisorthat.html'
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
	//var view = new View('thisorthat', html, model);

	$.subscribe('/newCategory/add', function (e, text) {
    categories.add({ name: text });
  });

});

//Execute after the DOM finishes loading
$(
	function () {
		
	}
);

/* TorT short for This or That*/
/*
    Description:
    Associated to the data-init attribute Runs first time
*/
function tortViewInit (e) {
	//alert("This or that View");
}


/*
    Description:
    Associated to the data-after-show attribute (Recurring execution) which executes afte the data-init attribute
*/
function afterTortViewShow(e){
    //Swap the pager to the top of the content
    $($(".km-scrollview").children("div").get(0)).insertAfter($(".km-scrollview").children("ol").get(0));
}