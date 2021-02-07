using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopLab2.DALModels
{
    public class UserItemsDAL
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserItemId { get; set; }


        [ForeignKey("Users")]
        public int UserID { get; set; }

        [ForeignKey("Items")]
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }


    }
}
