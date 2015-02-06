FavesApp.signupModel = (function () {

    //ViewModel for Sign Up View
    var viewModel = kendo.observable({
        userName: "",
        password: "",
        confirmPassword: "",
        userRegister: function () {
            var registerOptions = {
                url: FavesApp.configuration.accountUrl + "Register",
                requestType: "POST",
                dataType: "JSON",
                data: {
                    userName: viewModel.get("userName"),
                    password: viewModel.get("password"),
                    confirmPassword: viewModel.get("confirmPassword")
                },
                callBack: this.fnRegisterCallBack
            };

            FavesApp.dataAccess.callService(registerOptions);
        },

        //method for user registration
        fnRegisterCallBack: function (result) {
            if (result.success == true) {
                FavesApp.common.navigateToView("#faves-email-like-selection-view");
            } else {
                //any error handling code
                alert("There were issues :(");
            }
        }
    });

    return {
        viewModel: viewModel
    }
})();