/*jslint vars:true, plusplus:true */
/*global window, $ */

//counter for numbering the Person an Details input fields
var counter = 1;

//Disable the animation of the header name
$.ui.animateHeaders = false;

//Disable native look and feel
$.ui.useOSThemes = false;


window.onload = function () {
	//Set the size of div to a square (50% of the device width)
	$(".red_bg").width(0.5 * window.innerWidth);
	$(".red_bg").height(0.5 * window.innerHeight);

	//apply an onclick event handler to the play! button
	$("#play").click(function () {
		$.ui.loadContent("#forth", true, true, "fade");
	});

	//Stretch the Favorites Row accross two rows 
	$("#temp2").attr("colspan", "'2'");

	//Set the table size on the Gift Selection page
	$("#selection_container").width(0.8 * window.innerWidth);
	//$("#selection_container").height(0.25 * window.innerHeight);



	//Set the size of selction A & B div for this of This OR That! 
	$(".alizarin_bg").width(0.6 * window.innerHeight);
	$(".alizarin_bg").height(0.6 * window.innerHeight);
	$(".alizarin_bg").width(0.6 * window.innerHeight);
	$(".alizarin_bg").height(0.6 * window.innerHeight);

	//apply an onclick event handler to the Favs div
	$("#favs_row").click(function () {
		$.ui.loadContent("#favs_details_page", true, true, "fade");
	});

	//Set the size of ask_network div for for the Favs Details Page 
	$(".suggestion_item").width(0.25 * window.innerWidth);
	$(".suggestion_item").height(0.15 * window.innerHeight);


	//Set the size of ask_network button div for for the Favs Details Page 
	$("#ask_network_button").width(0.2 * window.innerWidth);
	$("#ask_network_button").height(0.2 * window.innerHeight);

	//Set the size of ask_network button div for for the Favs Details Page 
	$("#ask_network_column").width(0.15 * window.innerWidth);

	//Add the pressed event handler to the fav_occasion element on the favs_details page
	$("select").change(function() {
		alert("Occasion Date picker was Touch");
	});

	/*$("#fav_occasion").scroll(function() {
		alert("Occasion Date picker was Scrolled");
	});*/
}

/*
TEMPLATE
Function: functionName()
Parameter: parameterName is the 
Description:
*/
function login(method) {

}
