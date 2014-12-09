FavesRUS.userAccount = (function() {
	//ViewModel for User Account
	var viewModel = kendo.observable({
		isUserLoggedIn: false,
		firstName: "",
		lastName: "",
		userName: "",
		password: "",
		email: "",
		birthday: "",
		phone: "",
		gender: "",
		modoId: "",
		modoMembershipDate: "",
		cardOnFile: false,
		favFriends: [],
		userLogin: function () {
			var loginOptions = {

				url: 			FavesRUS.configuration.accountUrl,
				requestType: 	"GET",
				dataType: 		"JSON",
				httpHeader: 	"Authorization",
				headerValue:    "Basic " + btoa(this.userName + ":" + this.password), 
				callBack:       this.fnLoginCallBack
			};
			FavesRUS.dataAccess.callService(loginOptions);
		},
		fnLoginCallBack: function (result) {
			if(result.success === true){
				viewModel.set("firstName", result.data.FirstName);
				viewModel.set("lastName", result.data.LastName);
				//viewModel.set("email")
			}
		}
	})
})