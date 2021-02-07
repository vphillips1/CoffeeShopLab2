using CoffeeShopLab2.DALModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopLab2.Services
{
    public class ShopDBContext : DbContext
    {
        public ShopDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UsersDAL> Users { get; set; }
        public DbSet<ItemsDAL> Items { get; set; }

        public DbSet<UserItemsDAL> UserItems { get; set; }

    }
}
