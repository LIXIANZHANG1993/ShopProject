using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopProject.Data;
using ShopProject.Models.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.Controllers
{
    [Authorize]
    public class AdminPurchaseController : Controller
    {
        private readonly ShopDbContext _context;

        public AdminPurchaseController(ShopDbContext context)
        {
            _context = context;
        }


        // GET: AdminPurchase
        public async Task<IActionResult> Index()
        {
            var shopDbContext = _context.Purchases;
            return View(await shopDbContext.ToListAsync());
        }


        // GET: AdminPurchase/Details/id?
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases.FirstOrDefaultAsync(m=>m.Id == id);
            var shopDbContext =  _context.Orders.Where(m => m.PurchaseId == id).ToList();
            
            
            ViewModel viewModel = new ViewModel()
            {
                purchases = purchase,
                Orders = shopDbContext
               
                
             };
           


            if (purchase == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: AdminPurchase/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: AdminCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CreationDate,TotalPrice,Status,ReceiveName,ReceivePhoneNumber,ReceiveAddress,Note")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchase);
        }



        // GET: AdminCustomers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases.FindAsync(id);
            ViewBag.changestate = new List<SelectListItem>()
            {
                new SelectListItem {Text="Order processing" , Value = "Order processing"},
                new SelectListItem {Text="Shipping status" , Value = "Shipping status"},
                new SelectListItem {Text="Order completed" , Value = "Order completed"},
                new SelectListItem {Text="Order cancelled" , Value = "Order cancelled"}
            };
            if (purchase == null)
            {
                return NotFound();
            }
            return View(purchase);
        }

        // POST: AdminCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId,CreationDate,TotalPrice,Status,ReceiveName,ReceivePhoneNumber,ReceiveAddress,Note")] Purchase purchase)
        {
            if (id != purchase.Id)
            {
                return NotFound();
            }

            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.Id))
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
            return View(purchase);
        }


        // GET: AdminCustomers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }



        // POST: AdminCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool PurchaseExists(Guid id)
        {
            return _context.Purchases.Any(e => e.Id == id);
        }









    }
}
