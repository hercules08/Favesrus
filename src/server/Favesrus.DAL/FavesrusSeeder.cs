using Favesrus.Domain.Base;
using Favesrus.Domain.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Favesrus.DAL
{
    public static class FavesrusSeeder
    {
        private static Stack<string> _categoryStack;
        private static Stack<string> _retailerStack;
        private static Stack<string> _wishlistStack;

        public static List<GiftItem> GetGiftItems()
        {
            List<GiftItem> list = new List<GiftItem>();

            //list = Angie
            //    .Configure<GiftItem>()
            //    .Fill(p => p.ItemImage)

            return list;
        } 

        static UserManager<FavesrusUser> userMgr;
        static RoleManager<FavesrusRole> roleMgr;

        public static void Seed(FavesrusDbContext context)
        {
            userMgr = new UserManager<FavesrusUser>(new UserStore<FavesrusUser>(context));
            roleMgr = new RoleManager<FavesrusRole>(new RoleStore<FavesrusRole>(context));

            Retailer retailerBestBuy = new Retailer()
            {
                RetailerName = "BestBuy",
                RetailerLogo = "http://upload.wikimedia.org/wikipedia/commons/thumb/f/f5/Best_Buy_Logo.svg/300px-Best_Buy_Logo.svg.png",
            };

            Retailer retailerWalmart = new Retailer()
            {
                RetailerName = "Walmart",
                RetailerLogo = "http://upload.wikimedia.org/wikipedia/commons/thumb/7/76/New_Walmart_Logo.svg/1000px-New_Walmart_Logo.svg.png",
            };

            Retailer retailerSears = new Retailer()
            {
                RetailerName = "Sears",
                RetailerLogo = "http://www.buyvia.com/i/2013/10/Sears-logo.png"
            };

            Retailer retailerMacys = new Retailer()
            {
                RetailerName = "Macys",
                RetailerLogo = "http://www.sportsradio1450.com/wp-content/uploads/sites/2/2015/04/macys-logo.png"
            };

            Retailer retailerApple = new Retailer()
            {
                RetailerName = "Apple",
                RetailerLogo = "http://www.microb3.eu/sites/default/files/osd/apple.jpg"
            };

            Retailer retailerSamsung = new Retailer()
            {
                RetailerName = "Samsung",
                RetailerLogo = "http://www.samsung.com/us/images/common/samsung-logo.jpg"
            };

            Recommendation recommendationGamingFigures = new Recommendation()
            {
                CategoryName = "Interactive Gaming Figures",
                BackgroundColor = "red"
            };

            Recommendation recommendationSandalsWomen = new Recommendation()
            {
                CategoryName = "Sandals (Women)",
                BackgroundColor = "river-blue"
            };

            Recommendation recommendationSmartWatch = new Recommendation()
            {
                CategoryName = "Smart Watches",
                BackgroundColor = "black"
            };

            Category categoryElectronics = new Category()
            {
                CategoryName = "Electronics",
                BackgroundColor = "gray"
            };

            Category categoryKitchen = new Category()
            {
                CategoryName = "Kitchen",
                BackgroundColor = "yellow"
            };

            Category categoryGaming = new Category()
            {
                CategoryName = "Gaming",
                BackgroundColor = "green"
            };

            GiftItem giftItemMario = new GiftItem()
            {
                ItemName = "Mario Amiibo",
                ItemImage = "http://www.gamestop.com/common/images/lbox/104546b.jpg",
                Description = "Mario never hesitates to leap into action when there's trouble in the Mushroom Kingdom.",
                Retailer = new List<Retailer>
                {
                    retailerBestBuy,
                    retailerWalmart
                },
                Category = new List<BaseCategory>
                {
                    recommendationGamingFigures
                }

            };

            GiftItem giftItemMickey = new GiftItem()
            {
                ItemName = "Mickey Amiibo",
                ItemImage = "http://www.gamestop.com/common/images/lbox/102788b.jpg",
                Description = "Feeling mischievous? Join Sorcerer's Apprentice Mickey's spellbinding high jinks. With his magic sweep and bursts, he's got more moves under his Sorcerer's hat than a wizard in a wand shop. Abracadabra!",
                Retailer = new List<Retailer>
                {
                    retailerBestBuy,
                    retailerWalmart,
                    retailerSears
                },
                Category = new List<BaseCategory>
                {
                    recommendationGamingFigures
                }
            };

            GiftItem giftItemMaterialGirl = new GiftItem()
            {
                ItemName = "Material Girl Selena Rhinestone Flat Thong Sandals",
                ItemImage = "http://slimages.macys.com/is/image/MCY/products/3/optimized/2158793_fpx.tif?wid=262&hei=320&fit=fit,1&$filtersm$",
                Description = "Add some shimmer to your summer with the Selena rhinestone flat thong sandals by Material Girl.",
                Retailer = new List<Retailer>
                {
                    retailerMacys
                },
                Category = new List<BaseCategory>
                {
                    recommendationSandalsWomen
                }

            };

            GiftItem giftItemKalindiSandals = new GiftItem()
            {
                ItemName = "Easy Spirit Kalindi Flat Sandals",
                ItemImage = "http://slimages.macys.com/is/image/MCY/products/1/optimized/2707711_fpx.tif?wid=262&hei=320&fit=fit,1&$filtersm$",
                Description = "A shoe with innovative comfort technology has never looked so chic! Easy Spirit's Kalindi sandals are simple, sophisticated and perfect for everyday adventures.",
                Retailer = new List<Retailer>
                {
                    retailerMacys
                },
                Category = new List<BaseCategory>
                {
                    recommendationSandalsWomen
                }
            };

            GiftItem giftItemAppleWatch = new GiftItem()
            {
                ItemName = "Apple Smartwatch Sport 42mm Silver Aluminium Case White Sport Band",
                ItemImage = "http://ecx.images-amazon.com/images/I/31czk%2BukKPL.jpg",
                Description = "Silver or space gray anodized aluminum case<br>Retina display with Force Touch<br>Heart rate sensor, accelerometer, and gyroscope.",
                Retailer = new List<Retailer>
                {
                    retailerApple
                },
                Category = new List<BaseCategory>
                {
                    recommendationSmartWatch
                }
            };

            GiftItem giftItemSamsungWatch = new GiftItem()
            {
                ItemName = "Samsung Gear 2 Neo",
                ItemImage = "http://ecx.images-amazon.com/images/I/91V3t5zHNSL._SL1500_.jpg",
                Description = "The Samsung Gear 2 Neo is the smart companion watch tailored to your look and lifestyle. With real-time notifications, calls and fitness tracking right at our wrist, you can stay focused in the moment. No matter where your day takes you, your Gear 2 Neo matches your style to keep you connected without feeling distracted.",
                Retailer = new List<Retailer>
                {
                    retailerSamsung
                },
                Category = new List<BaseCategory>
                {
                    recommendationSmartWatch
                }
            };

            GiftItem giftItemPlaceHolder1 = new GiftItem()
            {
                ItemName = "Place Holder 1",
                ItemImage = "http://placehold.it/270x270",
                Description = "Place Holder 1"
            };

            GiftItem giftItemPlaceHolder2 = new GiftItem()
            {
                ItemName = "Place Holder 2",
                ItemImage = "http://placehold.it/270x270",
                Description = "Place Holder 2"
            };

            GiftItem giftItemPlaceHolder3 = new GiftItem()
            {
                ItemName = "Place Holder 3",
                ItemImage = "http://placehold.it/270x270",
                Description = "Place Holder 3"
            };

            GiftItem giftItemPlaceHolder4 = new GiftItem()
            {
                ItemName = "Place Holder 4",
                ItemImage = "http://placehold.it/270x270",
                Description = "Place Holder 4"
            };

            WishList wishlistDamola = new WishList()
            {
                WishListName = "Default",
                GiftItems = new List<GiftItem>
                {
                    giftItemMario,
                    giftItemPlaceHolder1
                }
            };

            WishList wishlistElroy = new WishList()
            {
                WishListName = "Default",
                GiftItems = new List<GiftItem>
                {
                    giftItemMickey,
                    giftItemPlaceHolder2
                }
            };


            FaveEvent faveEventChristmas = new FaveEvent()
            {
                EventDate = DateTime.Parse("12/25/2015"),
                EventImage = "http://christmas.jpg",
                EventName = "Christmas",
                SuggestedGiftItems = 
                new List<GiftItem> {
                    giftItemAppleWatch,
                    giftItemSamsungWatch
                }

            };

            FaveEvent faveEventElroyBirthday = new FaveEvent()
            {
                EventDate = DateTime.Parse("02/08/1988"),
                EventImage = "http://christmas.jpg",
                EventName = "Elroy's Birthday",
                SuggestedGiftItems = wishlistElroy.GiftItems
            };

            FavesrusUser userDamola = new FavesrusUser()
            {
                FirstName = "Damola",
                LastName = "Omotosho",
                Birthday = DateTime.Parse("2/8/1987"),
                Gender = Favesrus.Domain.Entity.Enum.Gender.MALE,
                ProfilePic = "http://damolaomotosho.com/images/my-photo.jpg",
                WishLists = new List<WishList>{ wishlistDamola },
                UserName = "damola.omotosho@gmail.com",
                FaveEvents = new List<FaveEvent>
                {
                    faveEventElroyBirthday
                }
            };

            FavesrusUser userElroy = new FavesrusUser()
            {
                FirstName = "Elroy",
                LastName = "Ashtian",
                Birthday = DateTime.Parse("4/10/1988"),
                Gender = Favesrus.Domain.Entity.Enum.Gender.FEMALE,
                ProfilePic = "",
                WishLists = new List<WishList> { wishlistElroy },
                UserName = "elroy@faves.com",
                FaveEvents = new List<FaveEvent>
                {
                    faveEventChristmas
                }
            };

            List<EntityBase> dbItems = new List<EntityBase>()
            {
                retailerBestBuy,
                retailerWalmart,
                retailerSears,
                retailerMacys,
                retailerApple,
                retailerSamsung,
                
                recommendationGamingFigures,
                recommendationSandalsWomen,
                recommendationSmartWatch,

                categoryKitchen,
                categoryElectronics,
                categoryGaming,
                
                giftItemMario,
                giftItemMickey, 
                giftItemMaterialGirl, 
                giftItemKalindiSandals, 
                giftItemAppleWatch,
                giftItemSamsungWatch, 
                giftItemPlaceHolder1, 
                giftItemPlaceHolder2, 
                giftItemPlaceHolder3, 
                giftItemPlaceHolder4,

                wishlistDamola,
                wishlistElroy
            };

            foreach (var item in dbItems)
            {
                Type entityType = item.GetType();
                Add(context, item, entityType);
            }

            userMgr.Create(userDamola,"12345678");
            userMgr.Create(userElroy,"12345678");
        }

        static void Add<T>(DbContext context, T entity, Type entityType) where T : class
        {
            try
            {
                ((DbContext)context).Set(entityType).Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("An error occurred during the Add Entity.\r\n{0}", ex.Message));
            }
        }
    }
}
