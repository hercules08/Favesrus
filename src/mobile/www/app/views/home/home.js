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
        e.view.element.find("#products-search-listview").data("kendoMobileListView").replace([ "Mario Amiibo", "Fox Amiibo", "Link Amiibo" ]);
        /*if(cordova){
            cordova.plugins.Keyboard.close();
        }*/
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
        });

        $("#search-reset-btn").unbind("click");
		$("#search-reset-btn").click(function(){
			$("#header-search").find($(".km-filter-reset")[0]).removeClass("show");
      		$("#header-search").find($(".km-filter-reset")[0]).addClass("hidden");
      		//Reset the input to empty
      		$(".km-filter-wrap input").val("");
      		e.view.element.find("#products-search-listview").data("kendoMobileListView").replace([ "No Products shown..." ]);
		});
        //homeShowCounter++;
    //}
    //if(homeShowCounter === 0) {
        $("#search-reset-btn>span:first-child").attr("style","content: '/\e031'");
        /*homeShowCounter++;
    }*/
    //alert($(".km-filter-wrap input")[0].outerHTML);
    //console.log("Keypress Registered: "+ (jQuery._data( $(".km-filter-wrap input")[0], "events" ).keypress !== undefined));
}

/*
    Description:
    Associated to the data-init attribute Runs first time
*/
function viewInit(e) {
    console.log("home-view init");
    checkLocalCredentials();
    e.view.element.find("#products-search-listview").kendoMobileListView({
		dataSource: [ "No Products shown..." ],
		/*filterable: {
		     placeholder: "Search for products..",
		     autoFilter: false
		}*/
    });
    $("#products-search-listview>li:first-child").addClass("text-center");
}
