using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.Models.Shop
{
    public class Order //訂單明細
    {
        public int Id { get; set; }
        public Guid PurchaseId { get; set; }
        public long ProductModel { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }

        public string ProductName { get; set; }

        public virtual Product Product { get; set; }
        public virtual Purchase Purchase { get; set; }
    }
}
