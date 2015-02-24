Favesrus Server
=========

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

"token_type":"bearer",
"expires_in":1199
}

Response Error
{ "error":"invalid_grant", "error_description":"The username or password is incorrect" }

Notes:

You must attach the bearer token for all request that require authentication


Auth token expires after 20 minutes.


Testing using Advanced Rest Client

http://favesrus.com/api/account/register - firstName=Damola&email=damola.omotosho%40gmail.com&password=12345678

http://favesrus.com/api/account/registerfacebook

firstName=Elroy&email=elroy%40gmail.com&providerkey=12345678

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