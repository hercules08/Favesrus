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
        /*e.view.element.find("#products-search-listview").data("kendoMobileListView").replace([ "Mario Amiibo", "Fox Amiibo", "Link Amiibo" ]);
        */
        var template = kendo.template($("#productSearchTemplate").html()); //Get the external template definition
        var temp_data = '{"Status":"recommendations", "Model":{"items":[{"id":"345435","name":"Mario Amiibo","image":"images/image_placeholder.png", "description":"Interactive Play with Nintendo console games"},{"id":"3545354","name":"Luigi Amiibo","image":"images/image_placeholder.png", "description":"Interactive Play with Nintendo console games"},{"id":"3454363","name":"Peach Amiibo","image":"images/image_placeholder.png", "description":"Interactive Play with Nintendo console games"},{"id":"3454398","name":"Wario Amiibo","image":"images/image_placeholder.png", "description":"Interactive Play with Nintendo console games"},{"id":"3454005","name":"Pit Amiibo","image":"images/image_placeholder.png", "description":"Interactive Play with Nintendo console games"}]}}';
        //var data = ["Recommendation1", "Recommendation2", "Recommendation3", "Recommendation4", "Recommendation5"]; //Create some dummy data
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
    }, 1000);
}

/*
    Description:
    Associated to the data-after-show attribute (Recurring execution) which executes afte the data-init attribute
*/
function afterHomeViewShow(e){
    console.log("After Home View Event");
    setWishlistIcon();
    //alert($("form").length+"Forms");
    //Remove unnecssary search box if needed
    /*for(var i =0; i < $("form").length; i++){
    	if ($("#home-view").find($("form")[i]).attr("id") === "header-search") {
    	}
    	else {
    		$("#home-view").find($("form")[i]).css("display","none")
    	}
    }*/
    //if (homeShowCounter === 0){
    	//console.log("homeShowCounter "+homeShowCounter);
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

        $("#search-reset-btn").unbind("click");
		$("#search-reset-btn").click(function(){
			$("#header-search").find($(".km-filter-reset")[0]).removeClass("show");
      		$("#header-search").find($(".km-filter-reset")[0]).addClass("hidden");
      		//Reset the input to empty
      		$(".km-filter-wrap input").val("");
      		e.view.element.find("#products-search-listview").data("kendoMobileListView").replace([ "No Products shown..." ]);
            $("#products-search-listview>li:first-child").addClass("text-center");
            //Show this or that option (Dual Search)
            $(".secondary-search-container").removeClass("hidden");
        });
        //homeShowCounter++;
    //}
    //if(homeShowCounter === 0) {
        $("#search-reset-btn>span:first-child").attr("style","content: '/\e031'");
        /*homeShowCounter++;
    }*/
    $("#dual-search-img").unbind("click");
    $("#dual-search-img").click(function(){
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
    $(".km-footer").find("a:nth-child(2)>span:first-child").removeClass("km-icon");
    $(".km-footer").find("a:nth-child(2)>span:first-child").removeClass("km-wish-list");
    $(".km-footer").find("a:nth-child(2)>span:first-child").addClass("icon-wishlist");
}
