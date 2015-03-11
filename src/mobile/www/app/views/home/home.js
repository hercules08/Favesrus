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
        e.view.element.find("#products-search-listview").data("kendoMobileListView").append([ "Mario Amiibo", "Fox Amiibo", "Link Amiibo" ]);
        if(cordova){
            cordova.plugins.Keyboard.close();
        }
    }, 400);
}

/*
    Description:
    Associated to the data-after-show attribute (Recurring execution) which executes afte the data-init attribute
*/
function afterHomeViewShow(e){
    if (homeShowCounter == 0){
        $(".km-filter-wrap input").keypress(function(event) {
            var code = (event.keyCode ? event.keyCode : event.which);
            if(code == 13) { //Enter keycode for 'Search' key
                setProductData(e);
            }
        });
        homeShowCounter++;
    }
}

function viewInit(e) {
    e.view.element.find("#products-search-listview").kendoMobileListView({
		dataSource: [ "No Products shown..." ],
		filterable: {
		    placeholder: "Search for products..",
		    autoFilter: false
		}
    });
    $("#products-search-listview>li:first-child").addClass("text-center");
}
