using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopLab2.Models.CoffeeShop
{
    public class User
    {

        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string PasswordConfirmation { get; set; }

        public double UserFunds { get; set; }

        public string UserName { get; set; }

        public List<Item> UserPurchases { get; set; }
    }
}
