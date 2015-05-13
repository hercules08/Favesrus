Favesrus Server
=========


Document Overview
---------------------

This document provides information about the Favesrus Application Programming Interface. This document will specify the necessary values for requests to the API and provides sample responses. 

The API is used to quickly search, "Fave" and purchase gifts. API accepts GET,POST,PUT, and DELETE verbs utilizing a RESTFUL API. The server returns replies in the form of JSON objects. The Favesrus mobile app will use javascript, preferably jquery to parse JSON responses and update the UI.

**The dev resources used to test are available here:** [http://dev.favesrus.com/api/](http://dev.favesrus.com/api)

**The live resources will are located here:**
[http://favesrus.com/api](http://favesrus.com/api/) or [api.favesrus.com](api.favesrus.com)

JSON Format
------------------

The server replies to API request in the form of JSON objects. A sample response for the query [http://dev.favesrus.com/api/retailer/1](http://favesrus.com/api/retailer/1) is below.

	GET: http://dev.favesrus.com/api/retailer/1
	
	HTTP/1.1 200 OK
	Content-Type: application/json; charset=utf-8

	{
		"id":"1",
		"name":"BestBuy",
		"website": "http://bestbuy.com"
	}
	
Favesrus Status Codes
---------------------


1. **200** ~ Ok. The request was received and processed in good order.
1. **204** ~ Ok. No content.
1. **304** ~ Not modified. This is useful to know that data you tried to update was not modified.
1. **400** ~ Bad request due to bad syntax/params.
1. **401** ~ (HTTP) Not authorized. This is to inform you that either the authentication attempt failed or a session has expired.
1. **403** ~ (HTTP) Forbidden, no matter if you try to authenticate. Do not attempt to use this resource again (pretty please)
1. **404** ~ (HTTP) Not found.
1. **410** ~ (HTTP) Gone. Resource has permanently been removed. Do not attempt to use this resource again (pretty please)
1. **429** ~ Too many requests in the allotted amount of time. 
1. **500** ~ (HTTP) Internal server error. Bad bad things have happened. Help us by reporting this to Modo.
1. **503** (HTTP) Service unavailable or one of our key partner's API is down temporarily and the request cannot be processed successfully.



Testing using Advanced Rest Client or Fiddler the Content-Type must be set to application/x-www-form-urlencoded.


GiftItem
=

(GET)/giftitem/getgiftitems
-

**Context:** You want all the gift items in the system 

**Request:** 

	GET: http://dev.favesrus.com/api/giftitem/getgiftitems
 
**Response:**

	HTTP/1.1 201 OK
	Content-Type: application/json; charset=utf-8

	{
		"status": "successful_wishlist_add",
		"model": {
			"items": null,
			"entity": "Successful add to wishlist"
		},
		"message": "Successful add to wishlist",
		"hasItems": false
	}


(POST)/giftitem/gettotlist
-
**Context:** You want a list of item pairs for "This or That"

**Request:**

	POST: http://dev.favesrus.com/api/giftitem/gettotlist
	Content-Type: application/x-www-form-urlencoded
	
	RequestData:
	{
		"userid":"12jsdfij42342",
		"recommendationIds": ["1", "2", "3"],
		"returnedSetNumber": 5
	}

**Response:**

	HTTP/1.1 201 OK
	Content-Type: application/json; charset=utf-8

	{
	  "status": "get_recommendations_success",
	  "model": {
	    "items": [
	      {
	        "id": 1,
	        "itemName": "add",
	        "itemImage": "http://www.gamasutra.com/db_area/images/blog/231001/contrastmario.jpg",
	        "description": "Mario",
	        "itemPrice": null
	      },
	      {
	        "id": 2,
	        "itemName": "subtract",
	        "itemImage": "http://assets2.ignimgs.com/2015/01/29/luigi-1jpg-c41e5d.jpg",
	        "description": "Luigi",
	        "itemPrice": null
	      },
	      {
	        "id": 3,
	        "itemName": "multiply",
	        "itemImage": "https://assets.pokemon.com/static2/_ui/img/chrome/external_link_bumper.png",
	        "description": "Pikchachu",
	        "itemPrice": null
	      },
	      {
	        "id": 4,
	        "itemName": "divide",
	        "itemImage": "http://assets22.pokemon.com/assets/cms2/img/pokedex/full/007.png",
	        "description": "Squirtle",
	        "itemPrice": null
	      }
	    ],
	    "entity": null
	  },
	  "message": "Successfully retireved recommendations",
	  "hasItems": true
	}


Wishlist
=

(POST)/wishlist/addgiftitemtowishlist
-

**Context:** Add a giftitem to a user wishlist


**Request:**

	POST: http://dev.favesrus.com/api/wishlist/addgiftitemtowishlist
	Content-Type: application/x-www-form-urlencoded
	
	Request Data:
	{
		"userId": "12345678",
		"giftItemId": "10",
		"wishListId": "1"
	}

**Response:**

	HTTP/1.1 201 OK
	Content-Type: application/json; charset=utf-8

	{
		"status": "successful_wishlist_add",
		"model": {
			"items": null,
			"entity": "Successful add to wishlist"
		},
		"message": "Successful add to wishlist",
		"hasItems": false
	}

**Error Response :**
	
	HTTP/1.1 400 Error
	Content-Type: application/json; charset=utf-8

	{
		"status": "invalid_modelstate",
		"model": {
			"items": null,
			"entity": {
				"errorItem": "Email",
				"reason": "The Email field is required."
			}
		},
		"message": "Bad model state",
		"hasItems": false
	}

(POST)/wishlist/removegiftitemfromwishlist
-

**Context:** Remove a giftitem to a user wishlist


**Request:**

	POST: http://dev.favesrus.com/api/wishlist/addgiftitemtowishlist
	Content-Type: application/x-www-form-urlencoded
	
	Request Data:
	{
		"userId": "12345678",
		"giftItemId": "10",
		"wishListId": "1"
	}

**Response:**

	HTTP/1.1 201 OK
	Content-Type: application/json; charset=utf-8

	{
		"status": "successful_wishlist_delete",
		"model": {
			"items": null,
			"entity": "Successful delete from wishlist"
		},
		"message": "Successful delete from wishlist",
		"hasItems": false
	}



**Error Response :**
	
	HTTP/1.1 400 Error
	Content-Type: application/json; charset=utf-8

	{
		"status": "invalid_modelstate",
		"model": {
			"items": null,
			"entity": {
				"errorItem": "Email",
				"reason": "The Email field is required."
			}
		},
		"message": "Bad model state",
		"hasItems": false
	}

Category & Recommendations
=

(GET)/api/category
------------------
**Context:**
You need all the current category's in the system.
	
**Request:**

	GET: http://dev.favesrus.com/api/category/

**Response:**

	HTTP/1.1 204 No Content
	
**Fiddler query string:**

email=elroy@gmail.com

(GET)/recommendation/getrecommendations
-

**Context:** Get the list of recommendations and their id's


**Request:**

	GET: http://dev.favesrus.com/api/recommendation/getrecommendations

**Response:**

	HTTP/1.1 201 OK
	Content-Type: application/json; charset=utf-8

	{
	  "status": "get_recommendations_success",
	  "model": {
      "items": [
      		{"id": 1, "categoryName": "Amibo", "giftItems": []},
      		{"id": 2, "categoryName": "Shoes", "giftItems": []},
      		{"id": 3, "categoryName": "Cars", "giftItems": []}
     	],
      "entity": null
	  },
	  "message": "Successfully retrieved recommendations list",
	  "hasItems": true
	}


**Error Response :**
	
	HTTP/1.1 400 Error
	Content-Type: application/json; charset=utf-8

	{
		"status": "invalid_modelstate",
		"model": {
			"items": null,
			"entity": {
				"errorItem": "Email",
				"reason": "The Email field is required."
			}
		},
		"message": "Bad model state",
		"hasItems": false
	}


Account
=
(POST)/account/forgotpassword
-
**Context:** You forgot your password & need to temporarily reset it.

**Request:** 

	POST: http://dev.favesrus.com/account/forgotpassword
	Content-Type: application/x-www-form-urlencoded
	
	Request Data:
	{
		"email":"damola.omotosho@gmail.com"
	}
**Response:**


(POST)/account/register
----------------------

**Context:**
You would like to register for Favesrus with a new email address and password
	
**Request:**

	POST: http://dev.favesrus.com/api/account/register
	Content-Type: application/x-www-form-urlencoded
	
	Request Data:
	{
		"email":"damola.omotosho@gmail.com",
		"password":"12345678"
	}
	
	Optional Params: firstname, lastname, email, password, gender, birthday, profilepic


**Response:**

	HTTP/1.1 201 OK
	Content-Type: application/json; charset=utf-8

	{
		"id":"asdfj-12312m-12312mkmf-2321",
		"modoAccountId":"",
		"firstName:"",
		"lastName:"",
		"birthday": "",
		"profilePic": null
	}

**Fiddler query string:**

email=damola.omotosho%40gmail.com&password=12345678

(POST)/account/registerfacebook
----------------------

**Context:**
You would like to register for Favesrus using facebook
	
**Request:**

	POST: http://dev.favesrus.com/api/account/registerfacebook
	Content-Type: application/x-www-form-urlencoded
	
	Request Data:
	{
		"email":"elroy@gmail.com",
		"providerkey":"12345678"
	}
	
	Optional Params: firstname, lastname, email, password, gender, birthday, profilepic


**Response:**

	HTTP/1.1 201 OK
	Content-Type: application/json; charset=utf-8

	{
		"id":"asdfj-12312m-12312mkmf-2321",
		"modoAccountId":"asdfj-12312m-12312mkmf-2321",
		"firstName:"Elroy",
		"lastName:"Ashtian",
		"birthday": "1/8/1988",
		"profilePic": null
	}

**Fiddler query string:**

email=elroy@gmail.com&providerkey=12345678

(POST)/account/login
----------------------
**Context:**
You already registered for Favesrus and would like to login with your email and password.
	
**Request:**

	POST: http://dev.favesrus.com/api/account/login
	Content-Type: application/x-www-form-urlencoded
	
	Request Data:
	{
		"email":"damola.omotosho@gmail.com",
		"password":"12345678"
	}


**Response:**

	HTTP/1.1 200 OK
	Content-Type: application/json; charset=utf-8

	{
		"id":"asdfj-12312m-12312mkmf-2321",
		"modoAccountId":"1231-1234ksf-123123nmm",
		"firstName:"Damola",
		"lastName:"Omotosho",
		"birthday": "2/8/1987",
		"profilePic": "http://damolaomotosho.com/content/images/damola.jpg"
	}

**Fiddler query string:**

username=damola.omotosho@gmail.com&password=12345678

(POST)/account/loginfacebook
----------------------

**Context:**
You would like to login/register for Favesrus using facebook
	
**Request:**

	POST: http://dev.favesrus.com/api/account/loginfacebook
	Content-Type: application/x-www-form-urlencoded
	
	Request Data:
	{
		"email":"elroy@gmail.com",
		"providerKey":"12345678"
	}


**Response:**

	HTTP/1.1 200 OK
	Content-Type: application/json; charset=utf-8

	{
		"id":"asdfj-12312m-12312mkmf-2321",
		"modoAccountId":"1231-1234ksf-123123nmm",
		"firstName:"Elroy",
		"lastName:"Ashtian",
		"birthday": "1/8/1988",
		"profilePic": "http://elroy.com/content/images/damola.jpg"
	}

**Fiddler query string:**

email=elroy@gmail.com&providerkey=12345678


(POST)/account/forgotpassword
------------------
**Context:**
You need to reset your password
	
**Request:**

	POST: http://dev.favesrus.com/api/account/forgotpassword
	Content-Type: application/x-www-form-urlencoded
	
	Request Data:
	{
		"email":"elroy@gmail.com",
	}


**Response:**

	HTTP/1.1 204 No Content
	
**Fiddler query string:**

email=elroy@gmail.com

(POST)http://dev.favesrus.com/authenticate
-
**Context:** Get authorization token when using basic authentication

**Request:**

	POST: http://dev.favesrus.com/autenticate
	Content-Type: application/x-www-form-urlencoded
	
	Request Data:
	{
		"grant_type": "password",
		"username": "damola.omotosho@gmail.com",
		"password": "12345678"
	}

**Response:**

	HTTP/1.1 201 OK
	Content-Type: application/json; charset=utf-8

	{
		"access_token":"eytWGFHJEWJFOI23IRYH23J14N2J4N123MR4N132L432K4YOI3241234J2314I2341234456781324KJ2LJHUFOYHAKJFNEWKLFEWFKEWFWEF",
		"token_type":"bearer",
		"expires_in":1199
	}

**Error Response :**
	
	HTTP/1.1 400 Error
	Content-Type: application/json; charset=utf-8

	{ 
		"error":"invalid_grant", 
		"error_description":"The username or password is incorrect" 
	}

**Fiddler query string:**

grant_type=password&username=damola.omotosho%40gmail.com&password=12345678

**Notes:**

- You must attach the bearer token for all request that require authentication
 
- Auth token expires after 20 minutes.
