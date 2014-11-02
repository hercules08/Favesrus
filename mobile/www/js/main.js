/*jslint vars:true, plusplus:true */
/*global window, $ */

//counter for numbering the Person an Details input fields
var counter = 1;

//Disable the animation of the header name
$.ui.animateHeaders = false;

//Disable native look and feel
$.ui.useOSThemes = false;


window.onload = function () {
	$("#intro_page_button_container").width(0.35 * window.innerWidth);
	$(".intro_image_button").width(0.35 * window.innerWidth);

	$("#fb_login_button").click(function () {
		userLogin('Facebook');
	});

	$("#signup_button").click(function () {
		userLogin('Signup');
	});
	

	//Set the size of div to a square (50% of the device width)
	$(".red_bg").width(0.5 * window.innerWidth);
	$(".red_bg").height(0.5 * window.innerHeight);

	//Set the size for the Play button
	$("#play").width(0.40 * window.innerWidth);
	//apply an onclick event handler to the play! button
	$("#play").click(function () {
		$.ui.loadContent("#forth", false, false, "fade");
	});

	//Set the size for the Play button
	//$("#upcoming_image").width(0.60 * window.innerWidth);

	$("#spacer").height("10px")

	//Set the table size on the Gift Selection page
	$("#selection_container").width(0.8 * window.innerWidth);
	//$("#selection_container").height(0.25 * window.innerHeight);

	//Set the size of selction A & B div for this of This OR That! 
	$(".alizarin_bg").width(0.6 * window.innerHeight);
	$(".alizarin_bg").height(0.6 * window.innerHeight);
	$(".alizarin_bg").width(0.6 * window.innerHeight);
	$(".alizarin_bg").height(0.6 * window.innerHeight);

	//apply an onclick event handler to the Favs div
	/*$("#transition_button").click(function () {
		$.ui.loadContent("#menu_page", false, false, "fade");
	});*/

	//
	/*$("#favs_row").click(function () {
		$.ui.loadContent("#favs_details_page", false, false, "fade");
	});*/

	//Set the size of favs person div for for the menu Page 
	$(".person").width(0.15 * window.innerWidth);
	$(".person").height(0.15 * window.innerWidth);


	//Set the size of ask_network div for for the Favs Details Page 
	$(".suggestion_item").width(0.25 * window.innerWidth);
	$(".suggestion_item").height(0.15 * window.innerHeight);


	//Set the size of ask_network button div for for the Favs Details Page 
	$("#ask_network_button").width(0.2 * window.innerWidth);
	$("#ask_network_button").height(0.2 * window.innerHeight);

	//Set the size of ask_network button div for for the Favs Details Page 
	$("#ask_network_column").width(0.15 * window.innerWidth);

	//Add the scroll event handler to the fav_occasion element on the favs_details page
	/*document.getElementsByName("fav_ocassion")[0].onchange = function() {
	//document.getElementsByName("fav_ocassion")[0].onscroll = function() {
		alert("Occasion Date picker was Touch");
	};*/
}

/*
TEMPLATE
Function: functionName()
Parameter: parameterName is the 
Description:
*/
function userLogin(option) {
	$.ui.loadContent("#loading_page", false, false, "fade");
	setTimeout(function () {
		$.ui.loadContent("#menu_page", false, false, "fade");
		//load json data
		loadjsonData();
	}, 2000);
}


function updateFavDetails(data) {

}

/*
TEMPLATE
Function: functionName()
Parameter: parameterName is the 
Description:
*/
function loadjsonData(){
	var data;
	//$.get("mypage.php?foo=bar",function(data){});
	$.getJSON("http://71.237.221.15/giftly/api/user/1", function(data) {
		console.log(data);
		//Load the user name in Header
		$("#username").text("Hi "+data.FirstName+" "+data.LastName+"!");
		//Load images to the Favs section
		$("#person1").attr("src",data.Favs[0].Pic);
		$("#person1").click(function () {
			$.ui.loadContent("#favs_details_page", false, false, "fade");
			updateFavDetails(data);
		});
		$("#person2").attr("src",data.Favs[1].Pic);
		$("#person2").click(function () {
			$.ui.loadContent("#favs_details_page", false, false, "fade");
			updateFavDetails(data);
		});
		$("#person3").attr("src","http://www.nsbepropdx.org/uploads/2/3/7/3/23733030/9716396.jpg");
		$("#person3").click(function () {
			$.ui.loadContent("#favs_details_page", false, false, "fade");
			updateFavDetails(data);
		});
		$("#person4").attr("src","http://www.nsbepropdx.org/uploads/2/3/7/3/23733030/6245377.jpg");
		$("#person4").click(function () {
			$.ui.loadContent("#favs_details_page", false, false, "fade");
			updateFavDetails(data);
		});
	});	
	
}


