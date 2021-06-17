using Microsoft.AspNetCore.Mvc;
using ShopProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ShopProject.Models.Shop;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopProject.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ShopDbContext _context;
        public CartController(ShopDbContext context)
        {
            _context = context;
        }


        public IActionResult CartIndex()
        {


            var userId = User.FindFirst(ClaimTypes.Sid).Value;
            var itemlist = _context.Carts.Where(x => x.UserId == userId).ToList();
            ViewModel viewModel = new ViewModel()
            {
                carts = itemlist
            };
            if (viewModel.carts.Count() == 0)
            {
                ViewData["Error"] = "Sorry,there are no items in your cart!";
                return View(viewModel);

            }


            return View(viewModel);
        }

        //AddtoCart還沒完成，問題點在，不知道要怎麼把使用者想加入購物車的商品型號接過來用=>已解決，使用asp-route-{value},詳細筆記如下:

        //AddtoCart括號裡的參數，指的是從View那邊傳過來的資料，這邊因為要比對商品的Model所以寫入string Model
        //對應的code是在商品頁面，購物車的<a>連結裡面寫的asp-route-model[{value}]="@item.Model"


        public async Task<IActionResult> Additem(long model)
        {
            var userId = User.FindFirst(ClaimTypes.Sid).Value;
            var itemList = _context.Carts.Where(x => x.Model == model && x.UserId == userId).ToList();
            var item = itemList.FirstOrDefault();

            item.Amount = item.Amount + 1;
            _context.SaveChanges();


            var product = _context.Products.Where(x => x.Model == model).ToList().FirstOrDefault();
            product.Inventory = product.Inventory - 1;
            _context.Update(product);
            _context.SaveChanges();





            string itemcount = _context.Carts.Where(x => x.UserId == userId).Select(x => x.Amount).Sum().ToString();
            HttpContext.Session.SetString("AmountItem", itemcount);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Decreaseitem(long model)
        {
            var userId = User.FindFirst(ClaimTypes.Sid).Value;
            var itemList = _context.Carts.Where(x => x.Model == model && x.UserId == userId).ToList();
            var item = itemList.FirstOrDefault();


            var product = _context.Products.Where(x => x.Model == model).ToList().FirstOrDefault();
            if (item.Amount > 1)
            {
                product.Inventory = product.Inventory + 1;
                _context.Update(product);
                _context.SaveChanges();
            }


            item.Amount = item.Amount - 1;
            if (item.Amount < 1)
            {
                item.Amount = 1;
            }
            _context.SaveChanges();
            string itemcount = _context.Carts.Where(x => x.UserId == userId).Select(x => x.Amount).Sum().ToString();

            HttpContext.Session.SetString("AmountItem", itemcount);
            return Redirect(Request.Headers["Referer"].ToString());
        }





        public IActionResult AddtoCart(long model, string name, int price, int amount, string imgurl, int newamount) //int newamount 為了下拉式選單而寫
        {

            var userId = User.FindFirst(ClaimTypes.Sid).Value;
            var itemList = _context.Carts.Where(x => x.Model == model && x.UserId == userId).ToList();
            var item = itemList.FirstOrDefault();




            if (item != null)
            {

                item.Amount += amount;

                //if(newamount != 0) 原本是為了下拉式選單選取商品數量而寫，後來沒用到
                //{
                //    item.Amount = newamount;
                //}
                _context.Update(item); //這邊要使用Update去更新購物車裡同樣商品的數量
                _context.SaveChanges(); //記得更改完之後要SaveChange，不然會寫不進資料庫

                var product = _context.Products.Where(x => x.Model == model).ToList().FirstOrDefault();
                product.Inventory = product.Inventory - amount;
                _context.Update(product);
                _context.SaveChanges();

            }
            else
            {

                var product = _context.Products.Where(x => x.Model == model).ToList().FirstOrDefault();
                product.Inventory = product.Inventory - amount;
                _context.Update(product);
                _context.SaveChanges();


                var newitem = new Cart()
                {

                    Name = name,
                    Model = model,
                    Price = price,
                    Amount = amount,
                    ImgUrl = imgurl,
                    UserId = userId

                };

                _context.Carts.Add(newitem);
                _context.SaveChanges();
            }
            string itemcount = _context.Carts.Where(x => x.UserId == userId).Select(x => x.Amount).Sum().ToString();

            HttpContext.Session.SetString("AmountItem", itemcount);
            //這個購物車數量要在顯示時判斷使用者是否登入，登入成功才會顯示並去撈使用者在購物車資料表的產品數量總數
            return Redirect(Request.Headers["Referer"].ToString()); //這邊要再查一下資料，我其實看不懂是複製的
        }

        public IActionResult DeleteItem(long model)
        {
            var userId = User.FindFirst(ClaimTypes.Sid).Value;
            var itemList = _context.Carts.Where(x => x.Model == model && x.UserId == userId).ToList();
            var item = itemList.FirstOrDefault();
            _context.Remove(item);
            _context.SaveChanges();

            var product = _context.Products.Where(x => x.Model == model).ToList().FirstOrDefault();
            product.Inventory = product.Inventory + item.Amount;
            _context.Update(product);
            _context.SaveChanges();



            string itemcount = _context.Carts.Where(x => x.UserId == userId).Select(x => x.Amount).Sum().ToString();

            HttpContext.Session.SetString("AmountItem", itemcount);
            return Redirect(Request.Headers["Referer"].ToString());


        }

        public IActionResult CheckOut(ViewModel viewModel)
        {
            var userId = User.FindFirst(ClaimTypes.Sid).Value; //確認目前使用者是誰
            var cartlist = _context.Carts.Where(x => x.UserId == userId).ToList(); //用使用者ID去撈購物車資料庫
            var totalprice = (cartlist.Sum(total => total.Price * total.Amount) + 60); //設定總價格變數
            var newpurchase = new Purchase() //NEW一個購物車物件並初始化(以購物車的資料進行賦值)
            {

                UserId = userId,
                CreationDate = DateTime.Now,
                TotalPrice = totalprice,
                Status = "Order processing",
                ReceiveAddress = viewModel.purchases.ReceiveAddress,
                ReceivePhoneNumber = viewModel.purchases.ReceivePhoneNumber,
                ReceiveName = viewModel.purchases.ReceiveName,
                Note = viewModel.purchases.Note


            };

            _context.Purchases.Add(newpurchase);
            _context.SaveChanges();
            foreach (var x in cartlist)
            {
                Order neworder = new Order()
                {
                    ProductModel = x.Model,
                    Amount = x.Amount,
                    Price = x.Price * x.Amount,
                    PurchaseId = newpurchase.Id,
                    ProductName = x.Name


                };
                _context.Orders.Add(neworder);
                _context.SaveChanges();
            }

            var carts = _context.Carts.Where(x => x.UserId == userId).ToList();
            foreach (var cart in carts)
            {
                _context.Remove(cart);
                _context.SaveChanges();

            }
            string itemcount = _context.Carts.Where(x => x.UserId == userId).Select(x => x.Amount).Sum().ToString();

            HttpContext.Session.SetString("AmountItem", itemcount);

            var purchases = _context.Purchases.Where(x => x.UserId == userId).ToList().LastOrDefault();

            return View(purchases);
        }





    }
}
