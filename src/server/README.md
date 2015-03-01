Favesrus Server
=========


Document Overview
---------------------

This implementer's guide provides information about Favesrus’ Application Programming Interface (API). This document will specify the necessary values for requests and responses to and from the API and provides sample request and responses when accessing the API. 

The Favesrus API is used to quickly search, "Fave" and purchase gifts. The Favesrus API performs GET/POST request to the Favesrus server using a RESTFUL API. The Favesrus API returns replies in the form of JSON responses. The Favesrus mobile app will use javascript, preferably jquery to parse JSON responses and update the UI.

**The dev resources used to test are available here:** [http://dev.favesrus.com/api/](http://dev.favesrus.com/api)

**The live resources will are located here:**
[http://favesrus.com/api](http://favesrus.com/api/) or [api.favesrus.com](api.favesrus.com)

JSON Format
------------------

The server replies to API request in the form of JSON responses. A sample response for the query [http://dev.favesrus.com/api/retailer/1](http://favesrus.com/api/retailer/1) is below.

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


Get Authorization Token

http://favesrus.com/authenticate

Request

{
"grant_type": "password"
"username": "<username>"
"password": "<password>"
}

Response Success
{
"access_token":"eytWGFHJEWJFOI23IRYH23J14N2J4N123MR4N132L432K4YOI3241234J2314I2341234456781324KJ2LJHUFOYHAKJFNEWKLFEWFKEWFWEF"
- 
"token_type":"bearer",
"expires_in":1199
}

Response Error
{ "error":"invalid_grant", "error_description":"The username or password is incorrect" }

Notes:

You must attach the bearer token for all request that require authentication


Auth token expires after 20 minutes.



Testing using Advanced Rest Client(note Content-Type must be set to application/x-www-form-urlencoded)


Account
===

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

(POST)/account/login
----------------------

Context:

http://favesrus.com/api/account/login -
username=damola.omotosho@gmail.com&password=12345678

(POST)/account/loginfacebook
----------------------

Context:

http://favesrus.com/api/account/
loginfacebook - 
email=elroy@gmail.com&providerkey=12345678

**Links**

- Home -	[favit.io](http://favit.io)
- Dev - 	[dev.favit.io](dev.favit.io)
- Api - 	[api.favit.io](api.favit.io)
- Seller - 	[sell.favit.io](sell.favit.io)

**Front End Architecture**

- Developed In - Intel XDK
- Design - KendoUI
- Structure - Backbone.js
- Testing - QUnit

**Back End Architecture**

- Developed In - Visual Studio 2013
- Design - RESTFUL, HATEOAS
- Structure - WebApi 2, MVC5
- Testing - MSTest

Authors
-----------
- Elroy Ashtian, Jr.

- [Damola Omotosho](http://damolaomotosho.com)

- Wale Ogundipe

- Jeff Lofvers