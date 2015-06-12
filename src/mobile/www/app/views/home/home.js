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
var itemID, itemName, itemImageSrc, orignalSearchResetX = 0, orignalSearchResetY = 0;
var searchResetBtnPressed = false;

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
    Description: Set the relevant item information for the selected product to be added to your wishlist
*/
function setSelectedItemInfo(id, name, src) {
    itemID = id;
    itemName = name;
    itemImageSrc = src;
}

/*
    Description:
    Set the appropriate information in the datasource for products list view display
*/
function setProductData(e) {
        //Add Products to list view
        /*var temp_data = '{"Status":"search", "Model":{"items":[{"id":"345435","name":"Mario Amiibo","image":"http://www.gamestop.com/common/images/lbox/104546b.jpg", "description":"Interactive Play with Nintendo console games"},{"id":"3545354","name":"Luigi Amiibo","image":"http://www.gamestop.com/common/images/lbox/106342b.jpg", "description":"Interactive Play with Nintendo console games"},{"id":"3454363","name":"Peach Amiibo","image":"http://www.gamestop.com/common/images/lbox/104547b.jpg", "description":"Interactive Play with Nintendo console games"},{"id":"3454398","name":"Wario Amiibo","image":"http://i5.walmartimages.com/dfw/dce07b8c-2652/k2-_771924f6-f385-4dec-b08a-a5aa1cced6c3.v1.jpg", "description":"Interactive Play with Nintendo console games"},{"id":"3454005","name":"Pit Amiibo","image":"http://www.gamestop.com/common/images/lbox/106338b.jpg", "description":"Interactive Play with Nintendo console games"}]}}';
        var data = JSON.parse(temp_data);*/
        console.log("Search text: "+$(".km-filter-wrap input").val());
        webService("getgiftitemswithterm",'searchText='+$(".km-filter-wrap input").val());
        $(".km-filter-wrap input").blur();
        hideMobileKeyboard();
        showLoadingAnimation(true, "Loading...");

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
    $("#"+wishlistName+"-wishlist").append('<div class="gallery-image-container"><div class="delete invisible"><a data-role="button" class="km-widget km-button"><span class="km-icon km-close-btn km-notext"></span></a></div><img id="'+itemID+'-product"'+'class="gallery-image" src="'+itemImageSrc+'"/></div>');
    //TODO Create function to add info to object for sending with wishlist route
    //Add to Wishlist; Works
    /*var object = {};
    object.userId = "78219e90-1a89-4559-9838-205f53bd8aad";
    object.giftItemId = itemID;
    object.wishListId = 3;
    console.log(object);
    webService("addgiftitemtowishlist", JSON.stringify(object));*/
    //Remove Works
    /*var object = {};
    object.userId = "78219e90-1a89-4559-9838-205f53bd8aad";
    object.giftItemId = itemID;
    object.wishListId = 3;
    console.log(object);
    webService("removegiftitemfromwishlist", JSON.stringify(object));*/
}

