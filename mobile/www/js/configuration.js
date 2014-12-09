FavesRUS.configuration = (function () {
	var serviceUrl = "http://favesrus.com/api/";
	return {
		serviceUrl: 	serviceUrl,
		accountUrl: 	serviceUrl + "Account/",
		giftItemUrl: 	serviceUrl + "Item/",
		paymentUrl: 	serviceUrl + "Pay/",
		retailerUrl: 	serviceUrl + "Retailer/"
	}
})();

/*Now Service, Account, GiftItem, Payment and Retailer controller URLS are available*/