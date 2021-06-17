using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopProject.Data;
using ShopProject.Models.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ShopProject.Controllers
{
    public class ProductViewController : Controller
    {
        private readonly ShopDbContext _db;

        public ProductViewController (ShopDbContext db)
        {
            _db = db;
        }
        public IActionResult TopIndex(int page = 1)
        {
            IEnumerable<Product> objlist = _db.Products.Where(x => x.TypeId == 1);
            return View(objlist.ToPagedList(page,4));
        }
        public IActionResult BottomIndex(int page = 1)
        {
            IEnumerable<Product> objlist = _db.Products.Where(x => x.TypeId == 2);
            return View(objlist.ToPagedList(page, 4));
        }
        public IActionResult ShoesIndex(int page = 1)
        {
            IEnumerable<Product> objlist = _db.Products.Where(x => x.TypeId == 3);
            return View(objlist.ToPagedList(page, 4));
        }
        public IActionResult AccessoriesIndex(int page = 1)
        {
            IEnumerable<Product> objlist = _db.Products.Where(x => x.TypeId == 4);
            return View(objlist.ToPagedList(page, 4));
        }
        public IActionResult ProductDetails(long model)
        {
            IEnumerable<Product> objlist = _db.Products.Where(x => x.Model == model);
            //這裡是為了讓下拉式選單能夠有選項並且有值可以寫入新的訂購數量
            ViewBag.AddHowMany = new List<SelectListItem>()
            {
                new SelectListItem {Text="1", Value="1" },
                new SelectListItem {Text="2", Value="2" },
                new SelectListItem {Text="3", Value="3" },
                new SelectListItem {Text="4", Value="4" },
                new SelectListItem {Text="5", Value="5" },
                new SelectListItem {Text="6", Value="6" },
                new SelectListItem {Text="7", Value="7" },
                new SelectListItem {Text="8", Value="8" },
                new SelectListItem {Text="9", Value="9" },
                new SelectListItem {Text="10", Value="10" },
            };

            return View(objlist);
        }
        public IActionResult SearchProduct(string search, int page = 1)
        {




            if (search != null)
            {
                TempData["HoldSearch"] = search;
                TempData.Keep();

                IEnumerable<Product> product = _db.Products.Where(s => s.Name.Contains(search));

                return View(product.ToPagedList(page, 10));
            }
            TempData["HoldSearch"] = null;
            TempData.Keep();


            return Redirect(Request.Headers["Referer"].ToString());
        }

    }
}
