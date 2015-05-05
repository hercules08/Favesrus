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
function closeModal(modal_name, class_name) { //TODO fix for use
	//var temp_name = "#"+modal_name;
	//console.log($(temp_name));
	//console.log($("#thisorthat-recommendations-modal"));
	//$(temp_name).data("kendoMobileModalView").close();
	$("#"+modal_name).data("kendoMobileModalView").close();
	if (modal_name === "thisorthat-recommendations-modal" && (class_name.indexOf("left") !== -1)){
		$("#recommendations-btn").kendoMobileButton({ badge: $("#thisorthat-Recommendations-container").find(".selected").length });
		console.log("load this or that items");
		loadTortItems();
	}
	else if (modal_name === "thisorthat-recommendations-modal" && (class_name.indexOf("right") > -1)){
		//TODO Reset the selected recommendations
	}
}

/*----TorT short for This or That----*/
/*
    Description:
    Fires the first time the view renders.
*/
function tortViewInit (e) {
	console.log("Initiate This or that View");
	setTimeout(function () {
		if(localStorage.TTpreferences === undefined) { //No preferences set
			//alert("No preferences found!");
			// $("#thisorthat-recommendations-modal").data("kendoMobileModalView").open();
			$("#thisorthat-recommendations-modal .km-leftitem").click(function(){
				closeModal("thisorthat-recommendations-modal", $(this).attr('class'));
			});
			$("#thisorthat-recommendations-modal .km-rightitem").click(function(){
				closeModal("thisorthat-recommendations-modal", $(this).attr('class'));
			});
			loadRecommendations();
			$("#thisorthat-recommendations-modal").data("kendoMobileModalView").open();
		}
		else {
			//console.log("Preferences found!");
		}
		$("#thisorthat-view .km-rightitem").click(function(){
			returnHome();
		});
	}, 500);

	$("#recommendations-btn").click(function(){
		$("#thisorthat-recommendations-modal").data("kendoMobileModalView").open();
	});
	


	console.log("init"+$("#thisorthat-scrollview").data("kendoMobileScrollView"));
	//$("#thisorthat-scrollview").data("kendoMobileScrollView").setDataSource([{name:"Item 1"}]);
}


/*
    Description:
    Associated to the data-after-show attribute (Recurring execution) which executes afte the data-init attribute
*/
function afterTortViewShow(e){
    //Swap the pager to the top of the content
    console.log("after This or THat View Show")
    // $($(".km-scrollview").children("div").get(0)).insertAfter($(".km-scrollview").children("ol").get(0));
}

/*
	Description:
	Associated to the data-open attribute which executes after the modal is open
	TODO Load the collection of recommendations from the FavesRUs server
*/
function loadRecommendations() {
	console.log("load recommendations");
	var template = kendo.template($("#tortRecommendationsTemplate").html()); //Get the external template definition
    var temp_data = '{"Status":"recommendations", "model":{"items":[{"id":"345435","name":"Amiibos","image":"images/image_placeholder.png"},{"id":"3545354","name":"Celebrity Items","image":"images/image_placeholder.png"},{"id":"3454367","name":"Jeans","image":"images/image_placeholder.png"},{"id":"3454363","name":"Watches","image":"images/image_placeholder.png"},{"id":"3454398","name":"Shades","image":"images/image_placeholder.png"},{"id":"3454006","name":"Restaurants","image":"images/image_placeholder.png"},{"id":"3454005","name":"Big & Tall","image":"images/image_placeholder.png"}]}}';
    //var data = ["Recommendation1", "Recommendation2", "Recommendation3", "Recommendation4", "Recommendation5"]; //Create some dummy data
    var data = JSON.parse(temp_data);
    var result = template(data); //Execute the template
    $("#thisorthat-Recommendations-container").html(result); //Append the result

    //Enable Button Click Handler for each item returned
    data.model.items.forEach(function (element, index, array) {
    	$("#"+element.id+"-button").click(function(){
			if ($("#thisorthat-Recommendations-container").find(".selected").length < 3) {
				$("#"+element.id+"-button").toggleClass("selected");
				$("#"+element.id+"-button>a").toggleClass("hidden");
			}
			else {
				$("#"+element.id+"-button").removeClass("selected");
				$("#"+element.id+"-button>a").addClass("hidden");
			}
		});
    });
}

/*
	Description:
*/

function loadTortItems() {
	if($("#thisorthat-Recommendations-container").find(".selected").length > 0) {
    	console.log("display This or That items");
    	$(".no-recommendations-message").addClass("hidden");
    	$("#thisorthat-scrollview").removeClass("hidden");
    	//$("#thisorthat-view #pages-container").removeClass("hidden");
    	//TODO websService
    	var temp_data = '{"Status":"search", "Model":{"items":[{"id":"345435","name":"Mario Amiibo","image":"http://www.gamestop.com/common/images/lbox/104546b.jpg", "description":"Interactive Play with Nintendo console games"},{"id":"3545354","name":"Luigi Amiibo","image":"http://www.gamestop.com/common/images/lbox/106342b.jpg", "description":"Interactive Play with Nintendo console games"},{"id":"3454363","name":"Peach Amiibo","image":"images/image_placeholder.png", "description":"Interactive Play with Nintendo console games"},{"id":"3545354","name":"Pit Amiibo","image":"images/image_placeholder.png", "description":"Interactive Play with Nintendo console games"},{"id":"3545354","name":"Ness Amiibo","image":"images/image_placeholder.png", "description":"Interactive Play with Nintendo console games"}]}}';
    	var data = JSON.parse(temp_data);
    	$("#thisorthat-scrollview").data("kendoMobileScrollView").setDataSource(data.Model.items); //Works (By it's self)
    	// $("#thisorthat-scrollview").data("kendoMobileScrollView").refresh();
    	$("#thisorthat-scrollview>div:first-child").css("height", 0.65*window.innerHeight);
    	//$("#thisorthat-view #pages-container").html($($(".km-scrollview").children("ol").get(0))); //Move pager to top
    	//TODO Add the click event listener to this or that buttons
        $.each($("#thisorthat-button-container .tort-add"), function (index, element) {
            $(this).click(function () {
                /*itemID = $(this).parent().find(".item-name").attr("id");
                itemName = $(this).parent().find(".item-name").html();*/
                $("#addItem-actionsheet").data("kendoMobileActionSheet").open();
            })
        });
    }
    else {
    	console.log("hide This or That items");
    	$(".no-recommendations-message").removeClass("hidden");
    	$("#thisorthat-scrollview").addClass("hidden");
    	//$("#thisorthat-view #pages-container").addClass("hidden");
    }
}
