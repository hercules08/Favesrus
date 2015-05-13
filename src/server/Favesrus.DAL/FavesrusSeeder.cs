using Angela.Core;
using Favesrus.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
