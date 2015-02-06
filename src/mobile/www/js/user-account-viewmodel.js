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
		phoneNumber: "",
		gender: "",
		modoId: "",
		modoMembershipDate: "",
		cardOnFile: false,
		favFriends: [],
		favItems: [],
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
				viewModel.set("email", result.data.Email);
				viewModel.set("birthday", result.data.Birthday);
				viewModel.set("phoneNumber", result.data.PhoneNumber);
				viewModel.set("gender", result.data.Gender);
				viewModel.set("modoId", result.data.ModoId);
				viewModel.set("modoMembershipDate", result.data.ModoMembershipDate);
				viewModel.set("cardOnFile", result.data.CardOnFile);
				viewModel.set("favFriends", result.data.FavFriends);
				viewModel.set("favItems", result.data.FavItems);
			} else {
				//Handle error
			}
		}
	});

	return {
		viewModel: viewModel
	}
})();