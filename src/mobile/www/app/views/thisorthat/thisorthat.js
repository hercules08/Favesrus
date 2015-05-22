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
	//TODO webService
    var temp_data = '{"Status":"recommendations", "model":{"items":[{"id":"345415","name":"Interactive Gaming Figures","image":"http://www.gamestop.com/common/images/lbox/104546b.jpg"},{"id":"3545399","name":"Swimwear","image":"http://slimages.macys.com/is/image/MCY/products/6/optimized/2450776_fpx.tif?wid=262&hei=320&fit=fit,1&$filterxlrg$"},{"id":"3454367","name":"Sunglasses","image":"http://slimages.macys.com/is/image/MCY/products/8/optimized/2513158_fpx.tif?wid=262&hei=320&fit=fit,1&$filtersm$"},{"id":"3454363","name":"Smart Watches","image":"http://ecx.images-amazon.com/images/I/81Qkcobv5oL._SL1500_.jpg"},{"id":"3454006","name":"Designer Bags","image":"http://slimages.macys.com/is/image/MCY/products/2/optimized/2719462_fpx.tif?wid=262&hei=320&fit=fit,1&$filterxlrg$"},{"id":"1254005","name":"Sandals (Women)","image":"http://slimages.macys.com/is/image/MCY/products/1/optimized/2632151_fpx.tif?wid=262&hei=320&fit=fit,1&$filtersm$"},{"id":"0954005","name":"Cologne (Men)","image":"http://ecx.images-amazon.com/images/I/611lCGVDSnL._SX425_.jpg"}]}}';
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
    	//Test
    	var temp_data = '{"Status":"search", "model":{"items":[{"id":"3454388","name":"Mario Amiibo","image":"http://www.gamestop.com/common/images/lbox/104546b.jpg", "description":"Mario never hesitates to leap into action when there\'s trouble in the Mushroom Kingdom.", "category":"Interactive Gaming Figures","retailers":[{"id":"12332","name":"BestBuy","image":"http://upload.wikimedia.org/wikipedia/commons/thumb/f/f5/Best_Buy_Logo.svg/300px-Best_Buy_Logo.svg.png","price":"$12.96","lowest":"0"},{"id":"12334","name":"Walmart","image":"http://upload.wikimedia.org/wikipedia/commons/thumb/7/76/New_Walmart_Logo.svg/1000px-New_Walmart_Logo.svg.png","price":"$12.95","lowest":"1"}]},{"id":"3545354","name":"Disney INFINITY: Disney Originals (2.0 Edition) Crystal Sorcerer Mickey Figure","image":"http://www.gamestop.com/common/images/lbox/102788b.jpg", "description":"Feeling mischievous? Join Sorcerer\'s Apprentice Mickey\'s spellbinding high jinks. With his magic sweep and bursts, he\'s got more moves under his Sorcerer\'s hat than a wizard in a wand shop. Abracadabra!", "retailers":[{"id":"12332","name":"BestBuy","image":"http://upload.wikimedia.org/wikipedia/commons/thumb/f/f5/Best_Buy_Logo.svg/300px-Best_Buy_Logo.svg.png","price":"$12.96","lowest":"0"},{"id":"12334","name":"Walmart","image":"http://upload.wikimedia.org/wikipedia/commons/thumb/7/76/New_Walmart_Logo.svg/1000px-New_Walmart_Logo.svg.png","price":"$12.95","lowest":"1"},{"id":"12335","name":"Sears","image":"http://www.buyvia.com/i/2013/10/Sears-logo.png","price":"$13.96","lowest":"0"}]},{"id":"3454363","name":"Material Girl Selena Rhinestone Flat Thong Sandals","image":"http://slimages.macys.com/is/image/MCY/products/3/optimized/2158793_fpx.tif?wid=262&hei=320&fit=fit,1&$filtersm$", "description":"Add some shimmer to your summer with the Selena rhinestone flat thong sandals by Material Girl.","category":"Sandals (Women)"},{"id":"3545388","name":"Easy Spirit Kalindi Flat Sandals","image":"http://slimages.macys.com/is/image/MCY/products/1/optimized/2707711_fpx.tif?wid=262&hei=320&fit=fit,1&$filtersm$", "description":"A shoe with innovative comfort technology has never looked so chic! Easy Spirit\'s Kalindi sandals are simple, sophisticated and perfect for everyday adventures."},{"id":"35453578","name":"Apple Smartwatch Sport 42mm Silver Aluminium Case White Sport Band","image":"http://ecx.images-amazon.com/images/I/31czk%2BukKPL.jpg", "description":"Silver or space gray anodized aluminum case<br>Retina display with Force Touch<br>Heart rate sensor, accelerometer, and gyroscope","category":"Smart Watches"},{"id":"3545124","name":"Samsung Gear 2 Neo","image":"http://ecx.images-amazon.com/images/I/91V3t5zHNSL._SL1500_.jpg", "description":"The Samsung Gear 2 Neo is the smart companion watch tailored to your look and lifestyle. With real-time notifications, calls and fitness tracking right at our wrist, you can stay focused in the moment. No matter where your day takes you, your Gear 2 Neo matches your style to keep you connected without feeling distracted."},{"id":"3545394","name":"?","image":"images/image_placeholder.png", "description":""},{"id":"3545359","name":"?","image":"images/image_placeholder.png", "description":""},{"id":"3545984","name":"? ","image":"images/image_placeholder.png", "description":""},{"id":"35453674","name":"?","image":"images/image_placeholder.png", "description":""}]}}';
    	var data = JSON.parse(temp_data);
    	console.log(data);
    	$("#thisorthat-scrollview").data("kendoMobileScrollView").setDataSource(data.model.items); //Works (By it's self)
		// $("#thisorthat-scrollview").data("kendoMobileScrollView").refresh();
		$("#thisorthat-scrollview>div:first-child").css("height", 0.72*window.innerHeight);
    	/*
    	webService("gettotlist",'{userId: "732cb952-3392-493c-ad27-432113110081",recommendationIds: [1,2],returnedSetNumber: 2}');
    	APP.instance.showLoading();
		APP.instance.changeLoadingMessage("Loading Products...");
		*/
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
	Description: Get This or That (Dual Search) product information in the same column of the touched button
