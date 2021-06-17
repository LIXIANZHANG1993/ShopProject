using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopProject.Data;
using ShopProject.Models.Shop;

namespace ShopProject.Controllers
{
    public class AdminProductsController : Controller
    {
        private readonly ShopDbContext _context;
       

        public AdminProductsController(ShopDbContext context)
        {
            _context = context;
           
        }

        // GET: AdminProducts
        public async Task<IActionResult> Index()
        {
            var shopDbContext = _context.Products.Include(p => p.Type);
            return View(await shopDbContext.ToListAsync());
        }

        // GET: AdminProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: AdminProducts/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.productTypes, "Id", "Name"); //(選擇資料表的每一列資料,每一列其中id欄位對應的name值,id=1時 name的欄位值是top)
            //ViewBag.AddTypeName = new List<SelectListItem>()
            //{
            //    new SelectListItem {Text="上衣", Value="1" },
            //    new SelectListItem {Text="褲子", Value="2" },
            //    new SelectListItem {Text="鞋子", Value="3" },
            //    new SelectListItem {Text="飾品", Value="4" }

            //};

            var newModel = _context.Products.ToList().LastOrDefault().Model + 1;
            ViewBag.newModel = newModel;


            return View();
        }

        // POST: AdminProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Model,Price,CreationDate,TypeId,Amount,ImgUrl,Inventory")] Product product)
        {
            if (ModelState.IsValid)
            {
              
                var filePath = Path.Combine("~/", "images/", product.ImgUrl); //組合要上傳的圖片路徑(前端頁面使用 type="file")
                product.ImgUrl = filePath; //把組合好的路徑存到商品的ImgUrl欄位
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.productTypes, "Id", "Id", product.TypeId);
            return View(product);
        }

        // GET: AdminProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.productTypes, "Id", "Name", product.TypeId);
            return View(product);
        }

        // POST: AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Model,Price,CreationDate,TypeId,Amount,ImgUrl")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.productTypes, "Id", "Id", product.TypeId);
            return View(product);
        }

        // GET: AdminProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
