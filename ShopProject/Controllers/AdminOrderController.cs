using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopProject.Data;
using ShopProject.Models.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.Controllers
{
    public class AdminOrderController : Controller
    {
        private readonly ShopDbContext _context;

        public AdminOrderController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: AdminPurchase/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }




        // POST: AdminPurchase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PurchaseId,ProductModel,Amount,Price,ProductName")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details","AdminPurchase",new { id = order.PurchaseId}); //用new { id = order.PurchaseId}是因為，AdminPurchase/Details 需要傳入Guid id這個參數，為了傳遞這個參數所以使用
            }
            return View(order);
        }







        // GET: AdminPurchase/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }







        // POST: AdminOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();


            var getpurchaseid = order.PurchaseId;

            var purchaselist = _context.Orders.Where(x => x.PurchaseId == getpurchaseid).ToList();
            var totalprice = (purchaselist.Sum(total => total.Price * total.Amount) + 60);
            var purchasetotalprice = _context.Purchases.Where(x => x.Id == getpurchaseid).FirstOrDefault();
            purchasetotalprice.TotalPrice = totalprice;
            _context.SaveChanges();

            return RedirectToAction("Details", "AdminPurchase", new { id = order.PurchaseId }); ;
        }



        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }


    }
}
