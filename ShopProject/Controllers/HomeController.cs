using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopProject.Data;
using ShopProject.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ShopDbContext _context;

        public HomeController(ILogger<HomeController> logger,ShopDbContext context)
        {
            _logger = logger;
            _context = context;
            
        }
       
       

        public IActionResult Index()
        {
            //if (User.Identity.IsAuthenticated == true)
            //{
            //    var userId = User.FindFirst(ClaimTypes.Sid).Value;
            //    string itemcounts = _context.Carts.Where(x => x.UserId == userId).Select(x => x.Amount).Sum().ToString();

            //    HttpContext.Session.SetString("AmountItem", itemcounts);
            //};


            if (HttpContext.Session.GetString("Admin") == "Admin")
            {
                return RedirectToAction("Index", "Admin"); //確保使用者未使用LogOut後再次訪問網頁後顯示錯誤的Navbar
            }

            return View();
        }

       


        public IActionResult ShopIndex()
        {


            return View();
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
