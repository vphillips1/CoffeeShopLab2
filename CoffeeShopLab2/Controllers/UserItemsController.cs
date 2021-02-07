using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShopLab2.DALModels;
using CoffeeShopLab2.Models.CoffeeShop;
using CoffeeShopLab2.Models.UserItems;
using CoffeeShopLab2.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopLab2.Controllers
{
    public class UserItemsController : Controller
    {
        private readonly ILoggedUser _loggedUser;
        private readonly ShopDBContext _shopDBContext;
        private readonly IUserItems _userItems;

        public UserItemsController(ILoggedUser loggedUser, ShopDBContext shopDBContext, IUserItems userItems)
        {
            _loggedUser = loggedUser;
            _shopDBContext = shopDBContext;
            _userItems = userItems;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ItemDeleted()
        {
            return View();
        }

        public IActionResult AddItem() 
        {

            return View("Shop");
        }

        public IActionResult ItemDetails(int id) 
        {

            var viewModel = new ItemInfoViewModel();

            viewModel.AvailableItem = GetItemWhereIDIsFirst(id);

            return View(viewModel);
        }

        public IActionResult DeleteConfirmation(int id, int userid) 
        {
            var viewModel = new DeleteViewModel();
            viewModel.ItemId = id;
            viewModel.UserId = userid;

            return View(viewModel);
        }

        public IActionResult DeleteItem(int id, int userid) //LAB 24
        {

            var usersItems = _shopDBContext.UserItems.Where(user => user.UserID == _loggedUser.theUser.UserID).ToList();

            foreach (var usersItem in usersItems)
            {
                if (usersItem.ItemID == id)
                {
                    _shopDBContext.UserItems.Remove(usersItem);
                    _shopDBContext.SaveChanges();
                    break;
                }
            }

            return View("ItemDeleted");
        }

        public IActionResult UserPurchases()
        {
            if (_loggedUser.loggedIn == false)
            {

                return View("MustBeLoggedIn");
            }

            var viewModel = new UserPurchasesViewModel();
            viewModel.CurrentUserID = _loggedUser.theUser.UserID;

            var itemIdsOfCurrentUser = _shopDBContext.UserItems.Where(user => user.UserID == viewModel.CurrentUserID).ToList();



            viewModel.userPurchases = itemIdsOfCurrentUser.Select(itemDAL => new Item
            {
                ItemID = itemDAL.ItemID,
                Description = itemDAL.Description,
                Name = itemDAL.Name,
                Price = itemDAL.Price,
                Quantity = itemDAL.Quantity

            }).ToList();


            return View(viewModel);
        }

        private Item GetItemWhereIDIsFirst(int id)
        {
            ItemsDAL itemDAL = _shopDBContext.Items
                .Where(item => item.ItemID == id).FirstOrDefault();

            var item = new Item();
            item.ItemID = itemDAL.ItemID;
            item.Name = itemDAL.Name;
            item.Price = itemDAL.Price;
            item.Description = itemDAL.Description;
            item.Quantity = itemDAL.Quantity;

            return item;
        }


    }
}
