﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopLab2.Models.CoffeeShop
{
    public class LogInViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public int CurrentUserID { get; set; }

        public double Funds { get; set; }


    }
}
