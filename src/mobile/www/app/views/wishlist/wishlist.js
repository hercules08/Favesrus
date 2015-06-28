define([
  'views/view',
  'text!views/wishlist/wishlist.html'
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

    $.subscribe('/newCategory/add', function (e, text) {
        categories.add({ name: text });
    });

});

var wishlistShowCounter = 0;
var previousView = "";

//Execute after the DOM finishes loading
$(
	function () {
	}
);

/*
	Description: 
	Invoked when the 'data-after-show' event is triggered associated to the wishlist view
*/
function afterWishlistViewShow(e) {
	//setWishlistIcon();
    showElement("#wishlist-view #header-search", false);
    //if the previous view was thisorthat-view, display backbutton
    if(previousView.toLowerCase() === "thisorthat") {
        previousView = "";
        //$("#wishlist-view #wishlist-backbtn").removeClass("hidden");
        showElement("#wishlist-view #wishlist-backbtn", true);
    }
    else {
        // console.log("Not from This or That?!");
        //$("#wishlist-view #wishlist-backbtn").addClass("hidden");
        showElement("#wishlist-view #wishlist-backbtn", false);
    }
    //Open Login View if user hasn't sign in
    showLoadingAnimation(true, "Loading...");
    setTimeout(function() {
        showLoadingAnimation(false, "");
        if ((localStorage.loginstatus === "false") || (localStorage.loginstatus === undefined)) {
            //APP.instance.navigate("#login-view", "overlay:down"); //Uncomment for Testing
            //console.log("login-view Disabled");
        }
        else {
            console.log("You have already logged in!");
            //TODO Attempt to auth user
            //TODO attempt to GET wishlist items
        }
    }, 500);

	if ((wishlistShowCounter == 0) && (localStorage.loginstatus === "false") || (localStorage.loginstatus === undefined)){
        enableButtonTouchEventListeners("login");
        wishlistShowCounter = 1;
	}

    //kendo.unbind("#wishlist-toolbar>a:first-child");
    kendo.destroy("#wishlist-toolbar>a:first-child");
    $("#wishlist-toolbar>a:first-child").kendoTouch({
        tap: function (evt) {
            console.log("edit all items");
            $(".delete").toggleClass("invisible");
            $("#wishlist-toolbar>a:first-child").toggleClass("green-text");
        }
    });

    kendo.destroy("#wishlist-toolbar>a:nth-child(2)");
    $("#wishlist-toolbar>a:nth-child(2)").kendoTouch({
        tap: function (evt) {
            console.log("make list private or public");
            //TODO Check if the class is present, then remove
            if($("#wishlist-toolbar>a:nth-child(2)").hasClass("fa-lock") === true) {
                console.log("unlock");
                $("#wishlist-toolbar>a:nth-child(2)").removeClass("fa-lock");
                $("#wishlist-toolbar>a:nth-child(2)").addClass("fa-unlock");
                $("#wishlist-toolbar>a:nth-child(2)").toggleClass("green-text");
            }
            else if ($("#wishlist-toolbar>a:nth-child(2)").hasClass("fa-unlock") === true) {
                console.log("lock");
                $("#wishlist-toolbar>a:nth-child(2)").removeClass("fa-unlock");
                $("#wishlist-toolbar>a:nth-child(2)").addClass("fa-lock");
                $("#wishlist-toolbar>a:nth-child(2)").toggleClass("green-text");
            }
            
        }
    });

    //kendo.unbind("#wishlist-toolbar>a:first-child");
    kendo.destroy(".delete");
    $(".delete").kendoTouch({
        tap: function (evt) {
            console.log("delete");
            console.log($(this)[0].element.parent());
            $(this)[0].element.parent().remove();
            setWishlistTabBadge("#wishlist-view");
        }
    });

    //kendo.unbind("#wishlist-toolbar>a:first-child");
    kendo.destroy(".gallery-image");
    $(".gallery-image").kendoTouch({
        tap: function (evt) {
            console.log("view more information");
            console.log($(this)[0].element.parent());
            APP.instance.navigate("#wishlist-retailers-view", "fade")
        }
    });
}

/*
    Description: Increment Wishlist icon Badge in the Tabstrip
*/
function setWishlistTabBadge(currentView) {
    //Multiple tabstrips used
    /*$.each($(".tabstrip"), function (index, element) {
        if($("#wishlist-view .gallery").find("img").length > 0) {
            console.log($(this));
            $(this).data("kendoMobileTabStrip").badge(1,$("#wishlist-view .gallery").find("img").length);
            console.log("increase wishlist badge");
        }
    });*/
//TODO Remove variable and add strings back into statements
console.log(currentView);
console.log('"' + currentView + ' .tabstrip a:nth-child(2)' + '"');
    if($(currentView + ' .tabstrip a:nth-child(2)').children()[0].tagName.toLowerCase() === "img") {
        console.log("if");
        if ($(currentView + ' .tabstrip a:nth-child(2) span.km-badge').length === 0) {
            $(currentView + ' .tabstrip a:nth-child(2) img').after('<span class="km-badge">1</span>');
        }
        else {
            $(currentView + ' .tabstrip').data("kendoMobileTabStrip").badge(1,$("#wishlist-view .gallery").find("img").length);
        }
    }
    else {
        console.log("else");
        $(currentView + ' .tabstrip').data("kendoMobileTabStrip").badge(1,$("#wishlist-view .gallery").find("img").length);
    }
}

function notificationCallback(){

}


