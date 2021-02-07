using CoffeeShopLab2.Models.CoffeeShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopLab2.Services
{
    public interface IUserItems
    {

        public List<Item> userPurchases { get; }
    }
}
