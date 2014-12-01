//  Prevent Default Scrolling  
var preventDefaultScroll = function(event) 
{
    // Prevent scrolling on this element
    event.preventDefault();
    window.scroll(0,0);
    return false;
};
    
window.document.addEventListener("touchmove", preventDefaultScroll, false);

//Execute after the DOM finishes loading
$(
    function () {
        //Add the padding styling to the first button on the Login View
        $("#login_container img").first().css("padding-bottom", "10vh");
        
        //Add the padding-top styling to the first image on the Main Menu View
        $("#mainmenu_container img").first().css("padding-bottom", "5vh");
        
        //Add margin-left to the first <div> container
        //$(".person").first().css("margin-left", "92px");
        
        //$("a img").css("width","100%");
        
        //Make the images inside the selection Circles full width
        $(".selection img").css("width","100%");
        $(".selection img").css("height","100%");
        
        //Set the selection Circles to a default size
        $(".selection").width(0.55 * window.innerWidth);
        $(".selection").height(0.55 * window.innerWidth);
        
        //Add event listener that cretes border around the selected Selection circle
        $(".selection").click(
            function () {
                $(this).css("border","#e74c3c 2px solid");
            }
        );
        
        //Set the size for the person_container children
        $("#person_container img").width(0.25 * window.innerWidth);
        $("#person_container img").height(0.25 * window.innerWidth);
        $(".person").width(0.20 * window.innerWidth);
        $(".person").height(0.20 * window.innerWidth);
        
        //Make the images inside the person Circles full width
        $(".person img").css("width","100%");
        $(".person img").css("height","100%");
        
        
        
        $("#prev-img").click(function(e) {
            var scrollView = $("#scrollview").data("kendoMobileScrollView");
            scrollView.prev();
        });

        $("#next-img").click(function(e) {
            var scrollView = $("#scrollview").data("kendoMobileScrollView");
            scrollView.next();
        });
    }
); //After the DOM finishes Loading

//Close the SignUp Modal View
function closeModalViewLogin() {
    $("#signup-modalview").kendoMobileModalView("close");
}

//Open the SignUp Modal View
function openSignUpModalView(e) {
    $("#signup-modalview").data("kendoMobileModalView").open();
    //Add the month to the <select> tag
    //var monthsOfYear = ["Feb", "March", "April", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec"];
    /*TODO This is not working; Unable to create dropdown*/
    /*$("#birthday").kendoDropDownList({
                        dataTextField: "text",
                        dataValueField: "value",
                        dataSource: [{ text: "Jan", value: "Jan" }]
                    });
    var dropdownlist = $("#birthday").data("kendoDropDownList");*/
    
    //for (var i = 0;i < monthsOfYear.length; i++){
        /*option = document.createElement("option");
        option.text = monthsOfYear[i];
        option.value = monthsOfYear[i];
        select = document.getElementsByName("month")[0];
        select.appendChild(option);*/
        
        
        //dropdownlist.dataSource.add({ text: monthsOfYear[i], value: monthOfYear[i] });
    //}
}

//Open the Favorites Modal View
function openFavoritesModalView(e) {
    $("#fi-favorites-modalview").data("kendoMobileModalView").open();
}

//Close the Favorites Modal View
function closeFavoritesModalView() {
    $("#fi-favorites-modalview").kendoMobileModalView("close");
}


function loadjsonSelectionData(){
	var data;
    alert("hello");
	//$.get("mypage.php?foo=bar",function(data){});
	$.getJSON("http://71.237.221.15/giftly/api/item/getRandomItems", function(data) {
		console.log(data);
		//Load images to the Favs section
		$("#img_A").attr("src",data[0].ImageLink);
		$("#selection_A").click(function () {
			//updateServer(data, data.Favs[0].FirstName+" "+data.Favs[0].LastName);
			loadjsonSelectionData();
		});
		$("#img_B").attr("src",data[1].ImageLink);
		$("#selection_B").click(function () {
			//updateServer(data, data.Favs[1].FirstName+" "+data.Favs[1].LastName);
			loadjsonSelectionData();
		});
	});		
}

//apply an onclick event handler to the play! button
	$("#playtot-button").click(function () {
		loadjsonSelectionData();
	});


function loadjsonData(){
    //alert("Load JSON Data")
    var data;
	//$.get("mypage.php?foo=bar",function(data){});
	$.getJSON("http://71.237.221.15/giftly/api/user/1", function(data) {
		//console.log(data);
		//Load the user name in Header
		$("#username").html("Hi "+data.FirstName+"!");
		//Load images to the Favs section
		$("#person1").attr("src",data.Favs[0].Pic);
		$("#person1").click(function () {
//			$.ui.loadContent("#favs_details_page", false, false, "fade");
//			updateFavDetails(data, data.Favs[0].FirstName+" "+data.Favs[0].LastName);
		});
		$("#person2").attr("src",data.Favs[1].Pic);
		$("#person2").click(function () {
//			$.ui.loadContent("#favs_details_page", false, false, "fade");
//			updateFavDetails(data, data.Favs[1].FirstName+" "+data.Favs[1].LastName);
		});
//		$("#person3").attr("src","http://www.nsbepropdx.org/uploads/2/3/7/3/23733030/9716396.jpg");
//		$("#person3").click(function () {
//			$.ui.loadContent("#favs_details_page", false, false, "fade");
//			updateFavDetails(data, "");
//		});
//		$("#person4").attr("src","http://www.nsbepropdx.org/uploads/2/3/7/3/23733030/6245377.jpg");
//		$("#person4").click(function () {
//			$.ui.loadContent("#favs_details_page", false, false, "fade");
//			updateFavDetails(data, "");
//		});
	});	
	
}

function checkout(){
	$.getJSON("http://71.237.221.15/giftly/api/pay", function(data) {
		//alert("Checkout reached!");
	});

}

function showFavoritesOptions () {
    alert("hello");
}