/* -------------------- Wishlist Retailers View -------------*/
function afterWishlistRetailersViewShow(e) {
    showElement("#wishlist-retailers-view #header-search", false);
    showLoadingAnimation(true, "Loading...");
    var template = kendo.template($("#wishlistProductDetailsTemplate").html());
    var temp_data = '{ "id": "3545354", "name": "Disney INFINITY: Disney Originals (2.0 Edition) Crystal Sorcerer Mickey Figure", "image": "http://www.gamestop.com/common/images/lbox/102788b.jpg", "description": "Feeling mischievous? Join Sorcerer\'s Apprentice Mickey\'s spellbinding high jinks. With his magic sweep and bursts, he\'s got more moves under his Sorcerer\'s hat than a wizard in a wand shop. Abracadabra!", "numLikes": "6", "retailers": [ { "id": "12332", "name": "BestBuy", "image": "http://upload.wikimedia.org/wikipedia/commons/thumb/f/f5/Best_Buy_Logo.svg/300px-Best_Buy_Logo.svg.png", "price": "$12.99", "rating":"5", "numReviews": "500", "lowest": "0" }, { "id": "12334", "name": "Walmart", "image": "http://upload.wikimedia.org/wikipedia/commons/thumb/7/76/New_Walmart_Logo.svg/1000px-New_Walmart_Logo.svg.png", "price": "$12.95", "rating":"3", "numReviews": "305","lowest": "1" }, { "id": "12335", "name": "Sears", "image": "http://www.buyvia.com/i/2013/10/Sears-logo.png", "price": "$10.95", "rating":"4", "numReviews": "400", "lowest": "0" } ], "numRetailers": "3", "avgRating": "3", "numAvgReviews": "500" }';
    var data = JSON.parse(temp_data);
    var result = template(data); //Execute the template
    $("#wishlist-retailers-view #product-details-container").html(result); //Append the result
    template = kendo.template($("#wishlistProductRetailersTemplate").html());
    console.log(data.retailers[0].rating);
    result = template(data.retailers);
    $("#wishlist-retailers-view #product-retailers-listview").html(result); //Append the result
    showLoadingAnimation(false, "Loading...");

    //kendo.unbind("#wishlist-toolbar>a:first-child");
    kendo.destroy(".fa-trash");
    $(".fa-trash").kendoTouch({
        tap: function (evt) {
            if (navigator.notification) {
                navigator.notification.confirm (
                    "Are you sure?",    // message
                    notificationCallback,                             // callback
                    'Remove from Wishlist',                                  // title
                    ['Yes','No']                                       // buttonName
                );
            }
            else {
                alert("Are you sure?");
            }
        }
    });

    //kendo.unbind("#wishlist-toolbar>a:first-child");
    kendo.destroy(".fa-shopping-cart");
    $(".fa-shopping-cart").kendoTouch({
        tap: function (evt) {
            try {
                /*var ref = window.open('http://www.bestbuy.com/site/disney-interactive-disney-infinity-figure-sorcerers-apprentice-mickey-multi/3650039.p?id=1219092665944&skuId=3650039', '_blank', 'location=no');
                ref.addEventListener("exit", function(){
                    if (navigator.notification) {
                        navigator.notification.confirm(
                            "Are you sure?",    // message
                            notificationCallback,                             // callback
                            'Remove from Wishlist',                                  // title
                            ['Yes','No']                                        // buttonName
                        );
                    }
                    else {
                        alert("Are you sure?");
                    }
                });*/
                cordova.ThemeableBrowser.open('http://www.bestbuy.com/site/disney-interactive-disney-infinity-figure-sorcerers-apprentice-mickey-multi/3650039.p?id=1219092665944&skuId=3650039', '_blank', {
                    statusbar: {
                        color: '#16a085'
                    },
                    toolbar: {
                        height: 44,
                        color: '#16a085'
                    },
                    title: {
                        color: '#ffffff',
                        showPageTitle: true
                    },
                    /*backButton: {
                        image: 'back',
                        imagePressed: 'back_pressed',
                        align: 'left',
                        event: 'backPressed'
                    },
                    forwardButton: {
                        image: 'forward',
                        imagePressed: 'forward_pressed',
                        align: 'left',
                        event: 'forwardPressed'
                    },*/
                    closeButton: {
                        wwwImage: 'images/x-icon-64.png',
                        wwwImagePressed: 'images/x-icon-64.png',
                        wwwImageDensity: 2,
                        align: 'left',
                        event: 'closePressed'
                    }/*,
                    customButtons: [
                        {
                            image: 'share',
                            imagePressed: 'share_pressed',
                            align: 'right',
                            event: 'sharePressed'
                        }
                    ],
                    menu: {
                        image: 'menu',
                        imagePressed: 'menu_pressed',
                        title: 'Test',
                        cancel: 'Cancel',
                        align: 'right',
                        items: [
                            {
                                event: 'helloPressed',
                                label: 'Hello World!'
                            },
                            {
                                event: 'testPressed',
                                label: 'Test!'
                            }
                        ]
                    },
                    backButtonCanClose: true*/
                }).addEventListener('closePressed', function(e) {
                    if (navigator.notification) {
                        navigator.notification.confirm(
                            "Are you sure?",    // message
                            notificationCallback,                             // callback
                            'Remove from Wishlist',                                  // title
                            ['Yes','No']                                        // buttonName
                        );
                    }
                    else {
                        alert("Are you sure?");
                    }
                }).addEventListener(cordova.ThemeableBrowser.EVT_ERR, function(e) {
                    console.error(e.message);
                }).addEventListener(cordova.ThemeableBrowser.EVT_WRN, function(e) {
                    console.log(e.message);
                });

                /*
                //No longer needed
                $(".fa-shopping-cart").toggleClass("green-text");
                setTimeout(function(){
                    $(".fa-shopping-cart").toggleClass("green-text");
                },500);
                */
            }
            catch(ex) {
                alert("Exception: Open in Themeable InApp Browser!");
            }
        }
    });
}