/*
    Description:
    Associated to the data-after-show attribute (Recurring execution) which executes afte the data-init attribute
*/
function afterHomeViewShow(e){
    console.log("After Home View Event");
    showElement("#home-view #header-search", true);
    showElement("#wishlist-backbtn", false);
    //setWishlistIcon();
    $(".km-filter-wrap input").unbind("keypress");
    $(".km-filter-wrap input").keypress(function(event) {
        var code = (event.keyCode ? event.keyCode : event.which);
        //(alert)("Code: "+code);
        if(code === 13) { //Enter keycode for 'Search' key
            setProductData(e);
        }
    });
    
    $(".km-filter-wrap input").unbind("focus");
    $(".km-filter-wrap input").focus(function(event) {
        console.log("input focus");
        $("#home-view .cover").removeClass("hidden");
        if(searchResetBtnPressed === true) {
            $(".km-filter-wrap input").blur();
            //break;
            searchResetBtnPressed = false;
        }
        else {
            $("#header-search").find($(".km-filter-reset")[0]).removeClass("hidden");
            /*$("#header-search").find($(".km-filter-reset")[0]).addClass("show"); */
            //Hide This or That option (Dual Search)
            $(".secondary-search-container").addClass("hidden");
            $("#home-container").addClass("hidden");
        }
        
    });

    $(".km-filter-wrap input").unbind("focusout");
    $(".km-filter-wrap input").focusout(function(event) {
        console.log("input no longer focused");
        $(".cover").addClass("hidden");
        if($(".km-filter-wrap input").val() === "") {
            $("#header-search").find($(".km-filter-reset")[0]).removeClass("show");
            $("#header-search").find($(".km-filter-reset")[0]).addClass("hidden");
            //Show this or that option (Dual Search)
            e.view.element.find("#products-search-listview").data("kendoMobileListView").replace([ "" ]);
            $("#products-search-listview").addClass("hidden");
            $(".secondary-search-container").removeClass("hidden");
            $("#home-container").removeClass("hidden");
            APP.instance.view().scroller.reset(); //reset Scroller
        }
    });
    document.getElementById("search-reset-btn").removeEventListener("touchstart", function(){});
    document.getElementById("search-reset-btn").removeEventListener("touchmove", function(){});
    
    addTouchSMEvents("search-reset-btn", "green-text");

    document.getElementById("search-reset-btn").removeEventListener("touchend", function(){});
    document.getElementById("search-reset-btn").addEventListener("touchend", function (evt) {
        console.log();
        if( (evt.changedTouches[0].clientX >= orignalSearchResetX-10 && evt.changedTouches[0].clientX <= orignalSearchResetX+10) && (evt.changedTouches[0].clientY >= orignalSearchResetY-10 && evt.changedTouches[0].clientY <= orignalSearchResetY+10) ) {
            console.log("touchend search-reset-btn");
            $(".cover").addClass("hidden");
            $(".km-filter-wrap input").blur();
            searchResetBtnPressed = true;
            $(".km-filter-wrap input").val("");
            $("#search-reset-btn span").removeClass("green-text"); 
            $("#header-search").find($(".km-filter-reset")[0]).removeClass("show");
            $("#header-search").find($(".km-filter-reset")[0]).addClass("hidden");
            //Reset the input to empty
            $(".km-filter-wrap input").val("");
            e.view.element.find("#products-search-listview").data("kendoMobileListView").replace([ "" ]);
            $("#products-search-listview").addClass("hidden");
            $("#products-search-listview>li:first-child").addClass("text-center");
            //Show this or that option (Dual Search)
            $(".secondary-search-container").removeClass("hidden");
            $("#home-container").removeClass("hidden");
            APP.instance.view().scroller.reset(); //reset Scroller
        }
        
    });
    
    $("#search-reset-btn>span:first-child").attr("style","content: '/\e031'");

    //Test
    //kendo.unbind("#"+e.data[i].id+"-img");
    kendo.destroy("#upcoming-events-scrollview>div>div:first-child div:nth-child(2) .event");
    $("#upcoming-events-scrollview>div>div:first-child div:nth-child(2) .event").kendoTouch({
        tap: function (evt) {
            //Default/Testing
            APP.instance.navigate("#home-event-view", "slide:right");
            showElement("#home-event-view #header-search", false);
        }
    })
}

/*
    Description:
    Associated to the data-init attribute Runs first time before UI applied
*/
function homeViewInit(e) {
    console.log("home-view init");
    checkLocalCredentials();
    // Set the data for Upcoming Events
    //TODO
    //Set data for Popular Categories
    //Testing
    var temp_data='{ "Status": "categories", "model": { "items": [ { "id": "1", "name": "Smart Watches", "image": "http://ecx.images-amazon.com/images/I/81Qkcobv5oL._SL1500_.jpg", "color": "black" }, { "id": "2", "name": "Swimwear (Women)", "image": "http://slimages.macys.com/is/image/MCY/products/0/optimized/1818660_fpx.tif?wid=262&hei=320&fit=fit,1&$filtersm$", "color": "river-blue" }, { "id": "3", "name": "Designer Bags", "image": "http://slimages.macys.com/is/image/MCY/products/3/optimized/2583223_fpx.tif?wid=262&hei=320&fit=fit,1&$filtersm$", "color": "alizarin" }, { "id": "4", "name": "Fantasy (Books)", "image": "http://ecx.images-amazon.com/images/I/81U3NakcDKL._SL1500_.jpg", "color": "amethyst" }, { "id": "5", "name": "Sunglasses", "image": "http://slimages.macys.com/is/image/MCY/products/5/optimized/1027325_fpx.tif?wid=262&hei=320&fit=fit,1&$filtersm$", "color": "carrot-orange" }, { "id": "6", "name": "Sandals(Women)", "image": "http://slimages.macys.com/is/image/MCY/products/7/optimized/2478147_fpx.tif?wid=262&hei=320&fit=fit,1&$filtersm$", "color": "sunflower-yellow" } ] } }'
    var data = JSON.parse(temp_data);
    setHomeViewPopularData(data);
    //webService TODO
    //webService("getpopularcategories", "");
}

