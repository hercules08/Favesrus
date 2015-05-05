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


var originalTortBtnX = 0, originalTortBtnY = 0;

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
function closeModal(modal_name, class_name) {
	$("#"+modal_name).data("kendoMobileModalView").close();
	if (modal_name === "thisorthat-recommendations-modal" && (class_name.indexOf("left") !== -1)){
	}
	else if (modal_name === "thisorthat-recommendations-modal" && (class_name.indexOf("right") > -1)){
		$("#recommendations-btn").kendoMobileButton({ badge: $("#thisorthat-Recommendations-container").find(".selected").length });
		console.log("load/update this or that items");
		loadTortItems();
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
		/*8e.view.element.find("#recommendations-btn").click(function(){
			$("#thisorthat-recommendations-modal").data("kendoMobileModalView").open();
		});*/
		document.getElementById("recommendations-btn").addEventListener("touchend", function (evt) {
			setTimeout(function(){
				$("#thisorthat-recommendations-modal").data("kendoMobileModalView").open();
			}, 400);
			
		});

		if(localStorage.tortPreferences === undefined) { //No preferences set
			//alert("No preferences found!");
			// $("#thisorthat-recommendations-modal").data("kendoMobileModalView").open();
			//$("#thisorthat-recommendations-modal .km-leftitem").click(function(){
			document.querySelectorAll("#thisorthat-recommendations-modal .km-leftitem")[0].addEventListener("touchend", function (evt) {
				closeModal("thisorthat-recommendations-modal", $(this).attr('class'));
			});
			//$("#thisorthat-recommendations-modal .km-rightitem").click(function(){
			document.querySelectorAll("#thisorthat-recommendations-modal .km-rightitem")[0].addEventListener("touchend", function (evt) {
				closeModal("thisorthat-recommendations-modal", $(this).attr('class'));
			});
			if ($("#thisorthat-Recommendations-container").html() == "") {
				loadRecommendations();
			}
			$("#thisorthat-recommendations-modal").data("kendoMobileModalView").open();
		}
		else {
			//console.log("Preferences found!");
		}
		/*$("#thisorthat-view .km-leftitem").click(function(){
			returnHome();
		});*/
		document.querySelectorAll("#thisorthat-view .km-leftitem")[0].addEventListener("touchend", function (evt) {
			returnHome();
		});
	}, 500);
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
    var temp_data = '{"Status":"recommendations", "model":{"items":[{"id":"345415","name":"Amiibos","image":"images/image_placeholder.png"},{"id":"3545399","name":"Celebrity Items","image":"images/image_placeholder.png"},{"id":"3454367","name":"Jeans","image":"images/image_placeholder.png"},{"id":"3454363","name":"Watches","image":"images/image_placeholder.png"},{"id":"3454398","name":"Shades","image":"images/image_placeholder.png"},{"id":"3454006","name":"Restaurants","image":"images/image_placeholder.png"},{"id":"3454005","name":"Big & Tall","image":"images/image_placeholder.png"}]}}';
    //var data = ["Recommendation1", "Recommendation2", "Recommendation3", "Recommendation4", "Recommendation5"]; //Create some dummy data
    var data = JSON.parse(temp_data);
    var result = template(data); //Execute the template
    $("#thisorthat-Recommendations-container").html(result); //Append the result

    //Enable Button Click Handler for each item returned
    data.model.items.forEach(function (element, index, array) {
    	// $("#"+element.id+"-button").click(function(){
    	document.getElementById(element.id+"-button").addEventListener("touchend", function (evt) {
			if ($("#thisorthat-Recommendations-container").find(".selected").length < 3 && !($("#"+element.id+"-button").hasClass("selected"))) {
				console.log("selected");
				$("#"+element.id+"-button").toggleClass("selected");
				$("#"+element.id+"-button>a").toggleClass("hidden");
			}
			else {
				if($("#"+element.id+"-button").hasClass("selected")) {
					console.log("deselected");
					$("#"+element.id+"-button").removeClass("selected");
					$("#"+element.id+"-button>a").addClass("hidden");
				}
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
    	//TODO websService
    	var temp_data = '{"Status":"search", "Model":{"items":[{"id":"3454388","name":"Mario Amiibo","image":"http://www.gamestop.com/common/images/lbox/104546b.jpg", "description":"Interactive Play with Nintendo console games", "retailers":[{"id":"12332","name":"BestBuy","image":"http://upload.wikimedia.org/wikipedia/commons/thumb/f/f5/Best_Buy_Logo.svg/300px-Best_Buy_Logo.svg.png","price":"$12.96","lowest":"0"},{"id":"12334","name":"Walmart","image":"http://upload.wikimedia.org/wikipedia/commons/thumb/7/76/New_Walmart_Logo.svg/1000px-New_Walmart_Logo.svg.png","price":"$12.95","lowest":"1"}]},{"id":"3545354","name":"Luigi Amiibo","image":"http://www.gamestop.com/common/images/lbox/106342b.jpg", "description":"Interactive Play with Nintendo console games", "retailers":[{"id":"12332","name":"BestBuy","image":"http://upload.wikimedia.org/wikipedia/commons/thumb/f/f5/Best_Buy_Logo.svg/300px-Best_Buy_Logo.svg.png","price":"$12.96","lowest":"0"},{"id":"12334","name":"Walmart","image":"http://upload.wikimedia.org/wikipedia/commons/thumb/7/76/New_Walmart_Logo.svg/1000px-New_Walmart_Logo.svg.png","price":"$12.95","lowest":"1"},{"id":"12335","name":"Sears","image":"http://www.buyvia.com/i/2013/10/Sears-logo.png","price":"$13.96","lowest":"0"}]},{"id":"3454363","name":"Peach Amiibo","image":"http://www.gamestop.com/common/images/lbox/104547b.jpg", "description":"Interactive Play with Nintendo console games"},{"id":"3545388","name":"Pit Amiibo","image":"http://www.gamestop.com/common/images/lbox/106338b.jpg", "description":"Interactive Play with Nintendo console games"},{"id":"35453578","name":"Bowser Amiibo","image":"http://www.gamestop.com/common/images/lbox/108110b.jpg", "description":"Interactive Play with Nintendo console games"},{"id":"3545124","name":"Diddy Kong Amiibo","image":"http://www.gamestop.com/common/images/lbox/106346b.jpg", "description":"Interactive Play with Nintendo console games"},{"id":"3545394","name":"? Amiibo","image":"images/image_placeholder.png", "description":"Interactive Play with Nintendo console games"},{"id":"3545359","name":"? Amiibo","image":"images/image_placeholder.png", "description":"Interactive Play with Nintendo console games"},{"id":"3545984","name":"? Amiibo","image":"images/image_placeholder.png", "description":"Interactive Play with Nintendo console games"},{"id":"35453674","name":"? Amiibo","image":"images/image_placeholder.png", "description":"Interactive Play with Nintendo console games"}]}}';
    	var data = JSON.parse(temp_data);
    	$("#thisorthat-scrollview").data("kendoMobileScrollView").setDataSource(data.Model.items); //Works (By it's self)
    	// $("#thisorthat-scrollview").data("kendoMobileScrollView").refresh();
    	$("#thisorthat-scrollview>div:first-child").css("height", 0.70*window.innerHeight);
    }
    else {
    	console.log("hide This or That items");
    	$(".no-recommendations-message").removeClass("hidden");
    	$("#thisorthat-scrollview").addClass("hidden");
    	//$("#thisorthat-view #pages-container").addClass("hidden");
    }
}


/*
	Description:
*/
function initDetailsModal(e) {
	//$("#thisorthat-products-details-modal .km-leftitem").click(function(){
	document.querySelectorAll("#thisorthat-products-details-modal .km-leftitem")[0].addEventListener("touchend", function (evt) {
		closeModal("thisorthat-products-details-modal", $(this).attr('class'));
	});
}

/*
	Description: Load the selected products details from This or That choices
*/
function loadProductDetails(data) {
	var template = kendo.template($("#tortProductDetailsTemplate").html()); //Get the external template definition
    var result = template(data); //Execute the template
    $("#thisorthat-products-details-container").html(result); //Append the result

    console.log(data);
    try {
    	$("#product-retailers-container").removeClass("hidden");
    	template = kendo.template($("#productRetailersTemplate").html());
	    result = template(data.retailers);
	    $("#product-retailers-listview").html(result);
    }
    catch(ex) {
    	$("#product-retailers-container").addClass("hidden");
    }
}

/*
	Description: Get This or That (Dual Search) product information in thesame column of the touched button
*/
function getTortProduct(element, data) {
	$(this).find("img").attr("src")
	if ($(element).find("img").attr("src").indexOf("this") > -1) { //this 0
		console.log(data[0].name);
		setSelectedItemInfo(data[0].id, data[0].name, data[0].image);
	}
	else if ($(element).find("img").attr("src").indexOf("that") > -1) { //that 1
		console.log("This or That: "+data[1].name);
		setSelectedItemInfo(data[1].id, data[1].name, data[1].image);
	}
}

/*
	Description: Triggered when the "THis or That!"(Dual Search) Scroll View is swiped(changed) 
*/
function getViewableProductChange(e){
	var originalTouchX = 0;
	for(var i = 0; i < e.data.length; i++) {
		//Add inner shadow to button this or that buttons
		document.getElementById(e.data[i].id+"-btn").removeEventListener("touchstart", function(){});
		document.getElementById(e.data[i].id+"-btn").removeEventListener("touchstart", function(){});
	    document.getElementById(e.data[i].id+"-btn").removeEventListener("touchmove", function(){}); 
	    addTouchSMEvents(e.data[i].id+"-btn", "inner-shadow");
	    document.getElementById(e.data[i].id+"-btn").addEventListener("touchstart", function (evt) {
			originalTortBtnX = evt.changedTouches[0].clientX;
			originalTortBtnY = evt.changedTouches[0].clientY;
		});
		document.getElementById(e.data[i].id+"-btn").removeEventListener("touchend", function(){});
		document.getElementById(e.data[i].id+"-btn").addEventListener("touchend", function (evt) {
			if((evt.changedTouches[0].clientX >= originalTortBtnX-10 && evt.changedTouches[0].clientX <= originalTortBtnX+10) && (evt.changedTouches[0].clientY >= originalTortBtnY-5 && evt.changedTouches[0].clientY <= originalTortBtnY+5) ) {
				$(this).removeClass("inner-shadow");
				// console.log($(this).find("img").attr("src"));
				getTortProduct(this, e.data);
				$("#tort-addItem-actionsheet").data("kendoMobileActionSheet").open();
			}
		});

		document.getElementById(e.data[i].id+"-img").removeEventListener("touchstart", function(){});
		document.getElementById(e.data[i].id+"-img").addEventListener("touchstart", function (evt) {
			originalTouchX = evt.changedTouches[0].clientX;
		});
		document.getElementById(e.data[i].id+"-img").removeEventListener("touchend", function(){});
		document.getElementById(e.data[i].id+"-img").addEventListener("touchend", function (evt) {
			if(((originalTouchX - evt.changedTouches[0].clientX) < 20) && ((originalTouchX - evt.changedTouches[0].clientX) > -1)){
				$("#thisorthat-products-details-modal").data("kendoMobileModalView").open();
				if ($(this).attr("alt") === "0"){
					loadProductDetails(e.data[0]);
				}
				else if ($(this).attr("alt") === "1"){
					loadProductDetails(e.data[1]);
				}
			}
		});
	}
}
