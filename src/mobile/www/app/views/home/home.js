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
var itemID, itemName, itemImageSrc, itemCategoryName;
var cancelBtnTapped = false;
//var productResultsData = "";

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
function setSelectedItemInfo(id, name, src, category) {
    itemID = id;
    itemName = name;
    itemImageSrc = src;
    itemCategoryName = category;
}

/*
    Description:
    Set the appropriate information in the datasource for products list view display
*/
function setProductData(e) {
        //Add Products to list view
        console.log("Search text: "+$(".km-filter-wrap input").val());
        webService("getgiftitemswithterm",'searchText='+$(".km-filter-wrap input").val());
        $(".km-filter-wrap input").blur();
        hideMobileKeyboard();
        showLoadingAnimation(true, "Loading...");
}


/*
    Description:

*/
/*function getCategoryName(itemId, data) {
    // iterate over each element in the array
    for (var i = 0; i < data.length i++){
      // look for the entry with a matching `code` value
      if (data[i].id == itemId){
         // we found it
        console.log("item found in data");
      }
    }
    var name = "";
    return name;
}*/


/*
    Description:
    Add the currently selected item to the Default Wishlist
*/
function addToWishlistFrom(sourceView) {
    console.log("Add to Wishlist");
    console.log("Item ID: "+ itemID + " and Item Name: " + itemName + " | Category: " + itemCategoryName);
    insertIntoWishlist("default");
    if(sourceView === "#home-view") {
        setWishlistTabBadge(sourceView);
    } 
}

/*
    Description:
*/
function insertIntoWishlist(wishlistName) {
    console.log("insertIntolWishlist & # of Group Headers: "+$("#"+wishlistName+"-wishlist").find($(".group-header")).length);

    if($("#"+wishlistName+"-wishlist").find($(".group-header")).length > 0){ //Category header exist 
        for(var i = 0; i < ($("#"+wishlistName+"-wishlist").find($(".group-header")).length); i++) {
            console.log("# of Group Header" + $("#"+wishlistName+"-wishlist").find($(".group-header")).length + " & i: " + i);
            if($("#"+wishlistName+"-wishlist").find($(".group-header")[i]).html() === itemCategoryName) {
                //console.log("The #"+(i+1)+" group-header has the word Other in it");
                console.log("Category found in wishlist");
                //Insert product after category
                $('<div class="gallery-image-container"><div class="delete invisible"><a data-role="button" class="km-widget km-button"><span class="km-icon km-close-btn km-notext"></span></a></div><span><img id="'+itemID+'-product"'+'class="gallery-image" src="'+itemImageSrc+'"/></span></div>').insertAfter($("#"+wishlistName+"-wishlist").find($(".group-header")[i]));
                i = ($("#"+wishlistName+"-wishlist").find($(".group-header")).length + 1); //End loop
            }
            if(i == ($("#"+wishlistName+"-wishlist").find($(".group-header")).length - 1)) {//if category wasn't found
                console.log("Category not found in wishlist & Category created; Not First");
                $("#"+wishlistName+"-wishlist").append("<div class='group-header'>" + itemCategoryName + "</div>");
                $("#"+wishlistName+"-wishlist").append('<div class="gallery-image-container"><div class="delete invisible"><a data-role="button" class="km-widget km-button"><span class="km-icon km-close-btn km-notext"></span></a></div><span><img id="'+itemID+'-product"'+'class="gallery-image" src="'+itemImageSrc+'"/></span></div>');
                i = ($("#"+wishlistName+"-wishlist").find($(".group-header")).length + 1); //End loop
            }
        }
    }
    else { //first group header
        console.log("First Category created in wishlist");
        $("#"+wishlistName+"-wishlist").append("<div class='group-header'>" + itemCategoryName + "</div>");
        $("#"+wishlistName+"-wishlist").append('<div class="gallery-image-container"><div class="delete invisible"><a data-role="button" class="km-widget km-button"><span class="km-icon km-close-btn km-notext"></span></a></div><span><img id="'+itemID+'-product"'+'class="gallery-image" src="'+itemImageSrc+'"/></span></div>');
    }

    //$("#"+wishlistName+"-wishlist").append('<div class="gallery-image-container"><div class="delete invisible"><a data-role="button" class="km-widget km-button"><span class="km-icon km-close-btn km-notext"></span></a></div><img id="'+itemID+'-product"'+'class="gallery-image" src="'+itemImageSrc+'"/></div>');
    
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
        if(code === 13) { //Enter keycode for 'Search' key
            if ($(".km-filter-wrap input").val() !== "")
                setProductData(e);
        }
    });
    
    $(".km-filter-wrap input").unbind("focus");
    $(".km-filter-wrap input").focus(function(event) {
        console.log("search input focus");
        //if (cancelBtnTapped === false) { //Cancel button tapped
            //console.log("Cancel button NOT pressed");
            $("#home-view .cover").removeClass("hidden");
            $("#home-view .km-scroll-wrapper").data("kendoMobileScroller").disable();//disable scrolling
            if ($(".km-filter-wrap input").val() !== "")
                $("#header-search").find($(".km-filter-reset")[0]).removeClass("hidden");
            else
                $(".km-filter-wrap input").attr("placeholder", "");
            if ($(".km-filter-wrap").hasClass("active-search") === false)
                $(".km-filter-wrap").addClass("active-search"); //shrink
            setTimeout(function() {
                $("#header-search div:last-child").addClass("show");
            }, 850);
            //$("#home-container").addClass("hidden"); //Keep default home view element on screen
        //}
        /*else { //Cancel
            console.log("Cancel button pressed");
            cancelBtnTapped = false;
            //$(".km-filter-wrap input").blur();
        }*/
    });

    $(".km-filter-wrap input").unbind("keyup");
    $(".km-filter-wrap input").keyup(function() {
        if ($(".km-filter-wrap input").val() !== "")
            $("#header-search").find($(".km-filter-reset")[0]).removeClass("hidden");
        else
            $("#header-search").find($(".km-filter-reset")[0]).addClass("hidden");
    });

    $(".km-filter-wrap input").unbind("focusout");
    $(".km-filter-wrap input").focusout(function(event) { //Triggered via blur()
        console.log("input no longer focus");
        console.log("remove reset button");
        $("#header-search").find($(".km-filter-reset")[0]).removeClass("show");
        $("#header-search").find($(".km-filter-reset")[0]).addClass("hidden");
        $(".cover").addClass("hidden");
        $("#home-view .km-scroll-wrapper").data("kendoMobileScroller").enable();//enable scrolling
        if($(".km-filter-wrap input").val() === "") {
            // setTimeout(function() {
                // $(".km-filter-wrap").removeClass("active-search");
            $(".km-filter-wrap input").attr("placeholder", "Search Products or Categories");
            // },50);
            $("#header-search div:last-child").removeClass("show"); //hide Cancel
            $(".km-filter-wrap").removeClass("active-search"); // expand input
            //reset list
            if($("#products-search-listview").hasClass("hidden") === false) {
                e.view.element.find("#products-search-listview").data("kendoMobileListView").replace([ "" ]);
                $("#products-search-listview").addClass("hidden");
            }
            $("#home-container").removeClass("hidden"); //show default containers
            //APP.instance.view().scroller.reset(); //reset Scroller
        }
        else {
            console.log("input not empty");

        }
        //console.log("\n");
    });

    //Add tap events
    //kendo.unbind("#search-reset-btn");
    kendo.destroy("#search-reset-btn");
    $("#search-reset-btn").kendoTouch({
        tap: function (evt) {
            //$(".cover").addClass("hidden");
            $(".km-filter-wrap input").val("");
            $("#header-search").find($(".km-filter-reset")[0]).addClass("hidden");
        }
    });
    
    //kendo.unbind("#header-search div:last-child");
    kendo.destroy("#header-search div:last-child");
    $("#header-search div:last-child").kendoTouch({ //Search Cancel button
        tap: function (evt) {
            console.log("Cancel Search");
            cancelBtnTapped = true;
            $("#header-search div:last-child").removeClass("show");
            $(".km-filter-wrap input").val("");
            //$(".km-filter-wrap").removeClass("active-search"); //expand
            //console.log("input expand");
            $(".km-filter-wrap input").attr("readonly","readonly");
            console.log("input readonly");
            setTimeout(function() {
                $(".km-filter-wrap input").blur();
                $(".km-filter-wrap input").removeAttr("readonly");
                console.log("blur input");
            }, 850);
        }
    });

    $("#search-reset-btn>span:first-child").attr("style","content: '/\e031'");

    //Test
    //kendo.unbind("#"+e.data[i].id+"-img");
    kendo.destroy("#upcoming-events-scrollview>div>div:first-child div:nth-child(2) .event");
    $("#upcoming-events-scrollview>div>div:first-child div:nth-child(2) .event").kendoTouch({
        tap: function (evt) {
            //Default/Testing
            APP.instance.navigate("#home-event-view", "overlay:down");
            showElement("#home-event-view #header-search", false);
        }
    })
}

