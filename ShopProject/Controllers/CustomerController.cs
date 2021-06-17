using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopProject.Data;
using Microsoft.AspNetCore.Identity;
using ShopProject.Models.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ShopProject.Controllers
{
    
    public class CustomerController : Controller
    {
        private readonly ShopDbContext _context;
        public CustomerController(ShopDbContext context)
        {
            _context = context;
        }


        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Login(Customer model)
        {
           
            if (ModelState.IsValid)
            {
                var ListUser = _context.Customers.Where(s => s.Email == model.Email && s.Password == model.Password).ToList();
                Customer result = ListUser.FirstOrDefault();
                if (result == null)

                {
                    TempData["Message"] = "Account or Password incorrect";

                    return RedirectToAction("Login", "Customer");
                }
                //else if (result.Admin == 1)
                //{
                //    TempData["Name"] = "Hello" + "   " + result.Name;
                //    TempData["Product"] = "Products";
                //    TempData["Order"] = "Orders";
                //   TempData["Customer"] = "Customer";
                //   return RedirectToAction("Index", "Admin");


                //}
                //var identity = new ClaimsIdentity(new[] {
                //    new Claim(ClaimTypes.Name, result.Name)
                //}, CookieAuthenticationDefaults.AuthenticationScheme);
                //var principal = new ClaimsPrincipal(identity);

                //var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                //TempData["Name"] = "Hello" + "   " + result.Name;

                //var claimsIdentity = new GenericIdentity(ClaimTypes.Name, model.Email);
                //var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                //HttpContext.SignInAsync(claimsPrincipal);
                else
                {

                    string UserId = result.Id.ToString();
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Sid, UserId),
                        new Claim(ClaimTypes.Name, result.Name),
                        new Claim(ClaimTypes.Role, "User"),
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    HttpContext.SignInAsync(claimsPrincipal);

                    //var userId = User.FindFirst(ClaimTypes.Sid).Value;
                    string itemcounts = _context.Carts.Where(x => x.UserId == UserId).Select(x => x.Amount).Sum().ToString();

                    HttpContext.Session.SetString("AmountItem", itemcounts);
                    HttpContext.Session.SetString("UserName", result.Name);






                    if (result.Admin == true)
                    {
                        HttpContext.Session.SetString("Admin", "Admin");
                        return RedirectToAction("Index", "Admin");
                    }

                    
                }
                
            }
            
            return RedirectToAction("Index", "Home");
            
        }

        public  IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Clear(); //確保Session被清除，以免顯示錯誤的Navbar，如以Admin登入後再以使用者身分登入，卻顯示Admin的Navbar
            return RedirectToAction("Index","Home");
        }






        public IActionResult SignUp()
        {
            return View();
        }

        
        public IActionResult Register(Customer model)
        {
            if (ModelState.IsValid)
            {

                var ListUser = _context.Customers.Where(s => s.Email == model.Email ).ToList();
                Customer result = ListUser.FirstOrDefault();
                if (result != null)
                {
                    TempData["Message"] = "This Email is already registered";
                    return RedirectToAction("SignUp", "Customer");
                }

                else
                {
                    _context.Customers.Add(model);
                    _context.SaveChanges();
                    ChangePassWord changePassWord = new ChangePassWord()
                    {
                        UserId = model.Id,
                        Email = model.Email,
                        OriginalPassword = model.Password
                    };
                    _context.ChangePassWords.Add(changePassWord);

                    _context.SaveChanges();
                    TempData["Name"] ="Hello"+"    "+ model.Name;
                    return RedirectToAction("Login", "Customer");
                }
            }
            return View();
        }

        public IActionResult MemberCenter()
        {

            var userId = User.FindFirst(ClaimTypes.Sid).Value;
            var itemlist = _context.Customers.Where(x => x.Id.ToString() == userId).ToList();
            var item = itemlist.FirstOrDefault();
            var changepassword = _context.ChangePassWords.Where(x => x.UserId.ToString() == userId).ToList();
            var findpwd = changepassword.FirstOrDefault();
            ViewModel viewModel = new ViewModel()
            {
                customers = item,
                changePassWords = findpwd
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult MemberCenter( ViewModel viewModel)
        {
            //if (ModelState.IsValid)
            //{

                var userId = User.FindFirst(ClaimTypes.Sid).Value;
                var itemlist = _context.Customers.Where(x => x.Id.ToString() == userId).ToList();
                var item = itemlist.FirstOrDefault();
                //if(item.Password != customer.Password)
                //{
                //    ViewBag.error = "密碼錯誤";
                //    return View();
                //}
                item.Name = viewModel.customers.Name;
                //item.Password = customer.Password;
                item.Phone = viewModel.customers.Phone;
                item.Address = viewModel.customers.Address;
                item.Birthday = viewModel.customers.Birthday;
                item.Email = viewModel.customers.Email;
                item.Gender = viewModel.customers.Gender;
                _context.Customers.Update(item);
                _context.SaveChanges();
                HttpContext.Session.SetString("Name",viewModel.customers.Name);
                TempData["status"] = "Member profile has been modified";
            return View();
        }

        public IActionResult MyOrder()
        {

            var userId = User.FindFirst(ClaimTypes.Sid).Value;
            var myorder = _context.Purchases.Where(x => x.UserId == userId).ToList();


            return View(myorder);
        }

        public IActionResult ChangePassword(ViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.Sid).Value;
                var oldpassword = _context.Customers.Where(x => x.Id.ToString() == userId).FirstOrDefault();
                if (viewModel.changePassWords.CheckOriginalPassword != oldpassword.Password)
                {
                    TempData["status"] = "Does not match the old password";
                   
                    return Redirect(Request.Headers["Referer"].ToString());
                }
                else if (viewModel.changePassWords.CheckOriginalPassword == oldpassword.Password)
                {
                    if (viewModel.changePassWords.NewPassword != viewModel.changePassWords.ConfirmedPassword)
                    {
                        TempData["status"] = "Password confirmation error";
                        
                        return Redirect(Request.Headers["Referer"].ToString());
                    }

                    
                    var newPwd = _context.ChangePassWords.Where(x => x.UserId.ToString() == userId).ToList().FirstOrDefault();
                    var UserPwd = _context.Customers.Where(x => x.Id.ToString() == userId).ToList().FirstOrDefault();


                    UserPwd.Password = viewModel.changePassWords.NewPassword;
                    _context.Customers.Update(UserPwd);
                    _context.SaveChanges();


                    
                    newPwd.OriginalPassword = viewModel.changePassWords.NewPassword;
                    

                    _context.ChangePassWords.Update(newPwd);
                    _context.SaveChanges();



                }
                TempData["status"] = "Password has been changed";
                
            }
            

            return Redirect(Request.Headers["Referer"].ToString());
        }

    }



}