/*
    Description: add touch start and Move events
*/
function addTouchSMEvents(element, className) {
//function addTouchSMEvents(element, className, TouchBtnX0, TouchBtnY0) { //Use this options TODO 
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
        /*else if (element === "facebook-login-btn") {
            $("#" + element).addClass(className);
            orignalFacebookBtnX = originalX;
            orignalFacebookBtnY = originalY;
        }*/
        else if (element === "account-login-btn") {
            $("#" + element).addClass(className);
            orignalAccountBtnX = originalX;
            orignalAccountBtnY = originalY;
        }
        else if (element === "account-logout-btn") {
            $("#" + element).addClass(className);
            originalLogOutBtnX = originalX;
            originalLogOutBtnY = originalY;
        }
        else {
            $("#" + element).addClass(className);
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
        else if (element === "account-login-btn") {
            $("#" + element).removeClass(className);
        }
        else if (element === "account-logout-btn") {
            $("#" + element).removeClass(className);
        }
        else {
            $("#" + element).removeClass(className);
        }
        //evt.preventDefault();
    });
}

/*
    Description: Set the server's response data into the Home View List View List elements
*/
function setHomeViewProductData(webServiceResponse) {
    webServiceResponse = shortenText(webServiceResponse, "name");
    var data = webServiceResponse;
    // console.log(JSON.stringify(data));
    $("#home-view .cover").addClass("hidden");
    if (data.model.items.length > 2) {
        var template = kendo.template($("#productSearchTemplate").html()); //Get the external template definition
        console.log("Inside the template: "+JSON.stringify(data));
        var result = template(data); //Execute the template
        //console.log(data);
        $("#home-container").addClass("hidden");
        $("#products-search-listview").removeClass("hidden");
        APP.instance.view().element.find("#products-search-listview").html(result); //Append the result
        APP.instance.view().scroller.reset(); //reset Scroller
        //Add the touch event listener to all product add gift icons
        $.each($("#home-view .product-add"), function (index, element) {
            element.addEventListener("touchstart", function (evt) {
                    setSelectedItemInfo($(this).parent().prev().find(".item-name").attr("id"), $(this).parent().prev().find(".item-name").html(), $(this).parent().prev().prev().find(".item-image").attr("src"));
                    $("#addItem-actionsheet").data("kendoMobileActionSheet").open();
                    //evt.preventDefault(); 
                }, 
            false);
        });
    }
    else {
        //TODO
        APP.instance.view().element.find("#products-search-listview").data("kendoMobileListView").replace([ "No products available!!" ]);
        $("#products-search-listview>li:first-child").addClass("text-center");
        //console.log("No items available.");
    }
    showLoadingAnimation(false, "");
}

/*
    Description: Set the server's response data into the Home View Categories Squares
*/
function setHomeViewPopularData(webServiceResponse){
    var data = webServiceResponse;
    var template = kendo.template($("#popularCategoriesTemplate").html()); //Get the external template definition
    console.log("Popular Categories Data: "+JSON.stringify(data));
    var result = template(data); //Execute the template
    //console.log(data);
    $("#home-popular-categories-container").html(result); //Append the result
    //Add tap events
    //kendo.unbind("#"+e.data[i].id+"-img");
    kendo.destroy("#home-popular-categories-container>div");
    $("#home-popular-categories-container>div").kendoTouch({
        tap: function (evt) {
            //Default/Testing
            $(".km-filter-wrap input").val("watch")
            console.log("Search text: "+$(".km-filter-wrap input").val());
            webService("getgiftitemswithterm",'searchText='+$(".km-filter-wrap input").val());
            showLoadingAnimation(true, "Loading...");
            $("#home-view .cover").removeClass("hidden");
        }
    })
}

/*
    Description: Hide the mobile device's native keyboard
*/
function hideMobileKeyboard() {
    try {
        if(cordova){
            cordova.plugins.Keyboard.close();
        }
    }
    catch (error){
        console.log("Cordova object is not defined.")
    }    
}

/*
    Description: Hide/Show Kendo UI Loading animation
*/
function showLoadingAnimation(status, message){
    try{
	    if (status === true) {
	        APP.instance.showLoading();
	        APP.instance.changeLoadingMessage(message);
	    }
	    else if (status === false) {
	        APP.instance.hideLoading();
	    }
	}
	catch(ex){
		console.log("Exception "+ ex);
	}
}

/*
    Description: shorten text to ideal length based on device screen width
    Parameter: data - JSON object response from server, field - the desired item to be shortened
*/
function shortenText(data, field) {
    for (var i = 0; i < data.model.items.length; i++){
        console.log(data.model.items[i][field]);
        if ((window.innerWidth > 335 && window.innerWidth < 376) && data.model.items[i][field].length > 30) {
            data.model.items[i][field] = data.model.items[i][field].slice(0,48)+"...";
        }
        else if (window.innerWidth < 336 && data.model.items[i][field].length > 30) {
            data.model.items[i][field] = data.model.items[i][field].slice(0,28)+"...";
        }
    }
    return data;
}

/*
    Description: Hide/show element
*/

function showElement(element, status) {
    console.log("Show"+element);
    if (status === true)
        $(element).removeClass("hidden");
    else if (status === false)
        $(element).addClass("hidden");

    //console.log($(element));
}

function afterHomeEventViewShow(e) {

}