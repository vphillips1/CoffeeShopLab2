using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopLab2.Models.CoffeeShop
{
    public class AddUserFormViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }


        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please Enter Valid Email")]
        public string Email { get; set; }


        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid. please try again.")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Please Enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [Required(ErrorMessage = "Confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password")]

        public string PasswordConfirmation { get; set; }

        public string UserFunds { get; set; }

    }
}
