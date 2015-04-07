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
/*function setModalSize(modal_name, width, height) {
	console.log("Used");
	$("#"+modal_name).attr("data-width", width);
	$("#"+modal_name).attr("data-height", height);
}*/

/*
*/
function closeModal(modal_name) { //TODO fix for use
	//var temp_name = "#"+modal_name;
	//console.log($(temp_name));
	//console.log($("#thisorthat-recommendations-modal"));
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
			$("#thisorthat-recommendations-modal").data("kendoMobileModalView").open();
			$("#thisorthat-recommendations-modal .km-rightitem").click(function(){
				closeModal("thisorthat-recommendations-modal");
			});
			loadRecommendations();
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



/*
	Description:
	Associated to the data-open attribute which executes after the modal is open
	TODO Load the collection of recommendations from the FavesRUs server
*/
function loadRecommendations() {
	var template = kendo.template($("#tortRecommendationsTemplate").html()); //Get the external template definition
    var temp_data = '{"Status":"recommendations", "Model":{"items":[{"id":"345435","name":"Amiibos","image":"images/image_placeholder.png"},{"id":"3545354","name":"Celebrity Items","image":"images/image_placeholder.png"},{"id":"3454367","name":"Jeans","image":"images/image_placeholder.png"},{"id":"3454363","name":"Watches","image":"images/image_placeholder.png"},{"id":"3454398","name":"Shades","image":"images/image_placeholder.png"},{"id":"3454006","name":"Restaurants","image":"images/image_placeholder.png"},{"id":"3454005","name":"Big & Tall","image":"images/image_placeholder.png"}]}}';
    //var data = ["Recommendation1", "Recommendation2", "Recommendation3", "Recommendation4", "Recommendation5"]; //Create some dummy data
    var data = JSON.parse(temp_data);
    var result = template(data); //Execute the template
    $("#thisorthat-Recommendations-container").html(result); //Append the result

    //Enable Button Click Handler for each item returned
    data.Model.items.forEach(function (element, index, array) {
    	$("#"+element.id+"-button").click(function(){
			if ($("#thisorthat-Recommendations-container").find(".selected").length < 3) {
				$("#"+element.id+"-button").toggleClass("selected");
			}
			else {
				$("#"+element.id+"-button").removeClass("selected");
			}
		});
    });
}


