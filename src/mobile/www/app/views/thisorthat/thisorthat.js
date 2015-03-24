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

	/*$.subscribe('/newCategory/add', function (e, text) {
    categories.add({ name: text });
  });*/

});

//Execute after the DOM finishes loading
$(
	function () {
		
	}
);

/*
	Description: Set the width and height of a modal
*/
function setModalSize(modal_name, width, height) {
	$("#"+modal_name).attr("data-width", width);
	$("#"+modal_name).attr("data-height", height);
}

/*
*/
function closeModal(modal_name) { //TODO fix for use
	//var temp_name = "#"+modal_name;
	//console.log($(temp_name));
	//console.log($("#thisorthat-preferences-modal"));
	//$(temp_name).data("kendoMobileModalView").close();
	$("#"+modal_name).data("kendoMobileModalView").close();
}

/*----TorT short for This or That----*/
/*
    Description:
    Fires the first time the view renders.
*/
function tortViewInit (e) {
	//alert("This or that View");
	setTimeout(function () {
		if(localStorage.TTpreferences === undefined) { //No preferences set
			alert("No preferences found!");
			$("#thisorthat-preferences-modal").data("kendoMobileModalView").open();
			$("#thisorthat-preferences-modal a").click(function(){
				closeModal("thisorthat-preferences-modal");
			});
		}
		else {
			console.log("Preferences found!");
		}
	}, 500);
}


/*
    Description:
    Associated to the data-after-show attribute (Recurring execution) which executes afte the data-init attribute
*/
function afterTortViewShow(e){
    //Swap the pager to the top of the content
    $($(".km-scrollview").children("div").get(0)).insertAfter($(".km-scrollview").children("ol").get(0));
}