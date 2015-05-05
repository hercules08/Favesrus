define([
  'views/view',
  'text!views/home/home.html'
], function (View, html) {

    var categories = new kendo.data.DataSource({
    data: [
        { name: 'Work' },
        { name: 'Personal' },
        { name: 'Other' }
    ]
    });

    var model = {
        categories: categories,
        title: 'Title'
    };

    var view = new View('categories', html, model);

    $.subscribe('/newCategory/add', function (e, text) {
        categories.add({ name: text });
    });

});

var homeShowCounter = 0;
var itemID, itemName, orignalSearchResetX = 0, orignalSearchResetY = 0;


/*
    Description:
*/

/*
    Description:
    Invoked after the body loads
*/
$(
	function() {
		//console.log("How many Search bar forms are there? " + $("form").length);
	}
);

/*
    Description:
    Set the appropriate information in the datasource for products list view display
*/
function setProductData(e) {
    APP.instance.showLoading();
    APP.instance.changeLoadingMessage("Loading Products...");
    setTimeout(function() {
        APP.instance.hideLoading();
    }, 1000);
    setTimeout(function() {
        //Add Products to list view
        var template = kendo.template($("#productSearchTemplate").html()); //Get the external template definition
        var temp_data = '{"Status":"search", "Model":{"items":[{"id":"345435","name":"Mario Amiibo","image":"http://www.gamestop.com/common/images/lbox/104546b.jpg", "description":"Interactive Play with Nintendo console games"},{"id":"3545354","name":"Luigi Amiibo","image":"http://www.gamestop.com/common/images/lbox/106342b.jpg", "description":"Interactive Play with Nintendo console games"},{"id":"3454363","name":"Peach Amiibo","image":"images/image_placeholder.png", "description":"Interactive Play with Nintendo console games"},{"id":"3454398","name":"Wario Amiibo","image":"images/image_placeholder.png", "description":"Interactive Play with Nintendo console games"},{"id":"3454005","name":"Pit Amiibo","image":"images/image_placeholder.png", "description":"Interactive Play with Nintendo console games"}]}}';
        var data = JSON.parse(temp_data);
        
        var result = template(data); //Execute the template
        console.log(data);
        APP.instance.view().element.find("#products-search-listview").html(result); //Append the result

        try {
        	if(cordova){
            	cordova.plugins.Keyboard.close();
        	}
        }
        catch (error){
        	console.log("Cordova object is not defined.")
        }
        //Add the touch event listener to all product add gift icons
        $.each($("#home-view .product-add"), function (index, element) {
            element.addEventListener("touchstart", function (evt) {
                    //console.log("touchstart")
                    itemID = $(this).parent().find(".item-name").attr("id");
                    itemName = $(this).parent().find(".item-name").html();
                    $("#addItem-actionsheet").data("kendoMobileActionSheet").open();
                    //evt.preventDefault(); 
                }, 
            false);
        });
    }, 1000);
}

/*
    Description:
    Add the currently selected item to the Default Wishlist
*/
function addToDefaultWishlist() {
    console.log("Add to Default Wishlist");
    console.log("Item ID: "+ itemID + " and Item Name: " + itemName);
    insertIntoWishlist("default");
    setWishlistTabBadge();
}

/*
    Description:
*/
function insertIntoWishlist(wishlistName) {
    $("#"+wishlistName+"-wishlist").append('<img id="'+itemID+'-'+itemName+'"'+'class="gallery-image" src="images/image_placeholder.png"/>');
}

