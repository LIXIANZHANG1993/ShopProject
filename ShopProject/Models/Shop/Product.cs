using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopProject.Models.Shop
{
    public class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public long Model { get; set; }
        public int Price { get; set; }
        public DateTime CreationDate { get; set; }
        public int TypeId { get; set; }

        public int Inventory { get; set; }





        public string ImgUrl { get; set; }

        public virtual ProductType Type { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
