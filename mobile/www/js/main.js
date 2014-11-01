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
	$(".temp").width(0.5 * window.innerWidth);
	$(".temp").height(0.5 * window.innerHeight);

	//apply an onclick event handler
	$("#play").click(function () {
		$.ui.loadContent("#forth", true, true, "fade");
	});
}

/*
TEMPLATE
Function: functionName()
Parameter: parameterName is the 
Description:
*/
function login(method) {

}
