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


Account
===



(POST)http://dev.favesrus.com/authenticate
---

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