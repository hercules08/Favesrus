//Execute after the DOM finishes loading
$(
    function () {
        //Add the padding styling to the first button on the Login View
        $("#login_container img").first().css("padding-bottom", "10vh");
        
        //Add the padding-top styling to the first button on the Main Menu View
        $("#mainmenu_container img").first().css("padding-bottom", "5vh");
        
        $("a img").css("width","100%");
    }
);

//Close the SignUp Modal View
function closeModalViewLogin() {
    $("#signup-modalview").kendoMobileModalView("close");
}

//Open the SignUp Modal View
function openModalView(e) {
    $("#signup-modalview").data("kendoMobileModalView").open();
    //Add the month to the <select> tag
    var monthsOfYear = ["Feb", "March", "April", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec"];
    /*TODO This is not working; Unable to create dropdown*/
    /*$("#birthday").kendoDropDownList({
                        dataTextField: "text",
                        dataValueField: "value",
                        dataSource: [{ text: "Jan", value: "Jan" }]
                    });
    var dropdownlist = $("#birthday").data("kendoDropDownList");*/
    
    for (var i = 0;i < monthsOfYear.length; i++){
        /*option = document.createElement("option");
        option.text = monthsOfYear[i];
        option.value = monthsOfYear[i];
        select = document.getElementsByName("month")[0];
        select.appendChild(option);*/
        
        
        //dropdownlist.dataSource.add({ text: monthsOfYear[i], value: monthOfYear[i] });
    }
    
    
}