/*
    Description: set Product Category Data from webservice call
*/
//TODO
function setProductCategoriesData() {
    
}


/*
    Description:
    Associated to the data-init attribute Runs first time before UI applied
*/
function homeViewInit(e) {
    console.log("home-view init");
    checkLocalCredentials();
    //Set Product Categories
    setProductCategoriesData();
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
    Description: Set the server's response data into the Home View List View List elements
*/
function setHomeViewProductData(webServiceResponse) {
    webServiceResponse = shortenText(webServiceResponse, "name");
    data = webServiceResponse;
    // console.log(JSON.stringify(data));
    $("#home-view .cover").addClass("hidden");
    $("#home-view .km-scroll-wrapper").data("kendoMobileScroller").enable();//enable scrolling
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
                    setSelectedItemInfo($(this).parent().prev().find(".item-name").attr("id"), $(this).parent().prev().find(".item-name").html(), $(this).parent().prev().prev().find(".item-image").attr("src"), $(this).parent().parent().next().find(".item-category").html());
                    //$("#addItem-actionsheet").data("kendoMobileActionSheet").open();
                    addToWishlistFrom("#home-view");
                }, 
            false);
        });
    }
    else {
        //TODO
        $("#home-container").addClass("hidden");
        $("#products-search-listview").removeClass("hidden");
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
            $("#home-view .km-scroll-wrapper").data("kendoMobileScroller").disable();//disable scrolling
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
    //console.log("Show"+element);
    if (status === true)
        $(element).removeClass("hidden");
    else if (status === false)
        $(element).addClass("hidden");

    //console.log($(element));
}

function afterHomeEventViewShow(e) {

}