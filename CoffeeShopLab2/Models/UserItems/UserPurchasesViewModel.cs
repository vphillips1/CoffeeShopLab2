using CoffeeShopLab2.Models.CoffeeShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopLab2.Models.UserItems
{
    public class UserPurchasesViewModel
    {
        public List<Item> userPurchases { get; set; }

        public int CurrentUserID { get; set; }

    }
}
