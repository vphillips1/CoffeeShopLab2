using CoffeeShopLab2.Models.CoffeeShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopLab2.Services
{
    public class CoffeeShop : ILoggedUser
    {
        public User theUser { get; } = new User();
        public bool loggedIn { get; set; }

    }
}
