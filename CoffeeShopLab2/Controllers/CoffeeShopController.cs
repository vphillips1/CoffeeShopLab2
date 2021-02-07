using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShopLab2.DALModels;
using CoffeeShopLab2.Models.CoffeeShop;
using CoffeeShopLab2.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShopLab2.Controllers
{

    [Authorize]
    public class CoffeeShopController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILoggedUser _loggedUser;
        private readonly ShopDBContext _shopDBContext;
        private readonly IUserItems _userItem;

        public CoffeeShopController(ILoggedUser loggedUser, ShopDBContext shopDBContext, IUserItems userItem, UserManager<IdentityUser> userManager)
        {

            _userManager = userManager;
            _loggedUser = loggedUser;

            _shopDBContext = shopDBContext;

            _userItem = userItem;



        }


        public IActionResult Index()
        {

            return View();
        }

        public IActionResult AddUser()
        {
            return View();
        }


        public IActionResult FormResult(AddUserFormViewModel model)
        {
            var viewModel = new FormResultViewModel();
            var isDouble = double.TryParse(model.UserFunds, out double actualFunds);
            var user = new UsersDAL();
            
            
            

            
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.Password = model.Password;
                user.PasswordConfirmation = model.PasswordConfirmation;
                user.UserName = model.Email;
                user.UserFunds = actualFunds;

                viewModel.theUser = new User();
                viewModel.theUser.FirstName = model.FirstName;
                viewModel.theUser.LastName = model.LastName;
                viewModel.theUser.Email = model.Email;
                viewModel.theUser.PhoneNumber = model.PhoneNumber;
                viewModel.theUser.Password = model.Password;
                viewModel.theUser.UserFunds = actualFunds;
                viewModel.theUser.UserName = model.Email;
                viewModel.theUser.PasswordConfirmation = model.PasswordConfirmation;





                _shopDBContext.Users.Add(user);
                _shopDBContext.SaveChanges();

                var users = _shopDBContext.Users.ToList();


                var usersViewModelList = users
                    .Select(usersDal => new User()
                    {
                        FirstName = usersDal.FirstName,
                        LastName = usersDal.LastName,
                        Email = usersDal.Email,
                        Password = usersDal.Password,
                        PasswordConfirmation = usersDal.PasswordConfirmation,
                        UserID = usersDal.UserID
                    }).ToList();

                viewModel.Users = usersViewModelList;

                return View("FormResult", viewModel);


            

           
        }


        public IActionResult LogIn()
        {

            return View();
        }


        public IActionResult CurrentUser(LogInViewModel model)
        {
            foreach (var user in _shopDBContext.Users.ToList())
            {
                if (user.UserName == model.UserName && user.Password == model.Password)
                {
                    _loggedUser.theUser.UserName = user.UserName;
                    _loggedUser.theUser.Password = user.Password;
                    _loggedUser.theUser.Email = user.Email;
                    _loggedUser.theUser.PhoneNumber = user.PhoneNumber;
                    _loggedUser.theUser.Password = user.Password;
                    _loggedUser.theUser.PasswordConfirmation = user.PasswordConfirmation;
                    _loggedUser.theUser.UserFunds = user.UserFunds;
                    _loggedUser.theUser.UserID = user.UserID;
                }
            }

            _loggedUser.loggedIn = true;


            return View("SuccessfulLogIn");
        }


        public IActionResult LogOut()
        {
            _loggedUser.theUser.UserID = 0;
            _loggedUser.theUser.UserFunds = 0;
            _loggedUser.theUser.UserName = null;
            _loggedUser.loggedIn = false;
            return View();
        }

        public IActionResult Shop()
        {

            var viewModel = new ShopViewModel();

            viewModel.CurrentUserID = _loggedUser.theUser.UserID;
            viewModel.UserFunds = _loggedUser.theUser.UserFunds;

            var storeItems = _shopDBContext.Items.ToList();

            var userItems = _shopDBContext.UserItems.Where(userItem => userItem.UserID == _loggedUser.theUser.UserID).ToList();

            viewModel.Items = storeItems
                .Select(itemsDal => new Item()
                {
                    ItemID = itemsDal.ItemID,
                    Name = itemsDal.Name,
                    Description = itemsDal.Description,
                    Quantity = itemsDal.Quantity,
                    Price = itemsDal.Price
                }).ToList();


            foreach (var item in viewModel.Items)
            {
                foreach (var useritem in userItems)
                {
                    if (item.ItemID == useritem.ItemID)
                    {
                        item.Purchased = true;
                    }
                }
            }

            if (_loggedUser.theUser.UserID != 0)
            {
                return View(viewModel);
            }

            else
            {

                return View("PleaseLogIn");
            }
        }

        public IActionResult BuyResult(int ID)
        {
            var viewModel = new PurchaseResultViewModel();
            viewModel.UserFunds = _loggedUser.theUser.UserFunds;
            viewModel.CurrentUserID = _loggedUser.theUser.UserID;

            var userItem = new UserItemsDAL();
            userItem.UserID = viewModel.CurrentUserID;

            double price = 0;

            foreach (var item in _shopDBContext.Items.ToList())
            {
                if (item.ItemID == ID)
                {
                    price = item.Price;
                    userItem.ItemID = item.ItemID;
                    userItem.Name = item.Name;
                    userItem.Description = item.Description;
                    userItem.Quantity = item.Quantity;
                    userItem.Price = item.Price;

                }
            }

            _shopDBContext.Add(userItem);
            _shopDBContext.SaveChanges();

            bool enoughCash = false;

            foreach (var user in _shopDBContext.Users.ToList())
            {
                if (user.UserID == viewModel.CurrentUserID)
                {
                    if (viewModel.UserFunds > price)
                    {
                        enoughCash = true;
                        user.UserFunds = user.UserFunds - price;
                        _shopDBContext.SaveChanges();
                        viewModel.UserFunds = user.UserFunds;
                        _loggedUser.theUser.UserFunds = user.UserFunds;
                    }
                }
            }

            var errorModel = new ErrorPageViewModel();
            errorModel.UserFunds = viewModel.UserFunds;

            var shopModel = new ShopViewModel();
            shopModel.CurrentUserID = _loggedUser.theUser.UserID;
            shopModel.UserFunds = _loggedUser.theUser.UserFunds;



            if (enoughCash)
            {
                return View(viewModel);
            }
            else
            {
                return View("ErrorPage", errorModel);
            }

        }


        public IActionResult ErrorPage(ShopViewModel model)
        {
            var viewModel = new ErrorPageViewModel();
            viewModel.UserFunds = model.UserFunds;

            return View();
        }






        private User GetUserWhereIDIsFirst(int id)
        {
            UsersDAL userDAL = _shopDBContext.Users
                .Where(user => user.UserID == id).FirstOrDefault();


            var user = new User();
            user.UserID = userDAL.UserID;
            user.UserFunds = userDAL.UserFunds;
            return user;
        }




    }
}