/*
    Description:
    Associated to the data-after-show attribute (Recurring execution) which executes afte the data-init attribute
*/
function afterHomeViewShow(e){
    console.log("After Home View Event");
    //setWishlistIcon();
    $(".km-filter-wrap input").unbind("keypress");
    $(".km-filter-wrap input").keypress(function(event) {
        var code = (event.keyCode ? event.keyCode : event.which);
        //alert("Code: "+code);
        if(code === 13) { //Enter keycode for 'Search' key
            setProductData(e);
        }
    });
    
    $(".km-filter-wrap input").unbind("focus");
    $(".km-filter-wrap input").focus(function(event) {
  		$("#header-search").find($(".km-filter-reset")[0]).removeClass("hidden");
  		$("#header-search").find($(".km-filter-reset")[0]).addClass("show");
        //Hide This or That option (Dual Search)
        $(".secondary-search-container").addClass("hidden");
    });

    $(".km-filter-wrap input").unbind("focusout");
    $(".km-filter-wrap input").focusout(function(event) {
        //console.log("input no longer focused");
        if($(".km-filter-wrap input").val() === "") {
            $("#header-search").find($(".km-filter-reset")[0]).removeClass("show");
            $("#header-search").find($(".km-filter-reset")[0]).addClass("hidden");
            //Show this or that option (Dual Search)
            $(".secondary-search-container").removeClass("hidden");
        }
    });
    document.getElementById("search-reset-btn").removeEventListener("touchstart", function(){});
    document.getElementById("search-reset-btn").removeEventListener("touchmove", function(){});
    //TODO
    addTouchSMEvents("search-reset-btn", "green-text");

    document.getElementById("search-reset-btn").removeEventListener("touchend", function(){});
    document.getElementById("search-reset-btn").addEventListener("touchend", function (evt) {
        console.log();
        if( (evt.changedTouches[0].clientX >= orignalSearchResetX-10 && evt.changedTouches[0].clientX <= orignalSearchResetX+10) && (evt.changedTouches[0].clientY >= orignalSearchResetY-10 && evt.changedTouches[0].clientY <= orignalSearchResetY+10) ) {
            console.log("touchend");
            $("#search-reset-btn span").removeClass("green-text"); 
            $("#header-search").find($(".km-filter-reset")[0]).removeClass("show");
            $("#header-search").find($(".km-filter-reset")[0]).addClass("hidden");
            //Reset the input to empty
            $(".km-filter-wrap input").val("");
            e.view.element.find("#products-search-listview").data("kendoMobileListView").replace([ "No Products shown..." ]);
            $("#products-search-listview>li:first-child").addClass("text-center");
            //Show this or that option (Dual Search)
            $(".secondary-search-container").removeClass("hidden");
        }
		
    });
    
    $("#search-reset-btn>span:first-child").attr("style","content: '/\e031'");
    document.getElementById("dual-search-img").removeEventListener("touchend", function(){});

    document.getElementById("dual-search-img").addEventListener("touchend", function (evt) {
        APP.instance.navigate("thisorthat-view", "zoom");
    });
}

/*
    Description:
    Associated to the data-init attribute Runs first time before UI applied
*/
function homeViewInit(e) {
    console.log("home-view init");
    checkLocalCredentials();
    e.view.element.find("#products-search-listview").data("kendoMobileListView").append([ "No Products shown..." ]);
    $("#products-search-listview>li:first-child").addClass("text-center");
}

/*
    Description:
    Set the wishlist icon in place for the tabstrip (footer)
*/
function setWishlistIcon() {
    //$(".km-footer").find("a:nth-child(2)>span:first-child").removeClass("km-icon");
    //$(".km-footer").find("a:nth-child(2)>span:first-child").removeClass("km-wish-list");
    $(".km-footer").find("a:nth-child(2)>span:first-child").addClass("icon-wishlist");
}

/*
    Description: add touch start and Move events
*/
function addTouchSMEvents(element, className) {
    document.getElementById(element).addEventListener("touchstart", function (evt) {
        var originalX = evt.changedTouches[0].clientX;
        var originalY = evt.changedTouches[0].clientY;
        
        if (element === "search-reset-btn"){
            $("#" + element + " span").addClass(className);
            orignalSearchResetX = originalX;
            orignalSearchResetY = originalY;
        }
        else if (element === "facebook-login-btn") {
            $("#" + element).addClass(className);
            orignalFacebookBtnX = originalX;
            orignalFacebookBtnY = originalY;
        }
        //evt.preventDefault();
    });

    document.getElementById(element).addEventListener("touchmove", function (evt) {
        if (element === "search-reset-btn"){
            $("#" + element + " span").removeClass(className);
        }
        else if (element === "facebook-login-btn") {
            $("#" + element).removeClass(className);

        }
        //evt.preventDefault();
    });
}
