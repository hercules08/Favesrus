﻿using Favit.Model.Entities;
using Favit.Model.Factories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Favit.DAL
{
    public class FavitInitializer : 
        DropCreateDatabaseIfModelChanges<FavitDBContext>
    {

        protected override void Seed(FavitDBContext context)
        {
            base.Seed(context);
            Category shoes = CategoryFactory.Create("Shoes");
            Category shirts = CategoryFactory.Create("Shirts");
            context.Categories.Add(shoes);
            context.Categories.Add(shirts);

            Retailer nikeRetailer = new Retailer() { RetailerName="Nike", RetailerLogo="http://i.forbesimg.com/media/lists/companies/nike_416x416.jpg"};
            Retailer hmRetailer = new Retailer() {RetailerName="HM", RetailerLogo="http://www.polyvore.com/cgi/img-thing?.out=jpg&size=l&tid=31791934"};
            context.Retailers.Add(nikeRetailer);
            context.Retailers.Add(hmRetailer);

            Item shoeItem = new Item() { ItemName="Shoe", ItemPrice=50, Category = shoes, Retailer = nikeRetailer };
            Item shirtItem = new Item() { ItemName = "Blue Shirt", ItemPrice = 50, Category = shirts, Retailer = hmRetailer };
            context.Items.Add(shoeItem);
            context.Items.Add(shirtItem);

            Item[] walesFavoriteItems = {
                                            shoeItem,
                                            shirtItem
                                        };


            User[] favUsers = {
                                  new User() {
                                      Id=1,
                                      FirstName="Wale",
                                      LastName="Ogundipe",
                                      Email="hi@wale.me",
                                      Pic="https://pbs.twimg.com/profile_images/472108976327254016/Q6yDJl6S.jpeg",
                                      Birthday= new DateTime(1983, 10, 8),
                                      FavItems = walesFavoriteItems
                                  },
                                  new User() {
                                      Id=2,
                                      FirstName="Jeff",
                                      LastName="Lofvers",
                                      Email="jeff@lofvers.com",
                                      Pic="http://cdn.marketplaceimages.windowsphone.com/v8/images/42449ebb-a52d-4371-a44c-3d9561b5fff9?imageType=ws_icon_large",
                                      Birthday= new DateTime(1978, 10, 8),
                                  }
                              };

            User[] users = 
            {
                new User() {
                    Id=3,
                    FirstName = "Damola",
                    LastName ="Omotosho",
                    Pic = "http://www.damolaomotosho.com/Content/Images/damola3.jpg",
                    Email = "damola.omotosho@gmail.com",
                    Birthday = new DateTime(1987, 2, 8),
                    FavFriends = favUsers
                }
            };

            foreach(var user in users)
            {
                context.Set<User>().Add(user);
            }

        }
    }
}