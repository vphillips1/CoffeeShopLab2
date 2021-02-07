using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopLab2.Models.CoffeeShop
{
    public class ShopViewModel
    {
        public List<Item> Items { get; set; }

        public int CurrentUserID { get; set; }

        public double UserFunds { get; set; }

    }
}
