using CoffeeShopLab2.Models.CoffeeShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopLab2.Services
{
    public interface ILoggedUser
    {
        public User theUser { get; }
        public bool loggedIn { get; set; }
    }
}
