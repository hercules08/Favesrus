//  Prevent Default Scrolling  
var preventDefaultScroll = function(event) {
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
        
        //Make the images inside the selection Circles full width
        $(".selection img").css("width","100%");
        $(".selection img").css("height","100%");
        
        //Set the selection Circles to a default size
        var selection_div_width = 0.55 * window.innerWidth;
        var selection_div_height = 0.55 * window.innerWidth;
        $(".selection").width(selection_div_width);
        $(".selection").height(selection_div_height);
        
        //Set the selection selection notifier Image to a default size
        $(".selection_notifier").width(0.45 * selection_div_width);
        $(".selection_notifier").height(0.15 * selection_div_height);
        
        
        //Add Click event listener to Selection circle that highlights the selected item
        $(".selection").click(
            function () {
                highlightSelection($(this));            
            }
        );
        
        //Add Change event Listener to the select element
        $( "#product_category" ).change(function() {
            //alert( "Handler for .change() called." );
            loadSelectionImages();
        });
        
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

//Open the Favorites Modal View
function openUpcomingEventsModalView(e) {
    $("#fi-upcoming-events-modalview").data("kendoMobileModalView").open();
}

//Close the Upcoming Events Modal View
function closeUpcomingEventsModalView() {
    $("#fi-upcoming-events-modalview").kendoMobileModalView("close");
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

//apply an onclick event handler to the play! button NOT USED
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

/*---------------DISCOVER YOUR FAVE GIFTS---------------*/
/*Change the border to the selection div, fade in the "Faved Image", trigger loadSelectionImages*/
function highlightSelection(element) {
    if ($("#product_category").val() !== "none") {
        //Change the border
        element.css("border","#e74c3c 2px solid");
        //Fade in the "FavedIt Image"
        element.children().fadeTo(800,1);
        //Fade out the "FavedIt Image"
        element.children().fadeTo(100,0);

        //Trigger Transition in images in selection wait 1 sec
        setTimeout(
            function () {
                //Return to default border color
                element.css("border","#bdc3c7 2px solid");
                loadSelectionImages();
            },
            1000);
        
    }
    else {
        if (navigator.notification) {
            navigator.notification.alert(
                'Please Select a Product Category!',  // message
                "",         // callback
                'No Product Category Selected',            // title
                'OK'                  // buttonName
            );
        }
        else {
            alert("Select a Product Category!");
        }
    }
}



/*Load the appropriate image src for each Selection Circle image divs*/
function loadSelectionImages() {
    var temp_video_game_array=["http://www.gamestop.com/common/images/lbox/240129b.jpg",
                               "http://www.gamestop.com/common/images/lbox/240262b.jpg",
                               "http://www.gamestop.com/common/images/lbox/240172b1.jpg",
                               "http://www.gamestop.com/common/images/lbox/240274b.jpg",
                               "http://www.gamestop.com/common/images/lbox/102221b.jpg",
                               "http://www.gamestop.com/common/images/lbox/105861b.jpg",
                               "http://www.gamestop.com/common/images/lbox/240056b1.jpg"
                              ],
    temp_shoes_array=["http://a1.zassets.com/images/z/2/8/7/6/3/3/2876335-p-MULTIVIEW.jpg",
                      "http://a9.zassets.com/images/z/2/8/5/6/7/1/2856712-p-MULTIVIEW.jpg",
                      "http://a1.zassets.com/images/z/3/0/4/4/0/4/3044040-p-MULTIVIEW.jpg",
                      "http://a1.zassets.com/images/z/2/9/3/0/6/1/2930616-p-MULTIVIEW.jpg",
                      "http://a3.zassets.com/images/z/2/8/8/0/9/5/2880955-p-MULTIVIEW.jpg",      
                      "http://a2.zassets.com/images/z/1/0/4/4/9/6/1044961-p-MULTIVIEW.jpg",      
                      "http://a9.zassets.com/images/z/1/5/6/15651-p-MULTIVIEW.jpg"     
                    ], 
    temp_array = null;
    // Get the value from a dropdown select
    var product_category_val = $("#product_category").val();
    if (product_category_val === "video_games") {
        temp_array = temp_video_game_array;
    }
    else if (product_category_val === "shoes") {
        temp_array = temp_shoes_array;
    }
    
    if (temp_array !== null) {
        //Load an image into Selection A
        var a = Math.floor(Math.random() * 6);
        $("#selection_A").css("background-image","url('"+temp_array[a]+"')");
        var b = Math.floor(Math.random() * 6);
        //Prevent a from being the same as b
        while (a == b) {
            b = Math.floor(Math.random() * 6);
        }
    
        //Load an image into Selection B
        $("#selection_B").css("background-image","url('"+temp_array[b]+"')");
        
    }
    else {
        if (navigator.notification) {
            navigator.notification.alert(
                'Please Select a Product Category!',  // message
                "",         // callback
                'No Product Category Selected',            // title
                'OK'                  // buttonName
            );
        }
        else {
            alert("Select a Product Category!");
        }
    }
}