*/
function getTortProduct(element, data) {
	// $(this).find("img").attr("src")
	if ($(element).attr("class").indexOf("this") > -1) { //this 0
		console.log(data[0].name);
		setSelectedItemInfo(data[0].id, data[0].name, data[0].image);
	}
	else if ($(element).attr("class").indexOf("that") > -1) { //that 1
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
	    // addTouchSMEvents(e.data[i].id+"-btn", "inner-shadow");
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
		//Add Touch events to menu container
		/*document.getElementById(e.data[i].id+"-menu-container").removeEventListener("touchstart", function(){});
		document.getElementById(e.data[i].id+"-menu-container").addEventListener("touchstart", function (evt) {
			originalTouchX = evt.changedTouches[0].clientX;
		});
		document.getElementById(e.data[i].id+"-menu-container").removeEventListener("touchend", function(){});
		document.getElementById(e.data[i].id+"-menu-container").addEventListener("touchend", function (evt) {
			if(((originalTouchX - evt.changedTouches[0].clientX) < 20) && ((originalTouchX - evt.changedTouches[0].clientX) > -1)){
				console.log("Button: "+$(this).attr("alt"));
				//Open display the menu options
				if ($(this).attr("title") === "0"){
					console.log("First item: "+e.data[0].id);
					$("#"+e.data[0].id+"-menu-container .menu").addClass("hidden");
					$("#"+e.data[0].id+"-main-menu-back").addClass("hidden");
					$("#"+e.data[0].id+"-menu-container").removeClass("right-40");
					$("#"+e.data[0].id+"-menu-container").addClass("right-15");
					$("#"+e.data[0].id+"-menu-container .menu-option").removeClass("hidden");
					$("#"+e.data[0].id+"-info-icon").attr("class","menu svg");
					$("#"+e.data[0].id+"-heart-plus-icon").attr("class","menu svg");
					$("#"+e.data[0].id+"-list-plus-icon").attr("class","menu svg");
				}
				else if ($(this).attr("title") === "1"){
					console.log("Second item: "+e.data[1].id);
					$("#"+e.data[1].id+"-menu-container .menu").addClass("hidden");
					$("#"+e.data[1].id+"-main-menu-back").addClass("hidden");
					$("#"+e.data[1].id+"-menu-container").removeClass("right-40");
					$("#"+e.data[1].id+"-menu-container").addClass("right-15");
					$("#"+e.data[1].id+"-menu-container .menu-option").removeClass("hidden");
					$("#"+e.data[1].id+"-info-icon").attr("class","menu svg");
					$("#"+e.data[1].id+"-heart-plus-icon").attr("class","menu svg");
					$("#"+e.data[1].id+"-list-plus-icon").attr("class","menu svg");
				}
			}
		});*/
	}
	replaceSVGImage();
}



/*
	Description: Replace SVG images with inline SVG
*/
function replaceSVGImage() {
	$('img.svg').each(function(){
	    var img = $(this);
	    var imgID = img.attr('id');
	    var imgClass = img.attr('class');
	    var imgURL = img.attr('src');

	    $.get(imgURL, function(data) {
	        // Get the SVG tag, ignore the rest
	        var svg = $(data).find('svg');

	        // Add replaced image's ID to the new SVG
	        if(typeof imgID !== 'undefined') {
	            svg = svg.attr('id', imgID);
	        }
	        // Add replaced image's classes to the new SVG
	        if(typeof imgClass !== 'undefined') {
	            svg = svg.attr('class', imgClass+' replaced-svg');
	        }

	        // Remove any invalid XML tags as per http://validator.w3.org
	        svg = svg.removeAttr('xmlns:a');

	        // Replace image with new SVG
	        img.replaceWith(svg);

	    }, 'xml');

	});
}


function setTortScrollViewData(webServiceResponse) {
	var data = webServiceResponse;
	console.log(JSON.stringify(data));
	console.log(JSON.stringify(data.model.items));
	$("#thisorthat-scrollview").data("kendoMobileScrollView").setDataSource(data.model.items); //Works (By it's self)
	// $("#thisorthat-scrollview").data("kendoMobileScrollView").refresh();
	$("#thisorthat-scrollview>div:first-child").css("height", 0.70*window.innerHeight);
	APP.instance.hideLoading();
}

